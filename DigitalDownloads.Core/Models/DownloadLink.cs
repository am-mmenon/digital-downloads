

namespace DigitalDownloads.Core.Models
{
    public class DownloadLink
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public string BlobUrl { get; set; } = "";
        public DateTime ExpiryTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
