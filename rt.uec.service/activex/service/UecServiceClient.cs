// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UecServiceClient.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The uec client interop.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Windows.Forms;
using ProtoBuf.ServiceModel;
using rt.uec.model.Interfaces;
using rt.uec.service.serviceClient;

namespace rt.uec.service.activex.service
{
  


  #region references

  using rt.core.model.client;
  using rt.uec.model.enumerations;

  #endregion

  /// <summary>
  /// The uec client interop.
  /// </summary>
  [ComVisible(true)]
  [Guid("40AEE9C8-D365-4F14-9262-B2398872A5FD")]
  public class UecServiceClient : IUecServiceClient
  {
    #region Fields

    /// <summary>
    ///   ������
    /// </summary>
    private WcfProxy proxy;

    private string url;

    #endregion

    #region Properties

    /// <summary>
    ///   ������
    /// </summary>
    protected IUecService Service
    {
      get
      {
        if (proxy == null || proxy.State != CommunicationState.Opened)
        {
          if (proxy != null)
          {
            proxy.Dispose();
          }

          var path = Assembly.GetExecutingAssembly().Location + ".config";
          
          var canalFactory = new CustomClientChannel(path);
          proxy = new WcfProxy(canalFactory.Endpoint.Binding, new EndpointAddress(url));
          proxy.Endpoint.Behaviors.Add(new TokenMessageInspector());
          proxy.Endpoint.Behaviors.Add(new ProtoEndpointBehavior());
          proxy.Open();
        }

        return proxy.Service;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ��������� ���������� � ��� �������
    /// </summary>
    /// <param name="token">
    ///   ��������������� �����
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool OpenConnection(string token)
    {
      var split = token.Split('=');
      url = split[1];

      TokenMessageInspector.TokenFunc = () => new Token
      {
        Signature = string.Format("{0}=", split[0])
      };
      return true;
    }

    /// <summary>
    /// ��������� ����������
    /// </summary>
    public void CloseConnection()
    {
      proxy.Close();
      TokenMessageInspector.TokenFunc = () => null;
    }

    /// <summary>
    /// ���������� ���� �����������
    /// </summary>
    /// <param name="pcName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="version">
    /// ������ �����������
    /// </param>
    /// <param name="type">
    /// ��� �����������
    /// </param>
    /// <returns>
    /// ���� �����������
    /// </returns>
    public long GetCertificateKey(string pcName, int version, int type, ref byte[] key)
    {
      key = Service.GetCertificateKey(pcName, version, type);
      return 0;
    }

    /// <summary>
    /// ���������� ������� ��� ������������
    /// </summary>
    /// <param name="pcName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <returns>
    /// ������ ��� ������������
    /// </returns>
    public long GetCurrentCryptographyType(string pcName, ref CryptographyType type)
    {
      type = (CryptographyType)Service.GetCurrentCryptographyType(pcName);
      return 0;
    }

    /// <summary>
    /// ���������� ��� �������� ���  ������
    /// </summary>
    /// <param name="pcName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    public long GetCurrentReaderName(string pcName, ref string name)
    {
      try
      {
        name = Service.GetCurrentReaderName(pcName);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }

      return 0;
    }

    /// <summary>
    /// ���������� ��������� ����������������
    /// </summary>
    /// <param name="type"></param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public long GetProtocolSettings(ProtocolSettingsEnum type, ref string value)
    {
      value = Service.GetProtocolSettings(type.GetHashCode());
      return 0;
    }

    #endregion
  }
}