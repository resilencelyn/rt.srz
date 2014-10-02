﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Pages.Administrations
{
	public partial class AddPermissionEx : System.Web.UI.Page
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
				MasterPage.ActionTitle = "";//"Добавление разрешения";
			}
		}

		void MasterPage_Cancel()
		{
			RedirectUtils.RedirectToAdministrationPermissions(Response);
		}

		void MasterPage_Save()
		{
			var newPermissionId = addPermissionControl.SaveChanges();
			assignRolesToPermissionControl.SaveChanges(newPermissionId);
      RedirectUtils.RedirectToAdministrationPermissions(Response);
		}
	}
}