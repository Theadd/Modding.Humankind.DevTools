using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Interop;
using Amplitude.Mercury.Simulation;
using BepInEx.Configuration;
using Modding.Humankind.DevTools.DeveloperTools;
using UnityEngine;

namespace Modding.Humankind.DevTools.Core
{
    [DevToolsModule]
    internal class BuiltInModule
    {
        private static int _targetEmpireIndex;
        public static string Name => "BuiltInModuleActions";

        private static List<ConstructibleCostModifierDefinition> _constructibleCostModifiers =
            new List<ConstructibleCostModifierDefinition>();

        [OnGameHasLoaded]
        public static void OnGameHasLoaded()
        {
            Loggr.Debug("[OnGameHasLoaded] BuiltInModule.OnGameHasLoaded();");
            _constructibleCostModifiers = new List<ConstructibleCostModifierDefinition>();
            SetTargetEmpire(0);
        }

        [InGameKeyboardShortcut("Add300InfluenceToSelectedEmpire", KeyCode.R, KeyCode.LeftControl)]
        public static void Add300InfluenceToSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].InfluenceStock += 300;
            Loggr.Debug("Increased " + HumankindGame.Empires[_targetEmpireIndex].PersonaName +
                        "'s InfluenceStock by 300.");
        }
        
        [InGameKeyboardShortcut("Add100MoneyToSelectedEmpire", KeyCode.M, KeyCode.LeftControl)]
        public static void Add100MoneyToSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].MoneyStock += 100;
            Loggr.Debug("Increased " + HumankindGame.Empires[_targetEmpireIndex].PersonaName +
                        "'s Money by 100.");
        }

        [InGameKeyboardShortcut("Add2kResearchToSelectedEmpire", KeyCode.R, KeyCode.LeftShift)]
        public static void Add2kResearchToSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].ResearchStock += 2000;
            Loggr.Debug("Increased " + HumankindGame.Empires[_targetEmpireIndex].PersonaName +
                        "'s ResearchStock by 2000.");
        }

        [InGameKeyboardShortcut("SwitchTargetEmpire", KeyCode.T, KeyCode.LeftControl)]
        public static void SwitchTargetEmpire()
        {
            SetTargetEmpire((_targetEmpireIndex + 1) % HumankindGame.Empires.Length);
        }
        
        [InGameKeyboardShortcut("AllOtherEmpiresProposeAllianceToSelectedEmpire", KeyCode.A, KeyCode.LeftControl)]
        public static void AllOtherEmpiresProposeAllianceToSelectedEmpire()
        {
            var action = DiplomaticAction.ProposeAllianceTreaty;
            
            foreach (var empire in HumankindGame.Empires)
            {
                if (empire.EmpireIndex == _targetEmpireIndex)
                    continue;

                if (!empire.CanExecuteDiplomaticAction(action, _targetEmpireIndex))
                {
                    Loggr.Announce(empire + " can NOT propose alliance to " + HumankindGame.Empires[_targetEmpireIndex]);
                    continue;
                }
                
                empire.ExecuteDiplomaticAction(action, _targetEmpireIndex);
                Loggr.Announce(empire + " proposed alliance to " + HumankindGame.Empires[_targetEmpireIndex]);
            }
        }
        
        [InGameKeyboardShortcut("AllOtherEmpiresForceAllianceToSelectedEmpire", KeyCode.F, KeyCode.LeftControl)]
        public static void AllOtherEmpiresForceAllianceToSelectedEmpire()
        {
            var action = DiplomaticAction.ForceSignAlliance;
            
            foreach (var empire in HumankindGame.Empires)
            {
                if (empire.EmpireIndex == _targetEmpireIndex)
                    continue;

                if (!empire.CanExecuteDiplomaticAction(action, _targetEmpireIndex))
                {
                    Loggr.Announce(empire + " can NOT force alliance to " + HumankindGame.Empires[_targetEmpireIndex]);
                    continue;
                }
                
                empire.ExecuteDiplomaticAction(action, _targetEmpireIndex);
                Loggr.Announce(empire + " forced alliance to " + HumankindGame.Empires[_targetEmpireIndex]);
            }
        }

        [InGameKeyboardShortcut("EnableFogOfWarOfSelectedEmpire", KeyCode.Y, KeyCode.LeftControl)]
        public static void EnableFogOfWarOfSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].EnableFogOfWar(true);
        }
        
        [InGameKeyboardShortcut("DisableFogOfWarOfSelectedEmpire", KeyCode.U, KeyCode.LeftControl)]
        public static void DisableFogOfWarOfSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].EnableFogOfWar(false);
        }
        
        [InGameKeyboardShortcut("Add100ToResearchCostModifierToAllTechnologiesOfSelectedEmpire", KeyCode.W, KeyCode.LeftControl)]
        public static void Add100ToResearchCostModifierToSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex]
                .AddResearchCostModifier(100f, CostModifierDefinition.OperationTypes.Add);
        }
        
        [InGameKeyboardShortcut("Add 100 to ConstructibleCostModifier of player's empire", KeyCode.Q, KeyCode.LeftControl)]
        public static void Add100ToConstructibleCostModifierOfSelectedEmpire()
        {
            _constructibleCostModifiers.Add(HumankindGame.Empires[0]
                .AddConstructibleCostModifier(100f, CostModifierDefinition.OperationTypes.Add));
        }
        
        [InGameKeyboardShortcut("Reduce to 50% the ConstructibleCostModifier of player's empire", KeyCode.Q, KeyCode.LeftShift)]
        public static void ReduceTo50PercentTheConstructibleCostModifierOfSelectedEmpire()
        {
            _constructibleCostModifiers.Add(HumankindGame.Empires[0]
                .AddConstructibleCostModifier(0.5f, CostModifierDefinition.OperationTypes.Mult));
        }
        
        [InGameKeyboardShortcut("Remove all ConstructibleCostModifiers of player's empire", KeyCode.Q, KeyCode.LeftControl, KeyCode.LeftShift)]
        public static void RemoveAllConstructibleCostModifiers()
        {
            foreach (var modifier in _constructibleCostModifiers)
            {
                HumankindGame.Empires[0].RemoveConstructibleCostModifier(modifier);
            }
            _constructibleCostModifiers.Clear();
        }
        
        [InGameKeyboardShortcut("PrintSettlementsOfSelectedEmpire", KeyCode.S, KeyCode.LeftShift)]
        public static void PrintSettlementsOfSelectedEmpire()
        {
            foreach (var settlement in HumankindGame.Empires[_targetEmpireIndex].Settlements)
            {
                Loggr.Log("Settlement @ Tile: " + settlement.WorldPosition.ToTileIndex(), ConsoleColor.Cyan);
                Loggr.Log("EmpireIndex: " + settlement.EmpireIndex, ConsoleColor.Cyan);
                Loggr.Log("Type: " + (settlement.IsCity ? "CITY" : settlement.IsOutpost ? "OUTPOST" : "--OTHER--"), ConsoleColor.Cyan);
                Loggr.Log("IsCapital: " + settlement.IsCapital, ConsoleColor.Cyan);
                Loggr.Log("TerritoryCount: " + settlement.Territories.Length, ConsoleColor.Cyan);
                Loggr.Log("ArmiesCount: " + settlement.Armies.Count(), ConsoleColor.Cyan);
                Loggr.Log(" ", ConsoleColor.Cyan);
            }
        }
        
        [InGameKeyboardShortcut("IncreasePopulationByOneInCitiesOfSelectedEmpire", KeyCode.B, KeyCode.LeftShift)]
        public static void IncreasePopulationByOneInCitiesOfSelectedEmpire()
        {
            foreach (var city in HumankindGame.Empires[_targetEmpireIndex].Settlements.Where(settlement => settlement.IsCity))
            {
                city.Population += 1;
            }
        }
        
        [InGameKeyboardShortcut("PrintUnitDefinitionsInDatabase", KeyCode.U, KeyCode.LeftShift)]
        public static void PrintUnitDefinitionsInDatabase()
        {
            foreach (var unit in QuickAccess.UnitDefinitions)
            {
                Loggr.Log("UNIT: " + unit.name + " | FAMILY: " + unit.Family + " | SPAWN TYPE: " + unit.SpawnType.ToString(), ConsoleColor.Yellow);
            }
        }
        
        [InGameKeyboardShortcut("SpawnUnitInAllCitiesOfSelectedEmpire", KeyCode.I, KeyCode.LeftShift)]
        public static void SpawnUnitInAllCitiesOfSelectedEmpire()
        {
            var commonWarriors = QuickAccess.UnitDefinitions
                .Where(unit => unit.name == "LandUnit_Era6_Common_HelicopterGunships")
                .ElementAtOrDefault(0);
            
            foreach (var city in HumankindGame.Empires[_targetEmpireIndex].Settlements.Where(settlement => settlement.IsCity))
            {
                city.BuildUnit(commonWarriors);
            }
        }

        [InGameKeyboardShortcut("Iterate every city and center camera to it", KeyCode.C, KeyCode.LeftAlt)]
        public static void CenterCameraToNextCity()
        {
            HumankindGame.Empires.Settlements().IsCity().Interactive(city => {
                Loggr.Log(city);
            });
        }

        public static void SetTargetEmpire(int empireIndex)
        {
            Loggr.Log("SELECTED EMPIRE = [" + empireIndex + "] " + HumankindGame.Empires[empireIndex].PersonaName,
                ConsoleColor.Magenta);
            _targetEmpireIndex = empireIndex;
        }
    }
}