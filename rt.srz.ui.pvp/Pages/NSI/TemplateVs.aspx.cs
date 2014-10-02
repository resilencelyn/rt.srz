using rt.srz.ui.pvp.Enumerations;
using rt.srz.ui.pvp.Pages.Administrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.NSI
{
  public partial class TemplateVs : System.Web.UI.Page
  {
		private BaseAddCancelPage MasterPage
		{
			get { return (BaseAddCancelPage)this.Master; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			MasterPage.Save += MasterPage_Save;
			MasterPage.Cancel += MasterPage_Cancel;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MasterPage.ActionTitle = "";//"Добавление пользователя";
			}
		}

		void MasterPage_Cancel()
		{
			RedirectUtils.RedirectToTemplatesVs(Response);
		}

    void MasterPage_Save()
    {
      TemplateVsControl.SaveChanges();
      RedirectUtils.RedirectToTemplatesVs(Response);
    }
  }
}