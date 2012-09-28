<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.PuskStopReport>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PuskStop</title>
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
			white-space: nowrap;
		}
		
		
		table.cifr
		{
			margin-top:2px;
		}

		table td.right,table th.right{
			text-align: right;
		}	
	 </style>
</head>
<body>
	<h1>Работа генераторов с <%=Model.DateStart.ToString("dd.MM.yyyy HH:mm") %> по <%=Model.DateEnd.ToString("dd.MM.yyyy HH:mm") %></h1>
    <table class='cifr'>
		<tr>
			<th>Параметр</th>
			<th>Генератор №1</th>
			<th>Генератор №2</th>
			<th>Генератор №3</th>
			<th>Генератор №4</th>
			<th>Генератор №5</th>
			<th>Генератор №6</th>
			<th>Генератор №7</th>
			<th>Генератор №8</th>
			<th>Генератор №9</th>
			<th>Генератор №10</th>
			<th>Сумма</th>
		</tr>
		<tr>
			<th>Пусков</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.Data[ga].CountPusk.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.SumRecord.CountPusk.ToString("0") %> 
			</td>
		</tr>
		<tr>
			<th>Остановов</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.Data[ga].CountStop.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.SumRecord.CountStop.ToString("0") %>
			</td>
		</tr>
		<tr>
			<th>Пусков-переводов в ГР </th>
			<%for (int ga=1; ga <= 10; ga++) {
				if (ga <= 2 || ga >= 9) {%>
					<td>
						<%=Model.Data[ga].CountPuskGen.ToString("0")%> 
					</td>
				<%} else {%>
					<td>&nbsp;</td>
				<%}
			} %>
			<td>
				<%=Model.SumRecord.CountPuskGen.ToString("0") %> 
			</td>
		</tr>
		<tr>
			<th>Пусков-переводов в  СК</th>
			<%for (int ga=1; ga <= 10; ga++) {
				if (ga <= 2 || ga >= 9) {%>
					<td>						
						<%=Model.Data[ga].CountPuskSK.ToString("0")%>
					</td>
				<%} else {%>
					<td>&nbsp;</td>
				<%}
			} %>
			<td>
				<%=Model.SumRecord.CountPuskSK.ToString("0") %>
			</td>
		</tr>		
			
		<tr>
			<th>Время работы в ГР</th>
			<%for (int ga=1; ga <= 10; ga++) {
				if (ga <= 2 || ga >= 9) {%>
					<td>						
						<%=Model.Data[ga].HoursGenStr%>
					</td>
				<%} else {%>
					<td>&nbsp;</td>
				<%}
			} %>
			<td>
				&nbsp;
			</td>
		</tr>			

		<tr>
			<th>Время работы в СК</th>
			<%for (int ga=1; ga <= 10; ga++) {
				if (ga <= 2 || ga >= 9) {%>
					<td>						
						<%=Model.Data[ga].HoursSKStr%>
					</td>
				<%} else {%>
					<td>&nbsp;</td>
				<%}
			} %>
			<td>
				&nbsp;
			</td>
		</tr>			

		<tr>
			<th>Время работы</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.Data[ga].HoursWorkStr %>
				</td>
			<%} %>
			<td>
				&nbsp;
			</td>
		</tr>

		<tr>
			<th>Время простоя</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.Data[ga].HoursStayStr %>
				</td>
			<%} %>
			<td>
				&nbsp;
			</td>
		</tr>

	</table>
</body>
</html>
