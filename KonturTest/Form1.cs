using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace KonturTest
{
    public partial class Form1 : Form
    {
        // Поле для хранения имени текущего файла
        private string _currentFile = "Data1.xml";

        private readonly Dictionary<string, (int Order, string RuName)> _monthMap =
            new Dictionary<string, (int, string)>(StringComparer.OrdinalIgnoreCase)
        {
            { "january",   (1, "Январь") },
            { "february",  (2, "Февраль") },
            { "march",     (3, "Март") },
            { "april",     (4, "Апрель") },
            { "may",       (5, "Май") },
            { "june",      (6, "Июнь") },
            { "july",      (7, "Июль") },
            { "august",    (8, "Август") },
            { "september", (9, "Сентябрь") },
            { "october",   (10, "Октябрь") },
            { "november",  (11, "Ноябрь") },
            { "december",  (12, "Декабрь") }
        };
         
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Определяем файл при старте, чтобы знать куда добавлять
            if (File.Exists("Data1.xml")) _currentFile = "Data1.xml";
            else if (File.Exists("Data2.xml")) _currentFile = "Data2.xml";
        }

        private decimal ParseAmount(string amountStr)
        {
            if (string.IsNullOrEmpty(amountStr)) return 0;
            string clean = amountStr.Replace(',', '.').Trim();
            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                return result;
            return 0;
        }

        // Основная логика вынесена в отдельный метод
        private void RunCalculation()
        {
            try
            {
                richTextBox1.Clear();

                // Проверка файла (на случай если его удалили)
                if (!File.Exists(_currentFile))
                {
                    if (File.Exists("Data1.xml")) _currentFile = "Data1.xml";
                    else if (File.Exists("Data2.xml")) _currentFile = "Data2.xml";
                    else
                    {
                        MessageBox.Show("Файлы данных не найдены!");
                        return;
                    }
                }

                richTextBox1.AppendText($"Работаем с файлом: {_currentFile}\n");

                // 1. XSLT
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load("transform.xslt");
                xslt.Transform(_currentFile, "Employees.xml");

                // 2. Досчет Employees.xml
                XDocument empDoc = XDocument.Load("Employees.xml");
                foreach (var emp in empDoc.Descendants("Employee"))
                {
                    decimal totalSalary = 0;
                    foreach (var salary in emp.Elements("salary"))
                    {
                        totalSalary += ParseAmount(salary.Attribute("amount")?.Value);
                    }
                    emp.SetAttributeValue("SumSalary", totalSalary.ToString(CultureInfo.InvariantCulture));
                }
                empDoc.Save("Employees.xml");

                // 3. Досчет исходного файла
                XDocument dataDoc = XDocument.Load(_currentFile);
                var payElement = dataDoc.Root;
                decimal totalPay = 0;
                foreach (var item in dataDoc.Descendants("item"))
                {
                    totalPay += ParseAmount(item.Attribute("amount")?.Value);
                }
                payElement.SetAttributeValue("SumAmount", totalPay.ToString(CultureInfo.InvariantCulture));
                dataDoc.Save(_currentFile);

                // 4. Вывод отчета
                richTextBox1.AppendText($"Общая сумма выплат: {totalPay:N2}\n\n");

                // Сотрудники
                richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
                richTextBox1.AppendText("=== СПИСОК СОТРУДНИКОВ ===\n");
                richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);

                foreach (var emp in empDoc.Descendants("Employee"))
                {
                    string name = emp.Attribute("name")?.Value;
                    string surname = emp.Attribute("surname")?.Value;
                    decimal sum = ParseAmount(emp.Attribute("SumSalary")?.Value);
                    richTextBox1.AppendText($"{name} {surname}: {sum:N2}\n");
                }

                richTextBox1.AppendText("\n");

                // Месяца
                richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
                richTextBox1.AppendText("=== ВЫПЛАТЫ ПО МЕСЯЦАМ ===\n");
                richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);

                var monthStats = empDoc.Descendants("salary")
                    .GroupBy(x => x.Attribute("mount")?.Value.ToLower().Trim())
                    .Select(g => new
                    {
                        RawMonth = g.Key,
                        Total = g.Sum(x => ParseAmount(x.Attribute("amount")?.Value))
                    })
                    .OrderBy(x => _monthMap.ContainsKey(x.RawMonth) ? _monthMap[x.RawMonth].Order : 13);

                foreach (var stat in monthStats)
                {
                    string displayMonth = _monthMap.ContainsKey(stat.RawMonth)
                        ? _monthMap[stat.RawMonth].RuName
                        : stat.RawMonth;
                    richTextBox1.AppendText($"{displayMonth,-10}: {stat.Total:N2}\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Кнопка Рассчитать 
        private void button1_Click(object sender, EventArgs e)
        {
            RunCalculation();
        }

        // Кнопка Добавить 
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text)
                    || string.IsNullOrWhiteSpace(txtAmount.Text) || string.IsNullOrWhiteSpace(txtMonth.Text))
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }

                // Загружаем текущий файл
                if (!File.Exists(_currentFile))
                {
                    MessageBox.Show("Файл данных не найден.");
                    return;
                }

                XDocument doc = XDocument.Load(_currentFile);

                // Создаем новый элемент item
                XElement newItem = new XElement("item");
                newItem.SetAttributeValue("name", txtName.Text);
                newItem.SetAttributeValue("surname", txtSurname.Text);

                // Приводим сумму к инвариантному виду для XML
                decimal amountVal = ParseAmount(txtAmount.Text);
                newItem.SetAttributeValue("amount", amountVal.ToString(CultureInfo.InvariantCulture));

                newItem.SetAttributeValue("mount", txtMonth.Text.ToLower());

                // Логика добавления зависит от структуры файла (Data1 - плоский, Data2 - вложенный)
                // Проверяем, есть ли в корне элементы-месяцы (как в Data2)
                bool isNestedStructure = doc.Root.Elements().Any(el => _monthMap.ContainsKey(el.Name.LocalName));

                if (!isNestedStructure)
                {
                    // Data1: просто добавляем в корень Pay
                    doc.Root.Add(newItem);
                }
                else
                {
                    // Data2: ищем тег месяца
                    string monthTag = txtMonth.Text.ToLower();
                    XElement monthElement = doc.Root.Element(monthTag);

                    if (monthElement == null)
                    {
                        // Если месяца нет, создаем его
                        monthElement = new XElement(monthTag);
                        doc.Root.Add(monthElement);
                    }
                    monthElement.Add(newItem);
                }

                doc.Save(_currentFile);

                MessageBox.Show("Сотрудник добавлен! Пересчитываем...");

                // Очищаем поля
                txtName.Clear(); txtSurname.Clear(); txtAmount.Clear(); txtMonth.Clear();

                // Автоматический пересчет
                RunCalculation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
// Готово к проверке