using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public class DbEindproject : DbContext
    {
        public DbEindproject()
        {
            Database.Log = Console.Write;
            Database.SetInitializer<DbEindproject>(new DbEindprojectInitializer());
          //  Database.SetInitializer<DbEindproject>(new DropCreateDatabaseAlways<DbEindproject>());
        }

        public DbSet<Werknemer> Werknemers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<VerlofAanvraag> Verlofaanvragen { get; set; }
        public DbSet<CollectieveSluiting> Sluitingsdagen { get; set; }
        public DbSet<JaarlijksVerlof> JaarlijkseVerloven { get; set; }
    }
}