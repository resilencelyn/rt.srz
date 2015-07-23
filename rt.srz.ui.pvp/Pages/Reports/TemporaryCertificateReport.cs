// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemporaryCertificateReport.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages.Reports
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Printing;

  using DevExpress.Utils;
  using DevExpress.XtraPrinting;
  using DevExpress.XtraPrinting.Drawing;
  using DevExpress.XtraReports.UI;

  using NHibernate;

  using rt.core.model.interfaces;
  using rt.srz.model.algorithms;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  using IContainer = System.ComponentModel.IContainer;

  /// <summary>
  ///   Summary description for StatementReport
  /// </summary>
  public class TemporaryCertificateReport : XtraReport
  {
    #region Constants

    /// <summary>
    ///   The default control height.
    /// </summary>
    private const float defaultControlHeight = 43f;

    #endregion

    #region Fields

    /// <summary>
    ///   Required designer variable.
    /// </summary>
    private readonly IContainer components = null;

    /// <summary>
    ///   The securityService.
    /// </summary>
    private readonly ISecurityService securityService;

    /// <summary>
    ///   The statement service.
    /// </summary>
    private readonly IStatementService statementService;

    /// <summary>
    ///   The bottom margin.
    /// </summary>
    private BottomMarginBand BottomMargin;

    /// <summary>
    ///   The detail.
    /// </summary>
    private DetailBand Detail;

    /// <summary>
    ///   The top margin.
    /// </summary>
    private TopMarginBand TopMargin;

    /// <summary>
    ///   The lb address phone smo.
    /// </summary>
    private XRLabel lbAddressPhoneSmo;

    /// <summary>
    ///   The lb birth place.
    /// </summary>
    private XRLabel lbBirthPlace;

    /// <summary>
    ///   The lb day 1.
    /// </summary>
    private XRLabel lbDay1;

    /// <summary>
    ///   The lb day 2.
    /// </summary>
    private XRLabel lbDay2;

    /// <summary>
    ///   The lb female.
    /// </summary>
    private XRLabel lbFemale;

    /// <summary>
    ///   The lb fio.
    /// </summary>
    private XRLabel lbFio;

    /// <summary>
    ///   The lb line 1.
    /// </summary>
    private XRLabel lbLine1;

    /// <summary>
    ///   The lb line 2.
    /// </summary>
    private XRLabel lbLine2;

    /// <summary>
    ///   The lb line 3.
    /// </summary>
    private XRLabel lbLine3;

    /// <summary>
    ///   The lb male.
    /// </summary>
    private XRLabel lbMale;

    /// <summary>
    ///   The lb month 1.
    /// </summary>
    private XRLabel lbMonth1;

    /// <summary>
    ///   The lb month 2.
    /// </summary>
    private XRLabel lbMonth2;

    /// <summary>
    ///   The lb smo.
    /// </summary>
    private XRLabel lbSmo;

    /// <summary>
    ///   The lb year 1.
    /// </summary>
    private XRLabel lbYear1;

    /// <summary>
    ///   The lb year 2.
    /// </summary>
    private XRLabel lbYear2;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="TemporaryCertificateReport" /> class.
    /// </summary>
    public TemporaryCertificateReport()
    {
      InitializeComponent();
      securityService = ObjectFactory.GetInstance<ISecurityService>();
      statementService = ObjectFactory.GetInstance<IStatementService>();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The fill report data.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    public void FillReportData(Statement statement, Template template)
    {
      SetPositions(template);

      var personData = statement.InsuredPersonData;
      var pvp = statement.PointDistributionPolicy;
      var smo = pvp.Parent;

      lbSmo.Text = smo.FullName;

      if (string.IsNullOrEmpty(smo.Phone))
      {
        lbAddressPhoneSmo.Text = smo.Address;
      }
      else
      {
        lbAddressPhoneSmo.Text = string.Format("{0} {1}", smo.Address, smo.Phone);
      }

      // else if (string.IsNullOrEmpty(za7.In1.Phone.Code))
      // {
      // lbAddressPhoneSmo.Text = string.Format("{0} {1}", za7.In1.AddressSmoInStr, za7.In1.Phone.Phone);
      // }
      // else
      // {
      // lbAddressPhoneSmo.Text = string.Format("{0} ({1}){2}", za7.In1.AddressSmoInStr, za7.In1.Phone.Code, za7.In1.Phone.Phone);
      // }
      lbBirthPlace.Text = personData.Birthplace;

      // первые даты
      if (statement.DateIssueTemporaryCertificate.HasValue)
      {
        var date = ConversionHelper.DateTimeToStringGoznak(statement.DateIssueTemporaryCertificate.Value);
        lbDay1.Text = GetDay(date);
        lbMonth1.Text = GetMonth(date);
        lbYear1.Text = GetYear(date);

        var dateIssue = statement.DateIssueTemporaryCertificate.Value;
        var endDate = statementService.CalculateEndPeriodWorkingDay(dateIssue, 30);
        statementService.CalculateEndPeriodWorkingDay(dateIssue, 30);
        date = endDate.ToString("dd.MM.yyyy");

        lbDay2.Text = GetDay(date);
        lbMonth2.Text = GetMonth(date);
        lbYear2.Text = GetYear(date);
      }

      lbLine1.Text = string.Format("{0} {1} {2}", personData.LastName, personData.FirstName, personData.MiddleName);

      var birthday = personData.Birthday.HasValue
                       ? ConversionHelper.DateTimeToStringGoznak(personData.Birthday.Value)
                       : string.Empty;
      lbLine2.Text = string.Format(
                                   "{0} {1} {2}", 
                                   GetDate(birthday), 
                                   // тип документа удл
                                   statement.DocumentUdl.DocumentType.Name, 
                                   // серия и номер
                                   statement.DocumentUdl.SeriesNumber);

      var actualFrom = statement.DocumentUdl.DateIssue.HasValue
                         ? ConversionHelper.DateTimeToStringGoznak(statement.DocumentUdl.DateIssue.Value)
                         : string.Empty;
      lbLine3.Text = string.Format(
                                   "{0} {1}", 
                                   // когда и кем выдан
                                   statement.DocumentUdl.IssuingAuthority, 
                                   GetDate(actualFrom));

      if (personData.Gender.Code == "1")
      {
        lbMale.Text = "V";
      }
      else if (personData.Gender.Code == "2")
      {
        lbFemale.Text = "V";
      }

      lbFio.Text = securityService.GetCurrentUser().Fio;

      AutoscaleControlText(lbSmo);
      AutoscaleControlText(lbAddressPhoneSmo);
      AutoscaleControlText(lbLine1);
      AutoscaleControlText(lbLine2);
      AutoscaleControlText(lbLine3);
      AutoscaleControlText(lbFio);
      AutoscaleControlText(lbBirthPlace);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">
    /// true if managed resources should be disposed; otherwise, false.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }

      base.Dispose(disposing);
    }

    /// <summary>
    /// The autoscale control text.
    /// </summary>
    /// <param name="control">
    /// The control.
    /// </param>
    private void AutoscaleControlText(XRControl control)
    {
      var controlWidth = control.SizeF.Width;
      while (MeasureTextWidthPixels(((XtraReport)control.Report.Report).ReportUnit, control.Text, control.Font)
             > controlWidth)
      {
        control.Font = new Font(control.Font.FontFamily, control.Font.Size - 0.1f, control.Font.Style);
      }
    }

    /// <summary>
    /// The get convert.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <param name="mask">
    /// The mask.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetConvert(string date, string mask)
    {
      if (string.IsNullOrEmpty(date))
      {
        return null;
      }

      var result = DateTime.Parse(date);
      return result.ToString(mask);
    }

    /// <summary>
    /// The get date.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetDate(string date)
    {
      if (string.IsNullOrEmpty(date))
      {
        return null;
      }

      var result = DateTime.Parse(date);
      return result.ToString("dd.MM.yyyy");
    }

    /// <summary>
    /// The get day.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetDay(string date)
    {
      return GetConvert(date, "dd");
    }

    /// <summary>
    /// The get month.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetMonth(string date)
    {
      return GetConvert(date, "MMMM");
    }

    /// <summary>
    /// The get position.
    /// </summary>
    /// <param name="values">
    /// The values.
    /// </param>
    /// <param name="defaultValue">
    /// The default value.
    /// </param>
    /// <returns>
    /// The <see cref="Position"/>.
    /// </returns>
    private Position GetPosition(string values, Position defaultValue)
    {
      if (string.IsNullOrEmpty(values))
      {
        return defaultValue;
      }

      var result = new Position(defaultValue.Left, defaultValue.Bottom, defaultValue.Width);
      var vals = values.Split(';');
      if (vals.Length >= 1)
      {
        result.Left = int.Parse(vals[0]) * 10;
      }

      if (vals.Length >= 2)
      {
        result.Bottom = int.Parse(vals[1]) * 10;
      }

      if (vals.Length >= 3)
      {
        result.Width = int.Parse(vals[2]) * 10;
      }

      // если в позиции по ширине, лево и верх нули то используем значения по умолчанию
      result.Left = result.Left == 0 ? defaultValue.Left : result.Left;
      result.Bottom = result.Bottom == 0 ? defaultValue.Bottom : result.Bottom;
      result.Width = result.Width == 0 ? defaultValue.Width : result.Width;
      return result;
    }

    /// <summary>
    /// The get year.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetYear(string date)
    {
      var result = GetConvert(date, "yyyy");
      return !string.IsNullOrEmpty(result) ? result.Substring(2, 2) : null;
    }

    /// <summary>
    ///   Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      var resources = new ComponentResourceManager(typeof(TemporaryCertificateReport));
      Detail = new DetailBand();
      lbBirthPlace = new XRLabel();
      lbLine3 = new XRLabel();
      lbLine2 = new XRLabel();
      lbLine1 = new XRLabel();
      lbFio = new XRLabel();
      lbMonth2 = new XRLabel();
      lbYear2 = new XRLabel();
      lbDay2 = new XRLabel();
      lbDay1 = new XRLabel();
      lbYear1 = new XRLabel();
      lbMonth1 = new XRLabel();
      lbFemale = new XRLabel();
      lbMale = new XRLabel();
      lbAddressPhoneSmo = new XRLabel();
      lbSmo = new XRLabel();
      TopMargin = new TopMarginBand();
      BottomMargin = new BottomMarginBand();
      ((ISupportInitialize)this).BeginInit();

      // Detail
      Detail.BackColor = Color.Transparent;
      Detail.Borders = BorderSide.None;
      Detail.Controls.AddRange(
                               new XRControl[]
                               {
                                 lbBirthPlace, lbLine3, lbLine2, lbLine1, lbFio, lbMonth2, lbYear2, lbDay2, lbDay1, 
                                 lbYear1, lbMonth1, lbFemale, lbMale, lbAddressPhoneSmo, lbSmo
                               });
      Detail.Dpi = 254F;
      Detail.Font = new Font("Times New Roman", 11F);
      Detail.HeightF = 1867F;
      Detail.Name = "Detail";
      Detail.Padding = new PaddingInfo(0, 0, 0, 0, 254F);
      Detail.StylePriority.UseBackColor = false;
      Detail.StylePriority.UseBorders = false;
      Detail.StylePriority.UseFont = false;
      Detail.TextAlignment = TextAlignment.TopLeft;

      // lbBirthPlace
      lbBirthPlace.BorderColor = Color.Red;
      lbBirthPlace.Borders = BorderSide.None;
      lbBirthPlace.CanGrow = false;
      lbBirthPlace.Dpi = 254F;
      lbBirthPlace.LocationFloat = new PointFloat(683.9026F, 992.0817F);
      lbBirthPlace.Name = "lbBirthPlace";
      lbBirthPlace.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbBirthPlace.SizeF = new SizeF(1224.27F, 43F);
      lbBirthPlace.StylePriority.UseBorderColor = false;
      lbBirthPlace.StylePriority.UseBorders = false;
      lbBirthPlace.Text = "lbBirthPlace";

      // lbLine3
      lbLine3.BorderColor = Color.Red;
      lbLine3.Borders = BorderSide.None;
      lbLine3.CanGrow = false;
      lbLine3.Dpi = 254F;
      lbLine3.LocationFloat = new PointFloat(86.15535F, 896.091F);
      lbLine3.Name = "lbLine3";
      lbLine3.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbLine3.SizeF = new SizeF(1822.02F, 43F);
      lbLine3.StylePriority.UseBorderColor = false;
      lbLine3.StylePriority.UseBorders = false;
      lbLine3.Text = "lbLine3";

      // lbLine2
      lbLine2.BorderColor = Color.Red;
      lbLine2.Borders = BorderSide.None;
      lbLine2.CanGrow = false;
      lbLine2.Dpi = 254F;
      lbLine2.LocationFloat = new PointFloat(86.15967F, 774.2768F);
      lbLine2.Name = "lbLine2";
      lbLine2.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbLine2.SizeF = new SizeF(1822.02F, 43F);
      lbLine2.StylePriority.UseBorderColor = false;
      lbLine2.StylePriority.UseBorders = false;
      lbLine2.Text = "lbLine2";

      // lbLine1
      lbLine1.CanGrow = false;
      lbLine1.Dpi = 254F;
      lbLine1.LocationFloat = new PointFloat(852.1696F, 650.1341F);
      lbLine1.Name = "lbLine1";
      lbLine1.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbLine1.SizeF = new SizeF(1056.01F, 43F);
      lbLine1.Text = "lbLine1";

      // lbFio
      lbFio.CanGrow = false;
      lbFio.Dpi = 254F;
      lbFio.LocationFloat = new PointFloat(821.911F, 1489.498F);
      lbFio.Name = "lbFio";
      lbFio.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbFio.SizeF = new SizeF(1107.44F, 43F);
      lbFio.Text = "lbFio";

      // lbMonth2
      lbMonth2.CanGrow = false;
      lbMonth2.Dpi = 254F;
      lbMonth2.LocationFloat = new PointFloat(714.9119F, 1290.638F);
      lbMonth2.Name = "lbMonth2";
      lbMonth2.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbMonth2.SizeF = new SizeF(254F, 43F);
      lbMonth2.Text = "lbMonth1";

      // lbYear2
      lbYear2.CanGrow = false;
      lbYear2.Dpi = 254F;
      lbYear2.LocationFloat = new PointFloat(1074.745F, 1290.638F);
      lbYear2.Name = "lbYear2";
      lbYear2.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbYear2.SizeF = new SizeF(124.35F, 43F);
      lbYear2.Text = "lbYear1";

      // lbDay2
      lbDay2.CanGrow = false;
      lbDay2.Dpi = 254F;
      lbDay2.LocationFloat = new PointFloat(590.3396F, 1290.638F);
      lbDay2.Name = "lbDay2";
      lbDay2.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbDay2.SizeF = new SizeF(61.38F, 43F);
      lbDay2.Text = "lbDay1";

      // lbDay1
      lbDay1.CanGrow = false;
      lbDay1.Dpi = 254F;
      lbDay1.LocationFloat = new PointFloat(565.6874F, 505.46F);
      lbDay1.Name = "lbDay1";
      lbDay1.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbDay1.SizeF = new SizeF(61.38F, 43F);
      lbDay1.Text = "lbDay1";

      // lbYear1
      lbYear1.CanGrow = false;
      lbYear1.Dpi = 254F;
      lbYear1.LocationFloat = new PointFloat(1043.736F, 508F);
      lbYear1.Name = "lbYear1";
      lbYear1.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbYear1.SizeF = new SizeF(124.35F, 43F);
      lbYear1.Text = "lbYear1";

      // lbMonth1
      lbMonth1.CanGrow = false;
      lbMonth1.Dpi = 254F;
      lbMonth1.LocationFloat = new PointFloat(683.9026F, 508F);
      lbMonth1.Name = "lbMonth1";
      lbMonth1.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbMonth1.SizeF = new SizeF(254F, 43F);
      lbMonth1.Text = "lbMonth1";

      // lbFemale
      lbFemale.Borders = BorderSide.None;
      lbFemale.CanGrow = false;
      lbFemale.Dpi = 254F;
      lbFemale.LocationFloat = new PointFloat(642.3109F, 1119.082F);
      lbFemale.Name = "lbFemale";
      lbFemale.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbFemale.SizeF = new SizeF(63.5F, 43F);
      lbFemale.StylePriority.UseBorders = false;
      lbFemale.StylePriority.UseTextAlignment = false;
      lbFemale.TextAlignment = TextAlignment.TopCenter;

      // lbMale
      lbMale.Borders = BorderSide.None;
      lbMale.CanGrow = false;
      lbMale.Dpi = 254F;
      lbMale.LocationFloat = new PointFloat(385.2329F, 1119.082F);
      lbMale.Name = "lbMale";
      lbMale.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbMale.SizeF = new SizeF(63.5F, 43F);
      lbMale.StylePriority.UseBorders = false;
      lbMale.StylePriority.UseTextAlignment = false;
      lbMale.TextAlignment = TextAlignment.TopCenter;

      // lbAddressPhoneSmo
      lbAddressPhoneSmo.AutoWidth = true;
      lbAddressPhoneSmo.CanGrow = false;
      lbAddressPhoneSmo.Dpi = 254F;
      lbAddressPhoneSmo.Font = new Font("Times New Roman", 11F);
      lbAddressPhoneSmo.LocationFloat = new PointFloat(586.5286F, 142.875F);
      lbAddressPhoneSmo.Name = "lbAddressPhoneSmo";
      lbAddressPhoneSmo.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbAddressPhoneSmo.SizeF = new SizeF(1297.83F, 43F);
      lbAddressPhoneSmo.StylePriority.UseFont = false;
      lbAddressPhoneSmo.StylePriority.UseTextAlignment = false;
      lbAddressPhoneSmo.Text = "lbAddressPhoneSmo";
      lbAddressPhoneSmo.TextAlignment = TextAlignment.TopLeft;

      // lbSmo
      lbSmo.CanGrow = false;
      lbSmo.Dpi = 254F;
      lbSmo.Font = new Font("Times New Roman", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
      lbSmo.LocationFloat = new PointFloat(586.5286F, 0F);
      lbSmo.Multiline = true;
      lbSmo.Name = "lbSmo";
      lbSmo.Padding = new PaddingInfo(5, 5, 0, 0, 254F);
      lbSmo.SizeF = new SizeF(1297.83F, 43F);
      lbSmo.StylePriority.UseFont = false;
      lbSmo.StylePriority.UseTextAlignment = false;
      lbSmo.Text = "lbSmo";
      lbSmo.TextAlignment = TextAlignment.TopLeft;

      // TopMargin
      TopMargin.Borders = BorderSide.None;
      TopMargin.Dpi = 254F;
      TopMargin.HeightF = 0F;
      TopMargin.Name = "TopMargin";
      TopMargin.Padding = new PaddingInfo(0, 0, 0, 0, 254F);
      TopMargin.StylePriority.UseBorders = false;
      TopMargin.TextAlignment = TextAlignment.TopLeft;

      // BottomMargin
      BottomMargin.Dpi = 254F;
      BottomMargin.HeightF = 0F;
      BottomMargin.Name = "BottomMargin";
      BottomMargin.Padding = new PaddingInfo(0, 0, 0, 0, 254F);
      BottomMargin.TextAlignment = TextAlignment.TopLeft;

      // TemporaryCertificateReport
      Bands.AddRange(new Band[] { Detail, TopMargin, BottomMargin });
      Dpi = 254F;
      Landscape = true;
      Margins = new Margins(0, 0, 0, 0);
      PageHeight = 1480;
      PageWidth = 2100;
      PaperKind = PaperKind.A5;
      ReportUnit = ReportUnit.TenthsOfAMillimeter;
      SnapGridSize = 31.75F;
      Version = "13.1";
      Watermark.Image = (Image)resources.GetObject("TemporaryCertificateReport.Watermark.Image");
      Watermark.ImageViewMode = ImageViewMode.Zoom;
      BeforePrint += TemporaryCertificateReport_BeforePrint;
      ((ISupportInitialize)this).EndInit();
    }

    /// <summary>
    /// The measure text width pixels.
    /// </summary>
    /// <param name="unit">
    /// The unit.
    /// </param>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <param name="font">
    /// The font.
    /// </param>
    /// <returns>
    /// The <see cref="float"/>.
    /// </returns>
    private float MeasureTextWidthPixels(ReportUnit unit, string text, Font font)
    {
      var gr = Graphics.FromHwnd(IntPtr.Zero);

      int factor;
      if (unit == ReportUnit.HundredthsOfAnInch)
      {
        gr.PageUnit = GraphicsUnit.Inch;
        factor = 100;
      }
      else
      {
        gr.PageUnit = GraphicsUnit.Millimeter;
        factor = 10;
      }

      var size = gr.MeasureString(text, font);
      gr.Dispose();

      return size.Width * factor;
    }

    /// <summary>
    /// The set position.
    /// </summary>
    /// <param name="label">
    /// The label.
    /// </param>
    /// <param name="position">
    /// The position.
    /// </param>
    /// <param name="defaultPosition">
    /// The default position.
    /// </param>
    private void SetPosition(XRLabel label, string position, Position defaultPosition)
    {
      var pos = GetPosition(position, defaultPosition);
      label.HeightF = defaultControlHeight;
      label.LeftF = pos.Left;
      label.TopF = pos.Bottom - defaultControlHeight;
      label.WidthF = pos.Width;
    }

    /// <summary>
    /// The set positions.
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    private void SetPositions(Template template)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // если не найден шаблон (в том числе по умолчанию), то берём пустой объект - там в позициях будут нули и возьмётся значение по умолчанию при печати
      if (template == null)
      {
        template = new Template();
      }

      session.Evict(template);

      SetPosition(lbSmo, template.PosSmo, new Position(350f, 100f + defaultControlHeight, 1200f));
      SetPosition(lbAddressPhoneSmo, template.PosAddress, new Position(350f, 150f + defaultControlHeight, 1200f));
      SetPosition(lbDay1, template.PosDay1, new Position(350f, 400f + defaultControlHeight, 50f));
      SetPosition(lbMonth1, template.PosMonth1, new Position(420f, 400f + defaultControlHeight, 170f));
      SetPosition(lbYear1, template.PosYear1, new Position(640f, 400f + defaultControlHeight, 50f));
      SetPosition(lbDay2, template.PosDay2, new Position(870f, 800f + defaultControlHeight, 50f));
      SetPosition(lbMonth2, template.PosMonth2, new Position(940f, 800f + defaultControlHeight, 180f));
      SetPosition(lbYear2, template.PosYear2, new Position(1170f, 800f + defaultControlHeight, 50f));
      SetPosition(lbLine1, template.PosLine1, new Position(700f, 480f + defaultControlHeight, 820f));
      SetPosition(lbLine2, template.PosLin2, new Position(120f, 540f + defaultControlHeight, 1400f));
      SetPosition(lbLine3, template.PosLine3, new Position(120f, 600f + defaultControlHeight, 1400f));
      SetPosition(lbBirthPlace, template.PosBirthplace, new Position(340f, 655f + defaultControlHeight, 1150f));
      SetPosition(lbMale, template.PosMale, new Position(280f, 720f + defaultControlHeight, 40f));
      SetPosition(lbFemale, template.PosFemale, new Position(490f, 720f + defaultControlHeight, 40f));
      SetPosition(lbFio, template.PosFio, new Position(820f, 970f + defaultControlHeight, 400f));
    }

    /// <summary>
    /// The temporary certificate report_ before print.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void TemporaryCertificateReport_BeforePrint(object sender, PrintEventArgs e)
    {
    }

    #endregion

    /// <summary>
    ///   The position.
    /// </summary>
    private class Position
    {
      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="Position"/> class.
      /// </summary>
      /// <param name="left">
      /// The left.
      /// </param>
      /// <param name="bottom">
      /// The bottom.
      /// </param>
      /// <param name="width">
      /// The width.
      /// </param>
      public Position(float left, float bottom, float width)
      {
        Width = width;
        Left = left;
        Bottom = bottom;
      }

      #endregion

      #region Public Properties

      /// <summary>
      ///   Gets or sets the bottom.
      /// </summary>
      public float Bottom { get; set; }

      /// <summary>
      ///   Gets or sets the left.
      /// </summary>
      public float Left { get; set; }

      /// <summary>
      ///   Gets or sets the width.
      /// </summary>
      public float Width { get; set; }

      #endregion
    }
  }
}