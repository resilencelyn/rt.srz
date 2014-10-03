using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;

namespace rt.srz.ui.pvp.Pages.Reports
{
  using rt.core.model.core;

  public partial class StatementReport : rt.srz.ui.pvp.Pages.Reports.BaseStatementReport
  {
    public StatementReport()
    {
      InitializeComponent();
    }

    #region Fill Data


    public override void FillReportData(Statement statement, User currentUser)
    {
      base.FillReportData(statement, currentUser);

      // текст обращения в СМО
      lbAsk.Text = string.Format(@"Прошу зарегистрировать {0} в качестве лица, застрахованного по обязательному медицинскому страхованию, в страховой медицинской организации",
        statement.ModeFiling.Id == ModeFiling.ModeFiling1 ? "меня" : "гражданина, представителем которого я являюсь,");
      lbSmo1.Text = lbSmo.Text;

      lbInCase.Text = string.Format("и выдать {0} в соответствии с Федеральным законом \"Об обязательном медицинском страховании в Российской Федерации\" полис обязательного медицинского страхования {1}",
      statement.ModeFiling.Id == ModeFiling.ModeFiling1 ? "мне" : "гражданину, представителем которого я являюсь,", GetPolisType(statement));

      // Номер полиса
      if (!string.IsNullOrEmpty(statement.NumberPolicy))
      {
        cbAbsentPolicyNumber.Checked = false;
        lbPNum1.Text = statement.NumberPolicy[0].ToString();
        lbPNum2.Text = statement.NumberPolicy[1].ToString();
        lbPNum3.Text = statement.NumberPolicy[2].ToString();
        lbPNum4.Text = statement.NumberPolicy[3].ToString();
        lbPNum5.Text = statement.NumberPolicy[4].ToString();
        lbPNum6.Text = statement.NumberPolicy[5].ToString();
        lbPNum7.Text = statement.NumberPolicy[6].ToString();
        lbPNum8.Text = statement.NumberPolicy[7].ToString();
        lbPNum9.Text = statement.NumberPolicy[8].ToString();
        lbPNum10.Text = statement.NumberPolicy[9].ToString();
        lbPNum11.Text = statement.NumberPolicy[10].ToString();
        lbPNum12.Text = statement.NumberPolicy[11].ToString();
        lbPNum13.Text = statement.NumberPolicy[12].ToString();
        lbPNum14.Text = statement.NumberPolicy[13].ToString();
        lbPNum15.Text = statement.NumberPolicy[14].ToString();
        lbPNum16.Text = statement.NumberPolicy[15].ToString();
      }
      else
      {
        cbAbsentPolicyNumber.Checked = true;
      }
    }

    #endregion

  }
}
