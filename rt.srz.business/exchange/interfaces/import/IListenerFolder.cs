// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListenerFolder.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ������������� ��������� ������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.business.exchange.interfaces.import
{
  using Quartz;

  /// <summary>
  ///   ������������� ��������� ������
  /// </summary>
  public interface IListenerFolder : IJob
  {
    /// <summary>
    ///   ���� ��������
    /// </summary>
    string InputPath { get; }
  }
}