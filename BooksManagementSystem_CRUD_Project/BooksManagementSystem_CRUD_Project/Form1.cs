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

namespace BooksManagementSystem_CRUD_Project
{
    public partial class frmAuthors : Form
    {
        public frmAuthors()
        {
            InitializeComponent();
        }

        OleDbConnection booksConn;
        OleDbCommand authorsComm;
        OleDbDataAdapter authorsAdapter;
        DataTable authorsTable;
        CurrencyManager authorsManager;
        OleDbCommandBuilder builderComm;
        bool dbError = false;
        private string AppState { get; set; }
        private int CurrentPosition { get; set; }

        private void frmAuthors_Load(object sender, EventArgs e)
        {
            try
            {
                var connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DB\Books.accdb;
                                Persist Security Info=False;";
                booksConn = new OleDbConnection(connString);
                booksConn.Open();
                authorsComm = new OleDbCommand("SELECT * FROM Authors ORDER BY Author", booksConn);
                authorsAdapter = new OleDbDataAdapter();
                authorsTable = new DataTable();
                authorsAdapter.SelectCommand = authorsComm;
                authorsAdapter.Fill(authorsTable);
                //binding
                txtAuthorID.DataBindings.Add("Text", authorsTable, "AU_ID");
                txtAuthorName.DataBindings.Add("Text", authorsTable, "Author");
                txtAuthorBorn.DataBindings.Add("Text", authorsTable, "Year_Born");
                authorsManager = (CurrencyManager)BindingContext[authorsTable];
                SetAppState("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbError = true;
            }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            authorsManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            authorsManager.Position++;
        }

        private void frmClosing(object sender, FormClosingEventArgs e)
        {
            if (!dbError)
            {
                //close connection and dispose of all the objects
                booksConn.Close();
                booksConn.Dispose();
                authorsComm.Dispose();
                authorsAdapter.Dispose();
                authorsTable.Dispose();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                var savedRecord = txtAuthorName.Text; // name that is in the textbox
                authorsManager.EndCurrentEdit();
                builderComm = new OleDbCommandBuilder(authorsAdapter);
                //authorsAdapter.Update(authorsTable);

                if (AppState == "Edit")
                {
                    var authRow = authorsTable.Select("Au_ID = " + txtAuthorID.Text); // returns an array of Au_ID, Author, and Year_Born
                    if (String.IsNullOrEmpty(txtAuthorBorn.Text))
                    {
                        authRow[0]["Year_Born"] = DBNull.Value;
                    }
                    else
                    {
                        authRow[0]["Year_Born"] = txtAuthorBorn.Text;
                    }

                    authorsAdapter.Update(authorsTable);
                    txtAuthorBorn.DataBindings.Add("Text", authorsTable, "Year_Born");
                }
                else
                {
                    //var savedRecord = txtAuthorName.Text; // name that is in the textbox
                    authorsTable.DefaultView.Sort = "Author"; //refresh table and sort by author
                    authorsManager.Position = authorsTable.DefaultView.Find(savedRecord); // find the author we just added and find the position of the record and assign it to the author manage
                    authorsAdapter.Update(authorsTable);
                }

                MessageBox.Show("Record saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetAppState("View"); //after saving record, set app state to view
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Saving Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to delete the record", "Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
            {
                return;
            }

            try
            {
                authorsManager.RemoveAt(authorsManager.Position);
                builderComm = new OleDbCommandBuilder(authorsAdapter);
                authorsAdapter.Update(authorsTable);
                AppState = "Delete";            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Deleting Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void SetAppState(string appState)
        {
            switch (appState)
            {
                case "View": //view only state
                    //txtAuthorID.BackColor = Color.White;
                    //txtAuthorID.ForeColor = Color.Black;
                    txtAuthorName.ReadOnly = true;
                    txtAuthorBorn.ReadOnly = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    txtAuthorName.TabStop = false;
                    txtAuthorBorn.TabStop = false;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnSearchAuthor.Enabled = true;
                    txtSearchAuthor.Enabled = true;
                    break;
                default: //add and edit state
                    txtAuthorID.BackColor = Color.Red;
                    txtAuthorID.ForeColor = Color.White;
                    txtAuthorName.ReadOnly = false;
                    txtAuthorBorn.ReadOnly = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = true;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnSearchAuthor.Enabled = false;
                    txtSearchAuthor.Enabled = false;
                    txtAuthorName.Focus();
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtAuthorBorn.DataBindings.Clear(); // clear data bindings 
            SetAppState("Edit");
            AppState = "Edit";
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentPosition = authorsManager.Position;
                authorsManager.AddNew();
                SetAppState("Add");
                AppState = "Add";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Adding New Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            authorsManager.CancelCurrentEdit();

            if (AppState == "Edit")
            {
                txtAuthorBorn.DataBindings.Add("Text", authorsTable, "Year_Born");
            }

            if (AppState == "Add")
            {
                authorsManager.Position = CurrentPosition;
            }
            
            SetAppState("View");
        }

        private void txtAuthorBorn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //C# handles the conversion between int and char automatically, we do not need to perform any conversion or cast
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
            {
                e.Handled = false;
                lblWrongInput.Visible = false;
            }
            else
            {
                e.Handled = true;
                lblWrongInput.Visible = true;
            }
        }

        private bool ValidateInput()
        {
            string message = "";
            int inputYear, currentYear;
            bool allOK = true;

            if (txtAuthorName.Text.Trim().Equals("")) //if empty
            {
                message = "Author's name is required" + "\r\n";
                txtAuthorName.Focus();
                allOK = false;

            }

            if (!txtAuthorBorn.Text.Trim().Equals("")) //if not empty
            {
                inputYear = Convert.ToInt32(txtAuthorBorn.Text);
                currentYear = DateTime.Now.Year;
                if (inputYear >= currentYear)
                {
                    message += "Invalid Year";
                    txtAuthorBorn.Focus();
                    allOK = false;
                }
            }

            if (!allOK)
            {
                MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            return allOK;
        }

        private void txtAuthorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtAuthorBorn.Focus();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            authorsManager.Position = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            authorsManager.Position = authorsManager.Count - 1;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchAuthor_Click(object sender, EventArgs e)
        {
            if (txtSearchAuthor.Text.Equals("") || txtSearchAuthor.Text.Length < 3)
            {
                MessageBox.Show("Invalid Search", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow[] foundRows;
            authorsTable.DefaultView.Sort = "Author";
            foundRows = authorsTable.Select("Author LIKE '*" + txtSearchAuthor.Text + "*'");

            if (foundRows.Length == 0)
            {
                MessageBox.Show("No Record Found", "No Record Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmSearch searchForm = new frmSearch(foundRows, "Author");
                searchForm.ShowDialog();
                var index = searchForm.Index;
                authorsManager.Position = authorsTable.DefaultView.Find(foundRows[index]["Author"]);
                //authorsManager.Position = authorsTable.DefaultView.Find(foundRows[0]["Author"]);
            }
        }
    }
}
