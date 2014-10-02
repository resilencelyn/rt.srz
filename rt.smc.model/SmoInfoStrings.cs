//-----------------------------------------------------------------------
// <copyright file="SmoInfoStrings.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>The smo info strings.</summary>
  [ComVisible(true)]
  public class SmoInfoStrings
  {
    /// <summary>
    /// ������ ���
    /// </summary>
    public Eds Eds { get; set; }

    /// <summary>
    /// ���� ���
    /// </summary>
    public string OgrnSmo { get; set; }

    /// <summary>
    /// ����� ���������� ���
    /// </summary>
    public string Okato { get; set; }

    /// <summary>
    /// ���� ������ �������� ���������
    /// </summary>
    public string InsuranceStartDate { get; set; }

    /// <summary>
    /// ���� ��������� �������� ���������
    /// </summary>
    public string InsuranceEndDate { get; set; }
  }
}
