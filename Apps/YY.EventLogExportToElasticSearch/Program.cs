﻿using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Nest;
using YY.EventLogExportAssistant;
using YY.EventLogExportAssistant.ElasticSearch;

namespace YY.EventLogExportToElasticSearch
{
    class Program
    {
        #region Private Static Member Variables

        private static long _totalRows;
        private static long _lastPortionRows;
        private static DateTime _beginPortionExport;
        private static DateTime _endPortionExport;

        #endregion

        #region Static Methods

        static void Main()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IConfigurationSection eventLogSection = Configuration.GetSection("EventLog");
            string eventLogPath = eventLogSection.GetValue("SourcePath", string.Empty);
            int watchPeriodSeconds = eventLogSection.GetValue("WatchPeriod", 60);
            int watchPeriodSecondsMs = watchPeriodSeconds * 1000;
            bool useWatchMode = eventLogSection.GetValue("UseWatchMode", false);
            int portion = eventLogSection.GetValue("Portion", 1000);

            IConfigurationSection inforamtionSystemSection = Configuration.GetSection("InformationSystem");
            string inforamtionSystemName = inforamtionSystemSection.GetValue("Name", string.Empty);
            string inforamtionSystemDescription = inforamtionSystemSection.GetValue("Description", string.Empty);

            IConfigurationSection elasticSearchSection = Configuration.GetSection("ElasticSearch");
            Uri nodeAddress = elasticSearchSection.GetValue<Uri>("Node");
            string indexName = elasticSearchSection.GetValue<string>("IndexName");
            int maximumRetries = elasticSearchSection.GetValue<int>("MaximumRetries");
            int maxRetryTimeout = elasticSearchSection.GetValue<int>("MaxRetryTimeout");


            if (string.IsNullOrEmpty(eventLogPath))
            {
                Console.WriteLine("Не указан каталог с файлами данных журнала регистрации.");
                Console.WriteLine("Для выхода нажмите любую клавишу...");
                Console.Read();
                return;
            }

            Console.WriteLine();
            Console.WriteLine();

            ConnectionSettings elasticSettings = new ConnectionSettings(nodeAddress)
                .DefaultIndex(indexName)
                .MaximumRetries(maximumRetries)
                .MaxRetryTimeout(TimeSpan.FromSeconds(maxRetryTimeout));

            EventLogExportMaster exporter = new EventLogExportMaster();
            exporter.SetEventLogPath(eventLogPath);

            EventLogOnElasticSearch target = new EventLogOnElasticSearch(elasticSettings, portion);
            target.SetInformationSystem(new InformationSystemsBase()
            {
                Name = inforamtionSystemName,
                Description = inforamtionSystemDescription
            });
            exporter.SetTarget(target);

            exporter.BeforeExportData += BeforeExportData;
            exporter.AfterExportData += AfterExportData;
            exporter.OnErrorExportData += OnErrorExportData;

            if (useWatchMode)
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                        if (Console.ReadKey().KeyChar == 'q')
                            break;

                    while (exporter.NewDataAvailiable())
                    {
                        exporter.SendData();
                        Thread.Sleep(watchPeriodSecondsMs);
                    }
                }
            }
            else
                while (exporter.NewDataAvailiable())
                    exporter.SendData();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.Read();
        }

        #endregion

        #region Events

        private static void BeforeExportData(BeforeExportDataEventArgs e)
        {
            _beginPortionExport = DateTime.Now;
            _lastPortionRows = e.Rows.Count;
            _totalRows += e.Rows.Count;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("[{0}] Last read: {1}                         ", DateTime.Now, e.Rows.Count);
        }
        private static void AfterExportData(AfterExportDataEventArgs e)
        {
            _endPortionExport = DateTime.Now;
            var duration = _endPortionExport - _beginPortionExport;

            Console.WriteLine("[{0}] Total read: {1}                        ", DateTime.Now, _totalRows);
            Console.WriteLine("[{0}] {1} / {2} (sec.)                       ", DateTime.Now, _lastPortionRows, duration.TotalSeconds);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите 'q' для завершения отслеживания изменений...");
        }

        private static void OnErrorExportData(OnErrorExportDataEventArgs e)
        {
            Console.WriteLine(
                "Ошибка при экспорте данных." +
                "Критическая: {0}\n" +
                "\n" +
                "Содержимое события:\n" +
                "{1}" +
                "\n" +
                "Информация об ошибке:\n" +
                "\n" +
                "{2}",
                e.Critical, e.SourceData, e.Exception);
        }

        #endregion
    }
}