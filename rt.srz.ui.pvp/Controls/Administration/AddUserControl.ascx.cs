// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Web.UI;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;

#endregion

namespace rt.srz.ui.pvp.Controls.Administration
{
  using rt.core.model.core;
  using rt.core.model.interfaces;

  /// <summary>
  /// The add user control.
  /// </summary>
  public partial class AddUserControl : UserControl
  {
    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService _securityService;

    /// <summary>
    /// The _user.
    /// </summary>
    private User _user;

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
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["UserId"] == null)
      {
        _user = new User();
      }
      else
      {
        _user = _securityService.GetUser(Guid.Parse(Request.QueryString["UserId"]));
        //если это редактирование пользователя то не нужно проверять пустой пароль или нет
        rfPassword.Enabled = false;
      }

      if (!IsPostBack)
      {
        if (Request.QueryString["UserId"] == null)
        {
          lbTitle.Text = "Реквизиты пользователя";
          return;
        }

        tbLogin.Text = _user.Login;
        tbPassword.Text = _user.Password;
        tbEmail.Text = _user.Email;
        tbFio.Text = _user.Fio;
        lbTitle.Text = "Реквизиты пользователя";
        tbPassword.Enabled = false;
      }
    }

    /// <summary>
    /// The save changes.
    /// </summary>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    public Guid SaveChanges()
    {
      if (_user != null)
      {
        _user.Login = tbLogin.Text;
        _user.Email = tbEmail.Text;
        _user.Fio = tbFio.Text;

        // !!! не передавать внутрь функции пароль из tbPassword в режиме редактирования пользователя т.к. он там уже зашифрованный
        if (_user.Id == Guid.Empty)
        {
          _user.CreationDate = DateTime.Now;
          _user.LastLoginDate = DateTime.Now;
          _user.IsApproved = true;

////var hash = new PasswordHash(tbPassword.Text);
          ////string password = Convert.ToBase64String(hash.Hash);
          ////string salt = Convert.ToBase64String(hash.Salt);
          _user.Password = tbPassword.Text;

////_user.Salt = salt;
          return _securityService.AddUser(_user).Id;
        }
      }

      return _securityService.SaveUser(_user).Id;
    }
  }
}