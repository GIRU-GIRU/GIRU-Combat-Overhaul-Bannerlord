﻿using HarmonyLib;
using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using System.Collections.Generic;
using TaleWorlds.Engine;
using static HarmonyLib.AccessTools;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Definition;
using TaleWorlds.SaveSystem.Load;

namespace GCO.ReversePatches
{
    class MissionAccessTools
    {
        private static FieldRef<Mission, Dictionary<int, Mission.Missile>> accessTools_missiles = FieldRefAccess<Mission, Dictionary<int, Mission.Missile>>("_missiles");

        private static FieldRef<OrderController, List<Formation>> accessTools_selectedFormations = FieldRefAccess<OrderController, List<Formation>>("_selectedFormations");

        private static FieldRef<OrderController, Team> accessTools_team = FieldRefAccess<OrderController, Team>("_team");

        //private static MethodInfo accessTools_GetAttackCollisionResults = AccessTools.Method(typeof(Mission), "GetAttackCollisionResults", new Type[] {
        //            typeof(Agent),
        //            typeof(Agent),
        //            typeof(GameEntity),
        //            typeof(float),
        //            typeof(AttackCollisionData).MakeByRefType(),
        //            typeof(bool),
        //            typeof(bool),
        //            typeof(WeaponComponentData).MakeByRefType() });

        //internal static void GetAttackCollisionResults(ref Mission __instance, Agent attacker, Agent victim, GameEntity hitObject, float momentumRemaining, ref AttackCollisionData attackCollisionData, bool crushedThrough, bool cancelDamage, out WeaponComponentData shieldOnBack)
        //{
        //    shieldOnBack = null;
        //    var obj = new object[] { attacker, victim, hitObject, momentumRemaining, attackCollisionData, crushedThrough, cancelDamage, shieldOnBack };

        //    accessTools_GetAttackCollisionResults.Invoke(__instance, obj);
        //    attackCollisionData = (AttackCollisionData)obj[4];
        //}
        internal static Dictionary<int, Mission.Missile> Get_missiles(ref Mission __instance)
        {
            return accessTools_missiles(__instance);

        }

        internal static List<Formation> Get_selectedFormations(ref OrderController __instance)
        {
            return accessTools_selectedFormations(__instance);
        }

        internal static Team Get_team(ref OrderController __instance)
        {
            return accessTools_team(__instance);
        }

        internal static FieldRef<VillageType, ValueTuple<ItemObject, float>[]> accessTools_productions = FieldRefAccess<VillageType, ValueTuple<ItemObject, float>[]>("_productions");
        internal static ValueTuple<ItemObject, float>[] Get_productions(ref VillageType __instance)
        {
            return accessTools_productions(__instance);
        }

    }
}
