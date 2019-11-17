using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Bank;
using Payment.Core;
using Payment.DatabaseAdapter;

namespace Payment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(o =>
                    o.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IPaymentProcessing, PaymentProcessing>();
            services.AddScoped<IBankClient, BankClient>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
