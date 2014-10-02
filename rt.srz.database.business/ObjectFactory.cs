//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business
{
  using rt.srz.database.business.interfaces.pseudonymization;
  using rt.srz.database.business.pseudonymization;
  using rt.srz.database.business.standard;

  /// <summary>
  /// Фабрика объектов
  /// </summary>
  public static class ObjectFactory
  {
    /// <summary>
    /// Менеджер псевдонимизации
    /// </summary>
    private static readonly IPseudonymizationManager pseudonymizationManager = null;

    /// <summary>
    /// Менеждер стандартной псеводоминимизации
    /// </summary>
    private static readonly StandardPseudonymizationManager standardPseudonymizationManager = null;
    
    #region Конструктор
    static ObjectFactory()
    {
      pseudonymizationManager = new PseudonymizationManager(GetWriteFields());
      standardPseudonymizationManager = new StandardPseudonymizationManager();
      standardPseudonymizationManager.Initialize();
    }
    #endregion

    /// <summary>
    /// Возвращает менеждер псевдонимизации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IPseudonymizationManager GetPseudonymizationManager()
    {
      return pseudonymizationManager;
    }

     /// <summary>
    /// Возвращает менеждер псевдонимизации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static StandardPseudonymizationManager GetStandardPseudonymizationManager()
    {
      return standardPseudonymizationManager;
    }

    /// <summary>
    /// Создает набор писателей полей
    /// </summary>
    /// <returns></returns>
    private static IWriteField[] GetWriteFields()
    {
      return new IWriteField[] { new WriteLastNameField(), new WriteFirstNameField(), new WriteMiddleNameField(), 
        new WriteBirthdayField(), new WriteBirtplaceField(), new WriteSnilsField(), 
        new WriteDocumentTypeField(), new WriteDocumentSeriesField(), new WriteDocumentNumberField(),  
        new WritePolisTypeField(), new WritePolisSeriesField(), new WritePolisNumberField(),
        new WriteOkatoField(), new WriteAddress1StreetField(), new WriteAddress1HouseField(), new WriteAddress1RoomField(), 
        new WriteAddress2StreetField(), new WriteAddress2HouseField(), new WriteAddress2RoomField()};
    }
  }
}
