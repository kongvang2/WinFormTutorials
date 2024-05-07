using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    public class Seat
    {
        public int SeatID {  get; set; }
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
        public bool IsSeatTaken { get; set; }

        private OleDbCommand _command;
        private OleDbDataReader _reader;

        // Check if the plane is already full (used to place passenger on waiting list)
        public bool IsPlaneFull()
        {
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();
                _command = new OleDbCommand("SELECT * FROM Seats WHERE IsTaken = false", conn);
                _reader = _command.ExecuteReader();

                // if reader has no rows then return true (plane is full/ no empty seats)
                // if at least one seat was empty, then the has rows will be returned (it will have value) and we will return false
                // meaning the airpane is not full yet (there are still empty seats)
                return !_reader.HasRows ? true : false;
            }
        }

        //Check if seat is already taken
        public bool IsSeatAlreayTaken(string seatRow, string seatColumn)
        {
            using (var conn = new OleDbConnection(DataBaseObjects.ConnectionString))
            {
                conn.Open();
                _command = new OleDbCommand("SELECT * FROM Seats WHERE SeatRow = @SeatRow AND SeatColumn = @SeatColumn " +
                    "AND IsTaken = false", conn);
                _command.Parameters.Add(new OleDbParameter("SeatRow", seatRow));
                _command.Parameters.Add(new OleDbParameter("SeatColumn", seatColumn));

                _reader = _command.ExecuteReader();

                return !_reader.HasRows ? true : false;
            }
        }
    }
}
