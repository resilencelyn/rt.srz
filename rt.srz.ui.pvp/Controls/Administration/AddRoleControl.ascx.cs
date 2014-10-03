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
  using rt.core.model.core;

  public partial class AddRoleControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
		private Role _role;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["RoleId"] == null)
			{
				_role = new Role();
			}
			else
			{
        _role = _securityService.GetRole(Guid.Parse(Request.QueryString["RoleId"]));
			}
			if (!IsPostBack)
			{
				if (Request.QueryString["RoleId"] == null)
				{
					lbTitle.Text = "Добавление роли";
					return;
				}
				tbName.Text = _role.Name;
				lbTitle.Text = "Редактирование роли";
			}
		}

		public Guid SaveChanges()
		{
			if (_role != null)
			{
				_role.Name = tbName.Text;
			}
			return _securityService.SaveRole(_role);
		}
	}
}