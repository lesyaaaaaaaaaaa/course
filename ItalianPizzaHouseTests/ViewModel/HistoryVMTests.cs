using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItalianPizzaHouse.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItalianPizzaHouse.Model;

namespace ItalianPizzaHouse.ViewModel.Tests
{
    [TestClass()]
    public class HistoryVMTests
    {
        Core db = new Core();

        /// <summary>
        /// Проверяет, что метод EventHappened успешно добавляет новую запись в журнал событий.
        /// </summary>
        [TestMethod()]
        public void EventHappened_AddsNewHistoryEntry()
        {

            var userId = 1;
            var eventDate = DateTime.Now;
            var eventType = "Test event";

            HistoryVM.EventHappened(eventDate, eventType, userId);
            var historyEntry = db.context.Histories.Where(x => x.HistoryText == eventType).FirstOrDefault();

            Assert.IsNotNull(historyEntry);
            Assert.AreEqual(userId, historyEntry.UserId);
            Assert.AreEqual(eventDate.Date, historyEntry.DateHistory.Date);
            Assert.AreEqual(eventType, historyEntry.HistoryText);
            
        }
    }
}