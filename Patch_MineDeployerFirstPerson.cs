﻿using HarmonyLib;
using UnityEngine;

namespace Hikaria.ToolPlacementIndicatorPlus
{
    [HarmonyPatch(typeof(MineDeployerFirstPerson))]
    internal static class Patch_MineDeployerFirstPerson
    {
        [HarmonyPatch(nameof(MineDeployerFirstPerson.Update)), HarmonyPostfix]
        public static void MineDeployerFirstPerson__Update__Postfix(MineDeployerFirstPerson __instance)
        {
            if (!AssetsHelper.Initialized)
                return;
            if (CurrentLineRenderer == null)
                return;
            if (__instance.m_hasRayHit && __instance.m_lastCanPlace && __instance.CanWield)
            {
                if (!CurrentLineRenderer.gameObject.active)
                    CurrentLineRenderer.gameObject.SetActive(true);

                CurrentLineRenderer.SetPosition(0, __instance.m_lastRayHit.point);
                CurrentLineRenderer.SetPosition(1, __instance.m_lastRayHit.point + __instance.m_lastRayHit.normal * 5f);
            }
            else if (CurrentLineRenderer.gameObject.active)
            {
                CurrentLineRenderer.gameObject.SetActive(false);
            }
        }

        [HarmonyPatch(nameof(MineDeployerFirstPerson.OnWield)), HarmonyPostfix]
        public static void MineDeployerFirstPerson__OnWield__Postfix(MineDeployerFirstPerson __instance)
        {
            if (!AssetsHelper.Initialized)
                return;
            if (CurrentLineRenderer == null)
            {
                CurrentLineRenderer = AssetsHelper.GetLineRenderer();
                //CurrentLineRenderer.SetColors(LineColor, LineColor);
            }
        }

        [HarmonyPatch(nameof(MineDeployerFirstPerson.OnUnWield)), HarmonyPostfix]
        public static void MineDeployerFirstPerson__OnUnWield__Postfix(MineDeployerFirstPerson __instance)
        {
            if (!AssetsHelper.Initialized)
                return;
            CurrentLineRenderer.gameObject.SetActive(false);
            //if (CurrentLineRenderer != null)
            //    GameObject.Destroy(CurrentLineRenderer);
        }

        private static LineRenderer CurrentLineRenderer;

        //private static readonly Color LineColor = new(0.5f, 0.5f, 0f, 0.3f);
    }
}
