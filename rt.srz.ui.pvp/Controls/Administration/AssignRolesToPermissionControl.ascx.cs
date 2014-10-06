// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssignRolesToPermissionControl.ascx.cs" company="РусБИТех">
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
  /// The assign roles to permission control.
  /// </summary>
  public partial class AssignRolesToPermissionControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _permission id.
    /// </summary>
    private Guid permissionId;

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
    /// <param name="newPermissionId">
    /// The new permission id.
    /// </param>
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

      securityService.AssignRolesToPermission(
                                               newPermissionId != Guid.Empty ? newPermissionId : permissionId, 
                                               assignList, 
                                               detachList);
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
      permissionId = Request.QueryString["permissionId"] == null ? Guid.Empty : Guid.Parse(Request.QueryString["permissionId"]);

      if (!IsPostBack)
      {
        lbTitle.Text = string.Format("Назначение ролей для разрешения: {0}", Request.QueryString["permissionName"]);
        var roles = securityService.GetRolesByPermission(permissionId);
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