// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyTypeDetailControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Twins
{
  #region

  using System;
  using System.Globalization;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.srz.business.server;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  /// The search key type detail control.
  /// </summary>
  public partial class SearchKeyTypeDetailControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The statement service.
    /// </summary>
    private IStatementService statementService;

    /// <summary>
    /// The tf service.
    /// </summary>
    private ITFService tfService;

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
      tfService = ObjectFactory.GetInstance<ITFService>();
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
      // Попытка поднять редактиреумый объект из БД
      if (!IsPostBack)
      {
        // Начальное состояние полей редактирования
        tbLastNameLength.Enabled = false;
        tbFirstNameLength.Enabled = false;
        tbMiddleNameLength.Enabled = false;
        ddlBirtdateLength.Enabled = false;
        tbRegistrationStreetLength.Enabled = false;
        tbResidenceStreetLength.Enabled = false;
        tbTwinChars.Enabled = false;

        // заполняем combo "Тип операции"
        ddlOperationKey.Items.AddRange(
          statementService.GetNsiRecords(Oid.OperationKey).Where(x => x.Id != OperationKey.CentralSegmentKey).Select(
            x => new ListItem(x.Description, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());

        if (Request.QueryString["SearchKeyTypeId"] != null)
        {
          var keyType = tfService.GetSearchKeyType(Guid.Parse(Request.QueryString["SearchKeyTypeId"]));
          if (keyType != null)
          {
            MoveDataFromObject2GUI(keyType); // заполняем форму данными редактируемого объекта
          }
        }
      }
    }

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
      // переход на родительскую страницу
      Response.Redirect("~/Pages/Twins/SearchKeyTypes.aspx");
    }

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
      // Валидация
      Page.Validate();
      if (!Page.IsValid)
      {
        return;
      }

      // либо новый объект либо поднимаем редактируемый из БД
      var keyType = new SearchKeyType();
      if (Request.QueryString["SearchKeyTypeId"] != null)
      {
        keyType = tfService.GetSearchKeyType(Guid.Parse(Request.QueryString["SearchKeyTypeId"]));
      }

      // заполняем объекта данными из формы
      MoveDataFromGUI2Object(keyType);

      // сохраняем объект в БД
      tfService.SaveSearchKeyType(keyType);

      // переход на родительскую страницу
      Response.Redirect("~/Pages/Twins/SearchKeyTypes.aspx");
    }

    /// <summary>
    /// The move data from gu i 2 object.
    /// </summary>
    /// <param name="searchKeyType">
    /// The search key type.
    /// </param>
    private void MoveDataFromGUI2Object(SearchKeyType searchKeyType)
    {
      // Имя ключа
      searchKeyType.Name = tbKeyName.Text;

      // Код ключа
      searchKeyType.Code = tbKeyCode.Text;

      // Тип операции
      var operationKey = int.Parse(ddlOperationKey.SelectedValue);
      if (operationKey >= 0)
      {
        searchKeyType.OperationKey = statementService.GetConcept(operationKey);
      }

      // Фамилия
      searchKeyType.LastName = chbUseLastName.Checked;
      short length = 0;
      short.TryParse(tbLastNameLength.Text, out length);
      searchKeyType.LastNameLength = length;

      // Имя
      searchKeyType.FirstName = chbUseFirstName.Checked;
      length = 0;
      short.TryParse(tbFirstNameLength.Text, out length);
      searchKeyType.FirstNameLength = length;

      // Отчество
      searchKeyType.MiddleName = chbUseMiddleName.Checked;
      length = 0;
      short.TryParse(tbMiddleNameLength.Text, out length);
      searchKeyType.MiddleNameLength = length;

      // Дата рождения
      searchKeyType.Birthday = chbUseBirthDate.Checked;
      length = 0;
      short.TryParse(ddlBirtdateLength.SelectedValue, out length);
      searchKeyType.BirthdayLength = length;

      // Место рождения
      searchKeyType.Birthplace = chbUseBirthPlace.Checked;

      // СНИЛС
      searchKeyType.Snils = chbUseSnils.Checked;

      // УДЛ
      searchKeyType.DocumentType = chbUseUDLType.Checked;
      searchKeyType.DocumentSeries = chbUseUDLSeries.Checked;
      searchKeyType.DocumentNumber = chbUseUDLNumber.Checked;

      // ОКАТО
      searchKeyType.Okato = chbUseOkato.Checked;

      // Адрес регистрации
      searchKeyType.AddressStreet = chbUseRegistrationStreet.Checked;
      length = 0;
      short.TryParse(tbRegistrationStreetLength.Text, out length);
      searchKeyType.AddressStreetLength = length;
      searchKeyType.AddressHouse = chbUseRegistrationHouse.Checked;
      searchKeyType.AddressRoom = chbUseRegistrationRoom.Checked;

      // Адрес проживания
      searchKeyType.AddressStreet2 = chbUseResidenceStreet.Checked;
      length = 0;
      short.TryParse(tbResidenceStreetLength.Text, out length);
      searchKeyType.AddressStreetLength2 = length;
      searchKeyType.AddressHouse2 = chbUseResidenceHouse.Checked;
      searchKeyType.AddressRoom2 = chbUseResidenceRoom.Checked;

      // Двойные буквы
      searchKeyType.DeleteTwinChar = chbDeleteTwinChar.Checked;
      searchKeyType.IdenticalLetters = tbTwinChars.Text;

      // Прикрепление
      searchKeyType.Insertion = chbInsertion.Checked;
    }

    /// <summary>
    /// The move data from object 2 gui.
    /// </summary>
    /// <param name="searchKeyType">
    /// The search key type.
    /// </param>
    private void MoveDataFromObject2GUI(SearchKeyType searchKeyType)
    {
      // Имя ключа
      tbKeyName.Text = searchKeyType.Name;

      // Код ключа
      tbKeyCode.Text = searchKeyType.Code;

      // Тип операции
      if (searchKeyType.OperationKey != null)
      {
        ddlOperationKey.SelectedValue = searchKeyType.OperationKey.Id.ToString();
      }

      // Фамилия
      chbUseLastName.Checked = searchKeyType.LastName;
      if (chbUseLastName.Checked)
      {
        tbLastNameLength.Enabled = true;
        tbLastNameLength.Text = searchKeyType.LastNameLength.ToString();
      }

      // Имя
      chbUseFirstName.Checked = searchKeyType.FirstName;
      if (chbUseFirstName.Checked)
      {
        tbFirstNameLength.Enabled = true;
        tbFirstNameLength.Text = searchKeyType.FirstNameLength.ToString();
      }

      // Отчество
      chbUseMiddleName.Checked = searchKeyType.MiddleName;
      if (chbUseMiddleName.Checked)
      {
        tbMiddleNameLength.Enabled = true;
        tbMiddleNameLength.Text = searchKeyType.MiddleNameLength.ToString();
      }

      // Дата рождения
      chbUseBirthDate.Checked = searchKeyType.Birthday;
      if (chbUseBirthDate.Checked)
      {
        ddlBirtdateLength.Enabled = true;
        ddlBirtdateLength.SelectedValue = searchKeyType.BirthdayLength.ToString();
      }

      // Место рождения
      chbUseBirthPlace.Checked = searchKeyType.Birthplace;

      // СНИЛС
      chbUseSnils.Checked = searchKeyType.Snils;

      // УДЛ
      chbUseUDLType.Checked = searchKeyType.DocumentType;
      chbUseUDLSeries.Checked = searchKeyType.DocumentSeries;
      chbUseUDLNumber.Checked = searchKeyType.DocumentNumber;

      // ОКАТО
      chbUseOkato.Checked = searchKeyType.Okato;

      // Адрес регистрации
      chbUseRegistrationStreet.Checked = searchKeyType.AddressStreet;
      if (chbUseRegistrationStreet.Checked)
      {
        tbRegistrationStreetLength.Enabled = true;
        tbRegistrationStreetLength.Text = searchKeyType.AddressStreetLength.ToString();
      }

      chbUseRegistrationHouse.Checked = searchKeyType.AddressHouse;
      chbUseRegistrationRoom.Checked = searchKeyType.AddressRoom;

      // Адрес проживания
      searchKeyType.AddressStreet2 = chbUseResidenceStreet.Checked;
      if (searchKeyType.AddressStreet2)
      {
        tbResidenceStreetLength.Enabled = true;
        tbResidenceStreetLength.Text = searchKeyType.AddressStreetLength2.ToString();
      }

      searchKeyType.AddressHouse2 = chbUseResidenceHouse.Checked;
      searchKeyType.AddressRoom2 = chbUseResidenceRoom.Checked;

      // Двойные буквы
      chbDeleteTwinChar.Checked = searchKeyType.DeleteTwinChar;
      if (chbDeleteTwinChar.Checked)
      {
        tbTwinChars.Enabled = true;
        tbTwinChars.Text = searchKeyType.IdenticalLetters;
      }

      // Прикрепление
      chbInsertion.Checked = searchKeyType.Insertion;
    }

    #endregion
  }
}