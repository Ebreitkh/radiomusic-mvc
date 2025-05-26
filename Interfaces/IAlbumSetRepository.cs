using MusicRadio.Models;
using System.Collections.Generic;

namespace MusicRadio.Interfaces
{
    interface IAlbumSetRepository
    {
        List<AlbumSet> GetAll();
        AlbumSet GetById(int id);
        void Add(AlbumSet albumSet);
        void Update(AlbumSet albumSet);
        void Delete(int id);
    }
}
