using System.IO.Compression;
using System.Text;

using BDTheque.Web;
using BDTheque.Web.Middlewares;
using BDTheque.Web.Services;

using HotChocolate.AspNetCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using Serilog;
using Serilog.Events;

using Path = System.IO.Path;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    if (!Environment.UserInteractive)
    {
        string? pathToExe = Environment.ProcessPath;
        string? pathToContentRoot = Path.GetDirectoryName(pathToExe);
        if (!string.IsNullOrEmpty(pathToContentRoot))
        {
            Log.Information("Set current directory {CurrentDirectory}", pathToContentRoot);
            Directory.SetCurrentDirectory(pathToContentRoot);
        }
    }

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Configuration.Sources.Clear();
    builder.Configuration
        .AddContextualJsonFile("Settings/logging.json", false, true)
        .AddContextualJsonFile("Settings/database.json")
        .AddContextualJsonFile("Settings/server.json")
        .AddContextualJsonFile("Settings/authentication.json", false, true)
        .AddContextualJsonFile("Settings/system.json", true)
        .AddContextualJsonFile("Settings/appsettings.json", true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();

    builder.Host.UseSerilog(
        (ctx, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(ctx.Configuration);
            if (builder.Environment.IsDevelopment())
                loggerConfiguration.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information);
            loggerConfiguration.Filter
                .ByExcluding(logEvent => logEvent.Exception is HostAbortedException);
        }
    );

    builder.Services
        .AddHttpContextAccessor()
        .Configure<RouteOptions>(options => options.LowercaseUrls = true)
        .AddResponseCompression(
            options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            }
        )
        .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
        .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
                };
            }
        );

    builder.Services.AddHealthChecks();

    builder.Services.AddControllers().AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
#if RELEASE
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
#endif
        }
    );

    builder.Services
        .SetupApp(
            new ConfigureServices.Options(builder.Configuration)
            {
                Debug = builder.Environment.IsDevelopment()
            }
        );

    WebApplication app = builder.Build();

    #region Configure the HTTP request pipeline.

    app.UseMiddleware<RequestLoggingMiddleware>();
    app.UseMiddleware<ExceptionMiddleware>();

    // app.UseStaticFiles();

    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseRouting();

    app.UseWebSockets(); // for GraphQL subscriptions
    app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    // Middleware pour l'authentification et l'autorisation
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseResponseCompression();

    #endregion

    #region endpoints

    app.MapHealthChecks(Urls.HealthCheck);

    app.MapGraphQL().WithOptions(
        new GraphQLServerOptions
        {
            Tool =
            {
                Enable = app.Environment.IsDevelopment(),
                ServeMode = app.Environment.IsDevelopment() ? GraphQLToolServeMode.Latest : GraphQLToolServeMode.Embedded,
                Title = "BDTheque GraphQL API",
                DisableTelemetry = false,
                UseBrowserUrlAsGraphQLEndpoint = true
            }
        }
    );

    // if (!app.Environment.IsDevelopment())
    // {
    //     app.UseExceptionHandler("/error");
    //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //     app.UseHsts();
    // }

    #endregion

    app.Lifetime.ApplicationStarted.Register(() => OnStarted(app));

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shutdown complete");
    await Log.CloseAndFlushAsync();
}

return;

static void OnStarted(WebApplication app)
{
    foreach (string appUrl in app.Urls)
    {
        Log.Information("Health check on: {HealthCheckUrl}", new Uri(new Uri(appUrl), Urls.HealthCheck));
        Log.Information("GraphQL on: {GraphQLUrl}", new Uri(new Uri(appUrl), Urls.GraphQL));
    }
}
