using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.Common
{
  public partial class ConfirmControl : System.Web.UI.UserControl
  {
    public enum Mode 
    { 
      /// <summary>
      /// режим отображения - две коннпки - подтвердить, отменить
      /// </summary>
      Confirm, 

      /// <summary>
      /// режим отображения - одна кнопка закрыть
      /// </summary>
      Close 
    };

    private string _confirmedTargetBasic = "Menu";
    private string _confirmedArgumentBasic = "Delete";
    private string _message = "Вы уверены, что хотите удалить выбранную запись?";
    private Mode _mode = Mode.Confirm;

    /// <summary>
    /// Режим отображения диалогового окошка
    /// </summary>
    public Mode ConfirmMode
    {
      get { return _mode; }
      set { _mode = value; }
    }

    /// <summary>
    /// Аргумент используемый в качестве постбека
    /// </summary>
    public string ConfirmedArgumentUnique
    {
      get { return string.Format("{0}{1}", UniqueID, _confirmedArgumentBasic); }
    }

    /// <summary>
    /// Таргет используемый в качестве постбека
    /// </summary>
    public string ConfirmedTargetUnique
    {
      get { return string.Format("{0}{1}", UniqueID, _confirmedTargetBasic); }
    }

    /// <summary>
    /// Сообщение отображаемое пользователю
    /// </summary>
    public string Message
    {
      get 
      { 
        var value = ViewState[UniqueID];
        if (value != null)
        {
          _message = value.ToString();
        }
        return _message; 
      }
      set 
      {
        ViewState[UniqueID] = value;
        _message = value; 
      }
    }

    /// <summary>
    /// скрипт который надо прописать например для пункта меню в NavigateUrl для вызова данного контрола подтверждения
    /// </summary>
    public string ViewConfirmScript
    {
      get { return string.Format("javascript:ViewConfirm{0}();", UniqueID); }
    }

    /// <summary>
    /// скрипт который надо прописать например для кнонпки в OnClientClick для вызова данного контрола подтверждения
    /// </summary>
    public string ViewConfirmScriptForButton
    {
      get { return string.Format("javascript:ViewConfirm{0}(); return false", UniqueID); }
    }

    /// <summary>
    /// Устанавливает ид для того чтобы можно было вызвать из ява скрипта обращение по этому ид. 
    /// Используется в мастер странице для отображения сообщения что время сессии истекло
    /// Если на странице много контролов, то ид должны быть уникальные для правильного отображения из скрипта по ид
    /// </summary>
    /// <param name="id"></param>
    public void SetBehaviorId(string id)
    {
      extender.BehaviorID = id;
    }

    /// <summary>
    /// Отображение окошка конфирмации из серверного кода
    /// </summary>
    public void Show()
    {
      extender.Show();
    }

    /// <summary>
    /// Скрипт по кнопке отмена (закрыть)
    /// </summary>
    /// <param name="script"></param>
    public void SetCancelScript(string script)
    {
      extender.OnCancelScript = script;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (ConfirmMode == ConfirmControl.Mode.Close)
      {
        btnConfirm.Visible = false;
        btnCancel.Text = "Закрыть";
      }

      StringBuilder sb = new StringBuilder();
      sb.Append("<script type=\"text/javascript\">")
        .Append("function ViewConfirm").Append(UniqueID).Append("()")
        .Append("{")
        .Append("$find('").Append(extender.ClientID).Append("').show();")
        .Append("}")

        .Append("function deleteConfirmed").Append(UniqueID).Append("()")
        .Append("{")
        .Append("__doPostBack('").Append(ConfirmedTargetUnique).Append("', '").Append(ConfirmedArgumentUnique).Append("');")
        .Append("}")
        .Append("</script>");

      lbMessage.Text = Message;
      extender.OnOkScript = string.Format("deleteConfirmed{0}()", UniqueID);

      Page.RegisterClientScriptBlock(string.Format("KeyName{0}", UniqueID), sb.ToString());
    }
  }
}