using EindProjectBusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectDAL
{
    class Login
    {
        // Methode toevoegen die adhv username en pwd een Werknemer object teruggeeft indien de login ok was.
        // Methode om in de db de login te verifieëren.
        public Werknemer UserLogin(string userName, string password)
        {
            Werknemer werknemer = new Werknemer();
            if (ValidateLogin(userName, password)) return werknemer;
            else return null;
        }

        private bool ValidateLogin(string userName, string password)
        {
            return true;
        }
    }
}
