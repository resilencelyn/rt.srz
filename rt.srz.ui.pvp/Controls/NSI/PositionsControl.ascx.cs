using rt.srz.model.srz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.NSI
{
  public partial class PositionsControl : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetDataToControls(Template template)
    {
      controlSmo.SetData(template.PosSmo, "СМО");
      controlAddress.SetData(template.PosAddress, "Адрес");
      controlDay1.SetData(template.PosDay1, "День 1");
      controlMonth1.SetData(template.PosMonth1, "Месяц 1");
      controlYear1.SetData(template.PosYear1, "Год 1");
      controlBirthPlace.SetData(template.PosBirthplace, "Место рождения");
      controlMale.SetData(template.PosMale, "Мужской пол");
      controlFemale.SetData(template.PosFemale, "Женский пол");
      controlDay2.SetData(template.PosDay2, "День 2");
      controlMonth2.SetData(template.PosMonth2, "Месяц 2");
      controlYear2.SetData(template.PosYear2, "Год 2");
      controlFio.SetData(template.PosFio, "ФИО");
      controlLine1.SetData(template.PosLine1, "Линия 1");
      controlLine2.SetData(template.PosLin2, "Линия 2");
      controlLine3.SetData(template.PosLine3, "Линия 3");
    }

    public void SetControlsDataToObject(Template template)
    {
      template.PosSmo = controlSmo.GetData();
      template.PosAddress = controlAddress.GetData();
      template.PosDay1 = controlDay1.GetData();
      template.PosMonth1 = controlMonth1.GetData();
      template.PosYear1 = controlYear1.GetData();
      template.PosBirthplace = controlBirthPlace.GetData();
      template.PosMale = controlMale.GetData();
      template.PosFemale = controlFemale.GetData();
      template.PosDay2 = controlDay2.GetData();
      template.PosMonth2 = controlMonth2.GetData();
      template.PosYear2 = controlYear2.GetData();
      template.PosFio = controlFio.GetData();
      template.PosLine1 = controlLine1.GetData();
      template.PosLin2 = controlLine2.GetData();
      template.PosLine3 = controlLine3.GetData();
    }
  }
}