using System;
using System.Threading.Tasks;
using Moq;
using Payment.Core.Utility;
using Xunit;

namespace Payment.Core.Tests
{
    public class PaymentProcessingTests
    {
        [Fact]
        public async Task ShouldFailIfBankNotAuthorizeAsync()
        {
            var bank = new Mock<IBankClient>();
            var repo = new Mock<IPaymentRepository>();
            
            var request = new ValidPaymentRequest(
                cardNumber:"0000-0000-0000-0000",
                expiry: new DateTimeOffset(2020, 01, 01, 0, 0,0, TimeSpan.Zero),
                amount: 100,
                currency: "USD",
                cvv: "000");

            var bankRequest = new BankRequest("data");

            bank.Setup(m => m.CreateBankRequestAsync(
                    It.Is<ValidPaymentRequest>(d => d.CardNumber == request.CardNumber)))
                .ReturnsAsync(bankRequest);

            bank.Setup(m => m.AuthorizeAsync(
                    It.Is<BankRequest>(r => r.Data == bankRequest.Data)))
                .ReturnsAsync(Result<BankAuthorizationId>.CreateFailure);

            var sut = new PaymentProcessing(bank.Object, repo.Object);
            var actual = await sut.ProcessAsync(request);

            Assert.False(actual.IsSuccess);
        }

        [Fact]
        public async Task ShouldSuccessIfBankAuthorizeAsync()
        {
            var bank = new Mock<IBankClient>();
            var repo = new Mock<IPaymentRepository>();

            var request = new ValidPaymentRequest(
                cardNumber: "0000-0000-0000-0000",
                expiry: new DateTimeOffset(2020, 01, 01, 0, 0, 0, TimeSpan.Zero),
                amount: 100,
                currency: "USD",
                cvv: "000");

            var bankRequest = new BankRequest("data");

            bank.Setup(m => m.CreateBankRequestAsync(
                    It.Is<ValidPaymentRequest>(d => d.CardNumber == request.CardNumber)))
                .ReturnsAsync(bankRequest);

            bank.Setup(m => m.AuthorizeAsync(
                    It.Is<BankRequest>(r => r.Data == bankRequest.Data)))
                .ReturnsAsync(Result<BankAuthorizationId>.CreateSuccess(new BankAuthorizationId("Id")));

            var sut = new PaymentProcessing(bank.Object, repo.Object);
            var actual = await sut.ProcessAsync(request);

            Assert.True(actual.IsSuccess);
        }
    }
}
