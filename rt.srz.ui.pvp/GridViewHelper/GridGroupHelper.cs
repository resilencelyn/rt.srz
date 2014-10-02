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
  public class GridGroupHelper<DataItemType>
  {
    private readonly Expression<Func<DataItemType, string>> _columnToGroup;

    private string ColumnToGroupName
    {
      get { return ((MemberExpression)(_columnToGroup.Body)).Member.Name; }
    }

    public GridGroupHelper(Expression<Func<DataItemType, string>> columnToGroup)
    {
      _columnToGroup = columnToGroup;
    }

    public IList<DataItemType> GetDataList(GridView grid, Expression<Func<DataItemType, int>> columnRecordNumber, IList<DataItemType> list)
    {
      var groupfunc = _columnToGroup.Compile();
      var propRecordNumber = (PropertyInfo)((MemberExpression)(columnRecordNumber.Body)).Member;

      GridViewHelper helper = new GridViewHelper(grid);
      helper.RegisterGroup(ColumnToGroupName, true, true);
      helper.GroupHeader += new GroupEvent(helper_GroupHeader);

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
      return groups.SelectMany(x=>x.ToList()).ToList();
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
      if (groupName == ColumnToGroupName)
      {
        row.BackColor = Color.FromArgb(0xfe, 0xce, 0x71);//Color.LightBlue;
        row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
      }
    }
  }
}