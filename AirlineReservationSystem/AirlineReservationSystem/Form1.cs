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
    public partial class frmReservation : Form
    {
        // Passenger and Seat Objects
        Passenger passenger;
        Seat seat;

        // List of all seats (used to display the seats in the list box)
        public static List<Seat> seats;

        // DB Object
        private OleDbCommand _command;
        private OleDbDataReader _reader;

        public frmReservation()
        {
            InitializeComponent();
            _command = new OleDbCommand();
            seats = new List<Seat>();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        // When the form loads, display the list of seats and populate the drop-down with seat rows
        private void frmReservation_Load(object sender, EventArgs e)
        {
            PopulateSeatRows();
            PopulateAirplane();
        }

        // Add Passenger
        private void btnAddPassenger_Click(object sender, EventArgs e)
        {
            // Passenger and Seat Objects
            passenger = new Passenger();
            seat = new Seat();

            // Check what seat column was selected (A, B, C, D)
            var checkedButton = Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);

            // Validate input. Valid name, seat row and selection of seat column is required
            if (!passenger.IsValidPassenger(txtName.Text) || cmbSeatRow.SelectedIndex == -1 || 
                checkedButton == null)
            {
                MessageBox.Show("Please Enter Valid Name And Seat", "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // Check if plane is full. If it is, place passenger on waiting list
            if (seat.IsPlaneFull())
            {
                var msg = MessageBox.Show("The plane is full. Add passenger on waiting list?", "Plane Full",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                 
                if (msg == DialogResult.No) { return; }

                using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
                {
                    conn.Open();
                    _command = new OleDbCommand("INSERT INTO Passengers (PassengerName, PassengerOnWaitingList) " +
                                                "VALUES (@passengerName, true)", conn);
                    _command.Parameters.Add(new OleDbParameter("PassengerName", txtName.Text));
                    _command.ExecuteNonQuery();

                    _command = new OleDbCommand("INSERT INTO PassengerSeats (PassengerID, SeatID) " +
                                                "SELECT MAX(PassengerID), 0 FROM Passengers", conn);

                    _command.ExecuteNonQuery();

                    MessageBox.Show("Passenger " + txtName.Text + " was added to the waiting list",
                                    "Waiting List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return;
            }

            // Check if seat is taken, If it is, exit so the user can select a different seat
            if (seat.IsSeatAlreayTaken(cmbSeatRow.SelectedItem.ToString(), checkedButton.Text))
            {
                MessageBox.Show("The seat is already taken. Please select a different seat",
                                "Seat Taken", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // If everything is OK, add passenger and seat to database along with the assigned seat.
            // Insert new passenger into Passenger DB.
            // Update seat to Taken in Seats DB.
            // Insert Passenger and Seat ID in PassengerSeats DB.
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();
                _command = new OleDbCommand("INSERT INTO Passengers (PassengerName) " +
                                            "VALUES (@passengerName)", conn);
                _command.Parameters.Add(new OleDbParameter("PassengerName", txtName.Text));
                _command.ExecuteNonQuery();

                _command = new OleDbCommand("UPDATE Seats SET IsTaken = true WHERE " + 
                                            "SeatRow = @SeatRow AND SeatColumn = @SeatColumn", conn);
                _command.Parameters.Add(new OleDbParameter("SeatRow", cmbSeatRow.Text));
                _command.Parameters.Add(new OleDbParameter("Seatcolumn", checkedButton.Text));
                _command.ExecuteNonQuery();

                _command = new OleDbCommand("INSERT INTO PassengerSeats (SeatID, PassengerID) " +
                                            "SELECT Seats.SeatID, (SELECT MAX(PassengerID) FROM Passengers) " +
                                            "FROM Seats " +
                                            "WHERE Seats.SeatRow = @SeatRow AND Seats.SeatColumn = @SeatColumn",
                                            conn);
                _command.Parameters.Add(new OleDbParameter("SeatRow", cmbSeatRow.Text));
                _command.Parameters.Add(new OleDbParameter("SeatColumn", checkedButton.Text));
                _command.ExecuteNonQuery();

                MessageBox.Show("Passenger has been added", 
                                "Added Passenger", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PopulateAirplane();
            }
        }

        // Show all passengers
        private void btnShowPassenger_Click(object sender, EventArgs e)
        {
            // Get all passenger information from all 3 tables (will use Inner Join)
            // Place the result into DataTable and display the result in Lookups form
            // When focus is back, repopulate the list box with updated records
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();

                _command = new OleDbCommand
                    ("SELECT p.PassengerID AS ID, p.PassengerName AS Name, s.SeatRow, s.SeatColumn, " +
                    "p.PassengerOnWaitingList AS OnWaitingList " +
                    "FROM (Passengers p " +
                    "INNER JOIN PassengerSeats ps ON p.PassengerID = ps.PassengerID) " +
                    "INNER JOIN Seats s ON s.SeatID = ps.SeatID " +
                    "UNION " +
                    "SELECT p.PassengerID, p.PassengerName, null, null, p.PassengerOnWaitingList " +
                    "FROM Passengers p " +
                    "WHERE p.PassengerOnWaitingList = true " +
                    "ORDER BY s.SeatRow,s.SeatColumn", conn);

                DataTable dt = new DataTable();
                dt.Load(_command.ExecuteReader());
                frmPassengerLookup form = new frmPassengerLookup(dt);
                form.ShowDialog();
                PopulateAirplane();
            }
        }
         
        // Search for passenger
        private void btnSearchPassenger_Click(object sender, EventArgs e)
        {
            // Check if a search string was entered in the text box
            // Get all the passengers that match the search string. Get all the information from all 3 tables
            // Place the result in a DataTable and then display it in Lookups form
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();

                if (!txtName.Text.Trim().Equals(""))
                {
                    _command = new OleDbCommand
                    ("SELECT p.PassengerID AS ID, p.PassengerName AS Name, s.SeatRow, s.SeatColumn, " +
                    "p.PassengerOnWaitingList AS OnWaitingList " +
                    "FROM (Passengers p " +
                    "INNER JOIN PassengerSeats ps ON p.PassengerID = ps.PassengerID) " +
                    "INNER JOIN Seats s ON s.SeatID = ps.SeatID " +
                    "WHERE p.PassengerName LIKE @PassengerName " +
                    "UNION " +
                    "SELECT p.PassengerID, p.PassengerName, null, null, p.PassengerOnWaitingList " +
                    "FROM Passengers p " +
                    "WHERE p.PassengerOnWaitingList = true AND p.PassengerName LIKE @PassengerName " +
                    "ORDER BY s.SeatRow,s.SeatColumn", conn);
                    _command.Parameters.Add(new OleDbParameter("PassengerName", "%" + txtName.Text + "%"));

                    DataTable dt = new DataTable();
                    dt.Load(_command.ExecuteReader());
                    frmPassengerLookup form = new frmPassengerLookup(dt);
                    form.ShowDialog();
                    PopulateAirplane();
                }
                else
                {
                    MessageBox.Show("Please enter a valid name", "Invalid Input", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Display the seats in the List Box
        private void PopulateAirplane()
        {
            // Clear previous listbox and list of Seats
            lstOutput.Items.Clear();
            seats.Clear();

            // SELECT * seats FROM Seats DB.  Read result and create a Seat object with
            // ID, Row, Column, and IsTaken property from the reader
            // Add the Seat object to the list
            // Loop through the list and display the content in the list box
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();
                _command = new OleDbCommand("SELECT * FROM Seats ORDER BY SeatRow, SeatColumn", conn);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    var seat = new Seat();
                    seat.SeatID = Convert.ToInt32(_reader["SeatID"]);
                    seat.SeatRow = Convert.ToInt32(_reader["SeatRow"]);
                    seat.SeatColumn = _reader["SeatColumn"].ToString();
                    seat.IsSeatTaken = Convert.ToBoolean(_reader["IsTaken"]);

                    seats.Add(seat);
                }

                var msg = "";
                var counter = 0;
                for (int i = 0; i < seats.Count; i++)
                {
                    counter++; // seats start at 1
                    if (seats[i].IsSeatTaken)
                    {
                        msg += "  " + "XX" + "   ";
                    }
                    else
                    {
                        msg += "  " + seats[i].SeatRow + seats[i].SeatColumn + "   ";
                    }

                    if (counter % 4 == 0)
                    {
                        lstOutput.Items.Add(msg);
                        msg = "";
                    }
                    else if (counter % 2 == 0)
                    {
                        msg += "        ";
                    }
                }
            }
        }

        // Populate Drop=down with seat rows
        private void PopulateSeatRows()
        {
            // Get row numbers
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();
                _command = new OleDbCommand("SELECT DISTINCT SeatRow FROM Seats ORDER BY SeatRow", conn);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cmbSeatRow.Items.Add(_reader["SeatRow"]);
                }
            }
        }
    }
}
