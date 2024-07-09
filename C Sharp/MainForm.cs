using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace SuperstoreForecast
{
    public partial class MainForm : Form
    {
        private string filePath = @"C:\Users\arunb\Documents\C Sharp\SP_Coding_Exercise_Dataset_(1).xlsx";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var dataTable = new DataTable();

                // Assume first row contains column names
                bool firstRow = true;
                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        foreach (var cell in row.Cells())
                        {
                            dataTable.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        dataTable.Rows.Add();
                        int i = 0;
                        foreach (var cell in row.Cells())
                        {
                            dataTable.Rows[dataTable.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                // Bind data to a DataGridView or other UI elements
                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
