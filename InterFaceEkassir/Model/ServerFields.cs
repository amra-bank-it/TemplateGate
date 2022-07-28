using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Model
{
    /// <summary>
    /// Контейнер работы с полями сервера по умолчанию
    /// </summary>
    public class ServerFields
    {
        public string Account { get; set; }
        public decimal Value { get; set; }
        public decimal Total { get; set; }
        public decimal Fee { get; set; }
        public decimal Amount { get; set; }
        public long Serial { get; set; }
        public DateTime ServerTime { get; set; }
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string PointName { get; set; }
    }
}
