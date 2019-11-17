using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Models;
using Payment.Core;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentProcessing _paymentProcessing;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsController(
            IPaymentProcessing paymentProcessing,
            IPaymentRepository paymentRepository)
        {
            _paymentProcessing = paymentProcessing;
            _paymentRepository = paymentRepository;
        }

        [HttpPost]
        public async Task<JsonResult> Post(PaymentRequest request)
        {
            var validationResult = ValidPaymentRequest.Create(request);

            var response = !validationResult.IsSuccess
                ? new PaymentResponse(validationResult.Errors)
                : new PaymentResponse(await _paymentProcessing.ProcessAsync(validationResult.Value));

            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var payment = await _paymentRepository.GetAsync(new PaymentId(id));
            if (payment != null)
            {
                return Ok(new PaymentDetailsResponse(payment));
            }

            return NotFound();
        }
    }
}