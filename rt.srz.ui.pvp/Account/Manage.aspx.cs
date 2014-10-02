using System;

namespace rt.srz.ui.pvp.Account
{
    public partial class Account_Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    Form.Action = ResolveUrl("~/Account/Manage.aspx");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Ваш пароль был успешно изменен."
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The external login was removed."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }
        }

        protected void OnChangePasswordError(object sender, EventArgs e)
        {
            ChangePassword1.ChangePasswordFailureText = "Неверный пароль или новый пароль недействителен. Минимальная длина пароля: {0}.";
        }
    }
}