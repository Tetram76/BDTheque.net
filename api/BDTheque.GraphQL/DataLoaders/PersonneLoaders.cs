namespace BDTheque.GraphQL.DataLoaders;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;

public static class PersonneLoaders
{
    [DataLoader]
    internal static async Task<Personne> GetPersonneByIdAsync([ID] Guid id, IPersonneRepository personneRepository, CancellationToken cancellationToken) =>
        await personneRepository.GetById(id, cancellationToken);

    [DataLoader]
    internal static Task<IQueryable<Personne>> GetPersonneByNomAsync(string nom, BDThequeContext context) =>
        Task.FromResult(
            context.Personnes
#pragma warning disable CA1862 // cannot use string.Contains as it is not supported for conversion to SQL statements
                .Where(personne => personne.Nom.ToLower().Contains(nom.ToLower()))
#pragma warning restore CA1862
                .AsQueryable()
        );

    [DataLoader]
    public static async Task<IQueryable<Album>> GetPersonneAlbums(Personne personne, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(personne).Collection(p => p.Auteurs).LoadAsync(cancellationToken);
        return personne.Auteurs.SelectMany(auteur => auteur.AuteursAlbums).Select(auteurAlbum => auteurAlbum.Album).AsQueryable();
    }

    [DataLoader]
    public static async Task<IQueryable<Serie>> GetPersonneSeries(Personne personne, BDThequeContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Entry(personne).Collection(p => p.Auteurs).LoadAsync(cancellationToken);
        return personne.Auteurs.SelectMany(auteur => auteur.AuteursSeries).Select(auteurSerie => auteurSerie.Serie).AsQueryable();
    }
}
