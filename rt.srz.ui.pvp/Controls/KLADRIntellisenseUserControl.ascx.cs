using System;
using System.Configuration;
using System.Web;
using System.Text;
using StructureMap;
using rt.srz.model.srz;
using rt.srz.model.interfaces.service;

namespace rt.srz.ui.pvp.Controls
{
  public partial class KLADRIntellisenseUserControl : System.Web.UI.UserControl, IKLADRUserControl
  {
    protected string uniqueKey;
    private IKladrService _kladrService;
    private ISecurityService _securityService;

    public Guid SelectedKLADRId
    {
      get
      {
        if (!string.IsNullOrEmpty(hfKLADRHierarchy.Value))
        {
          string[] hierarchy = hfKLADRHierarchy.Value.Split(new char[] { ';' });
          if (hierarchy.Length > 0)
            return new Guid(hierarchy[hierarchy.Length - 1]);
        }
        return Guid.Empty;
      }
      set
      {
        if (value != Guid.Empty)
        {
          //Восстановление иерархии
          IKladrService service = ObjectFactory.GetInstance<IKladrService>();

          StringBuilder valueBuilder = new StringBuilder();
          StringBuilder hierarchyBuilder = new StringBuilder();
          Kladr kladr = service.GetKLADR(value);
          do
          {
            valueBuilder.Insert(0, string.Format("," + kladr.Name + " " + kladr.Socr + "."));
            hierarchyBuilder.Insert(0, string.Format(";" + kladr.Id));
            kladr = kladr.KLADRPARENT;
          }
          while (kladr != null);

          if (valueBuilder.Length > 0)
            valueBuilder.Remove(0, 1);
          valueBuilder.Append(",");

          if (hierarchyBuilder.Length > 0)
            hierarchyBuilder.Remove(0, 1);

          tbKLADRIntellisense.Text = valueBuilder.ToString();
          hfKLADRHierarchy.Value = hierarchyBuilder.ToString();
        }
      }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      _kladrService = ObjectFactory.GetInstance<IKladrService>();
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
      if (!IsPostBack)
      {
        SetDefaultSubject();
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      //Решение вопроса размещения нескольких компонентов на одной форме
      uniqueKey = Guid.NewGuid().ToString("N");
      aceKLADRIntellisense.OnClientItemSelected = "KLADR_itemSelected_" + uniqueKey;
      aceKLADRIntellisense.OnClientPopulating = "KLADR_ListPopulating_" + uniqueKey;
      aceKLADRIntellisense.OnClientPopulated = "KLADR_ListPopulated_" + uniqueKey;
      aceKLADRIntellisense.OnClientHidden = "KLADR_ListPopulated_" + uniqueKey;
      aceKLADRIntellisense.OnClientShowing = "KLADR_ListShowing_" + uniqueKey;
      tbKLADRIntellisense.Attributes["onkeypress"] = "KLADR_KeyPress_" + uniqueKey + "();";
      tbKLADRIntellisense.Attributes["onkeydown"] = "KLADR_KeyDown_" + uniqueKey + "();";
      tbKLADRIntellisense.Attributes["onfocus"] = "KLADR_DisableSelection_" + uniqueKey + "();";
      tbKLADRIntellisense.Attributes["onmouseup"] = "KLADR_MouseUp_" + uniqueKey + "();";
      tbKLADRIntellisense.Attributes["onfocus"] = "KLADR_OnFocus_" + uniqueKey + "();";

      aceKLADRIntellisense.MinimumPrefixLength = int.Parse(ConfigurationManager.AppSettings["MinimumPrefixLengthForAdress"]);
    }

    private void SetPostcode()
    {
      if (SelectedKLADRId == null)
        return;

      tbPostcode.Text = string.Empty;
      Kladr kladr = _kladrService.GetKLADR(SelectedKLADRId);
      if (kladr != null && kladr.Index != null)
        tbPostcode.Text = kladr.Index.ToString();
    }

    public void SetFocusFirstControl()
    {
      tbKLADRIntellisense.Focus();
    }
    
    public void Enable(bool bEnable)
    {
      tbPostcode.Enabled = bEnable;
      tbKLADRIntellisense.Enabled = bEnable;
      tbHouse.Enabled = bEnable;
      tbHousing.Enabled = bEnable;
      tbRoom.Enabled = bEnable;
    }

    public void SetDefaultSubject()
    {
      //Получение текущего региона для текущего пользователя
      User currentUser = _securityService.GetCurrentUser();
      if (currentUser != null && currentUser.PointDistributionPolicy != null && currentUser.PointDistributionPolicy.Parent != null && currentUser.PointDistributionPolicy.Parent.Parent != null)
      {
        var tfom = currentUser.PointDistributionPolicy.Parent.Parent;
        Kladr kladr = _kladrService.GetFirstLevelByTfoms(tfom);
        if (kladr != null)
        {
          //Установка региона по умолчанию
          SelectedKLADRId = kladr.Id;

          //Инициализация AutoComplete компонента
          aceKLADRIntellisense.ContextKey = SelectedKLADRId.ToString();

          //Установка идекса по умолчанию
          SetPostcode();
        }
      }
    }
  }
}