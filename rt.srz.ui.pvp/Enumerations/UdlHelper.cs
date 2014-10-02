using rt.srz.model.srz.concepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rt.srz.ui.pvp.Enumerations
{
  public static class UdlHelper
  {
    public static void GetSeries(int documentType, string originalSeries, out string series, out string additionalSeries)
    {
      //Серия
      series = string.Empty;
      additionalSeries = string.Empty;
      if (documentType == rt.srz.model.srz.concepts.DocumentType.BirthCertificateRf)
      {
        string[] seriesStr = originalSeries.Split(new char[] { '-' });
        if (seriesStr.Length >= 1)
        {
          series = seriesStr[0];
        }
        if (seriesStr.Length >= 2)
        {
          additionalSeries = seriesStr[1];
        }
      }
      else
      {
        series = originalSeries;
        additionalSeries = string.Empty;
      }
    }
  }
}