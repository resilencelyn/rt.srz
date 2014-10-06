// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Country.cs" company="пСЯахрЕУ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   яРПЮМЮ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> яРПЮМЮ </summary>
  public class Country : Concept
  {
    #region Constants

    /// <summary> юауюгхъ </summary>
    public const int ABH = 19;

    /// <summary> юпсаю </summary>
    public const int ABW = 33;

    /// <summary> ютцюмхярюм </summary>
    public const int AFG = 34;

    /// <summary> юмцнкю </summary>
    public const int AGO = 27;

    /// <summary> юмцхкэъ </summary>
    public const int AIA = 26;

    /// <summary> щкюмдяйхе нярпнбю </summary>
    public const int ALA = 259;

    /// <summary> юкаюмхъ </summary>
    public const int ALB = 23;

    /// <summary> юмднппю </summary>
    public const int AND = 28;

    /// <summary> назедхмеммше юпюаяйхе щлхпюрш </summary>
    public const int ARE = 167;

    /// <summary> юпцемрхмю </summary>
    public const int ARG = 31;

    /// <summary> юплемхъ </summary>
    public const int ARM = 32;

    /// <summary> юлепхйюмяйне яюлню </summary>
    public const int ASM = 25;

    /// <summary> юмрюпйрхдю </summary>
    public const int ATA = 29;

    /// <summary> тпюмжсгяйхе чфмше реппхрнпхх </summary>
    public const int ATF = 246;

    /// <summary> юмрхцсю х аюпасдю </summary>
    public const int ATG = 30;

    /// <summary> юбярпюкхъ </summary>
    public const int AUS = 20;

    /// <summary> юбярпхъ </summary>
    public const int AUT = 21;

    /// <summary> югепаюидфюм </summary>
    public const int AZE = 22;

    /// <summary> аспсмдх </summary>
    public const int BDI = 53;

    /// <summary> аекэцхъ </summary>
    public const int BEL = 41;

    /// <summary> аемхм </summary>
    public const int BEN = 42;

    /// <summary> анмщип, яхмр-щярюрхся х яюаю </summary>
    public const int BES = 46;

    /// <summary> аспйхмю-тюян </summary>
    public const int BFA = 52;

    /// <summary> аюмцкюдеь </summary>
    public const int BGD = 36;

    /// <summary> анкцюпхъ </summary>
    public const int BGR = 44;

    /// <summary> аюупеим </summary>
    public const int BHR = 38;

    /// <summary> аюцюлш </summary>
    public const int BHS = 35;

    /// <summary> анямхъ х цепжецнбхмю </summary>
    public const int BIH = 47;

    /// <summary> яем-аюпрекелх </summary>
    public const int BLM = 201;

    /// <summary> аекюпсяэ </summary>
    public const int BLR = 39;

    /// <summary> аекхг </summary>
    public const int BLZ = 40;

    /// <summary> аеплсдш </summary>
    public const int BMU = 43;

    /// <summary> анкхбхъ, лмнцнмюжхнмюкэмне цнясдюпярбн </summary>
    public const int BOL = 45;

    /// <summary> апюгхкхъ </summary>
    public const int BRA = 49;

    /// <summary> аюпаюдня </summary>
    public const int BRB = 37;

    /// <summary> апсмеи-дюпсяяюкюл </summary>
    public const int BRN = 51;

    /// <summary> асрюм </summary>
    public const int BTN = 54;

    /// <summary> нярпнб асбе </summary>
    public const int BVT = 172;

    /// <summary> анрябюмю </summary>
    public const int BWA = 48;

    /// <summary> жемрпюкэмн-ютпхйюмяйюъ пеяосакхйю </summary>
    public const int CAF = 248;

    /// <summary> йюмюдю </summary>
    public const int CAN = 105;

    /// <summary> йнйнянбше (йхкхмц) нярпнбю </summary>
    public const int CCK = 112;

    /// <summary> ьбеижюпхъ </summary>
    public const int CHE = 253;

    /// <summary> вхкх </summary>
    public const int CHL = 252;

    /// <summary> йхрюи </summary>
    public const int CHN = 111;

    /// <summary> йнр д▓хбсюп </summary>
    public const int CIV = 120;

    /// <summary> йюлепсм </summary>
    public const int CMR = 104;

    /// <summary> йнмцн, делнйпюрхвеяйюъ пеяосакхйю </summary>
    public const int COD = 116;

    /// <summary> йнмцн </summary>
    public const int COG = 115;

    /// <summary> нярпнбю йсйю </summary>
    public const int COK = 170;

    /// <summary> йнкслахъ </summary>
    public const int COL = 113;

    /// <summary> йнлнпш </summary>
    public const int COM = 114;

    /// <summary> йюан-бепде </summary>
    public const int CPV = 101;

    /// <summary> йнярю-пхйю </summary>
    public const int CRI = 119;

    /// <summary> йсаю </summary>
    public const int CUB = 121;

    /// <summary> йчпюяюн </summary>
    public const int CUW = 123;

    /// <summary> нярпнб пнфдеярбю </summary>
    public const int CXR = 175;

    /// <summary> нярпнбю йюилюм </summary>
    public const int CYM = 169;

    /// <summary> йхоп </summary>
    public const int CYP = 108;

    /// <summary> веьяйюъ пеяосакхйю </summary>
    public const int CZE = 251;

    /// <summary> цеплюмхъ </summary>
    public const int DEU = 71;

    /// <summary> дфхасрх </summary>
    public const int DJI = 83;

    /// <summary> днлхмхйю </summary>
    public const int DMA = 84;

    /// <summary> дюмхъ </summary>
    public const int DNK = 81;

    /// <summary> днлхмхйюмяйюъ пеяосакхйю </summary>
    public const int DOM = 85;

    /// <summary> юкфхп </summary>
    public const int DZA = 24;

    /// <summary> щйбюднп </summary>
    public const int ECU = 257;

    /// <summary> ецхоер </summary>
    public const int EGY = 86;

    /// <summary> щпхрпеъ </summary>
    public const int ERI = 261;

    /// <summary> гюоюдмюъ яюуюпю </summary>
    public const int ESH = 88;

    /// <summary> хяоюмхъ </summary>
    public const int ESP = 98;

    /// <summary> щярнмхъ </summary>
    public const int EST = 262;

    /// <summary> щтхнохъ </summary>
    public const int ETH = 263;

    /// <summary> тхмкъмдхъ </summary>
    public const int FIN = 241;

    /// <summary> тхдфх </summary>
    public const int FJI = 239;

    /// <summary> тнкйкемдяйхе нярпнбю (люкэбхмяйхе) </summary>
    public const int FLK = 242;

    /// <summary> тпюмжхъ </summary>
    public const int FRA = 243;

    /// <summary> тюпепяйхе нярпнбю </summary>
    public const int FRO = 238;

    /// <summary> лхйпнмегхъ, тедепюрхбмше ьрюрш </summary>
    public const int FSM = 149;

    /// <summary> цюанм </summary>
    public const int GAB = 62;

    /// <summary> янедхмеммне йнпнкебярбн </summary>
    public const int GBR = 214;

    /// <summary> цпсгхъ </summary>
    public const int GEO = 79;

    /// <summary> цепмях </summary>
    public const int GGY = 72;

    /// <summary> цюмю </summary>
    public const int GHA = 66;

    /// <summary> цхапюкрюп </summary>
    public const int GIB = 73;

    /// <summary> цбхмеъ </summary>
    public const int GIN = 69;

    /// <summary> цбюдексою </summary>
    public const int GLP = 67;

    /// <summary> цюлахъ </summary>
    public const int GMB = 65;

    /// <summary> цбхмеъ-ахяюс </summary>
    public const int GNB = 70;

    /// <summary> щйбюрнпхюкэмюъ цбхмеъ </summary>
    public const int GNQ = 258;

    /// <summary> цпежхъ </summary>
    public const int GRC = 78;

    /// <summary> цпемюдю </summary>
    public const int GRD = 76;

    /// <summary> цпемкюмдхъ </summary>
    public const int GRL = 77;

    /// <summary> цбюрелюкю </summary>
    public const int GTM = 68;

    /// <summary> тпюмжсгяйюъ цбхюмю </summary>
    public const int GUF = 244;

    /// <summary> цсюл </summary>
    public const int GUM = 80;

    /// <summary> цюиюмю </summary>
    public const int GUY = 64;

    /// <summary> цнмйнмц </summary>
    public const int HKG = 75;

    /// <summary> нярпнб уепд х нярпнбю люйднмюкэд </summary>
    public const int HMD = 176;

    /// <summary> цнмдспюя </summary>
    public const int HND = 74;

    /// <summary> унпбюрхъ </summary>
    public const int HRV = 247;

    /// <summary> цюхрх </summary>
    public const int HTI = 63;

    /// <summary> бемцпхъ </summary>
    public const int HUN = 56;

    /// <summary> хмднмегхъ </summary>
    public const int IDN = 92;

    /// <summary> нярпнб лщм </summary>
    public const int IMN = 173;

    /// <summary> хмдхъ </summary>
    public const int IND = 91;

    /// <summary> апхрюмяйюъ реппхрнпхъ б хмдхияйнл нйеюме </summary>
    public const int IOT = 50;

    /// <summary> хпкюмдхъ </summary>
    public const int IRL = 96;

    /// <summary> хпюм, хякюляйюъ пеяосакхйю </summary>
    public const int IRN = 95;

    /// <summary> хпюй </summary>
    public const int IRQ = 94;

    /// <summary> хякюмдхъ </summary>
    public const int ISL = 97;

    /// <summary> хгпюхкэ </summary>
    public const int ISR = 90;

    /// <summary> хрюкхъ </summary>
    public const int ITA = 99;

    /// <summary> ълюийю </summary>
    public const int JAM = 268;

    /// <summary> дфепях </summary>
    public const int JEY = 82;

    /// <summary> хнпдюмхъ </summary>
    public const int JOR = 93;

    /// <summary> ъонмхъ </summary>
    public const int JPN = 269;

    /// <summary> йюгюуярюм </summary>
    public const int KAZ = 102;

    /// <summary> йемхъ </summary>
    public const int KEN = 107;

    /// <summary> йхпцхгхъ </summary>
    public const int KGZ = 109;

    /// <summary> йюландфю </summary>
    public const int KHM = 103;

    /// <summary> йхпхаюрх </summary>
    public const int KIR = 110;

    /// <summary> яемр-йхря х мебхя </summary>
    public const int KNA = 206;

    /// <summary> йнпеъ, пеяосакхйю </summary>
    public const int KOR = 118;

    /// <summary> йсбеир </summary>
    public const int KWT = 122;

    /// <summary> кюняяйюъ мюпндмн-делнйпюрхвеяйюъ пеяосакхйю </summary>
    public const int LAO = 124;

    /// <summary> кхбюм </summary>
    public const int LBN = 128;

    /// <summary> кхаепхъ </summary>
    public const int LBR = 127;

    /// <summary> кхбхъ </summary>
    public const int LBY = 129;

    /// <summary> яемр-кчяхъ </summary>
    public const int LCA = 207;

    /// <summary> кхуремьреим </summary>
    public const int LIE = 131;

    /// <summary> ьпх-кюмйю </summary>
    public const int LKA = 256;

    /// <summary> кеянрн </summary>
    public const int LSO = 126;

    /// <summary> кхрбю </summary>
    public const int LTU = 130;

    /// <summary> кчйяеласпц </summary>
    public const int LUX = 132;

    /// <summary> кюрбхъ </summary>
    public const int LVA = 125;

    /// <summary> люйюн </summary>
    public const int MAC = 137;

    /// <summary> яем-люпрем </summary>
    public const int MAF = 202;

    /// <summary> люпнййн </summary>
    public const int MAR = 145;

    /// <summary> лнмюйн </summary>
    public const int MCO = 152;

    /// <summary> лнкднбю, пеяосакхйю </summary>
    public const int MDA = 151;

    /// <summary> людюцюяйюп </summary>
    public const int MDG = 135;

    /// <summary> люкэдхбш </summary>
    public const int MDV = 143;

    /// <summary> лейяхйю </summary>
    public const int MEX = 148;

    /// <summary> люпьюккнбш нярпнбю </summary>
    public const int MHL = 147;

    /// <summary> пеяосакхйю люйеднмхъ[3] </summary>
    public const int MKD = 138;

    /// <summary> люкх </summary>
    public const int MLI = 141;

    /// <summary> люкэрю </summary>
    public const int MLT = 144;

    /// <summary> лэъмлю </summary>
    public const int MMR = 155;

    /// <summary> вепмнцнпхъ </summary>
    public const int MNE = 250;

    /// <summary> лнмцнкхъ </summary>
    public const int MNG = 153;

    /// <summary> яебепмше люпхюмяйхе нярпнбю </summary>
    public const int MNP = 199;

    /// <summary> лнгюлахй </summary>
    public const int MOZ = 150;

    /// <summary> любпхрюмхъ </summary>
    public const int MRT = 134;

    /// <summary> лнмряеппюр </summary>
    public const int MSR = 154;

    /// <summary> люпрхмхйю </summary>
    public const int MTQ = 146;

    /// <summary> любпхйхи </summary>
    public const int MUS = 133;

    /// <summary> люкюбх </summary>
    public const int MWI = 139;

    /// <summary> люкюигхъ </summary>
    public const int MYS = 140;

    /// <summary> люинррю </summary>
    public const int MYT = 136;

    /// <summary> мюлхахъ </summary>
    public const int NAM = 156;

    /// <summary> мнбюъ йюкеднмхъ </summary>
    public const int NCL = 165;

    /// <summary> мхцеп </summary>
    public const int NER = 159;

    /// <summary> нярпнб мнптнкй </summary>
    public const int NFK = 174;

    /// <summary> мхцепхъ </summary>
    public const int NGA = 160;

    /// <summary> мхйюпюцсю </summary>
    public const int NIC = 162;

    /// <summary> мхсщ </summary>
    public const int NIU = 163;

    /// <summary> мхдепкюмдш </summary>
    public const int NLD = 161;

    /// <summary> мнпбецхъ </summary>
    public const int NOR = 166;

    /// <summary> меоюк </summary>
    public const int NPL = 158;

    /// <summary> мюспс </summary>
    public const int NRU = 157;

    /// <summary> мнбюъ гекюмдхъ </summary>
    public const int NZL = 164;

    /// <summary> нлюм </summary>
    public const int OMN = 168;

    /// <summary> чфмюъ няерхъ </summary>
    public const int OST = 266;

    /// <summary> оюйхярюм </summary>
    public const int PAK = 177;

    /// <summary> оюмюлю </summary>
    public const int PAN = 180;

    /// <summary> охрйепм </summary>
    public const int PCN = 185;

    /// <summary> оепс </summary>
    public const int PER = 184;

    /// <summary> тхкхоохмш </summary>
    public const int PHL = 240;

    /// <summary> оюкюс </summary>
    public const int PLW = 178;

    /// <summary> оюосю-мнбюъ цбхмеъ </summary>
    public const int PNG = 182;

    /// <summary> онкэью </summary>
    public const int POL = 186;

    /// <summary> осщпрн-пхйн </summary>
    public const int PRI = 188;

    /// <summary> йнпеъ, мюпндмн-делнйпюрхвеяйюъ пеяосакхйю </summary>
    public const int PRK = 117;

    /// <summary> онпрсцюкхъ </summary>
    public const int PRT = 187;

    /// <summary> оюпюцбюи </summary>
    public const int PRY = 183;

    /// <summary> оюкеярхмяйюъ реппхрнпхъ, нййсохпнбюммюъ </summary>
    public const int PSE = 179;

    /// <summary> тпюмжсгяйюъ онкхмегхъ </summary>
    public const int PYF = 245;

    /// <summary> йюрюп </summary>
    public const int QAT = 106;

    /// <summary> печмэнм </summary>
    public const int REU = 189;

    /// <summary> пслшмхъ </summary>
    public const int ROU = 192;

    /// <summary> пняяхъ </summary>
    public const int RUS = 190;

    /// <summary> псюмдю </summary>
    public const int RWA = 191;

    /// <summary> яюсднбяйюъ юпюбхъ </summary>
    public const int SAU = 196;

    /// <summary> ясдюм </summary>
    public const int SDN = 218;

    /// <summary> яемецюк </summary>
    public const int SEN = 204;

    /// <summary> яхмцюосп </summary>
    public const int SGP = 210;

    /// <summary> чфмюъ дфнпдфхъ х чфмше яюмдбхвебш нярпнбю </summary>
    public const int SGS = 265;

    /// <summary> ябърюъ екемю, нярпнб бнгмеяемхъ, рпхярюм-дю-йсмэъ </summary>
    public const int SHN = 198;

    /// <summary> ьохжаепцем х ъм люием </summary>
    public const int SJM = 255;

    /// <summary> янкнлнмнбш нярпнбю </summary>
    public const int SLB = 216;

    /// <summary> яэеппю-кенме </summary>
    public const int SLE = 220;

    /// <summary> щкэ-яюкэбюднп </summary>
    public const int SLV = 260;

    /// <summary> яюм-люпхмн </summary>
    public const int SMR = 194;

    /// <summary> янлюкх </summary>
    public const int SOM = 217;

    /// <summary> яем-оэеп х лхйекнм </summary>
    public const int SPM = 208;

    /// <summary> яепахъ </summary>
    public const int SRB = 209;

    /// <summary> чфмши ясдюм </summary>
    public const int SSD = 267;

    /// <summary> яюм-рнле х опхмяхох </summary>
    public const int STP = 195;

    /// <summary> яспхмюл </summary>
    public const int SUR = 219;

    /// <summary> якнбюйхъ </summary>
    public const int SVK = 212;

    /// <summary> якнбемхъ </summary>
    public const int SVN = 213;

    /// <summary> ьбежхъ </summary>
    public const int SWE = 254;

    /// <summary> ябюгхкемд </summary>
    public const int SWZ = 197;

    /// <summary> яем-люпрем (МХДЕПКЮМДЯЙЮЪ ВЮЯРЭ) </summary>
    public const int SXM = 203;

    /// <summary> яеиьекш </summary>
    public const int SYC = 200;

    /// <summary> яхпхияйюъ юпюаяйюъ пеяосакхйю </summary>
    public const int SYR = 211;

    /// <summary> нярпнбю репйя х йюийня </summary>
    public const int TCA = 171;

    /// <summary> вюд </summary>
    public const int TCD = 249;

    /// <summary> рнцн </summary>
    public const int TGO = 225;

    /// <summary> рюхкюмд </summary>
    public const int THA = 222;

    /// <summary> рюдфхйхярюм </summary>
    public const int TJK = 221;

    /// <summary> рнйекюс </summary>
    public const int TKL = 226;

    /// <summary> рспйлемхъ </summary>
    public const int TKM = 231;

    /// <summary> рхлнп-кеяре </summary>
    public const int TLS = 60;

    /// <summary> рнмцю </summary>
    public const int TON = 227;

    /// <summary> рпхмхдюд х рнаюцн </summary>
    public const int TTO = 228;

    /// <summary> рсмхя </summary>
    public const int TUN = 230;

    /// <summary> рспжхъ </summary>
    public const int TUR = 232;

    /// <summary> рсбюкс </summary>
    public const int TUV = 229;

    /// <summary> рюибюмэ (йхрюи) </summary>
    public const int TWN = 223;

    /// <summary> рюмгюмхъ, назедхмеммюъ пеяосакхйю </summary>
    public const int TZA = 224;

    /// <summary> сцюмдю </summary>
    public const int UGA = 233;

    /// <summary> сйпюхмю </summary>
    public const int UKR = 235;

    /// <summary> люкше рхуннйеюмяйхе нрдюкеммше нярпнбю янедхмеммшу ьрюрнб </summary>
    public const int UMI = 142;

    /// <summary> спсцбюи </summary>
    public const int URY = 237;

    /// <summary> янедхмеммше ьрюрш </summary>
    public const int USA = 215;

    /// <summary> сгаейхярюм </summary>
    public const int UZB = 234;

    /// <summary> оюояйхи опеярнк (цнясдюпярбн ≈ цнпнд бюрхйюм) </summary>
    public const int VAT = 181;

    /// <summary> яемр-бхмяемр х цпемюдхмш </summary>
    public const int VCT = 205;

    /// <summary> бемеясщкю анкхбюпхюмяйюъ пеяосакхйю </summary>
    public const int VEN = 57;

    /// <summary> бхпцхмяйхе нярпнбю, апхрюмяйхе </summary>
    public const int VGB = 58;

    /// <summary> бхпцхмяйхе нярпнбю, яью </summary>
    public const int VIR = 59;

    /// <summary> бэермюл </summary>
    public const int VNM = 61;

    /// <summary> бюмсюрс </summary>
    public const int VUT = 55;

    /// <summary> снккхя х тсрсмю </summary>
    public const int WLF = 236;

    /// <summary> яюлню </summary>
    public const int WSM = 193;

    /// <summary> иелем </summary>
    public const int YEM = 100;

    /// <summary> чфмюъ ютпхйю </summary>
    public const int ZAF = 264;

    /// <summary> гюлахъ </summary>
    public const int ZMB = 87;

    /// <summary> гхлаюабе </summary>
    public const int ZWE = 89;

    #endregion
  }
}