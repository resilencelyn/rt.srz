using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;

namespace rt.srz.ui.pvp.Pages
{
  using rt.core.model.security;

  public partial class SMCWrite : System.Web.UI.Page
  {
    #region Fields

    /// <summary>
    /// Экземпляр сервиса для работы с заявлениями
    /// </summary>
    protected IStatementService statementService = null;

    /// <summary>
    /// Экземпляр сервиса аутентификации
    /// </summary>
    protected IAuthService authService = null;

    #endregion

    #region Properties
    /// <summary>
    /// Gets the statement id.
    /// </summary>
    private string StatementId
    {
      get
      {
        return Request.QueryString["StatementId"];
      }
    }
    #endregion
    
    #region Event

    protected void Page_Init(object sender, EventArgs e)
    {
      statementService = ObjectFactory.GetInstance<IStatementService>();
      authService = ObjectFactory.GetInstance<IAuthService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      //Попытка загрузки заявления
      Guid statementId = Guid.Empty;
      if (!Guid.TryParse(StatementId, out statementId))
        return; //TODO: не верный Id, что делать

      Statement statement = statementService.GetStatement(statementId);
      if (statement != null)
      {
        if (statement.InsuredPersonData != null)
        {
          hfLastName.Value = statement.InsuredPersonData.LastName;
          lbLastName.Text = statement.InsuredPersonData.LastName;

          hfFirstName.Value = statement.InsuredPersonData.FirstName;
          lbFirstName.Text = statement.InsuredPersonData.FirstName;

          hfMiddleName.Value = statement.InsuredPersonData.MiddleName;
          lbMiddleName.Text = statement.InsuredPersonData.MiddleName;

          hfBirthdate.Value = statement.InsuredPersonData.Birthday.HasValue ? statement.InsuredPersonData.Birthday.Value.ToString("dd.MM.yyyy") : string.Empty;
          lbBirthdate.Text = string.Format("Дата рождения: {0}", statement.InsuredPersonData.Birthday.HasValue ? statement.InsuredPersonData.Birthday.Value.ToString("dd.MM.yyyy") : string.Empty);
        }

        if (statement.PointDistributionPolicy != null && statement.PointDistributionPolicy.Parent != null && statement.PointDistributionPolicy.Parent.Parent != null)
        {
          hfOGRN.Value = statement.PointDistributionPolicy.Parent.Ogrn;
          lbOGRN.Text = string.Format("ОГРН СМО: {0}", statement.PointDistributionPolicy.Parent.Ogrn);

          hfOKATO.Value = statement.PointDistributionPolicy.Parent.Parent.Okato;
          lbOKATO.Text = string.Format("ТФОМ ОКАТО: {0}", statement.PointDistributionPolicy.Parent.Parent.Okato);
        }

        hfDateFrom.Value = statement.DateFiling.HasValue ? statement.DateFiling.Value.ToString("dd.MM.yyyy") : string.Empty;
        lbDateFrom.Text = string.Format("Дата начала страхования: {0}", statement.DateFiling.HasValue ? statement.DateFiling.Value.ToString("dd.MM.yyyy") : string.Empty);

        hfDateTo.Value = new DateTime(2030, 1, 1).ToString("dd.MM.yyyy"); //TODO: логика для даты окончания;
        lbDateTo.Text = string.Format("Дата окончания страхования: {0}", new DateTime(2030, 1, 1).ToString("dd.MM.yyyy")); //TODO: логика для даты окончания;
      }
    }

    #endregion
  }
}