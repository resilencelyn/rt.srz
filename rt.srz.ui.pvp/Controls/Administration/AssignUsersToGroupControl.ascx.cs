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
	public partial class AssignUsersToGroupControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
    private Guid _groupId;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["GroupId"] == null)
			{
        _groupId = Guid.Empty;
			}
			else
			{
        _groupId = Guid.Parse(Request.QueryString["GroupId"]);
			}
			if (!IsPostBack)
			{
				lbTitle.Text = string.Format("Добавление пользователей в группу: {0}", Request.QueryString["GroupName"]);

				IList<User> groupUsers = _securityService.GetUsersByGroup(_groupId);
				IList<User> allUsers = _securityService.GetUsers();

				cblUsers.DataSource = allUsers;
				cblUsers.DataBind();
				foreach (ListItem item in cblUsers.Items)
				{
          if (groupUsers.Select(p => p.Id).Contains(Guid.Parse(item.Value)))
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

		public void SaveChanges(Guid newGroupId)
		{
      var assignList = new List<Guid>();
      var detachList = new List<Guid>();
			foreach (ListItem item in cblUsers.Items)
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
      _securityService.AssignUsersToGroup(newGroupId != Guid.Empty ? newGroupId : _groupId, assignList, detachList);
		}
	}
}