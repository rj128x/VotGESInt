<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.PBR.PBRFull>" ContentType="application/vnd.ms-excel; charset=utf-8"%>

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
	<h1><%=Model.Date.ToString("dd.MM.yyyy") %></h1>
	<table class='cifr'>
		<tr>
			<th rowspan='3'>Время</th>
			<th colspan='2'>ГТП-1 110кВ</th>
			<th colspan='4'>ГТП-2 220кВ</th>
			<th rowspan='3'>ГЭС</th>
			<th rowspan='3'>&nbsp;</th>
		</tr>
		<tr>
			<th>РГЕ-110кВ</th>
			<th rowspan='2'>Сумма</th>
			<th colspan='3'>РГЕ-220кВ</th>
			<th rowspan='2'>Сумма</th>			
		</tr>
		<tr>			
			<th>ГГ 1,2</th>			
			<th>ГГ 3,4</th>			
			<th>ГГ 5,6</th>			
			<th>ГГ 7-10</th>						
		</tr>
		<%foreach (DateTime dt in Model.GES.SteppedPBR.Keys) { %>
			<tr>
				<th><%=dt.AddHours(2).ToString("HH:mm") %></th>
				<td <%=Model.GTP1.ChangePBR[dt]?"class='fill'":""%>><%=Model.GTP1.SteppedPBR[dt].ToString("0.00") %></td>
				<th <%=Model.GTP1.ChangePBR[dt]?"class='fill'":""%>><%=Model.GTP1.SteppedPBR[dt].ToString("0.00") %></th>
				<td <%=Model.RGE2.ChangePBR[dt]?"class='fill'":""%>><%=Model.RGE2.SteppedPBR[dt].ToString("0.00") %></td>
				<td <%=Model.RGE3.ChangePBR[dt]?"class='fill'":""%>><%=Model.RGE3.SteppedPBR[dt].ToString("0.00") %></td>
				<td <%=Model.RGE4.ChangePBR[dt]?"class='fill'":""%>><%=Model.RGE4.SteppedPBR[dt].ToString("0.00") %></td>
				<th <%=Model.GTP2.ChangePBR[dt]?"class='fill'":""%>><%=Model.GTP2.SteppedPBR[dt].ToString("0.00") %></th>
				<th <%=Model.GES.ChangePBR[dt]?"class='fill'":""%>><%=Model.GES.SteppedPBR[dt].ToString("0.00") %></th>
				<td><%=Model.ChangePBR[dt]?"Смена нагрузки":"" %></td>
			</tr>
		<%} %>
		<tr>
			<th>Итог</th>
			<th><%=Model.GTP1.IntegratedPBR.Last().Value.ToString("0.00") %></th>
			<th><%=Model.GTP1.IntegratedPBR.Last().Value.ToString("0.00") %></th>
			<th><%=Model.RGE2.IntegratedPBR.Last().Value.ToString("0.00")%></th>
			<th><%=Model.RGE3.IntegratedPBR.Last().Value.ToString("0.00")%></th>
			<th><%=Model.RGE4.IntegratedPBR.Last().Value.ToString("0.00")%></th>
			<th><%=Model.GTP2.IntegratedPBR.Last().Value.ToString("0.00")%></th>
			<th><%=Model.GES.IntegratedPBR.Last().Value.ToString("0.00")%></th>
			<th>&nbsp;</th>
		</tr>
	</table>
</body>
</html>
