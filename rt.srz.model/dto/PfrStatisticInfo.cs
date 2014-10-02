using rt.srz.model.srz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.model.dto
{
  public class PfrStatisticInfo
  {
    public int TotalRecordCount
    {
      get; set;
    }

    public int NotFoundRecordCount
    {
      get; set;
    }

    public int InsuredRecordCount
    {
      get; set;
    }

    public int EmployedRecordCount
    {
      get; set;
    }

    public int FoundBySnilsRecordCount
    {
      get; set;
    }

    public int FoundByDataRecordCount
    {
      get; set;
    }

  }
}
