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
  using rt.core.model.interfaces;

  public partial class AddPermissionControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
		private Permission _permission;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["PermissionId"] == null)
			{
				_permission = new Permission();
			}
			else
			{
        _permission = _securityService.GetPermission(Guid.Parse(Request.QueryString["PermissionId"]));
			}
			if (!IsPostBack)
			{
				if (Request.QueryString["PermissionId"] == null)
				{
					lbTitle.Text = "Добавление разрешения";
					return;
				}
				tbName.Text = _permission.Name;
				tbCode.Text = _permission.Code.ToString();
				lbTitle.Text = "Редактирование разрешения";
			}
		}

		public void ValidateCode(Object source, ServerValidateEventArgs args)
		{
			args.IsValid = !_securityService.ExistsPermissionCode(_permission.Id, int.Parse(tbCode.Text));
		}

		public Guid SaveChanges()
		{
			if (_permission != null)
			{
				_permission.Name = tbName.Text;
				_permission.Code = int.Parse(tbCode.Text);
			}
			return _securityService.SavePermission(_permission);
		}
	}
}