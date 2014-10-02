using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using StructureMap;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using System.Web.UI;

namespace rt.srz.ui.pvp.Controls
{
  public enum KLADRUserControlMode
  {
    Database, //Все данные загружаются из справочника в БД
    Free      //Данные для населенного пунута и улицы можно ввести вручную
  }

  public partial class KLADRUserControl : System.Web.UI.UserControl, IKLADRUserControl
  {
    #region Constants
    private const string MoscowOkato = "45000000000";
    private const string StPetersburgOkato = "40000000000";
    #endregion

    #region Fields
    private IKladrService _kladrService;
    private ISecurityService _securityService;
    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
      _kladrService = ObjectFactory.GetInstance<IKladrService>();
      _securityService = ObjectFactory.GetInstance<ISecurityService>();

      if (!IsPostBack)
      {
        //Режим по умолчанию - работа с базой данных
        Mode = KLADRUserControlMode.Database;
        LoadSubjects();
        ClearAndShowAllControls();
        SetDefaultSubject();
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
        UniqueKey = Guid.NewGuid().ToString("N");
    }

    protected void chbIsFreeAddress_CheckedChanged(object sender, EventArgs e)
    {
      //if (chbIsFreeAddress.Checked)
      //    Mode = KLADRUserControlMode.Free;
      //else
      //    Mode = KLADRUserControlMode.Database;
    }

    #endregion

    #region Properties
/// <summary>
    ///   The unique key.
    /// </summary>
    protected string UniqueKey { get; set; }

    public KLADRUserControlMode Mode
    {
      get
      {
        if (ViewState["Mode"] != null)
          return (KLADRUserControlMode)ViewState["Mode"];
        else
        {
          //TODO
          //что возвращать если "Mode" нет во ViewState?
          return KLADRUserControlMode.Database;
        }
      }
      set
      {
        switch (value)
        {
          case KLADRUserControlMode.Database:
            SetDatabaseMode();
            break;
          case KLADRUserControlMode.Free:
            SetFreeMode();
            break;
        }

        ViewState["Mode"] = value;
        UpdatePanelKLADR.Update();
      }
    }

    /// <summary>
    /// Выбранный регион
    /// </summary>
    public string Subject
    {
      get
      {
        if (ddlSubject.SelectedIndex != 0)
          return ddlSubject.SelectedItem.Text;

        return null;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;

        foreach (ListItem item in ddlSubject.Items)
          if (item.Text == value)
          {
            ddlSubject.SelectedValue = item.Value;
            break;
          }

        ddlSubject_SelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    /// Выбранный регион
    /// </summary>
    public string SubjectId
    {
      get
      {
        if (ddlSubject.SelectedIndex != 0)
          return ddlSubject.SelectedValue;

        return null;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;

        ddlSubject.SelectedValue = value;
        ddlSubject_SelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    /// Выбранный район
    /// </summary>
    public string Area
    {
      get
      {
        if (ddlArea.SelectedIndex != 0)
          return ddlArea.SelectedItem.Text;

        return null;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;

        foreach (ListItem item in ddlArea.Items)
          if (item.Text == value)
          {
            ddlArea.SelectedValue = item.Value;
            break;
          }

        ddlArea_SelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    /// Выбранный город
    /// </summary>
    public string City
    {
      get
      {
        if (ddlCity.SelectedIndex != 0)
          return ddlCity.SelectedItem.Text;

        return null;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;

        foreach (ListItem item in ddlCity.Items)
          if (item.Text == value)
          {
            ddlCity.SelectedValue = item.Value;
            break;
          }

        ddlCity_SelectedIndexChanged(null, null);
      }
    }

    /// <summary>
    /// Выбранный населенный пункт
    /// </summary>
    public string Town
    {
      get
      {
        switch (Mode)
        {
          case KLADRUserControlMode.Database:
            {
              if (ddlTown.SelectedIndex != 0)
                return ddlTown.SelectedItem.Text;
              return null;
            }
          case KLADRUserControlMode.Free:
            {
              if (tbTown.Text.Length != 0)
                return tbTown.Text;

              return null;
            }
          default:
            return null;
        }
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;

        if (Mode == KLADRUserControlMode.Database)
        {
          foreach (ListItem item in ddlTown.Items)
          {
            if (item.Text == value)
            {
              ddlTown.SelectedValue = item.Value;
              break;
            }
          }
          ddlTown_SelectedIndexChanged(null, null);
        }
        else
        {
          tbTown.Text = value;
        }
      }
    }

    /// <summary>
    /// Выбранная улица
    /// </summary>
    public string Street
    {
      get
      {
        switch (Mode)
        {
          case KLADRUserControlMode.Database:
            {
              if (ddlStreet.SelectedIndex != 0)
                return ddlStreet.SelectedItem.Text;
              return null;
            }
          case KLADRUserControlMode.Free:
            {
              if (tbStreet.Text.Length != 0)
                return tbStreet.Text;

              return null;
            }
          default:
            return null;
        }
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;

        if (Mode == KLADRUserControlMode.Database)
        {
          foreach (ListItem item in ddlStreet.Items)
          {
            if (item.Text == value)
            {
              ddlStreet.SelectedValue = item.Value;
              break;
            }
          }
          ddlStreet_SelectedIndexChanged(null, null);
        }
        else
        {
          tbStreet.Text = value;
        }
      }
    }

    //Текущее значение идентификатора и базы КЛАДР
    public Guid SelectedKLADRId
    {
      get
      {
        //if (Mode == KLADRUserControlMode.Database)
        //{
          if (ddlStreet.SelectedIndex != 0)
            return new Guid(ddlStreet.SelectedValue);
          else if (ddlTown.SelectedIndex != 0)
            return new Guid(ddlTown.SelectedValue);
          else if (ddlCity.SelectedIndex != 0)
            return new Guid(ddlCity.SelectedValue);
          else if (ddlArea.SelectedIndex != 0)
            return new Guid(ddlArea.SelectedValue);
          else if (ddlSubject.SelectedIndex != 0)
            return new Guid(ddlSubject.SelectedValue);
        //}

          return Guid.Empty;
      }
      set
      {
      }
    }

    #endregion

    #region Methods

    public void SetFocusFirstControl()
    {
      ddlSubject.Focus();
    }

    private void SetPostcode()
    {
      if (SelectedKLADRId == null)
        return;

      tbPostcode.Text = string.Empty;
      Kladr kladr = _kladrService.GetKLADR(SelectedKLADRId);
      if (kladr != null && kladr.Index != null)
        tbPostcode.Text = kladr.Index.ToString();

      UpdatePanelPostcode.Update();
    }
    
    public void Enable(bool bEnable)
    {
      tbPostcode.Enabled = bEnable;
      ddlSubject.Enabled = bEnable;
      ddlArea.Enabled = bEnable;
      ddlCity.Enabled = bEnable;
      ddlTown.Enabled = bEnable;
      ddlStreet.Enabled = bEnable;
      tbStreet.Enabled = bEnable;
      tbTown.Enabled = bEnable;
      tbHouse.Enabled = bEnable;
      tbHousing.Enabled = bEnable;
      tbRoom.Enabled = bEnable;
    }
    
    public void SetDefaultSubject()
    {
      //Получение текущего региона для текущего пользователя
      User currentUser = _securityService.GetCurrentUser();
      if (currentUser != null && currentUser.PointDistributionPolicy != null && currentUser.PointDistributionPolicy.Parent != null && currentUser.PointDistributionPolicy.Parent.Parent != null)
      {
        var tfom = currentUser.PointDistributionPolicy.Parent.Parent;
        Kladr kladr = _kladrService.GetFirstLevelByTfoms(tfom);
        if (kladr != null)
        {
          //Установка региона по умолчанию
          SubjectId = kladr.Id.ToString();

          //Установка индекса по умолчанию
          SetPostcode();
        }
      }
    }

    private void ClearAndShowAllControls()
    {
      ClearAndShowControl(ddlArea, lblArea, "район");
      ClearAndShowControl(ddlCity, lblCity, "город");
      ClearAndShowControl(ddlTown, lblTown, "населенный пункт");
      ClearAndShowControl(ddlStreet, lblStreet, "улицу");
      if (Mode == KLADRUserControlMode.Free)
      {
        ddlTown.Visible = false;
        ddlStreet.Visible = false;
      }
    }

    private void ClearAndShowControl(DropDownList dropDownList, Label label, String text)
    {
      dropDownList.Items.Clear();
      dropDownList.Items.Add("Выберите " + text);
      dropDownList.Visible = true;
      label.Visible = true;
    }

    private void ClearControl(DropDownList dropDownList, String text)
    {
      dropDownList.Items.Clear();
      dropDownList.Items.Add("Выберите " + text);
    }

    private void FillDdl(IList<Kladr> addressObjects, DropDownList dropDownList)
    {
      foreach (Kladr addressObject in addressObjects)
        dropDownList.Items.Add(new ListItem(addressObject.Name + " " + addressObject.Socr, addressObject.Id.ToString()));
    }

    private void SetDatabaseMode()
    {
      //ClearAndShowAllControls();
      ddlTown.Visible = true;
      ddlStreet.Visible = true;
      tbTown.Visible = false;
      tbStreet.Visible = false;
    }

    private void SetFreeMode()
    {
      //ClearAndShowAllControls();
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
    /// Загрузка всех регионов
    /// </summary>
    private void LoadSubjects()
    {
      IList<Kladr> addressObjects = _kladrService.GetKLADRs(null, null, KLADRLevel.Subject);
      foreach (Kladr addressObject in addressObjects)
        ddlSubject.Items.Add(new ListItem(addressObject.Name + " " + addressObject.Socr, addressObject.Id.ToString()));
    }

    #endregion

    #region SelectedIndexChanged

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
      ClearAndShowAllControls();

      if (ddlSubject.SelectedIndex != 0)
      {
        LoadAreasByProvince();

        //Спец. Случай для Москвы и Питера
        //загружаем их же в поле "Населенный пункт"
        Kladr obj = _kladrService.GetKLADR(new Guid(ddlSubject.SelectedValue));
        if (obj != null && !string.IsNullOrEmpty(obj.Ocatd))
        {
          if (obj.Ocatd == MoscowOkato || obj.Ocatd == StPetersburgOkato)
          {
            ddlTown.Items.Add(new ListItem(obj.Name + " " + obj.Socr, obj.Id.ToString()));
            ddlTown.SelectedValue = obj.Id.ToString();
          }
        }
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
      ClearAndShowControl(ddlCity, lblCity, "город");
      if (Mode == KLADRUserControlMode.Database)
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
        LoadCityByArea();
      else
      {
        ClearAndShowAllControls();

        if (ddlSubject.SelectedIndex != 0)
          LoadAreasByProvince();
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Mode == KLADRUserControlMode.Database)
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
        //lblArea.Visible = false;
        //ddlArea.Visible = false;
        LoadTownsByCity();
      }
      else
      {
        //lblArea.Visible = true;
        //ddlArea.Visible = true;
        if (ddlArea.SelectedIndex != 0)
          LoadTownsByArea();
        else
          LoadStreetsByProvince();
      }

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Mode == KLADRUserControlMode.Database)
      {
        ClearAndShowControl(ddlStreet, lblStreet, "улицу");
      }
      else
      {
        ClearControl(ddlStreet, "улицу");
      }

      if (ddlTown.SelectedIndex != 0)
        LoadStreetsByTown();
      else
        LoadStreetsByCity();

      SetPostcode();
      UpdatePanelKLADR.Update();
    }

    protected void ddlStreet_SelectedIndexChanged(object sender, EventArgs e)
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

    #endregion

    #region Загрузка объектов по региону

    /// <summary>
    /// Загрузка районов по региону (если у региона нет округов)
    /// </summary>
    private void LoadAreasByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlSubject.SelectedValue), null, KLADRLevel.Area);
      if (addressObjects.Count == 0)
      {
        lblArea.Visible = false;
        ddlArea.Visible = false;
      }
      else
        FillDdl(addressObjects, ddlArea);

      LoadCitiesByProvince();
    }

    /// <summary>
    /// Загрузка городов по региону (если у региона нет округов и районов)
    /// </summary>
    private void LoadCitiesByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlSubject.SelectedValue), null, KLADRLevel.City);
      if (addressObjects.Count == 0)
      {
        lblCity.Visible = false;
        ddlCity.Visible = false;
      }
      else
        FillDdl(addressObjects, ddlCity);

      LoadTownsByProvince();
    }

    /// <summary>
    /// Загрузка населенного пункта по региону (если у региона нет округов, районов, городов и внутригородских районов)
    /// </summary>
    private void LoadTownsByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlSubject.SelectedValue), null, KLADRLevel.Town);
      if (addressObjects.Count == 0)
      {
        ddlTown.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblTown.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlTown);

      LoadStreetsByProvince();
    }

    /// <summary>
    /// Загрузка улицы по региону (если у региона нет округов, районов, городов, внутригородских районов и населенных пунктов)
    /// </summary>
    private void LoadStreetsByProvince()
    {
      if (ddlSubject.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlSubject.SelectedValue), null, KLADRLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlStreet);
    }

    #endregion

    #region Загрузка объектов по району

    /// <summary>
    /// Загрузка городов по району
    /// </summary>
    private void LoadCityByArea()
    {
      if (ddlArea.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlArea.SelectedValue), null, KLADRLevel.City);
      if (addressObjects.Count == 0)
      {
        lblCity.Visible = false;
        ddlCity.Visible = false;
      }
      else
        FillDdl(addressObjects, ddlCity);

      LoadTownsByArea();
    }

    /// <summary>
    /// Загрузка населенного пункта по району (если у района нет городов и внутригородских районов)
    /// </summary>
    private void LoadTownsByArea()
    {
      if (ddlArea.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlArea.SelectedValue), null, KLADRLevel.Town);
      if (addressObjects.Count == 0)
      {
        ddlTown.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblTown.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlTown);

      LoadStreetsByArea();
    }

    /// <summary>
    /// Загрузка улицы по району (если у района нет городов, внутригородских районов и населенных пунктов)
    /// </summary>
    private void LoadStreetsByArea()
    {
      if (ddlArea.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlArea.SelectedValue), null, KLADRLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlStreet);
    }

    #endregion

    #region Загрузка объектов по городу

    /// <summary>
    /// Загрузка населенного пункта по городу (если у города нет внутригородских районов)
    /// </summary>
    private void LoadTownsByCity()
    {
      if (ddlCity.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlCity.SelectedValue), null, KLADRLevel.Town);
      if (addressObjects.Count == 0)
      {
        ddlTown.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblTown.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlTown);

      LoadStreetsByCity();
    }

    /// <summary>
    /// Загрузка улицы по городу (если у города нет внутригородских районов и населенных пунктов)
    /// </summary>
    private void LoadStreetsByCity()
    {
      if (ddlCity.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlCity.SelectedValue), null, KLADRLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlStreet);
    }

    #endregion

    #region Загрузка объектов по населенному пункту

    /// <summary>
    /// Загрузка улицы по населенному пкнкту
    /// </summary>
    private void LoadStreetsByTown()
    {
      if (ddlTown.SelectedIndex == 0)
        return;

      IList<Kladr> addressObjects = _kladrService.GetKLADRs(new Guid(ddlTown.SelectedValue), null, KLADRLevel.Street);
      if (addressObjects.Count == 0)
      {
        ddlStreet.Visible = false;
        if (Mode == KLADRUserControlMode.Database)
        {
          lblStreet.Visible = false;
        }
      }
      else
        FillDdl(addressObjects, ddlStreet);
    }

    #endregion
  }
}