//*****************************************************************************
// Модуль для работы с отчетами (ведомостями) в формате HTML
//*****************************************************************************
using System.Data;
using System.IO;
using System;

namespace ProjectNSI
{
    // класс для работы c HTML отчетами (генерация и сохранение их)
    public static class HTMLWorker
    {
        #region Отчеты по разузлованию
        /// <summary>
        /// Генератор отчета по таблице "Полная применяемость"
        /// </summary>
        /// <param name="tr">Дерево разузлования</param>
        public static void GenerateFullApplicationReport(Tree tr)
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(GlobalVars.AppDir + @"\Reports\FullApllicationTableReport.html");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Ведомость по таблице Полная применяемость</title>
                              <style type='text/css'>
                              .h2 {
	                                text-align: left;
	                                display: table-caption;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #009;
	                                text-decoration: underline;
                                  }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body><p align = 'center'>");

            // определение количества корневых узлов (изделий)
            int count = tr.Root.Length;

            // для каждого изделия формирование своей таблицы
            string str;
            DataTable dt;
            for (int i = 0; i < count; i++)
            {
                // выборка из таблицы СНП описания продукции для корня
                str = String.Format("SELECT * FROM ProductNames WHERE ProductNames.ProductKey = {0}", tr.Root[i].PRootCode);
                dt = DBWorker.dbf.Execute(str);
                str = String.Format("{0} {1} {2}", dt.Rows[0].ItemArray[0], dt.Rows[0].ItemArray[1], dt.Rows[0].ItemArray[2]);

                // формирование заголовка таблицы и самой таблицы
                file.WriteLine("<table border='1' cellspacing='0' width='90%'>");
                file.WriteLine(String.Format("<caption align='left'><h2 class='h2'>Ведомость изделия: {0} </h2></caption>", str));
                file.WriteLine(@"<tr>
                                    <th>Код детали</th>
                                    <th>Наименование, обозначение детали</th>
                                    <th>Количество деталей, входящее в изделие</th>
                                </tr>");

                // загрузка данных для выбранного корня (изделия)
                str = String.Format(@"SELECT
                                                ProductNames.ProductKey,                                                
                                                ProductNames.Name || ' ' || ProductNames.Designation AS NameDes,
                                                FullApplication.Count
                                            FROM
                                                FullApplication
                                                INNER JOIN ProductNames ON (FullApplication.PackageDetails = ProductNames.ProductKey)
                                            WHERE
                                                FullApplication.ProductCode = {0}", tr.Root[i].PRootCode);
                dt = DBWorker.dbf.Execute(str);
                // формирование таблицы ведомости для данного изделия
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    file.WriteLine("<tr align = 'left'>");
                    for (int k = 0; k < dt.Columns.Count; k++)
                        file.Write(String.Format("<td>{0}</td> ", dt.Rows[j].ItemArray[k].ToString()));
                    file.WriteLine("</tr>");
                }
                // завершение таблицы
                file.WriteLine("</table>");
            }

            // заврешение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }
        #endregion

        #region Ведомости по материальному нормированию
        /// <summary>
        /// Генерация ведомости подетальных норм расхода материала
        /// </summary>
        public static void GeneratePeportDetailStandarts()
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(GlobalVars.AppDir + @"\Reports\DetailsStandartsReport.html");

            // загрузка данных из ТБД "СТМ"
            DataTable dtm = DBWorker.SelectDataFromTable("СТМ");
            // загрузка данных из ТБД "СНП"
            DataTable dtp = DBWorker.SelectDataFromTable("СНП");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Ведомость подетальных норм расхода материалов</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .MyP1 {
	                                font-size: 24px;
	                                font-weight: bold;
	                                color: #900;
                                    }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine("<h1 class='h1'>Ведомость подетальных норм расхода материалов</h1>");

            // определение количества корневых узлов (изделий)
            int count = 0;
            long[] Roots = GetProdCount(ref count);

            // для каждого изделия формирование своей таблицы
            string str;
            DataTable dt, dt2;
            for (int i = 0; i < count; i++)
            {
                // выборка из таблицы СНП описания продукции для корня
                str = String.Format("SELECT ProductKey, Name, Designation FROM ProductNames WHERE ProductNames.ProductKey = {0}", Roots[i]);
                dt = DBWorker.dbf.Execute(str);

                // формирование заголовка таблицы и самой таблицы
                file.WriteLine("<p align = 'center'><table border='1' cellspacing='0' width='90%'>");
                file.WriteLine(String.Format(@"<tr class='MyP1'>
                                                <th>Код, наименование изделия</th>
                                                <th>{0}</th>
                                                <th colspan='2'>{1}</th>
                                              </tr>", String.Format("{0} {1}", dt.Rows[0].ItemArray[0], dt.Rows[0].ItemArray[1]), dt.Rows[0].ItemArray[2]));

                // теперь для каждого материала в изделии отыскиваем датали и помещаем сведения в таблицу
                int matcount;
                long[] Mats = GetMaterialsCountForProduct(Roots[i], out matcount);

                for (int j = 0; j < matcount; j++)
                {
                    // создаем заголовок для материала
                    file.WriteLine(String.Format("<tr> <th>Код, наименование материала</th> <td colspan='3' align='left'>{0}</td> </tr>", GetMaterialData(dtm, Mats[j])));
                    // создаем заголовок для деталей из данного материала
                    file.WriteLine("<tr> <th>Код, наименование изделия</th> <th>Количество</th> <th>Норма на деталь</th> <th>Норма на изделие</th> </tr>");

                    // выбираем из БД нужные сведения дл ма риала текущей детали (ВТБД "СДМИ")
                    str = String.Format(@"SELECT DetMatOnProd.DetailCode, DetMatOnProd.Count, DetMatOnProd.Consumption, DetMatOnProd.Wastes
                                        FROM DetMatOnProd
                                        WHERE DetMatOnProd.ProductCode = {0} and DetMatOnProd.MaterialCode = {1}", Roots[i], Mats[j]);
                    dt2 = DBWorker.dbf.Execute(str);

                    int detcount = 0; // кол-во деталей из текущего материала
                    float cons = 0, wast = 0; // расходы и отходы (суммарные по изделию)
                    float tmp1, tmp2;
                    // пишем сведения о деталях из данного материала
                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        tmp1 = Convert.ToInt32(dt2.Rows[k].ItemArray[1]) * Convert.ToSingle(dt2.Rows[k].ItemArray[2]);
                        tmp2 = Convert.ToInt32(dt2.Rows[k].ItemArray[1]) * Convert.ToSingle(dt2.Rows[k].ItemArray[3]);
                        file.WriteLine(String.Format("<tr align='left'> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> </tr>",
                            GetProductData(dtp, (long)dt2.Rows[k].ItemArray[0]), dt2.Rows[k].ItemArray[1],
                            String.Format("Р: {0} О: {1}", dt2.Rows[k].ItemArray[2], dt2.Rows[k].ItemArray[3]),
                            String.Format("Р: {0} О: {1}", tmp1, tmp2)));
                        // калькуляция
                        detcount += Convert.ToInt32(dt2.Rows[k].ItemArray[1]);
                        cons += tmp1;
                        wast += tmp2;
                    }

                    // подведение итогов по материалу
                    file.WriteLine(String.Format("<tr align='left'> <th>Итого по материалу</th> <td>{0}</td> <td>-</td> <td>{1}</td> </tr>",
                        detcount, String.Format("Р: {0} О: {1}", cons, wast) ) );
                }

                // завершение таблицы
                file.WriteLine("</table></p> <p> </p>");
            }

            // завершение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }

        /// <summary>
        /// Генерация ведомости сводных норм расхода материалов на изделие
        /// </summary>
        public static void GenerateReportMaterialsStandartsOnProduct()
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(GlobalVars.AppDir + @"\Reports\MaterialsStandartsOnProductReport.html");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Ведомость сводных норм расхода материалов на изделие</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .h2 {
	                                text-align: left;
	                                display: table-caption;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #009;
	                                text-decoration: underline;
                                  }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine("<h1 class='h1'>Ведомость сводных норм расхода на изделие</h1><p align = 'center'>");

            // определение количества корневых узлов (изделий)
            int count = 0;
            long[] Roots = GetProdCount(ref count);

            // для каждого изделия формирование своей таблицы
            string str;
            DataTable dt;
            for (int i = 0; i < count; i++)
            {
                // выборка из таблицы СНП описания продукции для корня
                str = String.Format("SELECT ProductKey, Name, Designation FROM ProductNames WHERE ProductNames.ProductKey = {0}", Roots[i]);
                dt = DBWorker.dbf.Execute(str);
                str = String.Format("Код изделия: {0} Наименование: {1} Обозначение: {2}", dt.Rows[0].ItemArray[0], dt.Rows[0].ItemArray[1], dt.Rows[0].ItemArray[2]);

                // формирование заголовка таблицы и самой таблицы
                file.WriteLine("<table border='1' cellspacing='0' width='90%'>");
                file.WriteLine(String.Format("<caption align='left'><h2 class='h2'>{0}</h2></caption>", str));
                file.WriteLine(@"<tr>
                                    <th>Код материала</th>
                                    <th>Наименование, обозначение материала</th>
                                    <th>Норма расходов материала</th>
                                    <th>Норма отходов материала</th>
                                </tr>");

                // загрузка данных для выбранного изделия
                str = String.Format(@"SELECT 
                                ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation AS Product,
                                ProdNameStand.MaterialCode || ' ' || ReferenceMaterials.Name AS Material,
                                ProdNameStand.Consumption AS Consumption,
                                ProdNameStand.Wastes AS Wastes
                            FROM 
                                ProdNameStand
                                INNER JOIN ReferenceMaterials ON ( ProdNameStand.MaterialCode = ReferenceMaterials.MaterialCode )
                                INNER JOIN ProductNames On ( ProdNameStand.ProductCode = ProductNames.ProductKey )
                            WHERE
                                ProdNameStand.ProductCode = {0}", Roots[i]);
                dt = DBWorker.dbf.Execute(str);
                // формирование таблицы ведомости для данного изделия
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    file.WriteLine("<tr align = 'left'>");
                    for (int k = 0; k < dt.Columns.Count; k++)
                        file.Write(String.Format("<td>{0}</td> ", dt.Rows[j].ItemArray[k].ToString()));
                    file.WriteLine("</tr>");
                }

                // завершение таблицы
                file.WriteLine("</table>");
            }

            // заврешение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }

        #endregion

        #region Ведомости по потребностям в материалах в разрезе изделий
        /// <summary>
        /// Формирование ведомостей по файлам на каждый возможный период (месяц и год)
        /// </summary>
        public static string[] GenerateMaterialsNeedsReports()
        {
        	// определение месяцов и лет, для которых надо выдать ведомость
            int count = 0;
            MNDates[] dates = GlobalFunctions.GetMNDatesCount(ref count);
            string[] res = new string[count];

            // для каждой даты выдача своей ведомости
            for (int i = 0; i < count; i++)
            {
                GenerateMaterialsNeedsReport(dates[i], GlobalFunctions.GetMNReportFileName(dates[i]));
                res[i] = GlobalFunctions.GetMNReportFileName(dates[i]);
            }

            // возврат списка сгенерированный файлов
            return res;
        }

        public static void GenerateMaterialsNeedsReport(MNDates date, string fname)
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(fname);

            // загрузка данных из ТБД "СТМ"
            DataTable dtm = DBWorker.SelectDataFromTable("СТМ");
            // загрузка данных из ТБД "СЕИ"
            DataTable dtu = DBWorker.SelectDataFromTable("СЕИ");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Сводная ведомость потребности в материалах в разрезе изделий</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .MyP1 {
	                                font-size: 24px;
	                                font-weight: bold;
	                                color: #900;
                                    }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine(String.Format("<h1 class='h1'>Сводная ведомость потребности в материалах в разрезе изделий на {0} месяц {1} года</h1>", GlobalFunctions.GetMonthString(date.Month), date.Year));

            // определение количества изделий, выпускаемых в данном месяце
            int count = 0;
            int[] Plans;
            long[] Roots = GetProdCountInMonth(ref count, out Plans, date);

            // для каждого изделия формирование своей таблицы
            string str;
            DataTable dt;
            for (int i = 0; i < count; i++)
            {
                // выборка из таблицы СНП описания продукции для корня
                str = String.Format("SELECT ProductKey, Name, Designation FROM ProductNames WHERE ProductNames.ProductKey = {0}", Roots[i]);
                dt = DBWorker.dbf.Execute(str);

                // формирование заголовка таблицы и самой таблицы
                file.WriteLine("<p align = 'center'><table border='1' cellspacing='0' width='90%'>");
                file.WriteLine(String.Format(@"<tr class='MyP1'>
                                                <th>Код, наименование изделия</th>
                                                <td>{0}</td>
                                                <th>План выпуска изделий</th>
                                                <td>{1}</td>
                                              </tr>", String.Format("{0} {1}", dt.Rows[0].ItemArray[0], dt.Rows[0].ItemArray[1], dt.Rows[0].ItemArray[2]), Plans[i]));

                // формируем заголовок для материалов и их норм
                file.WriteLine("<tr> <th colspan='2'>Код, наименование материала</th> <th>Потребность в материале</th> <th>Единица измерения</th> </tr>");

                // выбираем сведения по материалам из таблицы "ПМТВ" для текущего изделия
                str = String.Format(@"SELECT 
                                        MaterialsNeeds.MaterialCode, 
                                        MaterialsNeeds.PlanCount
                                    FROM 
                                        MaterialsNeeds
                                        INNER JOIN ProdNameStand ON (ProdNameStand.ProductCode = MaterialsNeeds.ProductCode and ProdNameStand.MaterialCode = MaterialsNeeds.MaterialCode)
                                    WHERE ProdNameStand.ProductCode = {0};", Roots[i]);
                dt = DBWorker.dbf.Execute(str);

                // пишем данные о потребностях материалов в отчет
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    file.WriteLine(String.Format("<tr align='left'> <td colspan='2'>{0}</td> <td>{1}</td> <td>{2}</td> </tr>",
                        GetMaterialData(dtm, Convert.ToInt64(dt.Rows[j].ItemArray[0])), dt.Rows[j].ItemArray[1],
                        GetMaterialUnit(dtm, dtu, Convert.ToInt64(dt.Rows[j].ItemArray[0]))));
                }

                // завершение таблицы
                file.WriteLine("</table></p> <p> </p>");
            }
            // завершение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }
        #endregion

        #region Ведомости по потребностям в материалах (суммарно)
        /// <summary>
        /// Формирование ведомостей по файлам на каждый возможный период (месяц и год)
        /// </summary>
        public static string[] GenerateMaterialsNeeds2Reports()
        {
            // определение месяцов и лет, для которых надо выдать ведомость
            int count = 0;
            MNDates[] dates = GlobalFunctions.GetMNDatesCount(ref count);
            string[] res = new string[count];

            // для каждой даты выдача своей ведомости
            for (int i = 0; i < count; i++)
            {
                GenerateMaterialsNeeds2Report(dates[i], GlobalFunctions.GetMN2ReportFileName(dates[i]));
                res[i] = GlobalFunctions.GetMN2ReportFileName(dates[i]);
            }

            // возврат списка сгенерированный файлов
            return res;
        }

        public static void GenerateMaterialsNeeds2Report(MNDates date, string fname)
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(fname);

            // загрузка данных из ТБД "СТМ"
            DataTable dtm = DBWorker.SelectDataFromTable("СТМ");
            // загрузка данных из ТБД "СЕИ"
            DataTable dtu = DBWorker.SelectDataFromTable("СЕИ");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Сводная ведомость потребности в материалах</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .MyP1 {
	                                font-size: 24px;
	                                font-weight: bold;
	                                color: #900;
                                    }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine(String.Format("<h1 class='h1'>Сводная ведомость потребности в материалах на {0} месяц {1} года</h1>", GlobalFunctions.GetMonthString(date.Month), date.Year));

            // для каждого изделия формирование своей таблицы
            string str;
            DataTable dt;

            // формирование заголовка таблицы и самой таблицы
            file.WriteLine("<p align = 'center'><table border='1' cellspacing='0' width='90%'>");

            // формируем заголовок для материалов и их норм
            file.WriteLine("<tr> <th>Код, наименование материала</th> <th>Потребность в материале</th> <th>Единица измерения</th> </tr>");

            // выбираем сведения по материалам из таблицы "ПМТВ" для текущего изделия
            str = String.Format(@"SELECT
                                        MaterialsNeeds2.MaterialCode,
                                        MaterialsNeeds2.PlanCount
                                     FROM
                                        MaterialsNeeds2
                                     WHERE
                                        MaterialsNeeds2.Month = {0} and MaterialsNeeds2.Year = {1};", date.Month, date.Year);
            dt = DBWorker.dbf.Execute(str);

            // пишем данные о потребностях материалов в отчет
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                file.WriteLine(String.Format("<tr align='left'> <td>{0}</td> <td>{1}</td> <td>{2}</td> </tr>",
                                             GetMaterialData(dtm, Convert.ToInt64(dt.Rows[j].ItemArray[0])), dt.Rows[j].ItemArray[1],
                                             GetMaterialUnit(dtm, dtu, Convert.ToInt64(dt.Rows[j].ItemArray[0]))));
            }

            // завершение таблицы
            file.WriteLine("</table></p> <p> </p>");
            // завершение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }

        #endregion

        #region Ведомости по трудовому нормированию

        #region Сводная нормативная трудоемкость по изделиям
        /// <summary>
        /// Генерация сводной ведомости нормативной трудоемкости по изделиям
        /// </summary>
        public static void GenerateReportComplexity()
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(GlobalVars.AppDir + @"\Reports\ComplexityReport.html");

            // загрузка данных из ТБД "СНП"
            DataTable dtp = DBWorker.SelectDataFromTable("СНП");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Сводная ведомость нормативной трудоемкости по изделиям</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .MyP1 {
	                                font-size: 24px;
	                                font-weight: bold;
	                                color: #900;
                                    }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine("<h1 class='h1'>Сводная ведомость нормативной трудоемкости по изделиям</h1>");

            // формирование заголовка таблицы и самой таблицы
            file.WriteLine("<p align = 'center'><table border='1' cellspacing='0' width='90%'>");
            file.WriteLine("<tr> <th>Код изделия</th> <th>Наименование изделия</th> <th>То</th> <th>Тв</th> <th>Тпз</th> <th>Тотл</th> <th>Тпт</th> <th>Тобс</th> <th>Тшк</th> </tr>");

            // определение количества изделий (строк таблицы)
            int count = 0;
            long[] Prods = GetProdCountС(ref count);

            // выберем все данные из таблицы "СНТИ"
            DataTable dt = DBWorker.SelectDataFromTable("СНТИ");

            // для каждого изделия формируем строку со сведениями
            for (int i = 0; i < count; i++)
            {
                file.WriteLine(String.Format("<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> <td>{4}</td> <td>{5}</td> <td>{6}</td> <td>{7}</td> <td>{8}</td> </tr>",
                    dt.Rows[i].ItemArray[0], GetProductData(dtp, Convert.ToInt64(dt.Rows[i].ItemArray[0])), dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[2],
                    dt.Rows[i].ItemArray[3], dt.Rows[i].ItemArray[4], dt.Rows[i].ItemArray[5], dt.Rows[i].ItemArray[6], GetCalcProdTime(dt.Rows[i])));
            }

            // завершение таблицы
            file.WriteLine("</table></p> <p> </p>");
            // завершение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        } 
        #endregion

        #region Ведомость нормативной трудоемкости производственной программы
        /// <summary>
        /// Формирование ведомостей по файлам на каждый возможный период (месяц и год)
        /// </summary>
        public static string[] GenerateComplexityProgramReports()
        {
            // определение месяцов и лет, для которых надо выдать ведомость
            int count = 0;
            MNDates[] dates = GlobalFunctions.GetMNDatesCount(ref count);
            string[] res = new string[count];

            // для каждой даты выдача своей ведомости
            for (int i = 0; i < count; i++)
            {
                GenerateComplexityProgramReport(dates[i], GlobalFunctions.GetСPReportFileName(dates[i]));
                res[i] = GlobalFunctions.GetСPReportFileName(dates[i]);
            }

            // возврат списка сгенерированный файлов
            return res;
        }

        public static void GenerateComplexityProgramReport(MNDates date, string fname)
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(fname);

            // загрузка данных из ТБД "СНП"
            DataTable dtp = DBWorker.SelectDataFromTable("СНП");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Ведомость нормативной трудоемкости производственной программы</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .MyP1 {
	                                font-size: 24px;
	                                font-weight: bold;
	                                color: #900;
                                    }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine(String.Format("<h1 class='h1'>Ведомость нормативной трудоемкости производственной программы на {0} месяц {1} года</h1>", GlobalFunctions.GetMonthString(date.Month), date.Year));

            // формирование заголовка таблицы и самой таблицы
            file.WriteLine("<p align = 'center'><table border='1' cellspacing='0' width='90%'>");

            // формируем заголовок для таблицы
            file.WriteLine("<tr class='MyP1'> <th>Код изделия</th> <th>Наименование изделия</th> <th>План выпуска</th> <th>Трудоемкость на изделие</th> <th>Трудоемкость по плану</th> </tr>");

            // выборка данных из таблицы "НТПП"
            string str = String.Format(@"SELECT
                                            ComplexityProgram.ProductCode,
                                            ComplexityProgram.PlanCount,
                                            ComplexityProgram.Tshk,
                                            ComplexityProgram.Tshk * ComplexityProgram.PlanCount
                                        FROM
                                            ComplexityProgram
                                        WHERE
                                            ComplexityProgram.Month = {0} and ComplexityProgram.Year = {1};", date.Month, date.Year);
            DataTable dt = DBWorker.dbf.Execute(str);

            // итоговая трудоемкость по плану
            float sum = 0, tmp;

            // формирование строк таблицы
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmp = Convert.ToSingle(dt.Rows[i].ItemArray[3]);
                file.WriteLine(String.Format("<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> <td>{4}</td> </tr>", dt.Rows[i].ItemArray[0],
                    GetProductData2(dtp, dt.Rows[i].Field<long>(0)), dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[2],
                    tmp));
                // подсчет трудоемкости по плану для итогов
                sum += tmp;
            }

            // вывод итогов в конце таблицы
            file.WriteLine(String.Format("<tr> <th>Итого:</th> <td colspan='3' align='center'>-</td> <td>{0}</td> </tr>", sum));

            // завершение таблицы
            file.WriteLine("</table></p> <p> </p>");
            // завершение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }
        #endregion

        #region Ведомость нормативной трудоемкости производственной программы по цехам
        /// <summary>
        /// Формирование ведомостей по файлам на каждый возможный период (месяц и год)
        /// </summary>
        public static string[] GenerateComplexityShopProgramReports()
        {
            // определение месяцов и лет, для которых надо выдать ведомость
            int count = 0;
            MNDates[] dates = GlobalFunctions.GetMNDatesCount(ref count);
            string[] res = new string[count];

            // для каждой даты выдача своей ведомости
            for (int i = 0; i < count; i++)
            {
                GenerateComplexityShopProgramReport(dates[i], GlobalFunctions.GetСSPReportFileName(dates[i]));
                res[i] = GlobalFunctions.GetСSPReportFileName(dates[i]);
            }

            // возврат списка сгенерированный файлов
            return res;
        }

        public static void GenerateComplexityShopProgramReport(MNDates date, string fname)
        {
            // создание файла и запись в него ведомости
            StreamWriter file = new StreamWriter(fname);

            // загрузка данных из ТБД "СНП"
            DataTable dtp = DBWorker.SelectDataFromTable("СНП");

            // формирование заголовка HTML файла
            string Header = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                              <html xmlns='http://www.w3.org/1999/xhtml'>
                              <head>
                              <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                              <title>Ведомость нормативной трудоемкости производственной программы</title>
                              <style type='text/css'>
                              .h1 {
                                    text-align: center;
	                                font-family: 'Times New Roman', Times, serif;
	                                color: #008;
	                                text-decoration: underline;
                                  }
                              .MyP1 {
	                                font-size: 24px;
	                                font-weight: bold;
	                                color: #900;
                                    }
                              </style>
                              </head>";
            file.WriteLine(Header);

            // начало тела HTML файла
            file.WriteLine("<body>");

            // формирование заголовка отчета
            file.WriteLine(String.Format("<h1 class='h1'>Ведомость нормативной трудоемкости производственной программы на {0} месяц {1} года</h1>", GlobalFunctions.GetMonthString(date.Month), date.Year));

            // формирование заголовка таблицы и самой таблицы
            file.WriteLine("<p align = 'center'><table border='1' cellspacing='0' width='90%'>");

            // формируем заголовок для таблицы
            file.WriteLine("<tr class='MyP1'> <th>Код, наименование изделия</th> <th>Код, наименование цеха</th> <th>План выпуска</th> <th>Трудоемкость на изделие</th> <th>Трудоемкость по плану</th> </tr>");

            // выборка данных из таблицы "НТППЦ" по запросу с сортировкой
            string str = String.Format(@"SELECT
                                            ComplexityShopProgram.ProductCode,
                                            ComplexityShopProgram.ShopCode,
                                            ComplexityShopProgram.PlanCount,
                                            ComplexityShopProgram.Tshk,
                                            ComplexityShopProgram.Tshk * ComplexityShopProgram.PlanCount AS PlanTshk
                                        FROM
                                            ComplexityShopProgram
                                        WHERE ComplexityShopProgram.Month = {0} and ComplexityShopProgram.Year = {1}
                                        ORDER BY ComplexityShopProgram.ProductCode;", date.Month, date.Year);
            DataTable dt = DBWorker.dbf.Execute(str);

            // итоговая трудоемкость по плану
            float sump = 0, sumall = 0, tmp;

            // формирование строк таблицы
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != dt.Rows.Count - 1)
                {
                    tmp = Convert.ToSingle(dt.Rows[i].ItemArray[4]);
                    file.WriteLine(String.Format("<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> <td>{4}</td> </tr>",
                                                 GetProductData(dtp, dt.Rows[i].Field<long>(0)), dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[2],
                                                 dt.Rows[i].ItemArray[3], tmp));
                    // подсчет трудоемкости по плану для итогов
                    sump += tmp;

                    // подсчет полных итогов по всем
                    sumall += tmp;

                    if (i != 0)
                        if (dt.Rows[i].Field<long>(0) != dt.Rows[i + 1].Field<long>(0)) // если далее следует новое изделие, то подводим итог по текущему изделию
                        {
                            file.WriteLine(String.Format("<tr> <th>Итого по изделию:</th> <td colspan='3' align='center'>-</td> <td>{0}</td> </tr>", sump));
                            sump = 0; // обнуляем трудоемкость по изделию
                        }
                }
                else
                {
                    tmp = Convert.ToSingle(dt.Rows[i].ItemArray[4]);
                    file.WriteLine(String.Format("<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> <td>{4}</td> </tr>",
                                                 GetProductData(dtp, dt.Rows[i].Field<long>(0)), dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[2],
                                                 dt.Rows[i].ItemArray[3], tmp));
                    // подсчет трудоемкости по плану для итогов
                    sump += tmp;

                    // подсчет полных итогов по всем
                    sumall += tmp;

                    // подводим итог по последнему изделию в списке
                    file.WriteLine(String.Format("<tr> <th>Итого по изделию:</th> <td colspan='3' align='center'>-</td> <td>{0}</td> </tr>", sump));
                    sump = 0; // обнуляем трудоемкость по изделию
                }
            }

            // вывод итогов в конце таблицы
            file.WriteLine(String.Format("<tr> <th>Итого:</th> <td colspan='3' align='center'>-</td> <td>{0}</td> </tr>", sumall));

            // завершение таблицы
            file.WriteLine("</table></p> <p> </p>");
            // завершение тела HTML файла
            file.WriteLine("</p></body></html>");

            // закрытие файла
            file.Close();
        }
        #endregion

        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Определение числа корневых изделий по ТБД "ПП"
        /// </summary>
        /// <param name="count">Искомое количество изделей</param>
        /// <returns>Коды найденных корневых изделий</returns>
        private static long[] GetProdCount(ref int count)
        {
            string str = "SELECT DISTINCT FullApplication.ProductCode FROM FullApplication";
            DataTable dt = DBWorker.dbf.Execute(str);

            count = dt.Rows.Count;
            long[] res = new long[count];
            for (int i = 0; i < count; i++)
                res[i] = dt.Rows[i].Field<long>(0);
            return res;
        }

        /// <summary>
        /// Определение количества и кодов изделий, выпускаемых в текущем месяце по дате
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static long[] GetProdCountInMonth(ref int count, out int[] plans, MNDates date)
        {
            // выборка данных по условию из ТБД "ПВИ"
            string str = String.Format("SELECT DISTINCT PlanProducts.ProductCode, PlanProducts.PlanCount FROM PlanProducts WHERE PlanProducts.Month = {0} and PlanProducts.Year = {1};",
                date.Month, date.Year);
            DataTable dt = DBWorker.dbf.Execute(str);

            count = dt.Rows.Count;
            long[] res = new long[count]; // коды изделий
            plans = new int[count]; // кол-во по плану для изделий
            for (int i = 0; i < count; i++)
            {
                res[i] = dt.Rows[i].Field<long>(0);
                plans[i] = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
            }
            return res;
        }

        /// <summary>
        /// Определение количества уникальных изделий по плану выпуска (из ТБД "ПВИ")
        /// </summary>
        /// <param name="count">Количество найденных изделий</param>
        /// <returns>Возвращает коды найденных изделий</returns>
        private static long[] GetProdCountPP(ref int count)
        {
        	// выборка всех строк из таблицы "ПВИ"
            string str = "SELECT PlanProducts.ProductCode FROM PlanProducts";
            DataTable dt = DBWorker.dbf.Execute(str);

            count = dt.Rows.Count;
            long[] res = new long[count]; // коды изделий
            for (int i = 0; i < count; i++)
                res[i] = dt.Rows[i].Field<long>(0);
            return res;
        }

        /// <summary>
        /// Определение количества уникальных изделий (из ТБД "СНТИ")
        /// </summary>
        /// <param name="count">Количество найденных изделий</param>
        /// <returns>Возвращает коды найденных изделий</returns>
        private static long[] GetProdCountС(ref int count)
        {
            // выборка всех строк из таблицы "ПВИ"
            string str = "SELECT Complexity.ProductCode FROM Complexity";
            DataTable dt = DBWorker.dbf.Execute(str);

            count = dt.Rows.Count;
            long[] res = new long[count]; // коды изделий
            for (int i = 0; i < count; i++)
                res[i] = dt.Rows[i].Field<long>(0);
            return res;
        }

        /// <summary>
        /// Определение количества используемых материалов в изделии
        /// </summary>
        /// <param name="code">Код изделия</param>
        /// <param name="count">Количество материалов</param>
        /// <returns>Возвращает массив кодов матариалов</returns>
        private static long[] GetMaterialsCountForProduct(long code, out int count)
        {
            string str = String.Format("SELECT DISTINCT DetMatOnProd.MaterialCode FROM DetMatOnProd WHERE DetMatOnProd.ProductCode = {0}", code);
            DataTable dt = DBWorker.dbf.Execute(str);

            count = dt.Rows.Count;
            long[] res = new long[count];
            for (int i = 0; i < count; i++)
                res[i] = dt.Rows[i].Field<long>(0);
            return res;
        }

        /// <summary>
        /// Построение строки с кодом и наименованием материала
        /// </summary>
        /// <param name="dt">Данные о всех материалах</param>
        /// <param name="matcode">Код материала</param>
        /// <returns>Возвращает строку с кодом и наименованием материала</returns>
        private static string GetMaterialData(DataTable dt, long matcode)
        {
            string str = "";

            for (int i = 0; i < dt.Rows.Count; i++)
                if ((long)dt.Rows[i].ItemArray[0] == matcode)
                {
                    str += String.Format("{0} {1}", matcode, dt.Rows[i].ItemArray[1]);
                    break;
                }

            return str;
        }

        /// <summary>
        /// Определение единицы измерения для материала
        /// </summary>
        /// <param name="dtm">Список материалов</param>
        /// <param name="dtu">Список единиц измерения</param>
        /// <param name="matcode">Код материала</param>
        /// <returns>Возвращает сокращенное обозначение единицы измерения для матриала</returns>
        private static string GetMaterialUnit(DataTable dtm, DataTable dtu, long matcode)
        {
            string str = "";

            // определим код единицы измерения
            int unitcode = -1;
            for (int i = 0; i < dtm.Rows.Count; i++)
                if ((long)dtm.Rows[i].ItemArray[0] == matcode)
                {
                    unitcode = Convert.ToInt32(dtm.Rows[i].ItemArray[2]);
                    break;
                }
            // определим название сокращенное единицы измерения
            for (int j = 0; j < dtu.Rows.Count; j++)
                if (Convert.ToInt32(dtu.Rows[j].ItemArray[0]) == unitcode)
                {
                    str += dtu.Rows[j].ItemArray[2];
                    break;
                }

            return str;
        }

        /// <summary>
        /// Построение строки с кодом, наименованием и обозначением детали
        /// </summary>
        /// <param name="dt">Данные о всех деталях</param>
        /// <param name="prodcode">Код детали</param>
        /// <returns>Возвращает строку с описанием детали</returns>
        private static string GetProductData(DataTable dt, long prodcode)
        {
            string str = "";

            for (int i = 0; i < dt.Rows.Count; i++)
                if ((long)dt.Rows[i].ItemArray[0] == prodcode)
                {
                    str += String.Format("{0} {1} {2}", prodcode, dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[2]);
                    break;
                }

            return str;
        }

        /// <summary>
        /// Построение строки с наименованием и обозначением детали
        /// </summary>
        /// <param name="dt">Данные о всех деталях</param>
        /// <param name="prodcode">Код детали</param>
        /// <returns>Возвращает строку с описанием детали</returns>
        private static string GetProductData2(DataTable dt, long prodcode)
        {
            string str = "";

            for (int i = 0; i < dt.Rows.Count; i++)
                if ((long)dt.Rows[i].ItemArray[0] == prodcode)
                {
                    str += String.Format("{0} {1}", dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[2]);
                    break;
                }

            return str;
        }

        /// <summary>
        /// Подсчет штучно-калькуляционного времени (Тшк) 
        /// </summary>
        /// <param name="dataRow">Строка таблицы с данными</param>
        /// <returns>Возвращает значение Тшк</returns>
        private static float GetCalcProdTime(DataRow dataRow)
        {
            float sum = 0;

            for (int i = 1; i < 7; i++)
                sum += Convert.ToSingle(dataRow.ItemArray[i]);

            return sum;
        }
        #endregion
    }
}
