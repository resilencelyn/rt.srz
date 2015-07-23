// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KLADRIntellisenseUserControl.ascx.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  using System;
  using System.Configuration;
  using System.Globalization;
  using System.Web.UI;

  using rt.core.model.interfaces;
  using rt.srz.business.configuration;
  using rt.srz.business.extensions;

  using StructureMap;

  /// <summary>
  ///   The kladr intellisense user control.
  /// </summary>
  public partial class KladrIntellisenseUserControl : UserControl, IKLADRUserControl
  {
    #region Fields

    /// <summary>
    ///   The kladr service.
    /// </summary>
    private IAddressService addressService;

    /// <summary>
    ///   The _security service.
    /// </summary>
    private ISecurityService securityService;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the selected kladr id.
    /// </summary>
    public Guid SelectedKLADRId
    {
      get
      {
        if (!string.IsNullOrEmpty(hfKladrHierarchy.Value))
        {
          var hierarchy = hfKladrHierarchy.Value.Split(new[] { ';' });
          if (hierarchy.Length > 0)
          {
            return new Guid(hierarchy[hierarchy.Length - 1]);
          }
        }

        return Guid.Empty;
      }

      set
      {
        if (value != Guid.Empty)
        {
          // Восстановление иерархии
          tbKLADRIntellisense.Text = addressService.GetUnstructureAddress(value);
          hfKladrHierarchy.Value = addressService.HierarchyBuild(value);
        }
      }
    }

    #endregion

    #region Properties

    /// <summary>
    ///   The unique key.
    /// </summary>
    protected string UniqueKey { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The enable.
    /// </summary>
    /// <param name="enable">
    /// The b enable.
    /// </param>
    public void Enable(bool enable)
    {
      tbPostcode.Enabled = enable;
      tbKLADRIntellisense.Enabled = enable;
      tbHouse.Enabled = enable;
      tbHousing.Enabled = enable;
      tbRoom.Enabled = enable;
    }

    /// <summary>
    ///   The set default subject.
    /// </summary>
    public void SetDefaultSubject()
    {
      // Получение текущего региона для текущего пользователя
      var currentUser = securityService.GetCurrentUser();
      if (currentUser.HasTf())
      {
        var tfom = currentUser.GetTf();
        var okato = string.Format("{0}000000", tfom.Okato.Trim());
        var kladr = addressService.GetFirstLevelByTfoms(okato);
        if (kladr != null)
        {
          // Установка региона по умолчанию
          SelectedKLADRId = kladr.Id;

          // Инициализация AutoComplete компонента
          aceKLADRIntellisense.ContextKey = SelectedKLADRId.ToString();

          // Установка идекса по умолчанию
          SetPostcode();
        }
      }
    }

    /// <summary>
    ///   The set focus first control.
    /// </summary>
    public void SetFocusFirstControl()
    {
      tbKLADRIntellisense.Focus();
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
      addressService = ObjectFactory.GetInstance<IAddressService>();
      securityService = ObjectFactory.GetInstance<ISecurityService>();
      if (!IsPostBack)
      {
        SetDefaultSubject();
      }
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
      // Решение вопроса размещения нескольких компонентов на одной форме
      UniqueKey = Guid.NewGuid().ToString("N");
      aceKLADRIntellisense.OnClientItemSelected = "KLADR_itemSelected_" + UniqueKey;
      aceKLADRIntellisense.OnClientPopulating = "KLADR_ListPopulating_" + UniqueKey;
      aceKLADRIntellisense.OnClientPopulated = "KLADR_ListPopulated_" + UniqueKey;
      aceKLADRIntellisense.OnClientHidden = "KLADR_ListPopulated_" + UniqueKey;
      aceKLADRIntellisense.OnClientShowing = "KLADR_ListShowing_" + UniqueKey;
      tbKLADRIntellisense.Attributes["onkeypress"] = "KLADR_KeyPress_" + UniqueKey + "();";
      tbKLADRIntellisense.Attributes["onkeydown"] = "KLADR_KeyDown_" + UniqueKey + "();";
      tbKLADRIntellisense.Attributes["onfocus"] = "KLADR_DisableSelection_" + UniqueKey + "();";
      tbKLADRIntellisense.Attributes["onmouseup"] = "KLADR_MouseUp_" + UniqueKey + "();";
      tbKLADRIntellisense.Attributes["onfocus"] = "KLADR_OnFocus_" + UniqueKey + "();";

      aceKLADRIntellisense.MinimumPrefixLength =
        int.Parse(ConfigurationManager.AppSettings["MinimumPrefixLengthForAdress"]);
    }

    /// <summary>
    ///   The set postcode.
    /// </summary>
    private void SetPostcode()
    {
      tbPostcode.Text = string.Empty;
      var kladr = addressService.GetAddress(SelectedKLADRId);
      if (kladr != null && kladr.Index != null)
      {
        tbPostcode.Text = kladr.Index.ToString();
      }
    }

    #endregion
  }
}