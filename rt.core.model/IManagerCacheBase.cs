// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerCacheBase.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  /// <summary>
  /// The ManagerCacheBase interface.
  /// </summary>
  public interface IManagerCacheBase
  {
    #region Public Methods and Operators

    /// <summary>
    ///   �������� ���, ����� ��� ��������� ��������� ������ ����� � ���� � �� � ����.
    /// </summary>
    void Refresh();

    #endregion
  }
}