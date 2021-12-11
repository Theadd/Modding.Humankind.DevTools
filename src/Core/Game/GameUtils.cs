using System.Collections.Generic;
using System.Linq;
using Amplitude.Mercury.AI;
using Amplitude.Mercury.AI.Brain;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Interop;
using Amplitude.Mercury.Interop.AI;
using Amplitude.Mercury.Presentation;
using Amplitude.Mercury.Sandbox;
using Amplitude.Mercury.Simulation;
using Empire = Amplitude.Mercury.Interop.AI.Entities.Empire;
using MajorEmpire = Amplitude.Mercury.Interop.AI.Entities.MajorEmpire;

namespace Modding.Humankind.DevTools.Core
{
    internal static class GameUtils
    {
        public static bool IsInValidGameState(AIController controller)
        {
            var aiPlayers = (IAIPlayer[]) R.Fields.AIPlayerByEmpireIndexField.GetValue(controller);

            if (!(Sandbox.MajorEmpires.Length > 0 && Sandbox.MajorEmpires.Length <= aiPlayers.Length))
                return false;

            for (var empireIndex = 0; empireIndex < Sandbox.MajorEmpires.Length; empireIndex++)
                if (aiPlayers[empireIndex] is AIPlayer aiPlayer)
                {
                    if ((Empire) R.Fields.ControlledEmpireField.GetValue(aiPlayer) is MajorEmpire controlledEmpire)
                    {
                        if (controlledEmpire.IsAlive && controlledEmpire.TotalEmpirePopulation == 0 &&
                            controlledEmpire.CombatStrength == 0)
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            return true;
        }

        public static HumankindEmpire[] GetGameEmpires(AIController controller)
        {
            var gameEmpires = new List<HumankindEmpire>();
            var aiPlayers = (IAIPlayer[]) R.Fields.AIPlayerByEmpireIndexField.GetValue(controller);

            for (var empireIndex = 0; empireIndex < Sandbox.MajorEmpires.Length; empireIndex++)
                gameEmpires.Add(HumankindEmpire.Create(
                    (Empire) R.Fields.ControlledEmpireField.GetValue(
                        (AIPlayer) aiPlayers[empireIndex]) as MajorEmpire));

            return gameEmpires.ToArray();
        }

        public static string[] GetGameStatistics(HumankindEmpire[] gameEmpires)
        {
            var lines = GameEmpireHelper.ToFieldNameStringArray(gameEmpires[0]);
            const string separator = " %White%|%Default% ";

            foreach (var gameEmpire in gameEmpires)
                lines = GameEmpireHelper.ToFormattedStringArray(gameEmpire)
                    .Select((line, index) => lines[index] + separator + line)
                    .ToArray();

            return lines;
        }

        public static GameSpeedDefinition GameSpeed()
        {
            return (GameSpeedDefinition) R.Fields.CurrentGameSpeedDefinitionField.GetValue(
                Sandbox.GameSpeedController);
        }

        public static int GetGameSpeedLevel()
        {
            var gameSpeed = GameSpeed();
            var speedMultiplier = (int) ((float) gameSpeed.DefaultGameSpeedMultiplier * 100);
            var speedName = gameSpeed.Name.ToString();

            if (speedMultiplier < 25)
                return GameSpeedLevel.Fastest;

            if (speedMultiplier >= 25 && speedMultiplier < 50 || speedName == "GameSpeed_Blitz")
                return GameSpeedLevel.Blitz;

            if (speedMultiplier >= 50 && speedMultiplier < 100 || speedName == "GameSpeed_Fast")
                return GameSpeedLevel.Fast;

            if (speedMultiplier >= 100 && speedMultiplier < 150 || speedName == "GameSpeed_Normal")
                return GameSpeedLevel.Normal;

            if (speedMultiplier >= 150 && speedMultiplier < 200 || speedName == "GameSpeed_Slow")
                return GameSpeedLevel.Slow;

            if (speedMultiplier == 200 || speedName == "GameSpeed_Endless")
                return GameSpeedLevel.Endless;

            return GameSpeedLevel.Slowest;
        }

        public static void UpdateGameUI()
        {
            if (!HumankindGame.IsGameLoaded)
                return;
            
            Amplitude.Mercury.Interop.AI.Entities.Army aiArmy = HumankindGame.Empires[0].Armies.FirstOrDefault();

            if (aiArmy == null)
                return;
            
            SimulationEntityGUID entityGUID = new SimulationEntityGUID(aiArmy.EntityGUID);
            
            SandboxManager.PostOrder(new OrderToggleAutoExplore
            {
                EntityGUID = entityGUID,
                AutoExplore = aiArmy.AutoExplore
            });
        }

        public static void CenterCameraAt(int tileIndex)
        {
            Presentation.PresentationCameraController.CenterCameraAt(tileIndex);
        }
    }
}