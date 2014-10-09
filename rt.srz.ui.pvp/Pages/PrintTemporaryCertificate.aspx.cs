using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Pages.Reports;
using StructureMap;

namespace rt.srz.ui.pvp.Pages
{
  public partial class PrintTemporaryCertificate : System.Web.UI.Page
  {
    private IStatementService _service;
    private IRegulatoryService regulatory;
    private Statement _statement;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IStatementService>();
      regulatory = ObjectFactory.GetInstance<IRegulatoryService>();      
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
      ReportViewer1.Report = new TemporaryCertificateReport();
      FillReportData(ReportViewer1.Report as TemporaryCertificateReport);
    }

    private void FillReportData(TemporaryCertificateReport report)
    {
      //уже проверяли найден ли шаблон, поэтому этот шаблон передаём через сессию
      Template template = (Template)Session[SessionConsts.CTemplateVsForPrint];//regulatory.GetTemplateByStatement(_statement);
      report.FillReportData(_statement, template);
    }

  }
}