using ItalianPizzaHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizzaHouse.ViewModel
{
    public class HistoryVM
    {
        static Core db = new Core();

        /// <summary>
        /// Добавляет новую запись о событии в журнал.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, вызвавшего событие.</param>
        /// <param name="eventDate">Дата и время события.</param>
        /// <param name="eventType">Описание типа события (по умолчанию "Неопознанное событие").</param>
        public static void EventHappened(DateTime eventDate, string eventType = "Неопознанное событие", int userId = 1)
        {
            if(userId == 0)
            {
                userId = 1;
            }
            Histories newHistoryEntry = new Histories()
            {
                UserId = userId,
                DateHistory = eventDate,
                HistoryText = eventType
            };
            db.context.Histories.Add(newHistoryEntry);
            db.context.SaveChanges();
            }
    }
}
