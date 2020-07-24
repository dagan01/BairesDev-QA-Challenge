using System.Collections.Generic;
using System.Linq;

namespace Cargsl.Bairesdev.Rankings
{
    public static class ScoringFunctions
    {
        public static double BasicScoringFunction(Profile p)
        {
            double score = 0;

            score += 30 * IndustryScoreModifier(p.Industry);
            score += 25 * RoleScoreModifier(p.CurrentRole);
            score += 15 * GeographicCountryScoreModifier(p.GeographicArea, p.Country);
            score += 15 * ConnectionScoreModifier(p.NumberOfConnections);
            score += 10 * TitleScoreModifier(p.Title);
            score += 5 * RecommendationScoreModifier(p.NumberOfRecommendations);

            return score;
        }

        private static double RoleScoreModifier(string currentRole)
        {
            var normalized = currentRole.ToLowerInvariant();
            var modifier = TitleScores.Keys.Where(key => normalized.Contains(key)).Sum(key => TitleScores[key]);
            return modifier > 1 ? 1 : modifier;
        }

        private static double IndustryScoreModifier(string industry)
        {
            var normalizedIndustry = industry.Trim();
            var score = IndustryScores.ContainsKey(normalizedIndustry.Trim()) ? IndustryScores[normalizedIndustry] : 0;
            return score;
        }

        private static double TitleScoreModifier(string title)
        {
            var normalized = title.ToLowerInvariant();
            var modifier = TitleScores.Keys.Where(key => normalized.Contains(key)).Sum(key => TitleScores[key]);
            return modifier > 1 ? 1 : modifier;
        }

        private static double ConnectionScoreModifier(int numberOfConnections)
        {
            return numberOfConnections / 500d;
        }

        private static double GeographicCountryScoreModifier(string geographicArea, string country)
        {
            var normalizedCountry = country.ToLowerInvariant().Trim();
            var modifier = CountryScores.ContainsKey(normalizedCountry) ? CountryScores[normalizedCountry] : 0;

            var normalizedGeographicArea = geographicArea.ToLowerInvariant().Trim();
            modifier += GeographicScores.Contains(normalizedGeographicArea) ? 0.25 : 0;

            return modifier > 1 ? 1 : modifier;
        }

        private static double RecommendationScoreModifier(int numberOfRecommendations)
        {
            var score = numberOfRecommendations / 50d;

            return score > 1 ? 1 : score;
        }

        #region Scoring Dictionaries

        private static readonly Dictionary<string, double> TitleScores = new Dictionary<string, double>()
        {
            { "avp", 0.5 },
            { "ceo", 0.75 },
            { "chairman", 0.75 },
            { "chief executive officer", 0.75 },
            { "chief information officer", 0.75 },
            { "chief technology officer", 0.75 },
            { "entrepreneur", 0.5 },
            { "cio", 0.75 },
            { "co founder", 0.75 },
            { "cofounder", 0.75 },
            { "co-founder", 0.75 },
            { "coo", 0.5 },
            { "cto", 0.75 },
            { "director", 0.25 },
            { "engineer", 0.25 },
            { "founder", 0.75 },
            { "outsourcing", 0.5 },
            { "owner", 0.75 },
            { "partner", 0.75 },
            { "president", 0.75 },
            { "program manager", 0.25 },
            { "software architect", 0.5 },
            { "svp", 0.5 },
            { "vice president", 0.5 },
            { "vp", 0.5 }
        };

        private static readonly Dictionary<string, double> IndustryScores = new Dictionary<string, double>()
        {
            {"Automotive", 0.5},
            {"Banking", 0.75},
            {"Biotechnology", 0.75},
            {"Capital Markets", 0.25},
            {"Commercial Real Estate", 0.25},
            {"Computer & Network Security", 1},
            {"Computer Games", 1},
            {"Computer Hardware", 1},
            {"Computer Networking", 1},
            {"Computer Software", 1},
            {"Defense & Space", 0.5},
            {"Design", 0.5},
            {"Education Management", 0.25},
            {"Electrical/Electronic Manufacturing", 0.75},
            {"Entertainment", 0.25},
            {"Financial Services", 0.5},
            {"Government Relations", 0.25},
            {"Health,Wellness and Fitness", 0.75},
            {"Hospital & Health Care", 0.75},
            {"Hospitality", 0.25},
            {"Human Resources", 0.25},
            {"Information Services", 1},
            {"Information Technology and Services", 1},
            {"Insurance", 0.5},
            {"International Trade and Development", 0.25},
            {"Internet", 1},
            {"Law Practice", 0.25},
            {"Management Consulting", 0.25},
            {"Marketing and Advertising", 0.25},
            {"Mechanical or Industrial Engineering", 0.25},
            {"Media Production", 0.5},
            {"Military", 0.75},
            {"Real Estate", 0.25},
            {"Retail", 0.25},
            {"Semiconductors", 0.75},
            {"Sports", 0.25},
            {"Telecommunications", 0.75},
            {"Transportation/Trucking/Railroad", 0.25},
            {"Venture Capital & Private Equity", 0.75},
            {"Wireless", 0.75}
        };

        private static readonly HashSet<string> GeographicScores = new HashSet<string>()
        {
            "austin, texas area, united states",
            "beijing city, china",
            "dallas/fort worth area, united states",
            "frankfurt am main area, germany",
            "greater atlanta area, united states",
            "greater boston area, united states",
            "greater chicago area, united states",
            "greater denver area, united states",
            "greater detroit area, united states",
            "greater los angeles area, united states",
            "greater new york city area, united states",
            "greater seattle area, united states",
            "hamburg area, germany",
            "kansas city, missouri area, United States",
            "las vegas, nevada area, united states",
            "london, greater london, united kingdom",
            "los angeles, california",
            "miami/fort lauderdale area, united states",
            "montreal, canada area, united states",
            "munich area, germany",
            "new delhi area, india",
            "new york, new york",
            "newport beach, california",
            "orange county, california area, united states",
            "orlando, florida area, united states",
            "san francisco bay area, united states",
            "san francisco, california",
            "san jose, california",
            "santa barbara, california",
            "santa barbara, california area, united states",
            "santa clara, california",
            "shanghai city, china",
            "washington d.c. metro area, united states",
            "washington, district of columbia",
            "west palm beach, florida area, united states",
            "wuxi city, china"
        };

        private static readonly Dictionary<string, double> CountryScores = new Dictionary<string, double>()
        {
            { "argentina", 0.75 },
            { "canada", 0.75 },
            { "china", 0.75 },
            { "germany", 0.75 },
            { "india", 0.25 },
            { "kuwait", 0.25 },
            { "peru", 0.5 },
            { "united kingdom", 0.75 },
            { "united states", 0.75 }
        };

        #endregion
    }
}
