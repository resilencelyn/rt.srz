// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkstationDetailControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using rt.srz.model.interfaces.service;
using rt.srz.model.srz;

using StructureMap;
using rt.srz.ui.pvp.Enumerations;

#endregion

namespace rt.srz.ui.pvp.Controls.Administration
{
  using rt.uec.model.enumerations;

  /// <summary>
  /// The workstation detail control.
  /// </summary>
  public partial class WorkstationDetailControl : UserControl
  {
    /// <summary>
    /// The statement service.
    /// </summary>
    private IRegulatoryService regulatoryService;


    /// <summary>
    /// The bind parent list.
    /// </summary>
    public event Action<IList<WorkstationGridRow>> BindParentList;
    public event Action<bool> EnabledGrid;

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
      regulatoryService = ObjectFactory.GetInstance<IStatementService>();
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
      if (!IsPostBack)
      {
        Session[SessionConsts.CWorkstationDict] = new Dictionary<Guid, IList<WorkstationGridRow>>();
      }

      var textBox = comboReaderName.FindControl("comboReaderName_TextBox") as TextBox;
      if (textBox != null)
      {
        textBox.Attributes.Add("onBlur", "ClientChangeReader(this);");
      }

      textBox = comboReaderSmcName.FindControl("comboReaderSmcName_TextBox") as TextBox;
      if (textBox != null)
      {
        textBox.Attributes.Add("onBlur", "ClientChangeSmcReader(this);");
      }
    }

    /// <summary>
    /// The set workstation detail caption.
    /// </summary>
    /// <param name="caption">
    /// The caption.
    /// </param>
    private void SetWorkstationDetailCaption(string caption)
    {
      lbDetailCaption.Text = caption;
    }

    /// <summary>
    /// The set workstation view state values.
    /// </summary>
    /// <param name="pdpId">
    ///   The pdp id.
    /// </param>
    /// <param name="isNew">
    ///   The is new.
    /// </param>
    private void SetWorkstationViewStateValues(Guid? pdpId, bool? isNew)
    {
      ViewState["workstationId"] = pdpId;
      ViewState["workstationIsNew"] = isNew;
    }

    /// <summary>
    /// The add workstation.
    /// </summary>
    public void AddWorkstation()
    {
      if (EnabledGrid != null)
      {
        EnabledGrid(false);
      }
      // присваиваем значение что было добавление. потом это значение используется для выставления имени компа по умолчанию и сбрасывается в java script
      wasAdded.Value = "add";
      SetWorkstationDetailCaption("Добавление рабочего места");
      SetWorkstationViewStateValues(Guid.Empty, true);
      SetEnableControls(true);
      ClearControlValues();
      tbName.Focus();
    }

    /// <summary>
    /// The open workstation.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public void OpenWorkstation(WorkstationGridRow row)
    {
      if (EnabledGrid != null)
      {
        EnabledGrid(false);
      }
      SetWorkstationDetailCaption("Редактирование рабочего места");
      SetWorkstationViewStateValues(row.Id, row.IsNew);
      AssignControlValuesFromWorkstation(row);
      SetEnableControls(true);
      tbName.Focus();
    }

    /// <summary>
    /// The delete workstation.
    /// </summary>
    /// <param name="row">
    ///   The row.
    /// </param>
    /// <param name="gridView">
    ///   The grid view.
    /// </param>
    /// <param name="currentPdpId">
    ///   The current pdp id.
    /// </param>
    public void DeleteWorkstation(WorkstationGridRow row, GridView gridView, Guid? currentPdpId)
    {
      if (currentPdpId == null)
      {
        return;
      }

      var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
      var list = dict[(Guid)currentPdpId];
      list.Remove(row);
      Session[SessionConsts.CWorkstationDict] = dict;
      gridView.SelectedIndex = -1;
      gridView.DataSource = list;
      gridView.DataBind();
      ClearControlValues();
    }

    /// <summary>
    /// Смена выбранного рабочего места в гриде
    /// </summary>
    /// <param name="gridView">
    /// </param>
    /// <param name="currentPdpId">
    ///   The current Pdp Id.
    /// </param>
    public void ChangeWorkstation(GridView gridView, Guid? currentPdpId)
    {
      SetWorkstationViewStateValues(null, null);
      SetEnableControls(false);
      if (gridView.SelectedIndex < 0)
      {
        ClearControlValues();
      }
      else
      {
        if (currentPdpId == null)
        {
          return;
        }

        var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
        AssignControlValuesFromWorkstation(dict[(Guid)currentPdpId].FirstOrDefault(r => r.Id == (Guid)gridView.SelectedDataKey.Value));
      }
    }

    /// <summary>
    /// The validate workstation.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private bool ValidateWorkstation()
    {
      rfName.Validate();
      return rfName.IsValid;
    }

    // сохранение рабочего места
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
      if (ViewState["workstationId"] == null)
      {
        return;
      }

      if (!ValidateWorkstation())
      {
        return;
      }

      var addNew = (bool)ViewState["workstationIsNew"];
      var w = new Workstation();
      if (!addNew)
      {
        // нам нужен объект который не будет привязан к сессии, так как при endrequest session.Flush() в Global.asax изменения попадают в базу
        // все остальные поля присвоятся в AssignWorkstationValuesFromControls
        w.Id = (Guid)ViewState["workstationId"];
      }

      var currentPdpId = (Guid)Session[SessionConsts.CPdpId];
      AssignWorkstationValuesFromControls(w, addNew, currentPdpId);

      var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
      IList<WorkstationGridRow> list;
      if (!dict.ContainsKey(currentPdpId))
      {
        list = new List<WorkstationGridRow>();
        dict.Add(currentPdpId, list);
      }
      else
      {
        list = dict[currentPdpId];
      }

      // добавляем новый элемент
      if (addNew && (Guid)ViewState["workstationId"] == Guid.Empty)
      {
        var addedRow = new WorkstationGridRow(w);
        addedRow.IsNew = true;
        list.Add(addedRow);
      }
      else
      {
        var editedRow = list.FirstOrDefault(r => r.Id == (Guid)ViewState["workstationId"]);
        editedRow.AssignFrom(w);
      }

      dict[currentPdpId] = list;

      Session[SessionConsts.CWorkstationDict] = dict;

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
      ClearSessionValues();
      SetWorkstationViewStateValues(null, null);
      if (EnabledGrid != null)
      {
        EnabledGrid(true);
      }
    }

    /// <summary>
    /// отмена сохранения рабочего места
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      SetWorkstationViewStateValues(null, null);
      ClearControlValues();
      SetEnableControls(false);
      if (EnabledGrid != null)
      {
        EnabledGrid(true);
      }
    }

    /// <summary>
    /// для всех рабочих станций пдп(operationPdpId) проставляем новый pdp_id(существующий или сгенерированный в зависимости от операции) и сохраняем в сессию
    /// </summary>
    /// <param name="operationPdpId">
    /// </param>
    /// <param name="newPdpId">
    /// </param>
    public void SetPdpIdForWorkstations(Guid operationPdpId, Guid newPdpId)
    {
      var dict = (Dictionary<Guid, IList<WorkstationGridRow>>)Session[SessionConsts.CWorkstationDict];
      if (!dict.ContainsKey(operationPdpId))
      {
        return;
      }

      var list = dict[operationPdpId];
      foreach (var item in list)
      {
        if (item.PointDistributionPolicy.Id != newPdpId)
        {
          item.PointDistributionPolicy.Id = newPdpId;
        }
      }

      dict.Remove(operationPdpId);
      dict.Add(newPdpId, list);
      Session[SessionConsts.CWorkstationDict] = dict;
    }

    /// <summary>
    /// The assign workstation values from controls.
    /// </summary>
    /// <param name="w">
    ///   The w.
    /// </param>
    /// <param name="addNew">
    ///   The add new.
    /// </param>
    /// <param name="pdpId">
    ///   The pdp id.
    /// </param>
    public void AssignWorkstationValuesFromControls(Workstation w, bool addNew, Guid pdpId)
    {
      if ((Guid)ViewState["workstationId"] != Guid.Empty)
      {
        w.Id = (Guid)ViewState["workstationId"];
      }
      else if (addNew)
      {
        w.Id = Guid.NewGuid();
      }

      w.Name = tbName.Text;
      w.UecReaderName = readerName.Value;
      w.SmardCardReaderName = readerSmcName.Value;
      w.UecCerticateType = rblCrypto.SelectedValue == "GOST"
                                                   ? (byte)CryptographyType.GOST
                                                   : (byte)CryptographyType.RSA;

      if (w.PointDistributionPolicy == null)
      {
        w.PointDistributionPolicy = new Organisation();
      }

      w.PointDistributionPolicy.Id = pdpId;


      if (w.SertificateUecs == null)
      {
        w.SertificateUecs = new List<SertificateUec>();
      }
      else
      {
        w.SertificateUecs.Clear();
      }

      AddCertificate(w, SessionConsts.CTerminalGostContent, TypeSertificate.TerminalGOST);
      AddCertificate(w, SessionConsts.CTerminalRsaContent, TypeSertificate.TerminalRSA);
      AddCertificate(w, SessionConsts.CPrivateTerminalGostContent, TypeSertificate.PrivateTerminalGOST);
    }

    /// <summary>
    /// The add certificate.
    /// </summary>
    /// <param name="w">
    /// The w.
    /// </param>
    /// <param name="sessionValueName">
    /// The session value name.
    /// </param>
    /// <param name="certificateType">
    /// The certificate type.
    /// </param>
    private void AddCertificate(Workstation w, string sessionValueName, TypeSertificate certificateType)
    {
      if (Session[sessionValueName] != null)
      {
        var sert = new SertificateUec();
        sert.Key = (byte[])Session[sessionValueName];
        sert.Type = regulatoryService.GetConcept((int)certificateType);
        sert.IsActive = true;
        sert.Version = 1;
        w.SertificateUecs.Add(sert);
      }
    }

    /// <summary>
    /// The assign control values from workstation.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public void AssignControlValuesFromWorkstation(WorkstationGridRow row)
    {
      tbName.Text = row.Name;
      readerName.Value = row.UecReaderName;
      readerSmcName.Value = row.SmardCardReaderName;
      //comboReaderName.Text = row.UecReaderName;
      if (row.UecCerticateType == null || row.UecCerticateType == (int)CryptographyType.GOST)
      {
        rblCrypto.SelectedValue = "GOST";
      }
      else
      {
        rblCrypto.SelectedValue = "RSA";
      }
      uTerminalGOST.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.TerminalGOST, row.SertificateUecs);
      uTerminalRSA.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.TerminalRSA, row.SertificateUecs);
      uPrivateTerminalGOST.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.PrivateTerminalGOST, row.SertificateUecs);
    }

    /// <summary>
    /// The set enable controls.
    /// </summary>
    /// <param name="enable">
    /// The enable.
    /// </param>
    public void SetEnableControls(bool enable)
    {
      rfName.Enabled = enable;
      tbName.Enabled = enable;
      comboReaderName.Enabled = enable;
      comboReaderSmcName.Enabled = enable;
      rblCrypto.Enabled = enable;
      btnCancel.Enabled = enable;
      btnSave.Enabled = enable;

      //uTerminalGOST.Enabled = enable;
      //uTerminalRSA.Enabled = enable;
      //uPrivateTerminalGOST.Enabled = enable;
    }

    /// <summary>
    /// The clear control values.
    /// </summary>
    public void ClearControlValues()
    {
      tbName.Text = string.Empty;
      readerName.Value = string.Empty;
      readerSmcName.Value = string.Empty;
      comboReaderName.Text = string.Empty;
      comboReaderSmcName.Text = string.Empty;
      rblCrypto.SelectedValue = "GOST";
      ClearSessionValues();
    }

    /// <summary>
    /// The clear session values.
    /// </summary>
    private void ClearSessionValues()
    {
      Session[SessionConsts.CTerminalGostContent] = null;
      Session[SessionConsts.CTerminalRsaContent] = null;
      Session[SessionConsts.CPrivateTerminalGostContent] = null;
    }

    #region Loadded certificates

    /// <summary>
    /// The u terminal gos t_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uTerminalGOST_UploadComplete(object sender, EventArgs e)
    {
      if (uTerminalGOST.HasFile)
      {
        Session["TerminalGOSTContent"] = uTerminalGOST.FileBytes;
      }
    }

    /// <summary>
    /// The u terminal rs a_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uTerminalRSA_UploadComplete(object sender, EventArgs e)
    {
      if (uTerminalRSA.HasFile)
      {
        Session["TerminalRSAContent"] = uTerminalRSA.FileBytes;
      }
    }

    /// <summary>
    /// The u private terminal gos t_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uPrivateTerminalGOST_UploadComplete(object sender, EventArgs e)
    {
      if (uPrivateTerminalGOST.HasFile)
      {
        Session["PrivateTerminalGOSTContent"] = uPrivateTerminalGOST.FileBytes;
      }
    }

    #endregion
  }
}