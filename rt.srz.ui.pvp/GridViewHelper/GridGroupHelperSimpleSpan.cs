using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp
{
  public class GridGroupHelperSimpleSpan<DataItemType>
  {
    private readonly Expression<Func<DataItemType, string>> _columnToGroup;

    private string ColumnToGroupName
    {
      get { return ((MemberExpression)(_columnToGroup.Body)).Member.Name; }
    }

    public GridGroupHelperSimpleSpan(GridView grid, Expression<Func<DataItemType, string>> columnToGroup)
    {
      _columnToGroup = columnToGroup;
      grid.DataBound += (object sender, EventArgs e) => MadeSpanDataBound(grid);
    }

    public IList<DataItemType> GetDataList(GridView grid, Expression<Func<DataItemType, int>> columnRecordNumber, IList<DataItemType> list)
    {
      var groupfunc = _columnToGroup.Compile();
      var propRecordNumber = (PropertyInfo)((MemberExpression)(columnRecordNumber.Body)).Member;

      var groups = list.GroupBy(groupfunc);
      foreach (IGrouping<object, DataItemType> group in groups)
      {
        int j = 1;
        foreach (DataItemType item in group)
        {
          //проставляем номер в группе
          propRecordNumber.SetValue(item, j, null);
          j++;
        }
      }
      return groups.SelectMany(x => x.ToList()).ToList();
    }

    public void MadeSpanDataBound(GridView grid)
    {
      for (int i = grid.Rows.Count - 1; i > 0; i--)
      {
        GridViewRow row = grid.Rows[i];
        GridViewRow previousRow = grid.Rows[i - 1];

        for (int j = 0; j < row.Cells.Count; j++)
        {
          TableCell cell = row.Cells[j];

          if (!((cell is DataControlFieldCell && (cell as DataControlFieldCell).ContainingField is BoundField) &&
            ColumnToGroupName == ((cell as DataControlFieldCell).ContainingField as BoundField).DataField))
          {
            continue;
          }
          if (cell.Text == previousRow.Cells[j].Text)
          {
            if (previousRow.Cells[j].RowSpan == 0)
            {
              if (cell.RowSpan == 0)
              {
                previousRow.Cells[j].RowSpan += 2;
              }
              else
              {
                previousRow.Cells[j].RowSpan = cell.RowSpan + 1;
              }
              cell.Visible = false;
            }
          }
        }
      }
    }
  }
}