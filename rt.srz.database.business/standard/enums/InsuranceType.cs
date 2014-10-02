namespace rt.srz.database.business.standard.enums
{
  /// <summary>
  /// The insurance type.
  /// </summary>
  public enum InsuranceType : byte
  {
    /// <summary>
    /// "�" - ����� ��� ������� ������� (����� ���, �������� �� ���������� � ���� ������ �� ���)
    /// </summary>
    Obsolete = 1,

    /// <summary>
    /// "�" - ��������� ������������� (��������� �������������, �������������� ���������� ������ ������������� ������������ �����������)
    /// </summary>
    Temporary = 5,

    /// <summary>
    /// "�" - ����� ��� ������ ������� (����� ��� ������� �������, �������� � ������������ � ������������ ������ �� ���)
    /// </summary>
    Effective2011 = 11,

    /// <summary>
    ///   "�" - ����������� ����� ��� ������� �������
    /// </summary>
    Electronic2011 = 12,

    /// <summary>
    ///   "�" - ������������� ����������� �����
    /// </summary>
    UniversalElectronicCard = 14,

    /// <summary>
    /// The unknown.
    /// </summary>
    Unknown = 255,
  }
}