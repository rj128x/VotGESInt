using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida
{
	public static class PiramidaRecords
	{
		public static PiramidaRecord P_GES=new PiramidaRecord(2, 0, 1, "P ГЭС");
		public static PiramidaRecord P_GTP1=new PiramidaRecord(2, 0, 2, "P ГТП1");
		public static PiramidaRecord P_GTP2=new PiramidaRecord(2, 0, 3, "P ГТП2");

		public static PiramidaRecord P_IKM_SN=new PiramidaRecord(2, 0, 14, "Расход на собственные нужды ГЭС");
		public static PiramidaRecord P_IKM_Nebalans_GES=new PiramidaRecord(2, 0, 24, "Небаланс по ГЭС");		
		public static PiramidaRecord P_IKM_SP=new PiramidaRecord(2, 0, 52, "Собственное потребление");



		public static PiramidaRecord P_3AT_500_Priem=new PiramidaRecord(0, 8739, 1, "3АТ 500 кВ Прием (P)");
		public static PiramidaRecord P_3AT_500_Otd=new PiramidaRecord(0, 8739, 2, "3АТ 500 кВ Отдача (P)");
		public static PiramidaRecord Q_3AT_500_Priem=new PiramidaRecord(0, 8739, 3, "3АТ 500 кВ Прием (Q)");
		public static PiramidaRecord Q_3AT_500_Otd=new PiramidaRecord(0, 8739, 4, "3АТ 500 кВ Отдача (Q)");

		public static PiramidaRecord P_2AT_500_Priem=new PiramidaRecord(0, 8739, 5, "2АТ 500 кВ Прием (P)");
		public static PiramidaRecord P_2AT_500_Otd=new PiramidaRecord(0, 8739, 6, "2АТ 500 кВ Отдача (P)");
		public static PiramidaRecord Q_2AT_500_Priem=new PiramidaRecord(0, 8739, 7, "2АТ 500 кВ Прием (Q)");
		public static PiramidaRecord Q_2AT_500_Otd=new PiramidaRecord(0, 8739, 8, "2АТ 500 кВ Отдача (Q)");

		public static PiramidaRecord P_SN_20NDS1_Priem=new PiramidaRecord(0, 8739, 9, "СН 20НДС-1 прием (P)");
		public static PiramidaRecord Q_SN_20NDS1_Priem=new PiramidaRecord(0, 8739, 10, "СН 20НДС-1 прием (Q)");
		public static PiramidaRecord P_1KU_K2_Priem=new PiramidaRecord(0, 8739, 11, "Компрессор-2 1КУ прием (P)");
		public static PiramidaRecord Q_1KU_K2_Priem=new PiramidaRecord(0, 8739, 12, "Компрессор-2 1КУ прием (Q)");
		public static PiramidaRecord P_1KU_31T_Priem=new PiramidaRecord(0, 8739, 13, "31Т 1КУ прием (P)");
		public static PiramidaRecord Q_1KU_31T_Priem=new PiramidaRecord(0, 8739, 14, "31Т 1КУ прием (Q)");


		public static PiramidaRecord P_SN_29T_Priem=new PiramidaRecord(0, 8739, 15, "СН 29Т прием (P)");
		public static PiramidaRecord P_SN_27T_Priem=new PiramidaRecord(0, 8739, 17, "СН 27Т прием (P)");
		public static PiramidaRecord P_SN_22T_Priem=new PiramidaRecord(0, 8739, 18, "СН 22Т прием (P)");
		public static PiramidaRecord P_SN_30T_31T_Priem=new PiramidaRecord(0, 8739, 19, "СН 30Т 31Т прием (P)");
		public static PiramidaRecord P_SN_25T_37T_Priem=new PiramidaRecord(0, 8739, 20, "СН 25Т 37Т прием (P)");
		public static PiramidaRecord P_SN_33T_Priem=new PiramidaRecord(0, 8739, 21, "СН 33Т прием (P)");
		public static PiramidaRecord P_SN_21T_Priem=new PiramidaRecord(0, 8739, 22, "СН 21Т прием (P)");
		public static PiramidaRecord P_SN_34T_Priem=new PiramidaRecord(0, 8739, 23, "СН 34Т прием (P)");

		public static PiramidaRecord P_1KU_K5_Priem=new PiramidaRecord(0, 8739, 25, "Компрессор-5 1КУ прием (P)");
		public static PiramidaRecord Q_1KU_K5_Priem=new PiramidaRecord(0, 8739, 26, "Компрессор-5 1КУ прием (Q)");

		public static PiramidaRecord P_SN_32T_Priem=new PiramidaRecord(0, 8739, 27, "СН 32Т прием (P)");

		public static PiramidaRecord P_SN_20NDS2_Priem=new PiramidaRecord(0, 8739, 28, "СН 20НДС-2 прием (P)");
		public static PiramidaRecord Q_SN_20NDS2_Priem=new PiramidaRecord(0, 8739, 29, "СН 20НДС-2 прием (Q)");

		public static PiramidaRecord P_SN_23T_Priem=new PiramidaRecord(0, 8739, 30, "СН 23Т прием (P)");
		public static PiramidaRecord P_SN_28T_Priem=new PiramidaRecord(0, 8739, 31, "СН 28Т прием (P)");
		public static PiramidaRecord P_SN_35T_Priem=new PiramidaRecord(0, 8739, 32, "СН 35Т прием (P)");
		public static PiramidaRecord P_SN_36T_Priem=new PiramidaRecord(0, 8739, 33, "СН 36Т прием (P)");
		public static PiramidaRecord P_SN_38T_Priem=new PiramidaRecord(0, 8739, 34, "СН 38Т прием (P)");

		public static PiramidaRecord P_SN_TVI_Priem=new PiramidaRecord(0, 8739, 35, "СН ТВИ прием (P)");
		public static PiramidaRecord Q_SN_TVI_Priem=new PiramidaRecord(0, 8739, 36, "СН ТВИ прием (Q)");

		public static PiramidaRecord P_SN_24T_Priem=new PiramidaRecord(0, 8739, 37, "СН 24Т прием (P)");

		public static PiramidaRecord P_Vozb_GA9_Priem=new PiramidaRecord(0, 8740, 1, "Возбуждение Г/А 9 прием (P)");
		public static PiramidaRecord P_Vozb_GA10_Priem=new PiramidaRecord(0, 8740, 2, "Возбуждение Г/А 10 прием (P)");
		public static PiramidaRecord P_SN_19T_Priem=new PiramidaRecord(0, 8740, 3, "СН 19Т прием (P)");
		public static PiramidaRecord P_SN_20T_Priem=new PiramidaRecord(0, 8740, 4, "СН 20Т прием (P)");
		public static PiramidaRecord P_Vozb_GA7_Priem=new PiramidaRecord(0, 8740, 5, "Возбуждение Г/А 7 прием (P)");
		public static PiramidaRecord P_Vozb_GA8_Priem=new PiramidaRecord(0, 8740, 6, "Возбуждение Г/А 8 прием (P)");
		public static PiramidaRecord P_SN_17T_Priem=new PiramidaRecord(0, 8740, 7, "СН 17Т прием (P)");
		public static PiramidaRecord P_SN_18T_Priem=new PiramidaRecord(0, 8740, 8, "СН 28Т прием (P)");

		public static PiramidaRecord P_1T_110_Priem=new PiramidaRecord(0, 8740, 9, "1Т 110 кВ Прием (P)");
		public static PiramidaRecord P_1T_110_Otd=new PiramidaRecord(0, 8740, 10, "1Т 110 кВ Отдача (P)");
		public static PiramidaRecord Q_1T_110_Priem=new PiramidaRecord(0, 8740, 11, "1Т 110 кВ Прием (Q)");
		public static PiramidaRecord Q_1T_110_Otd=new PiramidaRecord(0, 8740, 12, "1Т 110 кВ Отдача (Q)");

		public static PiramidaRecord P_2AT_220_Priem=new PiramidaRecord(0, 8740, 13, "2АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_2AT_220_Otd=new PiramidaRecord(0, 8740, 14, "2АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_2AT_220_Priem=new PiramidaRecord(0, 8740, 15, "2АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_2AT_220_Otd=new PiramidaRecord(0, 8740, 16, "2АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_3AT_220_Priem=new PiramidaRecord(0, 8740, 17, "3АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_3AT_220_Otd=new PiramidaRecord(0, 8740, 18, "3АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_3AT_220_Priem=new PiramidaRecord(0, 8740, 19, "3АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_3AT_220_Otd=new PiramidaRecord(0, 8740, 20, "3АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_56AT_220_Priem=new PiramidaRecord(0, 8740, 21, "5-6АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_56AT_220_Otd=new PiramidaRecord(0, 8740, 22, "5-6АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_56AT_220_Priem=new PiramidaRecord(0, 8740, 23, "5-6АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_56AT_220_Otd=new PiramidaRecord(0, 8740, 24, "5-6АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_4T_220_Priem=new PiramidaRecord(0, 8740, 25, "4Т 220 кВ Прием (P)");
		public static PiramidaRecord P_4T_220_Otd=new PiramidaRecord(0, 8740, 26, "4Т 220 кВ Отдача (P)");
		public static PiramidaRecord Q_4T_220_Priem=new PiramidaRecord(0, 8740, 27, "4Т 220 кВ Прием (Q)");
		public static PiramidaRecord Q_4T_220_Otd=new PiramidaRecord(0, 8740, 28, "4Т 220 кВ Отдача (Q)");

		public static PiramidaRecord P_Vozb_GA5_Priem=new PiramidaRecord(0, 8740, 29, "Возбуждение Г/А 5 прием (P)");
		public static PiramidaRecord P_Vozb_GA6_Priem=new PiramidaRecord(0, 8740, 30, "Возбуждение Г/А 6 прием (P)");
		public static PiramidaRecord P_SN_15T_Priem=new PiramidaRecord(0, 8740, 31, "СН 15Т прием (P)");
		public static PiramidaRecord P_SN_16T_Priem=new PiramidaRecord(0, 8740, 32, "СН 26Т прием (P)");
		public static PiramidaRecord P_SN_8T_Priem=new PiramidaRecord(0, 8740, 33, "СН 8Т прием (P)");

		public static PiramidaRecord P_Vozb_GA1_Priem=new PiramidaRecord(0, 8740, 34, "Возбуждение Г/А 1 прием (P)");
		public static PiramidaRecord P_Vozb_GA2_Priem=new PiramidaRecord(0, 8740, 35, "Возбуждение Г/А 2 прием (P)");
		public static PiramidaRecord P_SN_11T_Priem=new PiramidaRecord(0, 8740, 36, "СН 11Т прием (P)");
		public static PiramidaRecord P_SN_12T_Priem=new PiramidaRecord(0, 8740, 37, "СН 12Т прием (P)");
		public static PiramidaRecord P_SN_7T_Priem=new PiramidaRecord(0, 8740, 38, "СН 7Т прием (P)");

		public static PiramidaRecord P_Vozb_GA3_Priem=new PiramidaRecord(0, 8740, 39, "Возбуждение Г/А 3 прием (P)");
		public static PiramidaRecord P_Vozb_GA4_Priem=new PiramidaRecord(0, 8740, 40, "Возбуждение Г/А 4 прием (P)");
		public static PiramidaRecord P_SN_13T_Priem=new PiramidaRecord(0, 8740, 41, "СН 13Т прием (P)");
		public static PiramidaRecord P_SN_14T_Priem=new PiramidaRecord(0, 8740, 42, "СН 14Т прием (P)");

		public static PiramidaRecord P_56AT_110_Priem=new PiramidaRecord(0, 8740, 43, "5-6АТ 110 кВ Прием (P)");
		public static PiramidaRecord P_56AT_110_Otd=new PiramidaRecord(0, 8740, 44, "5-6АТ 110 кВ Отдача (P)");
		public static PiramidaRecord Q_56AT_110_Priem=new PiramidaRecord(0, 8740, 45, "5-6АТ 110 кВ Прием (Q)");
		public static PiramidaRecord Q_56AT_110_Otd=new PiramidaRecord(0, 8740, 46, "5-6АТ 110 кВ Отдача (Q)");

		public static PiramidaRecord P_VL110_Svetlaya_Priem=new PiramidaRecord(0, 8737, 1, "ВЛ 110 Светлая прием (P)");
		public static PiramidaRecord P_VL110_Svetlaya_Otd=new PiramidaRecord(0, 8737, 2, "ВЛ 110 Светлая отдача (P)");
		public static PiramidaRecord Q_VL110_Svetlaya_Priem=new PiramidaRecord(0, 8737, 3, "ВЛ 110 Светлая прием (Q)");
		public static PiramidaRecord Q_VL110_Svetlaya_Otd=new PiramidaRecord(0, 8737, 4, "ВЛ 110 Светлая отдача (Q)");

		public static PiramidaRecord P_VL110_Ivanovka_Priem=new PiramidaRecord(0, 8737, 5, "ВЛ 110 Ивановка прием (P)");
		public static PiramidaRecord P_VL110_Ivanovka_Otd=new PiramidaRecord(0, 8737, 6, "ВЛ 110 Ивановка отдача (P)");
		public static PiramidaRecord Q_VL110_Ivanovka_Priem=new PiramidaRecord(0, 8737, 7, "ВЛ 110 Ивановка прием (Q)");
		public static PiramidaRecord Q_VL110_Ivanovka_Otd=new PiramidaRecord(0, 8737, 8, "ВЛ 110 Ивановка отдача (Q)");

		public static PiramidaRecord P_VL110_Kauchuk_Priem=new PiramidaRecord(0, 8737, 9, "ВЛ 110 Каучук прием (P)");
		public static PiramidaRecord P_VL110_Kauchuk_Otd=new PiramidaRecord(0, 8737, 10, "ВЛ 110 Каучук отдача (P)");
		public static PiramidaRecord Q_VL110_Kauchuk_Priem=new PiramidaRecord(0, 8737, 11, "ВЛ 110 Каучук прием (Q)");
		public static PiramidaRecord Q_VL110_Kauchuk_Otd=new PiramidaRecord(0, 8737, 12, "ВЛ 110 Каучук отдача (Q)");

		public static PiramidaRecord P_VL110_TEC_Priem=new PiramidaRecord(0, 8737, 13, "ВЛ 110 ЧаТЭЦ прием (P)");
		public static PiramidaRecord P_VL110_TEC_Otd=new PiramidaRecord(0, 8737, 14, "ВЛ 110 ЧаТЭЦ отдача (P)");
		public static PiramidaRecord Q_VL110_TEC_Priem=new PiramidaRecord(0, 8737, 15, "ВЛ 110 ЧаТЭЦ прием (Q)");
		public static PiramidaRecord Q_VL110_TEC_Otd=new PiramidaRecord(0, 8737, 16, "ВЛ 110 ЧаТЭЦ отдача (Q)");

		public static PiramidaRecord P_VL110_Berezovka_Priem=new PiramidaRecord(0, 8737, 17, "ВЛ 110 Березовка прием (P)");
		public static PiramidaRecord P_VL110_Berezovka_Otd=new PiramidaRecord(0, 8737, 18, "ВЛ 110 Березовка отдача (P)");
		public static PiramidaRecord Q_VL110_Berezovka_Priem=new PiramidaRecord(0, 8737, 19, "ВЛ 110 Березовка прием (Q)");
		public static PiramidaRecord Q_VL110_Berezovka_Otd=new PiramidaRecord(0, 8737, 20, "ВЛ 110 Березовка отдача (Q)");

		public static PiramidaRecord P_VL220_Svetlaya_Priem=new PiramidaRecord(0, 8737, 21, "ВЛ 220 Светлая прием (P)");
		public static PiramidaRecord P_VL220_Svetlaya_Otd=new PiramidaRecord(0, 8737, 22, "ВЛ 220 Светлая отдача (P)");
		public static PiramidaRecord Q_VL220_Svetlaya_Priem=new PiramidaRecord(0, 8737, 23, "ВЛ 220 Светлая прием (Q)");
		public static PiramidaRecord Q_VL220_Svetlaya_Otd=new PiramidaRecord(0, 8737, 24, "ВЛ 220 Светлая отдача (Q)");

		public static PiramidaRecord P_VL220_Kauchuk1_Priem=new PiramidaRecord(0, 8737, 25, "ВЛ 220 Каучук-1 прием (P)");
		public static PiramidaRecord P_VL220_Kauchuk1_Otd=new PiramidaRecord(0, 8737, 26, "ВЛ 220 Каучук-1 отдача (P)");
		public static PiramidaRecord Q_VL220_Kauchuk1_Priem=new PiramidaRecord(0, 8737, 27, "ВЛ 220 Каучук-1 прием (Q)");
		public static PiramidaRecord Q_VL220_Kauchuk1_Otd=new PiramidaRecord(0, 8737, 28, "ВЛ 220 Каучук-1 отдача (Q)");

		public static PiramidaRecord P_VL220_Kauchuk2_Priem=new PiramidaRecord(0, 8737, 29, "ВЛ 220 Каучук-2 прием (P)");
		public static PiramidaRecord P_VL220_Kauchuk2_Otd=new PiramidaRecord(0, 8737, 30, "ВЛ 220 Каучук-2 отдача (P)");
		public static PiramidaRecord Q_VL220_Kauchuk2_Priem=new PiramidaRecord(0, 8737, 31, "ВЛ 220 Каучук-2 прием (Q)");
		public static PiramidaRecord Q_VL220_Kauchuk2_Otd=new PiramidaRecord(0, 8737, 32, "ВЛ 220 Каучук-2 отдача (Q)");

		public static PiramidaRecord P_VL220_Izhevsk1_Priem=new PiramidaRecord(0, 8737, 33, "ВЛ 220 Ижевск-1 прием (P)");
		public static PiramidaRecord P_VL220_Izhevsk1_Otd=new PiramidaRecord(0, 8737, 34, "ВЛ 220 Ижевск-1 отдача (P)");
		public static PiramidaRecord Q_VL220_Izhevsk1_Priem=new PiramidaRecord(0, 8737, 35, "ВЛ 220 Ижевск-1 прием (Q)");
		public static PiramidaRecord Q_VL220_Izhevsk1_Otd=new PiramidaRecord(0, 8737, 36, "ВЛ 220 Ижевск-1 отдача (Q)");

		public static PiramidaRecord P_VL220_Izhevsk2_Priem=new PiramidaRecord(0, 8737, 37, "ВЛ 220 Ижевск-2 прием (P)");
		public static PiramidaRecord P_VL220_Izhevsk2_Otd=new PiramidaRecord(0, 8737, 38, "ВЛ 220 Ижевск-2 отдача (P)");
		public static PiramidaRecord Q_VL220_Izhevsk2_Priem=new PiramidaRecord(0, 8737, 39, "ВЛ 220 Ижевск-2 прием (Q)");
		public static PiramidaRecord Q_VL220_Izhevsk2_Otd=new PiramidaRecord(0, 8737, 40, "ВЛ 220 Ижевск-2 отдача (Q)");

		public static PiramidaRecord P_VL110_KSHT1_Priem=new PiramidaRecord(0, 8737, 41, "ВЛ 110 КШТ-1 прием (P)");
		public static PiramidaRecord P_VL110_KSHT1_Otd=new PiramidaRecord(0, 8737, 42, "ВЛ 110 КШТ-1 отдача (P)");
		public static PiramidaRecord Q_VL110_KSHT1_Priem=new PiramidaRecord(0, 8737, 43, "ВЛ 110 КШТ-1 прием (Q)");
		public static PiramidaRecord Q_VL110_KSHT1_Otd=new PiramidaRecord(0, 8737, 44, "ВЛ 110 КШТ-1 отдача (Q)");

		public static PiramidaRecord P_VL110_KSHT2_Priem=new PiramidaRecord(0, 8737, 45, "ВЛ 110 КШТ-2 прием (P)");
		public static PiramidaRecord P_VL110_KSHT2_Otd=new PiramidaRecord(0, 8737, 46, "ВЛ 110 КШТ-2 отдача (P)");
		public static PiramidaRecord Q_VL110_KSHT2_Priem=new PiramidaRecord(0, 8737, 47, "ВЛ 110 КШТ-2 прием (Q)");
		public static PiramidaRecord Q_VL110_KSHT2_Otd=new PiramidaRecord(0, 8737, 48, "ВЛ 110 КШТ-2 отдача (Q)");

		public static PiramidaRecord P_VL110_Dubovaya_Priem=new PiramidaRecord(0, 8737, 49, "ВЛ 110 Дубовая прием (P)");
		public static PiramidaRecord P_VL110_Dubovaya_Otd=new PiramidaRecord(0, 8737, 50, "ВЛ 110 Дубовая отдача (P)");
		public static PiramidaRecord Q_VL110_Dubovaya_Priem=new PiramidaRecord(0, 8737, 51, "ВЛ 110 Дубовая прием (Q)");
		public static PiramidaRecord Q_VL110_Dubovaya_Otd=new PiramidaRecord(0, 8737, 52, "ВЛ 110 Дубовая отдача (Q)");

		public static PiramidaRecord P_VL110_Vodozabor2_Priem=new PiramidaRecord(0, 8737, 53, "ВЛ 110 Водозабор-2 прием (P)");
		public static PiramidaRecord P_VL110_Vodozabor2_Otd=new PiramidaRecord(0, 8737, 54, "ВЛ 110 Водозабор-2 отдача (P)");
		public static PiramidaRecord Q_VL110_Vodozabor2_Priem=new PiramidaRecord(0, 8737, 55, "ВЛ 110 Водозабор-2 прием (Q)");
		public static PiramidaRecord Q_VL110_Vodozabor2_Otd=new PiramidaRecord(0, 8737, 56, "ВЛ 110 Водозабор-2 отдача (Q)");

		public static PiramidaRecord P_VL110_Vodozabor1_Priem=new PiramidaRecord(0, 8737, 57, "ВЛ 110 Водозабор-1 прием (P)");
		public static PiramidaRecord P_VL110_Vodozabor1_Otd=new PiramidaRecord(0, 8737, 58, "ВЛ 110 Водозабор-1 отдача (P)");
		public static PiramidaRecord Q_VL110_Vodozabor1_Priem=new PiramidaRecord(0, 8737, 59, "ВЛ 110 Водозабор-1 прием (Q)");
		public static PiramidaRecord Q_VL110_Vodozabor1_Otd=new PiramidaRecord(0, 8737, 60, "ВЛ 110 Водозабор-1 отдача (Q)");

		public static PiramidaRecord P_VL500_Emelino_Priem=new PiramidaRecord(0, 8737, 61, "ВЛ 500 Емелино прием (P)");
		public static PiramidaRecord P_VL500_Emelino_Otd=new PiramidaRecord(0, 8737, 62, "ВЛ 500 Емелино отдача (P)");
		public static PiramidaRecord Q_VL500_Emelino_Priem=new PiramidaRecord(0, 8737, 63, "ВЛ 500 Емелино прием (Q)");
		public static PiramidaRecord Q_VL500_Emelino_Otd=new PiramidaRecord(0, 8737, 64, "ВЛ 500 Емелино отдача (Q)");

		public static PiramidaRecord P_VL500_Karmanovo_Priem=new PiramidaRecord(0, 8737, 65, "ВЛ 500 Карманово прием (P)");
		public static PiramidaRecord P_VL500_Karmanovo_Otd=new PiramidaRecord(0, 8737, 66, "ВЛ 500 Карманово отдача (P)");
		public static PiramidaRecord Q_VL500_Karmanovo_Priem=new PiramidaRecord(0, 8737, 67, "ВЛ 500 Карманово прием (Q)");
		public static PiramidaRecord Q_VL500_Karmanovo_Otd=new PiramidaRecord(0, 8737, 68, "ВЛ 500 Карманово отдача (Q)");

		public static PiramidaRecord P_VL500_Vyatka_Priem=new PiramidaRecord(0, 8737, 69, "ВЛ 500 Вятка прием (P)");
		public static PiramidaRecord P_VL500_Vyatka_Otd=new PiramidaRecord(0, 8737, 70, "ВЛ 500 Вятка отдача (P)");
		public static PiramidaRecord Q_VL500_Vyatka_Priem=new PiramidaRecord(0, 8737, 71, "ВЛ 500 Вятка прием (Q)");
		public static PiramidaRecord Q_VL500_Vyatka_Otd=new PiramidaRecord(0, 8737, 72, "ВЛ 500 Вятка отдача (Q)");

		public static PiramidaRecord P_GA1_Priem=new PiramidaRecord(0, 8738, 1, "Генератор-1 прием (P)");
		public static PiramidaRecord P_GA1_Otd=new PiramidaRecord(0, 8738, 2, "Генератор-1 отдача (P)");
		public static PiramidaRecord Q_GA1_Priem=new PiramidaRecord(0, 8738, 3, "Генератор-1 прием (Q)");
		public static PiramidaRecord Q_GA1_Otd=new PiramidaRecord(0, 8738, 4, "Генератор-1 отдача (Q)");

		public static PiramidaRecord P_GA2_Priem=new PiramidaRecord(0, 8738, 5, "Генератор-2 прием (P)");
		public static PiramidaRecord P_GA2_Otd=new PiramidaRecord(0, 8738, 6, "Генератор-2 отдача (P)");
		public static PiramidaRecord Q_GA2_Priem=new PiramidaRecord(0, 8738, 7, "Генератор-2 прием (Q)");
		public static PiramidaRecord Q_GA2_Otd=new PiramidaRecord(0, 8738, 8, "Генератор-2 отдача (Q)");

		public static PiramidaRecord P_KL6_Shluz1_Priem=new PiramidaRecord(0, 8738, 9, "КЛ 6 Шлюз-1 прием (P)");
		public static PiramidaRecord P_KL6_Shluz1_Otd=new PiramidaRecord(0, 8738, 10, "КЛ 6 Шлюз-1 отдача (P)");
		public static PiramidaRecord Q_KL6_Shluz1_Priem=new PiramidaRecord(0, 8738, 11, "КЛ 6 Шлюз-1 прием (Q)");
		public static PiramidaRecord Q_KL6_Shluz1_Otd=new PiramidaRecord(0, 8738, 12, "КЛ 6 Шлюз-1 отдача (Q)");
		public static PiramidaRecord P_KL6_Shluz2_Priem=new PiramidaRecord(0, 8738, 13, "КЛ 6 Шлюз-2 прием (P)");
		public static PiramidaRecord P_KL6_Shluz2_Otd=new PiramidaRecord(0, 8738, 14, "КЛ 6 Шлюз-2 отдача (P)");
		public static PiramidaRecord Q_KL6_Shluz2_Priem=new PiramidaRecord(0, 8738, 15, "КЛ 6 Шлюз-2 прием (Q)");
		public static PiramidaRecord Q_KL6_Shluz2_Otd=new PiramidaRecord(0, 8738, 16, "КЛ 6 Шлюз-2 отдача (Q)");

		public static PiramidaRecord P_GA3_Priem=new PiramidaRecord(0, 8738, 17, "Генератор-3 прием (P)");
		public static PiramidaRecord P_GA3_Otd=new PiramidaRecord(0, 8738, 18, "Генератор-3 отдача (P)");
		public static PiramidaRecord Q_GA3_Priem=new PiramidaRecord(0, 8738, 18, "Генератор-3 прием (Q)");
		public static PiramidaRecord Q_GA3_Otd=new PiramidaRecord(0, 8738, 20, "Генератор-3 отдача (Q)");

		public static PiramidaRecord P_GA4_Priem=new PiramidaRecord(0, 8738, 21, "Генератор-4 прием (P)");
		public static PiramidaRecord P_GA4_Otd=new PiramidaRecord(0, 8738, 22, "Генератор-4 отдача (P)");
		public static PiramidaRecord Q_GA4_Priem=new PiramidaRecord(0, 8738, 23, "Генератор-4 прием (Q)");
		public static PiramidaRecord Q_GA4_Otd=new PiramidaRecord(0, 8738, 24, "Генератор-4 отдача (Q)");

		public static PiramidaRecord P_GA5_Priem=new PiramidaRecord(0, 8738, 25, "Генератор-5 прием (P)");
		public static PiramidaRecord P_GA5_Otd=new PiramidaRecord(0, 8738, 26, "Генератор-5 отдача (P)");
		public static PiramidaRecord Q_GA5_Priem=new PiramidaRecord(0, 8738, 27, "Генератор-5 прием (Q)");
		public static PiramidaRecord Q_GA5_Otd=new PiramidaRecord(0, 8738, 28, "Генератор-5 отдача (Q)");

		public static PiramidaRecord P_GA6_Priem=new PiramidaRecord(0, 8738, 29, "Генератор-6 прием (P)");
		public static PiramidaRecord P_GA6_Otd=new PiramidaRecord(0, 8738, 30, "Генератор-6 отдача (P)");
		public static PiramidaRecord Q_GA6_Priem=new PiramidaRecord(0, 8738, 31, "Генератор-6 прием (Q)");
		public static PiramidaRecord Q_GA6_Otd=new PiramidaRecord(0, 8738, 32, "Генератор-6 отдача (Q)");

		public static PiramidaRecord P_KL6_Filtr1_Priem=new PiramidaRecord(0, 8738, 33, "КЛ 6 Фильтр-1 прием (P)");
		public static PiramidaRecord P_KL6_Filtr1_Otd=new PiramidaRecord(0, 8738, 34, "КЛ 6 Фильтр-1 отдача (P)");
		public static PiramidaRecord Q_KL6_Filtr1_Priem=new PiramidaRecord(0, 8738, 35, "КЛ 6 Фильтр-1 прием (Q)");
		public static PiramidaRecord Q_KL6_Filtr1_Otd=new PiramidaRecord(0, 8738, 36, "КЛ 6 Фильтр-1 отдача (Q)");
		public static PiramidaRecord P_KL6_Filtr2_Priem=new PiramidaRecord(0, 8738, 37, "КЛ 6 Фильтр-2 прием (P)");
		public static PiramidaRecord P_KL6_Filtr2_Otd=new PiramidaRecord(0, 8738, 38, "КЛ 6 Фильтр-2 отдача (P)");
		public static PiramidaRecord Q_KL6_Filtr2_Priem=new PiramidaRecord(0, 8738, 39, "КЛ 6 Фильтр-2 прием (Q)");
		public static PiramidaRecord Q_KL6_Filtr2_Otd=new PiramidaRecord(0, 8738, 40, "КЛ 6 Фильтр-2 отдача (Q)");

		public static PiramidaRecord P_GA7_Priem=new PiramidaRecord(0, 8738, 41, "Генератор-7 прием (P)");
		public static PiramidaRecord P_GA7_Otd=new PiramidaRecord(0, 8738, 42, "Генератор-7 отдача (P)");
		public static PiramidaRecord Q_GA7_Priem=new PiramidaRecord(0, 8738, 43, "Генератор-7 прием (Q)");
		public static PiramidaRecord Q_GA7_Otd=new PiramidaRecord(0, 8738, 44, "Генератор-7 отдача (Q)");

		public static PiramidaRecord P_GA8_Priem=new PiramidaRecord(0, 8738, 45, "Генератор-8 прием (P)");
		public static PiramidaRecord P_GA8_Otd=new PiramidaRecord(0, 8738, 46, "Генератор-8 отдача (P)");
		public static PiramidaRecord Q_GA8_Priem=new PiramidaRecord(0, 8738, 47, "Генератор-8 прием (Q)");
		public static PiramidaRecord Q_GA8_Otd=new PiramidaRecord(0, 8738, 48, "Генератор-8 отдача (Q)");

		public static PiramidaRecord P_GA9_Priem=new PiramidaRecord(0, 8738, 49, "Генератор-9 прием (P)");
		public static PiramidaRecord P_GA9_Otd=new PiramidaRecord(0, 8738, 50, "Генератор-9 отдача (P)");
		public static PiramidaRecord Q_GA9_Priem=new PiramidaRecord(0, 8738, 51, "Генератор-9 прием (Q)");
		public static PiramidaRecord Q_GA9_Otd=new PiramidaRecord(0, 8738, 52, "Генератор-9 отдача (Q)");

		public static PiramidaRecord P_GA10_Priem=new PiramidaRecord(0, 8738, 53, "Генератор-10 прием (P)");
		public static PiramidaRecord P_GA10_Otd=new PiramidaRecord(0, 8738, 54, "Генератор-10 отдача (P)");
		public static PiramidaRecord Q_GA10_Priem=new PiramidaRecord(0, 8738, 55, "Генератор-10 прием (Q)");
		public static PiramidaRecord Q_GA10_Otd=new PiramidaRecord(0, 8738, 56, "Генератор-10 отдача (Q)");

		public static PiramidaRecord Water_NB=new PiramidaRecord(2, 1, 275, "НБ");
		public static PiramidaRecord Water_VB=new PiramidaRecord(2, 1, 274, "ВБ");
		public static PiramidaRecord Water_Napor=new PiramidaRecord(2, 1, 276, "Напор");
		public static PiramidaRecord Water_Temp=new PiramidaRecord(2, 1, 373, "Температура");
		public static PiramidaRecord Water_QGES=new PiramidaRecord(2, 1, 354, "Расход ГЭС");
		public static PiramidaRecord Water_QOptGES=new PiramidaRecord(2, 10, 1, "Опт. расход ГЭС");
		public static PiramidaRecord Water_QOptGTP1=new PiramidaRecord(2, 10, 2, "Опт. расход ГТП-1");
		public static PiramidaRecord Water_QOptGTP2=new PiramidaRecord(2, 10, 3, "Опт. расход ГТП-2");

		public static PiramidaRecord Water_Q_GA1=new PiramidaRecord(2, 1, 104, "Расход ГА-1");
		public static PiramidaRecord Water_Q_GA2=new PiramidaRecord(2, 1, 129, "Расход ГА-2");
		public static PiramidaRecord Water_Q_GA3=new PiramidaRecord(2, 1, 154, "Расход ГА-3");
		public static PiramidaRecord Water_Q_GA4=new PiramidaRecord(2, 1, 179, "Расход ГА-4");
		public static PiramidaRecord Water_Q_GA5=new PiramidaRecord(2, 1, 204, "Расход ГА-5");
		public static PiramidaRecord Water_Q_GA6=new PiramidaRecord(2, 1, 229, "Расход ГА-6");
		public static PiramidaRecord Water_Q_GA7=new PiramidaRecord(2, 1, 254, "Расход ГА-7");
		public static PiramidaRecord Water_Q_GA8=new PiramidaRecord(2, 1, 279, "Расход ГА-8");
		public static PiramidaRecord Water_Q_GA9=new PiramidaRecord(2, 1, 304, "Расход ГА-9");
		public static PiramidaRecord Water_Q_GA10=new PiramidaRecord(2, 1, 329, "Расход ГА-10");
		
		public static PiramidaRecord GSV2=new PiramidaRecord(2, 7, 2, "Верхний бьеф на 8 утра");
		public static PiramidaRecord GSV3=new PiramidaRecord(2, 7, 3, "Нижний бьеф на 8 утра");
		public static PiramidaRecord GSV4=new PiramidaRecord(2, 7, 4, "Нижний бьеф (средний за сутки)");
		public static PiramidaRecord GSV5=new PiramidaRecord(2, 7, 5, "Нижний бьеф (макс. за сутки)");
		public static PiramidaRecord GSV6=new PiramidaRecord(2, 7, 6, "Нижний бьеф (мин за сутки)");
		public static PiramidaRecord GSV7=new PiramidaRecord(2, 7, 7, "Среднесуточный напор (брутто)");
		public static PiramidaRecord GSV8=new PiramidaRecord(2, 7, 8, "Среднесуточный напор (нетто)");
		public static PiramidaRecord GSV9=new PiramidaRecord(2, 7, 9, "Среднесуточный напор (нетто с учетом потери на сут.рег.)");
		public static PiramidaRecord GSV10=new PiramidaRecord(2, 7, 10, "Перепад на решетках");
		public static PiramidaRecord GSV11=new PiramidaRecord(2, 7, 11, "Суточная выработка эл.энергии");
		public static PiramidaRecord GSV12=new PiramidaRecord(2, 7, 12, "Выработка эл.энергии с начала месяца");
		public static PiramidaRecord GSV13=new PiramidaRecord(2, 7, 13, "Нагрузка ГЭС (средняя)");
		public static PiramidaRecord GSV14=new PiramidaRecord(2, 7, 14, "Нагрузка ГЭС (макс)");
		public static PiramidaRecord GSV15=new PiramidaRecord(2, 7, 15, "Нагрузка ГЭС (мин)");
		public static PiramidaRecord GSV16=new PiramidaRecord(2, 7, 16, "Средний расход воды (турбины)");
		public static PiramidaRecord GSV17=new PiramidaRecord(2, 7, 17, "Средний расход воды (водослив)");
		public static PiramidaRecord GSV18=new PiramidaRecord(2, 7, 18, "Средний расход воды (фильтр)");
		public static PiramidaRecord GSV19=new PiramidaRecord(2, 7, 19, "Средний расход воды (шлюзов.)");
		public static PiramidaRecord GSV20=new PiramidaRecord(2, 7, 20, "Средний расход воды (общий)");
		public static PiramidaRecord GSV21=new PiramidaRecord(2, 7, 21, "Удельный расход");
		public static PiramidaRecord GSV22=new PiramidaRecord(2, 7, 22, "Расход в НБ КамГЭС");
		public static PiramidaRecord GSV23=new PiramidaRecord(2, 7, 23, "Боковой приток");
		public static PiramidaRecord GSV24=new PiramidaRecord(2, 7, 24, "Наш приток");
		public static PiramidaRecord GSV25=new PiramidaRecord(2, 7, 25, "Верхний бьеф КамГЭС");
		public static PiramidaRecord GSV26=new PiramidaRecord(2, 7, 26, "Нижний бьеф КамГЭС");
		public static PiramidaRecord GSV27=new PiramidaRecord(2, 7, 27, "Приток КамГЭС");
		public static PiramidaRecord GSV28=new PiramidaRecord(2, 1, 373, "Температура","sut");

		public static PiramidaRecord MB_I_SHSV_220=new PiramidaRecord(2, 3, 0, "Ток ШСВ 220");
		public static PiramidaRecord MB_I_SHSV_110=new PiramidaRecord(2, 3, 2, "Ток ШСВ 110");
		public static PiramidaRecord MB_U_1SH_220=new PiramidaRecord(2, 3, 6, "U 1 с.ш. 220");
		public static PiramidaRecord MB_F_1SH_220=new PiramidaRecord(2, 3, 8, "F 1 с.ш. 220");
		public static PiramidaRecord MB_U_1SH_110=new PiramidaRecord(2, 3, 10, "U 1 с.ш. 110");
		public static PiramidaRecord MB_F_1SH_110=new PiramidaRecord(2, 3, 12, "F 1 с.ш. 110");
		public static PiramidaRecord MB_U_2SH_220=new PiramidaRecord(2, 3, 18, "U 2 с.ш. 220");
		public static PiramidaRecord MB_F_2SH_220=new PiramidaRecord(2, 3, 20, "F 2 с.ш. 220");
		public static PiramidaRecord MB_U_2SH_110=new PiramidaRecord(2, 3, 22, "U 2 с.ш. 110");
		public static PiramidaRecord MB_F_2SH_110=new PiramidaRecord(2, 3, 24, "F 2 с.ш. 110");
		public static PiramidaRecord MB_I_Izhevsk1_220=new PiramidaRecord(2, 3, 30, "I Иж-1 220");
		public static PiramidaRecord MB_P_Izhevsk1_220=new PiramidaRecord(2, 3, 32, "P Иж-1 220");
		public static PiramidaRecord MB_Q_Izhevsk1_220=new PiramidaRecord(2, 3, 34, "Q Иж-1 220");
		public static PiramidaRecord MB_I_Izhevsk2_220=new PiramidaRecord(2, 3, 48, "I Иж-2 220");
		public static PiramidaRecord MB_P_Izhevsk2_220=new PiramidaRecord(2, 3, 50, "P Иж-2 220");
		public static PiramidaRecord MB_Q_Izhevsk2_220=new PiramidaRecord(2, 3, 52, "Q Иж-2 220");
		public static PiramidaRecord MB_I_Kauchuk1_220=new PiramidaRecord(2, 3, 66, "I Кау-1 220");
		public static PiramidaRecord MB_P_Kauchuk1_220=new PiramidaRecord(2, 3, 68, "P Кау-1 220");
		public static PiramidaRecord MB_Q_Kauchuk1_220=new PiramidaRecord(2, 3, 70, "Q Кау-1 220");
		public static PiramidaRecord MB_I_Kauchuk2_220=new PiramidaRecord(2, 3, 84, "I Кау-2 220");
		public static PiramidaRecord MB_P_Kauchuk2_220=new PiramidaRecord(2, 3, 86, "P Кау-2 220");
		public static PiramidaRecord MB_Q_Kauchuk2_220=new PiramidaRecord(2, 3, 88, "Q Кау-2 220");
		public static PiramidaRecord MB_P_GES_Zad=new PiramidaRecord(2, 3, 91, "Задание P");

		public static PiramidaRecord MB_I_Svetlaya_220=new PiramidaRecord(2, 3, 102, "I Светлая 220");
		public static PiramidaRecord MB_P_Svetlaya_220=new PiramidaRecord(2, 3, 104, "P Светлая 220");
		public static PiramidaRecord MB_Q_Svetlaya_220=new PiramidaRecord(2, 3, 106, "Q Светлая 220");
		public static PiramidaRecord MB_I_OVV_220=new PiramidaRecord(2, 3, 120, "I OBB 220");
		public static PiramidaRecord MB_P_OVV_220=new PiramidaRecord(2, 3, 122, "P OBB 220");
		public static PiramidaRecord MB_Q_OVV_220=new PiramidaRecord(2, 3, 124, "Q OBB 220");
		public static PiramidaRecord MB_I_2AT_220=new PiramidaRecord(2, 3, 138, "I 2AT 220");
		public static PiramidaRecord MB_P_2AT_220=new PiramidaRecord(2, 3, 140, "P 2AT 220");
		public static PiramidaRecord MB_Q_2AT_220=new PiramidaRecord(2, 3, 142, "Q 2AT 220");
		public static PiramidaRecord MB_I_3AT_220=new PiramidaRecord(2, 3, 156, "I 3AT 220");
		public static PiramidaRecord MB_P_3AT_220=new PiramidaRecord(2, 3, 158, "P 3AT 220");
		public static PiramidaRecord MB_Q_3AT_220=new PiramidaRecord(2, 3, 160, "Q 3AT 220");
		public static PiramidaRecord MB_I_4T_220=new PiramidaRecord(2, 3, 174, "I 4T 220");
		public static PiramidaRecord MB_P_4T_220=new PiramidaRecord(2, 3, 176, "P 4T 220");
		public static PiramidaRecord MB_Q_4T_220=new PiramidaRecord(2, 3, 178, "Q 4T 220");
		public static PiramidaRecord MB_I_56AT_220=new PiramidaRecord(2, 3, 192, "I 5,6 AT 220");
		public static PiramidaRecord MB_P_56AT_220=new PiramidaRecord(2, 3, 194, "P 5,6 AT 220");
		public static PiramidaRecord MB_Q_56AT_220=new PiramidaRecord(2, 3, 196, "Q 5,6 AT 220");
		public static PiramidaRecord MB_GA1_Rashod=new PiramidaRecord(2, 3, 238, "Г1 Расход");
		public static PiramidaRecord MB_GA1_Napor=new PiramidaRecord(2, 3, 230, "Г1 Напор");
		public static PiramidaRecord MB_GA1_Irotor=new PiramidaRecord(2, 3, 210, "Г1 Ток ротора");
		public static PiramidaRecord MB_GA1_P=new PiramidaRecord(2, 3, 216, "Г1 Активная мощность");
		public static PiramidaRecord MB_GA1_Q=new PiramidaRecord(2, 3, 218, "Г1 Реактивная мощность");
		public static PiramidaRecord MB_GA1_IstatorA=new PiramidaRecord(2, 3, 220, "Г1 Ток статора, фаза А");
		public static PiramidaRecord MB_GA1_IstatorB=new PiramidaRecord(2, 3, 222, "Г1 Ток статора, фаза В");
		public static PiramidaRecord MB_GA1_IstatorC=new PiramidaRecord(2, 3, 224, "Г1 Ток статора, фаза С");
		public static PiramidaRecord MB_GA2_Rashod=new PiramidaRecord(2, 3, 288, "Г2 Расход");
		public static PiramidaRecord MB_GA2_Napor=new PiramidaRecord(2, 3, 280, "Г2 Напор");
		public static PiramidaRecord MB_GA2_Irotor=new PiramidaRecord(2, 3, 260, "Г2 Ток ротора");
		public static PiramidaRecord MB_GA2_P=new PiramidaRecord(2, 3, 266, "Г2 Активная мощность");
		public static PiramidaRecord MB_GA2_Q=new PiramidaRecord(2, 3, 268, "Г2 Реактивная мощность");
		public static PiramidaRecord MB_GA2_IstatorA=new PiramidaRecord(2, 3, 270, "Г2 Ток статора, фаза А");
		public static PiramidaRecord MB_GA2_IstatorB=new PiramidaRecord(2, 3, 272, "Г2 Ток статора, фаза В");
		public static PiramidaRecord MB_GA2_IstatorC=new PiramidaRecord(2, 3, 274, "Г2 Ток статора, фаза С");
		public static PiramidaRecord MB_GA3_Rashod=new PiramidaRecord(2, 3, 338, "Г3 Расход");
		public static PiramidaRecord MB_GA3_Napor=new PiramidaRecord(2, 3, 330, "Г3 Напор");
		public static PiramidaRecord MB_GA3_Irotor=new PiramidaRecord(2, 3, 310, "Г3 Ток ротора");
		public static PiramidaRecord MB_GA3_P=new PiramidaRecord(2, 3, 316, "Г3 Активная мощность");
		public static PiramidaRecord MB_GA3_Q=new PiramidaRecord(2, 3, 318, "Г3 Реактивная мощность");
		public static PiramidaRecord MB_GA3_IstatorA=new PiramidaRecord(2, 3, 320, "Г3 Ток статора, фаза А");
		public static PiramidaRecord MB_GA3_IstatorB=new PiramidaRecord(2, 3, 322, "Г3 Ток статора, фаза В");
		public static PiramidaRecord MB_GA3_IstatorC=new PiramidaRecord(2, 3, 324, "Г3 Ток статора, фаза С");
		public static PiramidaRecord MB_GA4_Rashod=new PiramidaRecord(2, 3, 388, "Г4 Расход");
		public static PiramidaRecord MB_GA4_Napor=new PiramidaRecord(2, 3, 380, "Г4 Напор");
		public static PiramidaRecord MB_GA4_Irotor=new PiramidaRecord(2, 3, 360, "Г4 Ток ротора");
		public static PiramidaRecord MB_GA4_P=new PiramidaRecord(2, 3, 366, "Г4 Активная мощность");
		public static PiramidaRecord MB_GA4_Q=new PiramidaRecord(2, 3, 368, "Г4 Реактивная мощность");
		public static PiramidaRecord MB_GA4_IstatorA=new PiramidaRecord(2, 3, 370, "Г4 Ток статора, фаза А");
		public static PiramidaRecord MB_GA4_IstatorB=new PiramidaRecord(2, 3, 372, "Г4 Ток статора, фаза В");
		public static PiramidaRecord MB_GA4_IstatorC=new PiramidaRecord(2, 3, 374, "Г4 Ток статора, фаза С");
		public static PiramidaRecord MB_GA5_Rashod=new PiramidaRecord(2, 3, 438, "Г5 Расход");
		public static PiramidaRecord MB_GA5_Napor=new PiramidaRecord(2, 3, 430, "Г5 Напор");
		public static PiramidaRecord MB_GA5_Irotor=new PiramidaRecord(2, 3, 410, "Г5 Ток ротора");
		public static PiramidaRecord MB_GA5_P=new PiramidaRecord(2, 3, 416, "Г5 Активная мощность");
		public static PiramidaRecord MB_GA5_Q=new PiramidaRecord(2, 3, 418, "Г5 Реактивная мощность");
		public static PiramidaRecord MB_GA5_IstatorA=new PiramidaRecord(2, 3, 420, "Г5 Ток статора, фаза А");
		public static PiramidaRecord MB_GA5_IstatorB=new PiramidaRecord(2, 3, 422, "Г5 Ток статора, фаза В");
		public static PiramidaRecord MB_GA5_IstatorC=new PiramidaRecord(2, 3, 424, "Г5 Ток статора, фаза С");
		public static PiramidaRecord MB_GA6_Rashod=new PiramidaRecord(2, 3, 488, "Г6 Расход");
		public static PiramidaRecord MB_GA6_Napor=new PiramidaRecord(2, 3, 480, "Г6 Напор");
		public static PiramidaRecord MB_GA6_Irotor=new PiramidaRecord(2, 3, 460, "Г6 Ток ротора");
		public static PiramidaRecord MB_GA6_P=new PiramidaRecord(2, 3, 466, "Г6 Активная мощность");
		public static PiramidaRecord MB_GA6_Q=new PiramidaRecord(2, 3, 468, "Г1 Реактивная мощность");
		public static PiramidaRecord MB_GA6_IstatorA=new PiramidaRecord(2, 3, 470, "Г6 Ток статора, фаза А");
		public static PiramidaRecord MB_GA6_IstatorB=new PiramidaRecord(2, 3, 472, "Г6 Ток статора, фаза В");
		public static PiramidaRecord MB_GA6_IstatorC=new PiramidaRecord(2, 3, 474, "Г6 Ток статора, фаза С");
		public static PiramidaRecord MB_GA7_Rashod=new PiramidaRecord(2, 3, 538, "Г7 Расход");
		public static PiramidaRecord MB_GA7_Napor=new PiramidaRecord(2, 3, 530, "Г7 Напор");
		public static PiramidaRecord MB_GA7_Irotor=new PiramidaRecord(2, 3, 510, "Г7 Ток ротора");
		public static PiramidaRecord MB_GA7_P=new PiramidaRecord(2, 3, 516, "Г7 Активная мощность");
		public static PiramidaRecord MB_GA7_Q=new PiramidaRecord(2, 3, 518, "Г7 Реактивная мощность");
		public static PiramidaRecord MB_GA7_IstatorA=new PiramidaRecord(2, 3, 520, "Г7 Ток статора, фаза А");
		public static PiramidaRecord MB_GA7_IstatorB=new PiramidaRecord(2, 3, 522, "Г7 Ток статора, фаза В");
		public static PiramidaRecord MB_GA7_IstatorC=new PiramidaRecord(2, 3, 524, "Г7 Ток статора, фаза С");

		public static PiramidaRecord MB_VB_Sgl=new PiramidaRecord(2, 3, 548, "Верхний бьеф сгл.");
		public static PiramidaRecord MB_NB_Sgl=new PiramidaRecord(2, 3, 550, "Нижний бьеф сгл.");
		public static PiramidaRecord MB_Napor_Sgl=new PiramidaRecord(2, 3, 552, "Напор сгл.");

		public static PiramidaRecord MB_GA8_Rashod=new PiramidaRecord(2, 3, 588, "Г8 Расход");
		public static PiramidaRecord MB_GA8_Napor=new PiramidaRecord(2, 3, 580, "Г8 Напор");
		public static PiramidaRecord MB_GA8_Irotor=new PiramidaRecord(2, 3, 560, "Г8 Ток ротора");
		public static PiramidaRecord MB_GA8_P=new PiramidaRecord(2, 3, 566, "Г8 Активная мощность");
		public static PiramidaRecord MB_GA8_Q=new PiramidaRecord(2, 3, 568, "Г8 Реактивная мощность");
		public static PiramidaRecord MB_GA8_IstatorA=new PiramidaRecord(2, 3, 570, "Г8 Ток статора, фаза А");
		public static PiramidaRecord MB_GA8_IstatorB=new PiramidaRecord(2, 3, 572, "Г8 Ток статора, фаза В");
		public static PiramidaRecord MB_GA8_IstatorC=new PiramidaRecord(2, 3, 574, "Г8 Ток статора, фаза С");
		public static PiramidaRecord MB_GA8_NaporZad=new PiramidaRecord(2, 3, 580, "Г8 Контроль напора");
		public static PiramidaRecord MB_GA9_Rashod=new PiramidaRecord(2, 3, 638, "Г9 Расход");
		public static PiramidaRecord MB_GA9_Napor=new PiramidaRecord(2, 3, 630, "Г9 Напор");
		public static PiramidaRecord MB_GA9_Irotor=new PiramidaRecord(2, 3, 610, "Г9 Ток ротора");
		public static PiramidaRecord MB_GA9_P=new PiramidaRecord(2, 3, 616, "Г9 Активная мощность");
		public static PiramidaRecord MB_GA9_Q=new PiramidaRecord(2, 3, 618, "Г9 Реактивная мощность");
		public static PiramidaRecord MB_GA9_IstatorA=new PiramidaRecord(2, 3, 620, "Г9 Ток статора, фаза А");
		public static PiramidaRecord MB_GA9_IstatorB=new PiramidaRecord(2, 3, 622, "Г9 Ток статора, фаза В");
		public static PiramidaRecord MB_GA9_IstatorC=new PiramidaRecord(2, 3, 624, "Г9 Ток статора, фаза С");
		public static PiramidaRecord MB_GA9_NaporZad=new PiramidaRecord(2, 3, 630, "Г9 Контроль напора");
		public static PiramidaRecord MB_GA10_Rashod=new PiramidaRecord(2, 3, 688, "Г10 Расход");
		public static PiramidaRecord MB_GA10_Napor=new PiramidaRecord(2, 3, 680, "Г10 Напор");
		public static PiramidaRecord MB_GA10_Irotor=new PiramidaRecord(2, 3, 660, "Г10 Ток ротора");
		public static PiramidaRecord MB_GA10_P=new PiramidaRecord(2, 3, 666, "Г10 Активная мощность");
		public static PiramidaRecord MB_GA10_Q=new PiramidaRecord(2, 3, 668, "Г10 Реактивная мощность");
		public static PiramidaRecord MB_GA10_IstatorA=new PiramidaRecord(2, 3, 670, "Г10 Ток статора, фаза А");
		public static PiramidaRecord MB_GA10_IstatorB=new PiramidaRecord(2, 3, 672, "Г10 Ток статора, фаза В");
		public static PiramidaRecord MB_GA10_IstatorC=new PiramidaRecord(2, 3, 674, "Г10 Ток статора, фаза С");
		public static PiramidaRecord MB_GA10_NaporZad=new PiramidaRecord(2, 3, 680, "Г10 Контроль напора");

		public static PiramidaRecord MB_Rashod=new PiramidaRecord(2, 3, 708, "Расход воды (Q) ГЭС");
		public static PiramidaRecord MB_RashodCalc=new PiramidaRecord(2, 30, 4, "Расход воды (Q) ГЭС");
		public static PiramidaRecord MB_T=new PiramidaRecord(2, 3, 746, "Температура наружного воздуха");
		public static PiramidaRecord MB_P_GES=new PiramidaRecord(2, 3, 750, "Суммарная Р ГЭС");
		public static PiramidaRecord MB_Q_GES=new PiramidaRecord(2, 3, 764, "Суммарная Q ГЭС");

		public static PiramidaRecord MB_I_KSHT1_110=new PiramidaRecord(2, 3, 810, "I КШТ-1 110КВ");
		public static PiramidaRecord MB_P_KSHT1_110=new PiramidaRecord(2, 3, 812, "P КШТ-1 110КВ");
		public static PiramidaRecord MB_Q_KSHT1_110=new PiramidaRecord(2, 3, 814, "Q КШТ-1 110КВ");
		public static PiramidaRecord MB_I_KSHT2_110=new PiramidaRecord(2, 3, 820, "I КШТ-2 110КВ");
		public static PiramidaRecord MB_P_KSHT2_110=new PiramidaRecord(2, 3, 822, "P КШТ-2 110КВ");
		public static PiramidaRecord MB_Q_KSHT2_110=new PiramidaRecord(2, 3, 824, "Q КШТ-2 110КВ");
		public static PiramidaRecord MB_I_Kauchuk_110=new PiramidaRecord(2, 3, 830, "I Каучук 110КВ");
		public static PiramidaRecord MB_P_Kauchuk_110=new PiramidaRecord(2, 3, 832, "P Каучук 110КВ");
		public static PiramidaRecord MB_Q_Kauchuk_110=new PiramidaRecord(2, 3, 834, "Q Каучук 110КВ");
		public static PiramidaRecord MB_I_TEC_110=new PiramidaRecord(2, 3, 840, "I ЧаТЭЦ 110 кВ");
		public static PiramidaRecord MB_P_TEC_110=new PiramidaRecord(2, 3, 842, "P ЧаТЭЦ 110 кВ");
		public static PiramidaRecord MB_Q_TEC_110=new PiramidaRecord(2, 3, 844, "Q ЧаТЭЦ 110 кВ");
		public static PiramidaRecord MB_I_Berezovka_110=new PiramidaRecord(2, 3, 850, "I Березовка 110 кВ");
		public static PiramidaRecord MB_P_Berezovka_110=new PiramidaRecord(2, 3, 852, "P Березовка 110 кВ");
		public static PiramidaRecord MB_Q_Berezovka_110=new PiramidaRecord(2, 3, 854, "Q Березовка 110 кВ");
		public static PiramidaRecord MB_I_Dubovaya_110=new PiramidaRecord(2, 3, 860, "I Дубовая 110 кВ");
		public static PiramidaRecord MB_P_Dubovaya_110=new PiramidaRecord(2, 3, 862, "P Дубовая 110 кВ");
		public static PiramidaRecord MB_Q_Dubovaya_110=new PiramidaRecord(2, 3, 864, "Q Дубовая 110 кВ");
		public static PiramidaRecord MB_I_Vodozabor1_110=new PiramidaRecord(2, 3, 870, "I Водозабор 1 - 110 кВ");
		public static PiramidaRecord MB_P_Vodozabor1_110=new PiramidaRecord(2, 3, 872, "P Водозабор 1 - 110 кВ");
		public static PiramidaRecord MB_Q_Vodozabor1_110=new PiramidaRecord(2, 3, 874, "Q Водозабор 1 - 110 кВ");
		public static PiramidaRecord MB_I_Vodozabor2_110=new PiramidaRecord(2, 3, 880, "I Водозабор 2 - 110 кВ");
		public static PiramidaRecord MB_P_Vodozabor2_110=new PiramidaRecord(2, 3, 882, "P Водозабор 2 - 110 кВ");
		public static PiramidaRecord MB_Q_Vodozabor2_110=new PiramidaRecord(2, 3, 884, "Q Водозабор 2 - 110 кВ");
		public static PiramidaRecord MB_I_Svetlaya_110=new PiramidaRecord(2, 3, 890, "I Светлая 110 кВ");
		public static PiramidaRecord MB_P_Svetlaya_110=new PiramidaRecord(2, 3, 892, "P Светлая 110 кВ");
		public static PiramidaRecord MB_Q_Svetlaya_110=new PiramidaRecord(2, 3, 894, "Q Светлая 110 кВ");
		public static PiramidaRecord MB_I_Ivanovka_110=new PiramidaRecord(2, 3, 900, "I Ивановка 110 кВ");
		public static PiramidaRecord MB_P_Ivanovka_110=new PiramidaRecord(2, 3, 902, "P Ивановка 110 кВ");
		public static PiramidaRecord MB_Q_Ivanovka_110=new PiramidaRecord(2, 3, 904, "Q Ивановка 110 кВ");
		public static PiramidaRecord MB_I_OVV_110=new PiramidaRecord(2, 3, 910, "I ОВВ 110 кВ");
		public static PiramidaRecord MB_P_OVV_110=new PiramidaRecord(2, 3, 912, "P ОВВ 110 кВ");
		public static PiramidaRecord MB_Q_OVV_110=new PiramidaRecord(2, 3, 914, "Q ОВВ 110 кВ");
		public static PiramidaRecord MB_I_Emelino_500=new PiramidaRecord(2, 3, 930, "I Емелино 500 кВ");
		public static PiramidaRecord MB_P_Emelino_500=new PiramidaRecord(2, 3, 932, "P Емелино 500 кВ");
		public static PiramidaRecord MB_Q_Emelino_500=new PiramidaRecord(2, 3, 934, "Q Емелино 500 кВ");
		public static PiramidaRecord MB_U_Emelino_500=new PiramidaRecord(2, 3, 936, "U Емелино 500 кВ");
		public static PiramidaRecord MB_F_Emelino_500=new PiramidaRecord(2, 3, 938, "F Емелино 500 кВ");
		public static PiramidaRecord MB_I_Karmanovo_500=new PiramidaRecord(2, 3, 940, "I Карманово 500 кВ");
		public static PiramidaRecord MB_P_Karmanovo_500=new PiramidaRecord(2, 3, 942, "P Карманово 500 кВ");
		public static PiramidaRecord MB_Q_Karmanovo_500=new PiramidaRecord(2, 3, 944, "Q Карманово 500 кВ");
		public static PiramidaRecord MB_U_Karmanovo_500=new PiramidaRecord(2, 3, 946, "U Карманово 500 кВ");
		public static PiramidaRecord MB_F_Karmanovo_500=new PiramidaRecord(2, 3, 948, "F Карманово 500 кВ");
		public static PiramidaRecord MB_I_Vyatka_500=new PiramidaRecord(2, 3, 950, "I Вятка 500 кВ");
		public static PiramidaRecord MB_P_Vyatka_500=new PiramidaRecord(2, 3, 952, "P Вятка 500 кВ");
		public static PiramidaRecord MB_Q_Vyatka_500=new PiramidaRecord(2, 3, 954, "Q Вятка 500 кВ");
		public static PiramidaRecord MB_U_Vyatka_500=new PiramidaRecord(2, 3, 956, "U Вятка 500 кВ");
		public static PiramidaRecord MB_F_Vyatka_500=new PiramidaRecord(2, 3, 958, "F Вятка 500 кВ");
		public static PiramidaRecord MB_I_1T_110=new PiramidaRecord(2, 3, 970, "I ВВ 1Т 110 кВ");
		public static PiramidaRecord MB_P_1T_110=new PiramidaRecord(2, 3, 972, "P ВВ 1Т 110 кВ");
		public static PiramidaRecord MB_Q_1T_110=new PiramidaRecord(2, 3, 974, "Q ВВ 1Т 110 кВ");
		public static PiramidaRecord MB_I_56AT_110=new PiramidaRecord(2, 3, 980, "I ВВ 5,6АТ 110 кВ");
		public static PiramidaRecord MB_P_56AT_110=new PiramidaRecord(2, 3, 982, "P ВВ 5,6АТ 110 кВ");
		public static PiramidaRecord MB_Q_56AT_110=new PiramidaRecord(2, 3, 984, "Q ВВ 5,6АТ 110 кВ");
		public static PiramidaRecord MB_I_2AT_500=new PiramidaRecord(2, 3, 990, "I ВВ 2АТ 500 кВ");
		public static PiramidaRecord MB_P_2AT_500=new PiramidaRecord(2, 3, 992, "P ВВ 2АТ 500 кВ");
		public static PiramidaRecord MB_Q_2AT_500=new PiramidaRecord(2, 3, 994, "Q ВВ 2АТ 500 кВ");
		public static PiramidaRecord MB_I_3AT_500=new PiramidaRecord(2, 3, 1000, "I ВВ 3АТ 500 кВ");
		public static PiramidaRecord MB_P_3AT_500=new PiramidaRecord(2, 3, 1002, "P ВВ 3АТ 500 кВ");
		public static PiramidaRecord MB_Q_3AT_500=new PiramidaRecord(2, 3, 1004, "Q ВВ 3АТ 500 кВ");

		/*
		=СЦЕПИТЬ("public static PiramidaRecord MB";[@Addr];"=new PiramidaRecord(2, 3,"; [@Addr];", """;[@Name];""");")
		 * */


		static void PiramidaRecord(){

		}

		public static void addRecord(string key, int objType, int obj, int item, string title) {
		}

	}
}

