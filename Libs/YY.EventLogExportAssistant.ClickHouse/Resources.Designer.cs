﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YY.EventLogExportAssistant.ClickHouse {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YY.EventLogExportAssistant.ClickHouse.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на CREATE TABLE IF NOT EXISTS LogFiles
        ///(
        ///	InformationSystem LowCardinality(String),
        ///	Id Int64 Codec(DoubleDelta, LZ4),
        ///	FileName LowCardinality(String),
        ///	CreateDate DateTime Codec(Delta, LZ4),
        ///	ModificationDate DateTime Codec(Delta, LZ4),
        ///	LastEventNumber Int64 Codec(DoubleDelta, LZ4),
        ///	LastCurrentFileReferences LowCardinality(String),
        ///	LastCurrentFileData LowCardinality(String),
        ///	LastStreamPosition Int64 Codec(DoubleDelta, LZ4)
        ///)
        ///engine = MergeTree()
        ///PARTITION BY (InformationSystem, toYYYYMM(Crea [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string Query_CreateTable_LogFiles {
            get {
                return ResourceManager.GetString("Query_CreateTable_LogFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на CREATE TABLE IF NOT EXISTS RowsData
        ///(
        ///    InformationSystem LowCardinality(String),
        ///    Id Int64 Codec(DoubleDelta, LZ4),
        ///    Period DateTime Codec(Delta, LZ4),
        ///    Severity LowCardinality(String),
        ///    ConnectId Int64 Codec(DoubleDelta, LZ4),
        ///    Session Int64 Codec(DoubleDelta, LZ4),
        ///    TransactionStatus LowCardinality(String),
        ///    TransactionDate DateTime Codec(Delta, LZ4),
        ///    TransactionId Int64 Codec(DoubleDelta, LZ4),
        ///    User LowCardinality(String),
        ///    UserUUID LowCardinality(String),
        /// [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string Query_CreateTable_RowsData {
            get {
                return ResourceManager.GetString("Query_CreateTable_RowsData", resourceCulture);
            }
        }
    }
}
