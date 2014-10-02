// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmcTerminal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;
using Pc.Policy.Smartcard;
using Pc.Policy.Smartcard.Crypto;
using Pc.Policy.Smartcard.Data;
using Pc.Policy.Smartcard.Pinpad;
using Pc.Policy.Smartcard.Shared;
using Pc.Shared.BER.Converter;
using Pc.Shared.Security.Crypto;
using Pc.Shared.Utils.Extensions;
using ProtoBuf.ServiceModel;
using rt.smc.model;
using SmartCard.PCSC;
using SmartCard.PCSC.Native;
using rt.smc.service.serviceClient;
using rt.uec.model.Interfaces;

#endregion

namespace rt.smc.service.activex
{
  using rt.core.model.client;

  /// <summary>
  ///   The elektropolis.
  /// </summary>
  [Guid("32B3D0CF-3031-40B1-B074-53495BE5D158")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  public class SmcTerminal : ISmcTerminal
  {
    /// <summary>
    ///   ������
    /// </summary>
    private WcfProxy<IUecService> proxy;

    private string url  ;

    /// <summary>
    ///   �������� ��������� ������������� �����
    /// </summary>
    public string ActiveToken { get; set; }

    /// <summary>
    ///   �������� ��������� ������������� ����-�����
    /// </summary>
    public string ActiveCardReader { get; set; }

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
          var canalFactory = new CustomClientChannel<IUecService>(path);
          proxy = new WcfProxy<IUecService>(canalFactory.Endpoint.Binding, new EndpointAddress(url));
          proxy.Endpoint.Behaviors.Add(new TokenMessageInspector());
          proxy.Endpoint.Behaviors.Add(new ProtoEndpointBehavior());
          proxy.Open();
        }

        return proxy.Service;
      }
    }

    /// <summary>
    /// ��������� ���������� � ��� �������
    /// </summary>
    /// <param name="token">
    ///   ��������������� �����
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [ComVisible(true)]
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
    [ComVisible(true)]
    public void CloseConnection()
    {
      TokenMessageInspector.TokenFunc = () => null;
    }

    /// <summary>
    /// The set card reader.
    /// </summary>
    /// <param name="cardReader">
    /// The card reader.
    /// </param>
    [ComVisible(true)]
    public void SetCardReader()
    {
      ActiveCardReader = Service.GetCurrentSmcReaderName(SystemInformation.ComputerName);
      ActiveToken = Service.GetCurrentSmcTokenReaderName(SystemInformation.ComputerName);
    }

    /// <summary>
    ///   ��������� ������ ��������� ����-������� � �������
    /// </summary>
    /// <returns> ������ ����-������� </returns>
    [ComVisible(false)]
    public List<string> GetCardReaders()
    {
      // ����� ������ ����-�������, ������������� � �������
      var manager = new PCSCReadersManager();
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);
      var cards = manager.OfType<ISCard>().ToList();
      var activeDevsName = new List<string>();
      foreach (var card in cards)
      {
        PolicySmartcardBase testConnect = null;
        try
        {
          testConnect = new PolicySmartcardBase(card);
        }
        catch (Exception)
        {
          testConnect = null;
        }
        finally
        {
          if (testConnect != null)
          {
            activeDevsName.Add(testConnect.BaseCard.ReaderName);
          }
        }
      }

      manager.ReleaseContext();
      return activeDevsName;
    }

    /// <summary>
    ///   ��������� ������ ��������� ������� � �������
    /// </summary>
    /// <returns> ������ ������� </returns>
    [ComVisible(false)]
    public List<string> GetRuTokens()
    {
      // ����� ������ �������, ������������� � �������
      var manager = new PCSCReadersManager();
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);
      var tokens = manager.OfType<ISCard>().Where(device => device.ReaderName.ToLower().Contains("tok")).ToList();
      var activeDevsName = new List<string>();
      foreach (var token in tokens)
      {
        PolicySmartcardBase testConnect = null;
        try
        {
          testConnect = new PolicySmartcardBase(token);
          testConnect.Connect();
        }
        catch
        {
          testConnect = null;
        }
        finally
        {
          if (testConnect != null)
          {
            activeDevsName.Add(testConnect.BaseCard.ReaderName);
            testConnect.Disconnect();
          }
        }
      }

      manager.ReleaseContext();
      return activeDevsName;
    }

    /// <summary>
    ///   ��������� ���������� � ����� (���, ����� �����, ������������� � �.�.)
    /// </summary>
    /// <returns> The <see cref="CardInfoStrings" /> . </returns>
    [ComVisible(true)]
    public CardInfoStrings GetCardInfo()
    {
      var manager = new PCSCReadersManager();

      // ������������ ���������� � �����-���� API
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

      // ����� ������ ������� � �������
      if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader))
      {
        // ��������� ������� ������
        var card = manager[ActiveCardReader];

        // �������� ������� ��� ������ � ������ ������ ���
        var policy = new PolicySmartcardBase(card);

        // ����������� � ����� ������ ���
        policy.Connect();

        // ������ ���������� � ����� ������ ��� ( ����������������� ������ )
        var cid = policy.GetCardID();

        // ������ ���������� � ����� ������ ��� ( ���������� � ���������� �� ������������� )
        var ccd = policy.GetICCD();

        CardInfoStrings cardInfo = null;
        if (cid != null)
        {
          cardInfo = new CardInfoStrings
                       {
                         CardHexNum = cid.SerialNumber.ToHexString(), 
                         CardType = cid.CardType, 
                         Vers = cid.CardVersion, 
                         InstitId = cid.InstitutionID.ToHexString(), 
                         AddInfo = cid.AdditionalInfo.ToHexString()
                       };
        }

        if (ccd != null)
        {
          cardInfo = cardInfo ?? new CardInfoStrings();
          cardInfo.IssuerCode = ccd.IssuerCode;
          cardInfo.IssuerData = ccd.IssuerData.ToHexString();
        }

        // ���������� �� ����� ������ ���
        policy.Disconnect();

        // ���������� �� �����-���� API
        manager.ReleaseContext();
        return cardInfo;
      }

      manager.ReleaseContext();
      return null;
    }

    /// <summary>
    ///   ��������� ���������� � ������� ���
    /// </summary>
    /// <returns> The <see cref="SmoInfoStrings" /> . </returns>
    [ComVisible(true)]
    public SmoInfoStrings GetCurrentSmo()
    {
      // ������ ���������� � ��������� ����� ������ ���
      var manager = new PCSCReadersManager();

      // ������������ ���������� � �����-���� API
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

      // ����� ������ ������� � �������
      if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader))
      {
        // ��������� ������� ������
        var card = manager[ActiveCardReader];

        // �������� ������� ��� ������ � ������ ������ ���
        var policy = new PolicySmartcardBase(card);

        // ����������� � ����� ������ ���
        policy.Connect();

        // ������ ���������� � ��������� ������ ���
        var smo = policy.GetCurrentSMOInformation();
        if (smo != null)
        {
          var smoInfo = new SmoInfoStrings
                          {
                            OgrnSmo = smo.OGRN, 
                            Okato = smo.OKATO, 
                            InsuranceStartDate =
                              FormatPolicyDate(smo.InsuranceStartDate, "�����������"), 
                            InsuranceEndDate =
                              FormatPolicyDate(smo.InsuranceExpireDate, "�� ����������")
                          };

          if (smo.EDS != null)
          {
            smoInfo.Eds = new Eds
                            {
                              Certificate = new X509Certificate2(smo.EDS.Certificate).Subject, 
                              Ecp = smo.EDS.Signature.ToHexString()
                            };

            try
            {
              smoInfo.Eds.StateEcp = smo.VerifyEDS() ? "�����" : "�������";
            }
            catch
            {
            }
          }

          policy.Disconnect();
          manager.ReleaseContext();
          return smoInfo;
        }

        // ���������� �� ����� ������ ���
        policy.Disconnect();

        // ���������� �� �����-���� API
        manager.ReleaseContext();
        return null;
      }

      manager.ReleaseContext();
      return null;
    }

    /// <summary>
    ///   ������ ���������� � ��������� ����� (��������������)
    /// </summary>
    /// <returns> The <see cref="OwnerInfo" /> . </returns>
    [ComVisible(true)]
    public OwnerInfo GetOwnerInfo()
    {
      // ������ ���������� � ��������� ����� ������ ���
      var manager = new PCSCReadersManager();
      try
      {
        // ������������ ���������� � �����-���� API
        manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

        // ����� ������ ������� � �������
        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader))
        {
          // ��������� ������� ������
          var card = manager[ActiveCardReader];

          // �������� ������� ��� ������ � ������ ������ ���
          var policy = new PolicySmartcardBase(card);

          // ����������� � ����� ������ ���
          policy.Connect();

          // ������ ���������� � ��������� ������ ���
          var ownerInfoCard = policy.GetOwnerInformation();
          if (ownerInfoCard != null)
          {
            var ownerInfo = new OwnerInfo
                              {
                                LastName = FormatPolicyText(ownerInfoCard.Identity_1, string.Empty),
                                FirstName = FormatPolicyText(ownerInfoCard.Identity_2, string.Empty),
                                MiddleName = FormatPolicyText(ownerInfoCard.Identity_3, string.Empty), 
                                Sex =
                                  ownerInfoCard.Sex == 1 ? "273" : ownerInfoCard.Sex == 2 ? "274" : string.Empty,
                                Birthday = FormatPolicyDate(ownerInfoCard.BirthDate, string.Empty),
                                BirthPlace = FormatPolicyText(ownerInfoCard.BirthPlace, string.Empty)
                              };

            if (ownerInfoCard.Citizenship != null)
            {
              ownerInfo.CitizenschipCode = FormatPolicyText(ownerInfoCard.Citizenship.CoutryCode, string.Empty);
              ownerInfo.CitizenschipName = FormatPolicyText(ownerInfoCard.Citizenship.CoutryCyrillicName, string.Empty);
            }

            ownerInfo.PolisNumber = ownerInfoCard.PolicyNumber;
            ownerInfo.PolisDateFrom = FormatPolicyDate(ownerInfoCard.IssueDate, string.Empty);
            ownerInfo.PolisDateTo = FormatPolicyDate(ownerInfoCard.ExpireDate, string.Empty);

            ownerInfo.Snils = FormatPolicyText(ownerInfoCard.SNILS, string.Empty);

            try
            {
              // ������ ��������� ������������ ����� ������ ���
              var sod = policy.GetSecurityInformation();
              ownerInfo.StateEcp = ownerInfoCard.VerifyEDS(sod) ? "�����" : "�������";
            }
            catch
            {
            }

            // ���������� �� ����� ������ ���
            policy.Disconnect();

            // ���������� �� �����-���� API
            manager.ReleaseContext();
            return ownerInfo;
          }

          // ���������� �� ����� ������ ���
          policy.Disconnect();

          // ���������� �� �����-���� API
          manager.ReleaseContext();
          return null;
        }

        manager.ReleaseContext();
        return null;
      }
      catch 
      {
        manager.ReleaseContext();
        throw;
      }
    }

    /// <summary>
    /// ����� ���
    /// </summary>
    /// <param name="ogrnSmo">
    /// ���� ��� 
    /// </param>
    /// <param name="okatoSmo">
    /// ����� ��� 
    /// </param>
    /// <returns>
    /// true - ���� �� ����������� 
    /// </returns>
    [ComVisible(true)]
    public bool ChangeSmo(string ogrnSmo, string okatoSmo, string dateFrom, string dateTo, string securityModulePin, string cardPin)
    {
      // ������ ���������� � ��������� ����� ������ ���
      var manager = new PCSCReadersManager();
      try
      {
        // ������������ ���������� � �����-���� API
        manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

        // ����� ������ ������� � �������
        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader) == false)
        {
          manager.ReleaseContext();
          return false;
        }

        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveToken) == false)
        {
          manager.ReleaseContext();
          return false;
        }

        // ��������� ������� ������
        var policyCard = manager[ActiveCardReader];
        var tokenCard = manager[ActiveToken];

        // �������� ������� ��� ������ � ������ ������ ���
        var policy = new PolicySmartcard(policyCard, tokenCard);

        // ����������� � ����� ������ ���
        policy.Connect();

        // �������������� � ������ ������ ������������
        // ���-��� ������ ������������ 12345678
        policy.Authentificate_Mutual(securityModulePin, true);
        var verifyArgs = new VerifyPINArguments
        {
          // ������ �������� ������������� ������� ������������� ���-���� ��� 
          // ������������ ���-���� � ������� �������� ����������. � ������ ��������� ������� ��������� � �������� false
          // ��������� ���������� ��������� 'PinValue'
          // ������������� ���-���� �������� ������ � ������, ���� ����� ������ ��� �����������
          // � ���������� � ���������� ������� �������� ����������.
          // ��������� ������� ������� �������� ���������� � ���������� ����� � ������� ������ 
          // 'PolicyPinpad.IsDeviceSupported(policy_card, false
          // �� ��������� ����������� � ����� ���� ����������� ���� ��������� �������� ����������
          UsePinpad = PolicyPinpad.IsDeviceSupported(policyCard, false), 

          // �������� ���-����. ������ �������� ������������ ������ ���� UsePinpad = false
          PinValue = "1234", 

          // ������ �� ����� ��� ������ ����� ���-���� ����������� �������� � ������ ������������� �������� ����������
          // ������������� ���������� �� ������� ������ VerifyPIN
          CancelHandler = null, 

          // ������ �� ����� ��� ��������� ������� ������� ������ �� �������� ����������
          // ����� �� ���������������� - ������������ ������ � ������ ������������� �������� ����������
          EnteredDigitHandler = key =>
                                  {
                                    // ���� ������, �����������
                                  }
        };
        
        // �������� ����
        policy.VerifyPIN(ref verifyArgs);

        // ������ ����
        DateTime dtFrom = DateTime.Now;
        DateTime.TryParse(dateFrom, out dtFrom);

        // ������ ����
        DateTime dtTo = DateTime.Now.AddYears(5);
        DateTime.TryParse(dateTo, out dtTo);
        
        // �������� ������� ������ ���
        var smo = new SMOInformation
                    {
                      OGRN = ogrnSmo, 
                      OKATO = okatoSmo,
                      InsuranceStartDate = dtFrom,
                      InsuranceExpireDate = dtTo, 
                      EDS = null /*��� ����� ���������������� ����� ����� �� ��������*/
                    };

        // [������������ ��� ������ ���]
        // �������� ��������� ������������ �������� ������������
        var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        // ����� ����������� � ������� ���������� �������
        var selected = X509Certificate2UI.SelectFromCollection(
          store.Certificates, 
          "�����������", 
          "�������� ���������� ��� ������������ ��� ���", 
          X509SelectionFlag.SingleSelection);
        if (selected.Count > 0)
        {
          // �������� ������� ������������ �������
          var signatureEncoder = new SignatureEncoder(selected[0]);

          // ������������ ���
          smo.EDS = new SMOInformationEDS
          {
            Signature = signatureEncoder.CreateSignHash(smo.EncodeBER()), // �������������� ������� � ������� BER-TLV
            Certificate = selected[0].Export(X509ContentType.Cert)
          };
        }

        // ������ ���������� � ����� ��� �� ����� ������ ���
        policy.CreateNewSMO(smo);

        // ���������� �� ����� ������ ���
        policy.Disconnect();

        // ���������� �� �����-���� API
        manager.ReleaseContext();
        
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    ///   The get smo history.
    /// </summary>
    /// <returns> The List. </returns>
    [ComVisible(false)]
    public List<SmoInfoStrings> GetSmoHistory()
    {
      const string pinCard = "5473";

      // ������ ���������� � ��������� ����� ������ ���
      var manager = new PCSCReadersManager();
      try
      {
        // ������������ ���������� � �����-���� API
        manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

        // ����� ������ ������� � �������
        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader) == false)
        {
          ////printf("���������� ������ �����-���� � ������ [{0}] �� ������� � �������.");
          manager.ReleaseContext();
          return null;
        }

        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveToken) == false)
        {
          ////printf("���������� ������ �����-���� � ������ [{0}] �� ������� � �������.");
          manager.ReleaseContext();
          return null;
        }

        // ��������� ������� ������
        var policyCard = manager[ActiveCardReader];
        var tokenCard = manager[ActiveToken];

        // �������� ������� ��� ������ � ������ ������ ���
        var policy = new PolicySmartcard(policyCard, tokenCard);

        // ����������� � ����� ������ ���
        policy.Connect();


        // �������������� � ������ ������ ������������
        policy.Authentificate_External(pinCard);
        policy.Authentificate_External(pinCard, true);

        // ������ ������� ���
        var historyList = policy.GetSMOHistory();
        var histSmos = new List<SmoInfoStrings>();
        historyList.ForEach(smo =>
                              {
                                if (smo != null)
                                {
                                  var smoInfo = new SmoInfoStrings
                                                  {
                                                    OgrnSmo = smo.OGRN, 
                                                    Okato = smo.OKATO, 
                                                    InsuranceStartDate =
                                                      FormatPolicyDate(
                                                        smo.InsuranceStartDate, "�����������"), 
                                                    InsuranceEndDate =
                                                      FormatPolicyDate(
                                                        smo.InsuranceExpireDate, "�� ����������"), 
                                                    Eds = smo.EDS == null
                                                            ? null
                                                            : new Eds
                                                                {
                                                                  Certificate =
                                                                    new X509Certificate2(smo.EDS.Certificate).Subject, 
                                                                  Ecp = smo.EDS.Signature.ToHexString()
                                                                }
                                                  };

                                  histSmos.Add(smoInfo);
                                }
                              });

        // ���������� �� ����� ������ ���
        policy.Disconnect();

        // ���������� �� �����-���� API
        manager.ReleaseContext();
        return histSmos;
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// The format policy date.
    /// </summary>
    /// <param name="date">
    /// The date. 
    /// </param>
    /// <param name="nullValue">
    /// The null_value. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    private string FormatPolicyDate(DateTime? date, string nullValue)
    {
      return date.HasValue == false ? nullValue : date.Value.ToString("dd.MM.yyyy");
    }

    /// <summary>
    /// The format policy text.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <param name="nullValue">
    /// The null value. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    private string FormatPolicyText(string value, string nullValue)
    {
      return string.IsNullOrEmpty(value) ? nullValue : value;
    }
  }
}