using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.ui.pvp.Pages.Reports;
using StructureMap;

namespace rt.srz.ui.pvp.Pages
{
  using rt.core.model.interfaces;

  public partial class PrintStatement : System.Web.UI.Page
  {
    private IStatementService _service;
    private ISecurityService _securityService;
    private Statement _statement;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IStatementService>();
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      Guid id;
      if (Session[SessionConsts.CGuidStatementId] == null)
      {
        throw new ArgumentNullException("Заявление было закрыто. Просмотр невозможен.");
      }

      id = (Guid)Session[SessionConsts.CGuidStatementId];
      _statement = _service.GetStatement(id);

      if (CauseReneval.IsReneval(_statement.CauseFiling.Id))
      {
        ReportViewer.Report = new StatementReinsuranceReport();
      }
      else
      {
        ReportViewer.Report = new StatementReport();
      }

      FillReportData(ReportViewer.Report as IReport);
    }

    private void FillReportData(IReport report)
    {
      report.FillReportData(_statement, _securityService.GetCurrentUser());
    }
  }
}