using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDownloads.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string BlobFileName { get; set; } = ""; // Azure Blob filename
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
