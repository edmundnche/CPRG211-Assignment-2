using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless.Data
{
    [Serializable]
    internal class Reservation
    {
        public string ReservationCode { get; set; }
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public decimal Cost { get; set; }
        public string PassengerName { get; set; }
        public string Citizenship { get; set; }
        public bool IsActive { get; set; }

        // Constructor for the Reservation class.
        public Reservation(string flightCode, string airline, decimal cost, string passengerName, string citizenship)
        {
            // Generate a new reservation code
            ReservationCode = GenerateReservationCode();
            FlightCode = flightCode;
            Airline = airline;
            Cost = cost;
            PassengerName = passengerName;
            Citizenship = citizenship;
            IsActive = true; // When a new reservation is made, it is active by default
        }

        // Generate a new reservation code
        private string GenerateReservationCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper(); // Generates a 5-character string
        }

        // Overrides the ToString method to display the reservation information.
        public override string ToString()
        {
            var status = IsActive ? "Active" : "Inactive";
            return $"Reservation Code: {ReservationCode}, Flight: {FlightCode}, Airline: {Airline}, Cost: {Cost}, Passenger: {PassengerName}, Citizenship: {Citizenship}, Status: {status}";
        }
    }

}