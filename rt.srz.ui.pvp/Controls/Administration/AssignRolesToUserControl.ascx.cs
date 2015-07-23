// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssignRolesToUserControl.ascx.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Administration
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;

  using StructureMap;

  /// <summary>
  /// The assign roles to user control.
  /// </summary>
  public partial class AssignRolesToUserControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService securityService;

    /// <summary>
    /// The _user id.
    /// </summary>
    private Guid userId;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The save changes.
    /// </summary>
    public void SaveChanges()
    {
      SaveChanges(Guid.Empty);
    }

    /// <summary>
    /// The save changes.
    /// </summary>
    /// <param name="newUserId">
    /// The new user id.
    /// </param>
    public void SaveChanges(Guid newUserId)
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

      securityService.AssignRolesToUser(newUserId != Guid.Empty ? newUserId : userId, assignList, detachList);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The page_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Init(object sender, EventArgs e)
    {
      securityService = ObjectFactory.GetInstance<ISecurityService>();
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      userId = Request.QueryString["userId"] == null ? Guid.Empty : Guid.Parse(Request.QueryString["userId"]);

      if (!IsPostBack)
      {
        lbTitle.Text = string.Format("Назначение ролей для пользователя: {0}", Request.QueryString["userName"]);
        var roles = securityService.GetRolesByUser(userId);
        var allRoles = securityService.GetRoles();

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

    #endregion
  }
}