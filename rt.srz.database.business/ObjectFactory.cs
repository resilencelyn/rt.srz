// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectFactory.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Фабрика объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business
{
  using rt.srz.database.business.interfaces.pseudonymization;
  using rt.srz.database.business.pseudonymization;
  using rt.srz.database.business.standard;

  /// <summary>
  ///   Фабрика объектов
  /// </summary>
  public static class ObjectFactory
  {
    #region Static Fields

    /// <summary>
    ///   Менеджер псевдонимизации
    /// </summary>
    private static readonly IPseudonymizationManager pseudonymizationManager;

    /// <summary>
    ///   Менеждер стандартной псеводоминимизации
    /// </summary>
    private static readonly StandardPseudonymizationManager standardPseudonymizationManager;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the <see cref="ObjectFactory"/> class.
    /// </summary>
    static ObjectFactory()
    {
      pseudonymizationManager = new PseudonymizationManager(GetWriteFields());
      standardPseudonymizationManager = new StandardPseudonymizationManager();
      standardPseudonymizationManager.Initialize();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Возвращает менеждер псевдонимизации
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="IPseudonymizationManager"/>.
    /// </returns>
    public static IPseudonymizationManager GetPseudonymizationManager()
    {
      return pseudonymizationManager;
    }

    /// <summary>
    /// Возвращает менеждер псевдонимизации
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="StandardPseudonymizationManager"/>.
    /// </returns>
    public static StandardPseudonymizationManager GetStandardPseudonymizationManager()
    {
      return standardPseudonymizationManager;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Создает набор писателей полей
    /// </summary>
    /// <returns>
    /// The <see cref="IWriteField[]"/>.
    /// </returns>
    private static IWriteField[] GetWriteFields()
    {
      return new IWriteField[]
             {
               new WriteLastNameField(), new WriteFirstNameField(), new WriteMiddleNameField(), new WriteBirthdayField(), 
               new WriteBirtplaceField(), new WriteSnilsField(), new WriteDocumentTypeField(), 
               new WriteDocumentSeriesField(), new WriteDocumentNumberField(), new WritePolisTypeField(), 
               new WritePolisSeriesField(), new WritePolisNumberField(), new WriteOkatoField(), 
               new WriteAddress1StreetField(), new WriteAddress1HouseField(), new WriteAddress1RoomField(), 
               new WriteAddress2StreetField(), new WriteAddress2HouseField(), new WriteAddress2RoomField()
             };
    }

    #endregion
  }
}