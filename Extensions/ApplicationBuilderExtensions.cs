using StepIn.Services.OrderAPI.Messaging;
using System.Runtime.CompilerServices;

namespace StepIn.Services.OrderAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationlife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationlife.ApplicationStarted.Register(OnStart);
            hostApplicationlife.ApplicationStopped.Register(OnStop);
            return app;
        }
        private static void OnStart() 
        {
            ServiceBusConsumer.Start();
        }
        private static void OnStop()
        {
            ServiceBusConsumer.Stop();
        }
    } 
}
