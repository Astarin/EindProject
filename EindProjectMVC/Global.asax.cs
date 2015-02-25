using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EindProjectBusinessModels;
using EindProjectDAL;
using EindProjectMVC;
using System.Diagnostics;

namespace EindProjectMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CheckVerlofAanvragenTrigger();
        }


        static void CheckVerlofAanvragenTrigger()
        {
            // Wanneer is middernacht ?
            // DateTime middernacht = DateTime.Today.AddDays(1)
            // HttpRuntime.Cache.Add("CheckVerlofAanvragenTrigger",
            //     string.Empty,
            //     null,
            //     middernacht,
            //     Cache.NoSlidingExpiration,
            //     CacheItemPriority.NotRemovable,
            //     new CacheItemRemovedCallback(CheckVerlofAanvragen));


            // Om demo/testing gaan we niet om middernacht checken, maar gewoon om de 10 minuten.
            HttpRuntime.Cache.Add("CheckVerlofAanvragenTrigger",
                string.Empty,
                null,
                Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(1),
                CacheItemPriority.NotRemovable,
                new CacheItemRemovedCallback(CheckVerlofAanvragen));
        }


        static void CheckVerlofAanvragen(string key, object value, CacheItemRemovedReason reason)
        {
            // TO DO
            // Als startdatum verlof = vandaag EN verlof nog niet goedgekeurd => goedkeuren die handel!
            //


            // overlopen van alle verlofaanvragen
            // indien status = ingediend => goedkeuren => saven
            using (DbEindproject db = new DbEindproject())
            {
                List<VerlofAanvraag> aanvragen = (from va in db.Verlofaanvragen
                                                 where va.StartDatum == DateTime.Today
                                                 && va.Toestand == Aanvraagstatus.Ingediend
                                                 select va).ToList();

                foreach (VerlofAanvraag aanvraag in aanvragen)
                {
                    // automatisch goedkeuren van deze verlofaanvragen
                    aanvraag.BehandeldDoor = null;
                    aanvraag.BehandelDatum = DateTime.Today;
                    aanvraag.Toestand = Aanvraagstatus.Goedgekeurd;
                }
                db.SaveChanges();
                

            }

            CheckVerlofAanvragenTrigger();
        }

    }
}