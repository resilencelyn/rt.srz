using System;
using System.Drawing;
using StructureMap;
using NHibernate;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Drawing;
using rt.srz.model.srz;
using rt.srz.model.interfaces.service;
using rt.srz.model.HL7.person.messages;
using rt.srz.business.configuration.algorithms;

namespace rt.srz.ui.pvp.Pages.Reports
{
  /// <summary>
  /// Summary description for StatementReport
  /// </summary>
  public class TemporaryCertificateReport : DevExpress.XtraReports.UI.XtraReport
  {
    #region report controls

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel lbSmo;
    private XRLabel lbAddressPhoneSmo;
    private XRLabel lbFemale;
    private XRLabel lbMale;
    private XRLabel lbDay1;
    private XRLabel lbYear1;
    private XRLabel lbMonth1;
    private XRLabel lbMonth2;
    private XRLabel lbYear2;
    private XRLabel lbDay2;
    private XRLabel lbLine1;
    private XRLabel lbFio;
    private XRLabel lbLine2;
    private XRLabel lbLine3;

    #endregion
    private XRLabel lbBirthPlace;

    private ISecurityService _sec;
    private const float defaultControlHeight = 43f;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public TemporaryCertificateReport()
    {
      InitializeComponent();
      _sec = ObjectFactory.GetInstance<ISecurityService>();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemporaryCertificateReport));
      this.Detail = new DevExpress.XtraReports.UI.DetailBand();
      this.lbBirthPlace = new DevExpress.XtraReports.UI.XRLabel();
      this.lbLine3 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbLine2 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbLine1 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFio = new DevExpress.XtraReports.UI.XRLabel();
      this.lbMonth2 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbYear2 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbDay2 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbDay1 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbYear1 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbMonth1 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFemale = new DevExpress.XtraReports.UI.XRLabel();
      this.lbMale = new DevExpress.XtraReports.UI.XRLabel();
      this.lbAddressPhoneSmo = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSmo = new DevExpress.XtraReports.UI.XRLabel();
      this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
      this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // Detail
      // 
      this.Detail.BackColor = System.Drawing.Color.Transparent;
      this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbBirthPlace,
            this.lbLine3,
            this.lbLine2,
            this.lbLine1,
            this.lbFio,
            this.lbMonth2,
            this.lbYear2,
            this.lbDay2,
            this.lbDay1,
            this.lbYear1,
            this.lbMonth1,
            this.lbFemale,
            this.lbMale,
            this.lbAddressPhoneSmo,
            this.lbSmo});
      this.Detail.Dpi = 254F;
      this.Detail.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.Detail.HeightF = 1867F;
      this.Detail.Name = "Detail";
      this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
      this.Detail.StylePriority.UseBackColor = false;
      this.Detail.StylePriority.UseBorders = false;
      this.Detail.StylePriority.UseFont = false;
      this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbBirthPlace
      // 
      this.lbBirthPlace.BorderColor = System.Drawing.Color.Red;
      this.lbBirthPlace.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.lbBirthPlace.CanGrow = false;
      this.lbBirthPlace.Dpi = 254F;
      this.lbBirthPlace.LocationFloat = new DevExpress.Utils.PointFloat(683.9026F, 992.0817F);
      this.lbBirthPlace.Name = "lbBirthPlace";
      this.lbBirthPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbBirthPlace.SizeF = new System.Drawing.SizeF(1224.27F, 43F);
      this.lbBirthPlace.StylePriority.UseBorderColor = false;
      this.lbBirthPlace.StylePriority.UseBorders = false;
      this.lbBirthPlace.Text = "lbBirthPlace";
      // 
      // lbLine3
      // 
      this.lbLine3.BorderColor = System.Drawing.Color.Red;
      this.lbLine3.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.lbLine3.CanGrow = false;
      this.lbLine3.Dpi = 254F;
      this.lbLine3.LocationFloat = new DevExpress.Utils.PointFloat(86.15535F, 896.091F);
      this.lbLine3.Name = "lbLine3";
      this.lbLine3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbLine3.SizeF = new System.Drawing.SizeF(1822.02F, 43F);
      this.lbLine3.StylePriority.UseBorderColor = false;
      this.lbLine3.StylePriority.UseBorders = false;
      this.lbLine3.Text = "lbLine3";
      // 
      // lbLine2
      // 
      this.lbLine2.BorderColor = System.Drawing.Color.Red;
      this.lbLine2.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.lbLine2.CanGrow = false;
      this.lbLine2.Dpi = 254F;
      this.lbLine2.LocationFloat = new DevExpress.Utils.PointFloat(86.15967F, 774.2768F);
      this.lbLine2.Name = "lbLine2";
      this.lbLine2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbLine2.SizeF = new System.Drawing.SizeF(1822.02F, 43F);
      this.lbLine2.StylePriority.UseBorderColor = false;
      this.lbLine2.StylePriority.UseBorders = false;
      this.lbLine2.Text = "lbLine2";
      // 
      // lbLine1
      // 
      this.lbLine1.CanGrow = false;
      this.lbLine1.Dpi = 254F;
      this.lbLine1.LocationFloat = new DevExpress.Utils.PointFloat(852.1696F, 650.1341F);
      this.lbLine1.Name = "lbLine1";
      this.lbLine1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbLine1.SizeF = new System.Drawing.SizeF(1056.01F, 43F);
      this.lbLine1.Text = "lbLine1";
      // 
      // lbFio
      // 
      this.lbFio.CanGrow = false;
      this.lbFio.Dpi = 254F;
      this.lbFio.LocationFloat = new DevExpress.Utils.PointFloat(821.911F, 1489.498F);
      this.lbFio.Name = "lbFio";
      this.lbFio.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbFio.SizeF = new System.Drawing.SizeF(1107.44F, 43F);
      this.lbFio.Text = "lbFio";
      // 
      // lbMonth2
      // 
      this.lbMonth2.CanGrow = false;
      this.lbMonth2.Dpi = 254F;
      this.lbMonth2.LocationFloat = new DevExpress.Utils.PointFloat(714.9119F, 1290.638F);
      this.lbMonth2.Name = "lbMonth2";
      this.lbMonth2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbMonth2.SizeF = new System.Drawing.SizeF(254F, 43F);
      this.lbMonth2.Text = "lbMonth1";
      // 
      // lbYear2
      // 
      this.lbYear2.CanGrow = false;
      this.lbYear2.Dpi = 254F;
      this.lbYear2.LocationFloat = new DevExpress.Utils.PointFloat(1074.745F, 1290.638F);
      this.lbYear2.Name = "lbYear2";
      this.lbYear2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbYear2.SizeF = new System.Drawing.SizeF(124.35F, 43F);
      this.lbYear2.Text = "lbYear1";
      // 
      // lbDay2
      // 
      this.lbDay2.CanGrow = false;
      this.lbDay2.Dpi = 254F;
      this.lbDay2.LocationFloat = new DevExpress.Utils.PointFloat(590.3396F, 1290.638F);
      this.lbDay2.Name = "lbDay2";
      this.lbDay2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbDay2.SizeF = new System.Drawing.SizeF(61.38F, 43F);
      this.lbDay2.Text = "lbDay1";
      // 
      // lbDay1
      // 
      this.lbDay1.CanGrow = false;
      this.lbDay1.Dpi = 254F;
      this.lbDay1.LocationFloat = new DevExpress.Utils.PointFloat(565.6874F, 505.46F);
      this.lbDay1.Name = "lbDay1";
      this.lbDay1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbDay1.SizeF = new System.Drawing.SizeF(61.38F, 43F);
      this.lbDay1.Text = "lbDay1";
      // 
      // lbYear1
      // 
      this.lbYear1.CanGrow = false;
      this.lbYear1.Dpi = 254F;
      this.lbYear1.LocationFloat = new DevExpress.Utils.PointFloat(1043.736F, 508F);
      this.lbYear1.Name = "lbYear1";
      this.lbYear1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbYear1.SizeF = new System.Drawing.SizeF(124.35F, 43F);
      this.lbYear1.Text = "lbYear1";
      // 
      // lbMonth1
      // 
      this.lbMonth1.CanGrow = false;
      this.lbMonth1.Dpi = 254F;
      this.lbMonth1.LocationFloat = new DevExpress.Utils.PointFloat(683.9026F, 508F);
      this.lbMonth1.Name = "lbMonth1";
      this.lbMonth1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbMonth1.SizeF = new System.Drawing.SizeF(254F, 43F);
      this.lbMonth1.Text = "lbMonth1";
      // 
      // lbFemale
      // 
      this.lbFemale.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.lbFemale.CanGrow = false;
      this.lbFemale.Dpi = 254F;
      this.lbFemale.LocationFloat = new DevExpress.Utils.PointFloat(642.3109F, 1119.082F);
      this.lbFemale.Name = "lbFemale";
      this.lbFemale.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbFemale.SizeF = new System.Drawing.SizeF(63.5F, 43F);
      this.lbFemale.StylePriority.UseBorders = false;
      this.lbFemale.StylePriority.UseTextAlignment = false;
      this.lbFemale.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
      // 
      // lbMale
      // 
      this.lbMale.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.lbMale.CanGrow = false;
      this.lbMale.Dpi = 254F;
      this.lbMale.LocationFloat = new DevExpress.Utils.PointFloat(385.2329F, 1119.082F);
      this.lbMale.Name = "lbMale";
      this.lbMale.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbMale.SizeF = new System.Drawing.SizeF(63.5F, 43F);
      this.lbMale.StylePriority.UseBorders = false;
      this.lbMale.StylePriority.UseTextAlignment = false;
      this.lbMale.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
      // 
      // lbAddressPhoneSmo
      // 
      this.lbAddressPhoneSmo.AutoWidth = true;
      this.lbAddressPhoneSmo.CanGrow = false;
      this.lbAddressPhoneSmo.Dpi = 254F;
      this.lbAddressPhoneSmo.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbAddressPhoneSmo.LocationFloat = new DevExpress.Utils.PointFloat(586.5286F, 142.875F);
      this.lbAddressPhoneSmo.Name = "lbAddressPhoneSmo";
      this.lbAddressPhoneSmo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbAddressPhoneSmo.SizeF = new System.Drawing.SizeF(1297.83F, 43F);
      this.lbAddressPhoneSmo.StylePriority.UseFont = false;
      this.lbAddressPhoneSmo.StylePriority.UseTextAlignment = false;
      this.lbAddressPhoneSmo.Text = "lbAddressPhoneSmo";
      this.lbAddressPhoneSmo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbSmo
      // 
      this.lbSmo.CanGrow = false;
      this.lbSmo.Dpi = 254F;
      this.lbSmo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lbSmo.LocationFloat = new DevExpress.Utils.PointFloat(586.5286F, 0F);
      this.lbSmo.Multiline = true;
      this.lbSmo.Name = "lbSmo";
      this.lbSmo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
      this.lbSmo.SizeF = new System.Drawing.SizeF(1297.83F, 43F);
      this.lbSmo.StylePriority.UseFont = false;
      this.lbSmo.StylePriority.UseTextAlignment = false;
      this.lbSmo.Text = "lbSmo";
      this.lbSmo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // TopMargin
      // 
      this.TopMargin.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.TopMargin.Dpi = 254F;
      this.TopMargin.HeightF = 0F;
      this.TopMargin.Name = "TopMargin";
      this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
      this.TopMargin.StylePriority.UseBorders = false;
      this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // BottomMargin
      // 
      this.BottomMargin.Dpi = 254F;
      this.BottomMargin.HeightF = 0F;
      this.BottomMargin.Name = "BottomMargin";
      this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
      this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // TemporaryCertificateReport
      // 
      this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
      this.Dpi = 254F;
      this.Landscape = true;
      this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
      this.PageHeight = 1480;
      this.PageWidth = 2100;
      this.PaperKind = System.Drawing.Printing.PaperKind.A5;
      this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
      this.SnapGridSize = 31.75F;
      this.Version = "13.1";
      this.Watermark.Image = ((System.Drawing.Image)(resources.GetObject("TemporaryCertificateReport.Watermark.Image")));
      this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Zoom;
      this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.TemporaryCertificateReport_BeforePrint);
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Fill Data

    private class Position
    {
      public float Width { get; set; }
      public float Left { get; set; }
      public float Bottom { get; set; }

      public Position(float left, float bottom, float width)
      {
        Width = width;
        Left = left;
        Bottom = bottom;
      }
    }

    private Position GetPosition(string values, Position defaultValue)
    {
      if (string.IsNullOrEmpty(values))
      {
        return defaultValue;
      }
      Position result = new Position(defaultValue.Left, defaultValue.Bottom, defaultValue.Width);
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
      //если в позиции по ширине, лево и верх нули то используем значения по умолчанию
      result.Left = result.Left == 0 ? defaultValue.Left : result.Left;
      result.Bottom = result.Bottom == 0 ? defaultValue.Bottom : result.Bottom;
      result.Width = result.Width == 0 ? defaultValue.Width : result.Width;
      return result;
    }

    private void SetPosition(XRLabel label, string position, Position defaultPosition)
    {
      Position pos = GetPosition(position, defaultPosition);
      label.HeightF = defaultControlHeight;
      label.LeftF = pos.Left;
      label.TopF = pos.Bottom - defaultControlHeight;
      label.WidthF = pos.Width;
    }

    private void SetPositions(Template template)
    {
      ISession session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      //если не найден шаблон (в том числе по умолчанию), то берём пустой объект - там в позициях будут нули и возьмётся значение по умолчанию при печати
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
      //else if (string.IsNullOrEmpty(za7.In1.Phone.Code))
      //{
      //  lbAddressPhoneSmo.Text = string.Format("{0} {1}", za7.In1.AddressSmoInStr, za7.In1.Phone.Phone);
      //}
      //else
      //{
      //  lbAddressPhoneSmo.Text = string.Format("{0} ({1}){2}", za7.In1.AddressSmoInStr, za7.In1.Phone.Code, za7.In1.Phone.Phone);
      //}

      lbBirthPlace.Text = personData.Birthplace;

      //первые даты
      if (statement.DateIssueTemporaryCertificate.HasValue)
      {
        string date = ConversionHelper.DateTimeToStringGoznak(statement.DateIssueTemporaryCertificate.Value);
        lbDay1.Text = GetDay(date);
        lbMonth1.Text = GetMonth(date);
        lbYear1.Text = GetYear(date);

        DateTime dateIssue = statement.DateIssueTemporaryCertificate.Value;
        DateTime endDate = DateTymeHelper.CalculateEnPeriodWorkingDay(dateIssue, 30);
        date = endDate.ToString("dd.MM.yyyy");

        lbDay2.Text = GetDay(date);
        lbMonth2.Text = GetMonth(date);
        lbYear2.Text = GetYear(date);
      }

      lbLine1.Text = string.Format("{0} {1} {2}", personData.LastName, personData.FirstName, personData.MiddleName);

      var birthday = personData.Birthday.HasValue ? ConversionHelper.DateTimeToStringGoznak(personData.Birthday.Value) : string.Empty;
      lbLine2.Text = string.Format("{0} {1} {2}", GetDate(birthday),
        //тип документа удл
              statement.DocumentUdl.DocumentType.Name,
        //серия и номер
              statement.DocumentUdl.SeriesNumber);

      var actualFrom = statement.DocumentUdl.DateIssue.HasValue ? ConversionHelper.DateTimeToStringGoznak(statement.DocumentUdl.DateIssue.Value) : string.Empty;
      lbLine3.Text = string.Format("{0} {1}",
        //когда и кем выдан
              statement.DocumentUdl.IssuingAuthority,
              GetDate(actualFrom)
              );

      if (personData.Gender.Code == "1")
      {
        lbMale.Text = "V";
      }
      else if (personData.Gender.Code == "2")
      {
        lbFemale.Text = "V";
      }
      lbFio.Text = _sec.GetCurrentUser().Fio;

      AutoscaleControlText(lbSmo);
      AutoscaleControlText(lbAddressPhoneSmo);
      AutoscaleControlText(lbLine1);
      AutoscaleControlText(lbLine2);
      AutoscaleControlText(lbLine3);
      AutoscaleControlText(lbFio);
      AutoscaleControlText(lbBirthPlace);
    }

    private string GetDate(string date)
    {
      if (string.IsNullOrEmpty(date))
      {
        return null;
      }
      DateTime result = DateTime.Parse(date);
      return result.ToString("dd.MM.yyyy");
    }

    private string GetDay(string date)
    {
      return GetConvert(date, "dd");
    }

    private string GetMonth(string date)
    {
      return GetConvert(date, "MMMM");
    }

    private string GetYear(string date)
    {
      var result = GetConvert(date, "yyyy");
      return !string.IsNullOrEmpty(result) ? result.Substring(2, 2) : null;
    }

    private string GetConvert(string date, string mask)
    {
      if (string.IsNullOrEmpty(date))
      {
        return null;
      }
      DateTime result = DateTime.Parse(date);
      return result.ToString(mask);
    }

    private void AutoscaleControlText(XRControl control)
    {
      float controlWidth = control.SizeF.Width;
      while (MeasureTextWidthPixels(((XtraReport)control.Report.Report).ReportUnit, control.Text, control.Font) > controlWidth)
        control.Font = new Font(control.Font.FontFamily, control.Font.Size - 0.1f, control.Font.Style);
    }

    private float MeasureTextWidthPixels(ReportUnit unit, string text, Font font)
    {
      Graphics gr = Graphics.FromHwnd(IntPtr.Zero);

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

      SizeF size = gr.MeasureString(text, font);
      gr.Dispose();

      return size.Width * factor;
    }

    #endregion

    private void TemporaryCertificateReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
    }

  }
}
