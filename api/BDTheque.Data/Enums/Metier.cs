namespace BDTheque.Data.Enums;

using BDTheque.Extensions;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder RegisterEnums(this ModelBuilder builder)
        => builder.HasPostgresEnum("Metier".ToSnakeCase(), ["Scenariste", "Dessinateur", "Coloriste"]);
}
