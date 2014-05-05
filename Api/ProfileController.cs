using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moves.App.Helpers;
using Moves.Net.Model;

namespace Moves.App.Api
{
    public class ProfileController : System.Web.Http.ApiController
    {
        public User GetUser()
        {
            var user = MovesApplication.Client.Profile.GetUser();
            return user.Data;
		}
    }
}
