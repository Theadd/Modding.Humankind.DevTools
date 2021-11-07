namespace Modding.Humankind.DevTools.Core
{
    public class GameSpeedLevel
    {
        /// <summary>
        ///     Name: GameSpeed_Blitz
        ///     DefaultGameSpeedMultiplier: 0.25
        /// </summary>
        public static readonly int Blitz = 6;

        /// <summary>
        ///     Name: GameSpeed_Fast
        ///     DefaultGameSpeedMultiplier: 0.50
        /// </summary>
        public static readonly int Fast = 5;

        /// <summary>
        ///     Name: GameSpeed_Normal
        ///     DefaultGameSpeedMultiplier: 1.00
        /// </summary>
        public static readonly int Normal = 4;

        /// <summary>
        ///     Name: GameSpeed_Slow
        ///     DefaultGameSpeedMultiplier: 1.50
        /// </summary>
        public static readonly int Slow = 3;

        /// <summary>
        ///     Name: GameSpeed_Endless
        ///     DefaultGameSpeedMultiplier: 2.00
        /// </summary>
        public static readonly int Endless = 2;

        /// <summary>
        ///     For modded games with a new custom GameSpeedDefinition that has a DefaultGameSpeedMultiplier lower than 0.25
        ///     DefaultGameSpeedMultiplier: less than 0.25
        /// </summary>
        public static readonly int Fastest = 7;

        /// <summary>
        ///     For modded games with a new GameSpeedDefinition that has a DefaultGameSpeedMultiplier greater than 2.00
        ///     DefaultGameSpeedMultiplier: greater than 2.00
        /// </summary>
        public static readonly int Slowest = 1;
    }
}