namespace BDTheque.Data.Entities;

using BDTheque.Data.Extensions;
using Microsoft.EntityFrameworkCore;

public static partial class ModelBuilderExtensions
{
    public static ModelBuilder RegisterEnums(this ModelBuilder builder)
        => builder.HasPostgresEnum("Metier".ToSnakeCase(), ["Scenariste", "Dessinateur", "Coloriste"]);
}
