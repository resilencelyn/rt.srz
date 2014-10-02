
function EndRequest(s, e, scrollAreaId, hfScrollPositionId) 
{
  var h = document.getElementById(hfScrollPositionId);
  document.getElementById(scrollAreaId).scrollTop = h.value;
}

function OnLoad(scrollAreaId, hfScrollPositionId)
{
  var h = document.getElementById(hfScrollPositionId);
  document.getElementById(scrollAreaId).scrollTop = h.value;
}

function SetDivPos(scrollAreaId, hfScrollPositionId) {
  var intY = document.getElementById(scrollAreaId).scrollTop;
  var h = document.getElementById(hfScrollPositionId);
  h.value = intY;
}
