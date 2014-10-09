// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface SettingManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.srz.model.srz;

  /// <summary>
  ///   The interface SettingManager.
  /// </summary>
  public partial interface ISettingManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ��������� � ���� ��������� � ��� ��� ����� �������� ��������� �������� ������� ����������
    /// </summary>
    /// <param name="className">
    /// ��� ����������
    /// </param>
    void AddAllowChangeSetting(string className);

    /// <summary>
    /// ��������� � ���� ��������� �������� � ��� ��� � �� ���� ��������� � ������ ���������������� �����
    /// </summary>
    /// <param name="className">
    /// </param>
    void SaveCheckSetting(string className);

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> .
    /// </returns>
    Setting GetCurrentSetting(string name);

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
    /// ������� �� ���� ��������� � ��� ��� ����� �������� ��������� �������� ������� ����������
    /// </summary>
    /// <param name="className">
    /// ��� ����������
    /// </param>
    void RemoveAllowChangeSetting(string className);

    /// <summary>
    /// ������� ��������� �� ���� ������� ���� ����� ���������
    /// </summary>
    /// <param name="className">
    /// </param>
    void RemoveSetting(string className);

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

    #endregion

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/>.
    /// </returns>
    Setting GetSetting(string name);
  }
}