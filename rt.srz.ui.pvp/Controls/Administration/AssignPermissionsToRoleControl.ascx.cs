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
	public partial class AssignPermissionsToRoleControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
    private Guid _roleId;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["RoleId"] == null)
			{
        _roleId = Guid.Empty;
			}
			else
			{
        _roleId = Guid.Parse(Request.QueryString["RoleId"]);
			}
			if (!IsPostBack)
			{
				lbTitle.Text = string.Format("Добавление разрешений для роли: {0}", Request.QueryString["RoleName"]);

				IList<Permission> rolesPermissions = _securityService.GetRolePermissions(_roleId);
				IList<Permission> allPermissions = _securityService.GetPermissions();

				cblPermissions.DataSource = allPermissions;
				cblPermissions.DataBind();
				foreach (ListItem item in cblPermissions.Items)
				{
          if (rolesPermissions.Select(p => p.Id).Contains(Guid.Parse(item.Value)))
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

		public void SaveChanges(Guid newRoleId)
		{
			//список идентификаторов назначаемых разрешений
      List<Guid> assignList = new List<Guid>();
			//список идентификаторов разрешений, которые надо отсоединить от роли
      List<Guid> detachList = new List<Guid>();
			foreach (ListItem item in cblPermissions.Items)
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
      _securityService.AssignPermissionsToRole(newRoleId != Guid.Empty ? newRoleId : _roleId, assignList, detachList);
		}
	}
}