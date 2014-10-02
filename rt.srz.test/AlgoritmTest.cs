// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlgoritmTest.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The algoritm test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.test
{
  #region references

  using System;

  using NUnit.Framework;

  using rt.srz.business.configuration.algorithms;
  using rt.srz.model.algorithms;

  #endregion

  /// <summary>
  ///   The algoritm test.
  /// </summary>
  [TestFixture]
  public class AlgoritmTest
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate end date.
    /// </summary>
    [Test]
    public void CalculateEndDate()
    {
      var dateTo = DateTymeHelper.CalculateEnPeriodWorkingDay(new DateTime(2012, 12, 28), 30);
    }

    /// <summary>
    ///   The check bad enp.
    /// </summary>
    [Test]
    public void CheckBadEnp()
    {
      var r = EnpChecker.CheckIdentifier("7856130840000018");
      Assert.AreEqual(false, r);
    }

    /// <summary>
    ///   The check bad snils.
    /// </summary>
    [Test]
    public void CheckBadSnils()
    {
      var r = SnilsChecker.CheckIdentifier("03596739301");
      Assert.AreEqual(false, r);
    }

    /// <summary>
    ///   The check enp.
    /// </summary>
    [Test]
    public void CheckEnp()
    {
      var r = EnpChecker.CheckIdentifier("7856130840000017");
      Assert.AreEqual(true, r);
    }

    /// <summary>
    ///   The check snils.
    /// </summary>
    [Test]
    public void CheckEnpFaset()
    {
      var r = EnpChecker.CheckBirthdayAndGender("7856510844000446", new DateTime(1984, 3, 5), true);
      Assert.IsTrue(r);
    }

    /// <summary>
    ///   The check snils.
    /// </summary>
    [Test]
    public void CheckSnils()
    {
      var r = SnilsChecker.CheckIdentifier("03596739300");
      Assert.AreEqual(true, r);
    }

    /// <summary>
    ///   The check snils.
    /// </summary>
    [Test]
    public void TestParsePolis()
    {
      
    }

    #endregion
  }
}