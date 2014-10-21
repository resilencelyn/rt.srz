// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrUserControl.ascx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  using System;
  using System.Collections.Generic;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.interfaces;
  using rt.srz.business.configuration;
  using rt.srz.business.extensions;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The kladr user control mode.
  /// </summary>
  public enum KladrUserControlMode
  {
    /// <summary>
    ///   The database.
    /// </summary>
    Database, // Все данные загружаются из справочника в БД

    /// <summary>
    ///   The free.
    /// </summary>
    Free // Данные для населенного пунута и улицы можно ввести вручную
  }

  /// <summary>
  ///   The kladr user control.
  /// </summary>
  public partial class KladrUserControl : UserControl, IKLADRUserControl
  {
    #region Constants

    /// <summary>
    ///   The moscow okato.
    /// </summary>
    private const string MoscowOkato = "45000000000";

    /// <summary>
    ///   The st petersburg okato.
    /// </summary>
    private const string StPetersburgOkato = "40000000000";

    #endregion

    #region Fields

    /// <summary>
    ///   The _kladr service.
    /// </summary>
    private IAddressService addressService;

    /// <summary>
    ///   The _security service.
    /// </summary>
    private ISecurityService securityService;

    /// <summary>
    /// The organisation service.
    /// </summary>
    private IRegulatoryService regulatoryService;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Выбранный район
    /// </summary>
    public string Area
    {
      get
      {
        if (ddlArea.SelectedIndex != 0)
        {
          return ddlArea.SelectedItem.Text;
        }

        return null;
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          return;
        }

        foreach (ListItem item in ddlArea.Items)
        {
          if (item.Text == value)
          {
            ddlArea.SelectedValue = item.Value;
            break;
          }
        }

        DdlAreaSelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    ///   Выбранный город
    /// </summary>
    public string City
    {
      get
      {
        if (ddlCity.SelectedIndex != 0)
        {
          return ddlCity.SelectedItem.Text;
        }

        return null;
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          return;
        }

        foreach (ListItem item in ddlCity.Items)
        {
          if (item.Text == value)
          {
            ddlCity.SelectedValue = item.Value;
            break;
          }
        }

        DdlCitySelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    ///   Gets or sets the mode.
    /// </summary>
    public KladrUserControlMode Mode
    {
      get
      {
        if (ViewState["Mode"] != null)
        {
          return (KladrUserControlMode)ViewState["Mode"];
        }

        // TODO
        // что возвращать если "Mode" нет во ViewState?
        return KladrUserControlMode.Database;
      }

      set
      {
        switch (value)
        {
          case KladrUserControlMode.Database:
            SetDatabaseMode();
            break;
          case KladrUserControlMode.Free:
            SetFreeMode();
            break;
        }

        ViewState["Mode"] = value;
        UpdatePanelKLADR.Update();
      }
    }

    /// <summary>
    ///   Gets or sets the selected kladr id.
    /// </summary>
    public Guid SelectedKLADRId
    {
      get
      {
        // if (Mode == KLADRUserControlMode.Database)
        // {
        if (ddlStreet.SelectedIndex != 0)
        {
          return new Guid(ddlStreet.SelectedValue);
        }

        if (ddlTown.SelectedIndex != 0)
        {
          return new Guid(ddlTown.SelectedValue);
        }

        if (ddlCity.SelectedIndex != 0)
        {
          return new Guid(ddlCity.SelectedValue);
        }

        if (ddlArea.SelectedIndex != 0)
        {
          return new Guid(ddlArea.SelectedValue);
        }

        if (ddlSubject.SelectedIndex != 0)
        {
          return new Guid(ddlSubject.SelectedValue);
        }

        // }
        return Guid.Empty;
      }

      set
      {
      }
    }

    /// <summary>
    ///   Выбранная улица
    /// </summary>
    public string Street
    {
      get
      {
        switch (Mode)
        {
          case KladrUserControlMode.Database:
          {
            if (ddlStreet.SelectedIndex != 0)
            {
              return ddlStreet.SelectedItem.Text;
            }

            return null;
          }

          case KladrUserControlMode.Free:
          {
            if (tbStreet.Text.Length != 0)
            {
              return tbStreet.Text;
            }

            return null;
          }

          default:
            return null;
        }
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          return;
        }

        if (Mode == KladrUserControlMode.Database)
        {
          foreach (ListItem item in ddlStreet.Items)
          {
            if (item.Text == value)
            {
              ddlStreet.SelectedValue = item.Value;
              break;
            }
          }

          DdlStreetSelectedIndexChanged(null, null);
        }
        else
        {
          tbStreet.Text = value;
        }
      }
    }

    /// <summary>
    ///   Выбранный регион
    /// </summary>
    public string Subject
    {
      get
      {
        if (ddlSubject.SelectedIndex != 0)
        {
          return ddlSubject.SelectedItem.Text;
        }

        return null;
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          return;
        }

        foreach (ListItem item in ddlSubject.Items)
        {
          if (item.Text == value)
          {
            ddlSubject.SelectedValue = item.Value;
            break;
          }
        }

        DdlSubjectSelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    ///   Выбранный регион
    /// </summary>
    public string SubjectId
    {
      get
      {
        if (ddlSubject.SelectedIndex != 0)
        {
          return ddlSubject.SelectedValue;
        }

        return null;
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          return;
        }

        ddlSubject.SelectedValue = value;
        DdlSubjectSelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    ///   Выбранный населенный пункт
    /// </summary>
    public string Town
    {
      get
      {
        switch (Mode)
        {
          case KladrUserControlMode.Database:
          {
            if (ddlTown.SelectedIndex != 0)
            {
              return ddlTown.SelectedItem.Text;
            }

            return null;
          }

          case KladrUserControlMode.Free:
          {
            if (tbTown.Text.Length != 0)
            {
              return tbTown.Text;
            }

            return null;
          }

          default:
            return null;
        }
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          return;
        }

        if (Mode == KladrUserControlMode.Database)
        {
          foreach (ListItem item in ddlTown.Items)
          {
            if (item.Text == value)
            {
              ddlTown.SelectedValue = item.Value;
              break;
            }
          }

          DdlTownSelectedIndexChanged(null, null);
        }
        else
        {
          tbTown.Text = value;
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
      ddlSubject.Enabled = enable;
      ddlArea.Enabled = enable;
      ddlCity.Enabled = enable;
      ddlTown.Enabled = enable;
      ddlStreet.Enabled = enable;
      tbStreet.Enabled = enable;
      tbTown.Enabled = enable;
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
          SubjectId = kladr.Id.ToString();

          // Установка индекса по умолчанию
          SetPostcode();
        }
      }
    }

    /// <summary>
    ///   The set focus first control.
    /// </summary>
    public void SetFocusFirstControl()
    {
      ddlSubject.Focus();
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
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();

      if (!IsPostBack)
      {
        // Режим по умолчанию - работа с базой данных
        Mode = KladrUserControlMode.Database;
        LoadSubjects();
        ClearAndShowAllControls();
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
      if (!IsPostBack)
      {
        UniqueKey = Guid.NewGuid().ToString("N");
      }
    }

    /// <summary>
    /// The chb is free address_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void ChbIsFreeAddressCheckedChanged(object sender, EventArgs e)
    {
      // if (chbIsFreeAddress.Checked)
      // Mode = KLADRUserControlMode.Free;
      // else
      // Mode = KLADRUserControlMode.Database;
    }

    /// <summary>
    /// The ddl area_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DdlAreaSelectedIndexChanged(object sender, EventArgs e)
    {
      ClearAndShowControl(ddlCity, lblCity, "город");
      if (Mode == KladrUserControlMode.Database)
      {
        ClearAndShowControl(ddlTown, lblTown, "населенный пункт");
        ClearAndShowControl(ddlStreet, lblStreet, "улицу");
      }
      else
      {
        ClearControl(ddlTown, "населенный пункт");
        ClearControl(ddlStreet, "улицу");
      }

      if (ddlArea.SelectedIndex != 0)
      {
        LoadCityByArea();
      }
      else
      {
        ClearAndShowAllControls();

        if (ddlSubject.SelectedIndex != 0)
        {
          LoadAreasByProvince();
        }
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    /// <summary>
    /// The ddl city_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DdlCitySelectedIndexChanged(object sender, EventArgs e)
    {
      if (Mode == KladrUserControlMode.Database)
      {
        ClearAndShowControl(ddlTown, lblTown, "населенный пункт");
        ClearAndShowControl(ddlStreet, lblStreet, "улицу");
      }
      else
      {
        ClearControl(ddlTown, "населенный пункт");
        ClearControl(ddlStreet, "улицу");
      }

      if (ddlCity.SelectedIndex != 0)
      {
        // lblArea.Visible = false;
        // ddlArea.Visible = false;
        LoadTownsByCity();
      }
      else
      {
        // lblArea.Visible = true;
        // ddlArea.Visible = true;
        if (ddlArea.SelectedIndex != 0)
        {
          LoadTownsByArea();
        }
        else
        {
          LoadStreetsByProvince();
        }
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    /// <summary>
    /// The ddl street_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DdlStreetSelectedIndexChanged(object sender, EventArgs e)
    {
      if (ddlStreet.SelectedIndex != 0)
      {
        if (ddlArea.SelectedIndex == 0)
        {
          lblArea.Visible = false;
          ddlArea.Visible = false;
        }

        if (ddlCity.SelectedIndex == 0)
        {
          lblCity.Visible = false;
          ddlCity.Visible = false;
        }

        if (ddlTown.SelectedIndex == 0)
        {
          lblTown.Visible = false;
          ddlTown.Visible = false;
        }
      }
      else
      {
        if (ddlArea.SelectedIndex != 0)
        {
          lblArea.Visible = true;
          ddlArea.Visible = true;
          lblTown.Visible = true;
          ddlTown.Visible = true;
        }
        else
        {
          lblCity.Visible = true;
          ddlCity.Visible = true;
        }
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    /// <summary>
    /// The ddl subject_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DdlSubjectSelectedIndexChanged(object sender, EventArgs e)
    {
      ClearAndShowAllControls();

      if (ddlSubject.SelectedIndex != 0)
      {
        LoadAreasByProvince();

        // Спец. Случай для Москвы и Питера
        // загружаем их же в поле "Населенный пункт"
        var obj = addressService.GetAddress(new Guid(ddlSubject.SelectedValue));
        if (obj != null && !string.IsNullOrEmpty(obj.Okato))
        {
          if (obj.Okato == MoscowOkato || obj.Okato == StPetersburgOkato)
          {
            ddlTown.Items.Add(new ListItem(obj.Name + " " + obj.Socr, obj.Id.ToString()));
            ddlTown.SelectedValue = obj.Id.ToString();
          }
        }
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    /// <summary>
    /// The ddl town_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DdlTownSelectedIndexChanged(object sender, EventArgs e)
    {
      if (Mode == KladrUserControlMode.Database)
      {
        ClearAndShowControl(ddlStreet, lblStreet, "улицу");
      }
      else
      {
        ClearControl(ddlStreet, "улицу");
      }

      if (ddlTown.SelectedIndex != 0)
      {
        LoadStreetsByTown();
      }
      else
      {
        LoadStreetsByCity();
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    /// <summary>
    ///   The clear and show all controls.
    /// </summary>
    private void ClearAndShowAllControls()
    {
      ClearAndShowControl(ddlArea, lblArea, "район");
      ClearAndShowControl(ddlCity, lblCity, "город");
      ClearAndShowControl(ddlTown, lblTown, "населенный пункт");
      ClearAndShowControl(ddlStreet, lblStreet, "улицу");
      if (Mode == KladrUserControlMode.Free)
      {
        ddlTown.Visible = false;
        ddlStreet.Visible = false;
      }
    }

    /// <summary>
    /// The clear and show control.
    /// </summary>
    /// <param name="dropDownList">
    /// The drop down list.
    /// </param>
    /// <param name="label">
    /// The label.
    /// </param>
    /// <param name="text">
    /// The text.
    /// </param>
    private void ClearAndShowControl(DropDownList dropDownList, Label label, string text)
    {
      dropDownList.Items.Clear();
      dropDownList.Items.Add("Выберите " + text);
      dropDownList.Visible = true;
      label.Visible = true;
    }

    /// <summary>
    /// The clear control.
    /// </summary>
    /// <param name="dropDownList">
    /// The drop down list.
    /// </param>
    /// <param name="text">
    /// The text.
    /// </param>
    private void ClearControl(DropDownList dropDownList, string text)
    {
      dropDownList.Items.Clear();
      dropDownList.Items.Add("Выберите " + text);
    }

    /// <summary>
    /// The fill ddl.
    /// </summary>
    /// <param name="addressObjects">
    ///   The address objects.
    /// </param>
    /// <param name="dropDownList">
    ///   The drop down list.
    /// </param>
    private void FillDdl(IEnumerable<IAddress> addressObjects, DropDownList dropDownList)
    {
      foreach (var addressObject in addressObjects)
      {
        dropDownList.Items.Add(new ListItem(addressObject.Name + " " + addressObject.Socr, addressObject.Id.ToString()));
      }
    }

    /// <summary>
    ///   Загрузка районов по региону (если у региона нет округов)
    /// </summary>
    private void LoadAreasByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlSubject.SelectedValue), null, KladrLevel.Area);
      if (addressObjects.Count == 0)
      {
        lblArea.Visible = false;
        ddlArea.Visible = false;
      }
      else
      {
        FillDdl(addressObjects, ddlArea);
      }

      LoadCitiesByProvince();
    }

    /// <summary>
    ///   Загрузка городов по региону (если у региона нет округов и районов)
    /// </summary>
    private void LoadCitiesByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlSubject.SelectedValue), null, KladrLevel.City);
      if (addressObjects.Count == 0)
      {
        lblCity.Visible = false;
        ddlCity.Visible = false;
      }
      else
      {
        FillDdl(addressObjects, ddlCity);
      }

      LoadTownsByProvince();
    }

    /// <summary>
    ///   Загрузка городов по району
    /// </summary>
    private void LoadCityByArea()
    {
      if (ddlArea.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlArea.SelectedValue), null, KladrLevel.City);
      if (addressObjects.Count == 0)
      {
        lblCity.Visible = false;
        ddlCity.Visible = false;
      }
      else
      {
        FillDdl(addressObjects, ddlCity);
      }

      LoadTownsByArea();
    }

    /// <summary>
    ///   Загрузка улицы по району (если у района нет городов, внутригородских районов и населенных пунктов)
    /// </summary>
    private void LoadStreetsByArea()
    {
      if (ddlArea.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlArea.SelectedValue), null, KladrLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlStreet);
      }
    }

    /// <summary>
    ///   Загрузка улицы по городу (если у города нет внутригородских районов и населенных пунктов)
    /// </summary>
    private void LoadStreetsByCity()
    {
      if (ddlCity.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlCity.SelectedValue), null, KladrLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlStreet);
      }
    }

    /// <summary>
    ///   Загрузка улицы по региону (если у региона нет округов, районов, городов, внутригородских районов и населенных
    ///   пунктов)
    /// </summary>
    private void LoadStreetsByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlSubject.SelectedValue), null, KladrLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlStreet);
      }
    }

    /// <summary>
    ///   Загрузка улицы по населенному пкнкту
    /// </summary>
    private void LoadStreetsByTown()
    {
      if (ddlTown.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlTown.SelectedValue), null, KladrLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlStreet);
      }
    }

    /// <summary>
    ///   Загрузка всех регионов
    /// </summary>
    private void LoadSubjects()
    {
      var addressObjects = addressService.GetAddressList(null, null, KladrLevel.Subject);
      foreach (var addressObject in addressObjects)
      {
        ddlSubject.Items.Add(new ListItem(addressObject.Name + " " + addressObject.Socr, addressObject.Id.ToString()));
      }
    }

    /// <summary>
    ///   Загрузка населенного пункта по району (если у района нет городов и внутригородских районов)
    /// </summary>
    private void LoadTownsByArea()
    {
      if (ddlArea.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlArea.SelectedValue), null, KladrLevel.Town);
      if (addressObjects.Count == 0)
      {
        ddlTown.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblTown.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlTown);
      }

      LoadStreetsByArea();
    }

    /// <summary>
    ///   Загрузка населенного пункта по городу (если у города нет внутригородских районов)
    /// </summary>
    private void LoadTownsByCity()
    {
      if (ddlCity.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlCity.SelectedValue), null, KladrLevel.Town);
      if (addressObjects.Count == 0)
      {
        ddlTown.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblTown.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlTown);
      }

      LoadStreetsByCity();
    }

    /// <summary>
    ///   Загрузка населенного пункта по региону (если у региона нет округов, районов, городов и внутригородских районов)
    /// </summary>
    private void LoadTownsByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
      {
        return;
      }

      var addressObjects = addressService.GetAddressList(new Guid(ddlSubject.SelectedValue), null, KladrLevel.Town);
      if (addressObjects.Count == 0)
      {
        ddlTown.Visible = false;
        if (Mode == KladrUserControlMode.Database)
        {
          lblTown.Visible = false;
        }
      }
      else
      {
        FillDdl(addressObjects, ddlTown);
      }

      LoadStreetsByProvince();
    }

    /// <summary>
    ///   The set database mode.
    /// </summary>
    private void SetDatabaseMode()
    {
      // ClearAndShowAllControls();
      ddlTown.Visible = true;
      ddlStreet.Visible = true;
      tbTown.Visible = false;
      tbStreet.Visible = false;
    }

    /// <summary>
    ///   The set free mode.
    /// </summary>
    private void SetFreeMode()
    {
      // ClearAndShowAllControls();
      ddlTown.Visible = false;
      tbTown.Text = string.Empty;
      tbTown.Visible = true;
      lblTown.Visible = true;

      ddlStreet.Visible = false;
      tbStreet.Text = string.Empty;
      tbStreet.Visible = true;
      lblStreet.Visible = true;
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

      UpdatePanelPostcode.Update();
    }

    #endregion
  }
}