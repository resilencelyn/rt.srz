using rt.srz.business.interfaces.logicalcontrol;
using rt.srz.business.manager.logicalcontrol;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  public partial class ManageChecks : System.Web.UI.Page
  {
    private IStatementService _service;
    private ISecurityService _sec;
    private bool _isAdmin;
    private bool _isAdminTf;
    private bool _allowInstall;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IStatementService>();
      _sec = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      GridGroupHelperSimpleSpan<ICheckStatement> helper = new GridGroupHelperSimpleSpan<ICheckStatement>(grid, x => x.LevelDescription);
      if (!IsPostBack)
      {
        User currentUser = _sec.GetCurrentUser();
        _isAdminTf = _sec.IsUserAdminTF(currentUser.Id);
        _isAdmin = currentUser.IsAdmin;
        _allowInstall = _sec.GetIsCurrentUserAllowPermission(PermissionCode.Installation);
        ViewState["isAdmin"] = _isAdmin;
        ViewState["isAdminTf"] = _isAdminTf;
        ViewState["allowInstall"] = _allowInstall;

        //если пользователь не главный администратор, то ему запрещено выставлять возможность включения-отключения проверок
        //ни для какого пользователя нельзя выбирать возможность включения выключения проверок
        //if (_currentUser.IsAdmin)
        //{
        //  grid.Columns[3].Visible = true;
        //}

        ////если есть права на отображение установки, то отображаем все проверки независимо от свойства видимость
        //IList<ICheckStatement> list;
        //if (_sec.GetIsCurrentUserAllowPermission(PermissionCode.Installation))
        //{
        //  list = ObjectFactory.GetAllInstances<ICheckStatement>();
        //}
        //else
        //{
        //  list = ObjectFactory.GetAllInstances<ICheckStatement>().Where(x => x.Visible == true).ToList();
        //}

        //1. Админ СМО их видеть не должен! Не его это дело. 
        //2. Админ ТФ может видеть все, но снимать только те 4 штуки. 
        //3. Супер админ может видеть все, снимать может только 4 штуки, НО если доступен пукт установка, то он может снимать все.
        IList<ICheckStatement> list = null;
        //т.е. список заполняем данными только если это админ или админ тф
        if (_isAdminTf || _isAdmin)
        {
          list = ObjectFactory.GetAllInstances<ICheckStatement>();
        }

        grid.DataSource = helper.GetDataList(grid, x => x.RecordNumber, list);
        grid.DataBind();
      }
      else
      {
        _isAdmin = (bool)ViewState["isAdmin"];
        _isAdminTf = (bool)ViewState["isAdminTf"];
        _allowInstall = (bool)ViewState["allowInstall"];
      }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
      if (_isAdmin && _allowInstall)
      {
        return;
      }

      for (int i = 0; i < grid.Rows.Count; i++)
      {
        GridViewRow row = grid.Rows[i];

        if (row.RowType == DataControlRowType.DataRow)
        {
          ////для админа не дизаблим рядок грида т.к. он сам назначает можно ли менять указанную проверку
          ////если у проверки не проставлен признак AllowCheck, то изменять значение проверять или нет нельзя
          //bool res;
          //if (!_currentUser.IsAdmin && bool.TryParse(((HiddenField)row.Cells[1].FindControl("hfAllowChange")).Value, out res) && !res)
          //{
          //  row.BackColor = Color.LightGray;
          //  row.Enabled = false;
          //  continue;
          //}
          ////если не админ то нельзя изменять AllowChange - почему-то зараза cbAllowChange_CheckedChanged  срабатывает даже если скрыта колонка но не задизаблен контрол
          //if (!_currentUser.IsAdmin)
          //{
          //  ((CheckBox)row.Cells[1].FindControl("cbAllowChange")).Enabled = false;
          //}

          bool res;
          //2. Админ ТФ может видеть все, но снимать только те 4 штуки. Поэтому если свойство Visible = false значит запрещаем редактирование
          if (_isAdminTf && bool.TryParse(((HiddenField)row.Cells[1].FindControl("hfVisible")).Value, out res) && !res)
          {
            row.Enabled = false;
            ((CheckBox)row.Cells[1].FindControl("cbCheck")).Enabled = false;
            continue;
          }

          //3. Супер админ может видеть все, снимать может только 4 штуки, НО если доступен пукт установка, то он может снимать все.
          if ((_isAdmin && bool.TryParse(((HiddenField)row.Cells[1].FindControl("hfVisible")).Value, out res) && !res))
          {
            row.Enabled = false;
            ((CheckBox)row.Cells[1].FindControl("cbCheck")).Enabled = false;
            //((CheckBox)row.Cells[1].FindControl("cbAllowChange")).Enabled = false;
          }
        }
      }
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void cbCheck_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox cbox = (CheckBox)sender;
      GridViewRow row = (GridViewRow)cbox.NamingContainer;
      grid.SelectRow(row.RowIndex);

      HiddenField className = (HiddenField)row.FindControl("hfClassName");
      if (!cbox.Checked)
      {
        //add to settings data row
        _service.AddSetting(className.Value);
      }
      else
      {
        //remove data row
        _service.RemoveSetting(className.Value);
      }
    }

    protected void cbAllowChange_CheckedChanged(object sender, EventArgs e)
    {
      //CheckBox cbox = (CheckBox)sender;
      //GridViewRow row = (GridViewRow)cbox.NamingContainer;
      //grid.SelectRow(row.RowIndex);

      //HiddenField className = (HiddenField)row.FindControl("hfClassName");
      //if (!cbox.Checked)
      //{
      //  //add to settings data row
      //  _service.AddAllowChangeSetting(Check.GetAllowChangeName(className.Value));
      //}
      //else
      //{
      //  //remove data row
      //  _service.RemoveAllowChangeSetting(Check.GetAllowChangeName(className.Value));
      //}
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

  }
}