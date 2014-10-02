var calM;
var calY;

function pageLoad() {
  calM = $find("monthCalendar");
  if (calM == undefined)
    return;
  calM._todaysDateFormat = "MMMM yyyy";
  
  calY = $find("yearCalendar");
  if (calY == undefined)
    return;
  calY._todaysDateFormat = "yyyy";

  calM._cell$delegates =
        {
          mouseover: Function.createDelegate(calM, calM._cell_onmouseover),
          mouseout: Function.createDelegate(calM, calM._cell_onmouseout),

          click: Function.createDelegate(calM, function (e) {
            e.stopPropagation();
            e.preventDefault();

            if (!calM._enabled) return;

            var target = e.target;
            var visibleDate = calM._getEffectiveVisibleDate();

            Sys.UI.DomElement.removeCssClass(target.parentNode, "ajax__calendar_hover");

            switch (target.mode) {
              case "prev":
              case "next":
                calM._switchMonth(target.date);
                break;
              case "title":
                switch (calM._mode) {
                  case "days": calM._switchMode("months"); break;
                  case "months": calM._switchMode("years"); break;
                }
                break;
              case "month":
                calM.set_selectedDate(target.date);
                calM._switchMonth(target.date);
                calM._blur.post(true);
                calM.raiseDateSelectionChanged();
                break;
              case "year":
                if (target.date.getFullYear() == visibleDate.getFullYear())
                  calM._switchMode("months");
                else {
                  calM._visibleDate = target.date;
                  calM._switchMode("months");
                }
                break;
              case "today":
                calM.set_selectedDate(target.date);
                calM._switchMonth(target.date);
                calM._blur.post(true);
                calM.raiseDateSelectionChanged();
                break;
            }
          })
        }


  calY._cell$delegates =
        {
          mouseover: Function.createDelegate(calY, calY._cell_onmouseover),
          mouseout: Function.createDelegate(calY, calY._cell_onmouseout),

          click: Function.createDelegate(calY, function (e) {
            e.stopPropagation();
            e.preventDefault();

            if (!calY._enabled) return;

            var target = e.target;

            Sys.UI.DomElement.removeCssClass(target.parentNode, "ajax__calendar_hover");

            switch (target.mode) {
              case "prev":
              case "next":
                calY._switchMonth(target.date);
                break;
              case "year":
                calY.set_selectedDate(target.date);
                calY._switchMonth(target.date);
                calY._blur.post(true);
                calY.raiseDateSelectionChanged();
                break;
              case "today":
                calY.set_selectedDate(target.date);
                calY._switchMonth(target.date);
                calY._blur.post(true);
                calY.raiseDateSelectionChanged();
                break;
            }
          })
        }
}

function onMonthCalendarShown(sender) {
  sender._switchMode("months", true);

  if (calM._monthsBody) {
    for (var i = 0; i < calM._monthsBody.rows.length; i++) {
      var row = calM._monthsBody.rows[i];
      for (var j = 0; j < row.cells.length; j++)
        $common.removeHandlers(row.cells[j].firstChild, calM._cell$delegates);
    }

    for (var i = 0; i < calM._monthsBody.rows.length; i++) {
      var row = calM._monthsBody.rows[i];
      for (var j = 0; j < row.cells.length; j++)
        $addHandlers(row.cells[j].firstChild, calM._cell$delegates);
    }
  }
}

function onYearCalendarShown(sender) {
  sender._switchMode("years", true);

  if (calY._yearsBody) {
    for (var i = 0; i < calY._yearsBody.rows.length; i++) {
      var row = calY._yearsBody.rows[i];
      for (var j = 0; j < row.cells.length; j++)
        $common.removeHandlers(row.cells[j].firstChild, calY._cell$delegates);
    }

    for (var i = 0; i < calY._yearsBody.rows.length; i++) {
      var row = calY._yearsBody.rows[i];
      for (var j = 0; j < row.cells.length; j++)
        $addHandlers(row.cells[j].firstChild, calY._cell$delegates);
    }
  }
}