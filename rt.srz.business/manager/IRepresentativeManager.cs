// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepresentativeManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface RepresentativeManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.srz.model.srz;

  /// <summary>
  ///   The interface RepresentativeManager.
  /// </summary>
  public partial interface IRepresentativeManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������� ������ �� ������ � ����� ���������
    /// </summary>
    /// <param name="number">
    /// </param>
    /// <param name="series">
    /// </param>
    /// <returns>
    /// The <see cref="Representative"/>.
    /// </returns>
    Representative GetRepresentativeContactInfoByUdl(string number, string series);

    #endregion
  }
}