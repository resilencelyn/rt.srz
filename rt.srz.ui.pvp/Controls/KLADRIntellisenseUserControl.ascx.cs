// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KLADRIntellisenseUserControl.ascx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  using System;
  using System.Configuration;
  using System.Text;
  using System.Web.UI;

  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

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
    private IKladrService kladrService;

    /// <summary>
    /// The organisation service.
    /// </summary>
    private IRegulatoryService regulatoryService;

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
        if (!string.IsNullOrEmpty(hfKLADRHierarchy.Value))
        {
          var hierarchy = hfKLADRHierarchy.Value.Split(new[] { ';' });
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
          var service = ObjectFactory.GetInstance<IKladrService>();

          var valueBuilder = new StringBuilder();
          var hierarchyBuilder = new StringBuilder();
          var kladr = service.GetKladr(value);
          do
          {
            valueBuilder.Insert(0, string.Format("," + kladr.Name + " " + kladr.Socr + "."));
            hierarchyBuilder.Insert(0, string.Format(";" + kladr.Id));
            kladr = kladr.KLADRPARENT;
          }
          while (kladr != null);

          if (valueBuilder.Length > 0)
          {
            valueBuilder.Remove(0, 1);
          }

          valueBuilder.Append(",");

          if (hierarchyBuilder.Length > 0)
          {
            hierarchyBuilder.Remove(0, 1);
          }

          tbKLADRIntellisense.Text = valueBuilder.ToString();
          hfKLADRHierarchy.Value = hierarchyBuilder.ToString();
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
        var kladr = regulatoryService.GetFirstLevelByTfoms(tfom);
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
      kladrService = ObjectFactory.GetInstance<IKladrService>();
      securityService = ObjectFactory.GetInstance<ISecurityService>();
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
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
      var kladr = kladrService.GetKladr(SelectedKLADRId);
      if (kladr != null && kladr.Index != null)
      {
        tbPostcode.Text = kladr.Index.ToString();
      }
    }

    #endregion
  }
}