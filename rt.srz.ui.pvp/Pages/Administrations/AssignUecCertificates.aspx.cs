using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.services;
using rt.srz.ui.pvp.Enumerations;
using rt.uec.model.Interfaces;
using StructureMap;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  using rt.uec.model.enumerations;

  public partial class AssignUecCertificates : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        Session[SessionConsts.CUc1GostContent] = null;
        Session[SessionConsts.CUc1RsaContent] = null;
        Session[SessionConsts.COko1GostContent] = null;
        Session[SessionConsts.COko1RsaContent] = null;

        var tfService = ObjectFactory.GetInstance<ITFService>();
        var sertificates = tfService.GetGlobalSertificates();
        uUC1GOST.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.UC1GOST, sertificates);
        uUC1RSA.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.UC1RSA, sertificates);
        uOKO1GOST.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.OKO1GOST, sertificates);
        uOKO1RSA.LoadedEarlierText = UtilsHelper.GetLoadedSertificateText(TypeSertificate.OKO1RSA, sertificates);
      }
    }

    /// <summary>
    /// The u u c 1 gos t_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uUC1GOST_UploadComplete(object sender, EventArgs e)
    {
      if (uUC1GOST.HasFile)
      {
        Session[SessionConsts.CUc1GostContent] = uUC1GOST.FileBytes;
      }
    }

    /// <summary>
    /// The u u c 1 gos t_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uUC1RSA_UploadComplete(object sender, EventArgs e)
    {
      if (uUC1RSA.HasFile)
      {
        Session[SessionConsts.CUc1RsaContent] = uUC1RSA.FileBytes;
      }
    }

    /// <summary>
    /// The u u c 1 gos t_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uOKO1GOST_UploadComplete(object sender, EventArgs e)
    {
      if (uOKO1GOST.HasFile)
      {
        Session[SessionConsts.COko1GostContent] = uOKO1GOST.FileBytes;
      }
    }

    /// <summary>
    /// The u u c 1 gos t_ upload complete.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void uOKO1RSA_UploadComplete(object sender, EventArgs e)
    {
      if (uOKO1RSA.HasFile)
      {
        Session[SessionConsts.COko1RsaContent] = uOKO1RSA.FileBytes;
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      //  Сохранение сертификатов
      var uecService = ObjectFactory.GetInstance<IUecService>();
      if (Session[SessionConsts.CUc1GostContent] != null)
        uecService.SaveSmoSertificateKey(Guid.Empty, 1, (int)TypeSertificate.UC1GOST, (byte[])Session[SessionConsts.CUc1GostContent]);
      if (Session[SessionConsts.CUc1RsaContent] != null)
        uecService.SaveSmoSertificateKey(Guid.Empty, 1, (int)TypeSertificate.UC1RSA, (byte[])Session[SessionConsts.CUc1RsaContent]);
      if (Session[SessionConsts.COko1GostContent] != null)
        uecService.SaveSmoSertificateKey(Guid.Empty, 1, (int)TypeSertificate.OKO1GOST, (byte[])Session[SessionConsts.COko1GostContent]);
      if (Session[SessionConsts.COko1RsaContent] != null)
        uecService.SaveSmoSertificateKey(Guid.Empty, 1, (int)TypeSertificate.OKO1RSA, (byte[])Session[SessionConsts.COko1RsaContent]);

      RedirectUtils.RedirectToMain(Response);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      RedirectUtils.RedirectToMain(Response);
    }
  }
}