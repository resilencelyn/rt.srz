<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.Test" %>

<%@ Register Src="../../Controls/Administration/SmoDetailControl.ascx" TagName="SmoDetailControl" TagPrefix="uc" %>

	<script type="text/javascript" src="../../../Scripts/DateTextBox.js"></script>
	<script type="text/javascript" src="../../Scripts/LFM.js"></script>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
	<ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="True" />    

	<uc:SmoDetailControl ID="smoDetailControl" runat="server" />

    </div>
    </form>
</body>
</html>
