using Azure.Messaging.ServiceBus;
using StepIn.Services.OrderAPI.Messages;
using StepIn.Services.OrderAPI.Models;
using StepIn.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using StepIn.MessageBus;

namespace StepIn.Services.OrderAPI.Messaging
{
  public class AzureServiceBusConsumer : IAzureServiceBusConsumer
  {

    private readonly string topicToMessageBus;
    private readonly string subscriptionToMessageBus;
    private readonly string connectionstringToMessageBus;


    private readonly IConfiguration _configuration;

    private readonly OrderRepository _orderRepository;

    private ServiceBusProcessor couponProcessor;

    private readonly IMessageBus _messageBus;

    public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration, IMessageBus messageBus)
    {
      _orderRepository = orderRepository;
      _configuration = configuration;
      _messageBus = messageBus;

      connectionstringToMessageBus = _configuration.GetValue<string>("ConnectionstringToMessageBus");
      topicToMessageBus = _configuration.GetValue<string>("TopicToMessageBus");
      subscriptionToMessageBus = _configuration.GetValue<string>("SubscriptionToMessageBus");

      var client = new ServiceBusClient(connectionstringToMessageBus);
      couponProcessor = client.CreateProcessor(topicToMessageBus, subscriptionToMessageBus);

    }

    public async Task Start()
    {
      couponProcessor.ProcessMessageAsync += OnCouponMessageReceived;
      couponProcessor.ProcessErrorAsync += ErrorHandler;
      await couponProcessor.StartProcessingAsync();

    }
    public async Task Stop()
    {
      await couponProcessor.StopProcessingAsync();
      await couponProcessor.DisposeAsync();
    }

    Task ErrorHandler(ProcessErrorEventArgs args)
    {
      Console.WriteLine(args.Exception.ToString());
      return Task.CompletedTask;
    }

    private async Task OnCouponMessageReceived(ProcessMessageEventArgs args)
    {
      var message = args.Message;
      if (message != null)
      {
        var body = Encoding.UTF8.GetString(message.Body);

        CouponDto couponDto = JsonConvert.DeserializeObject<CouponDto>(body);

        try
        {
          await _orderRepository.AddCoupon(couponDto);
          await args.CompleteMessageAsync(args.Message);

        }
        catch (Exception ex)
        {
          throw;
        }

      }
    }
  }
}
