// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementHl7Manager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement hl 7 manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System.Collections.Generic;

  using rt.core.business.security.interfaces;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.HL7.person.messages;
  using rt.srz.model.HL7.person.target;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  using Document = rt.srz.model.HL7.person.target.Document;

  #endregion

  /// <summary>
  ///   The statement hl 7 manager.
  /// </summary>
  public class StatementHl7Manager : IStatementHl7Manager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get za 7.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="ZPI_ZA7"/>.
    /// </returns>
    public ZPI_ZA7 GetZa7(Statement statement)
    {
      var za7 = new ZPI_ZA7
                {
                  Zah = GetZah(statement), 
                  In1 = GetIn(statement), 
                  Nk1 = GetNk(statement), 
                  Znd = GetZnd(statement), 
                  Msh = GetMsh()
                };

      return za7;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get in.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="In1Card"/>.
    /// </returns>
    private In1Card GetIn(Statement statement)
    {
      var pvp = statement.PointDistributionPolicy;
      var smo = pvp.Parent;
      var personData = statement.InsuredPersonData;
      var residencyDocument = statement.ResidencyDocument;
      var polisEndDate = statement.ResidencyDocument != null && statement.ResidencyDocument.DateExp.HasValue
                           ? ConversionHelper.DateTimeToStringShort(statement.ResidencyDocument.DateExp.Value)
                           : string.Empty;

      var contact = statement.ContactInfo;

      var in1 = new In1Card();

      // IN1.1	SI	��	���������� ����� ��������
      in1.Id = "1";

      // IN1.2	CWE	��	������������� ����� �����������
      in1.PlanId = new PlanId { Id = "���", Oid = "1.2.643.2.40.5.100.72" };

      // IN1.3	CX	��	������������� �����������
      in1.CompanyId = new CompanyId { Id = smo.Ogrn, CompanyIdType = "NII" };

      // IN1.4	XON	���	������������ �����������
      in1.CompanyName = new CompanyName { Name = smo.FullName };

      // IN1.5	XAD	���	����� ���
      // in1.AddressSmo.Postcode = smo.Postcode;
      in1.AddressSmoInStr = smo.Address;

      // IN1.6	XPN	���	���������� ���� � ���
      ////in1.FioInSmo.Name = smo.FirstName;
      ////in1.FioInSmo.Otchestvo = smo.MiddleName;
      in1.FioInSmo.Surname.surname = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser().Fio;

      // IN1.7	XTN	���	���������� �������� ���
      in1.Phone.Phone = smo.Phone;

      // IN1.12	DT	��	���� ������ �������� ���������
      in1.DateBeginInsurence = statement.DateFiling.HasValue
                                 ? ConversionHelper.DateTimeToStringGoznak(statement.DateFiling.Value)
                                 : string.Empty;

      // IN1.13	DT	��	���� ��������� �������� ���������
      in1.DateEndInsurence = polisEndDate;

      // IN1.15	IS	��	��� ���������� �����������
      in1.CodeOfRegion = smo.Parent.Okato;

      // IN1.16	XPN	��	�������, ���, �������� 
      in1.FioList = new List<Fio>
                    {
                      new Fio(
                        new Surname(personData.FirstName), 
                        personData.LastName, 
                        personData.MiddleName, 
                        "L")
                    };

      // IN1.18	DTM	��	���� ��������
      in1.BirthDay = personData.Birthday.HasValue
                       ? ConversionHelper.DateTimeToStringGoznak(personData.Birthday.Value)
                       : string.Empty;

      var adr = statement.Address;
      var adr2 = statement.Address2 ?? adr;
      in1.AddressList = new List<AddressCard>
                        {
                          adr.IsHomeless.HasValue && adr.IsHomeless.Value
                            ? new AddressCard { IsHomeless = "1", AddressType = "L" }
                            : new AddressCard
                              {
                                StructureAddress =
                                  new StructureAddress
                                  {
                                    Building = adr.Housing, 
                                    Room =
                                      adr.Room.ToString(), 
                                    Street = adr.Street
                                  }, 
                                RegionName = adr.Subject, 
                                Region = adr.Okato, 
                                City = adr.City, 
                                Town = adr.Town, 
                                District = adr.Area, 
                                Building = adr.House, 
                                RegistrationDate =
                                  adr.DateRegistration.HasValue
                                    ? ConversionHelper.DateTimeToStringGoznak(
                                                                              adr
                                                                                .DateRegistration
                                                                                .Value)
                                    : string.Empty, 
                                CountryCode = "RUS", 
                                AddressType = "L", 
                                Postcode = adr.Postcode
                              }
                        };

      if (!(adr.IsHomeless.HasValue && adr.IsHomeless.Value))
      {
        in1.AddressList.Add(
                            new AddressCard
                            {
                              StructureAddress =
                                new StructureAddress
                                {
                                  Building = adr2.Housing, 
                                  Room = adr2.Room.ToString(), 
                                  Street = adr2.Street
                                }, 
                              RegionName = adr2.Subject, 
                              Region = adr2.Okato, 
                              City = adr2.City, 
                              Town = adr2.Town, 
                              RegistrationDate =
                                adr2.DateRegistration.HasValue
                                  ? ConversionHelper.DateTimeToStringGoznak(adr2.DateRegistration.Value)
                                  : string.Empty, 
                              District = adr2.Area, 
                              Building = adr2.House, 
                              CountryCode = "RUS", 
                              AddressType = "H", 
                              Postcode = adr2.Postcode
                            });
      }

      // IN1.35	IS	���	��� ���������
      in1.InsuranceType = statement.FormManufacturing != null ? statement.FormManufacturing.Code : string.Empty;

      // IN1.36	ST	��	����� ���������� �������������
      in1.InsuranceSerNum = statement.NumberTemporaryCertificate;

      // IN1.151 ���� ������ ���������� �������������
      if (statement.DateIssueTemporaryCertificate.HasValue)
      {
        in1.TemporaryCertificateDateIssue =
          ConversionHelper.DateTimeToStringGoznak(statement.DateIssueTemporaryCertificate.Value);
      }

      // IN1.43	IS	���	���
      in1.Sex = personData.Gender.Code;

      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();

      // IN1.49	CX	��	������ ���������������
      in1.IdentificatorsList = new List<IdentificatorsCard>
                               {
                                 new IdentificatorsCard
                                 {
                                   identificator =
                                     documentManager
                                     .GetSerNumDocument(
                                                        statement
                                                          .DocumentUdl), 
                                   identificatorType =
                                     statement.DocumentUdl
                                              .DocumentType.Code, 
                                   identificatorTypeName =
                                     statement.DocumentUdl
                                              .DocumentType.Name, 
                                   ActualFrom =
                                     statement.DocumentUdl.DateIssue
                                              .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentUdl
                                                                     .DateIssue
                                                                     .Value)
                                       : string.Empty, 
                                   ActualTo =
                                     statement.DocumentUdl.DateExp
                                              .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentUdl
                                                                     .DateExp
                                                                     .Value)
                                       : string.Empty, 
                                   Organization =
                                     new OrganizationName
                                     {
                                       Name =
                                         statement
                                         .DocumentUdl
                                         .IssuingAuthority
                                     }
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificator =
                                     documentManager
                                     .GetSerNumDocument(
                                                        statement
                                                          .DocumentRegistration), 
                                   identificatorType =
                                     statement.DocumentRegistration
                                              .DocumentType.Code, 
                                   identificatorTypeName =
                                     statement.DocumentRegistration
                                              .DocumentType.Name, 
                                   ActualFrom =
                                     statement.DocumentRegistration
                                              .DateIssue.HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentRegistration
                                                                     .DateIssue
                                                                     .Value)
                                       : string.Empty, 
                                   ActualTo =
                                     statement.DocumentRegistration
                                              .DateExp.HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentRegistration
                                                                     .DateExp
                                                                     .Value)
                                       : string.Empty, 
                                   Organization =
                                     new OrganizationName
                                     {
                                       Name =
                                         statement
                                         .DocumentRegistration
                                         .IssuingAuthority
                                     }
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificatorType =
                                     "ResidencyDocument", 
                                   ActualFrom =
                                     residencyDocument != null
                                     && residencyDocument.DateIssue
                                                         .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   residencyDocument
                                                                     .DateIssue
                                                                     .Value)
                                       : string.Empty, 
                                   ActualTo =
                                     residencyDocument != null
                                     && residencyDocument.DateExp
                                                         .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   residencyDocument
                                                                     .DateExp
                                                                     .Value)
                                       : string.Empty, 
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificatorType = "NI", 
                                   identificator =
                                     statement.NumberPolicy, 
                                   Country = null, 
                                   Organization = null, 
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificatorType = "PEN", 
                                   identificator = personData.Snils, 
                                   Country = null, 
                                   Organization = null
                                 }
                               };

      // IN1.52	ST	��	����� ��������
      in1.PlaceOfBirth = personData.Birthplace;

      // IN1.100
      in1.Category = new CneStructure
                     {
                       FiveDigitCode = personData.Category.Code, 
                       Oid = Oid.����������������������������, 
                       Name = personData.Category.Name
                     };

      // IN.101 �����������
      in1.National = new National
                     {
                       Country = personData.IsNotCitizenship ? "�/�" : personData.Citizenship.Name, 
                       TableCode = personData.IsNotCitizenship ? "�/�" : personData.Citizenship.Code, 
                     };

      // IN.150 ������ ��������
      if (personData.OldCountry != null)
      {
        in1.BirthCountry = personData.OldCountry.Name;
      }

      // IN.102 �������
      in1.IsRefugee = personData.IsRefugee ? "1" : "0";

      // IN1.103 ���������� ������
      in1.TelecommunicationAddresseList = new List<TelecommunicationAddress>
                                          {
                                            new TelecommunicationAddress
                                            {
                                              Email =
                                                contact
                                                .Email, 
                                              Phone =
                                                contact
                                                .HomePhone
                                            }, 
                                            new TelecommunicationAddress
                                            {
                                              Phone =
                                                contact
                                                .WorkPhone
                                            }, 
                                          };

      return in1;
    }

    /// <summary>
    ///   The get msh.
    /// </summary>
    /// <returns>
    ///   The <see cref="MSH" />.
    /// </returns>
    private MSH GetMsh()
    {
      return null;
    }

    /// <summary>
    /// The get nk.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Nk1"/>.
    /// </returns>
    private Nk1 GetNk(Statement statement)
    {
      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();
      if (!statement.HasPetition.HasValue)
      {
        return null;
      }

      if (statement.Representative == null)
      {
        return null;
      }

      var representative = statement.Representative;
      if (statement.ModeFiling.Id == ModeFiling.ModeFiling1)
      {
        return null;
      }

      var nk1 = new Nk1
                {
                  // �������, ���, �������� (@Representative.LastName, @Representative.FirstName, @Representative.MiddleName)
                  Fio =
                    new Fio(
                    new Surname(representative.LastName), 
                    representative.FirstName, 
                    representative.MiddleName, 
                    "L"), 

                  // ��������� � ��������������� ����, �������� � ������� ������� � ��������� (@ASMOT, @ASFAT, @ASOT)
                  DegreeOfRelationship =
                    new Document
                    {
                      Code = representative.RelationType.Code, 
                      Name = representative.RelationType.Name, 
                      Oid = Oid.�����������������������������, 
                      Assignment = null
                    }, 
                  IdentificatorList =
                    new List<IdentificatorsCard>
                    {
                      new IdentificatorsCard
                      {
                        identificator =
                          documentManager.GetSerNumDocument(
                                                            representative
                                                              .Document
                                                              .Series, 
                                                            representative
                                                              .Document
                                                              .Number), 
                        enp = null, 
                        identificatorType =
                          representative.Document
                                        .DocumentType.Code, 
                        identificatorTypeName =
                          representative.Document
                                        .DocumentType.Name, 
                        ActualFrom =
                          representative.Document.DateIssue
                                        .HasValue
                            ? ConversionHelper
                                .DateTimeToStringGoznak(
                                                        representative
                                                          .Document
                                                          .DateIssue
                                                          .Value)
                            : string.Empty, 
                      }
                    }, 
                  TelecommunicationAddresseList =
                    new List<TelecommunicationAddress>
                    {
                      new TelecommunicationAddress
                      {
                        Phone =
                          representative
                          .HomePhone
                      }, 
                      new TelecommunicationAddress
                      {
                        Phone =
                          representative
                          .WorkPhone
                      }
                    }
                };

      return nk1;
    }

    /// <summary>
    /// The get zah.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Zah"/>.
    /// </returns>
    private Zah GetZah(Statement statement)
    {
      var zah = new Zah();

      // ZAH.1	CNE	��	��� ��������� � ������ ��� ������ ���
      zah.PreferenceOrChangeSmoType = statement.CauseFiling != null
                                      && CauseReinsurance.IsReinsurance(statement.CauseFiling.Id)
                                        ? new CneStructure
                                          {
                                            FiveDigitCode = statement.CauseFiling.Code, 
                                            Name = statement.CauseFiling.Name, 
                                            Oid = Oid.�����������������������������������������
                                          }
                                        : null;

      // ZAH.2	CNE	��	��� ��������� �� ������ ������
      var type =
        ObjectFactory.GetInstance<IConceptCacheManager>().SingleOrDefault(x => x.Id == statement.TypeStatementId);
      zah.PolicyIssueApplicationType = type != null
                                         ? new CneStructure
                                           {
                                             FiveDigitCode = type.Code, 
                                             Name = type.Name, 
                                             Oid = Oid.����������������
                                           }
                                         : null;

      // ZAH.3	CNE	��	������� ������ ��� ������ ������
      zah.PolicyIssueOrChangeReason = statement.CauseFiling != null && CauseReneval.IsReneval(statement.CauseFiling.Id)
                                        ? new CneStructure
                                          {
                                            FiveDigitCode = statement.CauseFiling.Code, 
                                            Name =
                                              statement.CauseFiling.Description.Replace(
                                                                                        "�������������� ������ ��� � ����� �", 
                                                                                        string.Empty)
                                                       .Replace(
                                                                "������ ��������� ������ ��� � ����� �", 
                                                                string.Empty), 
                                            Oid = Oid.���������������������������������������
                                          }
                                        : null;

      // ZAH.4	CNE	��	����� ������������ ������
      zah.PolicyForm = statement.FormManufacturing != null
                         ? new CneStructure
                           {
                             FiveDigitCode = statement.FormManufacturing.Code, 
                             Name = statement.FormManufacturing.Name, 
                             Oid = Oid.�����������������������
                           }
                         : null;

      // ZAH.5	ID	���	������� �������������
      zah.IsRepresentative = statement.HasPetition.HasValue && statement.HasPetition.Value ? "1" : "0";

      // ZAH.6	CNE	���	��� ���� ��������������� �����������
      zah.IntercessorialOrganizationTypeCode = null;

      // ZAH.7	CNE	���	������ ������ ���������
      zah.MethodOfApplicationSubmission = statement.ModeFiling != null
                                            ? new CneStructure
                                              {
                                                FiveDigitCode = statement.ModeFiling.Code, 
                                                Name = statement.ModeFiling.Name, 
                                                Oid = Oid.���������������������
                                              }
                                            : null;

      // ZAH.8	EI	��	������������� ��������� � ��������� �����������
      zah.ApplicationIDAtTheOrganizationReceivedIt = new EiStructure
                                                     {
                                                       Identificator = statement.Id.ToString(), 
                                                       OrganizationCode =
                                                         statement.PointDistributionPolicy.Parent.Code, 
                                                       Oid = "1.2.643.2.40.3.1.4.0", 
                                                       Iso = null
                                                     };

      // ZAH.9	EI	���	������������� ������ ������ �������
      zah.PolicyIssuingPointID = new EiStructure
                                 {
                                   Identificator = statement.PointDistributionPolicy.Code, 
                                   OrganizationCode = statement.PointDistributionPolicy.Parent.Code, 
                                   Oid = "1.2.643.2.40.3.1.4.0", 
                                   Iso = null
                                 };

      // ZAH.10	CNE	���	��� ���������� �����������
      zah.CodeOfTeritory = new CneStructure { CodeTfoms = statement.PointDistributionPolicy.Parent.Parent.Code };

      // ZAH.11	DT	���	���� ����������� ��������� �������������� �����
      if (statement.DateFiling != null)
      {
        zah.CompletionDate = ConversionHelper.DateTimeToStringGoznak(statement.DateFiling.Value);
      }

      // ZAH.12	ID	���	������� ������������ � ��������� ���
      zah.FamiliarizationAttribute = null;

      // ZAH.13	DTM	���	���� � ����� ����� ���������
      if (statement.DateFiling != null)
      {
        zah.RecipiencyDateTime =
          zah.CompletionDate = ConversionHelper.DateTimeToStringGoznak(statement.DateFiling.Value);
      }

      // ZAH.14	ST	���	��� ����, ���������� ���������
      zah.ReceivingEmployeeFullName = string.Empty;

      // ZAH.15	DTM	���	���� � ����� ���������� ��������� ���������
      zah.ProcessingEndingDateTime = null;

      // ZAH.16	CNE	���	������� ������
      zah.DeclineReason = null;

      return zah;
    }

    /// <summary>
    /// The get znd.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    private List<Znd> GetZnd(Statement statement)
    {
      var contentManager = ObjectFactory.GetInstance<IContentManager>();

      var listZnd = new List<Znd>();

      // Foto
      var fileName = string.Format("{0}_foto.jpg", statement.Id);
      var z = new Znd
              {
                Id = "1", 
                DispositionAndTitle =
                  new Document
                  {
                    Code = "2", 
                    Assignment = "����������", 
                    Name = fileName, 
                    Oid = "1.2.643.2.40.3.3.0.7.2"
                  }, 
                MimeType = new CneStructure { FiveDigitCode = "image/jpeg", Oid = "1.2.643.2.40.1.8.1" }, 
                ApplicationType = new ApplicationType { MainName = "Microsoft Paint" }, 
                Document�ontent = contentManager.GetFoto(statement.InsuredPersonData.Id), 
                DocumentName = fileName
              };

      listZnd.Add(z);

      // Signature
      fileName = string.Format("{0}_signature.jpg", statement.Id);
      z = new Znd
          {
            Id = "2", 
            DispositionAndTitle =
              new Document
              {
                Code = "3", 
                Assignment = "���������������� �������", 
                Name = fileName, 
                Oid = "1.2.643.2.40.3.3.0.7.2"
              }, 
            MimeType = new CneStructure { FiveDigitCode = "image/jpeg" }, 
            ApplicationType = new ApplicationType { MainName = "Microsoft Paint" }, 
            Document�ontent = contentManager.GetSignature(statement.InsuredPersonData.Id), 
            DocumentName = fileName
          };

      listZnd.Add(z);
      return listZnd;
    }

    #endregion
  }
}