using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls
{
  [DefaultProperty("Text")]
  [ToolboxData("<{0}:DateBox runat=server></{0}:DateBox>")]
  public class DateBox : TextBox, IScriptControl
  {
    #region Fields

    private ScriptManager _sm;
    private string _onBlur;
    private string _onPaste;
    private string _onKeyDown;

    #endregion

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
      get
      {
        String s = (String)ViewState["Text"];
        return ((s == null) ? String.Empty : s);
      }

      set
      {
        ViewState["Text"] = value;
      }
    }

    /// <summary>
    /// Дата
    /// </summary>
    public DateTime? Date
    {
      get 
      {
        DateTime result;
        if (DateTime.TryParse(Text, out result))
        {
          return result;
        }
        return null;
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (!this.DesignMode)
      {
        _sm = ScriptManager.GetCurrent(Page);
        if (_sm == null)
        {
          throw new HttpException("A ScriptManager control must exist on the current page.");
        }
        _sm.RegisterScriptControl(this);
      }
      base.OnPreRender(e);
    }

    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode)
      {
        _sm.RegisterScriptDescriptors(this);
      }
      base.Render(writer);
    }

    protected override void RenderContents(HtmlTextWriter output)
    {
      output.Write(Text);
    }

    protected virtual IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReference reference = new ScriptReference();
      reference.Path = ResolveUrl("~/Controls/DateBox.js");
      return new ScriptReference[] { reference };
    }

    protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor descriptor = new ScriptControlDescriptor(GetType().FullName, ClientID);
      //descriptor.AddProperty("onBlur", onBlur);
      return new ScriptDescriptor[] { descriptor };
    }

    IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
    {
      return GetScriptReferences();
    }

    IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
    {
      return GetScriptDescriptors();
    }
  }
}
