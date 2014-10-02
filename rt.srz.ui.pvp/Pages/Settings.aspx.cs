using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using StructureMap;
using rt.srz.model.interfaces.service;

namespace rt.srz.ui.pvp.Pages
{
  class SettingsDataSource
  {
    public string Name { get; set; }
    public Dictionary<string, string> List = new Dictionary<string, string>();
    public string SelectedValue { get; set; }

    public SettingsDataSource(String name, string selectedValue, Dictionary<string, string> list)
    {
      Name = name;
      SelectedValue = selectedValue;
      List = list;
    }
  }

  public partial class Settings : System.Web.UI.Page
  {
    private const string KladrControlType = "KLADRControlType";
    private const string ComPort = "COMPort";
    private const string PhotoControlType = "PhotoControlType";

    private ArrayList PopulateDataSource()
    {
      var settings = new ArrayList();

      #region KLADRControlType

      Dictionary<string, string> list = new Dictionary<string, string>();
      list.Add(KladrControlType + "*0", "Intellisense");
      list.Add(KladrControlType + "*1", "Structured");

      var statementService = ObjectFactory.GetInstance<IStatementService>();
      var kladrType = statementService.GetSettingCurrentUser(KladrControlType);
      settings.Add(new SettingsDataSource(KladrControlType, kladrType, list));

      #endregion

      #region COMPort

      list = new Dictionary<string, string>();
      list.Add(ComPort + "*0", "Не использовать");
      list.Add(ComPort + "*1", "COM1");
      list.Add(ComPort + "*2", "COM2");
      list.Add(ComPort + "*3", "COM3");
      list.Add(ComPort + "*4", "COM4");
      list.Add(ComPort + "*5", "COM5");
      list.Add(ComPort + "*6", "COM6");
      list.Add(ComPort + "*7", "COM7");
      list.Add(ComPort + "*8", "COM8");
      list.Add(ComPort + "*9", "COM9");
      list.Add(ComPort + "*10", "COM10");

      var comSetting = statementService.GetSettingCurrentUser(ComPort);
      settings.Add(new SettingsDataSource(ComPort, comSetting, list));

      #endregion

      #region PhotoControlType

      list = new Dictionary<string, string>();
      list.Add(PhotoControlType + "*0", "Upload");
      list.Add(PhotoControlType + "*1", "Webcam");

      var photoControlType = statementService.GetSettingCurrentUser(PhotoControlType);
      settings.Add(new SettingsDataSource(PhotoControlType, photoControlType, list));

      #endregion

      return settings;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        gvSettings.DataSource = PopulateDataSource();
        gvSettings.DataBind();
      }
    }

    protected void gvStates_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (!IsPostBack)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          DropDownList ddl = (DropDownList)e.Row.FindControl("ddlValues");

          Dictionary<string, string> list = ((SettingsDataSource)e.Row.DataItem).List;

          int selectedIndex = 0;
          foreach (var kvp in list)
          {
            if (kvp.Value == ((SettingsDataSource)e.Row.DataItem).SelectedValue)
            {
              selectedIndex = Int32.Parse(kvp.Key.Split('*')[1]);
              break;
            }
          }

          ddl.DataSource = list;
          ddl.DataTextField = "Value";
          ddl.DataValueField = "Key";
          ddl.DataBind();
          ddl.SelectedIndex = selectedIndex;
        }
      }
    }

    protected void ddlValues_SelectedIndexChanged(object sender, EventArgs e)
    {
      var ddl = ((DropDownList)sender);
      var param = ddl.SelectedValue.Split('*')[0];
      ObjectFactory.GetInstance<IStatementService>().SetSettingCurrentUser(param, ddl.SelectedItem.Text);
    }
  }
}