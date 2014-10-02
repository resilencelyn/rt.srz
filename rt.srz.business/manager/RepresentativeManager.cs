// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepresentativeManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The RepresentativeManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NHibernate;
using System.Linq;
using rt.srz.business.manager.cache;
using rt.srz.model.dto;
using rt.srz.model.srz;
using StructureMap;
using rt.srz.model.srz.concepts;

namespace rt.srz.business.manager
{
  /// <summary>
  ///   The RepresentativeManager.
  /// </summary>
  public partial class RepresentativeManager
  {
    /// <summary>
    /// ѕолучает данные по номеру и серии документа
    /// </summary>
    /// <param name="number"></param>
    /// <param name="series"></param>
    /// <returns></returns>
    public Representative GetRepresentativeContactInfoByUdl(string number, string series)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      if (string.IsNullOrEmpty(number))
      {
        return null;
      }

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      Document doc = null;
      var statement = session.QueryOver<Statement>()
          .JoinAlias(x => x.DocumentUdl, () => doc)
          .Where(x => doc.Number == number && series == doc.Series)
          .And(x => x.IsActive)
          .Take(1).List().FirstOrDefault();

      return statement != null
               ? new Representative
                   {
                     FirstName = statement.InsuredPersonData.FirstName,
                     LastName = statement.InsuredPersonData.LastName,
                     MiddleName = statement.InsuredPersonData.MiddleName,
                     RelationType = conceptManager.GetById(statement.InsuredPersonData.Gender.Id == Sex.Sex1 ? RelationType.Father : RelationType.Mother),
                     HomePhone = statement.ContactInfo.HomePhone,
                     WorkPhone = statement.ContactInfo.WorkPhone,
                     Document = statement.DocumentUdl
                   }
               : null;
    }
  }
}