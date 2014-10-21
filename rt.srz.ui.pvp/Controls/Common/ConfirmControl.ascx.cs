// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmControl.ascx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Common
{
  using System;
  using System.Text;
  using System.Web.UI;

  /// <summary>
  ///   The confirm control.
  /// </summary>
  public partial class ConfirmControl : UserControl
  {
    #region Constants

    /// <summary>
    ///   The _confirmed argument basic.
    /// </summary>
    private const string ConfirmedArgumentBasic = "Delete";

    /// <summary>
    ///   The _confirmed target basic.
    /// </summary>
    private const string ConfirmedTargetBasic = "Menu";

    #endregion

    #region Fields

    /// <summary>
    ///   The _message.
    /// </summary>
    private string message = "Вы уверены, что хотите удалить выбранную запись?";

    /// <summary>
    ///   The _mode.
    /// </summary>
    private Mode mode = Mode.Confirm;

    #endregion

    #region Enums

    /// <summary>
    ///   The mode.
    /// </summary>
    public enum Mode
    {
      /// <summary>
      ///   режим отображения - две коннпки - подтвердить, отменить
      /// </summary>
      Confirm, 

      /// <summary>
      ///   режим отображения - одна кнопка закрыть
      /// </summary>
      Close
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Режим отображения диалогового окошка
    /// </summary>
    public Mode ConfirmMode
    {
      get
      {
        return mode;
      }

      set
      {
        mode = value;
      }
    }

    /// <summary>
    ///   Аргумент используемый в качестве постбека
    /// </summary>
    public string ConfirmedArgumentUnique
    {
      get
      {
        return string.Format("{0}{1}", UniqueID, ConfirmedArgumentBasic);
      }
    }

    /// <summary>
    ///   Таргет используемый в качестве постбека
    /// </summary>
    public string ConfirmedTargetUnique
    {
      get
      {
        return string.Format("{0}{1}", UniqueID, ConfirmedTargetBasic);
      }
    }

    /// <summary>
    ///   Сообщение отображаемое пользователю
    /// </summary>
    public string Message
    {
      get
      {
        var value = ViewState[UniqueID];
        if (value != null)
        {
          message = value.ToString();
        }

        return message;
      }

      set
      {
        ViewState[UniqueID] = value;
        message = value;
      }
    }

    /// <summary>
    ///   скрипт который надо прописать например для пункта меню в NavigateUrl для вызова данного контрола подтверждения
    /// </summary>
    public string ViewConfirmScript
    {
      get
      {
        return string.Format("javascript:ViewConfirm{0}();", UniqueID);
      }
    }

    /// <summary>
    ///   скрипт который надо прописать например для кнонпки в OnClientClick для вызова данного контрола подтверждения
    /// </summary>
    public string ViewConfirmScriptForButton
    {
      get
      {
        return string.Format("javascript:ViewConfirm{0}(); return false", UniqueID);
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The hide.
    /// </summary>
    public void Hide()
    {
      deleteConfirmDiv.Style.Value = "display: none";
      extender.Hide();
    }

    /// <summary>
    /// Устанавливает ид для того чтобы можно было вызвать из ява скрипта обращение по этому ид.
    ///   Используется в мастер странице для отображения сообщения что время сессии истекло
    ///   Если на странице много контролов, то ид должны быть уникальные для правильного отображения из скрипта по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void SetBehaviorId(string id)
    {
      extender.BehaviorID = id;
    }

    /// <summary>
    /// Скрипт по кнопке отмена (закрыть)
    /// </summary>
    /// <param name="script">
    /// The script.
    /// </param>
    public void SetCancelScript(string script)
    {
      extender.OnCancelScript = script;
    }

    /// <summary>
    ///   Отображение окошка конфирмации из серверного кода
    /// </summary>
    public void Show()
    {
      extender.Show();
    }

    

    #endregion

    #region Methods

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
      if (ConfirmMode == Mode.Close)
      {
        btnConfirm.Visible = false;
        btnCancel.Text = @"Закрыть";
      }

      var sb = new StringBuilder();
      sb.Append("<script type=\"text/javascript\">")
        .Append("function ViewConfirm")
        .Append(UniqueID)
        .Append("()")
        .Append("{")
        .Append("$find('")
        .Append(extender.ClientID)
        .Append("').show();")
        .Append("}")
        .Append("function deleteConfirmed")
        .Append(UniqueID)
        .Append("()")
        .Append("{")
        .Append("__doPostBack('")
        .Append(ConfirmedTargetUnique)
        .Append("', '")
        .Append(ConfirmedArgumentUnique)
        .Append("');")
        .Append("}")
        .Append("</script>");

      lbMessage.Text = Message;
      extender.OnOkScript = string.Format("deleteConfirmed{0}()", UniqueID);

      Page.RegisterClientScriptBlock(string.Format("KeyName{0}", UniqueID), sb.ToString());
    }

    #endregion
  }
}