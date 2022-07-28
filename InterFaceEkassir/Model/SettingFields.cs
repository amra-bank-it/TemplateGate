using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Model
{
    /// <summary>
    /// Контейнер работы с полями настройки приложения при инициализации
    /// </summary>
    public class SettingFields
    {
        public int TraceLog { get; set; }
        public string SentryUrl { get; set; }
    }
}