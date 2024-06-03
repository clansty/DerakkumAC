namespace DerakkumAC
{
    public class Config
    {
        public string MqttHost { get; set; }
        public string MqttUser { get; set; }
        public string MqttPass { get; set; }
        public string PublishTopic { get; set; }
        public string PublishContent { get; set; }
        public string SubscribeTopic { get; set; }
    }
}