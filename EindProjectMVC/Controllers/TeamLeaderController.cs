using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectDAL;
using EindProjectBusinessModels;

namespace EindProjectMVC.Controllers
{
    public class TeamLeaderController : Controller
    {
        //
        // GET: /TeamLeader/
        public ActionResult Index()
        {
            DalMethodes Dal = new DalMethodes();
            Team team = new Team();
            //ViewBag.Teamleden = Dal.GeefTeamleden(team);
            return View();
        }

	}
}