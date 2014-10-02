// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardStep.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Web.UI;

  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The wizard step.
  /// </summary>
  public abstract class WizardStep : UserControl, IWizardStep
  {
    // <summary>
    // статус заявления
    // </summary>
    #region Properties

    protected Statement CurrentStatement
    {
      get
      {
        if (Session[SessionConsts.CCurrentStatement] != null)
        {
          return (Statement)Session[SessionConsts.CCurrentStatement];
        }

        if (Session[SessionConsts.CGuidStatementId] != null)
        {
          var statementId = (Guid)Session[SessionConsts.CGuidStatementId];
          var statementService = ObjectFactory.GetInstance<IStatementService>();
          var statement = statementService.GetStatement(statementId);
          if (statement != null)
          {
            return statement;
          }
        }

        return new Statement { Status = new Concept() };
      }

      set
      {
        Session[SessionConsts.CCurrentStatement] = value;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Проверка доступности элементов редактирования для ввода
    /// </summary>
    public abstract void CheckIsRightToEdit();

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
    public abstract void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true);

    /// <summary>
    /// Переносит данные из объекта в элементы на форме
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public abstract void MoveDataFromObject2GUI(Statement statement);

    /// <summary>
    ///   Установка фокуса на контрол при смене шага
    /// </summary>
    public abstract void SetDefaultFocus();

    #endregion

    #region Methods

    /// <summary>
    ///   The get property list for check is right to edit.
    /// </summary>
    /// <returns> The <see>
    ///                 <cref>List</cref>
    /// </see> . </returns>
    protected List<Concept> GetPropertyListForCheckIsRightToEdit()
    {
      var propertyList = new List<Concept>();
      if (CurrentStatement.Status != null)
      {
        propertyList.Add(CurrentStatement.Status);
      }
      else
      {
        propertyList.Add(new Concept { Id = StatusStatement.New });
      }

      if (CurrentStatement.CauseFiling != null)
      {
        propertyList.Add(CurrentStatement.CauseFiling);
      }
      return propertyList;
    }

    #endregion
  }
}