using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ApiReport1._0.Security
{
    public class TestAPIPrincipal : IPrincipal
    {
        public string UserName { get; set; }
        public IIdentity Identity { get; set; }

        //constructor 
        public TestAPIPrincipal(string username)
        {
            UserName = username;
            Identity = new GenericIdentity(username);
        }


        public bool IsInRole(string role)
        {
            if (role.Equals("user"))
                return true;
            else
                return false;
        }

    }
}