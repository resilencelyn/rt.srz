using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Controls.Twins;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages
{
  using rt.core.model.interfaces;

  public partial class SeparateNew : System.Web.UI.Page
  {
    #region Fields

    private ITFService _service;
    private IStatementService _statementService;
    private ISecurityService _security;

    #endregion

    #region Properties

    private IList<Statement> HistoryItems
    {
      get { return (List<Statement>)Session[SessionConsts.CStatementsHistory]; }
      set
      {
        Session[SessionConsts.CStatementsHistory] = value;
        gridTop.SetData(value);
      }
    }

    private IList<Statement> SeparateItems
    {
      get { return (List<Statement>)Session[SessionConsts.CStatementsToSeparate]; }
      set
      {
        Session[SessionConsts.CStatementsToSeparate] = value;
        gridBottom.SetData(value);
      }
    }

    private Guid InsuredPersonId
    {
      get { return (Guid)Session[SessionConsts.CInsuredId]; }
    }

    private bool AllowDeleteDeathInfo
    {
      get { return (bool)Session[SessionConsts.CDeleteDeathInfo]; }
    }

    #endregion

    #region Page events

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<ITFService>();
      _statementService = ObjectFactory.GetInstance<IStatementService>();
      _security = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        Guid statementId = (Guid)Session[SessionConsts.CGuidStatementId];
        Statement statement = _statementService.GetStatement(statementId);
        Session[SessionConsts.CInsuredId] = statement.InsuredPerson.Id;
        HistoryItems = _statementService.GetAllByInsuredId(statement.InsuredPerson.Id);
        SeparateItems = new List<Statement>();
      }
    }

    protected void Separate_Click(object sender, EventArgs e)
    {
      _service.Separate(InsuredPersonId, SeparateItems);
      RedirectUtils.RedirectToMain(Response);
    }

    /// <summary>
    /// перемещение из нижнего списка в верхний
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Top_Click(object sender, EventArgs e)
    {
      //из списка на разделение удаляем перемещаемые элементы
      var movedRows = gridBottom.GetSelected();
      var movedData = GetDataListByGuids(SeparateItems, movedRows);
      var fromList = SeparateItems;
      RemoveRows(ref fromList, movedRows);
      SeparateItems = fromList;
      //в список истории добавляем перемещаемые элементы
      var toAdd = HistoryItems;
      AddRows(toAdd, movedData);
      HistoryItems = toAdd;
      UpdateSeparateButton();
    }

    /// <summary>
    /// перемещение из верхнего списка в нижний
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Bottom_Click(object sender, EventArgs e)
    {
      //из списка истории удаляем перемещаемые элементы
      var movedRows = gridTop.GetSelected();
      var movedData = GetDataListByGuids(HistoryItems, movedRows);
      var fromList = HistoryItems;
      RemoveRows(ref fromList, movedRows);
      HistoryItems = fromList;
      //в список разделения добавляем перемещаемые элементы
      var toAdd = SeparateItems;
      AddRows(toAdd, movedData);
      SeparateItems = toAdd;
      UpdateSeparateButton();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Удаляет из списка заявлений указанные данные
    /// </summary>
    /// <param name="fromList"></param>
    /// <param name="deletedRows"></param>
    private void RemoveRows(ref IList<Statement> fromList, List<Guid> deletedIdRows)
    {
      fromList = fromList.Where(x => !deletedIdRows.Contains(x.Id)).ToList();
    }

    private void AddRows(IList<Statement> listToAdd, IList<Statement> addedRows)
    {
      foreach (var row in addedRows)
      {
        listToAdd.Add(row);
      }
    }

    /// <summary>
    /// Получает список заявлений по списку идентификаторов на основании данных
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IList<Statement> GetDataListByGuids(IList<Statement> data, List<Guid> ids)
    {
      return data.Where(x => ids.Contains(x.Id)).ToList();
    }

    private void UpdateSeparateButton()
    {
      btnSeparate.Enabled = HistoryItems.Count >= 1 && SeparateItems.Count >= 1;
    }

    #endregion

  }
}