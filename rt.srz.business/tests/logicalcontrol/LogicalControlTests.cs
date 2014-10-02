// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogicalControlTests.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The logical control tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.tests.logicalcontrol
{
  #region references

  using System.Linq;

  using NUnit.Framework;

  using rt.core.business.tests;
  using rt.srz.business.interfaces.logicalcontrol;
  using rt.srz.business.manager;
  using rt.srz.business.manager.logicalcontrol;
  using rt.srz.business.manager.rightedit;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The logical control tests.
  /// </summary>
  public class LogicalControlTests : BusinessTestsBase
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The external flc test.
    /// </summary>
    [Test]
    public void ExternalFlcTest()
    {
      var checkFatory = ObjectFactory.GetInstance<ICheckManager>();
      var statement = ObjectFactory.GetInstance<IStatementManager>().GetAll(1).Single();

      checkFatory.CheckStatement(statement, CheckLevelEnum.External);
    }

    /// <summary>
    ///   The external flc test.
    /// </summary>
    [Test]
    public void t()
    {
      var res = ObjectFactory.GetAllInstances<IStatementRightToEdit>();

      var checkFatory = ObjectFactory.GetInstance<IStatementRightToEditManager>();
    }

    #endregion
  }
}