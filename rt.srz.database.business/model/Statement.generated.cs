namespace rt.srz.database.business.model
{
  using System;

  public partial class Statement
  {
    public Statement() 
    {
    }

    public virtual Guid RowId { get; set; }

    public virtual System.DateTime? DateFiling { get; set; }

    public virtual bool? HasPetition { get; set; }

    public virtual string NumberPolicy { get; set; }

    public virtual string NumberTemporaryCertificate { get; set; }

    public virtual System.DateTime? DateIssueTemporaryCertificate { get; set; }

    public virtual bool? AbsentPrevPolicy { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual string NumberPolisCertificate { get; set; }

    public virtual System.DateTime? DateIssuePolisCertificate { get; set; }

    public virtual bool? PolicyIsIssued { get; set; }
  }
}
