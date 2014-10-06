using rt.srz.model.dto;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RangeNumberData = rt.srz.model.srz.RangeNumber;

namespace rt.srz.ui.pvp.Controls.NSI
{
  using rt.core.model.interfaces;

  public partial class RangeNumberControl : System.Web.UI.UserControl
  {
    #region Fields

    private INsiService _service;
    private ISmoService _smoService;
    private ISecurityService _sec;

    #endregion

    #region Properties

    private RangeNumberData CurrentRange
    {
      get { return ViewState["CurrentRange"] != null ? (RangeNumberData)ViewState["CurrentRange"] : null; }
      set { ViewState["CurrentRange"] = value; }
    }

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<INsiService>();
      _smoService = ObjectFactory.GetInstance<ISmoService>();
      _sec = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SetButtonsEnable(false);

        var criteria = new SearchSmoCriteria();
        criteria.Take = int.MaxValue;
        criteria.Oid = Oid.Smo;

        dlSmo.DataSource = _smoService.GetSmos(criteria).Rows; //_smoService.GetSmosByTfom(fomId);
        dlSmo.DataBind();

        if (Request.QueryString["Id"] == null)
        {
          CurrentRange = new RangeNumberData();
        }
        else
        {
          CurrentRange = _service.GetRangeNumber(Guid.Parse(Request.QueryString["Id"]));
        }
        LoadGridData();
        SetPreviousData(true);

        if (Request.QueryString["Id"] == null)
        {
          lbTitle.Text = "Добавление выделенного диапазона номеров вс";
          return;
        }
        tbFrom.Text = CurrentRange.RangelFrom.ToString();
        tbTo.Text = CurrentRange.RangelTo.ToString();
        dlSmo.SelectedValue = CurrentRange.Smo != null ? CurrentRange.Smo.Id.ToString() : null;
        //Редактирование выделенного диапазона номеров вс
        if (CurrentRange.ChangeDate.HasValue)
        {
          lbTitle.Text = string.Format("Выделенный диапазон номеров временных свидетельств от {0}", CurrentRange.ChangeDate.Value.ToShortDateString());
        }
        else
        {
          lbTitle.Text = "Выделенный диапазон номеров временных свидетельств";
        }
      }
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          SaveGridDataToObject();
          AddNewRowToGrid();
          break;
        case "Delete":
          SaveGridDataToObject();
          List<Guid> deletedIdRows = GetSelected();
          RangeNumberData range = CurrentRange;
          range.RangeNumbers = range.RangeNumbers.Where(x => !deletedIdRows.Contains(x.Id)).ToList();
          CurrentRange = range;
          LoadGridData();
          SetPreviousData(true);
          SetButtonsEnable(false);
          break;
      }
    }

    #endregion

    #region Validation

    #region Шапка

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
      var rn = new RangeNumberData();
      rn.Id = CurrentRange.Id;
      SetObjValues(rn);
      args.IsValid = !_service.IntersectWithOther(rn);
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
      var rn = new RangeNumberData();
      rn.Id = CurrentRange.Id;
      SetObjValues(rn);
      args.IsValid = rn.RangelFrom <= rn.RangelTo;
    }

    #endregion

    #region Grid

    protected void cvGridEmptyTemplate_ServerValidate(object source, ServerValidateEventArgs args)
    {
      SaveGridDataToObject();
      if (CurrentRange == null)
      {
        return;
      }
      foreach(RangeNumber subrange in CurrentRange.RangeNumbers)
      {
        if (subrange.Template == null)
        {
          args.IsValid = false;
          return;
        }
      }
    }

    protected void cvGridStartLargerEnd_ServerValidate(object source, ServerValidateEventArgs args)
    {
      SaveGridDataToObject();
      if (CurrentRange == null)
      {
        return;
      }
      foreach (RangeNumber subrange in CurrentRange.RangeNumbers)
      {
        if (subrange.RangelFrom > subrange.RangelTo)
        {
          args.IsValid = false;
          return;
        }
      }
    }

    protected void cvGridOutOfRange_ServerValidate(object source, ServerValidateEventArgs args)
    {
      SaveGridDataToObject();
      if (CurrentRange == null)
      {
        return;
      }
      foreach (RangeNumber subrange in CurrentRange.RangeNumbers)
      {
        if (subrange.RangelFrom < CurrentRange.RangelFrom || subrange.RangelTo > CurrentRange.RangelTo)
        {
          args.IsValid = false;
          return;
        }
      }
    }

    protected void cvGridIntersect_ServerValidate(object source, ServerValidateEventArgs args)
    {
      SaveGridDataToObject();
      if (CurrentRange == null)
      {
        return;
      }
      var intervals = CurrentRange.RangeNumbers;
      intervals.OrderBy(x => x.RangelFrom);

      var started = new RangeNumber();
      for (int i = 0; i < intervals.Count; i++)
      {
        if (i == 0)
        {
          started = intervals[0];
        }
        else
        {
          if (started.RangelFrom <= intervals[i].RangelFrom && intervals[i].RangelFrom <= started.RangelTo)
          {
            args.IsValid = false;
            return;
          }
          started = intervals[i];
        }
      }
    }

    #endregion

    #endregion

    #region Save

    private void SetObjValues(RangeNumberData obj)
    {
      obj.RangelFrom = int.Parse(tbFrom.Text);
      obj.RangelTo = int.Parse(tbTo.Text);
      obj.Smo = _smoService.GetSmo(Guid.Parse(dlSmo.SelectedValue));
    }

    public void SaveChanges()
    {
      SaveGridDataToObject();
      var range = CurrentRange;
      SetObjValues(range);
      _service.AddOrUpdateRangeNumber(range);
    }

    private void SetButtonsEnable(bool value)
    {
      //UtilsHelper.SetMenuButtonsEnable(value, MenuUpdatePanel, menu1);
      //var item = menu1.FindItem("Delete");
      //item.Enabled = GetSelected().Count > 0;
      //MenuUpdatePanel.Update();
    }

    #endregion

    #region Grid Methods

    public List<Guid> GetSelected()
    {
      var result = new List<Guid>();
      foreach (GridViewRow row in grid.Rows)
      {
        CheckBox cbCheck = row.FindControl("cbCheck") as CheckBox;
        if (cbCheck.Checked)
        {
          result.Add((Guid)grid.DataKeys[row.RowIndex].Value);
        }
      }
      return result;
    }

    private void LoadGridData()
    {
      grid.DataSource = CurrentRange.RangeNumbers;
      grid.DataBind();
      for (int i = 0; i < grid.Rows.Count; i++)
      {
        DropDownList ddl = (DropDownList)grid.Rows[i].Cells[2].FindControl("ddlTemplate");
        FillTemplatesDropDown(ddl);
      }
    }

    private void AddNewRowToGrid()
    {
      if (CurrentRange == null)
      {
        return;
      }
      RangeNumberData newRow = new RangeNumberData();
      newRow.Id = Guid.NewGuid();
      newRow.Parent = CurrentRange;
      newRow.Smo = CurrentRange.Smo;
      RangeNumberData range = CurrentRange;
      range.RangeNumbers.Add(newRow);
      CurrentRange = range;
      grid.DataSource = CurrentRange.RangeNumbers;
      grid.DataBind();
      SetPreviousData();
    }

    private void SetPreviousData(bool updateAllRows = false)
    {
      int rowIndex = 0;
      if (CurrentRange == null)
      {
        return;
      }
      if (CurrentRange.RangeNumbers.Count > 0)
      {
        for (int i = 0; i < CurrentRange.RangeNumbers.Count; i++)
        {
          DropDownList ddl = (DropDownList)grid.Rows[rowIndex].Cells[2].FindControl("ddlTemplate");
          FillTemplatesDropDown(ddl);
          if ((i < CurrentRange.RangeNumbers.Count - 1) || updateAllRows)
          {
            TextBox box1 = (TextBox)grid.Rows[rowIndex].Cells[0].FindControl("tbRangelFrom");
            TextBox box2 = (TextBox)grid.Rows[rowIndex].Cells[1].FindControl("tbRangelTo");
            box1.Text = CurrentRange.RangeNumbers[i].RangelFrom.ToString();
            box2.Text = CurrentRange.RangeNumbers[i].RangelTo.ToString();

            ddl.ClearSelection();
            if (CurrentRange.RangeNumbers[i].Template != null)
            {
              ddl.Items.FindByValue(CurrentRange.RangeNumbers[i].Template.Id.ToString()).Selected = true;
            }
          }
          rowIndex++;
        }
      }
    }


    private void FillTemplatesDropDown(DropDownList ddl)
    {
      ddl.DataSource = _service.GetTemplates();
      ddl.DataBind();
    }

    private void SaveGridDataToObject()
    {
      RangeNumberData range = CurrentRange;
      for (int i = 0; i < range.RangeNumbers.Count; i++)
      {
        TextBox box1 = (TextBox)grid.Rows[i].Cells[0].FindControl("tbRangelFrom");
        TextBox box2 = (TextBox)grid.Rows[i].Cells[1].FindControl("tbRangelTo");
        int value;
        if (int.TryParse(box1.Text, out value))
        {
          range.RangeNumbers[i].RangelFrom = value;
        }
        if (int.TryParse(box2.Text, out value))
        {
          range.RangeNumbers[i].RangelTo = value;
        }
        DropDownList ddl = (DropDownList)grid.Rows[i].Cells[2].FindControl("ddlTemplate");
        if (!string.IsNullOrEmpty(ddl.SelectedValue))
        {
          range.RangeNumbers[i].Template = _service.GetTemplate(Guid.Parse(ddl.SelectedValue));
        }
      }
      CurrentRange = range;
    }

    protected void btnSaveRowChanges_Click(object sender, EventArgs e)
    {
      SaveGridDataToObject();
    }

    #endregion

  }
}