// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorTemporaryCertificateNumberInRange.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator temporary certificate number in range.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The validator temporary certificate number in range.
  /// </summary>
  public class ValidatorTemporaryCertificateNumberInRange : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorTemporaryCertificateNumberInRange"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorTemporaryCertificateNumberInRange(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Complex, sessionFactory)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorTemporaryCertificateNumberInRange;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <exception cref="FaultTemporaryCertificateNumberInRangeException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      if (statement.PointDistributionPolicy == null || statement.PointDistributionPolicy.Parent == null)
      {
        return;
      }

      if (string.IsNullOrEmpty(statement.NumberTemporaryCertificate))
      {
        return;
      }

      var smoId = statement.PointDistributionPolicy.Parent.Id;
      var tempNum = int.Parse(statement.NumberTemporaryCertificate);
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var range =
        session.QueryOver<RangeNumber>()
               .Where(x => x.Smo.Id == smoId)
               .And(x => x.RangelFrom <= tempNum)
               .And(x => tempNum <= x.RangelTo)
               .Take(1)
               .RowCount();

      if (range == 0)
      {
        throw new FaultTemporaryCertificateNumberInRangeException();
      }
    }

    #endregion
  }
}