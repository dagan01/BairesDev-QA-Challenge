using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Cargsl.Bairesdev.Rankings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Cargsl BairesDev Rankings");

            if (args.Length == 0)
            {
                Console.WriteLine("Please add the path for the LinkedIn data file");
                return;
            }

            string[] data;

            try
            {
                data = System.IO.File.ReadAllLines(args[0], Encoding.ASCII);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Couldn't find file: '{ex.FileName}' ");
                return;
            }

            var sr = new ScoringRun();

            foreach (var item in data)
            {
                var elements = item.Split(new[] { '|' }, StringSplitOptions.None);

                int _;
                var prof = new Profile(ScoringFunctions.BasicScoringFunction)
                {
                    PublicProfileUrl = elements[0],
                    Name = elements[1],
                    LastName = elements[2],
                    Title = elements[3],
                    GeographicArea = elements[4],
                    NumberOfRecommendations = int.TryParse(elements[5], out _) ? int.Parse(elements[5]) : 0,
                    NumberOfConnections = int.TryParse(elements[6], out _) ? int.Parse(elements[6]) : 0,
                    CurrentRole = elements[7],
                    Industry = elements[8],
                    Country = elements[9]
                };

                if (prof.LastName.Length > sr.MaxLastNameLength)
                {
                    sr.MaxLastNameLength = prof.LastName.Length;
                }
                if (prof.Name.Length > sr.MaxNameLength)
                {
                    sr.MaxNameLength = prof.Name.Length;
                }

                sr.Profiles.Add(prof);
            }

            Console.WriteLine($"{"Score".PadLeft(6)} | {"Name".PadRight(sr.MaxNameLength)} | {"LastName".PadRight(sr.MaxLastNameLength)} | ProfileUrl");

            foreach (var profile in sr.Profiles.OrderByDescending(c => c.Score))
            {
                Console.WriteLine($"{profile.Score.ToString().PadLeft(6)} | {profile.Name.PadRight(sr.MaxNameLength)} | {profile.LastName.PadRight(sr.MaxLastNameLength)} | {profile.PublicProfileUrl}");
            }
        }

    }
}
