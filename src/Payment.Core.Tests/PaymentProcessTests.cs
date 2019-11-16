using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Payment.Core.Tests
{
    public class PaymentProcessTests
    {
        [Fact]
        public async Task ShouldFailIfBankNotAuthorizeAsync()
        {
            var bank = new Mock<IBankClient>();
            var repo = new Mock<IPaymentRepository>();
            
            var payment = new ValidPaymentData(
                cardNumber:"0000-0000-0000-0000",
                expiry: new DateTimeOffset(2020, 01, 01, 0, 0,0, TimeSpan.Zero),
                amount: 100,
                currency: "USD",
                cvv: "000");

            var bankRequest = new BankRequest("data");

            bank.Setup(m => m.CreateBankRequestAsync(
                    It.Is<ValidPaymentData>(d => d.CardNumber == payment.CardNumber)))
                .ReturnsAsync(bankRequest);

            bank.Setup(m => m.AuthorizeAsync(
                    It.Is<BankRequest>(r => r.Data == bankRequest.Data)))
                .ReturnsAsync(Result<BankRequestId>.CreateFailure);

            var sut = new ProcessPayment(bank.Object, repo.Object);
            var actual = await sut.ProcessAsync(payment);

            Assert.False(actual.IsSuccess);
        }

        [Fact]
        public async Task ShouldSuccessIfBankAuthorizeAsync()
        {
            var bank = new Mock<IBankClient>();
            var repo = new Mock<IPaymentRepository>();

            var payment = new ValidPaymentData(
                cardNumber: "0000-0000-0000-0000",
                expiry: new DateTimeOffset(2020, 01, 01, 0, 0, 0, TimeSpan.Zero),
                amount: 100,
                currency: "USD",
                cvv: "000");

            var bankRequest = new BankRequest("data");

            bank.Setup(m => m.CreateBankRequestAsync(
                    It.Is<ValidPaymentData>(d => d.CardNumber == payment.CardNumber)))
                .ReturnsAsync(bankRequest);

            bank.Setup(m => m.AuthorizeAsync(
                    It.Is<BankRequest>(r => r.Data == bankRequest.Data)))
                .ReturnsAsync(Result<BankRequestId>.CreateSuccess(new BankRequestId("Id")));

            var sut = new ProcessPayment(bank.Object, repo.Object);
            var actual = await sut.ProcessAsync(payment);

            Assert.False(actual.IsSuccess);
        }
    }
}
