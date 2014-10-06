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

  public partial class AssignGroupsToUserControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
    private Guid _userId;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["UserId"] == null)
			{
        _userId = Guid.Empty;
			}
			else
			{
        _userId = Guid.Parse(Request.QueryString["UserId"]);
			}
			if (!IsPostBack)
			{
				lbTitle.Text = string.Format("Добавление в группы пользователя: {0}", Request.QueryString["UserName"]);

				IList<Group> userGroups = _securityService.GetGroupsByUser(_userId);
				IList<Group> allGroups = _securityService.GetGroups();

				cblGroups.DataSource = allGroups;
				cblGroups.DataBind();
				foreach (ListItem item in cblGroups.Items)
				{
          if (userGroups.Select(p => p.Id).Contains(Guid.Parse(item.Value)))
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
			//список идентификаторов назначаемых групп
      List<Guid> assignList = new List<Guid>();
			//список идентификаторов групп, из которых надо удалить пользователя
      List<Guid> detachList = new List<Guid>();
			foreach (ListItem item in cblGroups.Items)
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
			_securityService.AssignGroupsToUser(newUserId != Guid.Empty ? newUserId : _userId, assignList, detachList);
		}

	}
}