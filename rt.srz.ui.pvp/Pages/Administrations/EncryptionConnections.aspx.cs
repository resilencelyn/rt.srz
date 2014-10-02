using rt.srz.business.algorithms;
using rt.srz.model.HL7.pfr;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  public partial class EncryptionConnections : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Encryption(bool EncryptoValue)
    {
      Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
      ConfigurationSection sec = config.GetSection("connectionStrings");
      if (EncryptoValue == true)
      {
        sec.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
        sec.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
      }
      else
      {
        sec.SectionInformation.UnprotectSection();
      }
      config.Save();
    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
      ZlList obj = new ZlList();
      XmlSerializer serializer = new XmlSerializer(typeof(ZlList));
      using (FileStream fs = new FileStream(@"c:\Projects\PolicyDistributionPoint\ExchangeTask\063131RS01002.xml", FileMode.Open, FileAccess.Read))
      {
        obj = (ZlList)serializer.Deserialize(fs);
      }
      XmlSerializationHelper.Deserialize(@"c:\Projects\PolicyDistributionPoint\ExchangeTask\063131RS01002.xml", obj);
//      Encryption(true);
    }

    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
      Encryption(false);
    }
  }
}