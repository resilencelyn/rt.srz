// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UirManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The class UirManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Criterion;

  using NLog;

  using rt.srz.business.manager.cache;
  using rt.srz.model.interfaces.service.uir;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;

  using StructureMap;

  using Document = rt.srz.model.srz.Document;

  #endregion

  /// <summary>
  ///   The class UirManager.
  /// </summary>
  public class UirManager : IUirManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get med ins state.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/> .
    /// </returns>
    public Response GetMedInsState(Request request)
    {
      var rq = request.UIRRequest;

      try
      {
        var specFormat = DocumentNumSeparator.SeparateSpecFormat(rq.Document.DocIdent);
        var statement = new Statement
                        {
                          InsuredPersonData =
                            new InsuredPersonDatum
                            {
                              FirstName = rq.FullName.FirstName,
                              LastName = rq.FullName.FamilyName,
                              MiddleName = rq.FullName.MiddleName,
                              Birthday = rq.Birth.BirthDate,
                              Birthplace = rq.Birth.BirthPlace
                            },
                          DocumentUdl = new Document
                                        {
                                          Series = specFormat[0],
                                          Number = specFormat[1]
                                        }
                        };
        if (rq.InsDate != null)
        {
          statement.MedicalInsurances = new[] { new MedicalInsurance { DateFrom = rq.InsDate.Value } };
        }

        return new Response
               {
                 UIRResponse =
                   new UIRResponse
                   {
                     UIRQueryResponse = ResponseMaping(statement),
                     Ack = Ack.AA.ToString()
                   }
               };
      }
      catch (Exception ex)
      {
        return new Response
               {
                 UIRResponse =
                   new UIRResponse
                   {
                     UIRQueryResponse = null,
                     Ack = Ack.AE.ToString(),
                     Err = new[] { new Err { ErrText = ex.Message } }
                   }
               };
      }
    }

    /// <summary>
    /// The get med ins state 2.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/> .
    /// </returns>
    public Response GetMedInsState2(Request2 request)
    {
      var rq = request.UIRRequest2;

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      try
      {
        var polisType =
          session.QueryOver<Concept>()
                 .Where(f => f.Name.Lower() == rq.PolicyType.Lower() || f.ShortName.Lower() == rq.PolicyType.Lower())
                 .List()
                 .SingleOrDefault();
        var polisSeria = DocumentNumSeparator.SeparateSpecFormat(rq.PolicyNumber)[0];
        var polisNumber = DocumentNumSeparator.SeparateSpecFormat(rq.PolicyNumber)[1];

        var statement = new Statement
                        {
                          InsuredPersonData =
                            new InsuredPersonDatum
                            {
                              FirstName = rq.FullName.FirstName,
                              LastName = rq.FullName.FamilyName,
                              MiddleName = rq.FullName.MiddleName,
                              Birthday = rq.Birth.BirthDate,
                              Birthplace = rq.Birth.BirthPlace
                            },
                          MedicalInsurances =
                            session.QueryOver<Organisation>()
                                   .Inner.JoinQueryOver(j => j.Parent)
                                   .Where(f => f.Code == rq.InsRegion)
                                   .List()
                                   .Select(
                                           m =>
                                           new MedicalInsurance
                                           {
                                             PolisType = polisType,
                                             PolisSeria = polisSeria,
                                             PolisNumber = polisNumber,
                                             Smo = m
                                           })
                                   .ToList()
                        };
        if (rq.InsDate != null)
        {
          statement.MedicalInsurances[0].DateFrom = rq.InsDate.Value;
        }

        return new Response
               {
                 UIRResponse =
                   new UIRResponse
                   {
                     UIRQueryResponse = ResponseMaping(statement),
                     Ack = Ack.AA.ToString()
                   }
               };
      }
      catch (Exception ex)
      {
        return new Response
               {
                 UIRResponse =
                   new UIRResponse
                   {
                     UIRQueryResponse = null,
                     Ack = Ack.AE.ToString(),
                     Err = new[] { new Err { ErrText = ex.Message } }
                   }
               };
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get insured persons by keys.
    /// </summary>
    /// <param name="keys">
    /// The keys.
    /// </param>
    /// <returns>
    /// The <see cref="IList{InsuredPerson}"/>.
    /// </returns>
    private IList<InsuredPerson> GetInsuredPersonsByKeys(IEnumerable<SearchKey> keys)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // поиск по ключам
      if (!keys.Any())
      {
        return null;
      }

      var query = QueryOver.Of<SearchKey>();
      query.WhereRestrictionOn(x => x.KeyValue)
           .IsIn(keys.Select(y => y.KeyValue).ToList())
           .Select(x => x.InsuredPerson.Id);

      return session.QueryOver<InsuredPerson>().WithSubquery.WhereProperty(x => x.Id).In(query).List();
    }

    /// <summary>
    /// The insured persons by keis.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{InsuredPerson}"/>.
    /// </returns>
    private IEnumerable<InsuredPerson> InsuredPersonsByKeis(Statement statement)
    {
      // Считаем ключи поиска
      // Расчет стандартных ключей
      IList<SearchKey> standardKeys;
      try
      {
        standardKeys = ObjectFactory.GetInstance<ISearchKeyManager>().CalculateStandardKeys(statement);
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().Error(ex);
        throw new StandardSearchKeyCalculationException();
      }

      // Расчет пользовательских ключей
      IList<SearchKey> userKeys = new List<SearchKey>();

      ////try
      ////{
      ////    // Получаем текущий ТФОМС
      ////    var tfoms =
      ////        ObjectFactory.GetInstance<IOrganisationCacheManager>()
      ////            .GetBy(x => x.Code == " todo окато запроса" && x.Oid.Id == Oid.Tfoms)
      ////            .First();

      ////    // Получаем все пользовательские ключи подлежащие пересчету
      ////    var keyTypeList =
      ////        ObjectFactory.GetInstance<ISessionFactory>()
      ////            .GetCurrentSession()
      ////            .QueryOver<SearchKeyType>()
      ////            .Where(
      ////                x =>
      ////                    x.IsActive && x.OperationKey.Id == OperationKey.FullScanAndSaveKey)
      ////            .List();

      ////    // Cчитаем пользовательские ключи
      ////    userKeys = ObjectFactory.GetInstance<ISearchKeyManager>().CalculateUserKeys(
      ////        keyTypeList,
      ////        statement.InsuredPersonData,
      ////        statement.DocumentUdl,
      ////        statement.Address,
      ////        statement.Address2,
      ////        statement.MedicalInsurances,
      ////        tfoms.Okato);
      ////}
      ////catch (Exception ex)
      ////{
      ////    throw new UserSearchKeyCalculationException();
      ////}
      var keys = standardKeys.Union(userKeys);

      return GetInsuredPersonsByKeys(keys);
    }

    /// <summary>
    /// The response maping.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="UIRResponseUIRQueryResponse[]"/>.
    /// </returns>
    private UIRResponseUIRQueryResponse[] ResponseMaping(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var organisationManager = ObjectFactory.GetInstance<IOrganisationCacheManager>();

      // поднимаем все страховки по InsurepPerson.PeriodInsurance.MedicalInsured
      // и мапим это все в ответ
      InsuredPerson person = null;

      return
        session.QueryOver<MedicalInsurance>()
               .JoinAlias(x => x.InsuredPerson, () => person)
               .WhereRestrictionOn(x => person.Id)
               .IsIn(InsuredPersonsByKeis(statement).Select(m => m.Id).ToArray())
               .List()
               .Select(
                       ins =>
                       new UIRResponseUIRQueryResponse
                       {
                         Insurance =
                           new Insurance
                           {
                             InsType =
                               conceptManager.GetById(ins.PolisType.Id)
                                             .Code,
                             InsRegion =
                               organisationManager.GetById(ins.Smo.Id)
                                                  .Parent.Code,
                             MedInsCompanyId =
                               organisationManager.GetById(ins.Smo.Id)
                                                  .Code,
                             StartDate = ins.DateFrom,
                             EndDate = ins.DateTo,
                             InsId =
                               DocumentNumSeparator.SpecFormat(ins.PolisSeria, ins.PolisNumber)
                           },
                         Person =
                           new Person
                           {
                             MainENP = ins.InsuredPerson.MainPolisNumber,
                             RegionalENP = ins.Statement.NumberPolicy
                           }
                       })
               .ToArray();
    }

    #endregion
  }
}