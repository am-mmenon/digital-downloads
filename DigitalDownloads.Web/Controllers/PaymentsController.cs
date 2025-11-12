using DigitalDownloads.Core.Config;
using DigitalDownloads.Core.Data;
using DigitalDownloads.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace DigitalDownloads.Web.Controllers
{
    [Route("[controller]")]
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly StripeSettings _stripeSettings;

        public PaymentsController(AppDbContext context, IOptions<StripeSettings> stripeOptions)
        {
            _context = context;
            _stripeSettings = stripeOptions.Value;
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromForm] int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound();

            var domain = $"{Request.Scheme}://{Request.Host}/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        PriceData=new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal=product.Price*100,
                            Currency="usd",
                            ProductData=new SessionLineItemPriceDataProductDataOptions
                            {
                                Name=product.Name
                            }
                        },
                        Quantity=1
                    }
                },
                Mode = "payment",
                SuccessUrl = domain + "payments/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "payments/cancel"

            };

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }

        [HttpGet("success")]
        public IActionResult Success(string session_id)
        {
            var service = new SessionService();
            var session = service.Get(session_id);
            var paymentIntentId = session.PaymentIntentId;

            // Save order to DB (simplified)
            var order = new Order
            {
                StripePaymentId = paymentIntentId ?? "",
                TotalAmount = (decimal)(session.AmountTotal / 100.0),
                CreatedAt = DateTime.UtcNow
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            return View(); // We'll add a simple Success.cshtml view
        }

        [HttpGet("cancel")]
        public IActionResult Cancel() => View();
    }

}

