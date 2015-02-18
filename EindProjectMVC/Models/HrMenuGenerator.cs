using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EindProjectMVC.Models
{
   static  public class HrMenuGenerator
    {
       public static string CreeerHrMenu()
       {
           return
           @"    <div class='jumbotron'>
        <h2>Hr Selecteer Werknemer</h2>
        <nav class='navbar navbar-inverse'>
            <form action='/HR/HrNieuweWerknemer' class='label label-primary' >
                <input name='Actie' type='submit' value='Nieuwe WerkNemer' class='button-invis'>
            </form>
            <form action='/HR/HrSelecteerWerknemer' class='label label-success'>
                <input name='Actie' type='submit' value='Wijzig Werknemer' class='button-invis'>
            </form>
            <form action='/HR/HrWJaarlijksVerlofToevoegen' class='label label-default'>
                <input name='Actie' type='submit' value='Verlof Toevoegen' class='button-invis'>
            </form>
            <form action='/HR/HrTeamToevoegen' class='label label-danger'>
                <input name='Actie' type='submit' value='Team Toevoegen' class='button-invis'>
            </form>

        </nav>
        </div>
           ";
       }
    }
}