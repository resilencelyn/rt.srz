// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckDocumentProperty.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The check document property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq.Expressions;
  using System.Text;
  using System.Text.RegularExpressions;

  using NHibernate;

  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.regex;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The check document property.
  /// </summary>
  public abstract class CheckDocumentProperty : Check
  {
    private readonly Func<Statement, object> deleg;

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckDocumentProperty"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    /// <param name="expression">
    /// The expression. 
    /// </param>
    protected CheckDocumentProperty(ISessionFactory sessionFactory, Expression<Func<Statement, object>> expression)
      : base(CheckLevelEnum.Simple, sessionFactory, expression)
    {
      deleg = expression.Compile();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      try
      {
        var document = (Document)deleg(statement);
        if (document == null)
        {
          throw new FaultEmptyDocumentException();
        }

        var patterns = document.DocumentType.Description;
        var seriablok = RegexFactory.RegexDocomentSeriaBlok.Match(patterns);
        var seriaPattern = seriablok.Success ? seriablok.Value.Substring(8, seriablok.Value.Length - 9) : string.Empty;
        var numberblok = RegexFactory.RegexDocomentNumberBlok.Match(patterns);
        var numberPatterns = numberblok.Success
                               ? numberblok.Value.Substring(8, numberblok.Value.Length - 9)
                               : string.Empty;

        if (document.DocumentType.Id != DocumentType.CertificationRegistration)
        {
          // У документа регистрации нет серии
          if (!CheckByPattern(seriaPattern, document.Series))
          {
            throw new FaultDocumentSeriesNemberPatternException();
          }
        }

        if (string.IsNullOrEmpty(document.Number) || !CheckByPattern(numberPatterns, document.Number))
        {
          throw new FaultDocumentSeriesNemberPatternException();
        }

        // Пропускаем проверки если причина - "Заявление на выбор или замену СМО не подавалось"
        if ((statement.CauseFiling == null) || (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization))
        {
          if (string.IsNullOrEmpty(document.IssuingAuthority))
          {
            throw new FaultDocumentIssuingAuthorityEmptyException();
          }

          if (!document.DateIssue.HasValue)
          {
            throw new FaultDocumentDateIssueEmptyException();
          }

          if (DocumentType.IsDocExp(document.DocumentType.Id) && !document.DateExp.HasValue)
          {
            throw new FaultDocumentDateExpEmptyException();
          }
        }

        if (document.DateIssue.HasValue && document.DateIssue.Value > DateTime.Today)
        {
          throw new FaultDocumentDateIssueFutureException();
        }

        if (DocumentType.IsDocExp(document.DocumentType.Id) && statement.DateFiling.HasValue && document.DateExp.HasValue && document.DateExp.Value < statement.DateFiling.Value)
        {
          throw new FaultDocumentExpiriedException();
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultEmptyDocumentException();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Проверяет строку по шаблону
    /// </summary>
    /// <param name="pattern">
    /// Шаблон 
    /// </param>
    /// <param name="value">
    /// Строка для проверки 
    /// </param>
    /// <returns>
    /// true - если значение соответствует шаблону, иначе false 
    /// </returns>
    private bool CheckByPattern(string pattern, string value)
    {
      var sb = new StringBuilder("^");
      foreach (var p in pattern)
      {
        switch (p)
        {
          case '0':
            sb.Append("\\d?");
            break;
          case '9':
            sb.Append("\\d");
            break;
          case 'S':
            sb.Append("[\\w\\- ]?");
            break;
          case 'Б':
            sb.Append("[А-ЯЁ]");
            break;
          case 'R':
            sb.Append("[IVLXC]+");
            break;
          case ' ':
            sb.Append("\\s");
            break;
          case '-':
            sb.Append("-");
            break;
          default:
            return false;
        }
      }

      sb.Append("$");
      var reges = new Regex(sb.ToString(), RegexOptions.Singleline);
      if (value == null)
        value = string.Empty;
      return reges.IsMatch(value);
    }

    #endregion
  }
}