// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListenerFolder.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������������� ��������� ������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.interfaces.import
{
  using Quartz;

  /// <summary>
  ///   ������������� ��������� ������
  /// </summary>
  public interface IListenerFolder : IJob
  {
    #region Public Properties

    /// <summary>
    ///   ���� ��������
    /// </summary>
    string InputPath { get; }

    #endregion
  }
}