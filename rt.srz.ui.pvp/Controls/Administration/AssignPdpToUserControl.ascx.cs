using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;

namespace rt.srz.ui.pvp.Controls.Administration
{
  public partial class AssignPdpToUserControl : System.Web.UI.UserControl
  {
    private ISecurityService _securityService;
    private ISmoService _smoService;
    private Guid _userId;

    protected void Page_Init(object sender, EventArgs e)
    {
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
      _smoService = ObjectFactory.GetInstance<ISmoService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["userId"] == null)
      {
        _userId = Guid.Empty;
      }
      else
      {
        _userId = Guid.Parse(Request.QueryString["userId"]);
      }
      if (!IsPostBack)
      {
        string userName = string.Empty;
        lbTitle.Text = string.Format("Назначение пункта выдачи для пользователя: {0}", Request.QueryString["userName"]);

        User currentUser = _securityService.GetCurrentUser();
        if (_securityService.IsUserHasAdminPermissions(currentUser))
        {
          AssignDataSourcesForAdminMode(currentUser);
        }
        else if (currentUser.PointDistributionPolicy != null && _securityService.IsUserAdminSmo(currentUser.Id))
        {
          AssignDataSourcesForOwnSmo(currentUser);
        }
        else if (currentUser.PointDistributionPolicy != null && _securityService.IsUserAdminTF(currentUser.Id))
        {
          AssignDataSourcesForOwnRegion(currentUser);
        }

        //случай добавления нового пользователя
        if (_userId == Guid.Empty)
        {
          //устанавливаем значения комбобоксов по умолчанию исходя из текущего пользователя, если для редактируемого пользователя не назначен pdp
          SetComboboValues(currentUser);
          //соответствует элементу не выбран
          dlPdp.SelectedIndex = 0;
          return;
        }

        //открыли на редактирование
        if (_userId != Guid.Empty)
        {
          User user = _securityService.GetUser(_userId);

          bool allowAssignPdp = true;
          //администратор смо не может назначать пункт выдачи для администратора территорального фонда
          if (!_securityService.IsUserHasAdminPermissions(currentUser) &&
            !_securityService.IsUserAdminTF(currentUser.Id))
          {
            allowAssignPdp = !_securityService.IsUserAdminTF(_userId);
          }
          dlTFoms.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;
          dlSmo.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;
          dlPdp.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;


          if (user.PointDistributionPolicy != null)
          {
            SetComboboValues(user);
          }
          else
          {
            //устанавливаем значения комбобоксов по умолчанию исходя из текущего пользователя, если для редактируемого пользователя не назначен pdp
            SetComboboValues(currentUser);
            //соответствует элементу не выбран
            dlPdp.SelectedIndex = 0;
          }
        }
      }
    }

    private void SetComboboValues(User user)
    {
      if (user.PointDistributionPolicy == null)
      {
        return;
      }
      dlTFoms.SelectedValue = user.PointDistributionPolicy.Parent.Parent.Id.ToString();
      dlSmo.SelectedValue = user.PointDistributionPolicy.Parent.Id.ToString();
      dlPdp.SelectedValue = user.PointDistributionPolicy.Id.ToString();
    }

    public void AssignDataSourcesForAdminMode(User currentUser)
    {
      //все территориальные фонды
      IList<Organisation> foms = _smoService.GetAllTfoms();
      dlTFoms.DataSource = foms;
      dlTFoms.DataBind();

      User user = _securityService.GetUser(_userId);
      if (user != null && user.PointDistributionPolicy != null)
      {
        //страховые медицинские организации принадлежащие территорильному фонду пользователя
        dlSmo.DataSource = _smoService.GetSmosByTfom(user.PointDistributionPolicy.Parent.Parent.Id);
        dlSmo.DataBind();

        //пункты выдачи страховой медицинской организации пользователя
        dlPdp.DataSource = GetPDPsBySmo(user.PointDistributionPolicy.Parent.Id);
        dlPdp.DataBind();
      }
      else
      {
        if (foms.Count > 0)
        {
          Guid fomId;
          Guid smoId;
          if (currentUser.PointDistributionPolicy != null)
          {
            fomId = currentUser.PointDistributionPolicy.Parent.Parent.Id;
          }
          else
          {
            fomId = foms.First().Id;
          }
          //все страховые медицинские организации по первому территориальномну фонду в выпадающем списке или по фонду текущего пользователя
          IList<Organisation> smos = _smoService.GetSmosByTfom(fomId);
          dlSmo.DataSource = smos;
          dlSmo.DataBind();
          if (smos.Count > 0)
          {
            if (currentUser.PointDistributionPolicy != null)
            {
              smoId = currentUser.PointDistributionPolicy.Parent.Id;
            }
            else
            {
              smoId = smos.First().Id;
            }
            //все пункты выдачи по первой страховой медицинской организации
            dlPdp.DataSource = GetPDPsBySmo(smoId);
            dlPdp.DataBind();
          }
        }
      }
    }

    public void AssignDataSourcesForOwnSmo(User currentUser)
    {
      var smo = _smoService.GetSmo(currentUser.PointDistributionPolicy.Parent.Id);

      //страховая медицинская огранизация текущего пользователя
      dlSmo.DataSource = new List<Organisation>() { smo };
      dlSmo.DataBind();

      //территориальный фонд организации текущего пользователя
      dlTFoms.DataSource = new List<Organisation>() { smo.Parent };
      dlTFoms.DataBind();

      //пункты выдачи страховой медицинской организации текущего пользователя
      dlPdp.DataSource = GetPDPsBySmo(smo.Id);
      dlPdp.DataBind();
    }

    public void AssignDataSourcesForOwnRegion(User currentUser)
    {
      var smo = _smoService.GetSmo(currentUser.PointDistributionPolicy.Parent.Id);

      //территориальный фонд страховой медицинской организации текущего пользователя
      dlTFoms.DataSource = new List<Organisation>() { smo.Parent };
      dlTFoms.DataBind();

      //страховые мед организации принадлежащие фонду текущего пользователя
      IList<Organisation> smos = _smoService.GetSmosByTfom(smo.Parent.Id);
      dlSmo.DataSource = smos;
      dlSmo.DataBind();

      User user = _securityService.GetUser(_userId);
      if (user != null && user.PointDistributionPolicy != null)
      {
        //пункты выдачи огрганизации пользователя
        dlPdp.DataSource = GetPDPsBySmo(user.PointDistributionPolicy.Parent.Id);
        dlPdp.DataBind();
      }
      else
      {
        //пункты выдачи для первой в выпадающем списке страховой мед организации
        if (smos.Count > 0)
        {
          Guid smoId;
          if (currentUser.PointDistributionPolicy != null)
          {
            smoId = currentUser.PointDistributionPolicy.Parent.Id;
          }
          else
          {
            smoId = smos.First().Id;
          }
          dlPdp.DataSource = GetPDPsBySmo(smoId);
          dlPdp.DataBind();
        }
      }
    }

    public void SaveChanges()
    {
      SaveChanges(Guid.Empty);
    }

    public void SaveChanges(Guid newUserId)
    {
      Guid? pdpId = (string.IsNullOrEmpty(dlPdp.SelectedValue) || dlPdp.SelectedValue == "-1") ? null : (Guid?)Guid.Parse(dlPdp.SelectedValue);
      _securityService.AssignPdpToUser(newUserId != Guid.Empty ? newUserId : _userId, pdpId);
    }

    protected void dlTFoms_SelectedIndexChanged(object sender, EventArgs e)
    {
      //получаем страховые медицинские организации по территорильному фонду
      if (string.IsNullOrEmpty(dlTFoms.SelectedValue))
      {
        return;
      }
      dlSmo.DataSource = _smoService.GetSmosByTfom(Guid.Parse(dlTFoms.SelectedValue));
      dlSmo.DataBind();
      dlSmo_SelectedIndexChanged(null, null);
    }

    protected void dlSmo_SelectedIndexChanged(object sender, EventArgs e)
    {
      //получаем пункты выдачи по страховой медицинской организации
      if (string.IsNullOrEmpty(dlSmo.SelectedValue))
      {
        return;
      }
      dlPdp.DataSource = GetPDPsBySmo(Guid.Parse(dlSmo.SelectedValue));
      dlPdp.DataBind();
    }

    /// <summary>
    /// Получает список плюс пустой элемент для удобства выбора и сброса значения в выпадающем списке
    /// </summary>
    /// <param name="smoId"></param>
    /// <returns></returns>
    private IList<Organisation> GetPDPsBySmo(Guid smoId)
    {
      IList<Organisation> result = _smoService.GetPDPsBySmo(smoId);
      var pdp = new Organisation();
      pdp.ShortName = "Не выбран";
      pdp.Id = Guid.Empty;
      result.Insert(0, pdp);
      return result;
    }

    protected void rfPoint_ServerValidate(object source, ServerValidateEventArgs args)
    {
      args.IsValid = Guid.Parse(dlPdp.SelectedValue) != Guid.Empty;
    }

  }
}