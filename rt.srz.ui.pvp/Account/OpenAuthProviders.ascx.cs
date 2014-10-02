using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.AspNet.Membership.OpenAuth;

namespace rt.srz.ui.pvp.Account
{
    public partial class Account_OpenAuthProviders : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                var provider = Request.Form["provider"];
                if (provider == null)
                {
                    return;
                }

                var redirectUrl = "~/Account/RegisterExternalLogin.aspx";
                if (!String.IsNullOrEmpty(ReturnUrl))
                {
                    var resolvedReturnUrl = ResolveUrl(ReturnUrl);
                    redirectUrl += "?ReturnUrl=" + HttpUtility.UrlEncode(resolvedReturnUrl);
                }

                //OpenAuth.RequestAuthentication(provider, redirectUrl);
            }
        }



        public string ReturnUrl { get; set; }


        //public IEnumerable<ProviderDetails> GetProviderNames()
        //{
        //    return OpenAuth.AuthenticationClients.GetAll();
        //}
    }
}