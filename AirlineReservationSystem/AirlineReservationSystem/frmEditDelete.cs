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

namespace AirlineReservationSystem
{
    public partial class frmEditDelete : Form
    {
        private OleDbCommand _command;
        private DataTable _table;
        private OleDbDataReader _reader;
        public frmEditDelete(DataTable table)
        {
            InitializeComponent();
            _command = new OleDbCommand();
            this._table = table;

        }

        // Bind the form objects to the data from the DataTable
        // DataTable is from frmPassengerLookup
        private void frmEditDelete_Load(object sender, EventArgs e)
        {
            // Bind the text boxes
            txtPassengerID.DataBindings.Add("Text", _table, "ID");
            txtPassengerName.DataBindings.Add("Text", _table, "Name");
            txtSeatID.DataBindings.Add("Text", _table, "SeatID");

            // Bind the drop-downs
            // If seats are empty (passenger is on waiting list),
            // Make the first index in drop=down selected ("None" for seat row and column)
            var r = _table.Rows[0]["SeatRow"].ToString();
            var row = r.Equals("") ? 0 : Convert.ToInt32(r);

            var c = _table.Rows[0]["SeatColumn"].ToString();
            var column = c.Equals("") ? "None" : c;
            cmbSeatRow.SelectedIndex = row;
            cmbSeatColumn.SelectedItem = column;

            chbWaiting.Checked = Convert.ToBoolean(_table.Rows[0]["OnWaitingList"]);


        }

        // Edit record 
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Validate input
            // Make sure passenger name has been entered
            if (txtPassengerName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Passenger Name is required.", "Invalid Input",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Passenger cannot be on the waiting list and have seat assigned
            if (chbWaiting.Checked && (cmbSeatRow.SelectedIndex > 0 || cmbSeatColumn.SelectedIndex > 0))
            {
                MessageBox.Show("Passenger cannot be on waiting list and have a seat assigned", "Bad Input",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Passenger must be either on waiting list or have seat assigned
            if (!chbWaiting.Checked && (cmbSeatRow.SelectedIndex <= 0 || cmbSeatColumn.SelectedIndex <= 0))
            {
                MessageBox.Show("Passenger seat has to have row and column assigned", "Bad Input",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update Record.
            // 1. Get the id of the new seat
            // 2. Check if the new seat is already taken
            // 3. Check if the user is only updating the passenger name.
            //      If so, then ignore that the seat is taken (it is taken by the same passenger)
            // 4. Update all tables
            //      - update passenger name in Passengers table
            //      - update seats table. The old seat needs to be updated to not taken, and new seat to taken.
            //      - assign new seatId to the passengerID in PassengerSeats table
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();
                _command = new OleDbCommand("SELECT SeatID, IsTaken FROM Seats WHERE " +
                                            "SeatRow = @SeatRow AND SeatColumn = @SeatColumn", conn);
                _command.Parameters.Add(new OleDbParameter("SeatRow", cmbSeatRow.SelectedItem));
                _command.Parameters.Add(new OleDbParameter("SeatColumn", cmbSeatColumn.SelectedItem));
                _reader = _command.ExecuteReader();

                var newSeatID = 0;
                bool newIsTaken = false;

                while (_reader.Read())
                {
                    newSeatID = Convert.ToInt32(_reader["SeatID"]);
                    newIsTaken = Convert.ToBoolean(_reader["IsTaken"]);
                }

                // Check if only the name is being updated.
                // If not, exit because the user needs to pick a different seat
                var oldID = 0;
                if (txtSeatID.Text.Equals(""))
                    oldID = 0;
                else
                    oldID = Convert.ToInt32(txtSeatID.Text);
                
                if (!txtSeatID.Equals(""))
                {
                    if (newSeatID != oldID && newIsTaken)
                    {
                        MessageBox.Show("Seat is already taken", "Seat Taken",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                // Update passenger's name
                _command = new OleDbCommand("UPDATE Passengers SET PassengerName = @PassengerName, " +
                                            "PassengerOnWaitingList = @OnWaitingList " +
                                            "Where PassengerID = @PassengerID", conn);
                _command.Parameters.Add(new OleDbParameter("PassengerName", txtPassengerName.Text));
                _command.Parameters.Add(new OleDbParameter("OnWaitingList", chbWaiting.Checked));
                _command.Parameters.Add(new OleDbParameter("PassengerID", txtPassengerID.Text));
                _command.ExecuteNonQuery();

                // Make original seat available
                _command = new OleDbCommand("UPDATE Seats SET IsTaken = false " +
                                            "WHERE seatID = @seatID", conn);
                _command.Parameters.Add(new OleDbParameter("seatID", txtSeatID.Text));
                _command.ExecuteNonQuery();

                // Make new seat taken
                _command = new OleDbCommand("UPDATE Seats SET IsTaken = true " +
                                            "WHERE seatID = @seatID", conn);
                _command.Parameters.Add(new OleDbParameter("seatID", newSeatID));
                _command.ExecuteNonQuery();

                // Update old seatID with the new one
                _command = new OleDbCommand("UPDATE PassengerSeats SET SeatID = @SeatID WHERE " +
                                            "PassengerID = @PassengerID", conn);
                _command.Parameters.Add(new OleDbParameter("SeatID", newSeatID));
                _command.Parameters.Add(new OleDbParameter("PassengerID", txtPassengerID.Text));
                _command.ExecuteNonQuery();

                MessageBox.Show("Record has been updated", "Update",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Ask user if he wants to delete passenger
            // If not to delete, then exit and do nothin
            // If to delete, then delete passenger from Passengers and PassengersSeat table
            /// The seat still exists, but we need to update the Seats table and maek the seat as not taken
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();

                var msg = MessageBox.Show("Are you sure you want to delete " + txtPassengerName.Text + " from the database?",
                                "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (msg == DialogResult.No) return;

                _command = new OleDbCommand("DELETE FROM Passengers WHERE PassengerID = @PassengerID", conn);
                _command.Parameters.Add(new OleDbParameter("PassengerID", txtPassengerID.Text));
                _command.ExecuteNonQuery();

                _command = new OleDbCommand("DELETE FROM PassengerSeats WHERE PassengerID = @PassengerID", conn);
                _command.Parameters.Add(new OleDbParameter("PassengerID", txtPassengerID.Text));
                _command.ExecuteNonQuery();

                if (!txtSeatID.Text.Equals(""))
                {
                    _command = new OleDbCommand("UPDATE Seats SET IsTaken = false WHERE seatID = @seatID", conn);
                    _command.Parameters.Add(new OleDbParameter("SeatID", txtSeatID.Text));
                    _command.ExecuteNonQuery();
                }

                MessageBox.Show("Record has been deleted", "Deleted",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
