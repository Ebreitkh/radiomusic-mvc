using MusicRadio.Models;
using System.Collections.Generic;

namespace MusicRadio.Interfaces
{
    interface ISongSetRepository
    {
        List<SongSet> GetAll();
        SongSet GetById(int id);
        void Add(SongSet songSet);
        void Update(SongSet songSet);
        void Delete(int id);
    }
}
