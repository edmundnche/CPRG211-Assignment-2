using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Traveless.Data;

namespace Traveless.Managers
{
    internal class ReservationManager
    {
        private const string ReservationsFileName = "reservations.json";
        private const string FlightsFileName = "flights.csv";
        private const string AirportsFileName = "airports.csv";

        private List<Reservation> reservations;
        private List<Flights> flights; // Keep the naming as it is in the Flights.cs
        private Dictionary<string, string> airports;

        public ReservationManager()
        {
            reservations = new List<Reservation>();
            flights = new List<Flights>();
            airports = new Dictionary<string, string>();
            LoadReservationsFromFile();
            LoadFlightsFromFile();
            LoadAirportsFromFile();
        }

        // Loads the reservation data
        private void LoadReservationsFromFile()
        {
            if (File.Exists(ReservationsFileName))
            {
                string jsonString = File.ReadAllText(ReservationsFileName);
                reservations = JsonSerializer.Deserialize<List<Reservation>>(jsonString) ?? new List<Reservation>();
            }
        }

        // Loads the data from flights
        private void LoadFlightsFromFile()
        {
            if (File.Exists(FlightsFileName))
            {
                var lines = File.ReadAllLines(FlightsFileName);
                foreach (var line in lines.Skip(1))
                {
                    try
                    {
                        var flight = Flights.FromCsv(line);
                        flights.Add(flight);
                    }
                    catch (Exception ex)
                    {
                        // Use's "ex" to log the error
                        Console.WriteLine($"Error parsing line in CSV file: {ex.Message}");
                    }
                }
            }
        }

        //Loads airport data
        private void LoadAirportsFromFile()
        {
            if (File.Exists(AirportsFileName))
            {
                airports = File.ReadAllLines(AirportsFileName)
                               .Select(line => line.Split(','))
                               .ToDictionary(split => split[0], split => split[1]);
            }
        }
        //Saves reservations
        public void SaveReservationsToFile()
        {
            string jsonString = JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ReservationsFileName, jsonString);
        }
        
        //formats the flights data to be displayed
        public List<Flights> FindFlights(string fromAirportCode, string toAirportCode, string day)
        {
            return flights.Where(f => f.FromAirport == fromAirportCode &&
                                      f.ToAirport == toAirportCode &&
                                      f.DayOfWeek.Equals(day, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        //formats the reservation data to be displayed
        public List<Reservation> FindReservations(string searchQuery)
        {
            return reservations.Where(r => r.PassengerName.Equals(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                           r.Airline.Equals(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                           r.ReservationCode.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        //adds new reservation
        public bool TryAddReservation(Reservation reservation)
        {
            var flight = flights.FirstOrDefault(f => f.FlightNumber == reservation.FlightCode);
            if (flight == null || !flight.ReserveSeat())
            {
                return false;
            }

            reservations.Add(reservation);
            SaveReservationsToFile();
            return true;
        }

        // Modifies existing Reservation
        public bool TryModifyReservation(string code, string newName, string newCitizenship, bool isActive)
        {
            var reservation = reservations.FirstOrDefault(r => r.ReservationCode == code);
            if (reservation == null)
            {
                return false;
            }

            reservation.PassengerName = newName;
            reservation.Citizenship = newCitizenship;
            reservation.IsActive = isActive;

            SaveReservationsToFile();
            return true;
        }
    }
}
