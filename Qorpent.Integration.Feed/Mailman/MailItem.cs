using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qorpent.Integration.Feed.Mailman
{
    /// <summary>
    /// 
    /// </summary>
    public class MailItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Время записи
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Текс
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Код формы
        /// </summary>
        public string FormCode { get; set; }
        /// <summary>
        /// Предприятие
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Период
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Получатель
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MetaData { get; set; }

    }
}
