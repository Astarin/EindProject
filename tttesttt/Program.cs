using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindProjectBusinessModels;

namespace tttesttt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DbEindproject db = new DbEindproject())
            {
                foreach (Team team in db.Teams)
                {
                    Console.WriteLine(team.Naam);
                }

                foreach (Werknemer wn in db.Werknemers)
                {
                    Console.WriteLine(wn.Voornaam);
                }
            }
        }
    }
}