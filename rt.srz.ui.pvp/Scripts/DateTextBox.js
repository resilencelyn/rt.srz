function DateField_KeyDown(dateField, calendarExtenderName) {
  lastKeyCodeEntered = window.event.keyCode;
  if ((lastKeyCodeEntered == '37')        //keyCode 37=left arrow
      || (lastKeyCodeEntered == '38')     //keyCode 38=up arrow
      || (lastKeyCodeEntered == '39')     //keyCode 39=right arrow
      || (lastKeyCodeEntered == '40'))    //keyCode 40=down arrow
  {
    var dtbehav = $find(calendarExtenderName);
    if (!dtbehav._isOpen) {
      //если при закрытом окошке выбора даты нажимаются клавиши вверх или вниз, то это окошко отображается для выбора даты
      if (lastKeyCodeEntered == '38' || lastKeyCodeEntered == '40') {
        dtbehav.show();
      }
      //если окошко закрыто то не надо перехватывать клавиши влево вправо, т.к. дату менять не надо в этом случае
      return;
    }

    var enteredDate = dtbehav.get_selectedDate();

    if (enteredDate == null) {
      enteredDate = new Date();
    }
    else {
      advanceValue = 0;
      switch (lastKeyCodeEntered) {
        case 37:
          advanceDays = -1;
          break;
        case 38:
          advanceDays = -7;
          break;
        case 39:
          advanceDays = 1;
          break;
        case 40:
          advanceDays = 7;
          break;
      }
      enteredDate.setDate(enteredDate.getDate() + advanceDays);
    }
    dateField.value = (enteredDate.getMonth() + 1) + "/" + enteredDate.getDate() + "/" + enteredDate.getFullYear();
    dtbehav.set_selectedDate(dateField.value);
  }
}

function CheckDate(textBox) {

  //если нажали ctrl + del то надо очистить текст контрола
  if (event.ctrlKey && event.keyCode == 46) {
    textBox.value = "";
    return false;
  }

  if (CheckSpecKeys(event.keyCode))
    return true;

  if (!CheckInput(event.keyCode, textBox))
    return false;

  if (textBox.value.length == 2) {
    var day = textBox.value;
    if (day == 0 || day > 31) {
      textBox.value = "";
      return false;
    }
    textBox.value = textBox.value + '.';
    return;
  }

  if (textBox.value.length == 5) {
    var month = textBox.value.substring(3, 5);
    if (month == 0 || month > 12) {
      textBox.value = textBox.value.substring(0, 3);
      return false;
    }
    textBox.value = textBox.value + '.';
    return;
  }
}

function VerifyDate(textBox) {
  var dateString = textBox.value.split(".");
  if (dateString != undefined && dateString.length == 3 && dateString[2].length == 2)
    dateString[2] = '20' + dateString[2];
  var date = new Date(dateString[2], dateString[1] - 1, dateString[0]);

  if (isNaN(date.getTime()))
    textBox.value = "";
  else {
    var currDay = date.getDate().toString().length == 1 ? '0' + date.getDate() : date.getDate();
    var currMonth = (date.getMonth() + 1).toString().length == 1 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);

    var currYear = date.getFullYear().toString().length == 3 ? '0' + date.getFullYear() : date.getFullYear();

    if (parseInt(currYear) < 1753)
      currYear = "1753";
    textBox.value = currDay + "." + currMonth + "." + currYear;
  }
}

function CheckMonth(textBox) {

  if (CheckSpecKeys(event.keyCode))
    return true;

  if (textBox.value.length == 7)
    return false;

  if (!CheckInput(event.keyCode, textBox))
    return false;

  if (textBox.value.length == 2) {
    var month = textBox.value;
    if (month == 0 || month > 12) {
      textBox.value = "";
      return false;
    }
    textBox.value = textBox.value + '.';
    return;
  }
}

function VerifyMonth(textBox) {

  var dateString = textBox.value.split(".");
  var date = new Date(dateString[1], dateString[0] - 1, 01);

  if (isNaN(date.getTime()))
    textBox.value = "";
  else {
    var currMonth = (date.getMonth() + 1).toString().length == 1 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
    var currYear = date.getFullYear().toString().length == 3 ? '0' + date.getFullYear() : date.getFullYear();
    textBox.value = currMonth + "." + currYear;
  }
}

function CheckYear(textBox) {

  if (CheckSpecKeys(event.keyCode))
    return true;

  if (textBox.value.length == 4)
    return false;

  if (!CheckInput(event.keyCode))
    return false;
}

function VerifyYear(textBox) {

  var date = new Date(textBox.value, 01, 01);

  if (isNaN(date.getTime()))
    textBox.value = "";
  else {
    var currYear = date.getFullYear().toString().length == 3 ? '0' + date.getFullYear() : date.getFullYear();

    if (parseInt(currYear) < 1753)
      currYear = "1753";

    textBox.value = currYear;
  }
}

function CheckSpecKeys(keypressed) {

  switch (keypressed) {
    //backspace
    case 8:
      return true;
      //tab
    case 9:
      return true;
      //enter
    case 13:
      return true;
      //ctrl
    case 17:
      return true;

    case 18:
      return true;
      //end
    case 35:
      return true;
      //home
    case 36:
      return true;
      //left arrow
    case 37:
      return true;
      //right arrow
    case 39:
      return true;
      //delete
    case 46:
      return true;
  }
}

function CheckInput(keypressed, textBox) {

  if (event.shiftKey) {
    event.returnValue = false;
    return false;
  }

  if (String.fromCharCode(keypressed).match(/\s\w*/)) {
    return false;
  }

  //если длина 10 - т.е. введена полноостью дата, то при нажатии циферуи очищаем поле
  var len10;
  if (textBox!= null && textBox.value.length == 10)
    len10 = true;

  switch (keypressed) {
    //основная клавиатура циферки
    case 48:
    case 49: 
    case 50:
    case 51:
    case 52:
    case 53:
    case 54:
    case 55:
    case 56:
    case 57:

      //доп клавиатура циферки
    case 96:
    case 97:
    case 98:
    case 99:
    case 100:
    case 101:
    case 102:
    case 103:
    case 104:
    case 105:
      {
        if (len10) 
        {
          textBox.value = "";
        }
        return true;
      }
  }

  if (!String.fromCharCode(keypressed).match(/^\s*\d+\s*$/)) {
    return false;
  }
  return true;
}