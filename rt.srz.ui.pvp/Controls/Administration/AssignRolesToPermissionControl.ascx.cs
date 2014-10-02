using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;

namespace rt.srz.ui.pvp.Controls.Administration
{
	public partial class AssignRolesToPermissionControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
    private Guid _permissionId;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["permissionId"] == null)
			{
        _permissionId = Guid.Empty;
			}
			else
			{
        _permissionId = Guid.Parse(Request.QueryString["permissionId"]);
			}
			if (!IsPostBack)
			{
				string permissionName = string.Empty;
				lbTitle.Text = string.Format("Назначение ролей для разрешения: {0}", Request.QueryString["permissionName"]);
				IList<Role> roles = _securityService.GetRolesByPermission(_permissionId);
				IList<Role> allRoles = _securityService.GetRoles();

				cblRoles.DataSource = allRoles;
				cblRoles.DataBind();

				foreach (ListItem item in cblRoles.Items)
				{
          if (roles.Select(p => p.Id).Contains(Guid.Parse(item.Value)))
					{
						item.Selected = true;
					}
				}
			}
		}

		public void SaveChanges()
		{
      SaveChanges(Guid.Empty);
		}

		public void SaveChanges(Guid newPermissionId)
		{
      var assignList = new List<Guid>();
      var detachList = new List<Guid>();
			foreach (ListItem item in cblRoles.Items)
			{
				if (item.Selected)
				{
          assignList.Add(Guid.Parse(item.Value));
				}
				else
				{
          detachList.Add(Guid.Parse(item.Value));
				}
			}
      _securityService.AssignRolesToPermission(newPermissionId != Guid.Empty ? newPermissionId : _permissionId, assignList, detachList);
		}
	}
}