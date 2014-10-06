// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterOrganisationTfoms.cs" company="РусБИТех">
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

  using rt.srz.model.HL7.nsi;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The importer file tfoms organisation.
  /// </summary>
  public class ImporterOrganisationTfoms : ImporterFileOrganisationBase
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ImporterOrganisationTfoms" /> class.
    /// </summary>
    public ImporterOrganisationTfoms()
      : base(TypeSubject.Tfoms)
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
        return model.srz.Oid.Tfoms;
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
      return file.Name.Contains("Справочник ТФОМС");
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
      foreach (var tfoms in packet.Tfoms)
      {
        var org = new Organisation
                  {
                    IsActive = true, 
                    IsOnLine = true, 
                    TimeRunFrom = new DateTime(1900, 1, 1, 9, 0, 0), 
                    TimeRunTo = new DateTime(1900, 1, 1, 21, 0, 0), 
                    Code = tfoms.TfKod, 
                    Oid = oid, 
                    Okato = tfoms.TfOkato, 
                    Ogrn = tfoms.TfOgrn, 
                    FullName = tfoms.NameTfp, 
                    ShortName = tfoms.NameTfk, 
                    Address = string.Format("{0},{1}", tfoms.Index, tfoms.Address), 
                    LastName = tfoms.FamDir, 
                    FirstName = tfoms.ImDir, 
                    MiddleName = tfoms.OtDir, 
                    Phone = tfoms.Phone, 
                    Fax = tfoms.Fax, 
                    Website = tfoms.Www, 
                    EMail = tfoms.EMail
                  };

        cqtasks.Enqueue(org);
      }
    }

    #endregion
  }
}