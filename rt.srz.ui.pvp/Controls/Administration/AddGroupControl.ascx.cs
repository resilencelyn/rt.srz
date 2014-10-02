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
	public partial class AddGroupControl : System.Web.UI.UserControl
	{
		private ISecurityService _securityService;
		private Group _group;

		protected void Page_Init(object sender, EventArgs e)
		{
			_securityService = ObjectFactory.GetInstance<ISecurityService>();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["GroupId"] == null)
			{
				_group = new Group();
			}
			else
			{
				_group = _securityService.GetGroup(Guid.Parse(Request.QueryString["GroupId"]));
			}
			if (!IsPostBack)
			{
				if (Request.QueryString["GroupId"] == null)
				{
					lbTitle.Text = "Добавление группы";
					return;
				}
				tbName.Text = _group.Name;
				lbTitle.Text = "Редактирование группы";
			}
		}

		public Guid SaveChanges()
		{
			if (_group != null)
			{
				_group.Name = tbName.Text;
			}
			return _securityService.SaveGroup(_group);
		}
	}
}