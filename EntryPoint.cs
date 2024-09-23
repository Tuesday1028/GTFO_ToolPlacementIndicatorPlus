using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace Hikaria.ToolPlacementIndicatorPlus
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class EntryPoint : BasePlugin
    {
        public override void Load()
        {
            Instance = this;

            AssetsHelper.Initialize();

            m_Harmony = new(PluginInfo.GUID);
            m_Harmony.PatchAll();

            Logs.LogMessage("OK");
        }

        private Harmony m_Harmony;

        public static EntryPoint Instance { get; private set; }
    }
}
