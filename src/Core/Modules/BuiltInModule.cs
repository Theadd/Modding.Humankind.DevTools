using System;
using Amplitude.Mercury.Data.Simulation;
using UnityEngine;

namespace Modding.Humankind.DevTools.Core
{
    [DevToolsModule]
    internal class BuiltInModule
    {
        private static int _targetEmpireIndex;
        public static string Name => "BuiltInModuleActions";

        [OnGameHasLoaded]
        public static void OnGameHasLoaded()
        {
            Loggr.Debug("[OnGameHasLoaded]BuiltInModule.OnGameHasLoaded();");

            SetTargetEmpire(0);
        }

        [InGameLeftControlShortcut(KeyCode.P, "PrintGameStatistics")]
        public static void PrintGameStatistics()
        {
            Loggr.Log(HumankindGame.ToString(), ConsoleColor.Cyan);
        }

        [InGameLeftControlShortcut(KeyCode.I, "Add300InfluenceToSelectedEmpire")]
        public static void Add300InfluenceToSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].InfluenceStock += 300;
            Loggr.Debug("Increased " + HumankindGame.Empires[_targetEmpireIndex].PersonaName +
                        "'s InfluenceStock by 300.");
        }

        [InGameLeftControlShortcut(KeyCode.R, "Add2kResearchToSelectedEmpire")]
        public static void Add2kResearchToSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].ResearchStock += 2000;
            Loggr.Debug("Increased " + HumankindGame.Empires[_targetEmpireIndex].PersonaName +
                        "'s ResearchStock by 2000.");
        }

        [InGameLeftControlShortcut(KeyCode.K, "SwitchTargetEmpire")]
        public static void SwitchTargetEmpire()
        {
            SetTargetEmpire((_targetEmpireIndex + 1) % HumankindGame.Empires.Length);
        }
        
        [InGameLeftControlShortcut(KeyCode.A, "AllOtherEmpiresProposeAllianceToSelectedEmpire")]
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
        
        [InGameLeftControlShortcut(KeyCode.F, "AllOtherEmpiresForceAllianceToSelectedEmpire")]
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

        [InGameLeftControlShortcut(KeyCode.Y, "EnableFogOfWarOfSelectedEmpire")]
        public static void EnableFogOfWarOfSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].EnableFogOfWar(true);
        }
        
        [InGameLeftControlShortcut(KeyCode.U, "DisableFogOfWarOfSelectedEmpire")]
        public static void DisableFogOfWarOfSelectedEmpire()
        {
            HumankindGame.Empires[_targetEmpireIndex].EnableFogOfWar(false);
        }

        public static void SetTargetEmpire(int empireIndex)
        {
            Loggr.Log("SELECTED EMPIRE = [" + empireIndex + "] " + HumankindGame.Empires[empireIndex].PersonaName,
                ConsoleColor.Magenta);
            _targetEmpireIndex = empireIndex;
        }
    }
}