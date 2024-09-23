using AssetShards;
using UnityEngine;

namespace Hikaria.ToolPlacementIndicatorPlus
{
    internal static class AssetsHelper
    {
        public static void Initialize()
        {
            AssetShardManager.add_OnStartupAssetsLoaded(new Action(LoadAssets));
        }

        private static void LoadAssets()
        {
            if (!AssetShardManager.s_loadedAssetsLookup.TryGetValue(EXPLOSIVE_MINE_PREFAB, out var obj))
            {
                Logs.LogFatal($"Unable to find EXPLOSIVE_MINE_PREFAB: {EXPLOSIVE_MINE_PREFAB}");
                return;
            }

            lineRenderPrefab = obj.Cast<GameObject>().GetComponent<MineDeployerInstance_Detect_Laser>().m_lineRenderer;

            Initialized = true;
        }

        public static LineRenderer GetLineRenderer()
        {
            var line = GameObject.Instantiate<LineRenderer>(lineRenderPrefab);
            GameObject.DontDestroyOnLoad(line);
            return line;
        }

        public static bool Initialized { get; private set; }

        public const string EXPLOSIVE_MINE_PREFAB = "ASSETS/ASSETPREFABS/ITEMS/TOOLS/MINEDEPLOYER/MINEDEPLOYERINSTANCE_EXPLOSIVE.PREFAB";

        public static LineRenderer lineRenderPrefab;
    }
}
