using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Entity
{
   public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double DailyRate { get; set; }
        public string Status { get; set; } // available, notAvailable
        public int PassengerCapacity { get; set; }
        public int EngineCapacity { get; set; }

        // Constructors
        public Vehicle() { } // Default constructor

        public Vehicle(int vehicleID, string make, string model, int year, double dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }


    }
}
