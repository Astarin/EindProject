using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EindProjectDAL
{
    public class TeamHeeftWerknemerException : Exception
    {
        public TeamHeeftWerknemerException()
        {

        }
        public TeamHeeftWerknemerException(string message) : base(message)
        {
            
        }
        public TeamHeeftWerknemerException(string message, Exception e)
            : base(message,e )
        {

        }
    }
}
