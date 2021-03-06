﻿using System;
using System.Reflection;
using GCO.GCOToolbox;
using GCO.Patches;
using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Screen;

namespace GCO.ModOptions
{
    internal static class HarmonyPatchesConfiguration
    {
        internal static void CleaveEnabledPatch(Harmony harmony)
        {
            var decideWeaponCollisionReaction = typeof(Mission).GetMethod("DecideWeaponCollisionReaction", BindingFlags.NonPublic | BindingFlags.Instance);
            var DecideWeaponCollisionReactionPostfix = typeof(MissionPatches).GetMethod(nameof(MissionPatches.DecideWeaponCollisionReactionPostfix), BindingFlags.NonPublic | BindingFlags.Static);

            var meleeHitCallback = typeof(Mission).GetMethod("MeleeHitCallback", BindingFlags.NonPublic | BindingFlags.Instance);
            var meleeHitCallbackPostfix = typeof(MissionPatches).GetMethod(nameof(MissionPatches.MeleeHitCallbackPostfix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(decideWeaponCollisionReaction, null, new HarmonyMethod(DecideWeaponCollisionReactionPostfix), null);
            harmony.Patch(meleeHitCallback, null, new HarmonyMethod(meleeHitCallbackPostfix), null);
        }

        internal static void SimplifiedSurrenderLogicEnabledPatch(Harmony harmony)
        {
            var doesSurrenderIsLogicalForParty = typeof(PartyBaseHelper).GetMethod("DoesSurrenderIsLogicalForParty");
            var doesSurrenderIsLogicalForPartyPostfix = typeof(PartyBaseHelperPatches).GetMethod(nameof(PartyBaseHelperPatches.DoesSurrenderIsLogicalForPartyPostfix), BindingFlags.NonPublic | BindingFlags.Static);

            var conversation_bandits_will_join_player_on_condition = typeof(BanditsCampaignBehavior).GetMethod("conversation_bandits_will_join_player_on_condition", BindingFlags.NonPublic | BindingFlags.Instance);
            var conversation_bandits_will_join_player_on_conditionPostfix = typeof(BanditsCampaignBehaviorPatches)
                                                                                .GetMethod(nameof(BanditsCampaignBehaviorPatches.conversation_bandits_will_join_player_on_conditionPostfix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(doesSurrenderIsLogicalForParty, null, new HarmonyMethod(doesSurrenderIsLogicalForPartyPostfix), null);
            harmony.Patch(conversation_bandits_will_join_player_on_condition, null,
                new HarmonyMethod(conversation_bandits_will_join_player_on_conditionPostfix), null);
        }

        internal static void StandardizedFlinchOnEnemiesEnablePatch(Harmony harmony)
        {
            var createBlow = typeof(Mission).GetMethod("CreateBlow", BindingFlags.NonPublic | BindingFlags.Instance);
            var createBlowPrefix = typeof(MissionPatches).GetMethod(nameof(MissionPatches.CreateBlowPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(createBlow, new HarmonyMethod(createBlowPrefix), null, null, null);
        }

        internal static void KillFriendliesPatch(Harmony harmony)
        {
            var cancelsDamageAndBlocksAttackBecauseOfNonEnemyCase = typeof(Mission).GetMethod("CancelsDamageAndBlocksAttackBecauseOfNonEnemyCase", BindingFlags.NonPublic | BindingFlags.Instance);
            var cancelsDamageAndBlocksAttackBecauseOfNonEnemyCasePrefix = typeof(MissionPatches).GetMethod(nameof(MissionPatches.CancelsDamageAndBlocksAttackBecauseOfNonEnemyCasePrefix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(cancelsDamageAndBlocksAttackBecauseOfNonEnemyCase,
                new HarmonyMethod(cancelsDamageAndBlocksAttackBecauseOfNonEnemyCasePrefix), null, null, null);
        }

        internal static void OrderVoiceCommandQueuingPatch(Harmony harmony)
        {
            var selectFormationMakeVoice = typeof(OrderController).GetMethod("SelectFormationMakeVoice", BindingFlags.NonPublic | BindingFlags.Static);
            var SelectFormationMakeVoicePrefix = typeof(OrderControllerPatches).GetMethod(nameof(OrderControllerPatches.SelectFormationMakeVoicePrefix), BindingFlags.NonPublic | BindingFlags.Static);

            var afterSetOrderMakeVoice = typeof(OrderController).GetMethod("AfterSetOrderMakeVoice", BindingFlags.NonPublic | BindingFlags.Static);
            var afterSetOrderMakeVoicePrefix = typeof(OrderControllerPatches).GetMethod(nameof(OrderControllerPatches.AfterSetOrderMakeVoicePrefix), BindingFlags.NonPublic | BindingFlags.Static);

            var selectAllFormations = typeof(OrderController).GetMethod("SelectAllFormations", BindingFlags.NonPublic | BindingFlags.Instance);
            var selectAllFormationsPrefix = typeof(OrderControllerPatches).GetMethod(nameof(OrderControllerPatches.SelectAllFormationsPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(selectAllFormations, new HarmonyMethod(selectAllFormationsPrefix), null, null, null);
            harmony.Patch(selectFormationMakeVoice, new HarmonyMethod(SelectFormationMakeVoicePrefix), null, null, null);
            harmony.Patch(afterSetOrderMakeVoice, new HarmonyMethod(afterSetOrderMakeVoicePrefix), null, null, null);
        }

        internal static void OrderControllerCameraImprovementsPatch(Harmony harmony)
        {
            var updateCamera = typeof(MissionScreen).GetMethod("UpdateCamera", BindingFlags.NonPublic | BindingFlags.Instance);
            var updateCameraPrefix = typeof(MissionScreenPatches).GetMethod(nameof(MissionScreenPatches.UpdateCameraPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(updateCamera, new HarmonyMethod(updateCameraPrefix), null, null, null);

            //var updateCamera = typeof(MissionScreen).GetMethod("UpdateCamera", BindingFlags.NonPublic | BindingFlags.Instance);
            //var updateCameraPostfix = typeof(MissionScreenPatches).GetMethod(nameof(MissionScreenPatches.UpdateCameraPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            //harmony.Patch(updateCamera, null, new HarmonyMethod(updateCameraPostfix), null, null);
        }

        internal static void ProjectileBalancingEnabledPatch(Harmony harmony)
        {
            var missileHitCallback = typeof(Mission).GetMethod("MissileHitCallback", BindingFlags.NonPublic | BindingFlags.Instance);
            var missileHitCallbackPrefix = typeof(MissionPatchesProjectile).GetMethod(nameof(MissionPatchesProjectile.MissileHitCallbackPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            //var getAttackCollisionResults = typeof(Mission).GetMethod("GetAttackCollisionResults", BindingFlags.NonPublic | BindingFlags.Instance);
            //var getAttackCollisionResultsPrefix = typeof(ProjectileBalanceLogic).GetMethod(nameof(ProjectileBalanceLogic.GetAttackCollisionResultsPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            var getWeaponSkill = typeof(AgentStatCalculateModel).GetMethod("GetWeaponSkill", BindingFlags.NonPublic | BindingFlags.Instance);
            var getWeaponSkillPostfix = typeof(MissionPatchesProjectile).GetMethod(nameof(MissionPatchesProjectile.GetWeaponSkillPostfix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(missileHitCallback, new HarmonyMethod(missileHitCallbackPrefix), null, null, null);
            harmony.Patch(getWeaponSkill, null, new HarmonyMethod(getWeaponSkillPostfix), null, null);



        }

    

        internal static void HyperArmorAndProjectileBalancing(Harmony harmony)
        {
            var getDefendCollisionResultsAux = typeof(Mission).GetMethod("GetDefendCollisionResultsAux", BindingFlags.NonPublic | BindingFlags.Static);
            var getDefendCollisionResultsAuxPrefix = typeof(MissionPatches).GetMethod(nameof(MissionPatches.GetDefendCollisionResultsAuxPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            var registerBlow = typeof(Mission).GetMethod("RegisterBlow", BindingFlags.NonPublic | BindingFlags.Instance);
            var registerBlowPrefix = typeof(MissionPatches).GetMethod(nameof(MissionPatches.RegisterBlowPrefix), BindingFlags.NonPublic | BindingFlags.Static);

            harmony.Patch(getDefendCollisionResultsAux, new HarmonyMethod(getDefendCollisionResultsAuxPrefix), null, null, null);
            harmony.Patch(registerBlow, new HarmonyMethod(registerBlowPrefix), null, null, null);
        }
    }
}
