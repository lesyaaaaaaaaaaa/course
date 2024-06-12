using ClosedXML.Excel;
using ItalianPizzaHouse.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ItalianPizzaHouse.View
{
    /// <summary>
    /// Interaction logic for EventLogPage.xaml
    /// </summary>
    public partial class EventLogPage : Page
    {
        Core db = new Core();
        public EventLogPage()
        {
            InitializeComponent();
            LoadEventLog();
        }

        private void LoadEventLog(DateTime? startDate = null, DateTime? endDate = null)
        {
            var events = db.context.Histories.AsQueryable();

            if (startDate.HasValue)
                events = events.Where(e => e.DateHistory >= startDate.Value);

            if (endDate.HasValue)
                events = events.Where(e => e.DateHistory <= endDate.Value);

            // Убираем GroupBy() и материализуем данные с помощью ToList()
            EventLogDataGrid.ItemsSource = events.ToList();
        }

        private void FilterEvents(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            LoadEventLog(startDate, endDate);
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Журнал событий");

                // 2. Записываем заголовки столбцов
                for (int col = 1; col <= EventLogDataGrid.Columns.Count; col++)
                {
                    worksheet.Cell(1, col).Value = EventLogDataGrid.Columns[col - 1].Header.ToString(); // Преобразуем в строку
                    worksheet.Cell(1, col).Style.Font.Bold = true;
                }

                // 3. Записываем данные из DataGrid
                for (int row = 0; row < EventLogDataGrid.Items.Count; row++)
                {
                    var item = EventLogDataGrid.Items[row] as Histories; // Приводим к типу History

                    if (item != null) // Проверяем на null
                    {
                        worksheet.Cell(row + 2, 1).Value = item.IdHistory;
                        worksheet.Cell(row + 2, 2).Value = item.UserId;
                        worksheet.Cell(row + 2, 3).Value = item.HistoryText;
                        worksheet.Cell(row + 2, 4).Value = item.DateHistory.ToString("dd.MM.yyyy HH:mm:ss"); // Форматируем дату
                    }
                }

                // 4. Сохраняем файл
                SaveFileDialog saveFileDialog = new SaveFileDialog(); 
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"EventLog_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Экспорт в Excel выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
