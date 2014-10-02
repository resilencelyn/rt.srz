
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace rt.atl.model.atl
{
  using rt.core.model;

  [DataContract] 
   public partial class person : BusinessBase<int>
   {
      public person() { }

	  [XmlElement(Order = 1)]
      [DataMember(Order = 1)]
      public override int Id
      {
        get
        {
          return base.Id;
        }
        set
        {
          base.Id = value;
        }
      } 


        [XmlElement(Order =  2)]
        [DataMember(Order =  2)]
        public virtual string Fam { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Im { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual string Ot { get; set;}
        
        [XmlElement(Order =  5)]
        [DataMember(Order =  5)]
        public virtual int? W { get; set;}
        
        [XmlElement(Order =  6)]
        [DataMember(Order =  6)]
        public virtual System.DateTime? Dr { get; set;}
        
        [XmlElement(Order =  7)]
        [DataMember(Order =  7)]
        public virtual bool? Dra { get; set;}
        
        [XmlElement(Order =  8)]
        [DataMember(Order =  8)]
        public virtual string Mr { get; set;}
        
        [XmlElement(Order =  9)]
        [DataMember(Order =  9)]
        public virtual System.DateTime? Ds { get; set;}
        
        [XmlElement(Order =  10)]
        [DataMember(Order =  10)]
        public virtual string Ss { get; set;}
        
        [XmlElement(Order =  11)]
        [DataMember(Order =  11)]
        public virtual string Doctp { get; set;}
        
        [XmlElement(Order =  12)]
        [DataMember(Order =  12)]
        public virtual string Docs { get; set;}
        
        [XmlElement(Order =  13)]
        [DataMember(Order =  13)]
        public virtual string Docn { get; set;}
        
        [XmlElement(Order =  14)]
        [DataMember(Order =  14)]
        public virtual System.DateTime? Docdt { get; set;}
        
        [XmlElement(Order =  15)]
        [DataMember(Order =  15)]
        public virtual string Docorg { get; set;}
        
        [XmlElement(Order =  16)]
        [DataMember(Order =  16)]
        public virtual string Cn { get; set;}
        
        [XmlElement(Order =  17)]
        [DataMember(Order =  17)]
        public virtual string Subj { get; set;}
        
        [XmlElement(Order =  18)]
        [DataMember(Order =  18)]
        public virtual string Rn { get; set;}
        
        [XmlElement(Order =  19)]
        [DataMember(Order =  19)]
        public virtual string Indx { get; set;}
        
        [XmlElement(Order =  20)]
        [DataMember(Order =  20)]
        public virtual string Rnname { get; set;}
        
        [XmlElement(Order =  21)]
        [DataMember(Order =  21)]
        public virtual string City { get; set;}
        
        [XmlElement(Order =  22)]
        [DataMember(Order =  22)]
        public virtual string Np { get; set;}
        
        [XmlElement(Order =  23)]
        [DataMember(Order =  23)]
        public virtual string Ul { get; set;}
        
        [XmlElement(Order =  24)]
        [DataMember(Order =  24)]
        public virtual string Dom { get; set;}
        
        [XmlElement(Order =  25)]
        [DataMember(Order =  25)]
        public virtual string Kor { get; set;}
        
        [XmlElement(Order =  26)]
        [DataMember(Order =  26)]
        public virtual string Kv { get; set;}
        
        [XmlElement(Order =  27)]
        [DataMember(Order =  27)]
        public virtual System.DateTime? Dmj { get; set;}
        
        [XmlElement(Order =  28)]
        [DataMember(Order =  28)]
        public virtual bool? Bomj { get; set;}
        
        [XmlElement(Order =  29)]
        [DataMember(Order =  29)]
        public virtual string Psubj { get; set;}
        
        [XmlElement(Order =  30)]
        [DataMember(Order =  30)]
        public virtual string Prn { get; set;}
        
        [XmlElement(Order =  31)]
        [DataMember(Order =  31)]
        public virtual string Pindx { get; set;}
        
        [XmlElement(Order =  32)]
        [DataMember(Order =  32)]
        public virtual string Prnname { get; set;}
        
        [XmlElement(Order =  33)]
        [DataMember(Order =  33)]
        public virtual string Pcity { get; set;}
        
        [XmlElement(Order =  34)]
        [DataMember(Order =  34)]
        public virtual string Pnp { get; set;}
        
        [XmlElement(Order =  35)]
        [DataMember(Order =  35)]
        public virtual string Pul { get; set;}
        
        [XmlElement(Order =  36)]
        [DataMember(Order =  36)]
        public virtual string Pdom { get; set;}
        
        [XmlElement(Order =  37)]
        [DataMember(Order =  37)]
        public virtual string Pkor { get; set;}
        
        [XmlElement(Order =  38)]
        [DataMember(Order =  38)]
        public virtual string Pkv { get; set;}
        
        [XmlElement(Order =  39)]
        [DataMember(Order =  39)]
        public virtual System.DateTime? Pdmj { get; set;}
        
        [XmlElement(Order =  40)]
        [DataMember(Order =  40)]
        public virtual string Rdoctp { get; set;}
        
        [XmlElement(Order =  41)]
        [DataMember(Order =  41)]
        public virtual string Rdocs { get; set;}
        
        [XmlElement(Order =  42)]
        [DataMember(Order =  42)]
        public virtual string Rdocn { get; set;}
        
        [XmlElement(Order =  43)]
        [DataMember(Order =  43)]
        public virtual System.DateTime? Rdocdt { get; set;}
        
        [XmlElement(Order =  44)]
        [DataMember(Order =  44)]
        public virtual string Rdocorg { get; set;}
        
        [XmlElement(Order =  45)]
        [DataMember(Order =  45)]
        public virtual System.DateTime? Rdocend { get; set;}
        
        [XmlElement(Order =  46)]
        [DataMember(Order =  46)]
        public virtual string Email { get; set;}
        
        [XmlElement(Order =  47)]
        [DataMember(Order =  47)]
        public virtual string Q { get; set;}
        
        [XmlElement(Order =  48)]
        [DataMember(Order =  48)]
        public virtual string Prz { get; set;}
        
        [XmlElement(Order =  49)]
        [DataMember(Order =  49)]
        public virtual string Enp { get; set;}
        
        [XmlElement(Order =  50)]
        [DataMember(Order =  50)]
        public virtual int? Opdoc { get; set;}
        
        [XmlElement(Order =  51)]
        [DataMember(Order =  51)]
        public virtual string Okato { get; set;}
        
        [XmlElement(Order =  52)]
        [DataMember(Order =  52)]
        public virtual System.DateTime? Dhpol { get; set;}
        
        [XmlElement(Order =  53)]
        [DataMember(Order =  53)]
        public virtual System.DateTime? Dbeg { get; set;}
        
        [XmlElement(Order =  54)]
        [DataMember(Order =  54)]
        public virtual System.DateTime? Dend { get; set;}
        
        [XmlElement(Order =  55)]
        [DataMember(Order =  55)]
        public virtual bool? Petition { get; set;}
        
        [XmlElement(Order =  56)]
        [DataMember(Order =  56)]
        public virtual string Lpu { get; set;}
        
        [XmlElement(Order =  57)]
        [DataMember(Order =  57)]
        public virtual string Lpuwk { get; set;}
        
        [XmlElement(Order =  58)]
        [DataMember(Order =  58)]
        public virtual string Lpust { get; set;}
        
        [XmlElement(Order =  59)]
        [DataMember(Order =  59)]
        public virtual int? Lpuuch { get; set;}
        
        [XmlElement(Order =  60)]
        [DataMember(Order =  60)]
        public virtual string Sp { get; set;}
        
        [XmlElement(Order =  61)]
        [DataMember(Order =  61)]
        public virtual string Kt { get; set;}
        
        [XmlElement(Order =  62)]
        [DataMember(Order =  62)]
        public virtual string Okved { get; set;}
        
        [XmlElement(Order =  63)]
        [DataMember(Order =  63)]
        public virtual string Kladrs { get; set;}
        
        [XmlElement(Order =  64)]
        [DataMember(Order =  64)]
        public virtual string Zfam { get; set;}
        
        [XmlElement(Order =  65)]
        [DataMember(Order =  65)]
        public virtual string Zim { get; set;}
        
        [XmlElement(Order =  66)]
        [DataMember(Order =  66)]
        public virtual string Zot { get; set;}
        
        [XmlElement(Order =  67)]
        [DataMember(Order =  67)]
        public virtual int? Zt { get; set;}
        
        [XmlElement(Order =  68)]
        [DataMember(Order =  68)]
        public virtual System.DateTime? Zdr { get; set;}
        
        [XmlElement(Order =  69)]
        [DataMember(Order =  69)]
        public virtual string Zmr { get; set;}
        
        [XmlElement(Order =  70)]
        [DataMember(Order =  70)]
        public virtual string Zdoctp { get; set;}
        
        [XmlElement(Order =  71)]
        [DataMember(Order =  71)]
        public virtual string Zdocs { get; set;}
        
        [XmlElement(Order =  72)]
        [DataMember(Order =  72)]
        public virtual string Zdocn { get; set;}
        
        [XmlElement(Order =  73)]
        [DataMember(Order =  73)]
        public virtual System.DateTime? Zdocdt { get; set;}
        
        [XmlElement(Order =  74)]
        [DataMember(Order =  74)]
        public virtual string Zdocorg { get; set;}
        
        [XmlElement(Order =  75)]
        [DataMember(Order =  75)]
        public virtual string Zaddr { get; set;}
        
        [XmlElement(Order =  76)]
        [DataMember(Order =  76)]
        public virtual int? Pid111 { get; set;}
        
        [XmlElement(Order =  77)]
        [DataMember(Order =  77)]
        public virtual System.DateTime? Dedit { get; set;}
        
        [XmlElement(Order =  78)]
        [DataMember(Order =  78)]
        public virtual System.DateTime? Derp { get; set;}
        
        [XmlElement(Order =  79)]
        [DataMember(Order =  79)]
        public virtual bool? Unemp { get; set;}
        
        [XmlElement(Order =  80)]
        [DataMember(Order =  80)]
        public virtual string Extid { get; set;}
        
        [XmlElement(Order =  81)]
        [DataMember(Order =  81)]
        public virtual string Phone { get; set;}
        
        [XmlElement(Order =  82)]
        [DataMember(Order =  82)]
        public virtual string Zphone { get; set;}
        
        [XmlElement(Order =  83)]
        [DataMember(Order =  83)]
        public virtual string Spol { get; set;}
        
        [XmlElement(Order =  84)]
        [DataMember(Order =  84)]
        public virtual string Npol { get; set;}
        
        [XmlElement(Order =  85)]
        [DataMember(Order =  85)]
        public virtual string Qogrn { get; set;}
        
        [XmlElement(Order =  86)]
        [DataMember(Order =  86)]
        public virtual System.DateTime? Dstop { get; set;}
        
        [XmlElement(Order =  87)]
        [DataMember(Order =  87)]
        public virtual System.DateTime? Dviz { get; set;}
        
        [XmlElement(Order =  88)]
        [DataMember(Order =  88)]
        public virtual int? Meth { get; set;}
        
        [XmlElement(Order =  89)]
        [DataMember(Order =  89)]
        public virtual string Zenp { get; set;}
        
        [XmlElement(Order =  90)]
        [DataMember(Order =  90)]
        public virtual System.DateTime? Denp { get; set;}
        
        [XmlElement(Order =  91)]
        [DataMember(Order =  91)]
        public virtual bool? Erp { get; set;}
        
        [XmlElement(Order =  92)]
        [DataMember(Order =  92)]
        public virtual string Zerr { get; set;}
        
        [XmlElement(Order =  93)]
        [DataMember(Order =  93)]
        public virtual int? Opsmo { get; set;}
        
        [XmlElement(Order =  94)]
        [DataMember(Order =  94)]
        public virtual int? Oppol { get; set;}
        
        [XmlElement(Order =  95)]
        [DataMember(Order =  95)]
        public virtual int? Polpr { get; set;}
        
        [XmlElement(Order =  96)]
        [DataMember(Order =  96)]
        public virtual int? Polvid { get; set;}
        
        [XmlElement(Order =  97)]
        [DataMember(Order =  97)]
        public virtual int? Polisid { get; set;}
        
        [XmlElement(Order =  98)]
        [DataMember(Order =  98)]
        public virtual string Pfrss { get; set;}
        
        [XmlElement(Order =  99)]
        [DataMember(Order =  99)]
        public virtual int? Rstop { get; set;}
        
        [XmlElement(Order =  100)]
        [DataMember(Order =  100)]
        public virtual System.DateTime? Dakt { get; set;}
        
        [XmlElement(Order =  101)]
        [DataMember(Order =  101)]
        public virtual string Nakt { get; set;}
        
        [XmlElement(Order =  102)]
        [DataMember(Order =  102)]
        public virtual string Takt { get; set;}
        
        [XmlElement(Order =  103)]
        [DataMember(Order =  103)]
        public virtual System.DateTime? Dzp1 { get; set;}
        
        [XmlElement(Order =  104)]
        [DataMember(Order =  104)]
        public virtual string Zp1repl { get; set;}
        
        [XmlElement(Order =  105)]
        [DataMember(Order =  105)]
        public virtual bool? Del { get; set;}
        
        [XmlElement(Order =  106)]
        [DataMember(Order =  106)]
        public virtual string Oldenp { get; set;}
        
        [XmlElement(Order =  107)]
        [DataMember(Order =  107)]
        public virtual int? Tenp { get; set;}
        
        [XmlElement(Order =  108)]
        [DataMember(Order =  108)]
        public virtual string Senp { get; set;}
        
        [XmlElement(Order =  109)]
        [DataMember(Order =  109)]
        public virtual string Photo { get; set;}
        
        [XmlElement(Order =  110)]
        [DataMember(Order =  110)]
        public virtual string Ptype { get; set;}
        
        [XmlElement(Order =  111)]
        [DataMember(Order =  111)]
        public virtual int? Drt { get; set;}
        
        [XmlElement(Order =  112)]
        [DataMember(Order =  112)]
        public virtual string Fiopr { get; set;}
        
        [XmlElement(Order =  113)]
        [DataMember(Order =  113)]
        public virtual string Contact { get; set;}
        
        [XmlElement(Order =  114)]
        [DataMember(Order =  114)]
        public virtual string Nord { get; set;}
        
        [XmlElement(Order =  115)]
        [DataMember(Order =  115)]
        public virtual System.DateTime? Dord { get; set;}
        
        [XmlElement(Order =  116)]
        [DataMember(Order =  116)]
        public virtual string Signat { get; set;}
        
        [XmlElement(Order =  117)]
        [DataMember(Order =  117)]
        public virtual System.DateTime? Dz { get; set;}
        
        [XmlElement(Order =  118)]
        [DataMember(Order =  118)]
        public virtual int? Lpuauto { get; set;}
        
        [XmlElement(Order =  119)]
        [DataMember(Order =  119)]
        public virtual System.DateTime? Lpudt { get; set;}
        
        [XmlElement(Order =  120)]
        [DataMember(Order =  120)]
        public virtual System.DateTime? Lpudx { get; set;}
        
        [XmlElement(Order =  121)]
        [DataMember(Order =  121)]
        public virtual string Kladrg { get; set;}
        
        [XmlElement(Order =  122)]
        [DataMember(Order =  122)]
        public virtual string Kladrp { get; set;}
        
        [XmlElement(Order =  123)]
        [DataMember(Order =  123)]
        public virtual int? Bid { get; set;}
        
        [XmlElement(Order =  124)]
        [DataMember(Order =  124)]
        public virtual int? Mid { get; set;}
        
        [XmlElement(Order =  125)]
        [DataMember(Order =  125)]
        public virtual bool? Other { get; set;}
        
        [XmlElement(Order =  126)]
        [DataMember(Order =  126)]
        public virtual string Fame { get; set;}
        
        [XmlElement(Order =  127)]
        [DataMember(Order =  127)]
        public virtual string Ime { get; set;}
        
        [XmlElement(Order =  128)]
        [DataMember(Order =  128)]
        public virtual string Ote { get; set;}
        
        [XmlElement(Order =  129)]
        [DataMember(Order =  129)]
        public virtual string Ssold { get; set;}
        
        [XmlElement(Order =  130)]
        [DataMember(Order =  130)]
        public virtual int? Force { get; set;}
        
        [XmlElement(Order =  131)]
        [DataMember(Order =  131)]
        public virtual bool? PzScan { get; set;}
        
        [XmlElement(Order =  132)]
        [DataMember(Order =  132)]
        public virtual string Dost { get; set;}
        
        [XmlElement(Order =  133)]
        [DataMember(Order =  133)]
        public virtual System.DateTime? Docend { get; set;}
        
        [XmlElement(Order =  134)]
        [DataMember(Order =  134)]
        public virtual string BirthOksm { get; set;}
        
        [XmlElement(Order =  135)]
        [DataMember(Order =  135)]
        public virtual int? Kateg { get; set;}
        
        [XmlElement(Order =  136)]
        [DataMember(Order =  136)]
        public virtual System.DateTime? DstopCs { get; set;}
        
        [XmlElement(Order =  137)]
        [DataMember(Order =  137)]
        public virtual int? Zid { get; set;}
        
        [XmlElement(Order =  138)]
        [DataMember(Order =  138)]
        public virtual int? Pid21 { get; set;}
        
        [XmlElement(Order =  139)]
        [DataMember(Order =  139)]
        public virtual int? Pid3 { get; set;}
        
        [XmlElement(Order =  140)]
        [DataMember(Order =  140)]
        public virtual int? Pid4 { get; set;}
        
        [XmlElement(Order =  141)]
        [DataMember(Order =  141)]
        public virtual int? Pid5 { get; set;}
        
        [XmlElement(Order =  142)]
        [DataMember(Order =  142)]
        public virtual int? Pid121 { get; set;}
        
        [XmlElement(Order =  143)]
        [DataMember(Order =  143)]
        public virtual int? Pid22 { get; set;}
        
        [XmlElement(Order =  144)]
        [DataMember(Order =  144)]
        public virtual int? Pid6 { get; set;}
        
        [XmlElement(Order =  145)]
        [DataMember(Order =  145)]
        public virtual int? Pid7 { get; set;}
        
        [XmlElement(Order =  146)]
        [DataMember(Order =  146)]
        public virtual int? Pid8 { get; set;}
        
        [XmlElement(Order =  147)]
        [DataMember(Order =  147)]
        public virtual int? Pid9 { get; set;}
        
        [XmlElement(Order =  148)]
        [DataMember(Order =  148)]
        public virtual int? Pid10 { get; set;}
        
        [XmlElement(Order =  149)]
        [DataMember(Order =  149)]
        public virtual int? Pid112 { get; set;}
        
        [XmlElement(Order =  150)]
        [DataMember(Order =  150)]
        public virtual int? Pid122 { get; set;}
        
        [XmlElement(Order =  151)]
        [DataMember(Order =  151)]
        public virtual int? Pid13 { get; set;}
        
        [XmlElement(Order =  152)]
        [DataMember(Order =  152)]
        public virtual int? Pid14 { get; set;}
        
        [XmlElement(Order =  153)]
        [DataMember(Order =  153)]
        public virtual int? Pid15 { get; set;}
        
        [XmlElement(Order =  154)]
        [DataMember(Order =  154)]
        public virtual int? Pid16 { get; set;}
        
        [XmlElement(Order =  155)]
        [DataMember(Order =  155)]
        public virtual int? Pid17 { get; set;}
        
        [XmlElement(Order =  156)]
        [DataMember(Order =  156)]
        public virtual int? Pid18 { get; set;}
        
        [XmlElement(Order =  157)]
        [DataMember(Order =  157)]
        public virtual int? Pid19 { get; set;}
        
        [XmlElement(Order =  158)]
        [DataMember(Order =  158)]
        public virtual int? Pid20 { get; set;}
        
        [XmlIgnore] 
        public virtual IList<Poli> Polis { get; set;}

        [XmlIgnore]
        public virtual IList<Uec> Uec { get; set; }
		
       }
}
