// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssignPermissionsToRoleControl.ascx.cs" company="Альянс">
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
  /// The assign permissions to role control.
  /// </summary>
  public partial class AssignPermissionsToRoleControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _role id.
    /// </summary>
    private Guid roleId;

    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService securityService;

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
    /// <param name="newRoleId">
    /// The new role id.
    /// </param>
    public void SaveChanges(Guid newRoleId)
    {
      // список идентификаторов назначаемых разрешений
      var assignList = new List<Guid>();

      // список идентификаторов разрешений, которые надо отсоединить от роли
      var detachList = new List<Guid>();
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

      securityService.AssignPermissionsToRole(newRoleId != Guid.Empty ? newRoleId : roleId, assignList, detachList);
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
      roleId = Request.QueryString["RoleId"] == null ? Guid.Empty : Guid.Parse(Request.QueryString["RoleId"]);

      if (!IsPostBack)
      {
        lbTitle.Text = string.Format("Добавление разрешений для роли: {0}", Request.QueryString["RoleName"]);

        var rolesPermissions = securityService.GetRolePermissions(roleId);
        var allPermissions = securityService.GetPermissions();

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

    #endregion
  }
}