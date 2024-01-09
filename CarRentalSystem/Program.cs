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
            //FindLeaseById();
            //FindCustomerById(101);
            //CheckActiveLeases();
            //ListAvailableCars();
            // ListCustomers();
            //ListLeaseHistory();
            // LeaseCalculator();
            //ListRentedCars();
            // RetrievePaymentHistory();
            // RecordPayment();
            //RemoveCar();
            // RemoveCustomer();
            // ReturnCar();
            CalculateTotalRevenue();
            Console.ReadLine();
        }

        private static void CalculateTotalRevenue()
        {
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                decimal totalRevenue=carLeaseRepository.CalculateTotalRevenue();
                Console.WriteLine($"Total Revenue: {totalRevenue}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void  RetrievePaymentHistory()
        {
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                Console.WriteLine("Enter the Customer ID for which you want to retreive payment history: ");
                int custid = int.Parse(Console.ReadLine());
                FindCustomerById(custid);
                List<Payment> payment = carLeaseRepository.RetrievePaymentHistory(custid);
                foreach (Payment payments in payment)
                {
                    Console.WriteLine($"\nCustomerID : {custid}\nPaymentID : {payments.PaymentID}\nLeaseID : {payments.LeaseID}\nPaymentDate : {payments.PaymentDate}\nAmount: {payments.Amount}");
                    Console.WriteLine();
                }
            }
            catch (CustomerNotFoundException c)
            {
                //error Already shown in findcarbyid method
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private static void LeaseCalculator()
        {
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl(); 
                Console.WriteLine("Enter the lease id for which you want to calculate total cost: \n");
                int leaseid = int.Parse(Console.ReadLine());
                FindLeaseById(leaseid);
                carLeaseRepository.LeaseCalculator(leaseid);

            }
            catch(LeaseNotFoundException l)
            {
                Console.WriteLine(l.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void FindLeaseById(int leaseid)
        {
            ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                Lease foundLease = CarLeaseRepository.FindLeaseById(leaseid);

                // Use the foundCar object as needed
                Console.WriteLine("Lease Founded:");
                Console.WriteLine($"\tLease ID: {foundLease.LeaseID}");
                Console.WriteLine($"\tCustomer ID: {foundLease.CustomerID}");
                Console.WriteLine($"\tVehicle ID: {foundLease.VehicleID}");
                Console.WriteLine($"\tStart Date: {foundLease.StartDate.ToString("yyyy-MM-dd")}");
                Console.WriteLine($"\tEnd Date: {(foundLease.EndDate.ToString("yyyy-MM-dd"))}");
                Console.WriteLine();

            }
            catch (LeaseNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

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

        public static void FindCustomerById(int CustomerIDToFind)
        {
            ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                Customer foundCustomer = CarLeaseRepository.FindCustomerById(CustomerIDToFind);

                // Use the foundCar object as needed
                Console.WriteLine("Customer Founded:");
                Console.WriteLine($"Customer Id: {CustomerIDToFind}\nFirst Name: {foundCustomer.FirstName}\nLast Name: {foundCustomer.LastName}\nEmail: {foundCustomer.Email}\nPhone Number: {foundCustomer.PhoneNumber}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine("Sorry!, {0}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                Console.WriteLine("Error:  {0}", ex.Message);
            }
        }
        private static void UpdateCustomerInformation()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                ListCustomers();
                Console.WriteLine("Now Enter the Details of customerId which you want to Update: ");
                Console.WriteLine("\nEnter the Customer id:");
                int custid = int.Parse(Console.ReadLine());
                FindCustomerById(custid);
                Console.WriteLine("\nNow please Enter the other details:");
                Console.WriteLine("\nEnter the FirstName");
                string First_Name = Console.ReadLine();
                Console.WriteLine("\nEnter the LastName");
                string Last_Name = Console.ReadLine();
                Console.WriteLine("\nEnter the Email");
                string Email_ = Console.ReadLine();
                Console.WriteLine("\nEnter the Phone Number");
                string Phone_Number = Console.ReadLine();
                Customer newCustomer = new Customer
                {
                    CustomerID = custid,
                    FirstName = First_Name,
                    LastName = Last_Name,
                    Email = Email_,
                    PhoneNumber = Phone_Number
                };

                carLeaseRepository.UpdateCustomerInformation(newCustomer);
                Console.WriteLine("");
                ListCustomers();

            }
            catch (CustomerNotFoundException e)
            {
                
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
