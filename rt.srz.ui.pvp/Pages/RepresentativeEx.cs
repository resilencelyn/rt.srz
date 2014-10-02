// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepresentativeEx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages
{
  #region

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   Расширенный представитель
  /// </summary>
  public sealed class RepresentativeEx : Representative
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RepresentativeEx"/> class.
    /// </summary>
    /// <param name="representative">
    /// The representative. 
    /// </param>
    public RepresentativeEx(Representative representative)
    {
      if (representative != null)
      {
        FirstName = representative.FirstName;
        LastName = representative.LastName;
        MiddleName = representative.MiddleName;
        HomePhone = representative.HomePhone;
        WorkPhone = representative.WorkPhone;
        RelationTypeId = representative.RelationType != null ? representative.RelationType.Id : -1;
        if (representative.Document != null)
        {
          IssuingAuthority = representative.Document.IssuingAuthority;
          if (representative.Document.DateIssue != null)
          {
            DateIssue = representative.Document.DateIssue.Value.ToString("dd.MM.yyyy");
          }

          if (representative.Document.DateExp != null)
          {
            DateExp = representative.Document.DateExp.Value.ToString("dd.MM.yyyy");
          }
        }
      }
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the date issue.
    /// </summary>
    public string DateIssue { get; set; }

    /// <summary>
    ///   Gets or sets the date issue.
    /// </summary>
    public string DateExp { get; set; }

    /// <summary>
    ///   Gets or sets the issuing authority.
    /// </summary>
    public string IssuingAuthority { get; set; }

    /// <summary>
    ///   Gets or sets the relation type id.
    /// </summary>
    public int RelationTypeId { get; set; }

    #endregion
  }
}