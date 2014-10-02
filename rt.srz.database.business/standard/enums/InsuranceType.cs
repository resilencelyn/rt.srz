namespace rt.srz.database.business.standard.enums
{
  /// <summary>
  /// The insurance type.
  /// </summary>
  public enum InsuranceType : byte
  {
    /// <summary>
    /// "С" - полис ОМС старого образца (полис ОМС, выданный до вступления в силу Закона об ОМС)
    /// </summary>
    Obsolete = 1,

    /// <summary>
    /// "В" - временное свидетельство (временное свидетельство, подтверждающее оформление полиса обязательного медицинского страхования)
    /// </summary>
    Temporary = 5,

    /// <summary>
    /// "П" - полис ОМС нового образца (полис ОМС единого образца, выданный в соответствии с требованиями Закона об ОМС)
    /// </summary>
    Effective2011 = 11,

    /// <summary>
    ///   "Э" - электронный полис ОМС единого образца
    /// </summary>
    Electronic2011 = 12,

    /// <summary>
    ///   "У" - универсальная электронная карта
    /// </summary>
    UniversalElectronicCard = 14,

    /// <summary>
    /// The unknown.
    /// </summary>
    Unknown = 255,
  }
}