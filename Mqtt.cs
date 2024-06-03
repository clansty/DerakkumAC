using System.Threading;
using System.Threading.Tasks;
using MelonLoader;
using MQTTnet;
using MQTTnet.Client;

namespace DerakkumAC
{
    public class Mqtt
    {
        private static IMqttClient _mqttClient;

        public static void Init()
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(Program.Config.MqttHost).WithCredentials(Program.Config.MqttUser, Program.Config.MqttPass).Build();
            Task.Run(async () =>
            {
                await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                MelonLogger.Msg("MQTT Connected");
                _mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    var payload = e.ApplicationMessage.ConvertPayloadToString();
                    MelonLogger.Msg(payload);
                    Hook.ShowMessage(payload);

                    return Task.CompletedTask;
                };
                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(f => { f.WithTopic(Program.Config.SubscribeTopic); })
                    .Build();
                await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            });
        }

        public static async Task TurnOnAC()
        {
            var message = new MqttApplicationMessageBuilder().WithTopic(Program.Config.PublishTopic).WithPayload(Program.Config.PublishContent).Build();
            await _mqttClient.PublishAsync(message, CancellationToken.None);
            MelonLogger.Msg("AC Turned On");
        }
    }
}