// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterOrganisationMo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The importer file tfoms organisation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.nsi
{
  using System;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using rt.srz.model.Hl7.nsi;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The importer file tfoms organisation.
  /// </summary>
  public class ImporterOrganisationMo : ImporterFileOrganisationBase
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ImporterOrganisationMo" /> class.
    /// </summary>
    public ImporterOrganisationMo()
      : base(ExchangeSubjectType.Tfoms)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the oid.
    /// </summary>
    protected override string Oid
    {
      get
      {
        return model.srz.Oid.Mo;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The applies to.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      return file.Name.Contains("Реестр МО");
    }

    #endregion

    #region Methods

    /// <summary>
    /// The prepair list.
    /// </summary>
    /// <param name="packet">
    /// The packet.
    /// </param>
    /// <param name="cqtasks">
    /// The cqtasks.
    /// </param>
    /// <param name="oid">
    /// The oid.
    /// </param>
    protected override void PrepairList(Packet packet, ConcurrentQueue<Organisation> cqtasks, Oid oid)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      foreach (var mo in packet.MedCompany)
      {
        var tf =
          session.QueryOver<Organisation>()
                 .Where(x => x.Oid.Id == model.srz.Oid.Tfoms && x.Okato == mo.TfOkato)
                 .Take(1)
                 .List()
                 .SingleOrDefault();

        var org = new Organisation
                  {
                    IsActive = true, 
                    IsOnLine = true, 
                    TimeRunFrom = new DateTime(1900, 1, 1, 9, 0, 0), 
                    TimeRunTo = new DateTime(1900, 1, 1, 21, 0, 0), 
                    Parent = tf, 
                    Code = mo.Mcod, 
                    Oid = oid, 
                    ////Okato = mo.TfOkato, 
                    Ogrn = mo.Ogrn, 
                    FullName = mo.NamMop, 
                    ShortName = mo.NamMok, 
                    ////Inn = mo.Inn, 
                    ////EMail = mo.EMail,
                    ////Address = string.Format("{0},{1}", mo.JurAddress.IndexJ, mo.JurAddress.AddrJ), 
                    ////LastName = mo.FamRuk, 
                    ////FirstName = mo.ImRuk, 
                    ////MiddleName = mo.OtRuk, 
                    ////Phone = mo.Phone, 
                    ////Fax = mo.Fax, 
                    ////Website = mo.Www, 
                  };

        cqtasks.Enqueue(org);
      }
    }

    #endregion
  }
}