// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerCacheBase.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ManagerCacheBase interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  /// <summary>
  ///   The ManagerCacheBase interface.
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