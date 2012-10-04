<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.PuskStopReportFull>" %>
<%@ Import Namespace="VotGES.Piramida.Report" %>
<%@ Import Namespace="VotGES.Piramida" %>
<%@ Import Namespace="VotGES" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PuskStopFull</title>
	 <style>
	 	table,tr,td,p {
			font-family: 'Arial';
			font-size: 8pt;
		}
		
		@media print
		{
			table,tr,td,p {
				font-family: 'Arial';
				font-size: 7pt;
			}
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
			text-align: center;
			white-space: nowrap;
			padding-left: 1px;
			padding-right: 1px;
			width:80px;
		}

		table.cifr th{
			text-align: center;			
			padding-left: 1px;			
			padding-right: 1px;
			white-space: nowrap;
		}
				

		td.lb
		{
			border-left-width:medium;
		}
		
		td.rb
		{			
			border-right-width:medium;
		}
		
		td.runned
		{			
			background-color:Green;
		}
		
		td.stopped
		{
			background-color:Red;
		}
		
	 </style>
</head>
<body>
	<h1>Работа генераторов с <%=Model.DateStart.ToString("dd.MM.yyyy HH:mm") %> по <%=Model.DateEnd.ToString("dd.MM.yyyy HH:mm") %></h1>
    <table class='cifr'>
		<tr>
			<th rowspan='2'>Дата</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<th class='lb rb' colspan='<%=((ga<=2 || ga>=9)?3:1)%>'>
					Г/г №<%=ga %>
				</th>
			<%} %>
		</tr>		

		<tr>
			<%for (int ga=1; ga <= 10; ga++) { %>
				<th class='lb'>Раб</th>				
				<%if (ga <= 2 || ga >= 9) { %>
					<th>ГР</th>
					<th class='rb'>СК</th>
			<%}
			} %>
		</tr>	
		<%foreach (PuskStopEvent ev in Model.Data.Values) { %>
			<tr>
				<th class='lb rb'><%=ev.Date.ToString("dd.MM.yyyy HH:mm:ss") %></th>
				<%for (int ga=1; ga <= 10; ga++) { %>
				<td class='lb <%=ev.Data[ga].Runned?"runned":"stopped"%>'><%=PuskStopEvent.getValue(ev.Data[ga].Start, ev.Data[ga].Stop, "Пуск", "Стоп", "&nbsp;") %></td>				
				<%if (ga <= 2 || ga >= 9) { %>
					<td class='<%=ev.Data[ga].RunnedGR?"runned":"stopped"%>'><%=PuskStopEvent.getValue(ev.Data[ga].StartGR, ev.Data[ga].StopGR, "Пуск", "Стоп", "&nbsp;")%></td>
					<td class='rb <%=ev.Data[ga].RunnedSK?"runned":"stopped"%>'><%=PuskStopEvent.getValue(ev.Data[ga].StartSK, ev.Data[ga].StopSK, "Пуск", "Стоп", "&nbsp;")%></td>
				<%}
				} %>
			</tr>
		<%} %>
	</table>
</body>
</html>
