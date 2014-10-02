namespace rt.srz.database.business.standard.enums
{
  /// <summary>
  /// The ward unidimensional.
  /// </summary>
  public enum WardUnidimensional
  {
    /// <summary>
    /// The forward.
    /// </summary>
    Forward = 0x01,

    /// <summary>
    /// The back.
    /// </summary>
    Back = 0x02,

    // ��� �����������
    /// <summary>
    /// The both.
    /// </summary>
    Both = Forward | Back,

    // �� ������ �����������
    /// <summary>
    /// The none.
    /// </summary>
    None = 0x00,
  }
}