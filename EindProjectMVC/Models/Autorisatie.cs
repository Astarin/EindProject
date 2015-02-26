using EindProjectMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EindProjectMVC.Models
{
    public class Autorisatie : FilterAttribute, IActionFilter
    {
        public string RolOmschrijving { get; set; }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (filterContext.ActionParameters.ContainsKey(RolOmschrijving))
            //{
               //string rolString = filterContext.ActionParameters[RolOmschrijving] as string;
               //Rol rol= (Rol)Enum.Parse(typeof(Rol), rolString);
               Rol rol = (Rol)Enum.Parse(typeof(Rol), RolOmschrijving);

               if (!LoginMethode.HeeftRechten(rol))
               {
                   //FormsAuthentication.RedirectToLoginPage(); // werkt niet
                   filterContext.Result = new RedirectResult(@"\Home\Index");
               }

            //}
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
        
    }
}