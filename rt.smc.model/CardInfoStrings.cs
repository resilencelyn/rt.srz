//-----------------------------------------------------------------------
// <copyright file="CardInfoStrings.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>The card info strings.</summary>
  [ComVisible(true)]
  public class CardInfoStrings
  {
    /// <summary>
    /// ����� �����
    /// </summary>
    public string CardHexNum { get; set; }

    /// <summary>
    /// ��� �����
    /// </summary>
    public byte CardType { get; set; }

    /// <summary>
    /// ������ �����
    /// </summary>
    public short Vers { get; set; }

    /// <summary>
    /// ������������� ����������
    /// </summary>
    public string InstitId { get; set; }

    /// <summary>
    /// �������������� ����������
    /// </summary>
    public string AddInfo { get; set; }

    /// <summary>
    /// ��� ������������� ���� �����
    /// </summary>
    public byte IssuerCode { get; set; }

    /// <summary>
    /// ������ � ������������� ���� �����
    /// </summary>
    public string IssuerData { get; set; }
  }
}