// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.aspx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Account
{
  using System;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  /// <summary>
  /// The login.
  /// </summary>
  public partial class Login : Page
  {
    #region Fields

    /// <summary>
    /// The _service.
    /// </summary>
    private ISecurityService service;

    #endregion

    #region Methods

    /// <summary>
    /// The login user_ on logged in.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void LoginUser_OnLoggedIn(object sender, EventArgs e)
    {
      // не можем получить текущего юзера из контекста внутри сервиса т.к. контекст еще не обновит внутри данные, только в следующий запрос
      var textBox = LoginUser.FindControl("UserName") as TextBox;
      if (textBox != null)
      {
        var userName = textBox.Text;
        var currentUser = service.GetUserByName(userName);

        // если что либо из списка ниже не в онлайне и пользователь не админ и не админские права, то выдаём страницу что сайт на тех обслуживании
        var pvp = currentUser.PointDistributionPolicy();
        if (pvp != null && (!pvp.IsOnLine || !pvp.Parent.IsOnLine || !pvp.Parent.Parent.IsOnLine)
            && !service.IsUserHasAdminPermissions(currentUser))
        {
          RedirectUtils.RedirectToTechnical(Response);
        }
        else
        {
          Response.Redirect("~/Pages/Main.aspx");
        }
      }
    }

    /// <summary>
    /// The login user_ on login error.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void LoginUser_OnLoginError(object sender, EventArgs e)
    {
      LoginUser.FailureText = "Неверный пароль или имя пользователя!";
      lblFailureText.Visible = true;
    }

    /// <summary>
    /// The page_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Init(object sender, EventArgs e)
    {
      service = ObjectFactory.GetInstance<ISecurityService>();
    }

    #endregion
  }
}