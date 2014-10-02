// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PdpDetailWorkstationListControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using rt.srz.ui.pvp.Enumerations;
using AjaxControlToolkit;

#endregion

namespace rt.srz.ui.pvp.Controls.Administration
{
  // словарь соответсвующий рабочим станциям (пункт выдачи, список станций) сохраняется так же в сесии Session["workstationDict"]

  /// <summary>
  /// The pdp detail workstation list control.
  /// </summary>
  public partial class PdpDetailWorkstationListControl : UserControl
  {
    /// <summary>
    /// The _smo service.
    /// </summary>
    private ISmoService _smoService;

    /// <summary>
    /// The bind parent list.
    /// </summary>
    public event Action<IList<PdpGridRow>> BindParentList;
    public event Action<bool> EnabledPdpGrid;

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
      _smoService = ObjectFactory.GetInstance<ISmoService>();
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
      workstationDetailControl.BindParentList += BindParentListHandle;
      workstationDetailControl.EnabledGrid += EnableGrid;
      if (!IsPostBack)
      {
        UtilsHelper.SetMenuButtonsEnable(SelectedWorkstation != null, null, menu1);
        Session[SessionConsts.CWorkstationDict] = new Dictionary<Guid, IList<WorkstationGridRow>>();
      }
    }

    /// <summary>
    /// The bind parent list handle.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    private void BindParentListHandle(IList<WorkstationGridRow> list)
    {
      gridView.DataSource = list;
      gridView.DataBind();
    }

    protected void EnableGrid(bool value)
    {
      gridView.Enabled = value;
      if (!value)
      {
        foreach (GridViewRow row in gridView.Rows)
        {
          row.Attributes.Remove("onmouseover");
          row.Attributes.Remove("onmouseout");
          row.Attributes.Remove("onclick");
        }
      }
      else
      {
        foreach (GridViewRow row in gridView.Rows)
        {
          row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
          row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
          row.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView, "Select$" + row.RowIndex);
        }
      }
      menu1.Enabled = value;
    }

    /// <summary>
    /// The save pdp grid list.
    /// </summary>
    /// <param name="smoId">
    /// The smo id.
    /// </param>
    public void SavePdpGridList(Guid smoId)
    {
      var pvpList = new List<Organisation>();
      var pdpGridRows = (IList<PdpGridRow>)Session[SessionConsts.CPdpList];
      if (pdpGridRows != null)
      {
        foreach (var item in pdpGridRows)
        {
          if (item.Parent == null)
          {
            item.Parent = new Organisation();
          }

          // получаем все рабочие станции для пдп (которые зачитали - их могли и изменять, удалять, добавлять)
          // если станций нету в сессии значит их не изменяли
          var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
          IList<WorkstationGridRow> workstations;
          if (dict.ContainsKey(item.Id))
          {
            workstations = dict[item.Id];
            if (item.Workstations == null)
            {
              item.Workstations = new List<Workstation>();
            }
            else
            {
              item.Workstations.Clear();
            }

            // для текущего пдп если изменяли его workstations назначаем эти значения из сессии
            foreach (var w in workstations)
            {
              if (w.IsNew)
              {
                w.Id = Guid.Empty;
              }
              item.Workstations.Add(w.Workstation);
            }
          }

          // обнуляем присвоенные ранее идентификаторы для добавленных но не сохраненных пдп
          if (item.IsNew)
          {
            item.Id = Guid.Empty;
            if (item.Workstations != null)
            {
              foreach (var w in item.Workstations)
              {
                w.PointDistributionPolicy = item.Pdp;
                w.PointDistributionPolicy.Id = Guid.Empty;
              }
            }
          }

          item.Parent.Id = smoId;
          pvpList.Add(item.Pdp);
        }
      }

      if (pvpList.Count > 0)
      {
        _smoService.SavePdps(smoId, pvpList);
      }
    }

    /// <summary>
    /// The assign control values from pdp.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public void AssignControlValuesFromPdp(PdpGridRow row)
    {
      // заполняем данные пункта выдачи
      tbFullName.Text = row.FullName;
      tbShortName.Text = row.ShortName;
      tbCode.Text = row.Code;
      tbFirstName.Text = row.FirstName;
      tbLastName.Text = row.LastName;
      tbMiddleName.Text = row.MiddleName;
      tbPhone.Text = row.Phone;
      tbFax.Text = row.Fax;
      tbEmail.Text = row.EMail;

      // заполняем рабочие станции, соответствующие пункту выдачи
      var workstations = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
      if (workstations.ContainsKey(row.Id))
      {
        gridView.DataSource = workstations[row.Id];
        gridView.DataBind();
      }
      else if (!row.IsNew)
      {
        var list = _smoService.GetWorkstationsByPdp(row.Id);
        IList<WorkstationGridRow> rows;
        if (list != null)
        {
          rows = list.Select(p => new WorkstationGridRow(p)).ToList();
        }
        else
        {
          rows = new List<WorkstationGridRow>();
        }

        workstations.Add(row.Id, rows);
        Session[SessionConsts.CWorkstationDict] = workstations;
        gridView.DataSource = rows;
        gridView.DataBind();
      }
    }

    /// <summary>
    /// The set enable controls.
    /// </summary>
    /// <param name="enable">
    /// The enable.
    /// </param>
    public void SetEnableControls(bool enable)
    {
      rfShortName.Enabled = enable;
      rfFullName.Enabled = enable;
      rfCode.Enabled = enable;
      vEmail.Enabled = enable;

      tbCode.Enabled = enable;
      tbEmail.Enabled = enable;
      tbFax.Enabled = enable;
      tbFirstName.Enabled = enable;
      tbFullName.Enabled = enable;
      tbLastName.Enabled = enable;
      tbMiddleName.Enabled = enable;
      tbPhone.Enabled = enable;
      tbShortName.Enabled = enable;
      btnSave.Enabled = enable;
      btnCancel.Enabled = enable;

      // lbDetailCaption.Visible = enable;
      menu1.Enabled = enable;
      UtilsHelper.SetMenuButtonsEnable(SelectedWorkstation != null, null, menu1);
      gridView.Enabled = enable;

      if (!enable)
      {
        SetPdpDetailCaption("Пункт выдачи");
        workstationDetailControl.SetEnableControls(false);
      }
    }

    /// <summary>
    /// The clear control values.
    /// </summary>
    public void ClearControlValues()
    {
      tbCode.Text = string.Empty;
      tbEmail.Text = string.Empty;
      tbFax.Text = string.Empty;
      tbFirstName.Text = string.Empty;
      tbFullName.Text = string.Empty;
      tbLastName.Text = string.Empty;
      tbMiddleName.Text = string.Empty;
      tbPhone.Text = string.Empty;
      tbShortName.Text = string.Empty;
      gridView.DataSource = null;
      gridView.DataBind();
      workstationDetailControl.ClearControlValues();
    }

    #region Workstation

    /// <summary>
    /// Gets the selected workstation.
    /// </summary>
    private WorkstationGridRow SelectedWorkstation
    {
      get
      {
        if (CurrentPdpId == null)
        {
          return null;
        }

        var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
        return dict == null || !dict.ContainsKey((Guid)CurrentPdpId) || gridView.SelectedDataKey == null ? null :
          dict[(Guid)CurrentPdpId].Where(r => r.Id == (Guid)gridView.SelectedDataKey.Value).FirstOrDefault();
      }
    }

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      //если задизаблено меню то нечего выбирать элементы, иначе кнопки останутся задизаблены в менюшке а рядок сменится.
      if (!menu1.Enabled)
      {
        base.Render(writer);
        return;
      }

      UtilsHelper.AddAttributesToGridRow(Page.ClientScript, gridView);
      UtilsHelper.AddDoubleClickAttributeToGrid(Page.ClientScript, gridView);

      //foreach (GridViewRow r in gridView.Rows)
      //{
      //  if (r.RowType == DataControlRowType.DataRow)
      //  {
      //    r.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
      //    r.Attributes["onmouseout"] = "this.style.textDecoration='none';";
      //    r.ToolTip = "Click to select row";
      //    r.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView, "Select$" + r.RowIndex, true);
      //  }
      //}

      base.Render(writer);
    }

    /// <summary>
    /// The grid view_ deleting.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void gridView_Deleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    /// <summary>
    /// The grid view_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      workstationDetailControl.ChangeWorkstation(gridView, CurrentPdpId);
      UtilsHelper.SetMenuButtonsEnable(SelectedWorkstation != null, null, menu1);
    }

    /// <summary>
    /// The menu_ menu item click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          workstationDetailControl.AddWorkstation();

          // contentUpdatePanel.Update();
          break;
        case "Open":
          OpenWorkstation();
          // contentUpdatePanel.Update();
          break;
        case "Delete":
          if (gridView.SelectedDataKey == null)
          {
            return;
          }

          workstationDetailControl.DeleteWorkstation(SelectedWorkstation, gridView, CurrentPdpId);
          UtilsHelper.SetMenuButtonsEnable(SelectedWorkstation != null, null, menu1);

          // contentUpdatePanel.Update();
          break;
      }
    }

    private void OpenWorkstation()
    {
      if (gridView.SelectedDataKey == null)
      {
        return;
      }

      workstationDetailControl.OpenWorkstation(SelectedWorkstation);
    }

    #endregion

    #region Pdp

    /// <summary>
    /// Gets the current pdp id.
    /// </summary>
    private Guid? CurrentPdpId
    {
      get { return (Guid?)Session[SessionConsts.CPdpId]; }
    }

    /// <summary>
    /// The set pdp detail caption.
    /// </summary>
    /// <param name="caption">
    /// The caption.
    /// </param>
    private void SetPdpDetailCaption(string caption)
    {
      lbDetailCaption.Text = caption;
    }

    /// <summary>
    /// The set pdp view state values.
    /// </summary>
    /// <param name="pdpId">
    /// The pdp id.
    /// </param>
    /// <param name="isNew">
    /// The is new.
    /// </param>
    private void SetPdpViewStateValues(Guid? pdpId, bool? isNew)
    {
      Session[SessionConsts.CPdpId] = pdpId;
      ViewState["pdpIsNew"] = isNew;
    }

    /// <summary>
    /// The load pdps.
    /// </summary>
    /// <param name="smoId">
    /// The smo id.
    /// </param>
    /// <param name="gridView">
    /// The grid view.
    /// </param>
    public void LoadPdps(Guid smoId, GridView gridView)
    {
      var list = _smoService.GetPDPsBySmo(smoId);
      IList<PdpGridRow> rows;
      if (list != null)
      {
        rows = list.Select(p => new PdpGridRow(p)).ToList();
      }
      else
      {
        rows = new List<PdpGridRow>();
      }

      Session[SessionConsts.CPdpList] = rows;
      gridView.DataSource = rows;
      gridView.DataBind();

      SetEnableControls(false);
    }

    /// <summary>
    /// The add pdp.
    /// </summary>
    public void AddPdp()
    {
      if (EnabledPdpGrid != null)
      {
        EnabledPdpGrid(false);
      }
      SetPdpDetailCaption("Добавление пункта выдачи");
      SetPdpViewStateValues(Guid.Empty, true);
      SetEnableControls(true);
      ClearControlValues();
      tbShortName.Focus();
    }

    /// <summary>
    /// The open pdp.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public void OpenPdp(PdpGridRow row)
    {
      if (EnabledPdpGrid != null)
      {
        EnabledPdpGrid(false);
      }
      SetPdpDetailCaption("Редактирование пункта выдачи");
      SetPdpViewStateValues(row.Id, row.IsNew);
      AssignControlValuesFromPdp(row);
      SetEnableControls(true);
      tbShortName.Focus();
    }

    /// <summary>
    /// The delete pdp.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="gridView">
    /// The grid view.
    /// </param>
    public void DeletePdp(PdpGridRow row, GridView gridView)
    {
      var list = (IList<PdpGridRow>)Session[SessionConsts.CPdpList];
      list.Remove(row);
      Session[SessionConsts.CPdpList] = list;
      var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
      if (dict.ContainsKey(row.Id))
      {
        dict.Remove(row.Id);
      }

      Session[SessionConsts.CWorkstationDict] = dict;
      gridView.SelectedIndex = -1;
      gridView.DataSource = list;
      gridView.DataBind();
      ClearControlValues();
    }

    /// <summary>
    /// Смена выбранного пдп в гриде
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="gridView">
    /// </param>
    public void ChangePdp(PdpGridRow row, GridView gridView)
    {
      SetPdpViewStateValues(row.Id, null);
      SetEnableControls(false);
      if (gridView.SelectedIndex < 0)
      {
        ClearControlValues();
      }
      else
      {
        var list = (IList<PdpGridRow>)Session[SessionConsts.CPdpList];
        workstationDetailControl.ClearControlValues();
        AssignControlValuesFromPdp(list.Where(r => r.Id == (Guid)gridView.SelectedDataKey.Value).First());
      }
    }

    /// <summary>
    /// The v code_ server validate.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void vCode_ServerValidate(object source, ServerValidateEventArgs args)
    {
      if (CurrentPdpId == null)
      {
        args.IsValid = true;
        return;
      }
      // проверяем чтобы в пределах заданной смо все пдп имели разный код
      var list = (IList<PdpGridRow>)Session[SessionConsts.CPdpList];
      var found = list.Where(w => w.Code == args.Value && w.Id != CurrentPdpId);
      args.IsValid = found == null || found.Count() == 0;
    }

    /// <summary>
    /// The validate pdp.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private bool ValidatePdp()
    {
      rfShortName.Validate();
      rfFullName.Validate();
      rfCode.Validate();
      vEmail.Validate();
      vCode.Validate();
      return rfShortName.IsValid && rfFullName.IsValid && rfCode.IsValid && vEmail.IsValid && vCode.IsValid;
    }

    // сохранение пдп
    /// <summary>
    /// The btn save_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (CurrentPdpId == null)
      {
        return;
      }

      if (!ValidatePdp())
      {
        return;
      }

      var addNew = (bool)ViewState["pdpIsNew"];
      var pdp = new Organisation();
      if (!addNew)
      {
        // нам нужен объект который не будет привязан к сессии, так как при endrequest session.Flush() в Global.asax изменения попадают в базу
        // все остальные поля присвоятся в AssignWorkstationValuesFromControls
        pdp.Id = (Guid)CurrentPdpId;
      }

      AssignPdpValuesFromControls(pdp, addNew);

      // для всех рабочих станций пдп проставляем новый pdp_id(существующий или сгенерированный в зависимости от операции)
      // и сохраняем в сессию
      workstationDetailControl.SetPdpIdForWorkstations((Guid)CurrentPdpId, pdp.Id);

      var list = (IList<PdpGridRow>)Session[SessionConsts.CPdpList];

      // добавляем новый элемент
      if (addNew && (Guid)CurrentPdpId == Guid.Empty)
      {
        var addedRow = new PdpGridRow(pdp);
        addedRow.IsNew = true;
        list.Add(addedRow);
      }
      else
      {
        var editedRow = list.Where(r => r.Id == (Guid)CurrentPdpId).FirstOrDefault();
        editedRow.AssignFrom(pdp);
      }

      Session[SessionConsts.CPdpList] = list;

      if (BindParentList != null)
      {
        BindParentList(list);
      }

      // если было добавление то очищаем контролы
      if (addNew)
      {
        ClearControlValues();
      }

      SetEnableControls(false);
      SetPdpViewStateValues(null, null);

      if (EnabledPdpGrid != null)
      {
        EnabledPdpGrid(true);
      }
    }

    // отмена сохранения пдп
    /// <summary>
    /// The btn cancel_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      // удаляем из сессии список рабочих станций, которые для текущего пдп
      var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
      dict.Remove((Guid)CurrentPdpId);
      Session[SessionConsts.CWorkstationDict] = dict;

      SetPdpViewStateValues(null, null);
      ClearControlValues();
      SetEnableControls(false);
      if (EnabledPdpGrid != null)
      {
        EnabledPdpGrid(true);
      }
    }

    /// <summary>
    /// The assign pdp values from controls.
    /// </summary>
    /// <param name="pdp">
    /// The pdp.
    /// </param>
    /// <param name="addNew">
    /// The add new.
    /// </param>
    public void AssignPdpValuesFromControls(Organisation pdp, bool addNew)
    {
      if (CurrentPdpId != null &&  CurrentPdpId != Guid.Empty)
      {
        pdp.Id = (Guid)CurrentPdpId;
      }
      else if (addNew)
      {
        pdp.Id = Guid.NewGuid();
      }

      pdp.Oid = new Oid { Id = Oid.Pvp };
      pdp.FullName = tbFullName.Text;
      pdp.ShortName = tbShortName.Text;
      if (pdp.Parent == null)
      {
        pdp.Parent = new Organisation();
      }

      pdp.Code = tbCode.Text;
      pdp.FirstName = tbFirstName.Text;
      pdp.LastName = tbLastName.Text;
      pdp.MiddleName = tbMiddleName.Text;
      pdp.Phone = tbPhone.Text;
      pdp.Fax = tbFax.Text;
      pdp.EMail = tbEmail.Text;
      pdp.IsActive = true;
    }

    #endregion

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

    protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "DoubleClick":
          gridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
          gridView_SelectedIndexChanged(null, null);
          OpenWorkstation();
          break;
      }
    }
  }

  #region Grid Rows classes

  /// <summary>
  ///   Класс для ряда грида с рабочей станцией
  /// </summary>
  public class WorkstationGridRow : Workstation
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkstationGridRow"/> class.
    /// </summary>
    /// <param name="w">
    /// The w.
    /// </param>
    public WorkstationGridRow(Workstation w)
    {
      AssignFrom(w);
    }

    /// <summary>
    /// Gets or sets a value indicating whether is new.
    /// </summary>
    public bool IsNew { get; set; }

    /// <summary>
    /// Gets the workstation.
    /// </summary>
    public Workstation Workstation
    {
      get
      {
        var result = new Workstation();
        result.Id = Id;
        result.Name = Name;
        result.PointDistributionPolicy = PointDistributionPolicy;
        result.SertificateUecs = SertificateUecs;
        result.UecCerticateType = UecCerticateType;
        result.UecReaderName = UecReaderName;
        result.SmardCardReaderName = SmardCardReaderName;
        return result;
      }
    }

    /// <summary>
    /// The assign from.
    /// </summary>
    /// <param name="w">
    /// The w.
    /// </param>
    public void AssignFrom(Workstation w)
    {
      Id = w.Id;
      Name = w.Name;
      PointDistributionPolicy = w.PointDistributionPolicy;
      if (SertificateUecs != null)
      {
        SertificateUecs.Clear();
      }
      else
      {
        SertificateUecs = new List<SertificateUec>();
      }

      foreach (var sert in w.SertificateUecs)
      {
        var s = new SertificateUec();
        s.Id = sert.Id;
        s.IsActive = sert.IsActive;
        s.Key = sert.Key;
        s.Type = sert.Type;
        s.Version = sert.Version;
        s.Workstation = new Workstation();
        s.Workstation.Id = w.Id;
        s.InstallDate = sert.InstallDate;
        SertificateUecs.Add(s);
      }

      UecCerticateType = w.UecCerticateType;
      UecReaderName = w.UecReaderName;
      SmardCardReaderName = w.SmardCardReaderName;
    }
  }

  /// <summary>
  ///   Класс для ряда грида с пдп
  /// </summary>
  public class PdpGridRow : Organisation
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PdpGridRow"/> class.
    /// </summary>
    /// <param name="pdp">
    /// The pdp.
    /// </param>
    public PdpGridRow(Organisation pdp)
    {
      AssignFrom(pdp);
    }

    /// <summary>
    /// Gets or sets a value indicating whether is new.
    /// </summary>
    public bool IsNew { get; set; }

    /// <summary>
    /// Gets the pdp.
    /// </summary>
    public Organisation Pdp
    {
      get
      {
        var result = new Organisation();
        result.Id = Id;
        result.IsActive = IsActive;
        result.IsOnLine = true;
        result.LastName = LastName;
        result.FirstName = FirstName;
        result.MiddleName = MiddleName;
        result.Phone = Phone;
        result.Fax = Fax;
        result.EMail = EMail;
        result.ShortName = ShortName;
        result.FullName = FullName;
        result.Code = Code;
        result.Parent = Parent;
        result.Workstations = Workstations;
        result.Oid = Oid;
        return result;
      }
    }

    /// <summary>
    /// The assign from.
    /// </summary>
    /// <param name="pdp">
    /// The pdp.
    /// </param>
    public void AssignFrom(Organisation pdp)
    {
      Id = pdp.Id;
      IsActive = pdp.IsActive;
      LastName = pdp.LastName;
      FirstName = pdp.FirstName;
      MiddleName = pdp.MiddleName;
      Phone = pdp.Phone;
      Fax = pdp.Fax;
      EMail = pdp.EMail;
      ShortName = pdp.ShortName;
      FullName = pdp.FullName;
      Code = pdp.Code;
      Parent = pdp.Parent;
      Oid = pdp.Oid;
    }
  }

  #endregion
}