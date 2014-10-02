// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SertificateUecManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Linq;
using NHibernate;
using rt.srz.business.manager.cache;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;

#endregion

namespace rt.srz.business.manager
{
  /// <summary>
  ///   The SertificateUecManager.
  /// </summary>
  public partial class SertificateUecManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ���� �����������
    /// </summary>
    /// <param name="workstationName">
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
    public byte[] GetCertificateKey(string workstationName, int version, int type)
    {
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();
      var workstationManager = ObjectFactory.GetInstance<IWorkstationManager>();
      var sertificateUecManager = ObjectFactory.GetInstance<ISertificateUecManager>();

      // ���� ����������� ������ ������� �������
      var workstation =
        workstationManager.GetBy(
          x => x.Name == workstationName && x.PointDistributionPolicy.Id == user.PointDistributionPolicy.Id).FirstOrDefault();

      SertificateUec sert;
      if (workstation != null)
      {
        sert =
          sertificateUecManager
            .GetBy(x => x.IsActive && x.Version == version && x.Type.Id == type && x.Workstation.Id == workstation.Id)
            .FirstOrDefault();
        if (sert != null)
        {
          return sert.Key;
        }
      }

      // ���� ����������� ������ ���
      sert =
        sertificateUecManager
          .GetBy(x => x.IsActive && x.Version == version && x.Type.Id == type && x.Smo.Id == user.PointDistributionPolicy.Parent.Id)
          .FirstOrDefault();
      
      if (sert != null)
        return sert.Key;

      // ���� ���������� �����������
      sert =
        sertificateUecManager
          .GetBy(x => x.IsActive && x.Version == version && x.Type.Id == type && x.Smo.Id == null)
          .FirstOrDefault();
      
      return sert != null ? sert.Key : null;
    }

    /// <summary>
    /// The save sertificate key.
    /// </summary>
    /// <param name="workstationId">
    ///   The workstation Id. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="key">
    ///   The key. 
    /// </param>
    public void SaveWorkstationSertificateKey(Guid workstationId, short version, int type, byte[] key)
    {
      var sertificateUecManager = ObjectFactory.GetInstance<ISertificateUecManager>();
      var sert =
        sertificateUecManager.GetBy(x => x.IsActive && x.Version == version && x.Workstation.Id == workstationId && x.Type.Id == type).FirstOrDefault();
      SaveSertificate(sert, version, type, key, workstationId, null);
    }

    /// <summary>
    /// The save sertificate key.
    /// </summary>
    /// <param name="workstationId">
    ///   The workstation Id. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="hexKey">
    ///   The hex key. 
    /// </param>
    public void SaveWorkstationSertificateKey(Guid workstationId, short version, int type, string hexKey)
    {
      var key = Enumerable.Range(0, hexKey.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hexKey.Substring(x, 2), 16)).ToArray();
      SaveWorkstationSertificateKey((Guid)workstationId, version, type, key);
    }

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    ///   The smo id. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="hexKey">
    ///   The hex key. 
    /// </param>
    public void SaveSmoSertificateKey(Guid smoId, short version, int type, string hexKey)
    {
      var key = Enumerable
                  .Range(0, hexKey.Length)
                  .Where(x => x % 2 == 0).Select(x => Convert.ToByte(hexKey.Substring(x, 2), 16))
                  .ToArray();
      SaveSmoSertificateKey((Guid)smoId, version, type, key);
    }

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    ///   The smo id. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="key">
    ///   The key. 
    /// </param>
    public void SaveSmoSertificateKey(Guid smoId, short version, int type, byte[] key)
    {
      var sertificateUecManager = ObjectFactory.GetInstance<ISertificateUecManager>();
      Guid? tempSmoId = smoId != Guid.Empty ? (Guid?)smoId : null;
      var sert = sertificateUecManager.GetBy(x => x.IsActive && x.Version == version && x.Type.Id == type && x.Smo.Id == tempSmoId).FirstOrDefault();
      SaveSertificate(sert, version, type, key, null, smoId);
    }

    /// <summary>
    /// The save sertificate.
    /// </summary>
    /// <param name="sert">
    ///   The sert. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="key">
    ///   The key. 
    /// </param>
    /// <param name="workstationId">
    ///   The workstation id. 
    /// </param>
    /// <param name="smoId">
    ///   The smo id. 
    /// </param>
    private void SaveSertificate(SertificateUec sert, short version, int type, byte[] key, Guid? workstationId, Guid? smoId)
    {
      if (sert != null)
      {
        sert.Key = key;
        sert.InstallDate = DateTime.Now.Date;
      }
      else
      {
        var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();
        var workstationManager = ObjectFactory.GetInstance<IWorkstationManager>();
        var smoManager = ObjectFactory.GetInstance<IOrganisationManager>();
        sert = new SertificateUec
                 {
                   IsActive = true,
                   Key = key,
                   Type = conceptCacheManager.GetById(type),
                   Version = version,
                   Workstation = workstationId.HasValue ? workstationManager.GetById(workstationId.Value) : null,
                   Smo = smoId.HasValue ? smoManager.GetById(smoId.Value) : null,
                   InstallDate = DateTime.Now.Date
                 };
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        session.Save(sert);
        session.Flush();
      }
    }

    #endregion
  }
}