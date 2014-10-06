// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageChecks.aspx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages.Administrations
{
  using System;
  using System.Collections.Generic;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model;
  using rt.core.model.interfaces;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces;
  using rt.srz.model.interfaces.service;

  using StructureMap;

  /// <summary>
  /// The manage checks.
  /// </summary>
  public partial class ManageChecks : Page
  {
    #region Fields

    /// <summary>
    /// The _allow install.
    /// </summary>
    private bool allowInstall;

    /// <summary>
    /// The _is admin.
    /// </summary>
    private bool isAdmin;

    /// <summary>
    /// The _is admin tf.
    /// </summary>
    private bool isAdminTf;

    /// <summary>
    /// The _sec.
    /// </summary>
    private ISecurityService sec;

    /// <summary>
    /// The _service.
    /// </summary>
    private IStatementService service;

    #endregion

    #region Methods

    /// <summary>
    /// The on data bound.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void OnDataBound(object sender, EventArgs e)
    {
      if (isAdmin && allowInstall)
      {
        return;
      }

      for (var i = 0; i < grid.Rows.Count; i++)
      {
        var row = grid.Rows[i];

        if (row.RowType == DataControlRowType.DataRow)
        {
          ////для админа не дизаблим рядок грида т.к. он сам назначает можно ли менять указанную проверку
          ////если у проверки не проставлен признак AllowCheck, то изменять значение проверять или нет нельзя
          // bool res;
          // if (!_currentUser.IsAdmin && bool.TryParse(((HiddenField)row.Cells[1].FindControl("hfAllowChange")).Value, out res) && !res)
          // {
          // row.BackColor = Color.LightGray;
          // row.Enabled = false;
          // continue;
          // }
          ////если не админ то нельзя изменять AllowChange - почему-то зараза cbAllowChange_CheckedChanged  срабатывает даже если скрыта колонка но не задизаблен контрол
          // if (!_currentUser.IsAdmin)
          // {
          // ((CheckBox)row.Cells[1].FindControl("cbAllowChange")).Enabled = false;
          // }
          bool res;

          // 2. Админ ТФ может видеть все, но снимать только те 4 штуки. Поэтому если свойство Visible = false значит запрещаем редактирование
          if (isAdminTf && bool.TryParse(((HiddenField)row.Cells[1].FindControl("hfVisible")).Value, out res) && !res)
          {
            row.Enabled = false;
            ((CheckBox)row.Cells[1].FindControl("cbCheck")).Enabled = false;
            continue;
          }

          // 3. Супер админ может видеть все, снимать может только 4 штуки, НО если доступен пукт установка, то он может снимать все.
          if (isAdmin && bool.TryParse(((HiddenField)row.Cells[1].FindControl("hfVisible")).Value, out res) && !res)
          {
            row.Enabled = false;
            ((CheckBox)row.Cells[1].FindControl("cbCheck")).Enabled = false;

            // ((CheckBox)row.Cells[1].FindControl("cbAllowChange")).Enabled = false;
          }
        }
      }
    }

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
      service = ObjectFactory.GetInstance<IStatementService>();
      sec = ObjectFactory.GetInstance<ISecurityService>();
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
      var helper = new GridGroupHelperSimpleSpan<ICheckStatement>(grid, x => x.LevelDescription);
      if (!IsPostBack)
      {
        var currentUser = sec.GetCurrentUser();
        isAdminTf = sec.IsUserAdminTF(currentUser.Id);
        isAdmin = currentUser.IsAdmin;
        allowInstall = sec.GetIsCurrentUserAllowPermission(PermissionCode.Installation);
        ViewState["isAdmin"] = isAdmin;
        ViewState["isAdminTf"] = isAdminTf;
        ViewState["allowInstall"] = allowInstall;

        // если пользователь не главный администратор, то ему запрещено выставлять возможность включения-отключения проверок
        // ни для какого пользователя нельзя выбирать возможность включения выключения проверок
        // if (_currentUser.IsAdmin)
        // {
        // grid.Columns[3].Visible = true;
        // }

        ////если есть права на отображение установки, то отображаем все проверки независимо от свойства видимость
        // IList<ICheckStatement> list;
        // if (_sec.GetIsCurrentUserAllowPermission(PermissionCode.Installation))
        // {
        // list = ObjectFactory.GetAllInstances<ICheckStatement>();
        // }
        // else
        // {
        // list = ObjectFactory.GetAllInstances<ICheckStatement>().Where(x => x.Visible == true).ToList();
        // }

        // 1. Админ СМО их видеть не должен! Не его это дело. 
        // 2. Админ ТФ может видеть все, но снимать только те 4 штуки. 
        // 3. Супер админ может видеть все, снимать может только 4 штуки, НО если доступен пукт установка, то он может снимать все.
        IList<ICheckStatement> list = null;

        // т.е. список заполняем данными только если это админ или админ тф
        if (isAdminTf || isAdmin)
        {
          list = ObjectFactory.GetAllInstances<ICheckStatement>();
        }

        grid.DataSource = helper.GetDataList(grid, x => x.RecordNumber, list);
        grid.DataBind();
      }
      else
      {
        isAdmin = (bool)ViewState["isAdmin"];
        isAdminTf = (bool)ViewState["isAdminTf"];
        allowInstall = (bool)ViewState["allowInstall"];
      }
    }

    /// <summary>
    /// The cb allow change_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void CbAllowChangeCheckedChanged(object sender, EventArgs e)
    {
      // CheckBox cbox = (CheckBox)sender;
      // GridViewRow row = (GridViewRow)cbox.NamingContainer;
      // grid.SelectRow(row.RowIndex);

      // HiddenField className = (HiddenField)row.FindControl("hfClassName");
      // if (!cbox.Checked)
      // {
      // //add to settings data row
      // _service.AddAllowChangeSetting(Check.GetAllowChangeName(className.Value));
      // }
      // else
      // {
      // //remove data row
      // _service.RemoveAllowChangeSetting(Check.GetAllowChangeName(className.Value));
      // }
    }

    /// <summary>
    /// The cb check_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void CbCheckCheckedChanged(object sender, EventArgs e)
    {
      var cbox = (CheckBox)sender;
      var row = (GridViewRow)cbox.NamingContainer;
      grid.SelectRow(row.RowIndex);

      var className = (HiddenField)row.FindControl("hfClassName");
      if (!cbox.Checked)
      {
        // add to settings data row
        service.AddSetting(className.Value);
      }
      else
      {
        // remove data row
        service.RemoveSetting(className.Value);
      }
    }

    /// <summary>
    /// The grid_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void GridSelectedIndexChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The grid_ sorting.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void GridSorting(object sender, GridViewSortEventArgs e)
    {
    }

    #endregion
  }
}