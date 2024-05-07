using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTester
{
    public partial class frmTester : Form
    {
        OleDbConnection conn;


        public frmTester()
        {
            InitializeComponent();
        }

        private void frmTester_Load(object sender, EventArgs e)
        {
            var connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DB\Books.accdb;
                                Persist Security Info=False;";
            conn = new OleDbConnection(connString);
            conn.Open();

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            OleDbCommand command = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable table = new DataTable();

            try
            {
                command = new OleDbCommand(txtCommand.Text, conn);
                adapter.SelectCommand = command;
                adapter.Fill(table);

                grdRecord.DataSource = table;
                lblCount.Text = table.Rows.Count.ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error In SQL Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            command.Dispose();
            adapter.Dispose();
            table.Dispose();
        }

        private void frmFormClosing(object sender, FormClosedEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }
    }
}


//Example COde Execution MYSQL:
/***
 * SELECT a.Author, t.Title, t.Year_Published
FROM 
((Titles AS t
INNER JOIN Title_Author AS ta ON ta.ISBN = t.ISBN)
INNER JOIN Authors AS a ON a.Au_ID = ta.Au_ID)
INNER JOIN Publishers AS p ON p.PubID = t.PubID
WHERE t.Year_Published = 1988 and p.Name = "WEST"
***/

/*
SELECT a.Author, t.Title, t.Year_Published
FROM 
((Titles AS t
INNER JOIN Title_Author AS ta ON ta.ISBN = t.ISBN)
INNER JOIN Authors AS a ON a.Au_ID = ta.Au_ID)
INNER JOIN Publishers AS p ON p.PubID = t.PubID
WHERE t.Year_Published = 1988 and p.Name = "WEST"
ORDER BY a.Author DESC
*/