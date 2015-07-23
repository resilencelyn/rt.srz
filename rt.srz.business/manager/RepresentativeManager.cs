// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepresentativeManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The RepresentativeManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System.Linq;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The RepresentativeManager.
  /// </summary>
  public partial class RepresentativeManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получает данные по номеру и серии документа
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="series">
    /// The series.
    /// </param>
    /// <returns>
    /// The <see cref="Representative"/>.
    /// </returns>
    public Representative GetRepresentativeContactInfoByUdl(string number, string series)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      if (string.IsNullOrEmpty(number))
      {
        return null;
      }

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      Document doc = null;
      var statement =
        session.QueryOver<Statement>()
               .JoinAlias(x => x.DocumentUdl, () => doc)
               .Where(x => doc.Number == number && series == doc.Series)
               .And(x => x.IsActive)
               .Take(1)
               .List()
               .FirstOrDefault();

      return statement != null
               ? new Representative
                 {
                   FirstName = statement.InsuredPersonData.FirstName, 
                   LastName = statement.InsuredPersonData.LastName, 
                   MiddleName = statement.InsuredPersonData.MiddleName, 
                   RelationType =
                     conceptManager.GetById(
                                            statement.InsuredPersonData.Gender.Id == Sex.Sex1
                                              ? RelationType.Father
                                              : RelationType.Mother), 
                   HomePhone = statement.ContactInfo.HomePhone, 
                   WorkPhone = statement.ContactInfo.WorkPhone, 
                   Document = statement.DocumentUdl
                 }
               : null;
    }

    #endregion
  }
}