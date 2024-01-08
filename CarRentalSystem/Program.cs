using CarRentalSystem.DAO;
using CarRentalSystem.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    class Program
    {
        static SqlConnection conn;
        static void Main(string[] args)
        {
            //AddCar();
            // AddCustomer();
            //CreateLease();
            //FindCarById();
            // FindCustomerById();
            //CheckActiveLeases();
            //ListAvailableCars();
            // ListCustomers();
            //ListLeaseHistory();
            //ListRentedCars();
            // RecordPayment();
            //RemoveCar();
            // RemoveCustomer();
            ReturnCar();
           Console.ReadLine();
        }


        private static void AddCar()
        {
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl(); // Instantiate your repository

                // Adding a new car
                Vehicle newCar = new Vehicle
                {
                    Make = "Maruti Suzuki",
                    Model = "Swift",
                    Year = 2024,
                    DailyRate = 120.0,
                    Status = "available",
                    PassengerCapacity = 4,
                    EngineCapacity = 2200
                };
                carLeaseRepository.AddCar(newCar);
                // Fetching cars after insertion
                Console.WriteLine("Fetching Cars After Insertion:\n ");
                List<Vehicle> allCars = carLeaseRepository.ListAllCars();

                foreach (Vehicle car in allCars)
                {
                    Console.WriteLine($"\tVehicle ID: {car.VehicleID}");
                    Console.WriteLine($"\tMake ID: {car.Make}");
                    Console.WriteLine($"\tModel: {car.Model}");
                    Console.WriteLine($"\tYear: {car.Year}");
                    Console.WriteLine($"\tDaily Rate: {car.DailyRate}");
                    Console.WriteLine($"\tStatus : {car.Status}");
                    Console.WriteLine($"\tPassenger Capacity : {car.PassengerCapacity}");
                    Console.WriteLine($"\tEngine Capacity : {car.EngineCapacity}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle other potential exceptions
            }

        }


        private static void AddCustomer()
        {
            try{ 
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl(); // Instantiate your repository

            // Adding a new customer
            Customer newCustomer = new Customer
            {
                FirstName = "Rk",
                LastName = "Singh",
                Email = "rk@example.com",
                PhoneNumber = "1231267890"
            };
            carLeaseRepository.AddCustomer(newCustomer);

            // Fetch and display all customers
            List<Customer> allCustomers = carLeaseRepository.ListCustomers();
            Console.WriteLine("\nAll Customers:\n");
            foreach (Customer customer in allCustomers)
            {
                    Console.WriteLine($"Customer Id: {customer.CustomerID}\n First Name: {customer.FirstName}\n Last Name: {customer.LastName}\n Email: {customer.Email}\n Phone Number: {customer.PhoneNumber}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle other potential exceptions
            }
    }
        private static void CreateLease()
        {
            try { 
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl(); // Instantiate your repository

            // Creating a new lease
            int customerId = 104; // Replace with the actual customer ID
            int carId = 9; // Replace with the actual car ID
            DateTime startDate = new DateTime(2024, 01, 10); // Assigning manually (year, month, day)
            DateTime endDate = new DateTime(2024,01,15);

                Lease newLease = carLeaseRepository.CreateLease(customerId, carId, startDate, endDate);
                
                List<Lease> leaseList = carLeaseRepository.ListLeaseHistory();
                foreach (Lease lease in leaseList)
                {
                    // Display lease information (adapt based on Lease properties)
                    Console.WriteLine($"\tLease ID: {lease.LeaseID}");
                    Console.WriteLine($"\tCustomer ID: {lease.CustomerID}");
                    Console.WriteLine($"\tVehicle ID: {lease.VehicleID}");
                    Console.WriteLine($"\tStart Date: {lease.StartDate.ToString("yyyy-MM-dd")}");
                    Console.WriteLine($"\tEnd Date: {(lease.EndDate.ToString("yyyy-MM-dd"))}");
                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle other potential exceptions
            }
}

        public static void FindCarById()
        {
            ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                int carIDToFind = 1; // Replace with the actual car ID
                Vehicle foundCar = CarLeaseRepository.FindCarById(carIDToFind);

                // Use the foundCar object as needed
              Console.WriteLine("Car Founded:");
                Console.WriteLine($"Car Id: {carIDToFind}\t Make: {foundCar.Make}\t Model: {foundCar.Model}\t Year: {foundCar.Year}\t Daily Rate: {foundCar.DailyRate}\t Status: {foundCar.Status}\t Passenger Capacity: {foundCar.PassengerCapacity}\t Engine Capacity: {foundCar.EngineCapacity}");
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine("Sorry!, {0}", ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                Console.WriteLine("Error: {0}", ex.Message);
            }

        }

        public static void FindCustomerById()
        {
            ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                int CustomerIDToFind = 101; // Replace with the actual car ID
                Customer foundCustomer = CarLeaseRepository.FindCustomerById(CustomerIDToFind);

                // Use the foundCar object as needed
                Console.WriteLine("Customer Founded:");
                Console.WriteLine($"Customer Id: {CustomerIDToFind}\t First Name: {foundCustomer.FirstName}\t Last Name: {foundCustomer.LastName}\t Email: {foundCustomer.Email}\t Phone Number: {foundCustomer.PhoneNumber}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine("Sorry!, {0}", ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                Console.WriteLine("Error:  {0}", ex.Message);
            }
        }

        private static void CheckActiveLeases()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Lease> ActiveLeaseList = carLeaseRepository.ListActiveLeases();
                if (ActiveLeaseList.Count > 0)
                {
                    Console.WriteLine("Found active leases:");
                    foreach (Lease lease in ActiveLeaseList)
                    {
                        // Display lease information (adapt based on Lease properties)
                        Console.WriteLine($"\tLease ID: {lease.LeaseID}");
                        Console.WriteLine($"\tCustomer ID: {lease.CustomerID}");
                        Console.WriteLine($"\tVehicle ID: {lease.VehicleID}");
                        Console.WriteLine($"\tStart Date: {lease.StartDate.ToString("yyyy-MM-dd")}");
                        Console.WriteLine($"\tEnd Date: {(lease.EndDate == null ? "Ongoing" : lease.EndDate.ToString("yyyy-MM-dd"))}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No active leases found.");
                }
            }
    
            catch(Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        private static void ListAvailableCars()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Vehicle> listAvailableCars = carLeaseRepository.ListAvailableCars();
                if (listAvailableCars.Count > 0)
                {
                    Console.WriteLine("Found List of Available Cars:");
                    foreach (Vehicle vehicle in listAvailableCars)
                    {
                        // Display lease information (adapt based on Lease properties)
                        Console.WriteLine($"\tVehicle ID: {vehicle.VehicleID}");
                        Console.WriteLine($"\tMake ID: {vehicle.Make}");
                        Console.WriteLine($"\tModel: {vehicle.Model}");
                        Console.WriteLine($"\tYear: {vehicle.Year}");
                        Console.WriteLine($"\tDaily Rate: {vehicle.DailyRate}");
                        Console.WriteLine($"\tStatus : {vehicle.Status}");
                        Console.WriteLine($"\tPassenger Capacity : {vehicle.PassengerCapacity}");
                        Console.WriteLine($"\tEngine Capacity : {vehicle.EngineCapacity}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No Car Available.");
                }
            }
 
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }


        private static void ListCustomers()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Customer> listcustomers = carLeaseRepository.ListCustomers();
                    Console.WriteLine("List of Customers: \n");
                    foreach (Customer customer in listcustomers)
                    {
                    // Display lease information (adapt based on Lease properties)
                    Console.WriteLine($"Customer Id: {customer.CustomerID}\n First Name: {customer.FirstName}\n Last Name: {customer.LastName}\n Email: {customer.Email}\n Phone Number: {customer.PhoneNumber}");
                    Console.WriteLine();
                    }
            }
                

            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        private static void ListLeaseHistory()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Lease> listLeaseHistory = carLeaseRepository.ListLeaseHistory();

                    Console.WriteLine("List of Lease History: ");
                    foreach (Lease lease in listLeaseHistory)
                    {
                        // Display lease information (adapt based on Lease properties)
                        Console.WriteLine($"\tLease ID: {lease.LeaseID}");
                        Console.WriteLine($"\tCustomer ID: {lease.CustomerID}");
                        Console.WriteLine($"\tVehicle ID: {lease.VehicleID}");
                        Console.WriteLine($"\tStart Date: {lease.StartDate.ToString("yyyy-MM-dd")}");
                        Console.WriteLine($"\tEnd Date: {(lease.EndDate.ToString("yyyy-MM-dd"))}");
                        Console.WriteLine();
                    }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        private static void ListRentedCars()
        {
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl(); // Instantiate your repository

                List<Vehicle> rentedCars = carLeaseRepository.ListRentedCars();

                // Display rented cars
                Console.WriteLine("Rented Cars: \n");
                foreach (Vehicle car in rentedCars)
                {
                    Console.WriteLine($"\tID: {car.VehicleID}");
                    Console.WriteLine($"\tMake: {car.Make}");
                    Console.WriteLine($"\tModel: {car.Model}");
                    Console.WriteLine($"\tYear: {car.Year}");
                    Console.WriteLine($"\tDaily Rate: {car.DailyRate}");
                    Console.WriteLine($"\tStatus: {car.Status}");
                    Console.WriteLine($"\tPassenger Capacity: {car.PassengerCapacity}");
                    Console.WriteLine($"\tEngine Capacity: {car.EngineCapacity}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle other potential exceptions
            }
        }

        private static void RecordPayment()
        {
            try
            {
                int customerId = 104;
                int carId = 9;
                DateTime startDate = new DateTime(2024, 02, 10);
                DateTime endDate = new DateTime(2024, 02, 15);
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                Lease lease = carLeaseRepository.CreateLease(customerId, carId, startDate, endDate);
                Console.WriteLine($"LeaseID from CreateLease: {lease.LeaseID}");
                // Record a payment
                double paymentAmount = 550;
                carLeaseRepository.RecordPayment(lease, paymentAmount);

                // Optionally, display a success message or perform other actions
                Console.WriteLine("Payment recorded successfully.");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        private static void RemoveCar()
        {
            try
            {
                int carIDToRemove = 10; // Replace this with the actual car ID you want to remove
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                carLeaseRepository.RemoveCar(carIDToRemove);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void RemoveCustomer()
        {
            try
            {
                int customerIDToRemove = 107; // Replace with the actual CustomerID you want to remove
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                carLeaseRepository.RemoveCustomer(customerIDToRemove);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                
            }
        }

        private static void ReturnCar()
        {
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();

                // Assuming leaseID 10 is the ID for the car being returned
                int leaseIDToReturn = 203;
                carLeaseRepository.ReturnCar(leaseIDToReturn);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

    }
}
