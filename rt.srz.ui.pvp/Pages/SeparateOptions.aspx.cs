using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages
{
  public partial class SeparateOptions : System.Web.UI.Page
  {
    private IStatementService _statementService;
    private ITfomsService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<ITfomsService>();
      _statementService = ObjectFactory.GetInstance<IStatementService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSeparate_Click(object sender, EventArgs e)
    {
      ////if (rbl.SelectedValue == "all")
      ////{
      ////  var children = _statementService.GetChildInsuredPerson((Guid)Session[SessionConsts.c_MainInsuredId]);
      ////  foreach (var child in children)
      ////  {
      ////    _service.SeparatePersons(child.Id, (Guid)Session[SessionConsts.c_MainInsuredId]);
      ////  }
      ////}
      ////else
      ////{
      ////  _service.SeparatePersons((Guid)Session[SessionConsts.c_InsuredId], (Guid)Session[SessionConsts.c_MainInsuredId]);
      ////}
      RedirectUtils.RedirectToMain(Response);
    }
  }
}