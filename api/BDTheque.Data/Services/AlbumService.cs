namespace BDTheque.Data.Services;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

public class AlbumService(BDThequeContext context)
{
    public List<Album> GetAllAlbums() => context.Albums.ToList();
}
