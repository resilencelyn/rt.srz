using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls
{
  public partial class IssueOfPolicy : System.Web.UI.UserControl
  {
    #region Properties

    /// <summary>
    /// Тип полиса
    /// </summary>
    public string PolicyTypeId
    {
      get
      {
        return ddlPolicyType.SelectedValue;
      }
      set
      {
        ddlPolicyType.SelectedValue = value;
      }
    }

    /// <summary>
    /// Номер ЕНП
    /// </summary>
    public string EnpNumber
    {
      get
      {
        return tbEnpNumber.Text;
      }
      set
      {
        tbEnpNumber.Text = value;
      }
    }

    /// <summary>
    /// Номер бланка полиса
    /// </summary>
    public string PolicyNumber
    {
      get
      {
        return tbPolicyCertificateNumber.Text;
      }
      set
      {
        tbPolicyCertificateNumber.Text = value;
      }
    }

    /// <summary>
    /// Дата выдачи полиса
    /// </summary>
    public string PolicyDateIssue
    {
      get
      {
        return tbPolicyDateIssue.Text;
      }
      set
      {
        tbPolicyDateIssue.Text = value;
      }
    }

    /// <summary>
    /// Дата выдачи полиса
    /// </summary>
    public string PolicyDateEnd
    {
      get
      {
        return tbPolicyDateEnd.Text;
      }
      set
      {
        tbPolicyDateEnd.Text = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool EnabledPolicyType
    {
      get
      {
        return ddlPolicyType.Enabled;
      }
      set 
      {
        ddlPolicyType.Enabled = value;
      }
    }

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Используется родительским компонентом для заполнения списка доступных типов полиса
    /// </summary>
    /// <param name="documentList"></param>
    /// <param name="selectedValue"></param>
    public void FillPolicyTypeDdl(ListItem[] documentList, string selectedValue)
    {
      ddlPolicyType.Items.Clear();
      ddlPolicyType.Items.AddRange(documentList);
      if (!string.IsNullOrEmpty(selectedValue))
      {
        ddlPolicyType.SelectedValue = selectedValue;
        tbPolicyCertificateNumber.MaxLength = selectedValue == "322" ? 14 : 11;
      }
    }
    #endregion
  }
}