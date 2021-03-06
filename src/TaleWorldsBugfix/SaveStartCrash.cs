﻿using GCO.ReversePatches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem.Load;
using static HarmonyLib.AccessTools;

namespace GCO.TaleWorldsBugfix
{
    [HarmonyPatch]
    class SaveStartCrash
    {
        [HarmonyPatch(typeof(VillageType), "GetProductionPerDay", typeof(ItemCategory))]
        [HarmonyPrefix]
        public static bool GetProductionPerDay(ref VillageType __instance, ref float __result, ItemCategory itemCategory)
        {
            float num = 0f;
            var _productions = MissionAccessTools.Get_productions(ref __instance);
            foreach (ValueTuple<ItemObject, float> valueTuple in _productions)
            {
                if (valueTuple.Item1 != null)
                {
                    if (valueTuple.Item1.ItemCategory == itemCategory)
                    {
                        num += valueTuple.Item2;
                    }
                }
            }
            __result = num;

            return false;
        }
    }


    [HarmonyPatch]
    class TournamentBug
    {
        [HarmonyPatch(typeof(TournamentManager), "GetTournamentGame")]
        [HarmonyPrefix]
        public static void GetTournamentGamePrefix(TournamentManager __instance, ref List<TournamentGame> ____activeTournaments, Town town)
        {
            if (____activeTournaments != null)
            {
                ____activeTournaments.RemoveAll(x => x == null);
            }
        }    
    }


    [HarmonyPatch]
    class DisperseArmyBug
    {
        [HarmonyPatch(typeof(Army), "DisperseArmy")]
        [HarmonyPrefix]
        public static bool DisperseArmy(ref Army __instance, ref bool ____armyIsDispersing, ref List<MobileParty> ____parties, ref MBCampaignEvent ____hourlyTickEvent,  Army.ArmyDispersionReason reason = Army.ArmyDispersionReason.Unknown)
        {
            try
            {
                if (____armyIsDispersing)
                {
                    return false;
                }
                CampaignEventDispatcher.Instance.OnArmyDispersed(__instance, reason, __instance.Parties.Contains(MobileParty.MainParty));
                ____armyIsDispersing = true;
                int num = 0;
                for (int i = __instance.Parties.Count - 1; i >= num; i--)
                {
                    __instance.Parties[i].Army = null;
                }
                ____parties.Clear();
                __instance.Kingdom = null;
                if (__instance.LeaderParty == MobileParty.MainParty)
                {
                    MapState mapState = Game.Current.GameStateManager.ActiveState as MapState;
                    if (mapState != null)
                    {
                        mapState.OnDispersePlayerLeadedArmy();
                    }
                }
                Campaign.Current.DeletePeriodicEvent(____hourlyTickEvent);
                ____armyIsDispersing = false;

            }
            catch (Exception ex) 
            {
                InformationManager.DisplayMessage(new InformationMessage($"GCO prevented crash with error {ex.Message}"));
            }
           
            return false;
        }
    }

    [HarmonyPatch]
    class QuestFailedCrashBug
    {
        [HarmonyPatch(typeof(CampaignEvents), "OnQuestStarted")]
        [HarmonyPrefix]
        public static bool DisperseArmy(ref CampaignEvents __instance, ref MbEvent<QuestBase> ____onQuestStartedEvent, QuestBase quest)
        {
            try
            {
                if (____onQuestStartedEvent != null)
                {
                    ____onQuestStartedEvent.Invoke(quest);
                }
            }
            catch (Exception ex)
            {

                InformationManager.DisplayMessage(new InformationMessage($"GCO prevented quest crash with error {ex.Message}"));
            }
        
           
            return false;
        }
    }
}


    //[HarmonyPatch(typeof(TournamentCampaignBehavior), "ConsiderStartOrEndTournament")]
    //[HarmonyPrefix]
    //private bool ConsiderStartOrEndTournamentPrefix(Town town)
    //{
    //    ITournamentManager tournamentManager = Campaign.Current.TournamentManager;
    //    if (town != null)
    //    {
    //        TournamentGame tournamentGame = tournamentManager.GetTournamentGame(town);
    //        if (tournamentGame == null)
    //        {
    //            if (MBRandom.RandomFloat < 0.4f && MBRandom.RandomFloat < Campaign.Current.Models.TournamentModel.GetTournamentStartChance(town))
    //            {
    //                tournamentManager.AddTournament(Campaign.Current.Models.TournamentModel.CreateTournament(town));
    //                return false;
    //            }
    //        }
    //        else if (MBRandom.RandomFloat < Campaign.Current.Models.TournamentModel.GetTournamentEndChance(tournamentGame))
    //        {
    //            tournamentManager.ResolveTournament(tournamentGame);
    //        }
    //    }
    //    return false;
    //}




