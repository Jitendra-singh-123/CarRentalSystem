using NUnit.Framework;
using CarRentalSystem.DAO;
using CarRentalSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using CarRentalSystem.myexceptions;

namespace CarRentalSystemTesting
{
    [TestFixture]
    public class Tests
    {
        private ICarLeaseRepository carLeaseRepository;
        private  int leaseID;
        //public Lease l;
        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "C:\\Users\\acer\\source\\repos\\CarRentalSystem\\CarRentalSystem\\App.config");

            // AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\App.config", AppDomain.CurrentDomain.BaseDirectory));
            carLeaseRepository = new ICarLeaseRepositoryImpl();
           
        }

        [Test]
        public void AddCar_ShouldCreateCarSuccessfully()
        {
            // Arrange
            Vehicle newCar = new Vehicle
            {
                Make = "Maruti-Suzuki",
                Model = "Swift",
                Year = 2023,
                DailyRate = 150.0,
                Status = "available",
                PassengerCapacity = 5,
                EngineCapacity = 2500
            };

            // Act
            carLeaseRepository.AddCar(newCar);

            // Assert
            Vehicle retrievedCar = carLeaseRepository.FindCarById(newCar.VehicleID);
            Assert.IsNotNull(retrievedCar, "Car should be created successfully");
            Assert.AreEqual(newCar.Make, retrievedCar.Make, "Make of the car should match");
            Assert.AreEqual(newCar.Model, retrievedCar.Model, "Model of the car should match");
            Assert.AreEqual(newCar.Year, retrievedCar.Year, "Year of the car should match");
            Assert.AreEqual(newCar.DailyRate, retrievedCar.DailyRate, "Daily Rate of the car should match");
            Assert.AreEqual(newCar.Status, retrievedCar.Status, "Status of the car should match");
            Assert.AreEqual(newCar.PassengerCapacity, retrievedCar.PassengerCapacity, "Passenger Capacity of the car should match");
            Assert.AreEqual(newCar.EngineCapacity, retrievedCar.EngineCapacity, "Engine Capacity of the car should match");

        }

        [Test]
        public void CreateLease_ShouldCreateLeaseSuccessfully()
        {
            // Arrange
            int customerID = 109;
            int carID = 9;
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(7);
            // Act
            Lease createdLease = carLeaseRepository.CreateLease(customerID, carID, startDate, endDate);
            //l = new Lease();
            leaseID =  createdLease.LeaseID;
            // Assert
            Assert.IsNotNull(createdLease, "Lease should be created successfully");
            Assert.AreEqual(customerID, createdLease.CustomerID, "CustomerID should match");
            Assert.AreEqual(carID, createdLease.VehicleID, "VehicleID should match");
            Assert.AreEqual(startDate, createdLease.StartDate, "StartDate should match");
            Assert.AreEqual(endDate, createdLease.EndDate, "EndDate should match");
           
        }

        [Test]
        public void ListLeaseHistory_ShouldRetrieveLeaseHistorySuccessfully()
        {
            // Arrange 

            // Act
            CreateLease_ShouldCreateLeaseSuccessfully();
            List<Lease> leaseHistory = carLeaseRepository.ListLeaseHistory();
            // Assert
            Assert.IsNotNull(leaseHistory, "Lease history should not be null");
            Assert.IsNotEmpty(leaseHistory, "Lease history should not be empty");
            
            // Example: Check if the LeaseID, VehicleID, and CustomerID of the first lease in the list are valid
            Lease lastLease = leaseHistory.Last();
            Assert.AreEqual(lastLease.LeaseID, leaseID);

        }

        [Test]
        public void FindCarById_WhenCarNotFound_ShouldThrowCarNotFoundException()
        {
            // Arrange - Prepare the situation, like choosing a car ID that doesn't exist
            int nonExistingCarID = -1;

            // Act 
            TestDelegate act = () => carLeaseRepository.FindCarById(nonExistingCarID);

            int nonExistingCustomerID = -1;

            // Act 
            TestDelegate act1 = () => carLeaseRepository.FindCustomerById(nonExistingCustomerID);

            int nonExistingLeaseID = -1;

            // Act 
            TestDelegate act2 = () => carLeaseRepository.FindLeaseById(nonExistingLeaseID);

            // Assert - Check if the expected exception is thrown
            Assert.Throws<CarNotFoundException>(act);
            Assert.Throws<CustomerNotFoundException>(act1);
            Assert.Throws<LeaseNotFoundException>(act2);
        }
    }
}