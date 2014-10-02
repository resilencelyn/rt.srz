using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;
using rt.srz.ui.pvp.Pages.Administrations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.NSI
{
  public partial class FirstMiddleName : System.Web.UI.Page
  {
    private INsiService _service;
    private AutoComplete _name;

    private BaseAddCancelPage MasterPage
    {
      get { return (BaseAddCancelPage)this.Master; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      MasterPage.Save += MasterPage_Save;
      MasterPage.Cancel += MasterPage_Cancel;
      _service = ObjectFactory.GetInstance<INsiService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["Id"] == null)
      {
        _name = new AutoComplete();
      }
      else
      {
        _name = _service.GetFirstMiddleName(Guid.Parse(Request.QueryString["Id"]));
      }

      if (!IsPostBack)
      {
        MasterPage.ActionTitle = "";

        cbGender.DataSource = _service.GetConceptsByOid(Oid.Пол);
        cbGender.DataBind();
        cbType.DataSource = _service.GetConceptsByOid(Oid.AutoCompleteType);
        cbType.DataBind();

        if (Request.QueryString["Id"] == null)
        {
          lbTitle.Text = "Добавление имени/отчества";
          return;
        }
        tbName.Text = _name.Name;
        cbGender.SelectedValue = _name.Gender != null ? _name.Gender.Id.ToString() : null;
        cbType.SelectedValue = _name.Type != null ? _name.Type.Id.ToString() : null;
        lbTitle.Text = "Редактирование имени/отчества";
      }
    }

    void MasterPage_Cancel()
    {
      RedirectUtils.RedirectToFirstMiddleNames(Response);
    }

    void MasterPage_Save()
    {
      SaveChanges();
      RedirectUtils.RedirectToFirstMiddleNames(Response);
    }

    private void SetObjValues(AutoComplete obj)
    {
      obj.Name = tbName.Text.Trim();
      obj.Gender = _service.GetConcept(int.Parse(cbGender.SelectedValue));
      obj.Type = _service.GetConcept(int.Parse(cbType.SelectedValue));
      if (obj.Id == Guid.Empty)
      {
        obj.Relevance = 99999;
      }
    }

    private Guid SaveChanges()
    {
      SetObjValues(_name);
      return _service.AddOrUpdateFirstMiddleName(_name);
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
      AutoComplete name = new AutoComplete();
      name.Id = _name.Id;
      SetObjValues(name);
      args.IsValid = !_service.FirstMiddleNameExists(name);
    }

  }
}