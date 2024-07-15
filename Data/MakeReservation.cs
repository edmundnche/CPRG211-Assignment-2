using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless.Data
{
    internal class MakeReservation
    {
        public Flights Flights { get; set; }
        public string Name { get; set; }
        //public string Name { get;
        //    set {
        //        if (string.IsNullOrEmpty(Name)) 
        //        {
        //            // throw new Ex();
        //        }
        //    } }
        public string Citizenship { get; set; }
        public string ReservationCode { get; set; }

        //makes a reservation using all the information below
        public MakeReservation(string reservationCode, string name, string citizenship, Flights flights)
        {
            Flights = flights;
            Name = name;
            ReservationCode = reservationCode;
            Citizenship = citizenship;
        }
        //Generates reservation code based on specified format
        public static string GenerateReservationCode()
        {
            Random r = new Random();
            return $" {Convert.ToChar(r.Next(0, 26) + 65)}-{r.Next(0, 10)}{r.Next(0, 10)}{r.Next(0, 10)}{r.Next(0, 10)} ";
        }

        public override string ToString()
        {
            return $"Flight: {Flights}, Name: {Name}, Citizenship: {Citizenship}, Reservation Code: {ReservationCode}";
        }
    }
}
