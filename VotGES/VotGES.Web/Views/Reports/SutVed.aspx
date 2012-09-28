<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.SutVedReport>" %>
<%@ Import Namespace="VotGES.Piramida.Report" %>
<%@ Import Namespace="VotGES.Piramida" %>
<%@ Import Namespace="VotGES" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SutVed</title>
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

<% int[] hours={1,5,10,16,19,22}; 
	//int[] hours= { 6,7,8,9,10};%>
<body>			
	<table>
	<tr>
		<td valign='top'>			
			<table class='cifr'>			
			<tr>
				<th colspan='17'>Суточная ведомость за <%=Model.DateStart.AddHours(Model.AddHours).ToString("dd.MM.yyyy")%></th>
			</tr>
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
			</tr>
			<%		 
			 foreach (DateTime date in Model.Dates) {
			 if (date.Minute == 0) {
			 %>
				<tr>				
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
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
				</tr>
			<%}			 
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
					<th><%=Model.ResultData["VB_AVG"].ToString("0.00") %></th>
					<th><%=Model.ResultData["NB_AVG"].ToString("0.00") %></th>
					<th><%=Model.ResultData["T_AVG"].ToString("0.00") %></th>
					<th><%=Model.ResultData["RASHOD_AVG"].ToString("0.00") %></th>
				</tr>
			</table>
		</td>
		<td valign='top'>
			<table class='cifr'>
				<tr>
					<th colspan='6'>Режим станции</th>
				</tr>
				<tr>
					<th>Час</th>
					<th>P<sub>план</sub></th>
					<th>P<sub>факт</sub></th>
					<th>Час</th>
					<th>P<sub>план</sub></th>
					<th>P<sub>факт</sub></th>
			
				</tr>
				<%		 
					int index=0;
				 foreach (DateTime date in Model.PBR.HalfHoursPBR.Keys) {
				 if (index<24) {
				 %>
					<tr>								
						<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
						<td><%=Model.PBR.RealPBR[date].ToString("0.00")%></td>
						<td><%=Model.Dates.Contains(date)?Model[date, PiramidaRecords.MB_P_GES.Key].ToString("0.00"):"-"%></td>
				
						<th><%=date.AddHours(Model.AddHours).AddHours(12).ToString("HH:mm")%></th>			
						<td><%=Model.PBR.RealPBR[date.AddHours(12)].ToString("0.00")%></td>
						<td><%=Model.Dates.Contains(date.AddHours(12))?Model[date.AddHours(12), PiramidaRecords.MB_P_GES.Key].ToString("0.00"):"-"%></td>
					</tr>
				<%index++; }			 
			 } %>
				<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<th>Итог</th>
						<th><%=(Model.PBR.PBRSum).ToString("0.00")%></th>
						<th><%=(Model.PBR.PSum).ToString("0.00")%></th>
					</tr>
				</table>
		</td>
		<td valign='top'>
			<table class='cifr'>
				<tr>
					<th colspan='2'>P<sub>зад</sub></th>
				</tr>
				<tr>
					<th>Час</th>
					<th>P<sub>зад</sub></th>
				</tr>
				<tr>
					<th>Пред</th>
					<td><%=Model.LastP.ToString("0.00") %></td>
				</tr>
				<%foreach (KeyValuePair<DateTime,double>de in Model.PZad){ %>
				<tr>
					<th><%=de.Key.AddHours(Model.AddHours).ToString("HH:mm:ss")%></th>
					<td><%=de.Value.ToString("0.00") %></td>
				</tr>
				<%} %>
			</td>
		</table>
	</tr>
	
	
	</table>

	<hr />	
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
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_GA1_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA1_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA1_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA2_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA2_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA2_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA3_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA3_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA3_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA4_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA4_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA4_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA5_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA5_Q.Key].ToString("0.00")%></td>
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
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
			<th>P</th>
			<th>Q</th>
			<th>I<sub>рот</sub></th>
		</tr>
		<%foreach (DateTime date in Model.Dates) {
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
					<td><%=Model[date, PiramidaRecords.MB_GA6_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA6_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA6_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA7_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA7_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA7_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA8_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA8_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA8_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA9_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA9_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA9_Irotor.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA10_P.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA10_Q.Key].ToString("0.00")%></td>
					<td><%=Model[date, PiramidaRecords.MB_GA10_Irotor.Key].ToString("0.00")%></td>					
				</tr>
			<%}
		}%>
	</table>

	<hr />	
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
			<th>Пусков/Осановов</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.PuskStop.Data[ga].CountPusk.ToString("0") %>/
					<%=Model.PuskStop.Data[ga].CountStop.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.PuskStop.SumRecord.CountPusk.ToString("0") %> /
				<%=Model.PuskStop.SumRecord.CountStop.ToString("0") %>
			</td>
		</tr>
		<tr>
			<th>Пусков-переводов в ГР / СК</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.PuskStop.Data[ga].CountPuskGen.ToString("0") %> /
					<%=Model.PuskStop.Data[ga].CountPuskSK.ToString("0")%>
				</td>
			<%} %>
			<td>
				<%=Model.PuskStop.SumRecord.CountPuskGen.ToString("0") %> /
				<%=Model.PuskStop.SumRecord.CountPuskSK.ToString("0") %>
			</td>
		</tr>
		<tr>
			<th>Время работы/простоя</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.PuskStop.Data[ga].HoursWork.ToString("0.00") %> /
					<%=Model.PuskStop.Data[ga].HoursStay.ToString("0.00")%>
				</td>
			<%} %>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<th>Время работы в ГР / СК</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.PuskStop.Data[ga].HoursGen.ToString("0.00") %> /
					<%=Model.PuskStop.Data[ga].HoursSK.ToString("0.00")%>
				</td>
			<%} %>
			<td>&nbsp;</td>
		</tr>


		
	</table>




	<hr style="page-break-before: always"/>	
	<table class='cifr'>
		<tr>
			<th>110кВ</th>
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
			<th>Час</th>
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
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
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
			<th>110кВ</th>
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
			<th>Час</th>
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
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
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
	<table class='cifr'>
		<tr>
			<th>220кВ</th>
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
			<th>Час</th>
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
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
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
			<th>220кВ</th>
			<th colspan='3'>2АТ</th>
			<th colspan='3'>3АТ</th>
			<th colspan='3'>4Т</th>
			<th colspan='3'>5,6АТ</th>				
			<th colspan='1'>ШСВ</th>
		</tr>
		<tr>
			<th>Час</th>
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
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
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
	<table class='cifr'>
		<tr>
			<th>500кВ</th>
			<th colspan='5'>Емелино</th>
			<th colspan='5'>Карманово</th>
			<th colspan='5'>Вятка</th>
			<th colspan='3'>2АТ</th>
			<th colspan='3'>3АТ</th>
		</tr>
		<tr>
			<th>Час</th>
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
		 if (hours.Contains(date.AddHours(Model.AddHours).Hour) && date.Minute == 0) {
			%>				
				<tr>
					<th><%=date.AddHours(Model.AddHours).ToString("HH:mm")%></th>
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

