﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  public partial class AdministrationSmos : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void Render(HtmlTextWriter writer)
    {
      SmosControl.RenderInPage();
      base.Render(writer);
    }

  }
}