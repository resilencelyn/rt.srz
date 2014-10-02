using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rt.srz.model.srz;

namespace rt.srz.ui.pvp.Pages.Reports
{
  public interface IReport
  {
    /// <summary>
    /// Заполняет данные в отчете
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="currentUser"></param>
    void FillReportData(Statement statement, User currentUser);
  }
}