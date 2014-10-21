// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewStatementSelection.aspx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Web;
  using System.Web.Services;
  using System.Web.UI;

  using rt.core.model.interfaces;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using rt.srz.ui.pvp.Controllers;
  using rt.srz.ui.pvp.Templates;

  using StructureMap;

  #endregion

  /// <summary>
  ///   Класс страницы
  /// </summary>
  public partial class NewStatementSelection : Page
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get category.
    /// </summary>
    /// <param name="citizenshipId">
    /// The citizenship id.
    /// </param>
    /// <param name="withoutCitizenship">
    /// The without citizenship.
    /// </param>
    /// <param name="isRefugee">
    /// The is refugee.
    /// </param>
    /// <param name="strBirthDate">
    /// The str Birth Date.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    [WebMethod]
    public static List<ConceptDto> GetCategory(
      int citizenshipId, 
      bool withoutCitizenship, 
      bool isRefugee, 
      string strBirthDate)
    {
      var age = GetAge(strBirthDate);

      return
        ObjectFactory.GetInstance<IStatementService>()
                     .GetCategoryByCitizenship(citizenshipId, withoutCitizenship, isRefugee, age)
                     .Select(x => new ConceptDto { Id = x.Id, Name = x.Name })
                     .ToList();
    }

    /// <summary>
    /// Возвращает текущий документ УДЛ, хранящийся в сессии
    /// </summary>
    /// <returns>
    /// The <see cref="DocumentUdl"/>.
    /// </returns>
    [WebMethod(EnableSession = true)]
    public static DocumentUdl GetCurrentDocumentUdl()
    {
      var session = HttpContext.Current.Session;
      var statement = session[SessionConsts.CCurrentStatement] != null
                        ? (Statement)session[SessionConsts.CCurrentStatement]
                        : null;
      var document = new DocumentUdl();
      if (statement != null)
      {
        document.DocumentType = statement.DocumentUdl.DocumentType != null
                                  ? statement.DocumentUdl.DocumentType.ToString()
                                  : "-1";
        document.DocumentSeries = statement.DocumentUdl.Series;
        document.DocumentNumber = statement.DocumentUdl.Number;
        document.DocumentIssuer = statement.DocumentUdl.IssuingAuthority;
        document.DocumentIssueDate = statement.DocumentUdl.DateIssue.HasValue
                                       ? statement.DocumentUdl.DateIssue.Value.ToShortDateString()
                                       : null;
      }
      return document;
    }

    /// <summary>
    /// The get document types residency.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    [WebMethod]
    public static List<ConceptDto> GetDocumentResidencyTypes(int categoryId)
    {
      return
        ObjectFactory.GetInstance<IStatementService>()
                     .GetDocumentResidencyTypeByCategory(categoryId)
                     .Select(x => new ConceptDto { Id = x.Id, Name = x.Name })
                     .ToList();
    }

    /// <summary>
    /// The get document types.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <param name="strBirthDate">
    /// The str birth date.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    [WebMethod]
    public static List<ConceptDto> GetDocumentTypes(int categoryId, string strBirthDate)
    {
      var age = GetAge(strBirthDate);

      return
        ObjectFactory.GetInstance<IStatementService>()
                     .GetDocumentTypeByCategory(categoryId, age)
                     .Select(x => new ConceptDto { Id = x.Id, Name = x.Name })
                     .ToList();
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода имени
    /// </summary>
    /// <param name="prefixText">
    /// The prefix Text.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    [WebMethod]
    public static List<string> GetFirstNameAutoComplete(string prefixText, int count)
    {
      return IntellisenseController.GetFirstNameAutoComplete(prefixText, count);
    }

    /// <summary>
    /// Возвращает идентификатор пола по идентификатору из AutoComplete
    /// </summary>
    /// <param name="autoCompleteId">
    /// The auto Complete Id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [WebMethod]
    public static string GetGenderByAutoCompleteId(Guid autoCompleteId)
    {
      var regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      var ac = regulatoryService.GetAutoComplete(autoCompleteId);
      if (ac != null)
      {
        return ac.Gender.Id.ToString(CultureInfo.InvariantCulture);
      }

      return string.Empty;
    }

    /// <summary>
    /// Обслуживает Intellisense КЛАДР
    /// </summary>
    /// <param name="prefixText">
    /// The prefix Text.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <param name="contextKey">
    /// The context Key.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    [WebMethod]
    public static List<string> GetKladrList(string prefixText, int count, string contextKey)
    {
      return IntellisenseController.GetKladrList(prefixText, count, contextKey);
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода отчества
    /// </summary>
    /// <param name="prefixText">
    /// The prefix Text.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <param name="contextKey">
    /// The context Key.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    [WebMethod]
    public static List<string> GetMiddleNameAutoComplete(string prefixText, int count, string contextKey)
    {
      return IntellisenseController.GetMiddleNameAutoComplete(prefixText, count, contextKey);
    }

    /// <summary>
    /// Возвращает контактную информацию по документу удл, найденному по номеру и серии
    /// </summary>
    /// <param name="kladrId">
    /// The kladr Id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [WebMethod]
    public static string GetPostcodeByKladrId(Guid kladrId)
    {
      var kladrService = ObjectFactory.GetInstance<IAddressService>();
      var kladr = kladrService.GetAddress(kladrId);
      if (kladr != null && kladr.Index != null)
      {
        return kladr.Index.ToString();
      }

      return string.Empty;
    }

    /// <summary>
    /// Возвращает контактную информацию по документу удл, найденному по номеру и серии
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="series">
    /// The series.
    /// </param>
    /// <returns>
    /// The <see cref="RepresentativeEx"/>.
    /// </returns>
    [WebMethod]
    public static RepresentativeEx GetRepresentativeContactInfoByUdl(string number, string series)
    {
      var representative = IntellisenseController.GetRepresentativeContactInfoByUdl(number, series);
      if (representative != null)
      {
        // связано с ошибками сериализации через JavaScriptSerializer
        return new RepresentativeEx(representative);
      }

      return null;
    }

    /// <summary>
    /// Возвращает идентификатор представителя по идентификатору из AutoComplete
    /// </summary>
    /// <param name="autoCompleteId">
    /// The auto Complete Id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [WebMethod]
    public static string GetRepresentativeGenderByAutoCompleteId(Guid autoCompleteId)
    {
      var regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      var ac = regulatoryService.GetAutoComplete(autoCompleteId);
      if (ac != null)
      {
        switch (ac.Gender.Id)
        {
          case Sex.Sex1:
            return RelationType.Father.ToString(CultureInfo.InvariantCulture);
          case Sex.Sex2:
            return RelationType.Mother.ToString(CultureInfo.InvariantCulture);
        }
      }

      return string.Empty;
    }

    #endregion

    #region Methods

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
      if (Page.Master is AuthentificatedPage)
      {
        ((AuthentificatedPage)Page.Master).SetMenuAvailability();
      }
    }

    /// <summary>
    /// The get age.
    /// </summary>
    /// <param name="strBirthDate">
    /// The str birth date.
    /// </param>
    /// <returns>
    /// The <see cref="TimeSpan"/>.
    /// </returns>
    private static TimeSpan GetAge(string strBirthDate)
    {
      DateTime? birthDate = null;
      if (!string.IsNullOrEmpty(strBirthDate))
      {
        DateTime tempDate;
        if (DateTime.TryParse(strBirthDate, out tempDate))
        {
          birthDate = tempDate;
        }

        if (birthDate == null)
        {
          if (DateTime.TryParse("01.01." + strBirthDate, out tempDate))
          {
            birthDate = tempDate;
          }
        }
      }

      // 1 год = 365.242199 суток * 15 лет = 5113,390786 суток
      var age = new TimeSpan(5114, 0, 0, 0);
      if (birthDate.HasValue)
      {
        age = DateTime.Now - birthDate.Value;
      }

      return age;
    }

    #endregion
  }
}