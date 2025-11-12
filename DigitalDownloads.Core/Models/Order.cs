using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDownloads.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; } // nullable foreign key
        public Customer? Customer { get; set; }

        public decimal TotalAmount { get; set; }
        public string StripePaymentId { get; set; } = ""; // from Stripe API
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<DownloadLink> DownloadLinks { get; set; } = new List<DownloadLink>();
    }
}
