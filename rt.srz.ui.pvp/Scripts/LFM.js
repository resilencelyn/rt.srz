function limitKeyPressForLFM() {
  //Ограничение допустимых для ввода символов
  function KeyPressString(event) {
    //alow backspace(8), enter(13), and other non character keypress events
    if (event.which == "0" || event.which == "8" || event.which == "13" || event.ctrlKey || event.altKey) {
      return;
    }

    if (event.charCode != 0) {
      var regex = new RegExp("[$-/0-9A-Za-zА-ЯЁа-яё\x2E\x20\xA0\x5F\x96\x97\x27\x22\x60\x91\x93\x92\x94\x3C\x8B\xAB\x3E\x9B\xBB\x84]");
      var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
      if (!regex.test(key)) {
        event.preventDefault();
        return false;
      }
    }
  }

  //при использовании limitkeypress всё криво работает если ввести пробел - позволяет ввести только один пробел, который потом затирается. 
  //Если вводить одни пробелы, то вводится только один, но длина строки по количеству введённых пробелов при этом правильная
  $('.lastName').keypress(KeyPressString);
  $('.firstName').keypress(KeyPressString);
  $('.middleName').keypress(KeyPressString);

  //$('.firstName').limitkeypress({ rexp: /^[0-9A-Za-zА-ЯЁа-яё\x2E\x20\xA0\x5F\x2D\x96\x97\x27\x22\x60\x91\x93\x92\x94\3C\x8B\xAB\x3E\x9B\xBB\x84]*$/ });
  //$('.middleName').limitkeypress({ rexp: /^[0-9A-Za-zА-ЯЁа-яё\x2E\x20\xA0\x5F\x2D\x96\x97\x27\x22\x60\x91\x93\x92\x94\3C\x8B\xAB\x3E\x9B\xBB\x84]*$/ });
}

function PasteFio(elem, e) {
  var pasteData = window.clipboardData.getData('Text');
  var regex = new RegExp("^[$-/0-9A-Za-zА-ЯЁа-яё\x2E\x20\xA0\x5F\x96\x97\x27\x22\x60\x91\x93\x92\x94\x3C\x8B\xAB\x3E\x9B\xBB\x84]*$");
  if (!regex.test(pasteData)) {
    e.preventDefault();
    return false;
  }
  return true;
}

function PasteEnp(elem, e) {
  //запрещаем вставку содержимого енп если оно содержит отличные от цифирей символы
  var pasteData = window.clipboardData.getData('Text');
  var regex = new RegExp("^[0-9]*$");
  if (!regex.test(pasteData)) {
    e.preventDefault();
    return false;
  }
  return true;
}

function limitKeyPressForRoomNumber() {
  //Ограничение допустимых для ввода символов
  $('.room').limitkeypress({ rexp: /^[0-9]*$/ });
}

//Первый символ в верхнем регистре
function firstSymbolToUpper(textBox) {
  //Только для первого символа
  if (textBox.value.length != 0)
    return null;

  if (event.which == null) {  // IE
    if (event.keyCode < 32)
      return null; // спец. символ
    var symbol = String.fromCharCode(event.keyCode);
    symbol = symbol.toUpperCase(symbol);
    textBox.value += symbol;
    event.returnValue = false;
  }
}