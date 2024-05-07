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
using System.Xml.Linq;

namespace AirlineReservationSystem
{
    public partial class frmPassengerLookup : Form
    {
        private OleDbCommand _command;
        private DataTable _table;

        public frmPassengerLookup(DataTable table)
        {
            InitializeComponent();
            this._table = table;
        }

        private void frmPassengerLookup_Load(object sender, EventArgs e)
        {
            dgvOutput.DataSource = this._table;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvOutput_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the row number that was clicked
            var index = e.RowIndex;

            // Get the passengerID from the clicked row and pass it to the command
            int selectedID = Convert.ToInt32(dgvOutput.Rows[index].Cells[0].Value);

            // Selet all the passenger information from all three tables that matches
            // the passengerID of the passenger that was clicked
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();

                _command = new OleDbCommand
                ("SELECT p.PassengerID AS ID, s.SeatID, p.PassengerName AS Name, s.SeatRow, s.SeatColumn, " +
                "p.PassengerOnWaitingList AS OnWaitingList " +
                "FROM (Passengers p " +
                "INNER JOIN PassengerSeats ps ON p.PassengerID = ps.PassengerID) " +
                "INNER JOIN Seats s ON s.SeatID = ps.SeatID " +
                "WHERE p.PassengerID = @PassengerID " +
                "UNION " +
                "SELECT p.PassengerID, null, p.PassengerName, null, null, p.PassengerOnWaitingList " +
                "FROM Passengers p " +
                "WHERE p.PassengerOnWaitingList = true AND p.PassengerID LIKE @PassengerID " +
                "ORDER BY s.SeatRow,s.SeatColumn", conn);
                _command.Parameters.Add(new OleDbParameter("PassengerID", selectedID));

                DataTable dt = new DataTable();
                dt.Load(_command.ExecuteReader());
                frmEditDelete form = new frmEditDelete(dt);
                form.ShowDialog();
            }

            // Execute command and place results to DataTable and pass it to the EditDelete form
        }
    }
}
