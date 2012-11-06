<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.ReportAnswer>" ContentType="application/vnd.ms-excel; charset=utf-8" %>
<%@ Import Namespace="VotGES.Piramida.Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PBR</title>
	 <style>
	 	table,tr,td,p {
			font-family: 'Arial';
			font-size: 8pt;
		}
		
		h1,h2,h3,h4,h5,h6,hr
		{
			padding:0;
			margin:0;
		}
		
		
		h1
		{
			font-family: 'Arial';
			font-size: 10pt;
		}
		
		h2
		{
			font-family: 'Arial';
			font-size: 8pt;
		}
		
	 	table {
			border-collapse: collapse;		
		}
		
		td, th {
			border-width: 1px;
			border-style: solid;
			border-color: #BBBBFF;
			padding-left: 3px;
			padding-right: 3px;
		}
		
		table.cifr td{	
			text-align: right;
			white-space: nowrap;
			padding-left: 1px;
			padding-right: 1px;
			width:80px;
		}

		table.cifr th{
			text-align: center;			
			padding-left: 1px;			
			padding-right: 1px;
			width:80px;
		}
		
		
		table.cifr
		{
			margin-top:5px;
		}

		table td.right,table th.right{
			text-align: right;
		}	
		
		table td.fill, table th.fill{
			background-color:Gray;
		}	
	 </style>
</head>
<body>
	<table class='cifr'>
		<tr>
			<th>Дата</th>			
			<%foreach (string name in Model.Columns.Values) { %>
			<th><%=name %></th>
			<%} %>
		</tr>		
		<%foreach (ReportAnswerRecord record in Model.Data) { %>
			<tr>
				<th>&nbsp;<%=record.Header %></th>
				<%foreach (string id in Model.Columns.Keys){ %>
					<td><%=record.DataStr.ContainsKey(id)?record.DataStr[id].ToString(Model.Formats[id]):"-"%></td>
				<%} %>
			</tr>
		<%} %>	
	</table>
</body>
</html>
