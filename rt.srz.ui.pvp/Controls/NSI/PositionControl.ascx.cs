using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.NSI
{
  /// <summary>
  /// Контрол для отображения позиции поля в миллиметрах во временном свидетельстве
  /// </summary>
  public partial class PositionControl : System.Web.UI.UserControl
  {
    /// <summary>
    /// Название поля
    /// </summary>
    public string Title
    {
      get { return lbTitle.Text; }
      set { lbTitle.Text = value; }
    }

    /// <summary>
    /// Левая координата в миллиметрах
    /// </summary>
    public int Left
    {
      get { return string.IsNullOrEmpty(tbLeft.Text) ? 0 : int.Parse(tbLeft.Text); }
      set { tbLeft.Text = value.ToString(); }
    }

    /// <summary>
    /// Верхняя координата в миллиметрах
    /// </summary>
    public int Bottom
    {
      get { return string.IsNullOrEmpty(tbBottom.Text) ? 0 : int.Parse(tbBottom.Text); }
      set { tbBottom.Text = value.ToString(); }
    }

    public int Width
    {
      get { return string.IsNullOrEmpty(tbWidth.Text) ? 0 : int.Parse(tbWidth.Text); }
      set { tbWidth.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Результирующее значение как одна строка
    /// </summary>
    /// <returns></returns>
    public string GetData()
    {
      return string.Format("{0};{1};{2}", Left, Bottom, Width);
    }

    /// <summary>
    /// Установка значений из строки (разделителем является ';')
    /// </summary>
    /// <param name="values"></param>
    /// <param name="title"></param>
    public void SetData(string values, string title)
    {
      Title = title;
      if (string.IsNullOrEmpty(values))
      {
        return;
      }
      var vals = values.Split(';');
      if (vals.Length >= 1)
      {
        Left = int.Parse(vals[0]);
      }
      if (vals.Length >= 2)
      {
        Bottom = int.Parse(vals[1]);
      }
      if (vals.Length >= 3)
      {
        Width = int.Parse(vals[2]);
      }
    }

    protected void cv_ServerValidate(object source, ServerValidateEventArgs args)
    {
      args.IsValid = Left + Width <= 210;
    }

  }
}