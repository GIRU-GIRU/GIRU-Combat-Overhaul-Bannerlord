﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.SkinVoiceManager;

namespace GCO.ReversePatches
{
    [HarmonyPatch]
    public static class OrderControllerReversePatches
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(OrderController), "OnSelectedFormationsCollectionChanged")]
        internal static void OnSelectedFormationsCollectionChanged(this OrderController __instance)
        {
            throw new NotImplementedException("Need to patch first");
        }

    }
}
