using Microsoft.Maui.Controls.Shapes;
using System;
using System;
using System.Linq;

namespace Traveless.Data
{
    internal class Flights
    {
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }
        public string DayOfWeek { get; set; }
        public string DepartureTime { get; set; }
        public int TotalSeatNo { get; set; }
        public int AvailableSeats { get; private set; }
        public decimal Price { get; set; }

        public Flights(string flightNumber, string airline, string fromAirport, string toAirport, string dayOfWeek, string departureTime, int totalSeatNo, decimal price)
        {
            FlightNumber = flightNumber;
            Airline = airline;
            FromAirport = fromAirport;
            ToAirport = toAirport;
            DayOfWeek = dayOfWeek;
            DepartureTime = departureTime;
            TotalSeatNo = totalSeatNo;
            AvailableSeats = totalSeatNo; // Initially all seats are available
            Price = price;
        }

        // Call this method to reserve a seat on this flight. True = Seat Reserved successfully, Fail = No seats available
        public bool ReserveSeat()
        {
            if (AvailableSeats > 0)
            {
                AvailableSeats--;
                return true;
            }
            return false;
        }

        // If a reservation is canceled or changed, this method can free up a seat
        public void ReleaseSeat()
        {
            if (AvailableSeats < TotalSeatNo)
            {
                AvailableSeats++;
            }
        }
        // This takes the CSV string as an argument, parse it, and then initialize a Flights object with the parsed data.
        public static Flights FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            if (values.Length != 8)
            {
                throw new FormatException("CSV line is not in the expected format for parsing a Flights object.");
            }

            string flightNumber = values[0];
            string airline = values[1];
            string fromAirport = values[2];
            string toAirport = values[3];
            string dayOfWeek = values[4];
            string departureTime = values[5];
            int totalSeatNo = int.Parse(values[6]);
            decimal price = decimal.Parse(values[7]);

            return new Flights(flightNumber, airline, fromAirport, toAirport, dayOfWeek, departureTime, totalSeatNo, price);
        }


        public override string ToString()
        {
            return $"Flight {FlightNumber} by {Airline} departs on {DayOfWeek} at {DepartureTime}. Price: {Price}, Available Seats: {AvailableSeats}";
        }
    }
}