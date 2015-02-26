using EindProjectBusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState ;


namespace EindProjectMVC.Models
{
    public enum Rol{Hr, Teamleader}

    public static class LoginMethode
    {
        public static bool HeeftRechten(Rol rol)
        {
            if (HttpContext.Current.Session["currentUser"] != null)
	        {
                Werknemer user = (Werknemer)HttpContext.Current.Session["currentUser"];
                switch (rol)
                {
                    case Rol.Hr:
                        if (user.IsHr)
                        {
                            return true;
                        }
                        return false;
                    case Rol.Teamleader:
                        if (user.TeamLeader)
                        {
                            return true;
                        }
                        return false;
                    default:
                        return false;
                }
	        }
            return false;
        }

    }
}