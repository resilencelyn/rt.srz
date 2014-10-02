using System;
using System.Web.UI.WebControls;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using StructureMap;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.model.logicalcontrol;
using rt.srz.services;

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  public partial class Step4 : WizardStep
  {
    private IStatementService _statementService;

    #region IWizardStep implementation

    /// <summary>
    /// Переносит данные из объекта в элементы на форме
    /// </summary>
    /// <param name="obj"></param>
    public override void MoveDataFromObject2GUI(Statement statement)
    {
      if (statement.Representative != null)
      {
        //Фамилия
        tbLastName.Text = statement.Representative.LastName;
        //Имя
        tbFirstName.Text = statement.Representative.FirstName;
        //Отчество
        tbMiddleName.Text = statement.Representative.MiddleName;
        //Отношение к застрахованному лицу
        if (statement.Representative.RelationType != null)
          ddlRelationType.SelectedValue = statement.Representative.RelationType.Id.ToString();
        //Документ УДЛ
        if (statement.Representative.Document != null)
        {
          //Вид документа
          if (statement.Representative.Document.DocumentType != null)
            documentUDL.DocumentType = statement.Representative.Document.DocumentType.Id;
          //Серия
          documentUDL.DocumentSeries = statement.Representative.Document.Series;
          //Номер
          documentUDL.DocumentNumber = statement.Representative.Document.Number;
          //Орган
          documentUDL.DocumentIssuingAuthority = statement.Representative.Document.IssuingAuthority;
          //Дата выдачи
          documentUDL.DocumentIssueDate = statement.Representative.Document.DateIssue;
          documentUDL.DocumentExpDate = statement.Representative.Document.DateExp;
        }
        //Телефон домашний
        tbHomePhone.Text = statement.Representative.HomePhone;
        //Телефон рабочий
        tbWorkPhone.Text = statement.Representative.WorkPhone;
      }
    }

    /// <summary>
    /// Переносит данные из элементов на форме в объект
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// </summary>
    /// <param name="setCurrentStatement">
    /// Обновлять ли свойство CurrentStatement после присвоения заявлению данных из дизайна 
    /// </param>
    public override void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true)
    {
      if (statement.ModeFiling.Id == ModeFiling.ModeFiling2)
      {
        Representative representative = statement.Representative ?? new Representative();
        //Фамилия
        representative.LastName = tbLastName.Text;
        //Имя
        representative.FirstName = tbFirstName.Text;
        //Отчество
        representative.MiddleName = tbMiddleName.Text;
        //Отношение к застрахованному лицу
        int relationType = Int32.Parse(ddlRelationType.SelectedValue);
        if (relationType >= 0)
          representative.RelationType = _statementService.GetConcept(relationType);

        //Документ УДЛ
        Document document = representative.Document ?? new Document();
        //Вид документа УДЛ
        if (documentUDL.DocumentType >= 0)
          document.DocumentType = _statementService.GetConcept(documentUDL.DocumentType);
        //Серия
        document.Series = documentUDL.DocumentSeries;
        //Номер
        document.Number = documentUDL.DocumentNumber;
        //Номер
        document.IssuingAuthority = documentUDL.DocumentIssuingAuthority;

        //Дата выдачи
        document.DateIssue = documentUDL.DocumentIssueDate;
        document.DateExp = documentUDL.DocumentExpDate;

        //присвоение документа
        representative.Document = document;

        //Телефон домашний
        representative.HomePhone = tbHomePhone.Text;
        //Телефон рабочий
        representative.WorkPhone = tbWorkPhone.Text;

        statement.Representative = representative;
      }
      else
      {
        statement.Representative = null;
      }

      _statementService.TrimStatementData(statement);

      //сохранение изменений в сессию
      if (setCurrentStatement)
      {
        CurrentStatement = statement;
      }
    }

    /// <summary>
    /// Проверка доступности элементов редактирования для ввода
    /// </summary>
    public override void CheckIsRightToEdit()
    {
      List<Concept> propertyList = GetPropertyListForCheckIsRightToEdit();

      //ФИО, тип представителя, Телефон домашний, Телефон рабочий
      tbLastName.Enabled = tbFirstName.Enabled = tbMiddleName.Enabled = ddlRelationType.Enabled = tbHomePhone.Enabled = tbWorkPhone.Enabled =
        _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.Representative));

      //Документ УДЛ
      documentUDL.Enable(_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.Representative.Document)));
    }

    #endregion

    #region Events handlers

    protected void Page_Init(object sender, EventArgs e)
    {
      _statementService = ObjectFactory.GetInstance<IStatementService>();

      if (!IsPostBack)
      {
        FillInsuredRelation();
        FillDocTypeUDL();
      }
    }

    /// <summary>
    /// Установка фокуса на контрол при смене шага
    /// </summary>
    public override void SetDefaultFocus()
    {
      documentUDL.SetFocusFirstControl();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
			if (!IsPostBack)
			{
				documentUDL.AddNumberAttribute("onblur", "SetValuesByUdl();");
			}
      if (IsPostBack)
      {
        var st = CurrentStatement;
        MoveDataFromGui2Object(ref st);
      }
    }

    #endregion

    #region Private methods

    private void FillInsuredRelation()
    {
      foreach (var c in _statementService.GetNsiRecords(Oid.Отношениекзастрахованномулицу))
        ddlRelationType.Items.Add(new ListItem(c.Name, c.Id.ToString()));
    }

    private void FillDocTypeUDL()
    {
      documentUDL.FillDocumentTypeDdl(_statementService.GetDocumentTypeForRepresentative().
        Select(x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray(), null);
    }

    #endregion

    #region Validators

    /// <summary>
    /// The validate last name.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateLastName(object sender, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.Representative.LastName));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvLastName.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate first name.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateFirstName(Object source, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.Representative.FirstName));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvFirstName.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate middle name.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateMiddleName(object sender, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.Representative.MiddleName));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvMiddleName.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate relation type.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateRelationType(Object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.Representative.RelationType.Id));
    }

    /// <summary>
    /// The validate doc type.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateDocument(object source, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.Representative.Document));
      }
      catch (LogicalControlException exception)
      {
        args.IsValid = documentUDL.cvDocument.IsValid = false;
        documentUDL.cvDocument.Text = exception.GetAllMessages();
      }
    }

    public bool Validate()
    {
      var args = new ServerValidateEventArgs(string.Empty, true);

      ValidateLastName(cvLastName, args);
      if (!args.IsValid)
        return false;

      ValidateFirstName(cvFirstName, args);
      if (!args.IsValid)
        return false;

      ValidateMiddleName(cvMiddleName, args);
      if (!args.IsValid)
        return false;
      
      ValidateDocument(cvDocumentType, args);
      if (!args.IsValid)
        return false;

      return true;
    }

    #endregion
  }
}