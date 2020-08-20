using Intuit.Ipp.OAuth2PlatformClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace QB2.Controllers
{
    public class QBController : Controller
    {
        public static string clientId = ConfigurationManager.AppSettings["clientid"];
        public static string clientSecret = ConfigurationManager.AppSettings["clientsecret"];
        public static string redirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
        public static string environment = ConfigurationManager.AppSettings["appEnvironment"];

        public static OAuth2Client auth2Client = new OAuth2Client(clientId, clientSecret, redirectUrl, environment);

        public ActionResult InitiateAuth()
        {
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            string authorizeUrl = auth2Client.GetAuthorizationURL(scopes);
            return Redirect(authorizeUrl);
        }
    }
}