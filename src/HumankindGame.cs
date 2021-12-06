using System;
using Amplitude.Mercury.Data.Simulation;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{

    /// <summary>
    ///     Main starting class to work with for anything that deals with in-game state.
    /// </summary>
    /// <remarks>Most members are unreliable when <c>IsGameLoaded</c> is false.</remarks>
    public static class HumankindGame
    {
        /// <summary>
        ///     Whether current game (if any) is a continuation of a previously saved game or a newly started one.
        /// </summary>
        public static bool IsNewGame => GameController.IsNewGame;
        
        /// <summary>
        ///     Whether a game is fully loaded and ready to play with.
        /// </summary>
        public static bool IsGameLoaded => GameController.IsGameLoaded && GameController.IsReady;
        
        /// <summary>
        ///     Current game's turn,
        /// </summary>
        public static int Turn => GameController.CurrentTurn;
        
        /// <summary>
        ///     An array of <c>HumankindEmpire</c>'s present in the current game, if any.
        /// </summary>
        public static HumankindEmpire[] Empires => GameController.GameEmpires;
        
        /// <summary>
        ///     A unique number for every started new game.
        /// </summary>
        public static int GameID => GameController.GameID;

        /// <summary>
        ///     Gets current game's <c>GameSpeedDefinition</c> declared in <c>Amplitude.Mercury.Data.Simulation</c> namespace.
        /// </summary>
        public static GameSpeedDefinition GameSpeedDefinition => GameUtils.GameSpeed();
        
        /// <summary>
        ///     Returns an integer representing current's game speed, values range from 1 to 7 where 2 is Endless game
        ///     speed and 6 is Blitz game speed. 1 and 7 were introduced in case the user is playing with a modded game
        ///     where speed value multipliers make it faster than blitz (7) or slower than endless (1).
        ///     See <see href="GameSpeedLevel.md">GameSpeedLevel</see>.
        /// </summary>
        public static int GameSpeedLevel => GameUtils.GetGameSpeedLevel();

        /// <summary>
        ///     Returns a string that represents the current HumankindGame in a formatted table with all empires and
        ///     many of their values found in <c>HumankindEmpire</c> class.
        /// </summary>
        public new static string ToString() => string.Join("\n", GameUtils.GetGameStatistics(Empires));
        
        /// <summary>
        ///     Add/Remove Action handlers to be called at the start of every turn.
        /// </summary>
        /// <remarks>All registered actions are automatically removed when game unloads and before a game loads.</remarks>
        public static event Action OnNewTurnStart
        {
            add => GameController.AddOnNewTurnAction(value);
            remove => GameController.RemoveOnNewTurnAction(value);
        }
    }
}
