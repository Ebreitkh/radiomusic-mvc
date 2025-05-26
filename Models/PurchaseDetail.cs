using System;

namespace MusicRadio.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int AlbumId { get; set; }
        public double Total { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}