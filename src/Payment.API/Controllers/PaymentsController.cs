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

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(string id) =>
             new JsonResult(await _paymentRepository.GetAsync(new PaymentId(id)));

        //add validation
        [HttpPost]
        public async Task<JsonResult> Post(ValidPaymentRequest request)
        {
            var result = await _paymentProcessing.ProcessAsync(request);

            //GetPaymentID method?
            //mapping shouldHappen here? 
            var paymentId = result.Value;
            return new JsonResult(new PaymentResult(
                paymentId.Value,
                result.IsSuccess ? Status.Success : Status.Failure));
        }
    }
}