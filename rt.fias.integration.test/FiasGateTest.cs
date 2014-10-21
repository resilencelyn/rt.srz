// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiasGateTest.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.integration.test
{
  using System;

  using NUnit.Framework;

  using rt.core.model.interfaces;
  using rt.srz.services.client.services;

  /// <summary>
  /// The fias gate test.
  /// </summary>
  [TestFixture]
  public class FiasGateTest
  {
    /// <summary>
    /// The test get first level by tfoms.
    /// </summary>
    [Test]
    public void TestGetFirstLevelByTfoms()
    {
      try
      {
        // arrange
        var statementService = new AddressClient();

        // act
        var k = statementService.GetFirstLevelByTfoms("45000000000");

        // assert
        Assert.IsNotNull(k);
        Assert.AreEqual(k.Name, "Москва");
      }
      catch (Exception ex)
      {
        Assert.Fail("Тест завершился крахом. {0}", ex);
      }
    }

    /// <summary>
    /// The test get address.
    /// </summary>
    [Test]
    public void TestGetAddress()
    {
      try
      {
        // arrange
        var statementService = new AddressClient();

        // act
        var k = statementService.GetAddress(new Guid("0C5B2444-70A0-4932-980C-B4DC0D3F02B5"));

        // assert
        Assert.IsNotNull(k);
        Assert.AreEqual(k.Name, "Москва");
      }
      catch (Exception ex)
      {
        Assert.Fail("Тест завершился крахом. {0}", ex);
      }
    }

    /// <summary>
    /// The test get address list.
    /// </summary>
    [Test]
    public void TestGetAddressList()
    {
      try
      {
        // arrange
        var statementService = new AddressClient();

        // act
        var list = statementService.GetAddressList(new Guid("EA5E1E3D-2848-486A-BD48-AFBA7419356E"), "П%а%л%а%т%", null);

        // assert
        Assert.IsNotNull(list);
        Assert.AreEqual(list.Count, 2);
      }
      catch (Exception ex)
      {
        Assert.Fail("Тест завершился крахом. {0}", ex);
      }
    }

    [Test]
    public void TestGetStructureAddress()
    {
      try
      {
        // arrange
        var statementService = new AddressClient();

        // act
        var structureAddress = statementService.GetStructureAddress(new Guid("CDDF07A5-9617-4911-A6A4-122032151907"));

        // assert
        Assert.IsNotNull(structureAddress);
        Assert.AreEqual(structureAddress.Subject, "Белгородская обл");
        Assert.AreEqual(structureAddress.Area, "Красногвардейский р-н");
        Assert.AreEqual(structureAddress.Town, "Палатово с");
        Assert.AreEqual(structureAddress.Code, "3101200005702");
        Assert.AreEqual(structureAddress.OkatoRn, "14242000000");
        Assert.IsNull(structureAddress.City);
        Assert.IsNull(structureAddress.Street);
      }
      catch (Exception ex)
      {
        Assert.Fail("Тест завершился крахом. {0}", ex);
      }
    }

    /// <summary>
    /// The test get unstructure address.
    /// </summary>
    [Test]
    public void TestGetUnstructureAddress()
    {
      try
      {
        // arrange
        var statementService = new AddressClient();

        // act
        var unstructureAddress = statementService.GetUnstructureAddress(new Guid("CDDF07A5-9617-4911-A6A4-122032151907"));

        // assert
        Assert.AreEqual(unstructureAddress, "Белгородская обл.,Красногвардейский р-н.,Палатово с.,");
      }
      catch (Exception ex)
      {
        Assert.Fail("Тест завершился крахом. {0}", ex);
      }
    }

    /// <summary>
    /// The test hierarchy build.
    /// </summary>
    [Test]
    public void TestHierarchyBuild()
    {
      try
      {
        // arrange
        var statementService = new AddressClient();

        // act
        var hierarchyBuild = statementService.HierarchyBuild(new Guid("CDDF07A5-9617-4911-A6A4-122032151907"));

        // assert
        Assert.AreEqual(hierarchyBuild, "30547212-8edb-461e-a814-2101f0e72f5f;ea5e1e3d-2848-486a-bd48-afba7419356e;cddf07a5-9617-4911-a6a4-122032151907");
      }
      catch (Exception ex)
      {
        Assert.Fail("Тест завершился крахом. {0}", ex);
      }
    }
  }
}