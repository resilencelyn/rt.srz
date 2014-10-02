// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICheckStatement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The CheckObject interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.interfaces.logicalcontrol
{
  #region references

  using System;
  using System.Linq.Expressions;

  using rt.srz.business.manager.logicalcontrol;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The CheckObject interface.
  /// </summary>
  public interface ICheckStatement
  {
    #region Public Properties

    /// <summary>
    ///   Gets the expression.
    /// </summary>
    Expression<Func<Statement, object>> Expression { get; }

    /// <summary>
    ///   The level.
    /// </summary>
    CheckLevelEnum Level { get; }

    /// <summary>
    ///   The level description.
    /// </summary>
    string LevelDescription { get; }

    /// <summary>
    /// Название для отображения проверки 
    /// </summary>
    string Caption { get; }

    /// <summary>
    /// Проверять или нет свойство
    /// </summary>
    bool CheckRequired { get; }

    /// <summary>
    /// Можно ли включать\отключать свойство
    /// </summary>
    bool AllowChange { get; }

    /// <summary>
    /// Видимость проверки в списке на странице ( не учитывается если есть права на отображение пункта меню установка - в этом случае отображаются все)
    /// </summary>
    bool Visible { get; }

    /// <summary>
    /// для грида чтобы исползовать в кастве ключа
    /// </summary>
    string ClassName { get; }

    /// <summary>
    /// Номер записи
    /// </summary>
    int RecordNumber { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void CheckObject(Statement statement);

    #endregion
  }
}