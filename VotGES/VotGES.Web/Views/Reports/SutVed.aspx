﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.SutVedReport>" %>
<%@ Import Namespace="VotGES.Piramida.Report" %>
<%@ Import Namespace="VotGES.Piramida" %>
<%@ Import Namespace="VotGES" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SutVed</title>
	 <style>

	 	table,tr,td,p {
			font-family: 'Arial';
			font-size: 9pt;
		}
		
		h1,h2,h3,h4,h5,h6
		{
			padding:0;
			margin:0;
		}
		
		h1
		{
			font-family: 'Arial';
			font-size: 14pt;
		}
		
		h2
		{
			font-family: 'Arial';
			font-size: 12pt;
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
		}
		
		
		table.cifr
		{
			margin-top:5px;
		}

		table td.right,table th.right{
			text-align: right;
		}
	 </style>
</head>

<% int[] hours={0,1,5,10,16,19,22}; 
	//int[] hours= { 6,7,8,9,10};%>
<body>
	<h1>Суточная ведомость за <%=Model.DateStart.ToString("dd.MM.yyy")%></h1>
	<hr />		
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='2'>ГЭС</th>
			<th colspan='3'>U на шинах</th>
			<th colspan='3'>P 500 кВ</th>
			<th colspan='3'>Q 500 кВ</th>
			<th colspan='4'>Общестанционные</th>
			<th colspan='6'>Режим</th>
		</tr>
		<tr>
			<th>P</th>
			<th>Q</th>
			<th>110</th>
			<th>220</th>
			<th>500</th>
			<th>Емл</th>
			<th>Кар</th>
			<th>Вят</th>
			<th>Емл</th>
			<th>Кар</th>
			<th>Вят</th>
			<th>ВБ</th>
			<th>НБ</th>
			<th>Т</th>
			<th>Расх</th>
			<th>Час</th>
			<th>P<sub>план</sub></th>
			<th>P<sub>факт</sub></th>
			<th>Час</th>
			<th>P<sub>план</sub></th>
			<th>P<sub>факт</sub></th>
			
		</tr>
		<%		 
			DateTime dt=Model.Dates.First();
		 foreach (DateTime date in Model.Dates) {
		 if (date.Minute == 0) {
		 %>
			<tr>				
				<th><%=date.ToString("HH:mm")%></th>
				<td><%=Model[date, PiramidaRecords.MB_P_GES.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_Q_GES.Key].ToString("0.00")%></td>
				<td><%=Model[date, ReportMBRecords.MB_U_110.ID].ToString("0.00")%></td>
				<td><%=Model[date, ReportMBRecords.MB_U_220.ID].ToString("0.00")%></td>
				<td><%=Model[date, ReportMBRecords.MB_U_500.ID].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_P_Emelino_500.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_P_Karmanovo_500.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_P_Vyatka_500.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_Q_Emelino_500.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_Q_Karmanovo_500.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_Q_Vyatka_500.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_VB_Sgl.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_NB_Sgl.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_T.Key].ToString("0.00")%></td>
				<td><%=Model[date, PiramidaRecords.MB_Rashod.Key].ToString("0.00")%></td>
								
				<th><%=dt.ToString("HH:mm")%></th>
				<td><%=Model.PBR.HalfHoursPBR[dt].ToString("0.00")%></td>
				<td><%=Model.PBR.HalfHoursP[dt].ToString("0.00")%></td>
				
				<th><%=dt.AddHours(12).ToString("HH:mm")%></th>			
				<td><%=Model.PBR.HalfHoursPBR[dt.AddHours(12)].ToString("0.00")%></td>
				<td><%=Model.PBR.HalfHoursP[dt.AddHours(12)].ToString("0.00")%></td>
			</tr>
		<%dt = dt.AddMinutes(30);}			 
	 } %>
		<tr>
				<th>Среднее</th>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><%=Model.ResultData["VB_AVG"].ToString("0.00") %></td>
				<td><%=Model.ResultData["NB_AVG"].ToString("0.00") %></td>
				<td><%=Model.ResultData["T_AVG"].ToString("0.00") %></td>
				<td><%=Model.ResultData["RASHOD_AVG"].ToString("0.00") %></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<th>Итог</th>
				<td><%=(Model.PBR.PBRSum).ToString("0.00")%></td>
				<td><%=(Model.PBR.PSum).ToString("0.00")%></td>
			</tr>
	</table>
	<hr />	
	<h2>Генераторы</h2>
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='3'>Генератор №1</th>
			<th colspan='3'>Генератор №2</th>
			<th colspan='3'>Генератор №3</th>
			<th colspan='3'>Генератор №4</th>
			<th colspan='3'>Генератор №5</th>
		</tr>
		<tr>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
		</tr>
		<%foreach (DateTime date in Model.Dates) {
			if (hours.Contains(date.Hour) && date.Minute==0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_GA1_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA1_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA1_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA2_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA2_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA2_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA3_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA3_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA3_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA4_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA4_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA4_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA5_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA5_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA5_Irotor.Key].ToString("0.00")%></td>					
									

				</tr>
			<%}
		}%>
	</table>
		
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='3'>Генератор №6</th>
			<th colspan='3'>Генератор №7</th>
			<th colspan='3'>Генератор №8</th>
			<th colspan='3'>Генератор №9</th>
			<th colspan='3'>Генератор №10</th>
		</tr>
		<tr>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>I<sub>ст</sub></th>
			<th>I<sub>рот</sub></th>
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_GA6_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA6_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA6_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA7_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA7_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA7_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA8_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA8_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA8_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA9_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA9_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA9_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA10_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, ReportMBRecords.MB_GA10_Istator.ID].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA10_Irotor.Key].ToString("0.00")%></td>					
				</tr>
			<%}
		}%>
	</table>
	<hr />	
	<h2>ВЛ 110кВ</h2>
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='3'>КШТ-1</th>
			<th colspan='3'>КШТ-2</th>
			<th colspan='3'>Водозабор-1</th>
			<th colspan='3'>Водозабор-2</th>
			<th colspan='3'>Светлая</th>
			<th colspan='3'>ОВВ</th>
			<th colspan='2'>1СШ</th>
			<th colspan='2'>2СШ</th>
		</tr>
		<tr>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>U</th>
			<th>F</th>
			<th>U</th>
			<th>F</th>				
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_P_KSHT1_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_KSHT1_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_KSHT1_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_KSHT2_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_KSHT2_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_KSHT2_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Vodozabor1_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Vodozabor1_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Vodozabor1_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Vodozabor2_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Vodozabor2_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Vodozabor2_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Svetlaya_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Svetlaya_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Svetlaya_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_OVV_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_OVV_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_OVV_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_1SH_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_1SH_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_2SH_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_2SH_110.Key].ToString("0.00")%></td>				
				</tr>
			<%}
		}%>
	</table>    
		
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='3'>ВВ 1Т</th>
			<th colspan='3'>5,6 АТ</th>
			<th colspan='3'>Дубовая</th>
			<th colspan='3'>ЧаТЭЦ</th>
			<th colspan='3'>Березовка</th>
			<th colspan='3'>Ивановка</th>
			<th colspan='3'>Каучук</th>
			<th colspan='1'>ШСВ</th>
		</tr>
		<tr>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>I</th>			
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_P_1T_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_1T_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_1T_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_56AT_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_56AT_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_56AT_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Dubovaya_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Dubovaya_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Dubovaya_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_TEC_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_TEC_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_TEC_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Berezovka_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Berezovka_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Berezovka_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Ivanovka_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Ivanovka_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Ivanovka_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Kauchuk_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Kauchuk_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Kauchuk_110.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_SHSV_110.Key].ToString("0.00")%></td>					
				</tr>
			<%}
		}%>
	</table>    

	<hr />	
	<h2>ВЛ 220кВ</h2>
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='3'>Иж-1</th>
			<th colspan='3'>Иж-2</th>
			<th colspan='3'>Каучук-1</th>
			<th colspan='3'>Каучук-2</th>
			<th colspan='3'>Светлая</th>
			<th colspan='3'>ОВВ</th>
			<th colspan='2'>1СШ</th>
			<th colspan='2'>2СШ</th>
		</tr>
		<tr>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>U</th>
			<th>F</th>
			<th>U</th>
			<th>F</th>				
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_P_Izhevsk1_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Izhevsk1_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Izhevsk1_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Izhevsk2_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Izhevsk2_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Izhevsk2_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Kauchuk1_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Kauchuk1_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Kauchuk1_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Kauchuk2_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Kauchuk2_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Kauchuk2_220.Key].ToString("0.00")%></td>						

					<td><%=Model[date, PiramidaRecords.MB_P_Svetlaya_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Svetlaya_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Svetlaya_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_OVV_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_OVV_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_OVV_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_1SH_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_1SH_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_2SH_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_2SH_220.Key].ToString("0.00")%></td>				
				</tr>
			<%}
		}%>
	</table>    
		
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='3'>2АТ</th>
			<th colspan='3'>3АТ</th>
			<th colspan='3'>4Т</th>
			<th colspan='3'>5,6АТ</th>				
			<th colspan='1'>ШСВ</th>
		</tr>
		<tr>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>				
			<th>I</th>			
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_P_2AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_2AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_2AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_3AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_3AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_3AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_4T_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_4T_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_4T_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_56AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_56AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_56AT_220.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_SHSV_220.Key].ToString("0.00")%></td>					
				</tr>
			<%}
		}%>
	</table>    

	<hr />	
	<h2>ВЛ 500кВ</h2>
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Час</th>
			<th colspan='5'>Емелино</th>
			<th colspan='5'>Карманово</th>
			<th colspan='5'>Вятка</th>
			<th colspan='3'>2АТ</th>
			<th colspan='3'>3АТ</th>
		</tr>
		<tr>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>U</th>
			<th>F</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>U</th>
			<th>F</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>U</th>
			<th>F</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>
			<th>P</th>
			<th>Q</th>
			<th>I</th>				
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_P_Emelino_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Emelino_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Emelino_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_Emelino_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_Emelino_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Karmanovo_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Karmanovo_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Karmanovo_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_Karmanovo_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_Karmanovo_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_Vyatka_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_Vyatka_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_Vyatka_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_U_Vyatka_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_F_Vyatka_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_2AT_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_2AT_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_2AT_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_P_3AT_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_Q_3AT_500.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_I_3AT_500.Key].ToString("0.00")%></td>
				</tr>
			<%}
		}%>
	</table>    
    
</body>
</html>

