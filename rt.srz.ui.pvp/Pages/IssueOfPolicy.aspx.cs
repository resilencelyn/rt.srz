using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using rt.srz.model.interfaces.service;
using rt.srz.model.logicalcontrol;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.services;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;

namespace rt.srz.ui.pvp.Pages
{
  using rt.core.model.interfaces;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.ui.pvp.Controls;

  public partial class IssueOfPolicy : System.Web.UI.Page
  {
    #region Properties

    /// <summary>
    ///   Gets the statement id.
    /// </summary>
    private string StatementId
    {
      get { return Session[SessionConsts.CGuidStatementId] != null ? Session[SessionConsts.CGuidStatementId].ToString() : string.Empty; }
    }
    #endregion

    #region Methods
    private Statement CloneStatement()
    {
      var newStatement = new Statement();
      var statementService = ObjectFactory.GetInstance<IStatementService>();
      var oldStatement = statementService.GetStatement(new Guid(StatementId));

      if (oldStatement != null)
      {
        // клонирование
        newStatement.AbsentPrevPolicy = oldStatement.AbsentPrevPolicy;
        newStatement.DateFiling = oldStatement.DateFiling;
        newStatement.FormManufacturing = oldStatement.FormManufacturing;
        newStatement.CauseFiling = oldStatement.CauseFiling;
        newStatement.MedicalInsurances = oldStatement.MedicalInsurances.ToList();
        newStatement.InsuredPersonData = new InsuredPersonDatum();
        newStatement.InsuredPersonData.Birthday = oldStatement.InsuredPersonData.Birthday;
        newStatement.InsuredPersonData.Gender = oldStatement.InsuredPersonData.Gender;
      }

      return newStatement;
    }

    private Statement MoveDataFromGui2Object(Statement statement)
    {
      var statementService = ObjectFactory.GetInstance<IStatementService>();
      var regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();

      // Выдан полис
      statement.PolicyIsIssued = true;

      statement.NotCheckPolisNumber = true;

      if (statement.MedicalInsurances == null)
      {
        statement.MedicalInsurances = new List<MedicalInsurance>(2);
      }

      var insurance = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В && x.PolisType.Id != PolisType.С);
      if (insurance == null)
      {
        insurance = new MedicalInsurance();
        statement.MedicalInsurances.Add(insurance);
      }

      insurance.IsActive = true;
      insurance.Statement = statement;
      insurance.StateDateTo = new DateTime(2200, 1, 1);
      insurance.StateDateFrom = DateTime.Now;
      insurance.Smo = user.HasSmo() ? user.GetSmo() : null;

      // Тип выдаваемого полиса
      var policyType = int.Parse(ctrlIssueOfPolicy.PolicyTypeId);
      if (policyType >= 0)
      {
        insurance.PolisType = regulatoryService.GetConcept(policyType);
      }

      // Номер ЕНПk
      insurance.Enp = ctrlIssueOfPolicy.EnpNumber;
      // Серия полиса
      insurance.PolisSeria = string.Empty;
      // Номер полиса
      insurance.PolisNumber = ctrlIssueOfPolicy.PolicyNumber;
      // Дата выдачи
      DateTime dateTime;
      insurance.DateFrom = !string.IsNullOrEmpty(ctrlIssueOfPolicy.PolicyDateIssue) && DateTime.TryParse(ctrlIssueOfPolicy.PolicyDateIssue, out dateTime)
        ? dateTime : DateTime.Now.Date;
      // Дата окончания
      insurance.DateTo = !string.IsNullOrEmpty(ctrlIssueOfPolicy.PolicyDateEnd) && DateTime.TryParse(ctrlIssueOfPolicy.PolicyDateEnd, out dateTime)
        ? dateTime : DateTime.Now.Date;

      return statement;
    }
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        var statementService = ObjectFactory.GetInstance<IStatementService>();

        // Получаем заявление
        var statement = statementService.GetStatement(new Guid(StatementId));

        // Заполняем типы полисов
        if (statement != null)
        {
           // В случае если полис заказан в составе УЭК, то пользователь может осуществить только операцию
          // выдачи бумажного полиса
          var selectedValue = statement.FormManufacturing.Id == PolisType.К ? PolisType.П.ToString(CultureInfo.InvariantCulture) :
            statement.FormManufacturing.Id.ToString(CultureInfo.InvariantCulture);
          
          ctrlIssueOfPolicy.FillPolicyTypeDdl(
            statementService.GetTypePolisByFormManufacturing(statement.FormManufacturing.Id).Select(
              x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray(),
              selectedValue);
        }

        // Заполняем дату выдачи
        ctrlIssueOfPolicy.PolicyDateIssue = DateTime.Now.Date.ToShortDateString();

        // Заполняем дату окончания действия
        if (statement != null
          && statement.InsuredPersonData != null
          && (statement.InsuredPersonData.Citizenship == null
          || (statement.InsuredPersonData.Citizenship != null
          && statement.InsuredPersonData.Citizenship.Id != Country.RUS)))
        {
          if (statement.DocumentUdl != null && statement.DocumentUdl.DateExp.HasValue)
          {
            ctrlIssueOfPolicy.PolicyDateEnd = statement.DocumentUdl.DateExp.Value.ToShortDateString();
          }
          else
          {
            if (statement.ResidencyDocument.DateExp.HasValue)
            {
              ctrlIssueOfPolicy.PolicyDateEnd = statement.ResidencyDocument.DateExp.Value.ToShortDateString();
            }
            else
            {
              ctrlIssueOfPolicy.PolicyDateEnd = new DateTime(1900, 1, 1).ToShortDateString();
            }
          }
        }
        else
        {
          ctrlIssueOfPolicy.PolicyDateEnd = new DateTime(2200, 1, 1).ToShortDateString();
        }

        // заполняем номер полиса
        if (statement != null)
        {
          ctrlIssueOfPolicy.EnpNumber = statement.NumberPolicy;
        }
      }
    }

    protected void btnSaveStatement_Click(object sender, EventArgs e)
    {
      // Проверяем валидность данных
      if (!IsValid)
        return;

      // Получаем заявление
      var statementService = ObjectFactory.GetInstance<IStatementService>();
      var statement = statementService.GetStatement(new Guid(StatementId));

      // Пишем данные внесенные пользователем
      MoveDataFromGui2Object(statement);

      try
      {
        // Сохраняем заявление
        statementService.SaveStatement(statement);
      }
      catch (LogicalControlException ex)
      {
        // Другая ошибка
        cvErrors.Text = ex.GetAllMessages();
        return;
      }

      // Переход на главную страницу
      RedirectUtils.ClearInStatementEditing();
      RedirectUtils.RedirectToMain(Response);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      // Получаем заявление
      var statementService = ObjectFactory.GetInstance<IStatementService>();
      var statement = statementService.GetStatement(new Guid(StatementId));

      ////// Сохранение действий с ПД
      ////var userActionManager = new UserActionManager();
      ////userActionManager.LogAccessToPersonalData(statement, "Отмена заявления");

      // Переход на главную страницу
      RedirectUtils.ClearInStatementEditing();
      RedirectUtils.RedirectToMain(Response);
    }

    #endregion

    #region Validators

    protected void ValidatePolicyCertificateNumber(object source, ServerValidateEventArgs args)
    {
      var statement = CloneStatement();
      MoveDataFromGui2Object(statement);
      try
      {
        ObjectFactory.GetInstance<IStatementService>().CheckPropertyStatement(statement,
          Utils.GetExpressionNode(x => x.MedicalInsurances[1].PolisNumber));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = ctrlIssueOfPolicy.cvPolicyCertificateNumber.IsValid = false;
        ctrlIssueOfPolicy.cvPolicyCertificateNumber.Text = e.GetAllMessages();
      }
    }

    protected void ValidateEnpNumber(object source, ServerValidateEventArgs args)
    {
      var statement = CloneStatement();
      MoveDataFromGui2Object(statement);
      try
      {
        ObjectFactory.GetInstance<IStatementService>().CheckPropertyStatement(statement,
          Utils.GetExpressionNode(x => x.MedicalInsurances[1].Enp));
      }
      catch (LogicalControlException ex)
      {
        args.IsValid = ctrlIssueOfPolicy.cvEnpNumber.IsValid = false;
        ctrlIssueOfPolicy.cvEnpNumber.Text = ex.GetAllMessages();
      }
    }

    protected void ValidatePolicyType(object source, ServerValidateEventArgs args)
    {
      // Получаем заявление
      var statement = CloneStatement();
      MoveDataFromGui2Object(statement);
      try
      {
        ObjectFactory.GetInstance<IStatementService>().CheckPropertyStatement(statement,
          Utils.GetExpressionNode(x => x.FormManufacturing.Id));
      }
      catch (LogicalControlException ex)
      {
        args.IsValid = ctrlIssueOfPolicy.cvPolicyType.IsValid = false;
        ctrlIssueOfPolicy.cvPolicyType.Text = ex.GetAllMessages();
      }
    }

    protected void ValidatePolicyDateIssue(object source, ServerValidateEventArgs args)
    {
      // Получаем заявление
      var statement = CloneStatement();
      MoveDataFromGui2Object(statement);
      try
      {
        ObjectFactory.GetInstance<IStatementService>().CheckPropertyStatement(statement,
          Utils.GetExpressionNode(x => x.MedicalInsurances[1].DateFrom));
      }
      catch (LogicalControlException ex)
      {
        args.IsValid = ctrlIssueOfPolicy.cvPolicyDateIssue.IsValid = false;
        ctrlIssueOfPolicy.cvPolicyDateIssue.Text = ex.GetAllMessages();
      }
    }

    #endregion

    protected void btnOpenStatement_OnClick(object sender, EventArgs e)
    {
      Session[SessionConsts.COperation] = StatementSearchMenuItem.Edit;
      RedirectUtils.RedirectToStatement(Response);
    }
  }
}