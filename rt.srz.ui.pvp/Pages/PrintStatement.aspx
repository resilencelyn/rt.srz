<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintStatement.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.PrintStatement" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.XtraReports.Web" Assembly="DevExpress.XtraReports.v12.1.Web, Version=12.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/generator.js") %>"></script>

<script type="text/javascript">
	window.onload = open_in_new_tab;

	function open_in_new_tab() {
	  var str = Math.uuid();
	  var win = window.open('NewStatementSelection.aspx', '_reportblank' + str.toString());
	  if (win != null) {
	    win.focus();
	  }
	}
</script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Заявление</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False' ReportViewerID="ReportViewer">
				<Items>
					<dx:ReportToolbarButton ItemKind='Search' />
					<dx:ReportToolbarSeparator />
					<dx:ReportToolbarButton ItemKind='PrintReport' />
					<dx:ReportToolbarButton ItemKind='PrintPage' />
					<dx:ReportToolbarSeparator />
					<dx:ReportToolbarButton Enabled='False' ItemKind='FirstPage' />
					<dx:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' />
					<dx:ReportToolbarLabel ItemKind='PageLabel' />
					<dx:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'></dx:ReportToolbarComboBox>
					<dx:ReportToolbarLabel ItemKind='OfLabel' />
					<dx:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
					<dx:ReportToolbarButton ItemKind='NextPage' />
					<dx:ReportToolbarButton ItemKind='LastPage' />
					<dx:ReportToolbarSeparator />
					<dx:ReportToolbarButton ItemKind='SaveToDisk' />
					<dx:ReportToolbarButton ItemKind='SaveToWindow' />
					<dx:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
						<Elements>
							<dx:ListElement Value='pdf' />
							<dx:ListElement Value='mht' />
							<dx:ListElement Value='html' />
							<dx:ListElement Value='png' />
						</Elements>
					</dx:ReportToolbarComboBox>
				</Items>
				<Styles>
					<LabelStyle>
						<Margins MarginLeft='3px' MarginRight='3px' />
					</LabelStyle>
				</Styles>
			</dx:ReportToolbar>
			<dx:ReportViewer ID="ReportViewer" runat="server" ReportName="StatementReport"></dx:ReportViewer>
		</div>
	</form>
</body>
</html>
