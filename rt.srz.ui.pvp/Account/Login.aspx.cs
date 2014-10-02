using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Account
{
  public partial class Login : System.Web.UI.Page
  {
    private ISecurityService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void LoginUser_OnLoggedIn(object sender, EventArgs e)
    {
      // не можем получить текущего юзера из контекста внутри сервиса т.к. контекст еще не обновит внутри данные, только в следующий запрос
      var userName = (LoginUser.FindControl("UserName") as TextBox).Text;
      var currentUser = _service.GetUserByName(userName);

      // если что либо из списка ниже не в онлайне и пользователь не админ и не админские права, то выдаём страницу что сайт на тех обслуживании
      if (currentUser.PointDistributionPolicy != null && (
        // пункт выдачи
        !currentUser.PointDistributionPolicy.IsOnLine ||
        // смо
        !currentUser.PointDistributionPolicy.Parent.IsOnLine ||
        // территориальный фонд
        !currentUser.PointDistributionPolicy.Parent.Parent.IsOnLine) && !_service.IsUserHasAdminPermissions(currentUser))
      {
        RedirectUtils.RedirectToTechnical(Response);
      }
      else
      {
        Response.Redirect("~/Pages/Main.aspx");
      }
    }

    protected void LoginUser_OnLoginError(object sender, EventArgs e)
    {
      LoginUser.FailureText = "Неверный пароль или имя пользователя!";
      lblFailureText.Visible = true;
    }
  }
}
