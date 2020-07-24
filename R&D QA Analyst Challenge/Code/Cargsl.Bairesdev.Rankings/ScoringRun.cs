using System.Collections.Generic;

namespace Cargsl.Bairesdev.Rankings
{
    public class ScoringRun
    {
        public ScoringRun()
        {
            Profiles = new List<Profile>();
        }

        public List<Profile> Profiles { get; }

        public int MaxNameLength { get; set; }

        public int MaxLastNameLength { get; set; }
    }
}