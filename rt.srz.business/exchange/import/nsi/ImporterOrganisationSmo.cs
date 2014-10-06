// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterOrganisationSmo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The importer file tfoms organisation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.smo
{
  using System;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using rt.srz.model.HL7.nsi;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The importer file tfoms organisation.
  /// </summary>
  public class ImporterOrganisationSmo : ImporterFileOrganisationBase
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ImporterOrganisationSmo" /> class.
    /// </summary>
    public ImporterOrganisationSmo()
      : base(TypeSubject.Smo)
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
        return model.srz.Oid.Smo;
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
      return file.Name.Contains("Реестр СМО");
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
      foreach (var smo in packet.InsCompany)
      {
        var tf =
          session.QueryOver<Organisation>()
                 .Where(x => x.Oid.Id == model.srz.Oid.Tfoms && x.Okato == smo.TfOkato)
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
                    Code = smo.Smocod, 
                    Oid = oid, 
                    Okato = smo.TfOkato, 
                    Ogrn = smo.Ogrn, 
                    FullName = smo.NamSmop, 
                    ShortName = smo.NamSmok, 
                    Inn = smo.Inn, 
                    EMail = smo.EMail, 
                    Address = string.Format("{0},{1}", smo.PstAddress.IndexF, smo.PstAddress.AddrF), 
                    LastName = smo.FamRuk, 
                    FirstName = smo.ImRuk, 
                    MiddleName = smo.OtRuk, 
                    Phone = smo.Phone, 
                    Fax = smo.Fax, 
                    Website = smo.Www, 
                  };

        cqtasks.Enqueue(org);
      }
    }

    #endregion
  }
}