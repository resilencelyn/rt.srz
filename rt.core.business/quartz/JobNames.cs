using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.core.business.quartz
{
    public class JobNames
    {
        /// <summary>
        /// Константа
        /// </summary>
        public const string InitializationOfListeners = "Инициализация прослушивателей файловой системы";

        /// <summary>
        /// Константа
        /// </summary>
        public const string PacketLoading = "Загрузка пакетов";

        /// <summary>
        /// Константа
        /// </summary>
        public const string BackupDatabase = "Бэкап базы данных";
    }
}
