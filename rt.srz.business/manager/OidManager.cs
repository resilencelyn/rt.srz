// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OidManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The OidManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The OidManager.
  /// </summary>
  public partial class OidManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get category by citizenship.
    /// </summary>
    /// <param name="citizenshipId">
    /// The citizenship id.
    /// </param>
    /// <param name="isnotCitizenship">
    /// The isnot citizenship.
    /// </param>
    /// <param name="isrefugee">
    /// The isrefugee.
    /// </param>
    /// <param name="age">
    /// The age.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Concept> GetCategoryByCitizenship(
      int citizenshipId, 
      bool isnotCitizenship, 
      bool isrefugee, 
      TimeSpan age)
    {
      var res =
        ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.Категориязастрахованноголица);

      // Гражданин РФ
      if (citizenshipId == Country.RUS)
      {
        if (age < new TimeSpan(5114, 0, 0, 0))
        {
          return res.Where(x => x.Id == CategoryPerson.TerritorialRf).ToList();
        }

        return res.Where(x => x.Id == CategoryPerson.WorkerRf || x.Id == CategoryPerson.TerritorialRf).ToList();
      }

      // Без гражданства
      if (isnotCitizenship)
      {
        return
          res.Where(
                    x =>
                    x.Id == CategoryPerson.WorkerStatelessPermanently
                    || x.Id == CategoryPerson.TerritorialStatelessPermanently
                    || x.Id == CategoryPerson.WorkerStatelessTeporary
                    || x.Id == CategoryPerson.TerritorialStatelessTeporary).ToList();
      }

      // Без гражданства
      if (isrefugee)
      {
        return
          res.Where(x => x.Id == CategoryPerson.WorkerRefugee || x.Id == CategoryPerson.TerritorialRefugee).ToList();
      }

      return
        res.Where(
                  x =>
                  x.Id == CategoryPerson.WorkerAlienPermanently || x.Id == CategoryPerson.WorkerAlienTeporary
                  || x.Id == CategoryPerson.TerritorialAlienPermanently
                  || x.Id == CategoryPerson.TerritorialAlienTeporary).ToList();
    }

    /// <summary>
    /// The get document residency type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    public IList<Concept> GetDocumentResidencyTypeByCategory(int categoryId)
    {
      var res = ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.ДокументУдл);
      switch (categoryId)
      {
          // постоянно проживающий в Российской Федерации иностранный гражданин
        case CategoryPerson.WorkerAlienPermanently:
        case CategoryPerson.TerritorialAlienPermanently:
          return res.Where(x => x.Id == DocumentType.DocumentType11).OrderBy(x => x.Relevance).ToList();

          // временно проживающий в Российской Федерации иностранный гражданин
        case CategoryPerson.WorkerAlienTeporary:
        case CategoryPerson.TerritorialAlienTeporary:
          return res.Where(x => x.Id == DocumentType.DocumentType23).OrderBy(x => x.Relevance).ToList();

          // постоянно проживающее в Российской Федерации лицо без гражданства
        case CategoryPerson.WorkerStatelessPermanently:
        case CategoryPerson.TerritorialStatelessPermanently:
          return res.Where(x => x.Id == DocumentType.DocumentType22).OrderBy(x => x.Relevance).ToList();
      }

      return new List<Concept>();
    }

    /// <summary>
    ///   The get document type for registration document
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{Concept}" />.
    /// </returns>
    public IList<Concept> GetDocumentTypeForRegistrationDocument()
    {
      var res = ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.ДокументУдл);
      return res.Where(x => x.Id == DocumentType.CertificationRegistration).ToList();
    }

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{Concept}" />.
    /// </returns>
    public IList<Concept> GetDocumentTypeForRepresentative()
    {
      var res = ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.ДокументУдл);
      return
        res.Where(
                  x =>
                  x.Id == DocumentType.BirthCertificateRf || x.Id == DocumentType.DocumentType9
                  || x.Id == DocumentType.DocumentType10 || x.Id == DocumentType.DocumentType12
                  || x.Id == DocumentType.DocumentType13 || x.Id == DocumentType.PassportRf
                  || x.Id == DocumentType.DocumentType20 || x.Id == DocumentType.DocumentType21
                  || x.Id == DocumentType.DocumentType23 || x.Id == DocumentType.DocumentType22
                  || x.Id == DocumentType.DocumentType11).OrderBy(x => x.Relevance).ToList();
    }

    /// <summary>
    /// The get document type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <param name="age">
    /// The age.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    public IList<Concept> GetDocumentUdlTypeByCategory(int categoryId, TimeSpan age)
    {
      var res = ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.ДокументУдл);
      switch (categoryId)
      {
        case CategoryPerson.WorkerRf:
        case CategoryPerson.TerritorialRf:
        {
          // 1 год = 365.242199 суток * 14 лет = 5113,390786 суток
          if (age < new TimeSpan(5114, 0, 0, 0))
          {
            return
              res.Where(x => x.Id == DocumentType.BirthCertificateRf || x.Id == DocumentType.DocumentType24)
                 .OrderBy(x => x.Relevance)
                 .ToList();
          }

          if (age < new TimeSpan(5144, 0, 0, 0))
          {
            return
              res.Where(
                        x =>
                        x.Id == DocumentType.BirthCertificateRf || x.Id == DocumentType.DocumentType24
                        || x.Id == DocumentType.PassportRf || x.Id == DocumentType.DocumentType13)
                 .OrderBy(x => x.Relevance)
                 .ToList();
          }

          return
            res.Where(x => x.Id == DocumentType.PassportRf || x.Id == DocumentType.DocumentType13)
               .OrderBy(x => x.Relevance)
               .ToList();
        }

        case CategoryPerson.WorkerRefugee:
        case CategoryPerson.TerritorialRefugee:
          return
            res.Where(
                      x =>
                      x.Id == DocumentType.DocumentType10 || x.Id == DocumentType.DocumentType12
                      || x.Id == DocumentType.DocumentType25).OrderBy(x => x.Relevance).ToList();

        case CategoryPerson.WorkerAlienPermanently:
        case CategoryPerson.TerritorialAlienPermanently:
          return
            res.Where(x => x.Id == DocumentType.DocumentType9 || x.Id == DocumentType.DocumentType21)
               .OrderBy(x => x.Relevance)
               .ToList();

        case CategoryPerson.WorkerAlienTeporary:
        case CategoryPerson.TerritorialAlienTeporary:
          return
            res.Where(x => x.Id == DocumentType.DocumentType9 || x.Id == DocumentType.DocumentType21)
               .OrderBy(x => x.Relevance)
               .ToList();

        case CategoryPerson.WorkerStatelessPermanently:
        case CategoryPerson.TerritorialStatelessPermanently:
          return res.Where(x => x.Id == DocumentType.DocumentType11).OrderBy(x => x.Relevance).ToList();

        case CategoryPerson.WorkerStatelessTeporary:
        case CategoryPerson.TerritorialStatelessTeporary:
          var documentUdlTypeByCategory1 =
            res.Where(x => x.Id == DocumentType.DocumentType23).OrderBy(x => x.Relevance).ToList();
          var doc = res.Where(x => x.Id == DocumentType.DocumentType22).OrderBy(x => x.Relevance).First();
          doc = new DocumentType { Code = doc.Code, Name = doc.Name, ShortName = doc.ShortName, Id = -doc.Id };
          documentUdlTypeByCategory1.Add(doc);
          return documentUdlTypeByCategory1;
      }

      return new List<Concept>();
    }

    /// <summary>
    /// Возвращает список типов полиса в зависимости от причины обращения
    /// </summary>
    /// <param name="causeFilling">
    /// The cause Filling.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    public IList<Concept> GetFormManufacturingByCauseFilling(int causeFilling)
    {
      var res = ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.Формаизготовленияполиса);
      switch (causeFilling)
      {
        case CauseReinsurance.Choice:
          return
            res.Where(x => x.Id != PolisType.В && x.Id != PolisType.К && x.Id != PolisType.С)
               .OrderBy(x => x.Relevance)
               .ToList();
        default:
          return res.Where(x => x.Id != PolisType.В && x.Id != PolisType.С).OrderBy(x => x.Relevance).ToList();
      }
    }

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="nsiId">
    /// The nsi Id.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Concept> GetNsiRecords(string nsiId)
    {
      return
        ObjectFactory.GetInstance<IConceptCacheManager>()
                     .GetBy(x => x.Oid.Id == nsiId)
                     .OrderBy(x => x.Relevance)
                     .ThenBy(x => x.Name)
                     .ToList();
    }

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="nsiId">
    /// The nsi Id.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Concept> GetNsiRecords(IEnumerable<string> nsiId)
    {
      return ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(nsiId);
    }

    /// <summary>
    /// The get type polis by form manufacturing.
    /// </summary>
    /// <param name="formManufacturing">
    /// The form manufacturing.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    public IList<Concept> GetTypePolisByFormManufacturing(int formManufacturing)
    {
      var res = ObjectFactory.GetInstance<IConceptCacheManager>().GetBy(x => x.Oid.Id == Oid.Формаизготовленияполиса);
      return formManufacturing == PolisType.К
               ? res.Where(x => x.Id == PolisType.П || x.Id == PolisType.К).OrderBy(x => x.Relevance).ToList()
               : res.Where(x => x.Id == PolisType.П || x.Id == PolisType.Э).OrderBy(x => x.Relevance).ToList();
    }

    #endregion
  }
}