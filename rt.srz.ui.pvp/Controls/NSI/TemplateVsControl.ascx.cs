using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.NSI
{
  public partial class TemplateVsControl : System.Web.UI.UserControl
  {
    private INsiService _service;
    private Template _template;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<INsiService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["Id"] == null)
      {
        _template = new Template();
      }
      else
      {
        _template = _service.GetTemplate(Guid.Parse(Request.QueryString["Id"]));
      }

      if (!IsPostBack)
      {
        //по любому выставляем данные, даже если добавляем т.к. ещё выставляются подписи на контролах 
        positionsControl.SetDataToControls(_template);

        if (Request.QueryString["Id"] == null)
        {
          lbTitle.Text = "Добавление шаблона печати вс";
          return;
        }
        tbName.Text = _template.Name;
        cbByDefault.Checked = _template.Default.HasValue ? _template.Default.Value : false;
        lbTitle.Text = "Редактирование шаблона печати вс";
      }
    }

    private void SetObjValues(Template obj)
    {
      obj.Name = tbName.Text;
      obj.Default = cbByDefault.Checked;
      positionsControl.SetControlsDataToObject(obj);
    }

    public void SaveChanges()
    {
      SetObjValues(_template);
      _service.AddOrUpdateTemplate(_template);
    }
  }
}