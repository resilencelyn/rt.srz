// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserTests.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The user tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.tests
{
  using System;

  using AutoMapper;

  using NUnit.Framework;

  using rt.core.business.manager;

  using StructureMap;

  /// <summary>
  /// The user tests.
  /// </summary>
  public partial class UserTests
  {
    /// <summary>
    /// The test IsAdmin.
    /// </summary>
    [Test]
    public void TestAdminIsAdmin()
    {
      // arrange
      var userManager = ObjectFactory.GetInstance<IUserManager>();

      // action
      var user = userManager.GetById(new Guid("01000000-0000-0000-0000-000000000000"));

      // assert
      Assert.AreEqual(user.IsAdmin, true);
    }

    /// <summary>
    /// The test IsAdmin.
    /// </summary>
    [Test]
    public void TestNotAdminIsAdmin()
    {
      // arrange
      var userManager = ObjectFactory.GetInstance<IUserManager>();

      // action
      var user = userManager.GetById(new Guid("82000000-0000-0000-0000-000000000000"));

      // assert
      Assert.AreEqual(user.IsAdmin, false);
    }
  }
}