﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectBusinessModels;
using EindProjectDAL;

namespace EindProjectMVC.Controllers
{
    public class VerlofController : Controller
    {
        //
        // GET: /Verlof/
        public ActionResult Index(String Actie)
        {
            return View();
        }

        public ActionResult WerknemerAction(string PersoneelsNr)
        {
            if (PersoneelsNr == String.Empty || PersoneelsNr == null)
            {
                return View();
            }
            else
            {
                DalMethodes methode = new DalMethodes();

                Werknemer wn = methode.VraagWerknemerOp(PersoneelsNr, "", "").FirstOrDefault();

                return View("WerknemerIngelogd", wn);
            }
        }

        public ActionResult VerlofIndienen(VerlofAanvraag aanvraag, string PersoneelsNr)
        {
            DalMethodes methode = new DalMethodes();

            Werknemer wn = methode.VraagWerknemerOp(PersoneelsNr, "", "").FirstOrDefault();

            methode.IndienenVerlofaanvraag(wn, aanvraag);

            wn = methode.VraagWerknemerOp(PersoneelsNr, "", "").FirstOrDefault();

            return View("WerknemerIngelogd", wn);
        }
    }
}