// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwinPersonControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Twins
{
  #region

  using System;
  using System.Drawing;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  #endregion

  /// <summary>
  /// The twin person control.
  /// </summary>
  public partial class TwinPersonControl : UserControl
  {

    public enum ResultSet { Succeed, NotFoundStatement };

    #region Fields

    /// <summary>
    /// The _service.
    /// </summary>
    private ITFService _service;

    /// <summary>
    /// The _statement service.
    /// </summary>
    private IStatementService _statementService;

    #endregion

    #region Public Events

    /// <summary>
    /// The after join twins.
    /// </summary>
    public event Action AfterJoinTwins;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
      lbTitle.Text = lbTitle.Text = string.Format("Человек {0} Id", string.Empty);
      tbId.Text = null;
      tbLastName.Text = null;
      tbFirstName.Text = null;
      tbMiddleName.Text = null;
      tbBirthDate.Text = null;
      tbGender.Text = null;
      tbSnils.Text = null;

      tbUdlSeries.Text = null;
      tbUdlAdditionalSeries.Text = null;
      tbUdlNumber.Text = null;

      tbRegSeries.Text = null;
      tbRegAdditionalSeries.Text = null;
      tbRegNumber.Text = null;

      tbCitizenship.Text = null;
      tbBirthPlace.Text = null;
      tbCategory.Text = null;

      tbRegistrationAddress.Text = null;
      tbLiveAddress.Text = null;

      cbWithoutCitizenship.Checked = false;
      cbRefugee.Checked = false;

      tbEnp.Text = null;
      tbHomePhone.Text = null;
      tbWorkPhone.Text = null;
      tbEmail.Text = null;

      tbMedicalInsurance.Text = null;
      tbMedicalInsuranceFrom.Text = null;
      tbMedicalInsuranceTo.Text = null;
    }

    /// <summary>
    /// The hide join button.
    /// </summary>
    public void HideJoinButton()
    {
      joinDiv.Visible = false;
    }

    /// <summary>
    /// The set data.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    public ResultSet SetData(TwinItem item)
    {
      Statement statement;
      Statement statToComparing;
      if (item is InsuranceHistoryItem)
      {
        statement = (item as InsuranceHistoryItem).Statement;
        statToComparing = statement;
      }
      else
      {
        if (item.PersonNumber == 1)
        {
          statement = _statementService.GetStatementByInsuredPersonId(item.Person1Id);
          statToComparing = _statementService.GetStatementByInsuredPersonId(item.Person2Id);
        }
        else
        {
          statement = _statementService.GetStatementByInsuredPersonId(item.Person2Id);
          statToComparing = _statementService.GetStatementByInsuredPersonId(item.Person1Id);
        }
        if (statement == null || statToComparing == null)
        {
          return ResultSet.NotFoundStatement;
        }
      }

      ViewState["TwinId"] = item.TwinId;
      ViewState["Id1"] = item.Person1Id;
      ViewState["Id2"] = item.Person2Id;
      ViewState["Number"] = item.PersonNumber;

      lbTitle.Text = string.Format("Человек {0} Id", item.PersonNumber);
      var data = statement.InsuredPersonData;
      var dataToCompare = statToComparing.InsuredPersonData;

      tbId.Text = statement.InsuredPerson.Id.ToString();
      SetValue(tbLastName, data.LastName, dataToCompare.LastName);

      SetValue(tbFirstName, data.FirstName, dataToCompare.FirstName);
      SetValue(tbMiddleName, data.MiddleName, dataToCompare.MiddleName);
      SetValue(
        tbBirthDate, 
        data.Birthday.HasValue ? data.Birthday.Value.ToShortDateString() : data.Birthday2, 
        dataToCompare.Birthday.HasValue ? dataToCompare.Birthday.Value.ToShortDateString() : dataToCompare.Birthday2);
      SetValue(
        tbGender, 
        data.Gender != null ? data.Gender.Name : string.Empty, 
        dataToCompare.Gender != null ? dataToCompare.Gender.Name : string.Empty);
      SetValue(tbSnils, data.Snils, dataToCompare.Snils);

      string series;
      string additionalSeries;
      UdlHelper.GetSeries(
        statement.DocumentUdl.DocumentType.Id, statement.DocumentUdl.Series, out series, out additionalSeries);
      string cseries;
      string cadditionalSeries;
      UdlHelper.GetSeries(
        statToComparing.DocumentUdl.DocumentType.Id, 
        statToComparing.DocumentUdl.Series, 
        out cseries, 
        out cadditionalSeries);
      SetValue(tbUdlSeries, series, cseries);
      SetValue(tbUdlAdditionalSeries, additionalSeries, cadditionalSeries);
      SetValue(tbUdlNumber, statement.DocumentUdl.Number, statToComparing.DocumentUdl.Number);

      SetValue(
        tbCitizenship, 
        data.Citizenship != null ? data.Citizenship.Name : string.Empty, 
        dataToCompare.Citizenship != null ? dataToCompare.Citizenship.Name : string.Empty);
      SetValue(tbBirthPlace, data.Birthplace, dataToCompare.Birthplace);
      SetValue(
        tbCategory, 
        data.Category != null ? data.Category.Name : string.Empty, 
        dataToCompare.Category != null ? dataToCompare.Category.Name : string.Empty);
      SetValue(tbRegistrationAddress, statement.AddressRegistrationStr, statToComparing.AddressRegistrationStr);
      SetValue(tbLiveAddress, statement.AddressLiveStr, statToComparing.AddressLiveStr);

      UdlHelper.GetSeries(
        statement.DocumentRegistration != null ? statement.DocumentRegistration.DocumentType.Id : -1, 
        statement.DocumentRegistration != null ? statement.DocumentRegistration.Series : null, 
        out series, 
        out additionalSeries);
      UdlHelper.GetSeries(
        statToComparing.DocumentRegistration != null ? statToComparing.DocumentRegistration.DocumentType.Id : -1,
        statToComparing.DocumentRegistration != null ? statToComparing.DocumentRegistration.Series : null, 
        out cseries, 
        out cadditionalSeries);
      SetValue(tbRegSeries, series, cseries);
      SetValue(tbRegAdditionalSeries, additionalSeries, cadditionalSeries);
      SetValue(tbRegNumber, statement.DocumentUdl.Number, statToComparing.DocumentUdl.Number);

      SetValue(cbWithoutCitizenship, data.IsNotCitizenship, dataToCompare.IsNotCitizenship);
      SetValue(cbRefugee, data.IsRefugee, dataToCompare.IsRefugee);

      SetValue(tbEnp, statement.NumberPolicy, statToComparing.NumberPolicy);

      var statContactNull = statement.ContactInfo == null;
      var statToComparingContactNull = statToComparing.ContactInfo == null;

      SetValue(
        tbHomePhone, 
        !statContactNull ? statement.ContactInfo.HomePhone : null, 
        !statToComparingContactNull ? statToComparing.ContactInfo.HomePhone : null);
      SetValue(
        tbWorkPhone, 
        !statContactNull ? statement.ContactInfo.WorkPhone : null, 
        !statToComparingContactNull ? statToComparing.ContactInfo.WorkPhone : null);
      SetValue(
        tbEmail, 
        !statContactNull ? statement.ContactInfo.Email : null, 
        !statToComparingContactNull ? statToComparing.ContactInfo.Email : null);

      SetValue(tbMedicalInsurance, GetMedicalInsuranceSeriesNumber(statement), GetMedicalInsuranceSeriesNumber(statToComparing));
      SetValue(tbMedicalInsuranceFrom, GetMedicalInsuranceDateFrom(statement), GetMedicalInsuranceDateFrom(statToComparing));
      SetValue(tbMedicalInsuranceTo, GetMedicalInsuranceDateTo(statement), GetMedicalInsuranceDateTo(statToComparing));
      return ResultSet.Succeed;
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
      _service = ObjectFactory.GetInstance<ITFService>();
      _statementService = ObjectFactory.GetInstance<IStatementService>();
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
    }

    /// <summary>
    /// The btn join_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnJoin_Click(object sender, EventArgs e)
    {
      if (ViewState["Id1"] == null)
      {
        return;
      }

      var twinId = (Guid)ViewState["TwinId"];
      var Id1 = (Guid)ViewState["Id1"];
      var Id2 = (Guid)ViewState["Id2"];
      var number = (int)ViewState["Number"];
      Guid mainId;
      Guid secondId;
      if (number == 1)
      {
        mainId = Id1;
        secondId = Id2;
      }
      else
      {
        mainId = Id2;
        secondId = Id1;
      }

      _service.JoinTwins(twinId, mainId, secondId);
      if (AfterJoinTwins != null)
      {
        AfterJoinTwins();
      }
    }

    /// <summary>
    /// The get medical insurance.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetMedicalInsuranceSeriesNumber(Statement statement)
    {
      var insurance = GetMedicalInsurance(statement);
      return insurance != null ? insurance.SeriesNumber : null;
    }

    private string GetMedicalInsuranceDateFrom(Statement statement)
    {
      var insurance = GetMedicalInsurance(statement);
      return insurance != null ? insurance.DateFrom.ToShortDateString() : null;
    }

    private string GetMedicalInsuranceDateTo(Statement statement)
    {
      var insurance = GetMedicalInsurance(statement);
      return insurance != null ? insurance.DateTo.ToShortDateString() : null;
    }

    /// <summary>
    /// The get medical insurance.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="MedicalInsurance"/>.
    /// </returns>
    private MedicalInsurance GetMedicalInsurance(Statement statement)
    {
      var lastOrDefault = statement.InsuredPerson.MedicalInsurances.LastOrDefault(x => x.IsActive);
      return lastOrDefault;
    }

    /// <summary>
    /// The set value.
    /// </summary>
    /// <param name="tb">
    /// The tb.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="valueToCompare">
    /// The value to compare.
    /// </param>
    private void SetValue(TextBox tb, string value, string valueToCompare)
    {
      tb.Text = value;
      if (value != valueToCompare)
      {
        tb.ForeColor = Color.Red;
      }
      else
      {
        tb.ForeColor = Color.Black;
      }
    }

    /// <summary>
    /// The set value.
    /// </summary>
    /// <param name="cb">
    /// The cb.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="valueToCompare">
    /// The value to compare.
    /// </param>
    private void SetValue(CheckBox cb, bool value, bool valueToCompare)
    {
      cb.Checked = value;
      if (value != valueToCompare)
      {
        cb.ForeColor = Color.Red;
      }
    }

    #endregion
  }

  /// <summary>
  /// The twin item.
  /// </summary>
  public class TwinItem
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TwinItem"/> class.
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    /// <param name="personNumber">
    /// The person number.
    /// </param>
    /// <param name="person1Id">
    /// The person 1 id.
    /// </param>
    /// <param name="person2Id">
    /// The person 2 id.
    /// </param>
    public TwinItem(Guid twinId, int personNumber, Guid person1Id, Guid person2Id)
    {
      TwinId = twinId;
      PersonNumber = personNumber;
      Person1Id = person1Id;
      Person2Id = person2Id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TwinItem"/> class.
    /// </summary>
    protected TwinItem()
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the person 1 id.
    /// </summary>
    public Guid Person1Id { get; set; }

    /// <summary>
    /// Gets or sets the person 2 id.
    /// </summary>
    public Guid Person2Id { get; set; }

    /// <summary>
    /// Gets or sets the person number.
    /// </summary>
    public int PersonNumber { get; set; }

    /// <summary>
    /// Gets or sets the twin id.
    /// </summary>
    public Guid TwinId { get; set; }

    #endregion
  }

  /// <summary>
  /// The separate item.
  /// </summary>
  public class SeparateItem : TwinItem
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SeparateItem"/> class.
    /// </summary>
    /// <param name="personNumber">
    /// The person number.
    /// </param>
    /// <param name="person1Id">
    /// The person 1 id.
    /// </param>
    /// <param name="person2Id">
    /// The person 2 id.
    /// </param>
    public SeparateItem(int personNumber, Guid person1Id, Guid person2Id)
    {
      TwinId = Guid.Empty;
      PersonNumber = personNumber;
      Person1Id = person1Id;
      Person2Id = person2Id;
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="SeparateItem"/> class from being created.
    /// </summary>
    private SeparateItem()
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the twin id.
    /// </summary>
    protected Guid TwinId { get; set; }

    #endregion
  }

  /// <summary>
  /// The insurance history item.
  /// </summary>
  public class InsuranceHistoryItem : TwinItem
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="InsuranceHistoryItem"/> class.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public InsuranceHistoryItem(Statement statement)
    {
      TwinId = Guid.Empty;
      PersonNumber = 1;
      Person1Id = statement.InsuredPerson.Id;
      Person2Id = Person1Id;
      Statement = statement;
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="InsuranceHistoryItem"/> class from being created.
    /// </summary>
    private InsuranceHistoryItem()
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the statement.
    /// </summary>
    public Statement Statement { get; set; }

    #endregion
  }
}