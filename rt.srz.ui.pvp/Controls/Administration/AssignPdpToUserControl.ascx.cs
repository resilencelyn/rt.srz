// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssignPdpToUserControl.ascx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Administration
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.core;
  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  using User = rt.core.model.core.User;

  /// <summary>
  /// The assign pdp to user control.
  /// </summary>
  public partial class AssignPdpToUserControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService securityService;

    /// <summary>
    /// The _smo service.
    /// </summary>
    private ISmoService smoService;

    /// <summary>
    /// The _user id.
    /// </summary>
    private Guid userId;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The assign data sources for admin mode.
    /// </summary>
    /// <param name="currentUser">
    /// The current user.
    /// </param>
    public void AssignDataSourcesForAdminMode(User currentUser)
    {
      // все территориальные фонды
      var foms = smoService.GetAllTfoms();
      dlTFoms.DataSource = foms;
      dlTFoms.DataBind();

      var user = securityService.GetUser(userId);
      if (user != null && user.PointDistributionPolicyId != null)
      {
        // страховые медицинские организации принадлежащие территорильному фонду пользователя
        dlSmo.DataSource = smoService.GetSmosByTfom(user.GetTf().Id);
        dlSmo.DataBind();

        // пункты выдачи страховой медицинской организации пользователя
        dlPdp.DataSource = GetPdPsBySmo(user.GetSmo().Id);
        dlPdp.DataBind();
      }
      else
      {
        if (foms.Count > 0)
        {
          var fomId = currentUser.PointDistributionPolicyId != null ? currentUser.GetTf().Id : foms.First().Id;

          // все страховые медицинские организации по первому территориальномну фонду в выпадающем списке или по фонду текущего пользователя
          var smos = smoService.GetSmosByTfom(fomId);
          dlSmo.DataSource = smos;
          dlSmo.DataBind();
          if (smos.Count > 0)
          {
            var smoId = currentUser.PointDistributionPolicyId != null ? currentUser.GetSmo().Id : smos.First().Id;

            // все пункты выдачи по первой страховой медицинской организации
            dlPdp.DataSource = GetPdPsBySmo(smoId);
            dlPdp.DataBind();
          }
        }
      }
    }

    /// <summary>
    /// The assign data sources for own region.
    /// </summary>
    /// <param name="currentUser">
    /// The current user.
    /// </param>
    public void AssignDataSourcesForOwnRegion(User currentUser)
    {
      var smo = smoService.GetSmo(currentUser.GetSmo().Id);

      // территориальный фонд страховой медицинской организации текущего пользователя
      dlTFoms.DataSource = new List<Organisation> { smo.Parent };
      dlTFoms.DataBind();

      // страховые мед организации принадлежащие фонду текущего пользователя
      var smos = smoService.GetSmosByTfom(smo.Parent.Id);
      dlSmo.DataSource = smos;
      dlSmo.DataBind();

      var user = securityService.GetUser(userId);
      if (user != null && user.PointDistributionPolicyId != null)
      {
        // пункты выдачи огрганизации пользователя
        dlPdp.DataSource = GetPdPsBySmo(user.GetSmo().Id);
        dlPdp.DataBind();
      }
      else
      {
        // пункты выдачи для первой в выпадающем списке страховой мед организации
        if (smos.Count > 0)
        {
          Guid smoId = currentUser.PointDistributionPolicyId != null ? currentUser.GetSmo().Id : smos.First().Id;

          dlPdp.DataSource = GetPdPsBySmo(smoId);
          dlPdp.DataBind();
        }
      }
    }

    /// <summary>
    /// The assign data sources for own smo.
    /// </summary>
    /// <param name="currentUser">
    /// The current user.
    /// </param>
    public void AssignDataSourcesForOwnSmo(User currentUser)
    {
      var smo = smoService.GetSmo(currentUser.GetSmo().Id);

      // страховая медицинская огранизация текущего пользователя
      dlSmo.DataSource = new List<Organisation> { smo };
      dlSmo.DataBind();

      // территориальный фонд организации текущего пользователя
      dlTFoms.DataSource = new List<Organisation> { smo.Parent };
      dlTFoms.DataBind();

      // пункты выдачи страховой медицинской организации текущего пользователя
      dlPdp.DataSource = GetPdPsBySmo(smo.Id);
      dlPdp.DataBind();
    }

    /// <summary>
    /// The save changes.
    /// </summary>
    public void SaveChanges()
    {
      SaveChanges(Guid.Empty);
    }

    /// <summary>
    /// The save changes.
    /// </summary>
    /// <param name="newUserId">
    /// The new user id.
    /// </param>
    public void SaveChanges(Guid newUserId)
    {
      var pdpId = (string.IsNullOrEmpty(dlPdp.SelectedValue) || dlPdp.SelectedValue == "-1")
                    ? null
                    : (Guid?)Guid.Parse(dlPdp.SelectedValue);
      securityService.AssignPdpToUser(newUserId != Guid.Empty ? newUserId : userId, pdpId);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The page_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Init(object sender, EventArgs e)
    {
      securityService = ObjectFactory.GetInstance<ISecurityService>();
      smoService = ObjectFactory.GetInstance<ISmoService>();
    }

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
      userId = Request.QueryString["userId"] == null ? Guid.Empty : Guid.Parse(Request.QueryString["userId"]);

      if (!IsPostBack)
      {
        lbTitle.Text = string.Format("Назначение пункта выдачи для пользователя: {0}", Request.QueryString["userName"]);

        var currentUser = securityService.GetCurrentUser();
        if (securityService.IsUserHasAdminPermissions(currentUser))
        {
          AssignDataSourcesForAdminMode(currentUser);
        }
        else
        {
          if (currentUser.PointDistributionPolicy() != null && securityService.IsUserAdminSmo(currentUser.Id))
          {
            AssignDataSourcesForOwnSmo(currentUser);
          }
          else
          {
            if (currentUser.PointDistributionPolicy() != null && securityService.IsUserAdminTF(currentUser.Id))
            {
              AssignDataSourcesForOwnRegion(currentUser);
            }
          }
        }

        // случай добавления нового пользователя
        if (userId == Guid.Empty)
        {
          // устанавливаем значения комбобоксов по умолчанию исходя из текущего пользователя, если для редактируемого пользователя не назначен pdp
          SetComboboValues(currentUser);

          // соответствует элементу не выбран
          dlPdp.SelectedIndex = 0;
          return;
        }

        // открыли на редактирование
        if (userId != Guid.Empty)
        {
          var user = securityService.GetUser(userId);

          var allowAssignPdp = true;

          // администратор смо не может назначать пункт выдачи для администратора территорального фонда
          if (!securityService.IsUserHasAdminPermissions(currentUser)
              && !securityService.IsUserAdminTF(currentUser.Id))
          {
            allowAssignPdp = !securityService.IsUserAdminTF(userId);
          }

          dlTFoms.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;
          dlSmo.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;
          dlPdp.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;

          if (user.PointDistributionPolicy() != null)
          {
            SetComboboValues(user);
          }
          else
          {
            // устанавливаем значения комбобоксов по умолчанию исходя из текущего пользователя, если для редактируемого пользователя не назначен pdp
            SetComboboValues(currentUser);

            // соответствует элементу не выбран
            dlPdp.SelectedIndex = 0;
          }
        }
      }
    }

    /// <summary>
    /// The dl smo_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DlSmoSelectedIndexChanged(object sender, EventArgs e)
    {
      // получаем пункты выдачи по страховой медицинской организации
      if (string.IsNullOrEmpty(dlSmo.SelectedValue))
      {
        return;
      }

      dlPdp.DataSource = GetPdPsBySmo(Guid.Parse(dlSmo.SelectedValue));
      dlPdp.DataBind();
    }

    /// <summary>
    /// The dl t foms_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DlTFomsSelectedIndexChanged(object sender, EventArgs e)
    {
      // получаем страховые медицинские организации по территорильному фонду
      if (string.IsNullOrEmpty(dlTFoms.SelectedValue))
      {
        return;
      }

      dlSmo.DataSource = smoService.GetSmosByTfom(Guid.Parse(dlTFoms.SelectedValue));
      dlSmo.DataBind();
      DlSmoSelectedIndexChanged(null, null);
    }

    /// <summary>
    /// The rf point_ server validate.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void RfPointServerValidate(object source, ServerValidateEventArgs args)
    {
      args.IsValid = Guid.Parse(dlPdp.SelectedValue) != Guid.Empty;
    }

    /// <summary>
    /// Получает список плюс пустой элемент для удобства выбора и сброса значения в выпадающем списке
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    private IList<Organisation> GetPdPsBySmo(Guid smoId)
    {
      var result = smoService.GetPDPsBySmo(smoId);
      var pdp = new Organisation { ShortName = "Не выбран", Id = Guid.Empty };
      result.Insert(0, pdp);
      return result;
    }

    /// <summary>
    /// The set combobo values.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    private void SetComboboValues(User user)
    {
      if (user.PointDistributionPolicyId == null)
      {
        return;
      }

      dlTFoms.SelectedValue = user.GetTf().Id.ToString();
      dlSmo.SelectedValue = user.GetSmo().Id.ToString();
      dlPdp.SelectedValue = user.PointDistributionPolicyId.ToString();
    }

    #endregion
  }
}