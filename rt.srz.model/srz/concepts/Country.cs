// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Country.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> ������ </summary>
  public class Country : Concept
  {
    #region Constants

    /// <summary> ������� </summary>
    public const int ABH = 19;

    /// <summary> ����� </summary>
    public const int ABW = 33;

    /// <summary> ���������� </summary>
    public const int AFG = 34;

    /// <summary> ������ </summary>
    public const int AGO = 27;

    /// <summary> ������� </summary>
    public const int AIA = 26;

    /// <summary> ��������� ������� </summary>
    public const int ALA = 259;

    /// <summary> ������� </summary>
    public const int ALB = 23;

    /// <summary> ������� </summary>
    public const int AND = 28;

    /// <summary> ������������ �������� ������� </summary>
    public const int ARE = 167;

    /// <summary> ��������� </summary>
    public const int ARG = 31;

    /// <summary> ������� </summary>
    public const int ARM = 32;

    /// <summary> ������������ ����� </summary>
    public const int ASM = 25;

    /// <summary> ���������� </summary>
    public const int ATA = 29;

    /// <summary> ����������� ����� ���������� </summary>
    public const int ATF = 246;

    /// <summary> ������� � ������� </summary>
    public const int ATG = 30;

    /// <summary> ��������� </summary>
    public const int AUS = 20;

    /// <summary> ������� </summary>
    public const int AUT = 21;

    /// <summary> ����������� </summary>
    public const int AZE = 22;

    /// <summary> ������� </summary>
    public const int BDI = 53;

    /// <summary> ������� </summary>
    public const int BEL = 41;

    /// <summary> ����� </summary>
    public const int BEN = 42;

    /// <summary> ������, ����-�������� � ���� </summary>
    public const int BES = 46;

    /// <summary> �������-���� </summary>
    public const int BFA = 52;

    /// <summary> ��������� </summary>
    public const int BGD = 36;

    /// <summary> �������� </summary>
    public const int BGR = 44;

    /// <summary> ������� </summary>
    public const int BHR = 38;

    /// <summary> ������ </summary>
    public const int BHS = 35;

    /// <summary> ������ � ����������� </summary>
    public const int BIH = 47;

    /// <summary> ���-��������� </summary>
    public const int BLM = 201;

    /// <summary> �������� </summary>
    public const int BLR = 39;

    /// <summary> ����� </summary>
    public const int BLZ = 40;

    /// <summary> ������� </summary>
    public const int BMU = 43;

    /// <summary> �������, ����������������� ����������� </summary>
    public const int BOL = 45;

    /// <summary> �������� </summary>
    public const int BRA = 49;

    /// <summary> �������� </summary>
    public const int BRB = 37;

    /// <summary> ������-���������� </summary>
    public const int BRN = 51;

    /// <summary> ����� </summary>
    public const int BTN = 54;

    /// <summary> ������ ���� </summary>
    public const int BVT = 172;

    /// <summary> �������� </summary>
    public const int BWA = 48;

    /// <summary> ����������-����������� ���������� </summary>
    public const int CAF = 248;

    /// <summary> ������ </summary>
    public const int CAN = 105;

    /// <summary> ��������� (������) ������� </summary>
    public const int CCK = 112;

    /// <summary> ��������� </summary>
    public const int CHE = 253;

    /// <summary> ���� </summary>
    public const int CHL = 252;

    /// <summary> ����� </summary>
    public const int CHN = 111;

    /// <summary> ��� Ē����� </summary>
    public const int CIV = 120;

    /// <summary> ������� </summary>
    public const int CMR = 104;

    /// <summary> �����, ��������������� ���������� </summary>
    public const int COD = 116;

    /// <summary> ����� </summary>
    public const int COG = 115;

    /// <summary> ������� ���� </summary>
    public const int COK = 170;

    /// <summary> �������� </summary>
    public const int COL = 113;

    /// <summary> ������ </summary>
    public const int COM = 114;

    /// <summary> ����-����� </summary>
    public const int CPV = 101;

    /// <summary> �����-���� </summary>
    public const int CRI = 119;

    /// <summary> ���� </summary>
    public const int CUB = 121;

    /// <summary> ������� </summary>
    public const int CUW = 123;

    /// <summary> ������ ��������� </summary>
    public const int CXR = 175;

    /// <summary> ������� ������ </summary>
    public const int CYM = 169;

    /// <summary> ���� </summary>
    public const int CYP = 108;

    /// <summary> ������� ���������� </summary>
    public const int CZE = 251;

    /// <summary> �������� </summary>
    public const int DEU = 71;

    /// <summary> ������� </summary>
    public const int DJI = 83;

    /// <summary> �������� </summary>
    public const int DMA = 84;

    /// <summary> ����� </summary>
    public const int DNK = 81;

    /// <summary> ������������� ���������� </summary>
    public const int DOM = 85;

    /// <summary> ����� </summary>
    public const int DZA = 24;

    /// <summary> ������� </summary>
    public const int ECU = 257;

    /// <summary> ������ </summary>
    public const int EGY = 86;

    /// <summary> ������� </summary>
    public const int ERI = 261;

    /// <summary> �������� ������ </summary>
    public const int ESH = 88;

    /// <summary> ������� </summary>
    public const int ESP = 98;

    /// <summary> ������� </summary>
    public const int EST = 262;

    /// <summary> ������� </summary>
    public const int ETH = 263;

    /// <summary> ��������� </summary>
    public const int FIN = 241;

    /// <summary> ����� </summary>
    public const int FJI = 239;

    /// <summary> ������������ ������� (�����������) </summary>
    public const int FLK = 242;

    /// <summary> ������� </summary>
    public const int FRA = 243;

    /// <summary> ��������� ������� </summary>
    public const int FRO = 238;

    /// <summary> ����������, ������������ ����� </summary>
    public const int FSM = 149;

    /// <summary> ����� </summary>
    public const int GAB = 62;

    /// <summary> ����������� ����������� </summary>
    public const int GBR = 214;

    /// <summary> ������ </summary>
    public const int GEO = 79;

    /// <summary> ������ </summary>
    public const int GGY = 72;

    /// <summary> ���� </summary>
    public const int GHA = 66;

    /// <summary> ��������� </summary>
    public const int GIB = 73;

    /// <summary> ������ </summary>
    public const int GIN = 69;

    /// <summary> ��������� </summary>
    public const int GLP = 67;

    /// <summary> ������ </summary>
    public const int GMB = 65;

    /// <summary> ������-����� </summary>
    public const int GNB = 70;

    /// <summary> �������������� ������ </summary>
    public const int GNQ = 258;

    /// <summary> ������ </summary>
    public const int GRC = 78;

    /// <summary> ������� </summary>
    public const int GRD = 76;

    /// <summary> ���������� </summary>
    public const int GRL = 77;

    /// <summary> ��������� </summary>
    public const int GTM = 68;

    /// <summary> ����������� ������ </summary>
    public const int GUF = 244;

    /// <summary> ���� </summary>
    public const int GUM = 80;

    /// <summary> ������ </summary>
    public const int GUY = 64;

    /// <summary> ������� </summary>
    public const int HKG = 75;

    /// <summary> ������ ���� � ������� ���������� </summary>
    public const int HMD = 176;

    /// <summary> �������� </summary>
    public const int HND = 74;

    /// <summary> �������� </summary>
    public const int HRV = 247;

    /// <summary> ����� </summary>
    public const int HTI = 63;

    /// <summary> ������� </summary>
    public const int HUN = 56;

    /// <summary> ��������� </summary>
    public const int IDN = 92;

    /// <summary> ������ ��� </summary>
    public const int IMN = 173;

    /// <summary> ����� </summary>
    public const int IND = 91;

    /// <summary> ���������� ���������� � ��������� ������ </summary>
    public const int IOT = 50;

    /// <summary> �������� </summary>
    public const int IRL = 96;

    /// <summary> ����, ��������� ���������� </summary>
    public const int IRN = 95;

    /// <summary> ���� </summary>
    public const int IRQ = 94;

    /// <summary> �������� </summary>
    public const int ISL = 97;

    /// <summary> ������� </summary>
    public const int ISR = 90;

    /// <summary> ������ </summary>
    public const int ITA = 99;

    /// <summary> ������ </summary>
    public const int JAM = 268;

    /// <summary> ������ </summary>
    public const int JEY = 82;

    /// <summary> �������� </summary>
    public const int JOR = 93;

    /// <summary> ������ </summary>
    public const int JPN = 269;

    /// <summary> ��������� </summary>
    public const int KAZ = 102;

    /// <summary> ����� </summary>
    public const int KEN = 107;

    /// <summary> �������� </summary>
    public const int KGZ = 109;

    /// <summary> �������� </summary>
    public const int KHM = 103;

    /// <summary> �������� </summary>
    public const int KIR = 110;

    /// <summary> ����-���� � ����� </summary>
    public const int KNA = 206;

    /// <summary> �����, ���������� </summary>
    public const int KOR = 118;

    /// <summary> ������ </summary>
    public const int KWT = 122;

    /// <summary> �������� �������-��������������� ���������� </summary>
    public const int LAO = 124;

    /// <summary> ����� </summary>
    public const int LBN = 128;

    /// <summary> ������� </summary>
    public const int LBR = 127;

    /// <summary> ����� </summary>
    public const int LBY = 129;

    /// <summary> ����-����� </summary>
    public const int LCA = 207;

    /// <summary> ����������� </summary>
    public const int LIE = 131;

    /// <summary> ���-����� </summary>
    public const int LKA = 256;

    /// <summary> ������ </summary>
    public const int LSO = 126;

    /// <summary> ����� </summary>
    public const int LTU = 130;

    /// <summary> ���������� </summary>
    public const int LUX = 132;

    /// <summary> ������ </summary>
    public const int LVA = 125;

    /// <summary> ����� </summary>
    public const int MAC = 137;

    /// <summary> ���-������ </summary>
    public const int MAF = 202;

    /// <summary> ������� </summary>
    public const int MAR = 145;

    /// <summary> ������ </summary>
    public const int MCO = 152;

    /// <summary> �������, ���������� </summary>
    public const int MDA = 151;

    /// <summary> ���������� </summary>
    public const int MDG = 135;

    /// <summary> �������� </summary>
    public const int MDV = 143;

    /// <summary> ������� </summary>
    public const int MEX = 148;

    /// <summary> ���������� ������� </summary>
    public const int MHL = 147;

    /// <summary> ���������� ���������[3] </summary>
    public const int MKD = 138;

    /// <summary> ���� </summary>
    public const int MLI = 141;

    /// <summary> ������ </summary>
    public const int MLT = 144;

    /// <summary> ������ </summary>
    public const int MMR = 155;

    /// <summary> ���������� </summary>
    public const int MNE = 250;

    /// <summary> �������� </summary>
    public const int MNG = 153;

    /// <summary> �������� ���������� ������� </summary>
    public const int MNP = 199;

    /// <summary> �������� </summary>
    public const int MOZ = 150;

    /// <summary> ���������� </summary>
    public const int MRT = 134;

    /// <summary> ���������� </summary>
    public const int MSR = 154;

    /// <summary> ��������� </summary>
    public const int MTQ = 146;

    /// <summary> �������� </summary>
    public const int MUS = 133;

    /// <summary> ������ </summary>
    public const int MWI = 139;

    /// <summary> �������� </summary>
    public const int MYS = 140;

    /// <summary> ������� </summary>
    public const int MYT = 136;

    /// <summary> ������� </summary>
    public const int NAM = 156;

    /// <summary> ����� ��������� </summary>
    public const int NCL = 165;

    /// <summary> ����� </summary>
    public const int NER = 159;

    /// <summary> ������ ������� </summary>
    public const int NFK = 174;

    /// <summary> ������� </summary>
    public const int NGA = 160;

    /// <summary> ��������� </summary>
    public const int NIC = 162;

    /// <summary> ���� </summary>
    public const int NIU = 163;

    /// <summary> ���������� </summary>
    public const int NLD = 161;

    /// <summary> �������� </summary>
    public const int NOR = 166;

    /// <summary> ����� </summary>
    public const int NPL = 158;

    /// <summary> ����� </summary>
    public const int NRU = 157;

    /// <summary> ����� �������� </summary>
    public const int NZL = 164;

    /// <summary> ���� </summary>
    public const int OMN = 168;

    /// <summary> ����� ������ </summary>
    public const int OST = 266;

    /// <summary> �������� </summary>
    public const int PAK = 177;

    /// <summary> ������ </summary>
    public const int PAN = 180;

    /// <summary> ������� </summary>
    public const int PCN = 185;

    /// <summary> ���� </summary>
    public const int PER = 184;

    /// <summary> ��������� </summary>
    public const int PHL = 240;

    /// <summary> ����� </summary>
    public const int PLW = 178;

    /// <summary> �����-����� ������ </summary>
    public const int PNG = 182;

    /// <summary> ������ </summary>
    public const int POL = 186;

    /// <summary> ������-���� </summary>
    public const int PRI = 188;

    /// <summary> �����, �������-��������������� ���������� </summary>
    public const int PRK = 117;

    /// <summary> ���������� </summary>
    public const int PRT = 187;

    /// <summary> �������� </summary>
    public const int PRY = 183;

    /// <summary> ������������ ����������, �������������� </summary>
    public const int PSE = 179;

    /// <summary> ����������� ��������� </summary>
    public const int PYF = 245;

    /// <summary> ����� </summary>
    public const int QAT = 106;

    /// <summary> ������� </summary>
    public const int REU = 189;

    /// <summary> ������� </summary>
    public const int ROU = 192;

    /// <summary> ������ </summary>
    public const int RUS = 190;

    /// <summary> ������ </summary>
    public const int RWA = 191;

    /// <summary> ���������� ������ </summary>
    public const int SAU = 196;

    /// <summary> ����� </summary>
    public const int SDN = 218;

    /// <summary> ������� </summary>
    public const int SEN = 204;

    /// <summary> �������� </summary>
    public const int SGP = 210;

    /// <summary> ����� �������� � ����� ���������� ������� </summary>
    public const int SGS = 265;

    /// <summary> ������ �����, ������ ����������, �������-��-����� </summary>
    public const int SHN = 198;

    /// <summary> ���������� � �� ����� </summary>
    public const int SJM = 255;

    /// <summary> ���������� ������� </summary>
    public const int SLB = 216;

    /// <summary> ������-����� </summary>
    public const int SLE = 220;

    /// <summary> ���-��������� </summary>
    public const int SLV = 260;

    /// <summary> ���-������ </summary>
    public const int SMR = 194;

    /// <summary> ������ </summary>
    public const int SOM = 217;

    /// <summary> ���-���� � ������� </summary>
    public const int SPM = 208;

    /// <summary> ������ </summary>
    public const int SRB = 209;

    /// <summary> ����� ����� </summary>
    public const int SSD = 267;

    /// <summary> ���-���� � �������� </summary>
    public const int STP = 195;

    /// <summary> ������� </summary>
    public const int SUR = 219;

    /// <summary> �������� </summary>
    public const int SVK = 212;

    /// <summary> �������� </summary>
    public const int SVN = 213;

    /// <summary> ������ </summary>
    public const int SWE = 254;

    /// <summary> ��������� </summary>
    public const int SWZ = 197;

    /// <summary> ���-������ (������������� �����) </summary>
    public const int SXM = 203;

    /// <summary> ������� </summary>
    public const int SYC = 200;

    /// <summary> ��������� �������� ���������� </summary>
    public const int SYR = 211;

    /// <summary> ������� ����� � ������ </summary>
    public const int TCA = 171;

    /// <summary> ��� </summary>
    public const int TCD = 249;

    /// <summary> ���� </summary>
    public const int TGO = 225;

    /// <summary> ������� </summary>
    public const int THA = 222;

    /// <summary> ����������� </summary>
    public const int TJK = 221;

    /// <summary> ������� </summary>
    public const int TKL = 226;

    /// <summary> ��������� </summary>
    public const int TKM = 231;

    /// <summary> �����-����� </summary>
    public const int TLS = 60;

    /// <summary> ����� </summary>
    public const int TON = 227;

    /// <summary> �������� � ������ </summary>
    public const int TTO = 228;

    /// <summary> ����� </summary>
    public const int TUN = 230;

    /// <summary> ������ </summary>
    public const int TUR = 232;

    /// <summary> ������ </summary>
    public const int TUV = 229;

    /// <summary> ������� (�����) </summary>
    public const int TWN = 223;

    /// <summary> ��������, ������������ ���������� </summary>
    public const int TZA = 224;

    /// <summary> ������ </summary>
    public const int UGA = 233;

    /// <summary> ������� </summary>
    public const int UKR = 235;

    /// <summary> ����� ������������� ���������� ������� ����������� ������ </summary>
    public const int UMI = 142;

    /// <summary> ������� </summary>
    public const int URY = 237;

    /// <summary> ����������� ����� </summary>
    public const int USA = 215;

    /// <summary> ���������� </summary>
    public const int UZB = 234;

    /// <summary> ������� ������� (����������� � ����� �������) </summary>
    public const int VAT = 181;

    /// <summary> ����-������� � ��������� </summary>
    public const int VCT = 205;

    /// <summary> ��������� �������������� ���������� </summary>
    public const int VEN = 57;

    /// <summary> ���������� �������, ���������� </summary>
    public const int VGB = 58;

    /// <summary> ���������� �������, ��� </summary>
    public const int VIR = 59;

    /// <summary> ������� </summary>
    public const int VNM = 61;

    /// <summary> ������� </summary>
    public const int VUT = 55;

    /// <summary> ������ � ������ </summary>
    public const int WLF = 236;

    /// <summary> ����� </summary>
    public const int WSM = 193;

    /// <summary> ����� </summary>
    public const int YEM = 100;

    /// <summary> ����� ������ </summary>
    public const int ZAF = 264;

    /// <summary> ������ </summary>
    public const int ZMB = 87;

    /// <summary> �������� </summary>
    public const int ZWE = 89;

    #endregion
  }
}