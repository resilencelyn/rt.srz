﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;

namespace rt.srz.ui.pvp.Pages.Administrations
{
	public partial class AssignRolesForPermission : System.Web.UI.Page
	{
		private BaseAddCancelPage MasterPage
		{
			get { return (BaseAddCancelPage)this.Master; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			MasterPage.Cancel += MasterPage_Cancel;
			MasterPage.Save += MasterPage_Save;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MasterPage.ActionTitle = "";//string.Format("Назначение ролей для разрешения: {0}", Request.QueryString["permissionName"]);
			}
		}

		void MasterPage_Cancel()
		{
			RedirectUtils.RedirectToAdministrationPermissions(Response);
		}

		void MasterPage_Save()
		{
			assignRolesToPermissionControl.SaveChanges();
			RedirectUtils.RedirectToAdministrationPermissions(Response);
		}

	}
}