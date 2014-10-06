// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementTests.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.tests
{
  #region references

  using System.Linq;

  using NUnit.Framework;

  using rt.srz.business.manager;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The statement tests.
  /// </summary>
  public partial class StatementTests
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The clone.
    /// </summary>
    [Test]
    public void Clone()
    {
      var manager = ObjectFactory.GetInstance<IStatementManager>();
      var res = manager.GetAll(1).Single();
      var clone = manager.CreateFromExample(res);
    }

    #endregion
  }
}