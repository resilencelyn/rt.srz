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
	public partial class AssignRolesToUserControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
    private Guid _userId;


		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["userId"] == null)
			{
        _userId = Guid.Empty;
			}
			else
			{
        _userId = Guid.Parse(Request.QueryString["userId"]);
			}
			if (!IsPostBack)
			{
				string userName = string.Empty;
				lbTitle.Text = string.Format("Назначение ролей для пользователя: {0}", Request.QueryString["userName"]);
				IList<Role> roles = _securityService.GetRolesByUser(_userId);
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

		public void SaveChanges(Guid newUserId)
		{
      List<Guid> assignList = new List<Guid>();
      List<Guid> detachList = new List<Guid>();
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
			_securityService.AssignRolesToUser(newUserId != Guid.Empty ? newUserId : _userId, assignList, detachList);
		}
	}
}