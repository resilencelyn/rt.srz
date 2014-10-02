// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentUDLUserControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  #region

  using System;
  using System.Linq;
  using System.Globalization;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.srz.ui.pvp.Enumerations;

  #endregion

  /// <summary>
  ///   The document udl user control.
  /// </summary>
  public partial class DocumentUdlUserControl : UserControl
  {
    #region Public Properties

    /// <summary>
    ///   Gets the additional series client id.
    /// </summary>
    public string AdditionalSeriesClientId
    {
      get
      {
        return tbAdditionalSeries.ClientID;
      }
    }

    /// <summary>
    ///   Дата выдачи
    /// </summary>
    public DateTime? DocumentIssueDate
    {
      get
      {
        DateTime value;
        if (!DateTime.TryParse(tbIssueDate.Text, out value))
        {
          return null;
        }

        return value;
      }

      set
      {
        tbIssueDate.Text = value == null ? string.Empty : value.Value.ToShortDateString();
      }
    }

    /// <summary>
    ///   Действует до
    /// </summary>
    public DateTime? DocumentExpDate
    {
      get
      {
        DateTime value;
        if (!DateTime.TryParse(tbDateExp.Text, out value))
        {
          return null;
        }

        return value;
      }

      set
      {
        tbDateExp.Text = value == null ? string.Empty : value.Value.ToShortDateString();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether valid document.
    /// </summary>
    public bool ValidDocument
    {
      get
      {
        return chbValidDocument.Checked;
      }
      set
      {
        chbValidDocument.Checked = value;
      }
    }

    /// <summary>
    ///   Выдавший орган
    /// </summary>
    public string DocumentIssuingAuthority
    {
      get
      {
        return tbIssuingAuthority.Text;
      }

      set
      {
        tbIssuingAuthority.Text = value;
      }
    }

    /// <summary>
    ///   Номер документа
    /// </summary>
    public string DocumentNumber
    {
      get
      {
        return tbNumber.Text;
      }

      set
      {
        tbNumber.Text = value;
      }
    }

    /// <summary>
    ///   Серия документа
    /// </summary>
    public string DocumentSeries
    {
      get
      {
        // Серия
        if (DocumentType == model.srz.concepts.DocumentType.BirthCertificateRf)
        {
          return tbSeries.Text + "-" + tbAdditionalSeries.Text;
        }

        return tbSeries.Text;
      }

      set
      {
        if (value == null)
        {
          return;
        }

        string series;
        string additionalSeries;
        UdlHelper.GetSeries(DocumentType, value, out series, out additionalSeries);
        tbSeries.Text = series;
        tbAdditionalSeries.Text = additionalSeries;
      }
    }

    /// <summary>
    ///   Тип документа
    /// </summary>
    public int DocumentType
    {
      get
      {
        var i = int.Parse(ddlDocumentType.SelectedValue);
        if (i == -393)
        {
          i = 393;
        }

        return i;
      }

      set
      {
        if (ddlDocumentType.Items.FindByValue(value.ToString()) != null)
        {
          ddlDocumentType.SelectedValue = value.ToString(CultureInfo.InvariantCulture);
          hfSelectedDocType.Value = value.ToString(CultureInfo.InvariantCulture);
        }
      }
    }

    /// <summary>
    /// Тип документа строкой
    /// </summary>
    public string DocumentTypeStr
    {
      get { return ddlDocumentType.SelectedItem.Text; }
    }

    /// <summary>
    ///   Gets the document type client id.
    /// </summary>
    public string DocumentTypeClientId
    {
      get
      {
        return ddlDocumentType.ClientID;
      }
    }

    /// <summary>
    ///   Тип документа
    /// </summary>
    public int DocumentTypeInHf
    {
      get
      {
        if (!string.IsNullOrEmpty(hfSelectedDocType.Value))
        {
          return int.Parse(hfSelectedDocType.Value);
        }

        return -1;
      }
    }

    /// <summary>
    ///   Gets the number client id.
    /// </summary>
    public string NumberClientId
    {
      get
      {
        return tbNumber.ClientID;
      }
    }

    /// <summary>
    ///   Gets the series client id.
    /// </summary>
    public string SeriesClientId
    {
      get
      {
        return tbSeries.ClientID;
      }
    }

    #endregion

    #region Properties

    /// <summary>
    ///   The unique key.
    /// </summary>
    protected string UniqueKey { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add number attribute.
    /// </summary>
    /// <param name="key">
    /// The key. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public void AddNumberAttribute(string key, string value)
    {
      tbNumber.Attributes.Add(key, value);
    }

    public void EnableDocumentType(bool enable)
    {
      ddlDocumentType.Enabled = enable;
    }
    
    /// <summary>
    /// The enable.
    /// </summary>
    /// <param name="enable">
    /// The b enable. 
    /// </param>
    public void Enable(bool enable)
    {
      ddlDocumentType.Enabled = enable;
      tbSeries.Enabled = enable;
      tbAdditionalSeries.Enabled = enable;
      tbNumber.Enabled = enable;
      tbIssuingAuthority.Enabled = enable;
      tbIssueDate.Enabled = enable;
      tbDateExp.Enabled = enable;
    }

    /// <summary>
    /// Исполльзуется родительским компонентом для заполнения списка доступных документов
    /// </summary>
    /// <param name="documentList">
    /// The document List. 
    /// </param>
    /// <param name="selectedValue">
    /// The selected Value. 
    /// </param>
    public void FillDocumentTypeDdl(ListItem[] documentList, string selectedValue)
    {
      ddlDocumentType.Items.Clear();
      ddlDocumentType.Items.AddRange(documentList);
      if (!string.IsNullOrEmpty(selectedValue))
      {
        ddlDocumentType.SelectedValue = selectedValue;
      }
    }

    /// <summary>
    ///   The set focus first control.
    /// </summary>
    public void SetFocusFirstControl()
    {
      ddlDocumentType.Focus();
    }

    /// <summary>
    ///   The set simple mode.
    /// </summary>
    public void SetSimpleMode()
    {
      tbIssuingAuthority.Enabled = false;
      tbIssueDate.Enabled = false;
    }

    /// <summary>
    /// The show check box valid document.
    /// </summary>
    public void ShowCheckBoxValidDocument ()
    {
      chbValidDocument.Visible = true;
    }

    /// <summary>
    ///   The update document type.
    /// </summary>
    public void UpdateDocumentType()
    {
      DocTypeUpdatePanel.Update();
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
      UniqueKey = UniqueID; //Guid.NewGuid().ToString("N");
      ddlDocumentType.Attributes["onchange"] = "documentTypeChanged_" + UniqueKey + "()";
      tbSeries.Attributes["onkeydown"] = "translateKey_" + UniqueKey + "()";
      tbAdditionalSeries.Attributes["onkeypress"] = "toUpper_" + UniqueKey + "()";
      tbNumber.Attributes["onkeydown"] = "documentChanged_" + UniqueKey + "()";
      tbIssuingAuthority.Attributes["onkeydown"] = "documentChanged_" + UniqueKey + "()";
      tbIssueDate.Attributes["onchange"] = "documentChanged_" + UniqueKey + "()";
    }

    #endregion
  }
}