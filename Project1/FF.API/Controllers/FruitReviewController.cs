using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using FF.Contracts.Service;

namespace FF.API.Controllers
{
    public class FruitReviewController : BaseServiceController
    {
        public FruitReviewController(BasicAuthenticationHeaderValue basicAuthenticationHeaderValue, IIdentity userIdentity) : base(basicAuthenticationHeaderValue, userIdentity)
        {
        }


    }
}
