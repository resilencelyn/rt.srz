Type.registerNamespace("rt.srz.ui.pvp.Controls");

rt.srz.ui.pvp.Controls.DateBox = function (element) {
  rt.srz.ui.pvp.Controls.DateBox.initializeBase(this, [element]);
}

rt.srz.ui.pvp.Controls.DateBox.prototype = {

  initialize: function () {
    rt.srz.ui.pvp.Controls.DateBox.callBaseMethod(this, 'initialize');

    this._oldBlur = this.get_element().onblur;
    this.get_element().onblur = null;

    $addHandlers(this.get_element(), { 'blur': this._onBlur }, this);

    var element = this.get_element();

    element.attachEvent("onkeydown", function (e) {
      if (event.keyCode == 13) {
      VerifyDate(e.srcElement);
      return;
      }
      return CheckDate(e.srcElement);
      });

    $addHandlers(this.get_element(), { 'paste': function (e) { return false; } }, this);
  },

  dispose: function () {
    $clearHandlers(this.get_element());
    rt.srz.ui.pvp.Controls.DateBox.callBaseMethod(this, 'dispose');
  },

  oldBlur: function () {
    if (this._oldBlur) {
      this._oldBlur();
    }
  },

  _onBlur: function (e) {
    VerifyDate(e.target);
    this.oldBlur();
  },

}

rt.srz.ui.pvp.Controls.DateBox.registerClass('rt.srz.ui.pvp.Controls.DateBox', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
