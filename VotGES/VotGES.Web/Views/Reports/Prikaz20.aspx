<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.Prikaz20Report>" %>

<%@ Import Namespace="VotGES.Piramida.Report" %>
<%@ Import Namespace="VotGES.Piramida" %>
<%@ Import Namespace="VotGES" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Prikaz20</title>
	<style>
		table, tr, td, p
		{
			font-family: 'Arial';
			font-size: 8pt;
		}
		h1, h2, h3, h4, h5, h6, hr
		{
			padding: 0;
			margin: 0;
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
		table
		{
			border-collapse: collapse;
		}
		td, th
		{
			border-width: 1px;
			border-style: solid;
			border-color: #BBBBFF;
			padding-left: 3px;
			padding-right: 3px;
		}
		table.cifr td
		{
			text-align: right;
			white-space: nowrap;
			padding-left: 1px;
			padding-right: 1px;
			width: 80px;
		}
		
		table.cifr th
		{
			text-align: center;
			padding-left: 1px;
			padding-right: 1px;
		}
		
		
		table.cifr
		{
			margin-top: 5px;
		}
		
		table td.right, table th.right
		{
			text-align: right;
		}
		table td.left, table th.left
		{
			text-align: left;
			width: 350px;
		}
		table td.center, table th.center
		{
			text-align: center;
		}
	</style>
</head>
<body>
	<table class='cifr'>
		<tr>
			<td colspan='4' class='right' style='width: 100%'>
				Приложение №51<br />
				к приказу Минэнерго России<br />
				от 07.08.2008 №20
			</td>
		</tr>
		<tr>
			<td class='right'>
				<%=Model.Date.ToString("dd.MM.yyyy")%>
			</td>
			<td colspan='3'>
				&nbsp;
			</td>
			<tr>
				<tr>
					<td class='left'>
						тыс. кВт*час
					</td>
					<td colspan='3'>
						&nbsp;
					</td>
				</tr>
				<tr>
					<th>
						Наименование
					</th>
					<th>
						№стр
					</th>
					<th>
						Факт за сутки
					</th>
					<th>
						Факт нарастающим итогом с начала месяца
					</th>
				</tr>
				<tr>
					<th>
						А
					</th>
					<th>
						Б
					</th>
					<th>
						1
					</th>
					<th>
						2
					</th>
				</tr>
				<tr>
					<td class='left'>
						1. Выработка э/энергии, всего
					</td>
					<td class='center'>
						1
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GES.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GES.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.1. Выработка э/энергии по генератору №1
					</td>
					<td class='center'>
						2
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA1_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA1_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.2. Выработка э/энергии по генератору №2
					</td>
					<td class='center'>
						3
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA2_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA2_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.3. Выработка э/энергии по генератору №3
					</td>
					<td class='center'>
						4
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA3_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA3_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.4. Выработка э/энергии по генератору №4
					</td>
					<td class='center'>
						5
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA4_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA4_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.5. Выработка э/энергии по генератору №5
					</td>
					<td class='center'>
						6
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA5_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA5_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.6. Выработка э/энергии по генератору №6
					</td>
					<td class='center'>
						7
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA6_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA6_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.7. Выработка э/энергии по генератору №7
					</td>
					<td class='center'>
						8
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA7_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA7_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.8. Выработка э/энергии по генератору №8
					</td>
					<td class='center'>
						9
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA8_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA8_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.9. Выработка э/энергии по генератору №9
					</td>
					<td class='center'>
						10
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA9_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA9_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						1.10. Выработка э/энергии по генератору №10
					</td>
					<td class='center'>
						11
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[PiramidaRecords.P_GA10_Otd.Key]/1000.0).ToString("0") %>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[PiramidaRecords.P_GA10_Otd.Key]/1000.0).ToString("0") %>
					</td>
				</tr>
				<tr>
					<td class='left'>
						2. Сальдо перетоков
					</td>
					<td class='center'>
						12
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[ReportLinesRecords.P_VL110_Saldo.ID] / 1000.0 + 
						Model.ReportDay.ResultData[ReportLinesRecords.P_VL220_Saldo.ID] / 1000.0 + 
						Model.ReportDay.ResultData[ReportLinesRecords.P_VL500_Saldo.ID] / 1000.0+
						Model.ReportDay.ResultData[ReportLinesRecords.P_KL6_Saldo.ID] / 1000.0).ToString("0")%>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[ReportLinesRecords.P_VL110_Saldo.ID] / 1000.0 +
						Model.ReportMonth.ResultData[ReportLinesRecords.P_VL220_Saldo.ID] / 1000.0 +
						Model.ReportMonth.ResultData[ReportLinesRecords.P_VL500_Saldo.ID] / 1000.0 +
						Model.ReportMonth.ResultData[ReportLinesRecords.P_KL6_Saldo.ID] / 1000.0).ToString("0")%>
					</td>
				</tr>
				<tr>
					<td class='left'>
						2.1. Сальдо перетоков ФСК ЕЭС МЭС Урала (220кВ, 500кВ)
					</td>
					<td class='center'>
						13
					</td>
					<td>
						<%=(	Model.ReportDay.ResultData[ReportLinesRecords.P_VL220_Saldo.ID] / 1000.0 + 
						Model.ReportDay.ResultData[ReportLinesRecords.P_VL500_Saldo.ID] / 1000.0).ToString("0")%>
					</td>
					<td>
						<%=(	Model.ReportMonth.ResultData[ReportLinesRecords.P_VL220_Saldo.ID] / 1000.0 +
						Model.ReportMonth.ResultData[ReportLinesRecords.P_VL500_Saldo.ID] / 1000.0).ToString("0")%>
					</td>
				</tr>
				<tr>
					<td class='left'>
						2.2. Сальдо перетоков РСК ОАО "Пермэнерго" (110кВ и ниже)
					</td>
					<td class='center'>
						14
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[ReportLinesRecords.P_VL110_Saldo.ID] / 1000.0+
						Model.ReportDay.ResultData[ReportLinesRecords.P_KL6_Saldo.ID] / 1000.0).ToString("0")%>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[ReportLinesRecords.P_VL110_Saldo.ID] / 1000.0 +
						Model.ReportMonth.ResultData[ReportLinesRecords.P_KL6_Saldo.ID] / 1000.0).ToString("0")%>
					</td>
				</tr>
				<tr>
					<td class='left'>
						3.Потребление э/энергии, всего
					</td>
					<td class='center'>
						15
					</td>
					<td>
						<%=(Model.ReportDay.ResultData[ReportMainRecords.P_SP.ID] / 1000.0).ToString("0")%>
					</td>
					<td>
						<%=(Model.ReportMonth.ResultData[ReportMainRecords.P_SP.ID] / 1000.0).ToString("0")%>
					</td>
				</tr>
				<tr>
					<td class='left'>
						4.Отпуск тепла
					</td>
					<td class='center'>
						16
					</td>
					<td>
						&nbsp;
					</td>
					<td>
						&nbsp;
					</td>
				</tr>
	</table>
</body>
</html>
