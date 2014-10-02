// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataParserTest.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The algoritm test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.test
{
  #region references

  using NUnit.Framework;

  using rt.core.business.tests;

  #endregion

  /// <summary>
  ///   The algoritm test.
  /// </summary>
  [TestFixture]
  public class DataParserTest : BusinessTestsBase
  {
    #region Public Methods and Operators

    /// <summary>
    /// The check insured person data parse.
    /// </summary>
    [Test]
    public void CheckInsuredPersonDataParse()
    {
      // const string s = @"1-2-DF27|1|6|СНИЛС|0144051599(nerd) 
      // 1-2-DF2B|4|8|Номер полиса ОМС|7758330835001234| 
      // 1-2-5F20|0|26|ФИО|Кузнецов Александр Иванов.| 
      // 1-2-DF2D|0|8|Фамилия|Кузнецов| 
      // 1-2-DF2E|0|9|Имя|Александр| 
      // 1-2-DF2F|0|8|Отчество|Иванович| 
      // 1-2-DF23|0|80|Адрес эмитента|119333, Российская Федерация, город Москва, улица Тимура Фрунзе, 11, строение 15|
      // 1-2-5F2B|4|4|Дата рождения|19730309| 
      // 1-2-DF24|0|61|Место рождения|Российская Федерация, Ставропольский край, город Железноводск|
      // 1-2-5F35|3|1|Пол|М|";
      // InsuredPersonDatum data = DataParser.ParseInsuredPersonData(s);
      // Assert.AreEqual(data.LastName, "Кузнецов");
      // Assert.AreEqual(data.FirstName, "Александр");
      // Assert.AreEqual(data.MiddleName, "Иванович");
      // Assert.AreEqual(data.Birthday2, "19730309");
      // Assert.AreEqual(data.Birthday, new DateTime(1973, 3, 9));
      // Assert.AreEqual(data.Birthplace, "Российская Федерация, Ставропольский край, город Железноводск");
      // Assert.AreEqual(data.Gender.Id, Sex.Sex1);
    }

    /// <summary>
    /// The check medical insurance parse.
    /// </summary>
    [Test]
    public void CheckMedicalInsuranceParse()
    {
      // const string s = @"3-1-0|4|7|ОГРН страховщика|1027739815245| 
      // 3-1-7|4|3|ОКАТО субъекта РФ|79000| 
      // 3-1-10|4|4|Дата страхования|20100910 
      // 3-1-14|4|4|Дата окончания страхования|20120917|";

      // MedicalInsurance data = DataParser.ParseMedicalInsurance(s);

      // Assert.AreEqual(data.DateFrom, new DateTime(2010, 9, 10));
      // Assert.AreEqual(data.DateTo, new DateTime(2012, 9, 17));
      // Assert.NotNull(data.Smo);
    }

    #endregion
  }
}