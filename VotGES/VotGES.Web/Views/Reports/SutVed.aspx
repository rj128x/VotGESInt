<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.SutVedReport>" %>
<%@ Import Namespace="VotGES.Piramida.Report" %>
<%@ Import Namespace="VotGES.Piramida" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SutVed</title>
</head>
<body>
    <div>
		<table>
			<tr>
				<th rowspan='2'>Час</th>
				<th colspan='2'>ГЭС</th>
				<th colspan='3'>U на шинах</th>
				<th colspan='3'>P 500 кВ</th>
				<th colspan='3'>Q 500 кВ</th>
				<th colspan='4'>Общестанционные</th>
			</tr>
			<tr>
				<th>P</th>
				<th>Q</th>
				<th>110</th>
				<th>220</th>
				<th>500</th>
				<th>Емелино</th>
				<th>Карманово</th>
				<th>Вятка</th>
				<th>Емелино</th>
				<th>Карманово</th>
				<th>Вятка</th>
				<th>НБ</th>
				<th>ВБ</th>
				<th>Т</th>
				<th>Расход</th>
			</tr>
			<%foreach (DateTime date in Model.Dates){ %>
				<tr>
					<th><%=date.ToString("HH:mm") %></th>
					<td><%=Model[date,PiramidaRecords.MB_P_GES.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_Q_GES.Key] %></td>
					<td><%=Model[date,ReportMBRecords.MB_U_110.ID] %></td>
					<td><%=Model[date,ReportMBRecords.MB_U_220.ID] %></td>
					<td><%=Model[date,ReportMBRecords.MB_U_500.ID] %></td>
					<td><%=Model[date,PiramidaRecords.MB_P_Emelino_500.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_P_Karmanovo_500.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_P_Vyatka_500.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_Q_Emelino_500.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_Q_Karmanovo_500.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_Q_Vyatka_500.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_NB_Sgl.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_VB_Sgl.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_T.Key] %></td>
					<td><%=Model[date,PiramidaRecords.MB_Rashod.Key] %></td>
				</tr>
			<%} %>
		</table>
    
    </div>
</body>
</html>
