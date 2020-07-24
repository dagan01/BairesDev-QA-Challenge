using System;

namespace Cargsl.Bairesdev.Rankings
{
    public class Profile
    {
        private readonly Func<Profile, double> _scoringFunction;
        private double? _score;

        public Profile(Func<Profile, double> scoringFunction)
        {
            this._scoringFunction = scoringFunction;
        }

        public double Score
        {
            get
            {
                if (this._scoringFunction == null)
                {
                    return double.NaN;
                }

                if (this._score == null)
                {
                    this._score = this._scoringFunction(this);
                }

                return this._score.Value;
            }
        }

        public string PublicProfileUrl { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string GeographicArea { get; set; }
        public int NumberOfRecommendations { get; set; }
        public int NumberOfConnections { get; set; }
        public string CurrentRole { get; set; }
        public string Industry { get; set; }
        public string Country { get; set; }
    }
}