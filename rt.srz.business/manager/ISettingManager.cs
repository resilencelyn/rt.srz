// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface SettingManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System.ServiceModel;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface SettingManager.
  /// </summary>
  public partial interface ISettingManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetSettingCurrentUser(string nameSetting);

    /// <summary>
    /// The set setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    void SetSettingCurrentUser(string nameSetting, string value);

    /// <summary>
    /// ��������� � ���� ��������� �������� � ��� ��� � �� ���� ��������� � ������ ���������������� �����
    /// </summary>
    /// <param name="className"></param>
    void AddSetting(string className);

    /// <summary>
    /// ������� ��������� �� ���� ������� ���� ����� ���������
    /// </summary>
    /// <param name="className"></param>
    void RemoveSetting(string className);

    /// <summary>
    ///  ��������� � ���� ��������� � ��� ��� ����� �������� ��������� �������� ������� ����������
    /// </summary>
    /// <param name="className">��� ����������</param>
    void AddAllowChangeSetting(string className);

    /// <summary>
    ///  ������� �� ���� ��������� � ��� ��� ����� �������� ��������� �������� ������� ����������
    /// </summary>
    /// <param name="className">��� ����������</param>
    void RemoveAllowChangeSetting(string className);

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name. 
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> . 
    /// </returns>
    Setting GetSetting(string name);


    #endregion
  }
}