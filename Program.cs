using System.IO;
using MelonLoader;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DerakkumAC
{
    internal class Program : MelonMod
    {
        public static Config Config { get; set; }

        public override void OnInitializeMelon()
        {
            if (!File.Exists("DerakkumAC.yaml"))
            {
                MelonLogger.Error("Please create a DerakkumAC.yaml file in the game directory");
            }

            var yamlFile = File.ReadAllText("DerakkumAC.yaml");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // see height_in_inches in sample yml 
                .Build();
            Config = deserializer.Deserialize<Config>(yamlFile);

            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Hook));
            Mqtt.Init();
        }
    }
}