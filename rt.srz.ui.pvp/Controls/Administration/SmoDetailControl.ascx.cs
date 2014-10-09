// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmoDetailControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
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
  /// The smo detail control.
  /// </summary>
  public partial class SmoDetailControl : UserControl
  {
    /// <summary>
    /// The _smo.
    /// </summary>
    private Organisation _smo;

    private string _oid;

    /// <summary>
    /// The _smo service.
    /// </summary>
    private IRegulatoryService regulatoryService;

    /// <summary>
    /// The statement service.
    /// </summary>
    private IStatementService statementService;

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
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      statementService = ObjectFactory.GetInstance<IStatementService>();
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
      //дизаблим все контролы крорме сертификатов и кнопки включить/выключить
      tbShortName.Enabled = false;
      tbCode.Enabled = false;
      tbLastName.Enabled = false;
      tbFullName.Enabled = false;
      tbFirstName.Enabled = false;
      ddlFoms.Enabled = false;
      tbMiddleName.Enabled = false;
      tbPhone.Enabled = false;
      tbOgrn.Enabled = false;
      tbInn.Enabled = false;
      tbFax.Enabled = false;
      tbEmail.Enabled = false;
      tbAddress.Enabled = false;
      tbWebsite.Enabled = false;
      tbDateInclude.Enabled = false;
      tbDateExclude.Enabled = false;
      tbDateLicensing.Enabled = false;
      tbDateExpiryLicense.Enabled = false;
      tbLicense.Enabled = false;
      tbLicenseNumber.Enabled = false;


      if (Request.QueryString["SmoId"] == null)
      {
        _smo = new Organisation();
        _smo.IsActive = true;
      }
      else
      {
        _smo = regulatoryService.GetOrganisation(Guid.Parse(Request.QueryString["SmoId"]));
      }

      if (Request.QueryString["type"] != null && Request.QueryString["type"] == "mo")
      {
        _oid = Oid.Mo;
        //sertificateDiv.Visible = false;
        vCode.ErrorMessage = "МО с таким кодом уже существует!";
        rInn.Enabled = false;
        rOgrn.Enabled = false;
      }
      else
      {
        _oid = Oid.Smo;
        //sertificateDiv.Visible = true;
        rInn.Enabled = true;
        rOgrn.Enabled = true;
        vCode.ErrorMessage = "СМО с таким кодом уже существует!";
      }

      if (!IsPostBack)
      {
        ddlFoms.DataSource = regulatoryService.GetTfoms();
        ddlFoms.DataBind();
        SetTitle();
        AssignControlValuesFromSmo();
      }
    }

    private void SetTitle()
    {
      string prefix = "СМО";
      if (_oid == Oid.Mo)
      {
        prefix = "МО";
      }
      if (Request.QueryString["SmoId"] == null)
      {
        lbTitle.Text = string.Format("Добавление {0}", prefix);
      }
      else
      {
        lbTitle.Text = string.Format("Редактирование {0}", prefix);
      }
    }

    /// <summary>
    /// The assign control values from smo.
    /// </summary>
    private void AssignControlValuesFromSmo()
    {
      tbFullName.Text = _smo.FullName;
      tbShortName.Text = _smo.ShortName;
      ddlFoms.SelectedValue = _smo.Parent != null ? _smo.Parent.Id.ToString() : null;
      tbCode.Text = _smo.Code;
      tbInn.Text = _smo.Inn;
      tbOgrn.Text = _smo.Ogrn;
      tbFirstName.Text = _smo.FirstName;
      tbLastName.Text = _smo.LastName;
      tbMiddleName.Text = _smo.MiddleName;
      tbPhone.Text = _smo.Phone;
      tbFax.Text = _smo.Fax;
      tbEmail.Text = _smo.EMail;
      tbAddress.Text = _smo.Address;
      tbWebsite.Text = _smo.Website;
      tbDateInclude.Text = _smo.DateIncludeRegister != null
                             ? ((DateTime)_smo.DateIncludeRegister).ToShortDateString()
                             : string.Empty;
      tbDateExclude.Text = _smo.DateExcludeRegister != null
                             ? ((DateTime)_smo.DateExcludeRegister).ToShortDateString()
                             : string.Empty;
      tbDateLicensing.Text = _smo.DateLicensing != null
                               ? ((DateTime)_smo.DateLicensing).ToShortDateString()
                               : string.Empty;
      tbDateExpiryLicense.Text = _smo.DateExpiryLicense != null
                                   ? ((DateTime)_smo.DateExpiryLicense).ToShortDateString()
                                   : string.Empty;
      tbLicense.Text = _smo.LicenseData;
      tbLicenseNumber.Text = _smo.LicenseNumber;

      ViewState[UtilsHelper.c_IsOnline] = _smo.IsOnLine;
      UtilsHelper.SetTurnCaption(btnTurn, ViewState);
      //uUC1GOST.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.UC1GOST, _smo.SertificateUecs);
      //uUC1RSA.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.UC1RSA, _smo.SertificateUecs);
      //uOKO1GOST.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.OKO1GOST, _smo.SertificateUecs);
      //uOKO1RSA.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.OKO1RSA, _smo.SertificateUecs);
    }

    /// <summary>
    /// The assign smo values from controls.
    /// </summary>
    private void AssignSmoValuesFromControls()
    {
      _smo.FullName = tbFullName.Text;
      _smo.ShortName = tbShortName.Text;
      _smo.Parent = regulatoryService.GetOrganisation(Guid.Parse(ddlFoms.SelectedValue));
      _smo.Code = tbCode.Text;
      _smo.Inn = tbInn.Text;
      _smo.Ogrn = tbOgrn.Text;
      _smo.FirstName = tbFirstName.Text;
      _smo.LastName = tbLastName.Text;
      _smo.MiddleName = tbMiddleName.Text;
      _smo.Phone = tbPhone.Text;
      _smo.Fax = tbFax.Text;
      _smo.EMail = tbEmail.Text;
      _smo.Address = tbAddress.Text;
      _smo.Website = tbWebsite.Text;
      _smo.Oid = new Oid { Id = _oid };
      _smo.DateIncludeRegister = string.IsNullOrEmpty(tbDateInclude.Text)
                                   ? null
                                   : (DateTime?)DateTime.Parse(tbDateInclude.Text);
      _smo.DateExcludeRegister = string.IsNullOrEmpty(tbDateExclude.Text)
                                   ? null
                                   : (DateTime?)DateTime.Parse(tbDateExclude.Text);
      _smo.DateExpiryLicense = string.IsNullOrEmpty(tbDateExpiryLicense.Text)
                                 ? null
                                 : (DateTime?)DateTime.Parse(tbDateExpiryLicense.Text);
      _smo.DateLicensing = string.IsNullOrEmpty(tbDateLicensing.Text)
                             ? null
                             : (DateTime?)DateTime.Parse(tbDateLicensing.Text);
      _smo.LicenseData = tbLicense.Text;
      _smo.LicenseNumber = tbLicenseNumber.Text;
      _smo.IsOnLine = (bool)ViewState[UtilsHelper.c_IsOnline];

      AddCertificate(_smo, SessionConsts.CUc1GostContent, TypeSertificate.UC1GOST);
      AddCertificate(_smo, SessionConsts.CUc1RsaContent, TypeSertificate.UC1RSA);
      AddCertificate(_smo, SessionConsts.COko1GostContent, TypeSertificate.OKO1GOST);
      AddCertificate(_smo, SessionConsts.COko1RsaContent, TypeSertificate.OKO1RSA);
    }

    /// <summary>
    /// The add certificate.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="sessionValueName">
    /// The session value name. 
    /// </param>
    /// <param name="certificateType">
    /// The certificate type. 
    /// </param>
    private void AddCertificate(Organisation s, string sessionValueName, TypeSertificate certificateType)
    {
      if (Session[sessionValueName] != null)
      {
        var sert = new SertificateUec();
        sert.Key = (byte[])Session[sessionValueName];
        sert.Type = regulatoryService.GetConcept((int)certificateType);
        sert.IsActive = true;
        sert.Version = 1;
        if (s.SertificateUecs == null)
        {
          s.SertificateUecs = new List<SertificateUec>();
        }

        s.SertificateUecs.Add(sert);
      }
    }


    /// <summary>
    /// The save changes.
    /// </summary>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    public Guid SaveChanges()
    {
      AssignSmoValuesFromControls();
      ClearSessionValues();
      return regulatoryService.SaveSmo(_smo);
    }

    /// <summary>
    /// The clear session values.
    /// </summary>
    private void ClearSessionValues()
    {
      Session[SessionConsts.CUc1GostContent] = null;
      Session[SessionConsts.CUc1RsaContent] = null;
      Session[SessionConsts.COko1GostContent] = null;
      Session[SessionConsts.COko1RsaContent] = null;
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
      args.IsValid = !regulatoryService.SmoCodeExists(_smo.Id, args.Value);
    }

    #region Upload sertificates

    ///// <summary>
    ///// The u u c 1 gos t_ upload complete.
    ///// </summary>
    ///// <param name="sender">
    ///// The sender.
    ///// </param>
    ///// <param name="e">
    ///// The e.
    ///// </param>
    //protected void uUC1GOST_UploadComplete(object sender, EventArgs e)
    //{
    //  if (uUC1GOST.HasFile)
    //  {
    //    Session[SessionConsts.CUc1GostContent] = uUC1GOST.FileBytes;
    //  }
    //}

    ///// <summary>
    ///// The u u c 1 rs a_ upload complete.
    ///// </summary>
    ///// <param name="sender">
    ///// The sender.
    ///// </param>
    ///// <param name="e">
    ///// The e.
    ///// </param>
    //protected void uUC1RSA_UploadComplete(object sender, EventArgs e)
    //{
    //  if (uUC1RSA.HasFile)
    //  {
    //    Session[SessionConsts.CUc1RsaContent] = uUC1RSA.FileBytes;
    //  }
    //}

    ///// <summary>
    ///// The u ok o 1 gos t_ upload complete.
    ///// </summary>
    ///// <param name="sender">
    ///// The sender.
    ///// </param>
    ///// <param name="e">
    ///// The e.
    ///// </param>
    //protected void uOKO1GOST_UploadComplete(object sender, EventArgs e)
    //{
    //  if (uOKO1GOST.HasFile)
    //  {
    //    Session[SessionConsts.COko1GostContent] = uOKO1GOST.FileBytes;
    //  }
    //}

    ///// <summary>
    ///// The u ok o 1 rs a_ upload complete.
    ///// </summary>
    ///// <param name="sender">
    ///// The sender.
    ///// </param>
    ///// <param name="e">
    ///// The e.
    ///// </param>
    //protected void uOKO1RSA_UploadComplete(object sender, EventArgs e)
    //{
    //  if (uOKO1RSA.HasFile)
    //  {
    //    Session[SessionConsts.COko1RsaContent] = uOKO1RSA.FileBytes;
    //  }
    //}

    #endregion

    protected void btnTurn_Click(object sender, EventArgs e)
    {
      UtilsHelper.ChangeTurn(ViewState);
      UtilsHelper.SetTurnCaption(btnTurn, ViewState);
    }

    private void SetTurnCaption()
    {
      UtilsHelper.SetTurnCaption(btnTurn, ViewState);
    }

  }
}