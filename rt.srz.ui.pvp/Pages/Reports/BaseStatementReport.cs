using System;
using System.Web;
using System.Linq;
using rt.srz.model.srz;
using rt.srz.model.interfaces.service;
using StructureMap;
using DevExpress.XtraReports.UI;
using rt.srz.model.HL7.person.messages;
using rt.srz.model.srz.concepts;
using System.Text;
using rt.srz.business.manager.cache;

namespace rt.srz.ui.pvp.Pages.Reports
{
  /// <summary>
  /// Summary description for StatementReport
  /// </summary>
  public class BaseStatementReport : DevExpress.XtraReports.UI.XtraReport, IReport
  {
    protected DetailBand Detail;
    #region report controls

    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    protected XRLabel lbSmo;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel lbFio;
    private XRLabel xrLabel6;
    private XRLabel lbCauseFilling;
    protected XRLabel lbAsk;
    protected XRLabel lbReason;
    protected XRLabel xrLabel7;
    protected XRLabel xrLabel13;
    protected XRLabel lbMiddleName;
    protected XRLabel lbName;
    protected XRLabel lbFio1;
    protected XRLabel xrLabel16;
    protected XRLabel xrLabel15;
    protected XRLabel xrLabel14;
    protected XRLabel lbInsuranceCategory;
    protected XRLabel xrLabel21;
    protected XRLabel xrLabel20;
    protected XRLabel xrLabel17;
    protected XRLabel xrLabel22;
    protected XRLabel xrLabel23;
    protected XRLabel xrLabel32;
    protected XRLabel xrLabel31;
    protected XRLabel xrLabel30;
    protected XRLabel xrLabel29;
    protected XRLabel xrLabel28;
    protected XRLabel xrLabel27;
    protected XRLabel xrLabel26;
    protected XRLabel xrLabel25;
    protected XRLabel xrLabel24;
    protected XRLabel xrLabel33;
    protected XRLabel lbBirthdate;
    protected XRLabel lbBirthPlace;
    protected XRLabel lbUdlDoc;
    protected XRLabel lbCitizenship;
    protected XRLabel lbIssueInfo;
    protected XRLabel lbNumSeries;
    protected XRLabel lbSubject;
    protected XRLabel lbPostCode;
    protected XRLabel xrLabel34;
    protected XRLabel lbRegistrationDateByPlace;
    protected XRLabel lbFlat;
    protected XRLabel lbKorpus;
    protected XRLabel lbHouse;
    protected XRLabel lbStreet;
    protected XRLabel lbSattlement;
    protected XRLabel lbTown;
    protected XRLabel lbDistrict;
    protected XRLabel xrLabel40;
    protected XRLabel xrLabel41;
    protected XRLabel xrLabel39;
    protected XRLabel xrLabel38;
    protected XRLabel xrLabel37;
    protected XRLabel xrLabel36;
    protected XRLabel xrLabel35;
    protected XRLabel xrLabel42;
    protected XRLabel xrLabel43;
    protected XRLabel lbSattlementA;
    protected XRLabel lbKorpusA;
    protected XRLabel xrLabel58;
    protected XRLabel xrLabel57;
    protected XRLabel lbHouseA;
    protected XRLabel xrLabel55;
    protected XRLabel xrLabel54;
    protected XRLabel lbDistrictA;
    protected XRLabel xrLabel52;
    protected XRLabel lbPostCodeA;
    protected XRLabel lbSubjectA;
    protected XRLabel lbCityA;
    protected XRLabel lbStreetA;
    protected XRLabel xrLabel47;
    protected XRLabel xrLabel46;
    protected XRLabel xrLabel45;
    protected XRLabel xrLabel60;
    protected XRLabel lbFlatA;
    protected XRLabel xrLabel44;
    protected XRLabel xrLabel48;
    protected XRLabel lbRegDoc;
    protected XRLabel xrLabel50;
    protected XRLabel xrLabel51;
    protected XRLabel lbRegIssueInfo;
    protected XRLabel lbRegNumSeries;
    protected XRLabel xrLabel49;
    protected XRLabel lbFrom;
    protected XRLabel xrLabel63;
    protected XRLabel xrLabel62;
    protected XRLabel lbTo;
    protected XRLabel xrLabel59;
    protected XRLabel xrLabel56;
    protected XRLabel xrLabel53;
    protected XRLabel lbSnils1;
    protected XRLabel lbSnils2;
    protected XRLabel lbSnils3;
    protected XRLabel lbSnils4;
    protected XRLabel lbSnils5;
    protected XRLabel lbSnils6;
    protected XRLabel lbSnils7;
    protected XRLabel lbSnils8;
    protected XRLabel lbSnils9;
    protected XRLabel lbSnils10;
    protected XRLabel lbSnils11;
    protected XRLabel xrLabel76;
    protected XRLabel xrLabel75;
    protected XRLabel xrLabel74;
    protected XRLabel xrLabel61;
    protected XRLabel xrLabel64;
    protected XRLabel lbHomePhone;
    protected XRLabel xrLabel65;
    protected XRLabel lbOficialPhone;
    protected XRLabel xrLabel66;
    protected XRLabel lbEmail;
    protected XRLabel xrLabel67;
    protected XRLabel xrLabel68;
    protected XRLabel xrLabel69;
    protected XRLabel xrLabel70;
    protected XRLabel lb2Fio;
    protected XRLabel lb2Name;
    protected XRLabel xrLabel73;
    protected XRLabel lb2MiddleName;
    protected XRLabel xrLabel71;
    protected XRLabel lb2UdlDoc;
    protected XRLabel xrLabel77;
    protected XRLabel lb2NumSeries;
    protected XRLabel xrLabel79;
    protected XRLabel lb2IssueDate;
    protected XRLabel xrLabel81;
    protected XRLabel xrLabel88;
    protected XRLabel lb2HomePhone;
    protected XRLabel lb2OficialPhone;
    protected XRLabel xrLabel78;
    protected XRLabel xrLabel72;
    protected XRLabel xrLabel80;
    protected XRLabel xrLabel91;
    protected XRLabel xrLabel90;
    protected XRLabel lbSignExt;
    protected XRLabel xrLabel87;
    protected XRLabel lbSign;
    protected XRLabel xrLabel85;
    protected XRLabel lbSignAccept;
    protected XRLabel xrLabel93;
    protected XRLabel lbSignDate;
    protected XRLabel xrLabel95;
    protected XRLabel xrLabel89;
    protected XRLabel xrLabel92;
    protected XRLabel xrLabel86;
    protected XRLabel lbTempNum;
    protected XRLabel xrLabel94;
    protected XRLabel lbDate;
    protected XRLabel xrLabel98;
    protected XRLabel xrLabel97;
    protected XRLabel xrLabel96;
    protected XRCheckBox cbMale;
    protected XRCheckBox cbFemale;
    protected XRCheckBox cbIsHomeless;
    protected XRCheckBox cbMother;
    protected XRCheckBox cbFather;
    protected XRCheckBox cbOther;
    protected XRLabel xrLabel1;
    private XRPageBreak xrPageBreak1;

    #endregion

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public BaseStatementReport()
    {
      InitializeComponent();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseStatementReport));
      this.Detail = new DevExpress.XtraReports.UI.DetailBand();
      this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
      this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
      this.cbMother = new DevExpress.XtraReports.UI.XRCheckBox();
      this.cbFather = new DevExpress.XtraReports.UI.XRCheckBox();
      this.cbOther = new DevExpress.XtraReports.UI.XRCheckBox();
      this.cbIsHomeless = new DevExpress.XtraReports.UI.XRCheckBox();
      this.cbMale = new DevExpress.XtraReports.UI.XRCheckBox();
      this.cbFemale = new DevExpress.XtraReports.UI.XRCheckBox();
      this.lbDate = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel98 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel97 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel96 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbTempNum = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel94 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel95 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel89 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel92 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel86 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel91 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel90 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSignExt = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel87 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSign = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel85 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel80 = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2HomePhone = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2OficialPhone = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel78 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel72 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel88 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel81 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel71 = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2UdlDoc = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel77 = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2NumSeries = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2IssueDate = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel69 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel70 = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2Fio = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2Name = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel73 = new DevExpress.XtraReports.UI.XRLabel();
      this.lb2MiddleName = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel68 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel67 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel66 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbEmail = new DevExpress.XtraReports.UI.XRLabel();
      this.lbOficialPhone = new DevExpress.XtraReports.UI.XRLabel();
      this.lbHomePhone = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel65 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel64 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel76 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel75 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel74 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils1 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils2 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils3 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils4 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils5 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils6 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils7 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils8 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils9 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils10 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSnils11 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbTo = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFrom = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbRegDoc = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbRegIssueInfo = new DevExpress.XtraReports.UI.XRLabel();
      this.lbRegNumSeries = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSattlementA = new DevExpress.XtraReports.UI.XRLabel();
      this.lbKorpusA = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbHouseA = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbDistrictA = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbPostCodeA = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSubjectA = new DevExpress.XtraReports.UI.XRLabel();
      this.lbCityA = new DevExpress.XtraReports.UI.XRLabel();
      this.lbStreetA = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFlatA = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbRegistrationDateByPlace = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFlat = new DevExpress.XtraReports.UI.XRLabel();
      this.lbKorpus = new DevExpress.XtraReports.UI.XRLabel();
      this.lbHouse = new DevExpress.XtraReports.UI.XRLabel();
      this.lbStreet = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSattlement = new DevExpress.XtraReports.UI.XRLabel();
      this.lbTown = new DevExpress.XtraReports.UI.XRLabel();
      this.lbDistrict = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSubject = new DevExpress.XtraReports.UI.XRLabel();
      this.lbPostCode = new DevExpress.XtraReports.UI.XRLabel();
      this.lbCitizenship = new DevExpress.XtraReports.UI.XRLabel();
      this.lbIssueInfo = new DevExpress.XtraReports.UI.XRLabel();
      this.lbNumSeries = new DevExpress.XtraReports.UI.XRLabel();
      this.lbBirthPlace = new DevExpress.XtraReports.UI.XRLabel();
      this.lbUdlDoc = new DevExpress.XtraReports.UI.XRLabel();
      this.lbBirthdate = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbInsuranceCategory = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbMiddleName = new DevExpress.XtraReports.UI.XRLabel();
      this.lbName = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFio1 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbReason = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbAsk = new DevExpress.XtraReports.UI.XRLabel();
      this.lbCauseFilling = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbFio = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSmo = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSignDate = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel93 = new DevExpress.XtraReports.UI.XRLabel();
      this.lbSignAccept = new DevExpress.XtraReports.UI.XRLabel();
      this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
      this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // Detail
      // 
      this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrLabel1,
            this.cbMother,
            this.cbFather,
            this.cbOther,
            this.cbIsHomeless,
            this.cbMale,
            this.cbFemale,
            this.lbDate,
            this.xrLabel98,
            this.xrLabel97,
            this.xrLabel96,
            this.lbTempNum,
            this.xrLabel94,
            this.xrLabel95,
            this.xrLabel89,
            this.xrLabel92,
            this.xrLabel86,
            this.xrLabel91,
            this.xrLabel90,
            this.lbSignExt,
            this.xrLabel87,
            this.lbSign,
            this.xrLabel85,
            this.xrLabel80,
            this.lb2HomePhone,
            this.lb2OficialPhone,
            this.xrLabel78,
            this.xrLabel72,
            this.xrLabel88,
            this.xrLabel81,
            this.xrLabel71,
            this.lb2UdlDoc,
            this.xrLabel77,
            this.lb2NumSeries,
            this.xrLabel79,
            this.lb2IssueDate,
            this.xrLabel69,
            this.xrLabel70,
            this.lb2Fio,
            this.lb2Name,
            this.xrLabel73,
            this.lb2MiddleName,
            this.xrLabel68,
            this.xrLabel67,
            this.xrLabel66,
            this.lbEmail,
            this.lbOficialPhone,
            this.lbHomePhone,
            this.xrLabel65,
            this.xrLabel64,
            this.xrLabel61,
            this.xrLabel76,
            this.xrLabel75,
            this.xrLabel74,
            this.lbSnils1,
            this.lbSnils2,
            this.lbSnils3,
            this.lbSnils4,
            this.lbSnils5,
            this.lbSnils6,
            this.lbSnils7,
            this.lbSnils8,
            this.lbSnils9,
            this.lbSnils10,
            this.lbSnils11,
            this.xrLabel53,
            this.xrLabel63,
            this.xrLabel62,
            this.lbTo,
            this.xrLabel59,
            this.xrLabel56,
            this.lbFrom,
            this.xrLabel49,
            this.xrLabel48,
            this.lbRegDoc,
            this.xrLabel50,
            this.xrLabel51,
            this.lbRegIssueInfo,
            this.lbRegNumSeries,
            this.xrLabel44,
            this.lbSattlementA,
            this.lbKorpusA,
            this.xrLabel58,
            this.xrLabel57,
            this.lbHouseA,
            this.xrLabel55,
            this.xrLabel54,
            this.lbDistrictA,
            this.xrLabel52,
            this.lbPostCodeA,
            this.lbSubjectA,
            this.lbCityA,
            this.lbStreetA,
            this.xrLabel47,
            this.xrLabel46,
            this.xrLabel45,
            this.xrLabel60,
            this.lbFlatA,
            this.xrLabel43,
            this.xrLabel42,
            this.lbRegistrationDateByPlace,
            this.lbFlat,
            this.lbKorpus,
            this.lbHouse,
            this.lbStreet,
            this.lbSattlement,
            this.lbTown,
            this.lbDistrict,
            this.xrLabel40,
            this.xrLabel41,
            this.xrLabel39,
            this.xrLabel38,
            this.xrLabel37,
            this.xrLabel36,
            this.xrLabel35,
            this.xrLabel34,
            this.lbSubject,
            this.lbPostCode,
            this.lbCitizenship,
            this.lbIssueInfo,
            this.lbNumSeries,
            this.lbBirthPlace,
            this.lbUdlDoc,
            this.lbBirthdate,
            this.xrLabel33,
            this.xrLabel32,
            this.xrLabel31,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.lbInsuranceCategory,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel17,
            this.lbMiddleName,
            this.lbName,
            this.lbFio1,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.lbReason,
            this.xrLabel7,
            this.lbAsk,
            this.lbCauseFilling,
            this.xrLabel6,
            this.lbFio,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.lbSmo,
            this.xrLabel2,
            this.lbSignDate,
            this.xrLabel93,
            this.lbSignAccept});
      this.Detail.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.Detail.HeightF = 2583.833F;
      this.Detail.Name = "Detail";
      this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
      this.Detail.StylePriority.UseBorders = false;
      this.Detail.StylePriority.UseFont = false;
      this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrPageBreak1
      // 
      this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 880.46F);
      this.xrPageBreak1.Name = "xrPageBreak1";
      // 
      // xrLabel1
      // 
      this.xrLabel1.BackColor = System.Drawing.Color.LightGray;
      this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(12.50012F, 577.8334F);
      this.xrLabel1.Name = "xrLabel1";
      this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel1.SizeF = new System.Drawing.SizeF(751.9996F, 24.04169F);
      this.xrLabel1.StylePriority.UseBackColor = false;
      this.xrLabel1.StylePriority.UseBorders = false;
      // 
      // cbMother
      // 
      this.cbMother.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.cbMother.LocationFloat = new DevExpress.Utils.PointFloat(333.3749F, 2152.166F);
      this.cbMother.Name = "cbMother";
      this.cbMother.SizeF = new System.Drawing.SizeF(56.41705F, 23F);
      this.cbMother.StylePriority.UseFont = false;
      this.cbMother.Text = "мать";
      // 
      // cbFather
      // 
      this.cbFather.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.cbFather.LocationFloat = new DevExpress.Utils.PointFloat(449.2504F, 2152.166F);
      this.cbFather.Name = "cbFather";
      this.cbFather.SizeF = new System.Drawing.SizeF(58.04117F, 23F);
      this.cbFather.StylePriority.UseFont = false;
      this.cbFather.Text = "отец";
      // 
      // cbOther
      // 
      this.cbOther.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.cbOther.LocationFloat = new DevExpress.Utils.PointFloat(566.7913F, 2152.166F);
      this.cbOther.Name = "cbOther";
      this.cbOther.SizeF = new System.Drawing.SizeF(54.45929F, 23F);
      this.cbOther.StylePriority.UseFont = false;
      this.cbOther.Text = "иное";
      // 
      // cbIsHomeless
      // 
      this.cbIsHomeless.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.cbIsHomeless.LocationFloat = new DevExpress.Utils.PointFloat(12.00005F, 1348.396F);
      this.cbIsHomeless.Name = "cbIsHomeless";
      this.cbIsHomeless.SizeF = new System.Drawing.SizeF(29.1668F, 23F);
      this.cbIsHomeless.StylePriority.UseFont = false;
      this.cbIsHomeless.Text = "жен.";
      // 
      // cbMale
      // 
      this.cbMale.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.cbMale.LocationFloat = new DevExpress.Utils.PointFloat(115.0001F, 416.375F);
      this.cbMale.Name = "cbMale";
      this.cbMale.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100F);
      this.cbMale.SizeF = new System.Drawing.SizeF(53.08354F, 23F);
      this.cbMale.StylePriority.UseFont = false;
      this.cbMale.StylePriority.UsePadding = false;
      this.cbMale.Text = "муж.";
      // 
      // cbFemale
      // 
      this.cbFemale.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.cbFemale.LocationFloat = new DevExpress.Utils.PointFloat(213.4169F, 416.375F);
      this.cbFemale.Name = "cbFemale";
      this.cbFemale.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100F);
      this.cbFemale.SizeF = new System.Drawing.SizeF(53.16669F, 23F);
      this.cbFemale.StylePriority.UseFont = false;
      this.cbFemale.StylePriority.UsePadding = false;
      this.cbFemale.Text = "жен.";
      // 
      // lbDate
      // 
      this.lbDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbDate.LocationFloat = new DevExpress.Utils.PointFloat(79.70499F, 2505.542F);
      this.lbDate.Name = "lbDate";
      this.lbDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbDate.SizeF = new System.Drawing.SizeF(181.8785F, 23F);
      this.lbDate.StylePriority.UseFont = false;
      // 
      // xrLabel98
      // 
      this.xrLabel98.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel98.LocationFloat = new DevExpress.Utils.PointFloat(350.1668F, 2505.25F);
      this.xrLabel98.Name = "xrLabel98";
      this.xrLabel98.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel98.SizeF = new System.Drawing.SizeF(39.62512F, 23F);
      this.xrLabel98.StylePriority.UseFont = false;
      this.xrLabel98.Text = "М.П.";
      // 
      // xrLabel97
      // 
      this.xrLabel97.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel97.LocationFloat = new DevExpress.Utils.PointFloat(79.70499F, 2538.958F);
      this.xrLabel97.Name = "xrLabel97";
      this.xrLabel97.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel97.SizeF = new System.Drawing.SizeF(127.6666F, 15.70837F);
      this.xrLabel97.StylePriority.UseFont = false;
      this.xrLabel97.StylePriority.UseTextAlignment = false;
      this.xrLabel97.Text = "(число, месяц, год)";
      this.xrLabel97.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel96
      // 
      this.xrLabel96.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel96.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 2505.25F);
      this.xrLabel96.Name = "xrLabel96";
      this.xrLabel96.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel96.SizeF = new System.Drawing.SizeF(49.50014F, 23F);
      this.xrLabel96.StylePriority.UseFont = false;
      this.xrLabel96.Text = "Дата:";
      // 
      // lbTempNum
      // 
      this.lbTempNum.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbTempNum.LocationFloat = new DevExpress.Utils.PointFloat(275.5418F, 2468.125F);
      this.lbTempNum.Name = "lbTempNum";
      this.lbTempNum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbTempNum.SizeF = new System.Drawing.SizeF(488.9587F, 23F);
      this.lbTempNum.StylePriority.UseFont = false;
      this.lbTempNum.Text = "lbTempNum";
      // 
      // xrLabel94
      // 
      this.xrLabel94.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel94.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 2468.125F);
      this.xrLabel94.Name = "xrLabel94";
      this.xrLabel94.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel94.SizeF = new System.Drawing.SizeF(249.0834F, 23F);
      this.xrLabel94.StylePriority.UseFont = false;
      this.xrLabel94.Text = "Выдано временное свидетельство №";
      // 
      // xrLabel95
      // 
      this.xrLabel95.LocationFloat = new DevExpress.Utils.PointFloat(146.9167F, 2411.042F);
      this.xrLabel95.Name = "xrLabel95";
      this.xrLabel95.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel95.SizeF = new System.Drawing.SizeF(269.9586F, 23F);
      this.xrLabel95.Text = "_________________________________";
      // 
      // xrLabel89
      // 
      this.xrLabel89.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel89.LocationFloat = new DevExpress.Utils.PointFloat(122.5835F, 2441.666F);
      this.xrLabel89.Name = "xrLabel89";
      this.xrLabel89.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel89.SizeF = new System.Drawing.SizeF(311.0001F, 19.87451F);
      this.xrLabel89.StylePriority.UseFont = false;
      this.xrLabel89.StylePriority.UseTextAlignment = false;
      this.xrLabel89.Text = "(подпись представителя страховой медицинской организации (филиала))";
      this.xrLabel89.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel92
      // 
      this.xrLabel92.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel92.LocationFloat = new DevExpress.Utils.PointFloat(449.2504F, 2441.666F);
      this.xrLabel92.Name = "xrLabel92";
      this.xrLabel92.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel92.SizeF = new System.Drawing.SizeF(172.0001F, 23F);
      this.xrLabel92.StylePriority.UseFont = false;
      this.xrLabel92.Text = "(расшифровка подписи)";
      // 
      // xrLabel86
      // 
      this.xrLabel86.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel86.LocationFloat = new DevExpress.Utils.PointFloat(9.99999F, 2411.042F);
      this.xrLabel86.Name = "xrLabel86";
      this.xrLabel86.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel86.SizeF = new System.Drawing.SizeF(122.9168F, 23F);
      this.xrLabel86.StylePriority.UseFont = false;
      this.xrLabel86.Text = "Заявление принял:";
      // 
      // xrLabel91
      // 
      this.xrLabel91.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel91.LocationFloat = new DevExpress.Utils.PointFloat(595.6666F, 2354.791F);
      this.xrLabel91.Name = "xrLabel91";
      this.xrLabel91.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel91.SizeF = new System.Drawing.SizeF(42.29169F, 23F);
      this.xrLabel91.StylePriority.UseFont = false;
      this.xrLabel91.Text = "Дата";
      // 
      // xrLabel90
      // 
      this.xrLabel90.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel90.LocationFloat = new DevExpress.Utils.PointFloat(300.3751F, 2377.791F);
      this.xrLabel90.Name = "xrLabel90";
      this.xrLabel90.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel90.SizeF = new System.Drawing.SizeF(172.0001F, 23F);
      this.xrLabel90.StylePriority.UseFont = false;
      this.xrLabel90.Text = "(расшифровка подписи)";
      // 
      // lbSignExt
      // 
      this.lbSignExt.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSignExt.LocationFloat = new DevExpress.Utils.PointFloat(300.3751F, 2354.791F);
      this.lbSignExt.Name = "lbSignExt";
      this.lbSignExt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSignExt.SizeF = new System.Drawing.SizeF(276.5418F, 23F);
      this.lbSignExt.StylePriority.UseFont = false;
      this.lbSignExt.Text = "lbSignExt";
      // 
      // xrLabel87
      // 
      this.xrLabel87.LocationFloat = new DevExpress.Utils.PointFloat(268.75F, 2354.791F);
      this.xrLabel87.Name = "xrLabel87";
      this.xrLabel87.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel87.SizeF = new System.Drawing.SizeF(16.16669F, 23F);
      this.xrLabel87.Text = "/";
      // 
      // lbSign
      // 
      this.lbSign.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 2354.791F);
      this.lbSign.Name = "lbSign";
      this.lbSign.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSign.SizeF = new System.Drawing.SizeF(232.9167F, 23F);
      this.lbSign.Text = "_____________________________";
      // 
      // xrLabel85
      // 
      this.xrLabel85.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel85.LocationFloat = new DevExpress.Utils.PointFloat(16.16671F, 2377.791F);
      this.xrLabel85.Name = "xrLabel85";
      this.xrLabel85.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel85.SizeF = new System.Drawing.SizeF(245.4168F, 19.87451F);
      this.xrLabel85.StylePriority.UseFont = false;
      this.xrLabel85.StylePriority.UseTextAlignment = false;
      this.xrLabel85.Text = "(подпись застрахованного лица.его представителя)";
      this.xrLabel85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel80
      // 
      this.xrLabel80.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
      this.xrLabel80.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 2318.499F);
      this.xrLabel80.Multiline = true;
      this.xrLabel80.Name = "xrLabel80";
      this.xrLabel80.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel80.SizeF = new System.Drawing.SizeF(454.4199F, 17F);
      this.xrLabel80.StylePriority.UseFont = false;
      this.xrLabel80.StylePriority.UseTextAlignment = false;
      this.xrLabel80.Text = "3. Достоверность и полноту указанных сведений подтверждаю";
      this.xrLabel80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // lb2HomePhone
      // 
      this.lb2HomePhone.AutoWidth = true;
      this.lb2HomePhone.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2HomePhone.LocationFloat = new DevExpress.Utils.PointFloat(261.5835F, 2278.625F);
      this.lb2HomePhone.Name = "lb2HomePhone";
      this.lb2HomePhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2HomePhone.SizeF = new System.Drawing.SizeF(181.4168F, 17.00012F);
      this.lb2HomePhone.StylePriority.UseFont = false;
      this.lb2HomePhone.StylePriority.UseTextAlignment = false;
      this.lb2HomePhone.Text = "lb2HomePhone";
      this.lb2HomePhone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2OficialPhone
      // 
      this.lb2OficialPhone.AutoWidth = true;
      this.lb2OficialPhone.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2OficialPhone.LocationFloat = new DevExpress.Utils.PointFloat(566.7915F, 2278.625F);
      this.lb2OficialPhone.Name = "lb2OficialPhone";
      this.lb2OficialPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2OficialPhone.SizeF = new System.Drawing.SizeF(197.7089F, 17.00012F);
      this.lb2OficialPhone.StylePriority.UseFont = false;
      this.lb2OficialPhone.StylePriority.UseTextAlignment = false;
      this.lb2OficialPhone.Text = "lb2OficialPhone";
      this.lb2OficialPhone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel78
      // 
      this.xrLabel78.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel78.LocationFloat = new DevExpress.Utils.PointFloat(455.375F, 2278.625F);
      this.xrLabel78.Name = "xrLabel78";
      this.xrLabel78.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel78.SizeF = new System.Drawing.SizeF(96.25012F, 23F);
      this.xrLabel78.StylePriority.UseFont = false;
      this.xrLabel78.StylePriority.UseTextAlignment = false;
      this.xrLabel78.Text = "служебный:";
      this.xrLabel78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel72
      // 
      this.xrLabel72.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel72.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 2278.625F);
      this.xrLabel72.Name = "xrLabel72";
      this.xrLabel72.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel72.SizeF = new System.Drawing.SizeF(245.4168F, 27.16602F);
      this.xrLabel72.StylePriority.UseFont = false;
      this.xrLabel72.StylePriority.UseTextAlignment = false;
      this.xrLabel72.Text = "2.8. Контактный телефон домашний:";
      this.xrLabel72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel88
      // 
      this.xrLabel88.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel88.LocationFloat = new DevExpress.Utils.PointFloat(422.92F, 2180.17F);
      this.xrLabel88.Name = "xrLabel88";
      this.xrLabel88.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel88.SizeF = new System.Drawing.SizeF(226.1218F, 23.00024F);
      this.xrLabel88.StylePriority.UseFont = false;
      this.xrLabel88.StylePriority.UseTextAlignment = false;
      this.xrLabel88.Text = "(нужное отметить знаком \"v\")";
      this.xrLabel88.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel81
      // 
      this.xrLabel81.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel81.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 2152.166F);
      this.xrLabel81.Name = "xrLabel81";
      this.xrLabel81.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel81.SizeF = new System.Drawing.SizeF(290.6667F, 42.00073F);
      this.xrLabel81.StylePriority.UseFont = false;
      this.xrLabel81.StylePriority.UseTextAlignment = false;
      this.xrLabel81.Text = "2.4. Отношение к застрахованному лицу, сведения о котором указаны в заявлении:";
      this.xrLabel81.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel71
      // 
      this.xrLabel71.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel71.LocationFloat = new DevExpress.Utils.PointFloat(12.00002F, 2209.792F);
      this.xrLabel71.Name = "xrLabel71";
      this.xrLabel71.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel71.SizeF = new System.Drawing.SizeF(327.1669F, 23F);
      this.xrLabel71.StylePriority.UseFont = false;
      this.xrLabel71.StylePriority.UseTextAlignment = false;
      this.xrLabel71.Text = "2.5. Вид документа, удостоверяющего личность:";
      this.xrLabel71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2UdlDoc
      // 
      this.lb2UdlDoc.AutoWidth = true;
      this.lb2UdlDoc.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2UdlDoc.LocationFloat = new DevExpress.Utils.PointFloat(349.5836F, 2209.792F);
      this.lb2UdlDoc.Name = "lb2UdlDoc";
      this.lb2UdlDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2UdlDoc.SizeF = new System.Drawing.SizeF(414.4163F, 17F);
      this.lb2UdlDoc.StylePriority.UseFont = false;
      this.lb2UdlDoc.StylePriority.UseTextAlignment = false;
      this.lb2UdlDoc.Text = "lb2UdlDoc";
      this.lb2UdlDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel77
      // 
      this.xrLabel77.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel77.LocationFloat = new DevExpress.Utils.PointFloat(12.00005F, 2243.125F);
      this.xrLabel77.Name = "xrLabel77";
      this.xrLabel77.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel77.SizeF = new System.Drawing.SizeF(153.5835F, 23F);
      this.xrLabel77.StylePriority.UseFont = false;
      this.xrLabel77.StylePriority.UseTextAlignment = false;
      this.xrLabel77.Text = "2.6. Серия и номер:";
      this.xrLabel77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2NumSeries
      // 
      this.lb2NumSeries.AutoWidth = true;
      this.lb2NumSeries.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2NumSeries.LocationFloat = new DevExpress.Utils.PointFloat(189.7501F, 2243.125F);
      this.lb2NumSeries.Name = "lb2NumSeries";
      this.lb2NumSeries.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2NumSeries.SizeF = new System.Drawing.SizeF(243.8336F, 17F);
      this.lb2NumSeries.StylePriority.UseFont = false;
      this.lb2NumSeries.StylePriority.UseTextAlignment = false;
      this.lb2NumSeries.Text = "lb2NumSeries";
      this.lb2NumSeries.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel79
      // 
      this.xrLabel79.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel79.LocationFloat = new DevExpress.Utils.PointFloat(449.2504F, 2243.125F);
      this.xrLabel79.Name = "xrLabel79";
      this.xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel79.SizeF = new System.Drawing.SizeF(127.6665F, 23F);
      this.xrLabel79.StylePriority.UseFont = false;
      this.xrLabel79.StylePriority.UseTextAlignment = false;
      this.xrLabel79.Text = "2.7. Дата выдачи:";
      this.xrLabel79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2IssueDate
      // 
      this.lb2IssueDate.AutoWidth = true;
      this.lb2IssueDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2IssueDate.LocationFloat = new DevExpress.Utils.PointFloat(595.6666F, 2243.125F);
      this.lb2IssueDate.Name = "lb2IssueDate";
      this.lb2IssueDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2IssueDate.SizeF = new System.Drawing.SizeF(168.3334F, 17F);
      this.lb2IssueDate.StylePriority.UseFont = false;
      this.lb2IssueDate.StylePriority.UseTextAlignment = false;
      this.lb2IssueDate.Text = "lb2IssueDate";
      this.lb2IssueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel69
      // 
      this.xrLabel69.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel69.LocationFloat = new DevExpress.Utils.PointFloat(12.00002F, 2079.333F);
      this.xrLabel69.Name = "xrLabel69";
      this.xrLabel69.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel69.SizeF = new System.Drawing.SizeF(95.75001F, 23F);
      this.xrLabel69.StylePriority.UseFont = false;
      this.xrLabel69.StylePriority.UseTextAlignment = false;
      this.xrLabel69.Text = "2.1. Фамилия:";
      this.xrLabel69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel70
      // 
      this.xrLabel70.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel70.LocationFloat = new DevExpress.Utils.PointFloat(12.00002F, 2115.791F);
      this.xrLabel70.Name = "xrLabel70";
      this.xrLabel70.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel70.SizeF = new System.Drawing.SizeF(63.53834F, 23F);
      this.xrLabel70.StylePriority.UseFont = false;
      this.xrLabel70.StylePriority.UseTextAlignment = false;
      this.xrLabel70.Text = "2.2. Имя:";
      this.xrLabel70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2Fio
      // 
      this.lb2Fio.AutoWidth = true;
      this.lb2Fio.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2Fio.LocationFloat = new DevExpress.Utils.PointFloat(120.2501F, 2079.333F);
      this.lb2Fio.Name = "lb2Fio";
      this.lb2Fio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2Fio.SizeF = new System.Drawing.SizeF(643.7499F, 17F);
      this.lb2Fio.StylePriority.UseFont = false;
      this.lb2Fio.StylePriority.UseTextAlignment = false;
      this.lb2Fio.Text = "lb2Fio";
      this.lb2Fio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2Name
      // 
      this.lb2Name.AutoWidth = true;
      this.lb2Name.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2Name.LocationFloat = new DevExpress.Utils.PointFloat(98.37516F, 2115.791F);
      this.lb2Name.Name = "lb2Name";
      this.lb2Name.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2Name.SizeF = new System.Drawing.SizeF(308.5418F, 17F);
      this.lb2Name.StylePriority.UseFont = false;
      this.lb2Name.StylePriority.UseTextAlignment = false;
      this.lb2Name.Text = "lb2Name";
      this.lb2Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel73
      // 
      this.xrLabel73.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel73.LocationFloat = new DevExpress.Utils.PointFloat(429.3787F, 2115.791F);
      this.xrLabel73.Name = "xrLabel73";
      this.xrLabel73.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel73.SizeF = new System.Drawing.SizeF(106.9964F, 23F);
      this.xrLabel73.StylePriority.UseFont = false;
      this.xrLabel73.StylePriority.UseTextAlignment = false;
      this.xrLabel73.Text = "2.3. Отчество:";
      this.xrLabel73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lb2MiddleName
      // 
      this.lb2MiddleName.AutoWidth = true;
      this.lb2MiddleName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lb2MiddleName.LocationFloat = new DevExpress.Utils.PointFloat(550.2503F, 2115.791F);
      this.lb2MiddleName.Name = "lb2MiddleName";
      this.lb2MiddleName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lb2MiddleName.SizeF = new System.Drawing.SizeF(213.7497F, 17F);
      this.lb2MiddleName.StylePriority.UseFont = false;
      this.lb2MiddleName.StylePriority.UseTextAlignment = false;
      this.lb2MiddleName.Text = "lb2MiddleName";
      this.lb2MiddleName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel68
      // 
      this.xrLabel68.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
      this.xrLabel68.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 2046.708F);
      this.xrLabel68.Multiline = true;
      this.xrLabel68.Name = "xrLabel68";
      this.xrLabel68.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel68.SizeF = new System.Drawing.SizeF(377.2918F, 17F);
      this.xrLabel68.StylePriority.UseFont = false;
      this.xrLabel68.StylePriority.UseTextAlignment = false;
      this.xrLabel68.Text = "2. Сведения о представителе застрахованного лица";
      this.xrLabel68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // xrLabel67
      // 
      this.xrLabel67.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel67.LocationFloat = new DevExpress.Utils.PointFloat(468.9171F, 1902.083F);
      this.xrLabel67.Name = "xrLabel67";
      this.xrLabel67.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel67.SizeF = new System.Drawing.SizeF(127.6666F, 15.70837F);
      this.xrLabel67.StylePriority.UseFont = false;
      this.xrLabel67.StylePriority.UseTextAlignment = false;
      this.xrLabel67.Text = "(при наличии)";
      this.xrLabel67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel66
      // 
      this.xrLabel66.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel66.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 2009.791F);
      this.xrLabel66.Name = "xrLabel66";
      this.xrLabel66.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel66.SizeF = new System.Drawing.SizeF(249.0834F, 21.95837F);
      this.xrLabel66.StylePriority.UseFont = false;
      this.xrLabel66.StylePriority.UseTextAlignment = false;
      this.xrLabel66.Text = "1.17.2.  Адрес электронной почты";
      this.xrLabel66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbEmail
      // 
      this.lbEmail.AutoWidth = true;
      this.lbEmail.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbEmail.LocationFloat = new DevExpress.Utils.PointFloat(268.7501F, 2009.791F);
      this.lbEmail.Name = "lbEmail";
      this.lbEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbEmail.SizeF = new System.Drawing.SizeF(495.7504F, 17.00012F);
      this.lbEmail.StylePriority.UseFont = false;
      this.lbEmail.StylePriority.UseTextAlignment = false;
      this.lbEmail.Text = "lbEmail";
      this.lbEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbOficialPhone
      // 
      this.lbOficialPhone.AutoWidth = true;
      this.lbOficialPhone.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbOficialPhone.LocationFloat = new DevExpress.Utils.PointFloat(566.7915F, 1975.479F);
      this.lbOficialPhone.Name = "lbOficialPhone";
      this.lbOficialPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbOficialPhone.SizeF = new System.Drawing.SizeF(197.7089F, 17.00012F);
      this.lbOficialPhone.StylePriority.UseFont = false;
      this.lbOficialPhone.StylePriority.UseTextAlignment = false;
      this.lbOficialPhone.Text = "lbOficialPhone";
      this.lbOficialPhone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbHomePhone
      // 
      this.lbHomePhone.AutoWidth = true;
      this.lbHomePhone.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbHomePhone.LocationFloat = new DevExpress.Utils.PointFloat(268.75F, 1975.479F);
      this.lbHomePhone.Name = "lbHomePhone";
      this.lbHomePhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbHomePhone.SizeF = new System.Drawing.SizeF(181.4168F, 17.00012F);
      this.lbHomePhone.StylePriority.UseFont = false;
      this.lbHomePhone.StylePriority.UseTextAlignment = false;
      this.lbHomePhone.Text = "lbHomePhone";
      this.lbHomePhone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel65
      // 
      this.xrLabel65.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel65.LocationFloat = new DevExpress.Utils.PointFloat(460.9554F, 1975.479F);
      this.xrLabel65.Name = "xrLabel65";
      this.xrLabel65.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel65.SizeF = new System.Drawing.SizeF(90.66974F, 21.95837F);
      this.xrLabel65.StylePriority.UseFont = false;
      this.xrLabel65.StylePriority.UseTextAlignment = false;
      this.xrLabel65.Text = "служебный";
      this.xrLabel65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel64
      // 
      this.xrLabel64.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel64.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 1975.479F);
      this.xrLabel64.Name = "xrLabel64";
      this.xrLabel64.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel64.SizeF = new System.Drawing.SizeF(249.0834F, 21.95837F);
      this.xrLabel64.StylePriority.UseFont = false;
      this.xrLabel64.StylePriority.UseTextAlignment = false;
      this.xrLabel64.Text = "1.17.1.  Телефон (с кодом): домашний";
      this.xrLabel64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel61
      // 
      this.xrLabel61.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 1940.625F);
      this.xrLabel61.Name = "xrLabel61";
      this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel61.SizeF = new System.Drawing.SizeF(752.0003F, 21.95837F);
      this.xrLabel61.StylePriority.UseFont = false;
      this.xrLabel61.StylePriority.UseTextAlignment = false;
      this.xrLabel61.Text = "1.17. Контактная информация";
      this.xrLabel61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel76
      // 
      this.xrLabel76.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel76.LocationFloat = new DevExpress.Utils.PointFloat(353.5003F, 1902.083F);
      this.xrLabel76.Name = "xrLabel76";
      this.xrLabel76.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel76.SizeF = new System.Drawing.SizeF(14.50002F, 23F);
      this.xrLabel76.StylePriority.UseFont = false;
      this.xrLabel76.StylePriority.UseTextAlignment = false;
      this.xrLabel76.Text = "-";
      this.xrLabel76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // xrLabel75
      // 
      this.xrLabel75.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel75.LocationFloat = new DevExpress.Utils.PointFloat(230.9168F, 1902.083F);
      this.xrLabel75.Name = "xrLabel75";
      this.xrLabel75.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel75.SizeF = new System.Drawing.SizeF(14.50002F, 23F);
      this.xrLabel75.StylePriority.UseFont = false;
      this.xrLabel75.StylePriority.UseTextAlignment = false;
      this.xrLabel75.Text = "-";
      this.xrLabel75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // xrLabel74
      // 
      this.xrLabel74.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel74.LocationFloat = new DevExpress.Utils.PointFloat(107.7501F, 1902.083F);
      this.xrLabel74.Name = "xrLabel74";
      this.xrLabel74.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel74.SizeF = new System.Drawing.SizeF(14.50002F, 23F);
      this.xrLabel74.StylePriority.UseFont = false;
      this.xrLabel74.StylePriority.UseTextAlignment = false;
      this.xrLabel74.Text = "-";
      this.xrLabel74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils1
      // 
      this.lbSnils1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils1.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils1.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 1902.083F);
      this.lbSnils1.Name = "lbSnils1";
      this.lbSnils1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils1.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils1.StylePriority.UseBorders = false;
      this.lbSnils1.StylePriority.UseFont = false;
      this.lbSnils1.StylePriority.UseTextAlignment = false;
      this.lbSnils1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils2
      // 
      this.lbSnils2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils2.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils2.LocationFloat = new DevExpress.Utils.PointFloat(41.16672F, 1902.083F);
      this.lbSnils2.Name = "lbSnils2";
      this.lbSnils2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils2.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils2.StylePriority.UseBorders = false;
      this.lbSnils2.StylePriority.UseFont = false;
      this.lbSnils2.StylePriority.UseTextAlignment = false;
      this.lbSnils2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils3
      // 
      this.lbSnils3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils3.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils3.LocationFloat = new DevExpress.Utils.PointFloat(69.8334F, 1902.083F);
      this.lbSnils3.Name = "lbSnils3";
      this.lbSnils3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils3.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils3.StylePriority.UseBorders = false;
      this.lbSnils3.StylePriority.UseFont = false;
      this.lbSnils3.StylePriority.UseTextAlignment = false;
      this.lbSnils3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils4
      // 
      this.lbSnils4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils4.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils4.LocationFloat = new DevExpress.Utils.PointFloat(132.4167F, 1902.083F);
      this.lbSnils4.Name = "lbSnils4";
      this.lbSnils4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils4.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils4.StylePriority.UseBorders = false;
      this.lbSnils4.StylePriority.UseFont = false;
      this.lbSnils4.StylePriority.UseTextAlignment = false;
      this.lbSnils4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils5
      // 
      this.lbSnils5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils5.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils5.LocationFloat = new DevExpress.Utils.PointFloat(161.0834F, 1902.083F);
      this.lbSnils5.Name = "lbSnils5";
      this.lbSnils5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils5.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils5.StylePriority.UseBorders = false;
      this.lbSnils5.StylePriority.UseFont = false;
      this.lbSnils5.StylePriority.UseTextAlignment = false;
      this.lbSnils5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils6
      // 
      this.lbSnils6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils6.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils6.LocationFloat = new DevExpress.Utils.PointFloat(189.7501F, 1902.083F);
      this.lbSnils6.Name = "lbSnils6";
      this.lbSnils6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils6.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils6.StylePriority.UseBorders = false;
      this.lbSnils6.StylePriority.UseFont = false;
      this.lbSnils6.StylePriority.UseTextAlignment = false;
      this.lbSnils6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils7
      // 
      this.lbSnils7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils7.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils7.LocationFloat = new DevExpress.Utils.PointFloat(258.3333F, 1902.083F);
      this.lbSnils7.Name = "lbSnils7";
      this.lbSnils7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils7.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils7.StylePriority.UseBorders = false;
      this.lbSnils7.StylePriority.UseFont = false;
      this.lbSnils7.StylePriority.UseTextAlignment = false;
      this.lbSnils7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils8
      // 
      this.lbSnils8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils8.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils8.LocationFloat = new DevExpress.Utils.PointFloat(287F, 1902.083F);
      this.lbSnils8.Name = "lbSnils8";
      this.lbSnils8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils8.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils8.StylePriority.UseBorders = false;
      this.lbSnils8.StylePriority.UseFont = false;
      this.lbSnils8.StylePriority.UseTextAlignment = false;
      this.lbSnils8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils9
      // 
      this.lbSnils9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils9.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils9.LocationFloat = new DevExpress.Utils.PointFloat(315.6667F, 1902.083F);
      this.lbSnils9.Name = "lbSnils9";
      this.lbSnils9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils9.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils9.StylePriority.UseBorders = false;
      this.lbSnils9.StylePriority.UseFont = false;
      this.lbSnils9.StylePriority.UseTextAlignment = false;
      this.lbSnils9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils10
      // 
      this.lbSnils10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils10.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils10.LocationFloat = new DevExpress.Utils.PointFloat(376.2502F, 1902.083F);
      this.lbSnils10.Name = "lbSnils10";
      this.lbSnils10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils10.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils10.StylePriority.UseBorders = false;
      this.lbSnils10.StylePriority.UseFont = false;
      this.lbSnils10.StylePriority.UseTextAlignment = false;
      this.lbSnils10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbSnils11
      // 
      this.lbSnils11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
      this.lbSnils11.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbSnils11.LocationFloat = new DevExpress.Utils.PointFloat(404.9169F, 1902.083F);
      this.lbSnils11.Name = "lbSnils11";
      this.lbSnils11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSnils11.SizeF = new System.Drawing.SizeF(28.66669F, 23F);
      this.lbSnils11.StylePriority.UseBorders = false;
      this.lbSnils11.StylePriority.UseFont = false;
      this.lbSnils11.StylePriority.UseTextAlignment = false;
      this.lbSnils11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // xrLabel53
      // 
      this.xrLabel53.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 1867F);
      this.xrLabel53.Name = "xrLabel53";
      this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel53.SizeF = new System.Drawing.SizeF(752.0003F, 21.95837F);
      this.xrLabel53.StylePriority.UseFont = false;
      this.xrLabel53.StylePriority.UseTextAlignment = false;
      this.xrLabel53.Text = "1.16. Страховой номер индивидуального лицевого счета (СНИЛС)";
      this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel63
      // 
      this.xrLabel63.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(356.5003F, 1810.374F);
      this.xrLabel63.Name = "xrLabel63";
      this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel63.SizeF = new System.Drawing.SizeF(48.41672F, 23F);
      this.xrLabel63.StylePriority.UseFont = false;
      this.xrLabel63.StylePriority.UseTextAlignment = false;
      this.xrLabel63.Text = "по:";
      this.xrLabel63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel62
      // 
      this.xrLabel62.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 1810.374F);
      this.xrLabel62.Name = "xrLabel62";
      this.xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel62.SizeF = new System.Drawing.SizeF(49.50001F, 23F);
      this.xrLabel62.StylePriority.UseFont = false;
      this.xrLabel62.StylePriority.UseTextAlignment = false;
      this.xrLabel62.Text = "с:";
      this.xrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbTo
      // 
      this.lbTo.AutoWidth = true;
      this.lbTo.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbTo.LocationFloat = new DevExpress.Utils.PointFloat(433.5836F, 1810.374F);
      this.lbTo.Name = "lbTo";
      this.lbTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbTo.SizeF = new System.Drawing.SizeF(286.6668F, 17F);
      this.lbTo.StylePriority.UseFont = false;
      this.lbTo.StylePriority.UseTextAlignment = false;
      this.lbTo.Text = "lbTo";
      this.lbTo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel59
      // 
      this.xrLabel59.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(433.5836F, 1837.833F);
      this.xrLabel59.Name = "xrLabel59";
      this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel59.SizeF = new System.Drawing.SizeF(127.6666F, 15.70837F);
      this.xrLabel59.StylePriority.UseFont = false;
      this.xrLabel59.StylePriority.UseTextAlignment = false;
      this.xrLabel59.Text = "(число, месяц, год)";
      this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel56
      // 
      this.xrLabel56.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(96.37513F, 1837.833F);
      this.xrLabel56.Name = "xrLabel56";
      this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel56.SizeF = new System.Drawing.SizeF(107.875F, 15.70837F);
      this.xrLabel56.StylePriority.UseFont = false;
      this.xrLabel56.Text = "(число, месяц, год)";
      // 
      // lbFrom
      // 
      this.lbFrom.AutoWidth = true;
      this.lbFrom.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbFrom.LocationFloat = new DevExpress.Utils.PointFloat(96.37513F, 1810.374F);
      this.lbFrom.Name = "lbFrom";
      this.lbFrom.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbFrom.SizeF = new System.Drawing.SizeF(240.7918F, 17F);
      this.lbFrom.StylePriority.UseFont = false;
      this.lbFrom.StylePriority.UseTextAlignment = false;
      this.lbFrom.Text = "lbFrom";
      this.lbFrom.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel49
      // 
      this.xrLabel49.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 1735.75F);
      this.xrLabel49.Name = "xrLabel49";
      this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel49.SizeF = new System.Drawing.SizeF(752.0003F, 50.08337F);
      this.xrLabel49.StylePriority.UseFont = false;
      this.xrLabel49.StylePriority.UseTextAlignment = false;
      this.xrLabel49.Text = resources.GetString("xrLabel49.Text");
      this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel48
      // 
      this.xrLabel48.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(12.5001F, 1627.499F);
      this.xrLabel48.Name = "xrLabel48";
      this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel48.SizeF = new System.Drawing.SizeF(174.4999F, 23F);
      this.xrLabel48.StylePriority.UseFont = false;
      this.xrLabel48.StylePriority.UseTextAlignment = false;
      this.xrLabel48.Text = "а) вид документа:";
      this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbRegDoc
      // 
      this.lbRegDoc.AutoWidth = true;
      this.lbRegDoc.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbRegDoc.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 1627.499F);
      this.lbRegDoc.Name = "lbRegDoc";
      this.lbRegDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbRegDoc.SizeF = new System.Drawing.SizeF(563.2505F, 17F);
      this.lbRegDoc.StylePriority.UseFont = false;
      this.lbRegDoc.StylePriority.UseTextAlignment = false;
      this.lbRegDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel50
      // 
      this.xrLabel50.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 1660.833F);
      this.xrLabel50.Name = "xrLabel50";
      this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel50.SizeF = new System.Drawing.SizeF(153.5835F, 23F);
      this.xrLabel50.StylePriority.UseFont = false;
      this.xrLabel50.StylePriority.UseTextAlignment = false;
      this.xrLabel50.Text = "б) серия и номер:";
      this.xrLabel50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel51
      // 
      this.xrLabel51.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 1696.25F);
      this.xrLabel51.Name = "xrLabel51";
      this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel51.SizeF = new System.Drawing.SizeF(177F, 23F);
      this.xrLabel51.StylePriority.UseFont = false;
      this.xrLabel51.StylePriority.UseTextAlignment = false;
      this.xrLabel51.Text = "в) кем и когда выдан:";
      this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbRegIssueInfo
      // 
      this.lbRegIssueInfo.AutoWidth = true;
      this.lbRegIssueInfo.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbRegIssueInfo.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 1696.25F);
      this.lbRegIssueInfo.Name = "lbRegIssueInfo";
      this.lbRegIssueInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbRegIssueInfo.SizeF = new System.Drawing.SizeF(562.7501F, 17F);
      this.lbRegIssueInfo.StylePriority.UseFont = false;
      this.lbRegIssueInfo.StylePriority.UseTextAlignment = false;
      this.lbRegIssueInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbRegNumSeries
      // 
      this.lbRegNumSeries.AutoWidth = true;
      this.lbRegNumSeries.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbRegNumSeries.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 1660.833F);
      this.lbRegNumSeries.Name = "lbRegNumSeries";
      this.lbRegNumSeries.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbRegNumSeries.SizeF = new System.Drawing.SizeF(562.7501F, 17F);
      this.lbRegNumSeries.StylePriority.UseFont = false;
      this.lbRegNumSeries.StylePriority.UseTextAlignment = false;
      this.lbRegNumSeries.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel44
      // 
      this.xrLabel44.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(12.5001F, 1591.75F);
      this.xrLabel44.Name = "xrLabel44";
      this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel44.SizeF = new System.Drawing.SizeF(752.0003F, 21.95837F);
      this.xrLabel44.StylePriority.UseFont = false;
      this.xrLabel44.StylePriority.UseTextAlignment = false;
      this.xrLabel44.Text = "1.14. Сведения о документе, подтверждающем  регистрацию по месту жительства в Рос" +
    "сийской Федерации:";
      this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbSattlementA
      // 
      this.lbSattlementA.AutoWidth = true;
      this.lbSattlementA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSattlementA.LocationFloat = new DevExpress.Utils.PointFloat(566.7915F, 1522.999F);
      this.lbSattlementA.Name = "lbSattlementA";
      this.lbSattlementA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSattlementA.SizeF = new System.Drawing.SizeF(197.7089F, 17F);
      this.lbSattlementA.StylePriority.UseFont = false;
      this.lbSattlementA.StylePriority.UseTextAlignment = false;
      this.lbSattlementA.Text = "lbSattlement";
      this.lbSattlementA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbKorpusA
      // 
      this.lbKorpusA.AutoWidth = true;
      this.lbKorpusA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbKorpusA.LocationFloat = new DevExpress.Utils.PointFloat(560.6252F, 1557.707F);
      this.lbKorpusA.Name = "lbKorpusA";
      this.lbKorpusA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbKorpusA.SizeF = new System.Drawing.SizeF(35.95856F, 17F);
      this.lbKorpusA.StylePriority.UseFont = false;
      this.lbKorpusA.StylePriority.UseTextAlignment = false;
      this.lbKorpusA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel58
      // 
      this.xrLabel58.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(477.9172F, 1557.707F);
      this.xrLabel58.Name = "xrLabel58";
      this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel58.SizeF = new System.Drawing.SizeF(73.70804F, 23F);
      this.xrLabel58.StylePriority.UseFont = false;
      this.xrLabel58.StylePriority.UseTextAlignment = false;
      this.xrLabel58.Text = "з) корпус:";
      this.xrLabel58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel57
      // 
      this.xrLabel57.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(347.5837F, 1557.707F);
      this.xrLabel57.Name = "xrLabel57";
      this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel57.SizeF = new System.Drawing.SizeF(57.33319F, 23F);
      this.xrLabel57.StylePriority.UseFont = false;
      this.xrLabel57.StylePriority.UseTextAlignment = false;
      this.xrLabel57.Text = "ж) дом:";
      this.xrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbHouseA
      // 
      this.lbHouseA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbHouseA.LocationFloat = new DevExpress.Utils.PointFloat(412.7951F, 1557.707F);
      this.lbHouseA.Name = "lbHouseA";
      this.lbHouseA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbHouseA.SizeF = new System.Drawing.SizeF(49.45517F, 17F);
      this.lbHouseA.StylePriority.UseFont = false;
      this.lbHouseA.StylePriority.UseTextAlignment = false;
      this.lbHouseA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel55
      // 
      this.xrLabel55.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(12.5001F, 1443.833F);
      this.xrLabel55.Name = "xrLabel55";
      this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel55.SizeF = new System.Drawing.SizeF(148.5834F, 23F);
      this.xrLabel55.StylePriority.UseFont = false;
      this.xrLabel55.StylePriority.UseTextAlignment = false;
      this.xrLabel55.Text = "а) почтовый индекс:";
      this.xrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel54
      // 
      this.xrLabel54.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(449.2506F, 1522.999F);
      this.xrLabel54.Name = "xrLabel54";
      this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel54.SizeF = new System.Drawing.SizeF(98.99988F, 23F);
      this.xrLabel54.StylePriority.UseFont = false;
      this.xrLabel54.StylePriority.UseTextAlignment = false;
      this.xrLabel54.Text = "д) нас. пункт:";
      this.xrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbDistrictA
      // 
      this.lbDistrictA.AutoWidth = true;
      this.lbDistrictA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbDistrictA.LocationFloat = new DevExpress.Utils.PointFloat(548.2503F, 1485.499F);
      this.lbDistrictA.Name = "lbDistrictA";
      this.lbDistrictA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbDistrictA.SizeF = new System.Drawing.SizeF(216.2499F, 17F);
      this.lbDistrictA.StylePriority.UseFont = false;
      this.lbDistrictA.StylePriority.UseTextAlignment = false;
      this.lbDistrictA.Text = "lbDistrict";
      this.lbDistrictA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel52
      // 
      this.xrLabel52.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(449.2505F, 1485.499F);
      this.xrLabel52.Name = "xrLabel52";
      this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel52.SizeF = new System.Drawing.SizeF(70.33328F, 23F);
      this.xrLabel52.StylePriority.UseFont = false;
      this.xrLabel52.StylePriority.UseTextAlignment = false;
      this.xrLabel52.Text = "в) район:";
      this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbPostCodeA
      // 
      this.lbPostCodeA.AutoWidth = true;
      this.lbPostCodeA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbPostCodeA.LocationFloat = new DevExpress.Utils.PointFloat(198.7499F, 1443.833F);
      this.lbPostCodeA.Name = "lbPostCodeA";
      this.lbPostCodeA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbPostCodeA.SizeF = new System.Drawing.SizeF(565.7504F, 17F);
      this.lbPostCodeA.StylePriority.UseFont = false;
      this.lbPostCodeA.StylePriority.UseTextAlignment = false;
      this.lbPostCodeA.Text = "lbPostCode";
      this.lbPostCodeA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbSubjectA
      // 
      this.lbSubjectA.AutoWidth = true;
      this.lbSubjectA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSubjectA.LocationFloat = new DevExpress.Utils.PointFloat(146.9168F, 1485.499F);
      this.lbSubjectA.Name = "lbSubjectA";
      this.lbSubjectA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSubjectA.SizeF = new System.Drawing.SizeF(286.6669F, 17F);
      this.lbSubjectA.StylePriority.UseFont = false;
      this.lbSubjectA.StylePriority.UseTextAlignment = false;
      this.lbSubjectA.Text = "lbSubject";
      this.lbSubjectA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbCityA
      // 
      this.lbCityA.AutoWidth = true;
      this.lbCityA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbCityA.LocationFloat = new DevExpress.Utils.PointFloat(146.9168F, 1522.999F);
      this.lbCityA.Name = "lbCityA";
      this.lbCityA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbCityA.SizeF = new System.Drawing.SizeF(286.667F, 17F);
      this.lbCityA.StylePriority.UseFont = false;
      this.lbCityA.StylePriority.UseTextAlignment = false;
      this.lbCityA.Text = "lbTown";
      this.lbCityA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbStreetA
      // 
      this.lbStreetA.AutoWidth = true;
      this.lbStreetA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbStreetA.LocationFloat = new DevExpress.Utils.PointFloat(146.9168F, 1557.707F);
      this.lbStreetA.Name = "lbStreetA";
      this.lbStreetA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbStreetA.SizeF = new System.Drawing.SizeF(190.5419F, 17F);
      this.lbStreetA.StylePriority.UseFont = false;
      this.lbStreetA.StylePriority.UseTextAlignment = false;
      this.lbStreetA.Text = "lbStreet";
      this.lbStreetA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel47
      // 
      this.xrLabel47.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(12.5001F, 1557.707F);
      this.xrLabel47.Name = "xrLabel47";
      this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel47.SizeF = new System.Drawing.SizeF(105.75F, 23F);
      this.xrLabel47.StylePriority.UseFont = false;
      this.xrLabel47.StylePriority.UseTextAlignment = false;
      this.xrLabel47.Text = "е) улица:";
      this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel46
      // 
      this.xrLabel46.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(12.50006F, 1522.999F);
      this.xrLabel46.Name = "xrLabel46";
      this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel46.SizeF = new System.Drawing.SizeF(105.75F, 23F);
      this.xrLabel46.StylePriority.UseFont = false;
      this.xrLabel46.StylePriority.UseTextAlignment = false;
      this.xrLabel46.Text = "г) город:";
      this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel45
      // 
      this.xrLabel45.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(12.5001F, 1485.499F);
      this.xrLabel45.Name = "xrLabel45";
      this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel45.SizeF = new System.Drawing.SizeF(105.75F, 23F);
      this.xrLabel45.StylePriority.UseFont = false;
      this.xrLabel45.StylePriority.UseTextAlignment = false;
      this.xrLabel45.Text = "б) субъект РФ:";
      this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel60
      // 
      this.xrLabel60.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(606.3334F, 1557.707F);
      this.xrLabel60.Name = "xrLabel60";
      this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel60.SizeF = new System.Drawing.SizeF(93.66656F, 23F);
      this.xrLabel60.StylePriority.UseFont = false;
      this.xrLabel60.StylePriority.UseTextAlignment = false;
      this.xrLabel60.Text = "и) квартира:";
      this.xrLabel60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbFlatA
      // 
      this.lbFlatA.AutoWidth = true;
      this.lbFlatA.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbFlatA.LocationFloat = new DevExpress.Utils.PointFloat(700.0001F, 1557.707F);
      this.lbFlatA.Name = "lbFlatA";
      this.lbFlatA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbFlatA.SizeF = new System.Drawing.SizeF(63.99982F, 17F);
      this.lbFlatA.StylePriority.UseFont = false;
      this.lbFlatA.StylePriority.UseTextAlignment = false;
      this.lbFlatA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel43
      // 
      this.xrLabel43.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(12.50002F, 1385.417F);
      this.xrLabel43.Name = "xrLabel43";
      this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel43.SizeF = new System.Drawing.SizeF(752.0003F, 44.875F);
      this.xrLabel43.StylePriority.UseFont = false;
      this.xrLabel43.StylePriority.UseTextAlignment = false;
      this.xrLabel43.Text = "1.13. Адрес места пребывания  (указывается в случае пребывания гражданина по адре" +
    "су, отличному от адреса регистрации по месту жительства)";
      this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel42
      // 
      this.xrLabel42.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(60.41663F, 1348.396F);
      this.xrLabel42.Name = "xrLabel42";
      this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel42.SizeF = new System.Drawing.SizeF(297.4167F, 23F);
      this.xrLabel42.StylePriority.UseFont = false;
      this.xrLabel42.StylePriority.UseTextAlignment = false;
      this.xrLabel42.Text = "лицо без определенного места жительства";
      this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbRegistrationDateByPlace
      // 
      this.lbRegistrationDateByPlace.AutoWidth = true;
      this.lbRegistrationDateByPlace.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbRegistrationDateByPlace.LocationFloat = new DevExpress.Utils.PointFloat(300.3751F, 1310.417F);
      this.lbRegistrationDateByPlace.Name = "lbRegistrationDateByPlace";
      this.lbRegistrationDateByPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbRegistrationDateByPlace.SizeF = new System.Drawing.SizeF(206.9165F, 17F);
      this.lbRegistrationDateByPlace.StylePriority.UseFont = false;
      this.lbRegistrationDateByPlace.StylePriority.UseTextAlignment = false;
      this.lbRegistrationDateByPlace.Text = "lbRegistrationDateByPlace";
      this.lbRegistrationDateByPlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbFlat
      // 
      this.lbFlat.AutoWidth = true;
      this.lbFlat.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbFlat.LocationFloat = new DevExpress.Utils.PointFloat(700.0001F, 1274.291F);
      this.lbFlat.Name = "lbFlat";
      this.lbFlat.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbFlat.SizeF = new System.Drawing.SizeF(63.99982F, 17F);
      this.lbFlat.StylePriority.UseFont = false;
      this.lbFlat.StylePriority.UseTextAlignment = false;
      this.lbFlat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbKorpus
      // 
      this.lbKorpus.AutoWidth = true;
      this.lbKorpus.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbKorpus.LocationFloat = new DevExpress.Utils.PointFloat(560.6252F, 1274.291F);
      this.lbKorpus.Name = "lbKorpus";
      this.lbKorpus.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbKorpus.SizeF = new System.Drawing.SizeF(35.95856F, 17F);
      this.lbKorpus.StylePriority.UseFont = false;
      this.lbKorpus.StylePriority.UseTextAlignment = false;
      this.lbKorpus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbHouse
      // 
      this.lbHouse.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbHouse.LocationFloat = new DevExpress.Utils.PointFloat(412.7951F, 1274.291F);
      this.lbHouse.Name = "lbHouse";
      this.lbHouse.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbHouse.SizeF = new System.Drawing.SizeF(54.12491F, 17F);
      this.lbHouse.StylePriority.UseFont = false;
      this.lbHouse.StylePriority.UseTextAlignment = false;
      this.lbHouse.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbStreet
      // 
      this.lbStreet.AutoWidth = true;
      this.lbStreet.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbStreet.LocationFloat = new DevExpress.Utils.PointFloat(146.9167F, 1274.291F);
      this.lbStreet.Name = "lbStreet";
      this.lbStreet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbStreet.SizeF = new System.Drawing.SizeF(190.5419F, 17F);
      this.lbStreet.StylePriority.UseFont = false;
      this.lbStreet.StylePriority.UseTextAlignment = false;
      this.lbStreet.Text = "lbStreet";
      this.lbStreet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbSattlement
      // 
      this.lbSattlement.AutoWidth = true;
      this.lbSattlement.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSattlement.LocationFloat = new DevExpress.Utils.PointFloat(521.9199F, 1239.583F);
      this.lbSattlement.Name = "lbSattlement";
      this.lbSattlement.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSattlement.SizeF = new System.Drawing.SizeF(242.5804F, 17F);
      this.lbSattlement.StylePriority.UseFont = false;
      this.lbSattlement.StylePriority.UseTextAlignment = false;
      this.lbSattlement.Text = "lbSattlement";
      this.lbSattlement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbTown
      // 
      this.lbTown.AutoWidth = true;
      this.lbTown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbTown.LocationFloat = new DevExpress.Utils.PointFloat(146.9167F, 1239.583F);
      this.lbTown.Name = "lbTown";
      this.lbTown.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbTown.SizeF = new System.Drawing.SizeF(269.9586F, 17F);
      this.lbTown.StylePriority.UseFont = false;
      this.lbTown.StylePriority.UseTextAlignment = false;
      this.lbTown.Text = "lbTown";
      this.lbTown.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbDistrict
      // 
      this.lbDistrict.AutoWidth = true;
      this.lbDistrict.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbDistrict.LocationFloat = new DevExpress.Utils.PointFloat(490.917F, 1202.083F);
      this.lbDistrict.Name = "lbDistrict";
      this.lbDistrict.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbDistrict.SizeF = new System.Drawing.SizeF(273.5832F, 17F);
      this.lbDistrict.StylePriority.UseFont = false;
      this.lbDistrict.StylePriority.UseTextAlignment = false;
      this.lbDistrict.Text = "lbDistrict";
      this.lbDistrict.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel40
      // 
      this.xrLabel40.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 1310.417F);
      this.xrLabel40.Name = "xrLabel40";
      this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel40.SizeF = new System.Drawing.SizeF(279.7084F, 23F);
      this.xrLabel40.StylePriority.UseFont = false;
      this.xrLabel40.StylePriority.UseTextAlignment = false;
      this.xrLabel40.Text = "к) дата регистрации по месту жительства:";
      this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel41
      // 
      this.xrLabel41.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(606.3334F, 1274.291F);
      this.xrLabel41.Name = "xrLabel41";
      this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel41.SizeF = new System.Drawing.SizeF(93.66656F, 23F);
      this.xrLabel41.StylePriority.UseFont = false;
      this.xrLabel41.StylePriority.UseTextAlignment = false;
      this.xrLabel41.Text = "и) квартира:";
      this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel39
      // 
      this.xrLabel39.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(477.9171F, 1274.291F);
      this.xrLabel39.Name = "xrLabel39";
      this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel39.SizeF = new System.Drawing.SizeF(70.33319F, 23F);
      this.xrLabel39.StylePriority.UseFont = false;
      this.xrLabel39.StylePriority.UseTextAlignment = false;
      this.xrLabel39.Text = "з) корпус:";
      this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel38
      // 
      this.xrLabel38.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(347.5836F, 1274.291F);
      this.xrLabel38.Name = "xrLabel38";
      this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel38.SizeF = new System.Drawing.SizeF(57.33334F, 23F);
      this.xrLabel38.StylePriority.UseFont = false;
      this.xrLabel38.StylePriority.UseTextAlignment = false;
      this.xrLabel38.Text = "ж) дом:";
      this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel37
      // 
      this.xrLabel37.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(12.50002F, 1274.291F);
      this.xrLabel37.Name = "xrLabel37";
      this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel37.SizeF = new System.Drawing.SizeF(105.75F, 23F);
      this.xrLabel37.StylePriority.UseFont = false;
      this.xrLabel37.StylePriority.UseTextAlignment = false;
      this.xrLabel37.Text = "е) улица:";
      this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel36
      // 
      this.xrLabel36.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(422.92F, 1239.583F);
      this.xrLabel36.Name = "xrLabel36";
      this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel36.SizeF = new System.Drawing.SizeF(98.99988F, 23F);
      this.xrLabel36.StylePriority.UseFont = false;
      this.xrLabel36.StylePriority.UseTextAlignment = false;
      this.xrLabel36.Text = "д) нас. пункт:";
      this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel35
      // 
      this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 1239.583F);
      this.xrLabel35.Name = "xrLabel35";
      this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel35.SizeF = new System.Drawing.SizeF(105.75F, 23F);
      this.xrLabel35.StylePriority.UseFont = false;
      this.xrLabel35.StylePriority.UseTextAlignment = false;
      this.xrLabel35.Text = "г) город:";
      this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel34
      // 
      this.xrLabel34.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(422.92F, 1202.083F);
      this.xrLabel34.Name = "xrLabel34";
      this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel34.SizeF = new System.Drawing.SizeF(63.53833F, 23F);
      this.xrLabel34.StylePriority.UseFont = false;
      this.xrLabel34.StylePriority.UseTextAlignment = false;
      this.xrLabel34.Text = "в) район:";
      this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbSubject
      // 
      this.lbSubject.AutoWidth = true;
      this.lbSubject.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSubject.LocationFloat = new DevExpress.Utils.PointFloat(146.9167F, 1202.083F);
      this.lbSubject.Name = "lbSubject";
      this.lbSubject.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSubject.SizeF = new System.Drawing.SizeF(269.9586F, 17F);
      this.lbSubject.StylePriority.UseFont = false;
      this.lbSubject.StylePriority.UseTextAlignment = false;
      this.lbSubject.Text = "lbSubject";
      this.lbSubject.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbPostCode
      // 
      this.lbPostCode.AutoWidth = true;
      this.lbPostCode.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbPostCode.LocationFloat = new DevExpress.Utils.PointFloat(198.7498F, 1160.417F);
      this.lbPostCode.Name = "lbPostCode";
      this.lbPostCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbPostCode.SizeF = new System.Drawing.SizeF(565.7504F, 17F);
      this.lbPostCode.StylePriority.UseFont = false;
      this.lbPostCode.StylePriority.UseTextAlignment = false;
      this.lbPostCode.Text = "lbPostCode";
      this.lbPostCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbCitizenship
      // 
      this.lbCitizenship.AutoWidth = true;
      this.lbCitizenship.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbCitizenship.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 818.7504F);
      this.lbCitizenship.Name = "lbCitizenship";
      this.lbCitizenship.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbCitizenship.SizeF = new System.Drawing.SizeF(565.7504F, 17.00012F);
      this.lbCitizenship.StylePriority.UseFont = false;
      this.lbCitizenship.StylePriority.UseTextAlignment = false;
      this.lbCitizenship.Text = "lbCitizenship";
      this.lbCitizenship.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbIssueInfo
      // 
      this.lbIssueInfo.AutoWidth = true;
      this.lbIssueInfo.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbIssueInfo.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 781.2502F);
      this.lbIssueInfo.Name = "lbIssueInfo";
      this.lbIssueInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbIssueInfo.SizeF = new System.Drawing.SizeF(565.7504F, 17F);
      this.lbIssueInfo.StylePriority.UseFont = false;
      this.lbIssueInfo.StylePriority.UseTextAlignment = false;
      this.lbIssueInfo.Text = "lbIssueInfo";
      this.lbIssueInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbNumSeries
      // 
      this.lbNumSeries.AutoWidth = true;
      this.lbNumSeries.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbNumSeries.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 745.8333F);
      this.lbNumSeries.Name = "lbNumSeries";
      this.lbNumSeries.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbNumSeries.SizeF = new System.Drawing.SizeF(565.7504F, 17F);
      this.lbNumSeries.StylePriority.UseFont = false;
      this.lbNumSeries.StylePriority.UseTextAlignment = false;
      this.lbNumSeries.Text = "lbNumSeries";
      this.lbNumSeries.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbBirthPlace
      // 
      this.lbBirthPlace.AutoWidth = true;
      this.lbBirthPlace.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbBirthPlace.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 678.125F);
      this.lbBirthPlace.Name = "lbBirthPlace";
      this.lbBirthPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbBirthPlace.SizeF = new System.Drawing.SizeF(565.2502F, 17F);
      this.lbBirthPlace.StylePriority.UseFont = false;
      this.lbBirthPlace.StylePriority.UseTextAlignment = false;
      this.lbBirthPlace.Text = "lbBirthPlace";
      this.lbBirthPlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbUdlDoc
      // 
      this.lbUdlDoc.AutoWidth = true;
      this.lbUdlDoc.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbUdlDoc.LocationFloat = new DevExpress.Utils.PointFloat(350.0837F, 712.5F);
      this.lbUdlDoc.Name = "lbUdlDoc";
      this.lbUdlDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbUdlDoc.SizeF = new System.Drawing.SizeF(416.9166F, 17F);
      this.lbUdlDoc.StylePriority.UseFont = false;
      this.lbUdlDoc.StylePriority.UseTextAlignment = false;
      this.lbUdlDoc.Text = "lbUdlDoc";
      this.lbUdlDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbBirthdate
      // 
      this.lbBirthdate.AutoWidth = true;
      this.lbBirthdate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbBirthdate.LocationFloat = new DevExpress.Utils.PointFloat(201.2499F, 644.7917F);
      this.lbBirthdate.Name = "lbBirthdate";
      this.lbBirthdate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbBirthdate.SizeF = new System.Drawing.SizeF(565.7502F, 17F);
      this.lbBirthdate.StylePriority.UseFont = false;
      this.lbBirthdate.StylePriority.UseTextAlignment = false;
      this.lbBirthdate.Text = "lbBirthdate";
      this.lbBirthdate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel33
      // 
      this.xrLabel33.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(260.8335F, 845.8333F);
      this.xrLabel33.Name = "xrLabel33";
      this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel33.SizeF = new System.Drawing.SizeF(248.9583F, 15.70837F);
      this.xrLabel33.StylePriority.UseFont = false;
      this.xrLabel33.Text = "(название государства; лицо без гражданства)";
      // 
      // xrLabel32
      // 
      this.xrLabel32.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(12.50002F, 1202.083F);
      this.xrLabel32.Name = "xrLabel32";
      this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel32.SizeF = new System.Drawing.SizeF(105.75F, 23F);
      this.xrLabel32.StylePriority.UseFont = false;
      this.xrLabel32.StylePriority.UseTextAlignment = false;
      this.xrLabel32.Text = "б) субъект РФ:";
      this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel31
      // 
      this.xrLabel31.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(12.50002F, 1160.417F);
      this.xrLabel31.Name = "xrLabel31";
      this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel31.SizeF = new System.Drawing.SizeF(148.5834F, 23F);
      this.xrLabel31.StylePriority.UseFont = false;
      this.xrLabel31.StylePriority.UseTextAlignment = false;
      this.xrLabel31.Text = "а) почтовый индекс:";
      this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel30
      // 
      this.xrLabel30.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 1122.917F);
      this.xrLabel30.Name = "xrLabel30";
      this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel30.SizeF = new System.Drawing.SizeF(480.917F, 23F);
      this.xrLabel30.StylePriority.UseFont = false;
      this.xrLabel30.StylePriority.UseTextAlignment = false;
      this.xrLabel30.Text = "1.12. Адрес регистрации по месту жительства в Российской Федерации:";
      this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel29
      // 
      this.xrLabel29.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 818.7504F);
      this.xrLabel29.Name = "xrLabel29";
      this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel29.SizeF = new System.Drawing.SizeF(153.5835F, 23F);
      this.xrLabel29.StylePriority.UseFont = false;
      this.xrLabel29.StylePriority.UseTextAlignment = false;
      this.xrLabel29.Text = "1.11. Гражданство:";
      this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel28
      // 
      this.xrLabel28.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(12.50014F, 781.2502F);
      this.xrLabel28.Name = "xrLabel28";
      this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel28.SizeF = new System.Drawing.SizeF(177F, 23F);
      this.xrLabel28.StylePriority.UseFont = false;
      this.xrLabel28.StylePriority.UseTextAlignment = false;
      this.xrLabel28.Text = "1.10. Кем и когда выдан:";
      this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel27
      // 
      this.xrLabel27.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(12.50014F, 745.8333F);
      this.xrLabel27.Name = "xrLabel27";
      this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel27.SizeF = new System.Drawing.SizeF(153.5835F, 23F);
      this.xrLabel27.StylePriority.UseFont = false;
      this.xrLabel27.StylePriority.UseTextAlignment = false;
      this.xrLabel27.Text = "1.9. Серия и номер:";
      this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel26
      // 
      this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 712.5F);
      this.xrLabel26.Name = "xrLabel26";
      this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel26.SizeF = new System.Drawing.SizeF(327.1669F, 23F);
      this.xrLabel26.StylePriority.UseFont = false;
      this.xrLabel26.StylePriority.UseTextAlignment = false;
      this.xrLabel26.Text = "1.8. Вид документа, удостоверяющего личность:";
      this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel25
      // 
      this.xrLabel25.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 678.125F);
      this.xrLabel25.Name = "xrLabel25";
      this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel25.SizeF = new System.Drawing.SizeF(151.0835F, 23F);
      this.xrLabel25.StylePriority.UseFont = false;
      this.xrLabel25.StylePriority.UseTextAlignment = false;
      this.xrLabel25.Text = "1.7. Место рождения:";
      this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel24
      // 
      this.xrLabel24.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 644.7917F);
      this.xrLabel24.Name = "xrLabel24";
      this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel24.SizeF = new System.Drawing.SizeF(134.4168F, 23F);
      this.xrLabel24.StylePriority.UseFont = false;
      this.xrLabel24.StylePriority.UseTextAlignment = false;
      this.xrLabel24.Text = "1.6. Дата рождения:";
      this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel23
      // 
      this.xrLabel23.AutoWidth = true;
      this.xrLabel23.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 489.2083F);
      this.xrLabel23.Name = "xrLabel23";
      this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel23.SizeF = new System.Drawing.SizeF(751.9997F, 78.20837F);
      this.xrLabel23.StylePriority.UseFont = false;
      this.xrLabel23.StylePriority.UseTextAlignment = false;
      this.xrLabel23.Text = resources.GetString("xrLabel23.Text");
      this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel22
      // 
      this.xrLabel22.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(260.8335F, 614.6251F);
      this.xrLabel22.Name = "xrLabel22";
      this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel22.SizeF = new System.Drawing.SizeF(248.9583F, 15.70837F);
      this.xrLabel22.StylePriority.UseFont = false;
      this.xrLabel22.Text = "(подпись застрахованного лица или его представителя)";
      // 
      // lbInsuranceCategory
      // 
      this.lbInsuranceCategory.AutoWidth = true;
      this.lbInsuranceCategory.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbInsuranceCategory.LocationFloat = new DevExpress.Utils.PointFloat(277.5001F, 453.875F);
      this.lbInsuranceCategory.Name = "lbInsuranceCategory";
      this.lbInsuranceCategory.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbInsuranceCategory.SizeF = new System.Drawing.SizeF(486.9998F, 17F);
      this.lbInsuranceCategory.StylePriority.UseFont = false;
      this.lbInsuranceCategory.StylePriority.UseTextAlignment = false;
      this.lbInsuranceCategory.Text = "lbInsuranceCategory";
      this.lbInsuranceCategory.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel21
      // 
      this.xrLabel21.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(15.00013F, 453.875F);
      this.xrLabel21.Name = "xrLabel21";
      this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel21.SizeF = new System.Drawing.SizeF(251.5835F, 23F);
      this.xrLabel21.StylePriority.UseFont = false;
      this.xrLabel21.StylePriority.UseTextAlignment = false;
      this.xrLabel21.Text = "1.5. Категория застрахованного лица:";
      this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel20
      // 
      this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(324.0839F, 416.375F);
      this.xrLabel20.Name = "xrLabel20";
      this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel20.SizeF = new System.Drawing.SizeF(220.3334F, 23F);
      this.xrLabel20.StylePriority.UseFont = false;
      this.xrLabel20.StylePriority.UseTextAlignment = false;
      this.xrLabel20.Text = "(нужное отметить знаком \"v\")";
      this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel17
      // 
      this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(15.00013F, 416.375F);
      this.xrLabel17.Name = "xrLabel17";
      this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel17.SizeF = new System.Drawing.SizeF(63.53834F, 23F);
      this.xrLabel17.StylePriority.UseFont = false;
      this.xrLabel17.StylePriority.UseTextAlignment = false;
      this.xrLabel17.Text = "1.4. Пол:";
      this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbMiddleName
      // 
      this.lbMiddleName.AutoWidth = true;
      this.lbMiddleName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbMiddleName.LocationFloat = new DevExpress.Utils.PointFloat(552.5001F, 378.875F);
      this.lbMiddleName.Name = "lbMiddleName";
      this.lbMiddleName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbMiddleName.SizeF = new System.Drawing.SizeF(212F, 17F);
      this.lbMiddleName.StylePriority.UseFont = false;
      this.lbMiddleName.StylePriority.UseTextAlignment = false;
      this.lbMiddleName.Text = "lbMiddleName";
      this.lbMiddleName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbName
      // 
      this.lbName.AutoWidth = true;
      this.lbName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbName.LocationFloat = new DevExpress.Utils.PointFloat(115.0001F, 378.875F);
      this.lbName.Name = "lbName";
      this.lbName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbName.SizeF = new System.Drawing.SizeF(287.5F, 17F);
      this.lbName.StylePriority.UseFont = false;
      this.lbName.StylePriority.UseTextAlignment = false;
      this.lbName.Text = "lbName";
      this.lbName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbFio1
      // 
      this.lbFio1.AutoWidth = true;
      this.lbFio1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbFio1.LocationFloat = new DevExpress.Utils.PointFloat(115.0001F, 341.375F);
      this.lbFio1.Name = "lbFio1";
      this.lbFio1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbFio1.SizeF = new System.Drawing.SizeF(649.4999F, 17F);
      this.lbFio1.StylePriority.UseFont = false;
      this.lbFio1.StylePriority.UseTextAlignment = false;
      this.lbFio1.Text = "lbFio1";
      this.lbFio1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel16
      // 
      this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(427.5001F, 378.875F);
      this.xrLabel16.Name = "xrLabel16";
      this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel16.SizeF = new System.Drawing.SizeF(106.9964F, 23F);
      this.xrLabel16.StylePriority.UseFont = false;
      this.xrLabel16.StylePriority.UseTextAlignment = false;
      this.xrLabel16.Text = "1.3. Отчество:";
      this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel15
      // 
      this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(15.00013F, 378.875F);
      this.xrLabel15.Name = "xrLabel15";
      this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel15.SizeF = new System.Drawing.SizeF(63.53834F, 23F);
      this.xrLabel15.StylePriority.UseFont = false;
      this.xrLabel15.StylePriority.UseTextAlignment = false;
      this.xrLabel15.Text = "1.2. Имя:";
      this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel14
      // 
      this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(15.00013F, 341.375F);
      this.xrLabel14.Name = "xrLabel14";
      this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel14.SizeF = new System.Drawing.SizeF(95.75001F, 23F);
      this.xrLabel14.StylePriority.UseFont = false;
      this.xrLabel14.StylePriority.UseTextAlignment = false;
      this.xrLabel14.Text = "1.1. Фамилия:";
      this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel13
      // 
      this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
      this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(12.50013F, 305.9584F);
      this.xrLabel13.Multiline = true;
      this.xrLabel13.Name = "xrLabel13";
      this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel13.SizeF = new System.Drawing.SizeF(262.4167F, 17F);
      this.xrLabel13.StylePriority.UseFont = false;
      this.xrLabel13.StylePriority.UseTextAlignment = false;
      this.xrLabel13.Text = "1. Сведения о застрахованном лице";
      this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // lbReason
      // 
      this.lbReason.AutoWidth = true;
      this.lbReason.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbReason.LocationFloat = new DevExpress.Utils.PointFloat(87.5F, 268.4584F);
      this.lbReason.Multiline = true;
      this.lbReason.Name = "lbReason";
      this.lbReason.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbReason.SizeF = new System.Drawing.SizeF(674.5F, 17F);
      this.lbReason.StylePriority.UseFont = false;
      this.lbReason.StylePriority.UseTextAlignment = false;
      this.lbReason.Text = "lbReason";
      this.lbReason.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel7
      // 
      this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 268.4584F);
      this.xrLabel7.Name = "xrLabel7";
      this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel7.SizeF = new System.Drawing.SizeF(63.53834F, 23F);
      this.xrLabel7.StylePriority.UseFont = false;
      this.xrLabel7.StylePriority.UseTextAlignment = false;
      this.xrLabel7.Text = "в связи с";
      this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbAsk
      // 
      this.lbAsk.AutoWidth = true;
      this.lbAsk.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbAsk.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 184.2084F);
      this.lbAsk.Name = "lbAsk";
      this.lbAsk.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbAsk.SizeF = new System.Drawing.SizeF(749.4998F, 23F);
      this.lbAsk.StylePriority.UseFont = false;
      this.lbAsk.StylePriority.UseTextAlignment = false;
      this.lbAsk.Text = "ask";
      this.lbAsk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // lbCauseFilling
      // 
      this.lbCauseFilling.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.lbCauseFilling.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 146.7084F);
      this.lbCauseFilling.Name = "lbCauseFilling";
      this.lbCauseFilling.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbCauseFilling.SizeF = new System.Drawing.SizeF(749.4999F, 23F);
      this.lbCauseFilling.StylePriority.UseFont = false;
      this.lbCauseFilling.StylePriority.UseTextAlignment = false;
      this.lbCauseFilling.Text = "causeFilli";
      this.lbCauseFilling.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // xrLabel6
      // 
      this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
      this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(325F, 109.2084F);
      this.xrLabel6.Name = "xrLabel6";
      this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
      this.xrLabel6.StylePriority.UseFont = false;
      this.xrLabel6.StylePriority.UseTextAlignment = false;
      this.xrLabel6.Text = "Заявление";
      this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
      // 
      // lbFio
      // 
      this.lbFio.AutoWidth = true;
      this.lbFio.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbFio.LocationFloat = new DevExpress.Utils.PointFloat(37.5F, 46.70836F);
      this.lbFio.Name = "lbFio";
      this.lbFio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbFio.SizeF = new System.Drawing.SizeF(724.5F, 17.00001F);
      this.lbFio.StylePriority.UseFont = false;
      this.lbFio.StylePriority.UseTextAlignment = false;
      this.lbFio.Text = "Петухов Геннадий Иванович";
      this.lbFio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel5
      // 
      this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 46.70836F);
      this.xrLabel5.Name = "xrLabel5";
      this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel5.SizeF = new System.Drawing.SizeF(20.83F, 23F);
      this.xrLabel5.StylePriority.UseFont = false;
      this.xrLabel5.StylePriority.UseTextAlignment = false;
      this.xrLabel5.Text = "от";
      this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // xrLabel4
      // 
      this.xrLabel4.AutoWidth = true;
      this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(268.75F, 78.04171F);
      this.xrLabel4.Name = "xrLabel4";
      this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel4.SizeF = new System.Drawing.SizeF(217.7083F, 15.70834F);
      this.xrLabel4.StylePriority.UseFont = false;
      this.xrLabel4.Text = "(фамилия, имя, отчество (при наличии) заявителя)";
      // 
      // xrLabel3
      // 
      this.xrLabel3.AutoWidth = true;
      this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 7F);
      this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(246.875F, 17.00001F);
      this.xrLabel3.Name = "xrLabel3";
      this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel3.SizeF = new System.Drawing.SizeF(287.5F, 15.70834F);
      this.xrLabel3.StylePriority.UseFont = false;
      this.xrLabel3.Text = "(наименование страховой медицинской организации (филиала))";
      // 
      // lbSmo
      // 
      this.lbSmo.AutoWidth = true;
      this.lbSmo.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSmo.LocationFloat = new DevExpress.Utils.PointFloat(37.5F, 0F);
      this.lbSmo.Multiline = true;
      this.lbSmo.Name = "lbSmo";
      this.lbSmo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSmo.SizeF = new System.Drawing.SizeF(724.4999F, 17.00001F);
      this.lbSmo.StylePriority.UseFont = false;
      this.lbSmo.StylePriority.UseTextAlignment = false;
      this.lbSmo.Text = "lbSmo";
      this.lbSmo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // xrLabel2
      // 
      this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 11F);
      this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
      this.xrLabel2.Name = "xrLabel2";
      this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel2.SizeF = new System.Drawing.SizeF(9.375F, 23F);
      this.xrLabel2.StylePriority.UseFont = false;
      this.xrLabel2.StylePriority.UseTextAlignment = false;
      this.xrLabel2.Text = "в";
      this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // lbSignDate
      // 
      this.lbSignDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Underline);
      this.lbSignDate.LocationFloat = new DevExpress.Utils.PointFloat(648.7089F, 2354.791F);
      this.lbSignDate.Name = "lbSignDate";
      this.lbSignDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSignDate.SizeF = new System.Drawing.SizeF(115.7917F, 23F);
      this.lbSignDate.StylePriority.UseFont = false;
      this.lbSignDate.Text = "lbSignDate";
      // 
      // xrLabel93
      // 
      this.xrLabel93.LocationFloat = new DevExpress.Utils.PointFloat(429.3787F, 2411.042F);
      this.xrLabel93.Name = "xrLabel93";
      this.xrLabel93.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel93.SizeF = new System.Drawing.SizeF(16.16666F, 23F);
      this.xrLabel93.Text = "/";
      // 
      // lbSignAccept
      // 
      this.lbSignAccept.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Underline);
      this.lbSignAccept.LocationFloat = new DevExpress.Utils.PointFloat(449.2504F, 2411.042F);
      this.lbSignAccept.Name = "lbSignAccept";
      this.lbSignAccept.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.lbSignAccept.SizeF = new System.Drawing.SizeF(315.2501F, 23F);
      this.lbSignAccept.StylePriority.UseFont = false;
      // 
      // TopMargin
      // 
      this.TopMargin.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.TopMargin.HeightF = 13.87501F;
      this.TopMargin.Name = "TopMargin";
      this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
      this.TopMargin.StylePriority.UseBorders = false;
      this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // BottomMargin
      // 
      this.BottomMargin.Name = "BottomMargin";
      this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
      this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // BaseStatementReport
      // 
      this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
      this.Margins = new System.Drawing.Printing.Margins(38, 38, 14, 100);
      this.Version = "13.1";
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion


    #region Fill Data


    public virtual void FillReportData(Statement statement, User currentUser)
    {
      var pvp = statement.PointDistributionPolicy;
      var smo = pvp.Parent;
      var personData = statement.InsuredPersonData;
      var documentUdl = statement.DocumentUdl;
      var residencyDocument = statement.ResidencyDocument;
      var address = statement.Address;
      var address2 = statement.Address2;
      if (address2 == null)
      {
        address2 = address;
      }
      var contactInfo = statement.ContactInfo;
      //var documentRegistration = statement.DocumentRegistration;
      var representative = statement.Representative;

      // Название СМО
      lbSmo.Text = smo.FullName;

      // ФИО
      if (personData != null)
      {
        lbFio.Text = string.Format("{0} {1} {2}", personData.LastName, personData.FirstName, personData.MiddleName);
      }
      //если через представителя
      if (statement.Representative != null)
      {
        lbFio.Text = string.Format("{0} {1} {2}", statement.Representative.LastName, statement.Representative.FirstName, statement.Representative.MiddleName);
      }

      // Причина обращения в СМО
      lbCauseFilling.Text = GetTypeDescription(statement.TypeStatementId);

      lbReason.Text = GetReasonDecription(statement.CauseFiling.Id);


      //Сведения о застрахованном лице

      lbFio1.Text = !string.IsNullOrEmpty(personData.LastName) ? personData.LastName : "-";
      lbName.Text = !string.IsNullOrEmpty(personData.FirstName) ? personData.FirstName : "-";
      lbMiddleName.Text = !string.IsNullOrEmpty(personData.MiddleName) ? personData.MiddleName : "-";
      cbMale.Checked = personData.Gender.Id == Sex.Sex1;
      cbFemale.Checked = personData.Gender.Id == Sex.Sex2;
      lbInsuranceCategory.Text = personData.Category.Name;
      lbBirthdate.Text = personData.Birthday.HasValue ? personData.Birthday.Value.ToString("dd.MM.yyyy") : string.Empty;
      lbBirthPlace.Text = personData.OldCountry == null ? personData.Birthplace : string.Format("{0}, {1}", personData.OldCountry.Name, personData.Birthplace);
      lbUdlDoc.Text = documentUdl.DocumentType.Name;
      lbNumSeries.Text = documentUdl.SeriesNumber;
      lbIssueInfo.Text = string.Format("{0} {1}", documentUdl.IssuingAuthority,
        documentUdl.DateIssue.HasValue ? documentUdl.DateIssue.Value.ToString("dd.MM.yyyy") : string.Empty);
      lbCitizenship.Text = personData.Citizenship != null ? personData.Citizenship.Name : "Б/Г";
      if (personData.IsRefugee)
      {
        lbCitizenship.Text = string.Format("беженец ({0})", lbCitizenship.Text);
      }

      // адрес регистрации по месту жительства
      if (address != null)
      {
        lbPostCode.Text = address.Postcode;
        lbSubject.Text = address.Subject;
        lbDistrict.Text = address.Area;
        lbTown.Text = address.City;
        lbSattlement.Text = address.Town;
        lbStreet.Text = address.Street;
        lbHouse.Text = address.House;
        lbKorpus.Text = address.Housing;
        lbFlat.Text = address.Room.HasValue ? address.Room.Value.ToString() : string.Empty;
        lbRegistrationDateByPlace.Text = address.DateRegistration.HasValue ? address.DateRegistration.Value.ToString("dd.MM.yyyy") : string.Empty;
        cbIsHomeless.Checked = address.IsHomeless.Value;
      }
      if (address2 != null)
      {
        // адрес места пребывания
        // если адреса не совпадают то берём значение из указанного адреса в обратном случае из предыдущего
        lbPostCodeA.Text = address2.Postcode;
        lbSubjectA.Text = address2.Subject;
        lbDistrictA.Text = address2.Area;
        lbCityA.Text = address2.City;
        lbSattlementA.Text = address2.Town;
        lbStreetA.Text = address2.Street;
        lbHouseA.Text = address2.House;
        lbKorpusA.Text = address2.Housing;
        lbFlatA.Text = address2.Room.HasValue ? address2.Room.Value.ToString() : string.Empty;
      }

      //сведения о документе подтверждающем регистрацию
      //if (documentRegistration != null)
      //{
      //  lbRegDoc.Text = documentRegistration.DocumentType.Name;
      //  lbRegNumSeries.Text = documentRegistration.SeriesNumber;
      //  lbRegIssueInfo.Text = string.Format("{0} {1}", documentRegistration.IssuingAuthority,
      //    documentRegistration.DateIssue.HasValue ? documentRegistration.DateIssue.Value.ToString("dd.MM.yyyy") : string.Empty);
      //}
      //используем резиденси документ сейчас в качестве регистрационного
      if (residencyDocument != null)
      {
        lbRegDoc.Text = residencyDocument.DocumentType.Name;
        lbRegNumSeries.Text = residencyDocument.SeriesNumber;
        lbRegIssueInfo.Text = string.Format("{0} {1}", residencyDocument.IssuingAuthority,
          residencyDocument.DateIssue.HasValue ? residencyDocument.DateIssue.Value.ToString("dd.MM.yyyy") : string.Empty);
      }


      //Если беженец то даты берём из документа удл т.к. ResidencyDocument просто не будет
      //сели без гражданства, то берём тоже из удл даты, т.к. в качестве удл будет вид на жительство
      lbFrom.Text = string.Empty;
      lbTo.Text = string.Empty;
      if (personData.IsRefugee || personData.IsNotCitizenship)
      {
        lbFrom.Text = documentUdl.DateIssue.HasValue ? documentUdl.DateIssue.Value.ToString("dd.MM.yyyy") : string.Empty;
        lbTo.Text = documentUdl.DateExp.HasValue ? documentUdl.DateExp.Value.ToString("dd.MM.yyyy") : string.Empty;
      }
      else
      {
        if (residencyDocument != null)
        {
          lbFrom.Text = residencyDocument.DateIssue.HasValue ? residencyDocument.DateIssue.Value.ToString("dd.MM.yyyy") : string.Empty;
          lbTo.Text = residencyDocument.DateExp.HasValue ? residencyDocument.DateExp.Value.ToString("dd.MM.yyyy") : string.Empty;
        }
      }

      //снилс
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        lbSnils1.Text = personData.Snils[0].ToString();
        lbSnils2.Text = personData.Snils[1].ToString();
        lbSnils3.Text = personData.Snils[2].ToString();
        lbSnils4.Text = personData.Snils[3].ToString();
        lbSnils5.Text = personData.Snils[4].ToString();
        lbSnils6.Text = personData.Snils[5].ToString();
        lbSnils7.Text = personData.Snils[6].ToString();
        lbSnils8.Text = personData.Snils[7].ToString();
        lbSnils9.Text = personData.Snils[8].ToString();
        lbSnils10.Text = personData.Snils[9].ToString();
        lbSnils11.Text = personData.Snils[10].ToString();
      }

      //контактная информация
      if (contactInfo != null)
      {
        lbHomePhone.Text = contactInfo.HomePhone;
        lbOficialPhone.Text = contactInfo.WorkPhone;
        lbEmail.Text = contactInfo.Email;
      }

      //Сведения о представителе застрахованного лица
      if (statement.ModeFiling.Id == ModeFiling.ModeFiling2)
      {
        lb2Fio.Text = representative.LastName;
        lb2Name.Text = representative.FirstName;
        lb2MiddleName.Text = representative.MiddleName;
        switch (representative.RelationType.Id)
        {
          case RelationType.Mother:
            cbMother.Checked = true;
            break;
          case RelationType.Father:
            cbFather.Checked = true;
            break;
          case RelationType.Other:
            cbOther.Checked = true;
            break;
        }
        lb2UdlDoc.Text = representative.Document.DocumentType.Name;
        lb2NumSeries.Text = representative.Document.SeriesNumber;
        lb2IssueDate.Text = representative.Document.DateIssue.HasValue ? representative.Document.DateIssue.Value.ToString("dd.MM.yyyy") : string.Empty;
        lb2HomePhone.Text = representative.HomePhone;
        lb2OficialPhone.Text = representative.WorkPhone;
      }
      else
      {
        lb2Fio.Text = string.Empty;
        lb2Name.Text = string.Empty;
        lb2MiddleName.Text = string.Empty;
        lb2UdlDoc.Text = string.Empty;
        lb2NumSeries.Text = string.Empty;
        lb2IssueDate.Text = string.Empty;
        lb2OficialPhone.Text = string.Empty;
        lb2HomePhone.Text = string.Empty;
      }

      //сведения подтверждаю
      if (statement.ModeFiling.Id == ModeFiling.ModeFiling1)
      {
        lbSignExt.Text = string.Format("{0} {1} {2}", personData.LastName, personData.FirstName, personData.MiddleName);
      }
      else
      {
        lbSignExt.Text = string.Format("{0} {1} {2}", representative.LastName, representative.FirstName, representative.MiddleName);
      }
      lbSignDate.Text = statement.DateFiling.Value.ToString("dd.MM.yyyy");
      lbSignAccept.Text = currentUser.Fio;

      //Полис ОМС в составе универсальной электронной карты - временное свидетельство не выдаётся
      if (statement.FormManufacturing.Id == PolisType.К)
      {
        return;
      }

      lbTempNum.Text = null;
      if (statement.AbsentPrevPolicy.Value)
      {
        var temp = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
        if (temp != null)
        {
          lbTempNum.Text = temp.SeriesNumber;
          lbDate.Text = temp.DateFrom.ToString("dd.MM.yyyy");
        }
      }
    }

    /// <summary>
    /// Возвращает дату в формате день.месяц.год из даты в формате год-месяц-деньTвремя
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private string GetDate(string date)
    {
      if (string.IsNullOrEmpty(date))
      {
        return null;
      }
      DateTime result = DateTime.Parse(date);
      return result.ToString("dd.MM.yyyy");
    }

    private static string GetReasonDecription(int causeFilingId)
    {
      switch (causeFilingId)
      {
        case CauseReneval.GettingTheFirst:
          return "получением впервые";
        case CauseReneval.RenevalChangePersonDetails:
          return "изменением фамилии, имени, отчества (при наличии), пола, даты или места рождения, места жительства";
        case CauseReneval.RenevalInaccuracy:
          return "установлением неточности или ошибочности сведений, содержащихся в полисе";
        case CauseReneval.RenevalUnusable:
          return "ветхостью и непригодностью полиса";
        case CauseReneval.RenevalLoss:
          return "утратой ранее выданного полиса";
        case CauseReneval.RenevalExpiration:
          return "окончанием срока действия полиса";
        case CauseReneval.Edit:
          return "изменением данных о ЗЛ, не требующих выдачи нового полиса ОМС";

        case CauseReinsurance.Initialization:
          return "заявлением на выбор или замену СМО впервые";
        case CauseReinsurance.Choice:
          return "выбором страховой медицинской организации";
        case CauseReinsurance.ReinsuranceAtWill:
          return "заменой страховой медицинской организации в соответствии с правом замены один раз в течение календарного года";
        case CauseReinsurance.ReinsuranceWithTheMove:
          return " заменой страховой медицинской организации в связи со сменой места жительства";
        case CauseReinsurance.ReinsuranceStopFinance:
          return "заменой страховой медицинской организации в связи с прекращением действия договора о финансовом обеспечении обязательного медицинского страхования";
        default: return null;
      }
    }

    private static string GetTypeDescription(int typeId)
    {
      switch (typeId)
      {
        case TypeStatement.TypeStatement1:
          return "о выборе (замене)  страховой медицинской организации";
        case TypeStatement.TypeStatement2:
          return "о переоформлении полиса ОМС";
        case TypeStatement.TypeStatement3:
          return "о выдаче дубликата";
        case TypeStatement.TypeStatement4:
          return "об изменении данных о ЗЛ, не требующих выдачи нового полиса ОМС";
        default: return null;
      }
    }

    public string GetPolisType(Statement statement)
    {
      return GetPolisTypeDescription(statement.FormManufacturing.Id);
    }

    private static string GetPolisTypeDescription(int id)
    {
      switch (id)
      {
        // Временное свидетельство. не используется для печати
        case PolisType.В:
          return "Временное свидетельство";
        //Полис ОМС в составе универсальной электронной карты
        case PolisType.К:
          return "в составе универсальной электронной карты гражданина";
        //Бумажный полис ОМС единого образца
        case PolisType.П:
          return "в форме бумажного бланка";
        //Полис ОМС старого образ-ца. не используется для печати
        case PolisType.С:
          return "Полис ОМС старого образ-ца";
        //Электронный полис ОМС единого образца
        case PolisType.Э:
          return "в форме пластиковой карты с электронным носителем";
        default: return null;
      }
    }

    #endregion
  }
}
