using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManagementSystem_CRUD_Project
{
    public partial class frmTitles : Form
    {
        public frmTitles()
        {
            InitializeComponent();
        }

        OleDbConnection booksConn;
        OleDbCommand titlesComm;
        OleDbDataAdapter titlesAdapter;
        DataTable titlesTable;
        CurrencyManager titlesManager;
        private string AppState { get; set; }
        OleDbCommandBuilder builderComm;
        private int CurrentPosition { get; set; }

        // Adding Authors and Publishers to the Form
        OleDbCommand authorsCommand;
        OleDbDataAdapter authorsAdapter;
        DataTable [] authorsTable = new DataTable[4];
        ComboBox[] authorsCombo = new ComboBox[4]; 
        CurrencyManager authorsManager;

        OleDbCommand ISBNAuthorsComm;
        OleDbDataAdapter ISBNAuthorsAdapter;
        DataTable ISBNAuthorsTable;

        OleDbCommand publishersCommand;
        OleDbDataAdapter publishersAdapter;
        DataTable publishersTable;


        private void frmTitles_Load(object sender, EventArgs e)
        {
            try
            {
                string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DB\Books.accdb;" +
                                 "Persist Security Info=False;";

                booksConn = new OleDbConnection(connString);
                booksConn.Open();
                titlesComm = new OleDbCommand("SELECT * FROM Titles ORDER BY Title", booksConn);
                titlesAdapter = new OleDbDataAdapter();
                titlesAdapter.SelectCommand = titlesComm;
                titlesTable = new DataTable();
                titlesAdapter.Fill(titlesTable);

                txtTitles.DataBindings.Add("Text", titlesTable, "Title");
                txtYearPublished.DataBindings.Add("Text", titlesTable, "Year_Published");
                txtISBN.DataBindings.Add("Text", titlesTable, "ISBN");
                txtDescription.DataBindings.Add("Text", titlesTable, "Description");
                txtNotes.DataBindings.Add("Text", titlesTable, "Notes");
                txtSubject.DataBindings.Add("Text", titlesTable, "Subject");
                txtComments.DataBindings.Add("Text", titlesTable, "Comments");

                titlesManager = (CurrencyManager)BindingContext[titlesTable];

                // Loading Authors Database Objects
                authorsCombo[0] = cboAuthor1;
                authorsCombo[1] = cboAuthor2;
                authorsCombo[2] = cboAuthor3;
                authorsCombo[3] = cboAuthor4;

                authorsCommand = new OleDbCommand("SELECT * FROM Authors ORDER BY Author", booksConn);
                authorsAdapter = new OleDbDataAdapter();
                authorsAdapter.SelectCommand = authorsCommand;

                for (int i = 0; i < 4; i++)
                {
                    authorsTable[i] = new DataTable();
                    authorsAdapter.Fill(authorsTable[i]);
                    authorsCombo[i].DataSource = authorsTable[i];
                    authorsCombo[i].DisplayMember = "Author";
                    authorsCombo[i].ValueMember = "Au_ID";
                    authorsCombo[i].SelectedIndex = -1;
                }

                // Load the Publishers
                publishersCommand = new OleDbCommand("SELECT * FROM Publishers ORDER BY Name", booksConn);
                publishersAdapter = new OleDbDataAdapter();
                publishersTable = new DataTable();
                publishersAdapter.SelectCommand = publishersCommand;
                publishersAdapter.Fill(publishersTable);

                cboPublisher.DataSource = publishersTable;
                cboPublisher.DisplayMember = "Name";
                cboPublisher.ValueMember = "PubID";
                cboPublisher.DataBindings.Add("SelectedValue", titlesTable, "PubID");

                SetAppState("View");
                GetAuthors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void frmClosing(object sender, FormClosingEventArgs e)
        {
            booksConn.Close();
            booksConn.Dispose();
            titlesComm.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            titlesManager.Position = 0;
            GetAuthors();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            titlesManager.Position = titlesManager.Count - 1;
            GetAuthors();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            titlesManager.Position--;
            GetAuthors();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            titlesManager.Position++;
            GetAuthors();
        }

        private void btnFindTitles_Click(object sender, EventArgs e)
        {
            if (txtSearchTitles.Text.Equals("") || txtSearchTitles.Text.Length < 3)
            {
                MessageBox.Show("Invalid Search", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow[] foundRecords;
            titlesTable.DefaultView.Sort = "Title";
            foundRecords = titlesTable.Select("Title LIKE '*" + txtSearchTitles.Text + "*'");

            if (foundRecords.Length == 0)
            {
                MessageBox.Show("No Record Found", "No Record Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                frmSearch searchForm = new frmSearch(foundRecords, "Titles");
                searchForm.ShowDialog();
                var index = searchForm.Index;
                titlesManager.Position = titlesTable.DefaultView.Find(foundRecords[index]["Title"]);
                //titlesManager.Position = titlesTable.DefaultView.Find(foundRecords[0]["Title"]);
                GetAuthors();
            }
        }

        private void SetAppState(string appState)
        {
            switch (appState)
            {
                case "View":
                    txtTitles.ReadOnly = true;
                    txtYearPublished.ReadOnly = true;
                    txtISBN.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    txtNotes.ReadOnly = true;
                    txtSubject.ReadOnly = true;
                    txtComments.ReadOnly = true;
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    btnFindTitles.Enabled = true;
                    btnAuthors.Enabled = true;
                    btnPublishers.Enabled = true;
                    cboAuthor1.Enabled = false;
                    cboAuthor2.Enabled = false;
                    cboAuthor3.Enabled = false;
                    cboAuthor4.Enabled = false;
                    btnXAuthor1.Enabled = false;
                    btnXAuthor2.Enabled = false;
                    btnXAuthor3.Enabled = false;
                    btnXAuthor4.Enabled = false;
                    cboPublisher.Enabled = false;
                    break;
                default:
                    txtTitles.ReadOnly = false;
                    txtYearPublished.ReadOnly = false;
                    if (appState == "Add New")
                    {
                        txtISBN.ReadOnly = false;
                    }
                    else
                    {
                        txtISBN.ReadOnly = true;
                    }
                    
                    txtDescription.ReadOnly = false;
                    txtNotes.ReadOnly = false;
                    txtSubject.ReadOnly = false;
                    txtComments.ReadOnly = false;
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    btnFindTitles.Enabled = false;
                    btnAuthors.Enabled = false;
                    btnPublishers.Enabled = false;
                    cboAuthor1.Enabled = true;
                    cboAuthor2.Enabled = true;
                    cboAuthor3.Enabled = true;
                    cboAuthor4.Enabled = true;
                    btnXAuthor1.Enabled = true;
                    btnXAuthor2.Enabled = true;
                    btnXAuthor3.Enabled = true;
                    btnXAuthor4.Enabled = true;
                    cboPublisher.Enabled = true;
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtYearPublished.DataBindings.Clear();
            SetAppState("Edit");
            AppState = "Edit";
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            CurrentPosition = titlesManager.Position;
            SetAppState("Add New");
            titlesManager.AddNew();
            AppState = "Add New";
        }

        private bool ValidateInput()
        {
            string message = "";
            bool isOK = true;

            if (txtTitles.Text.Equals(""))
            {
                message = "You must enter a title.\r\n";
                isOK = false;
            }

            int inputYear, currentYear;
            if (!txtYearPublished.Text.Trim().Equals(""))
            {
                inputYear = Convert.ToInt32(txtYearPublished.Text);
                currentYear = DateTime.Now.Year;
                if (inputYear > currentYear)
                {
                    message += "Year Published vannot be greater than current year.\r\n";
                    isOK = false;
                }
            }

            if (!(txtISBN.Text.Length == 13))
            {
                message += "Incomplete ISBN.\r\n";
                isOK = false;
            }

            // To Do validate publisher

            if (!isOK)
            {
                MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return isOK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                var savedRecord = txtISBN.Text;
                titlesManager.EndCurrentEdit();
                builderComm = new OleDbCommandBuilder(titlesAdapter);
                
                if (AppState == "Edit")
                {
                    var titleRow = titlesTable.Select("ISBN = '" + savedRecord + "'");

                    if (String.IsNullOrEmpty(txtYearPublished.Text))
                    {
                        titleRow[0]["Year_Published"] = DBNull.Value;
                    }
                    else
                    {
                        titleRow[0]["Year_Published"] = txtYearPublished.Text;
                    }

                    titlesAdapter.Update(titlesTable);
                    txtYearPublished.DataBindings.Add("Text", titlesTable, "Year_Published");
                }
                else
                {
                    //titlesTable.DefaultView.Sort = "Title";
                    //titlesManager.Position = titlesTable.DefaultView.Find(savedRecord);
                    titlesAdapter.Update(titlesTable);
                    DataRow[] foundRecords;
                    titlesTable.DefaultView.Sort = "Title";
                    foundRecords = titlesTable.Select("ISBN = '" + savedRecord + "'");
                    titlesManager.Position = titlesTable.DefaultView.Find(foundRecords[0]["Title"]);
                    
                }

                builderComm = new OleDbCommandBuilder(ISBNAuthorsAdapter);
                if (ISBNAuthorsTable.Rows.Count != 0)
                {
                    for (int i = 0; i < ISBNAuthorsTable.Rows.Count; i++)
                    {
                        ISBNAuthorsTable.Rows[i].Delete();
                    }

                    ISBNAuthorsAdapter.Update(ISBNAuthorsTable);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (authorsCombo[i].SelectedIndex != -1)
                    {
                        ISBNAuthorsTable.Rows.Add();
                        ISBNAuthorsTable.Rows[ISBNAuthorsTable.Rows.Count - 1]["ISBN"] = txtISBN.Text;
                        ISBNAuthorsTable.Rows[ISBNAuthorsTable.Rows.Count - 1]["Au_ID"] = 
                                                                                    authorsCombo[i].SelectedValue;
                    }

                }

                ISBNAuthorsAdapter.Update(ISBNAuthorsTable);

                MessageBox.Show("Record Saved", "Record Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetAppState("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Saving Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
            {
                return;
            }

            try
            {
                titlesManager.RemoveAt(titlesManager.Position);
                builderComm = new OleDbCommandBuilder(titlesAdapter);
                titlesAdapter.Update(titlesTable);
                AppState = "Delete";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Record", "Error Deleting Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            titlesManager.CancelCurrentEdit();

            if (AppState == "Edit")
            {
                txtYearPublished.DataBindings.Add("Text", titlesTable, "Year_Published");
            }
            if (AppState == "Add New")
            {
                titlesManager.Position = CurrentPosition;
            }

            SetAppState("View");
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void GetAuthors()
        {
            for (int i=0; i<4; i++)
            {
                authorsCombo[i].SelectedIndex = -1;
            }
            ISBNAuthorsComm = new OleDbCommand("SELECT * FROM Title_Author WHERE ISBN ='" + 
                                                txtISBN.Text + "'", booksConn);
            ISBNAuthorsAdapter = new OleDbDataAdapter();
            ISBNAuthorsAdapter.SelectCommand = ISBNAuthorsComm;
            ISBNAuthorsTable = new DataTable();
            ISBNAuthorsAdapter.Fill(ISBNAuthorsTable);


            if (ISBNAuthorsTable.Rows.Count == 0)
            {
                return;
            }

            for (int i=0; i < ISBNAuthorsTable.Rows.Count; i++)
            {
                authorsCombo[i].SelectedValue = ISBNAuthorsTable.Rows[i]["Au_ID"].ToString();
            }
        }

        private void btnXAuthor_Click(object sender, EventArgs e)
        {
            Button btnClicked = (Button)sender;

            switch (btnClicked.Name)
            {
                case "btnXAuthor1":
                    cboAuthor1.SelectedIndex = -1;
                    break;
                case "btnXAuthor2":
                    cboAuthor2.SelectedIndex = -1;
                    break;
                case "btnXAuthor3":
                    cboAuthor3.SelectedIndex = -1;
                    break;
                case "btnAuthor4":
                    cboAuthor4.SelectedIndex = -1;
                    break;
            }
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            frmAuthors authorForm = new frmAuthors(); //create new object of our author form
            authorForm.ShowDialog();
            authorForm.Dispose();
            booksConn.Close();

            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DB\Books.accdb;" +
                                 "Persist Security Info=False;";

            booksConn = new OleDbConnection(connString);
            booksConn.Open();
            authorsAdapter.SelectCommand = authorsCommand;

            for (int i = 0; i < 4; i++)
            {
                authorsTable[i] = new DataTable();
                authorsAdapter.Fill(authorsTable[i]);
                authorsCombo[i].DataSource = authorsTable[i];
            }

            GetAuthors();
        }

        private void btnPublishers_Click(object sender, EventArgs e)
        {
            frmPublishers publisherForm = new frmPublishers();
            publisherForm.ShowDialog();
            publisherForm.Dispose();
            booksConn.Close();

            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DB\Books.accdb;" +
                                 "Persist Security Info=False;";

            booksConn = new OleDbConnection(connString);
            booksConn.Open();

            cboPublisher.DataBindings.Clear();

            publishersAdapter.SelectCommand = publishersCommand;
            publishersTable = new DataTable();
            publishersAdapter.Fill(publishersTable);

            cboPublisher.DataSource = publishersTable;
            cboPublisher.DisplayMember = "Name";
            cboPublisher.ValueMember = "PubID";
            cboPublisher.DataBindings.Add("SelectedValue", titlesTable, "PubID");

        }

        private void btnPrintRecord_Click(object sender, EventArgs e)
        {
            PrintDocument recordDocument = new PrintDocument();
            recordDocument.DocumentName = "Titles Record";
            recordDocument.PrintPage += new PrintPageEventHandler(this.PrintRecordPage);
            recordDocument.Print();
            recordDocument.Dispose();
        }

        private void PrintRecordPage(object sender, PrintPageEventArgs e)
        {
            Pen myPen = new Pen(Color.Black);
            e.Graphics.DrawRectangle(myPen, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, 100/*e.MarginBounds.Height*/);
            string s = "BOOKS DATABASE";
            Font myFont = new Font("Arial", 24, FontStyle.Bold);
            SizeF sSize = e.Graphics.MeasureString(s, myFont);

            e.Graphics.DrawString(s, myFont, Brushes.Black,
                                    e.MarginBounds.Left + Convert.ToInt32(0.5 * (e.MarginBounds.Width - sSize.Width)), //center horizontally
                                    e.MarginBounds.Top + Convert.ToInt32(0.5 * (100 - sSize.Height))); //center vertically

            myFont = new Font("Arial", 12, FontStyle.Regular);
            int y = 300;  //number of pixels below the header
            int dy = Convert.ToInt32(e.Graphics.MeasureString("S", myFont).Height); // Used to space out the text

            e.Graphics.DrawString("Title: " + txtTitles.Text, myFont, Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy; //move two lines then print header for authors
            e.Graphics.DrawString("Authors(s): ", myFont, Brushes.Black, e.MarginBounds.Left, y);

            int x = e.MarginBounds.Left + Convert.ToInt32(e.Graphics.MeasureString("Author(s): ", myFont).Width);

            if (ISBNAuthorsTable.Rows.Count != 0)
            {
                for (int i = 0; i < ISBNAuthorsTable.Rows.Count; i++)
                {
                    e.Graphics.DrawString(authorsCombo[i].Text, myFont, Brushes.Black, x, y);
                    y += dy;
                }
            }
            else
            {
                e.Graphics.DrawString("NONE", myFont, Brushes.Black, x, y);
                y += dy;
            }

            x = e.MarginBounds.Left;
            y += dy;

            e.Graphics.DrawString("ISBN: " + txtISBN.Text, myFont, Brushes.Black, x, y);
            y += 2 * dy;

            e.Graphics.DrawString("Year Published: " + txtYearPublished.Text, myFont, Brushes.Black, x, y);
            y += 2 * dy;

            e.Graphics.DrawString("Publisher: " + cboPublisher.Text, myFont, Brushes.Black, x, y);
            y += 2 * dy;

            e.Graphics.DrawString("Description: " + txtDescription.Text, myFont, Brushes.Black, x, y);
            y += 2 * dy;

            e.Graphics.DrawString("Notes: " + txtNotes.Text, myFont, Brushes.Black, x, y);
            y += 2 * dy;

            e.Graphics.DrawString("Subject: " + txtSubject.Text, myFont, Brushes.Black, x, y);
            y += 2 * dy;

            e.Graphics.DrawString("Comment: " + txtComments.Text, myFont, Brushes.Black, x, y);

            e.HasMorePages = false;
        }
    }
}
