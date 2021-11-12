using System.Collections.Generic;
using System.Linq;
using Amplitude.Mercury.Data.AI;

namespace Modding.Humankind.DevTools.Core
{
    internal static class GameEmpireHelper
    {
        public const int HeaderCellSize = 30;
        public const int CellSize = 16;
        public const string IconTurn = "\u0489";
        public static readonly string[] SEPARATOR = new[] {" ", " "};
        
        public static Archetype[] ArchetypesToArray(Archetype archetypes)
        {
            var res = new List<Archetype>();

            if ((uint) (archetypes & Archetype.Cruel) == (uint) Archetype.Cruel) res.Add(Archetype.Cruel);
            if ((uint) (archetypes & Archetype.Benevolent) == (uint) Archetype.Benevolent)
                res.Add(Archetype.Benevolent);
            if ((uint) (archetypes & Archetype.Traitorous) == (uint) Archetype.Traitorous)
                res.Add(Archetype.Traitorous);
            if ((uint) (archetypes & Archetype.Loyal) == (uint) Archetype.Loyal) res.Add(Archetype.Loyal);
            if ((uint) (archetypes & Archetype.Pacifist) == (uint) Archetype.Pacifist) res.Add(Archetype.Pacifist);
            if ((uint) (archetypes & Archetype.Militarist) == (uint) Archetype.Militarist)
                res.Add(Archetype.Militarist);
            if ((uint) (archetypes & Archetype.Careful) == (uint) Archetype.Careful) res.Add(Archetype.Careful);
            if ((uint) (archetypes & Archetype.RiskTaking) == (uint) Archetype.RiskTaking)
                res.Add(Archetype.RiskTaking);
            if ((uint) (archetypes & Archetype.Impulsive) == (uint) Archetype.Impulsive) res.Add(Archetype.Impulsive);
            if ((uint) (archetypes & Archetype.CoolHeaded) == (uint) Archetype.CoolHeaded)
                res.Add(Archetype.CoolHeaded);
            if ((uint) (archetypes & Archetype.Adaptative) == (uint) Archetype.Adaptative)
                res.Add(Archetype.Adaptative);
            if ((uint) (archetypes & Archetype.Committed) == (uint) Archetype.Committed) res.Add(Archetype.Committed);
            if ((uint) (archetypes & Archetype.Introvert) == (uint) Archetype.Introvert) res.Add(Archetype.Introvert);
            if ((uint) (archetypes & Archetype.Extrovert) == (uint) Archetype.Extrovert) res.Add(Archetype.Extrovert);
            if ((uint) (archetypes & Archetype.Hateful) == (uint) Archetype.Hateful) res.Add(Archetype.Hateful);
            if ((uint) (archetypes & Archetype.Open) == (uint) Archetype.Open) res.Add(Archetype.Open);
            if ((uint) (archetypes & Archetype.Wary) == (uint) Archetype.Wary) res.Add(Archetype.Wary);
            if ((uint) (archetypes & Archetype.Trusting) == (uint) Archetype.Trusting) res.Add(Archetype.Trusting);
            if ((uint) (archetypes & Archetype.Vindictive) == (uint) Archetype.Vindictive)
                res.Add(Archetype.Vindictive);
            if ((uint) (archetypes & Archetype.Forgiving) == (uint) Archetype.Forgiving) res.Add(Archetype.Forgiving);

            return res.ToArray();
        }
        
        public static string[] ToFormattedStringArray(HumankindEmpire empire)
        {
            const int size = -CellSize;

            return ToStringArray(empire).Select(item => $"{item[1],size}").ToArray();
        }

        public static string[] ToFieldNameStringArray(HumankindEmpire empire)
        {
            return ToStringArray(empire).Select(item => $"{item[0],HeaderCellSize}").ToArray();
        }
        
        public static string[][] ToStringArray(HumankindEmpire empire)
        {
            var moneyNet = empire.MoneyNet > 0 ? $"+{empire.MoneyNet}" : $"{empire.MoneyNet}";

            return new[]
            {
                new[] {$"TURN {HumankindGame.Turn} #", empire.EmpireIndex + ": " + empire.PersonaName},
                new[] {new string('=', HeaderCellSize), new string('=', CellSize)},
                new[] {"Territories (Cities)", $"{empire.TerritoryCount} ({empire.CityCount}/{empire.CityCap})"},
                new[] {"Stability", $"{empire.Stability}"},
                new[] {"EraLevel (SumOfEraStars)", $"{empire.EraLevel} ({empire.SumOfEraStars})"},
                new[] {"Population in cities (total)", $"{empire.SettlementsPopulation} ({empire.EmpirePopulation})"},
                SEPARATOR,
                new[] {"[MILITARY STATS]", ""},
                new[] {"CombatStrength", $"{empire.CombatStrength}"},
                new[] {"Armies (Units)", $"{empire.ArmyCount} ({empire.UnitCount})"},
                new[] {"Military Upkeep", $"{empire.MilitaryUpkeep}"},
                SEPARATOR,
                new[] {"[TECHNOLOGY STATS]", ""},
                new[] {"Nº Techs (Science)", $"{empire.CompletedTechnologiesCount} (+{empire.ResearchNet}/{IconTurn})"},
                new[] {"Technological Era Offset", $"{empire.TechnologicalEraOffset}"},
                new[] {"Available/Unlocked Techs.", $"{empire.AvailableTechnologiesCount} / {empire.UnlockedTechnologiesCount}"},
                SEPARATOR,
                new[] {"[ECONOMY STATS]", ""},
                new[] {"Money", $"{empire.MoneyStock} ({moneyNet}/{IconTurn})"},
                new[] {"Influence", $"{empire.InfluenceStock} (+{empire.InfluenceNet}/{IconTurn})"},
                new[] {"Accesses to Luxury/Strategic", $"{empire.LuxuryResourcesAccessCount} / {empire.StrategicResourcesAccessCount}"},
                new[] {"Trade Nodes Count", $"{empire.TradeNodesCount}"}
            };
        }
    }
}
