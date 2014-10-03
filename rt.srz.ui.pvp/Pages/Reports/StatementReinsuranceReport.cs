using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using rt.srz.model.srz.concepts;
using rt.srz.model.srz;

namespace rt.srz.ui.pvp.Pages.Reports
{
  using rt.core.model.core;

  public partial class StatementReinsuranceReport : rt.srz.ui.pvp.Pages.Reports.BaseStatementReport
  {
    public StatementReinsuranceReport()
    {
      InitializeComponent();
    }

    #region Fill Data

    public override void FillReportData(Statement statement, User currentUser)
    {
      base.FillReportData(statement, currentUser);

      lbAsk.Text = string.Format("Прошу выдать {0} в соответствии c Федеральным законом \"Об обязательном медицинском страховании в Российской Федерации\" {1} {2}",
        statement.ModeFiling.Id == ModeFiling.ModeFiling1 ? "мне" : "гражданину, представителем которого я являюсь,",
        statement.CauseFiling.Id == CauseReneval.RenevalLoss ? "дубликат полиса обязательного медицинского страхования" : "переоформленный полис обязательного медицинского страхования",
        GetPolisType(statement));

      //Данные из предыдущего заявления
      if (statement.PreviousStatement != null)
      {
        var prevPersonData = statement.PreviousStatement.InsuredPersonData;
        lbPrevFio1.Text = prevPersonData.LastName;
        lbPrevName.Text = prevPersonData.FirstName;
        lbPrevMiddleName.Text = prevPersonData.MiddleName;
        cbPrevMale.Checked = prevPersonData.Gender.Id == Sex.Sex1;
        cbPrevFemale.Checked = prevPersonData.Gender.Id == Sex.Sex2;
        lbPrevBirthdate.Text = prevPersonData.Birthday.HasValue ? prevPersonData.Birthday.Value.ToString("dd.MM.yyyy") : string.Empty;
        lbPrevBirthPlace.Text = prevPersonData.OldCountry == null ? prevPersonData.Birthplace :
          string.Format("{0}, {1}", prevPersonData.OldCountry.Name, prevPersonData.Birthplace);
      }
      else
      {
        lbPrevFio1.Text = string.Empty;
        lbPrevName.Text = string.Empty;
        lbPrevMiddleName.Text = string.Empty;
        cbPrevMale.Checked = false;
        cbPrevFemale.Checked = false;
        lbPrevBirthdate.Text = string.Empty;
        lbPrevBirthPlace.Text = string.Empty;
      }
    }

    #endregion

  }
}
