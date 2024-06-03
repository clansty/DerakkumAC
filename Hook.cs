using DB;
using HarmonyLib;
using Main;
using Manager;
using MelonLoader;
using Process;

namespace DerakkumAC
{
    public class Hook
    {
        private static IGenericManager _genericManager = null;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(GameMainObject), "Update")]
        public static void OnGameMainObjectUpdate()
        {
            if (InputManager.GetButtonPush(0, InputManager.ButtonSetting.Select) && InputManager.GetButtonDown(1, InputManager.ButtonSetting.Select))
            {
                if (_genericManager is null) return;
                Mqtt.TurnOnAC();
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ProcessManager), "SetMessageManager")]
        public static void OnSetMessageManager(IGenericManager genericManager)
        {
            _genericManager = genericManager;
            MelonLogger.Msg("MessageManagerSet");
        }

        public static void ShowMessage(string message, WindowSizeID size = WindowSizeID.Middle)
        {
            _genericManager?.Enqueue(0, WindowMessageID.CollectionAttentionEmptyFavorite, new WindowParam()
            {
                hideTitle = true,
                replaceText = true,
                text = message,
                changeSize = true,
                sizeID = size
            });
        }
    }
}