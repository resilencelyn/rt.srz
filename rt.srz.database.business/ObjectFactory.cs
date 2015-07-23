// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectFactory.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������� ��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business
{
  using rt.srz.database.business.interfaces.pseudonymization;
  using rt.srz.database.business.pseudonymization;
  using rt.srz.database.business.standard;

  /// <summary>
  ///   ������� ��������
  /// </summary>
  public static class ObjectFactory
  {
    #region Static Fields

    /// <summary>
    ///   �������� ���������������
    /// </summary>
    private static readonly IPseudonymizationManager PseudonymizationManager;

    /// <summary>
    ///   �������� ����������� ������������������
    /// </summary>
    private static readonly StandardPseudonymizationManager StandardPseudonymizationManager;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the <see cref="ObjectFactory"/> class.
    /// </summary>
    static ObjectFactory()
    {
      PseudonymizationManager = new PseudonymizationManager(GetWriteFields());
      StandardPseudonymizationManager = new StandardPseudonymizationManager();
      StandardPseudonymizationManager.Initialize();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ���������� �������� ���������������
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="IPseudonymizationManager"/>.
    /// </returns>
    public static IPseudonymizationManager GetPseudonymizationManager()
    {
      return PseudonymizationManager;
    }

    /// <summary>
    /// ���������� �������� ���������������
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="standard.StandardPseudonymizationManager"/>.
    /// </returns>
    public static StandardPseudonymizationManager GetStandardPseudonymizationManager()
    {
      return StandardPseudonymizationManager;
    }

    #endregion

    #region Methods

    /// <summary>
    /// ������� ����� ��������� �����
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