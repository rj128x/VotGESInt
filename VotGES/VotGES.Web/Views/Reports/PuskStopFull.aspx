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
				

		td.lb,th.lb
		{
			border-left-width:medium;
			border-left-color:#473C8B;
		}
		
		td.rb,th.rb
		{			
			border-right-width:medium;
			border-right-color:#473C8B;
		}
		
		td.runned
		{			
			background-color:#43CD80;
		}
		
		td.stopped
		{
			background-color:#FF7F50;
		}
		
	 </style>
</head>
<body>
	<h1>Работа генераторов с <%=Model.DateStart.ToString("dd.MM.yyyy HH:mm") %> по <%=Model.DateEnd.ToString("dd.MM.yyyy HH:mm") %></h1>
    <table class='cifr'>
		<tr>
			<th class='lb rb' rowspan='2'>Дата</th>
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
			<tr height="<%=Math.Round(ev.DiffMin/4+20)%>">
				<th class='lb rb'><%=ev.Date.ToString("dd.MM.yyyy HH:mm:ss") %></th>
				<%for (int ga=1; ga <= 10; ga++) { %>
				<td valign='top' class='lb <%=ev.Data[ga].Runned?"runned":"stopped"%>' title='<%=ga.ToString()+"ГГ - " +ev.Date.ToString("dd.MM.yyyy HH:mm:ss")%>'><%=PuskStopEvent.getValue(ev.Data[ga].Start, ev.Data[ga].Stop, "Пуск", "Стоп", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")%></td>				
				<%if (ga <= 2 || ga >= 9) { %>
					<td valign='top' class='<%=ev.Data[ga].RunnedGR?"runned":"stopped"%>' title='<%=ga.ToString()+"ГГ (ГР) - " +ev.Date.ToString("dd.MM.yyyy HH:mm:ss")%>'><%=PuskStopEvent.getValue(ev.Data[ga].StartGR, ev.Data[ga].StopGR, "Пуск", "Стоп", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")%></td>
					<td valign='top' class='rb <%=ev.Data[ga].RunnedSK?"runned":"stopped"%>' title='<%=ga.ToString()+"ГГ (СК) - " +ev.Date.ToString("dd.MM.yyyy HH:mm:ss")%>'><%=PuskStopEvent.getValue(ev.Data[ga].StartSK, ev.Data[ga].StopSK, "Пуск", "Стоп", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")%></td>
				<%}
				} %>
			</tr>
		<%} %>
	</table>
</body>
</html>
