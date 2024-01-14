using CarRentalSystem.DAO;
using CarRentalSystem.Entity;
using CarRentalSystem.myexceptions;
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

            int choice;

            try
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                Menu Drive Program For Car Rental System                       ");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                    Console.WriteLine("Menu For Car Rental System: ");
                    Console.WriteLine("1. Add Car");
                    Console.WriteLine("2. Add Customer");
                    Console.WriteLine("3. Create Lease");
                    Console.WriteLine("4. Find Car by ID");
                    Console.WriteLine("5. Find Lease by ID");
                    Console.WriteLine("6. Find Customer by ID");
                    Console.WriteLine("7. Check Active Leases");
                    Console.WriteLine("8. List Available Cars");
                    Console.WriteLine("9. List Customers");
                    Console.WriteLine("10. List Lease History");
                    Console.WriteLine("11. Lease Calculator");
                    Console.WriteLine("12. List Rented Cars");
                    Console.WriteLine("13. Retrieve Payment History");
                    Console.WriteLine("14. Record Payment");
                    Console.WriteLine("15. Remove Car");
                    Console.WriteLine("16. Remove Customer");
                    Console.WriteLine("17. Return Car");
                    Console.WriteLine("18. Calculate Total Revenue");
                    Console.WriteLine("19. List of all Cars");
                    Console.WriteLine("20. Update Customer information");
                    Console.WriteLine("0. Exit");

                    Console.Write("\nEnter your choice: \n");


                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {

                        case 1:
                            AddCar();
                            break;
                        case 2:
                            AddCustomer();
                            break;
                        case 3:
                            CreateLease();
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("--------------------------------------------------------------------------------------------------");
                            Console.WriteLine("                                   Enter Car Id to find                                          ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                            int carid = int.Parse(Console.ReadLine());
                            FindCarById(carid);
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("--------------------------------------------------------------------------------------------------");
                            Console.WriteLine("                                   Enter Lease Id to find                                          ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                            int leaseid = int.Parse(Console.ReadLine());
                            FindLeaseById(leaseid);
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("--------------------------------------------------------------------------------------------------");
                            Console.WriteLine("                                   Enter Customer Id to find                                          ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                            int custid = int.Parse(Console.ReadLine());
                            FindCustomerById(custid);
                            break;
                        case 7:
                            CheckActiveLeases();
                            break;
                        case 8:
                            ListAvailableCars();
                            break;
                        case 9:
                            ListCustomers();
                            break;
                        case 10:
                            ListLeaseHistory();
                            break;
                        case 11:
                            LeaseCalculator();
                            break;
                        case 12:
                            ListRentedCars();
                            break;
                        case 13:
                            RetrievePaymentHistory();
                            break;
                        case 14:
                            RecordPayment();
                            break;
                        case 15:
                            RemoveCar();
                            break;
                        case 16:
                            RemoveCustomer();
                            break;
                        case 17:
                            ReturnCar();
                            break;
                        case 18:
                            CalculateTotalRevenue();
                            break;
                        case 19:
                            ListAllCars();
                            break;
                        case 20:
                            UpdateCustomerInformation();
                            break;
                        case 0:
                            Console.WriteLine("Exiting the program.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                } while (choice != 0);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        public static void CalculateTotalRevenue()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                         Total Revenue                                           ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                decimal totalRevenue = carLeaseRepository.CalculateTotalRevenue();
                Console.WriteLine($"Total Revenue: {totalRevenue}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void RetrievePaymentHistory()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("           Enter the Customer ID for which you want to retreive payment history:                  ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void LeaseCalculator()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                               Enter Lease Id for which you want to find total cost               ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                int leaseid = int.Parse(Console.ReadLine());
                FindLeaseById(leaseid);
                carLeaseRepository.LeaseCalculator(leaseid);
            }
            catch (LeaseNotFoundException l)
            {
                Console.WriteLine(l.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void FindLeaseById(int leaseid)
        {
            try
            {
                ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
                Lease foundLease = CarLeaseRepository.FindLeaseById(leaseid);
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                            Lease ID Found                                                 ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                Console.WriteLine();
                Console.WriteLine($"Lease ID: {foundLease.LeaseID}");
                Console.WriteLine($"Customer ID: {foundLease.CustomerID}");
                Console.WriteLine($"Vehicle ID: {foundLease.VehicleID}");
                Console.WriteLine($"Start Date: {foundLease.StartDate.ToString("yyyy-MM-dd")}");
                Console.WriteLine($"End Date: {(foundLease.EndDate.ToString("yyyy-MM-dd"))}");
                Console.WriteLine();

            }
            catch (LeaseNotFoundException ex)
            {
                Console.WriteLine($"Sorry!, {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        public static void AddCar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Enter Details of car                                            ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                Console.WriteLine("Enter Make of Car:");
                string make = Console.ReadLine();
                Console.WriteLine("Enter Model of Car:");
                string model = Console.ReadLine();
                Console.WriteLine("Enter Year of Car:");
                int year = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Daily Rate of Car:");
                double dailyRate = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter Status of Car: ");
                string status = Console.ReadLine();
                Console.WriteLine("Enter Passenger Capacity of Car: ");
                int passengerCapacity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Engine Capacity of Car: ");
                int engineCapacity = int.Parse(Console.ReadLine());
                Vehicle newCar = new Vehicle
                {
                    Make = make,
                    Model = model,
                    Year = year,
                    DailyRate = dailyRate,
                    Status = status,
                    PassengerCapacity = passengerCapacity,
                    EngineCapacity = engineCapacity
                };
                carLeaseRepository.AddCar(newCar);
                // Fetching cars after insertion
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                   Fetching Car After Insertion                                    ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");

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
            }

        }

        public static void AddCustomer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            try
            {
                ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                   Enter Details of Customer                                      ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("Enter First Name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name: ");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter Email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Customer newCustomer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber
                };
                carLeaseRepository.AddCustomer(newCustomer);


                List<Customer> allCustomers = carLeaseRepository.ListCustomers();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                   Fetching Customer After Insertion                              ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                foreach (Customer customer in allCustomers)
                {
                    Console.WriteLine($"Customer Id: {customer.CustomerID}\n First Name: {customer.FirstName}\n Last Name: {customer.LastName}\n Email: {customer.Email}\n Phone Number: {customer.PhoneNumber}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void CreateLease()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                     Enter Details of Lease                                         ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                Console.WriteLine("Enter Available Customer Id:");
                int customerId = int.Parse(Console.ReadLine());
                carLeaseRepository.FindCustomerById(customerId);
                Console.WriteLine("Enter Available Car Id:");
                int carId = int.Parse(Console.ReadLine());
                carLeaseRepository.FindCarById(carId);
                Console.WriteLine("Enter Start Date(YYYY - MM - DD) of Lease: ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter End Date (YYYY-MM-DD) of Lease:");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                Lease newLease = carLeaseRepository.CreateLease(customerId, carId, startDate, endDate);
                List<Lease> leaseList = carLeaseRepository.ListLeaseHistory();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                   Fetching Lease After Insertion                              ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                foreach (Lease lease in leaseList)
                {
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
            }
        }

        public static void FindCarById(int CarIDToFind)
        {
            ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                int carIDToFind = CarIDToFind;
                Vehicle foundCar = CarLeaseRepository.FindCarById(carIDToFind);
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                            Car Found                                                   ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                Console.WriteLine($"Car Id: {carIDToFind}\nMake: {foundCar.Make}\nModel: {foundCar.Model}\nYear: {foundCar.Year}\nDaily Rate: {foundCar.DailyRate}\nStatus: {foundCar.Status}\nPassenger Capacity: {foundCar.PassengerCapacity}\nEngine Capacity: {foundCar.EngineCapacity}");
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

        public static void ListAllCars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Customers                                        ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Vehicle> listVehicle = carLeaseRepository.ListAllCars();

                foreach (Vehicle vehicle in listVehicle)
                {
                    Console.WriteLine($"Car Id: {vehicle.VehicleID}\nMake: {vehicle.Make}\nModel: {vehicle.Model}\nYear: {vehicle.Year}\nDaily Rate: {vehicle.DailyRate}\nStatus: {vehicle.Status}\nPassenger Capacity: {vehicle.PassengerCapacity}\nEngine Capacity: {vehicle.EngineCapacity}");

                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void FindCustomerById(int CustomerIDToFind)
        {
            ICarLeaseRepository CarLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                Customer foundCustomer = CarLeaseRepository.FindCustomerById(CustomerIDToFind);
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                   Customer Found                                                 ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                Console.WriteLine($"\nCustomer Id: {CustomerIDToFind}\nFirst Name: {foundCustomer.FirstName}\nLast Name: {foundCustomer.LastName}\nEmail: {foundCustomer.Email}\nPhone Number: {foundCustomer.PhoneNumber}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine("Sorry!, {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:  {0}", ex.Message);
            }
        }
        public static void UpdateCustomerInformation()
        {

            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                ListCustomers();
                Console.WriteLine("Now Enter the Details of customerId which you want to Update: ");
                Console.WriteLine("\nEnter the Customer id:");
                int custid = int.Parse(Console.ReadLine());
                carLeaseRepository.FindCustomerById(custid);
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

                bool status = carLeaseRepository.UpdateCustomerInformation(newCustomer);
                if (status == true)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                    Customer Updated Succesffuly                                    ");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                    Console.WriteLine();
                    ListCustomers();
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                   Customer not updated                                                 ");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                }


            }
            catch (CustomerNotFoundException c)
            {
                Console.WriteLine("Sorry!," + c.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }

        }
        public static void CheckActiveLeases()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Lease> ActiveLeaseList = carLeaseRepository.ListActiveLeases();
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                        Active Leases List                                          ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                if (ActiveLeaseList.Count > 0)
                {
                    foreach (Lease lease in ActiveLeaseList)
                    {
                        Console.WriteLine($"Lease ID: {lease.LeaseID}");
                        Console.WriteLine($"Customer ID: {lease.CustomerID}");
                        Console.WriteLine($"Vehicle ID: {lease.VehicleID}");
                        Console.WriteLine($"Start Date: {lease.StartDate.ToString("yyyy-MM-dd")}");
                        Console.WriteLine($"End Date: {(lease.EndDate == null ? "Ongoing" : lease.EndDate.ToString("yyyy-MM-dd"))}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No active leases found.");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        public static void ListAvailableCars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Vehicle> listAvailableCars = carLeaseRepository.ListAvailableCars();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                       List of Available Cars:                                         ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                if (listAvailableCars.Count > 0)
                {
                    foreach (Vehicle vehicle in listAvailableCars)
                    {
                        Console.WriteLine($"Vehicle ID: {vehicle.VehicleID}");
                        Console.WriteLine($"Make ID: {vehicle.Make}");
                        Console.WriteLine($"Model: {vehicle.Model}");
                        Console.WriteLine($"Year: {vehicle.Year}");
                        Console.WriteLine($"Daily Rate: {vehicle.DailyRate}");
                        Console.WriteLine($"Status : {vehicle.Status}");
                        Console.WriteLine($"Passenger Capacity : {vehicle.PassengerCapacity}");
                        Console.WriteLine($"Engine Capacity : {vehicle.EngineCapacity}");
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

        public static void ListCustomers()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Customers                                        ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                List<Customer> listcustomers = carLeaseRepository.ListCustomers();

                foreach (Customer customer in listcustomers)
                {
                    Console.WriteLine($"Customer Id: {customer.CustomerID}\n First Name: {customer.FirstName}\n Last Name: {customer.LastName}\n Email: {customer.Email}\n Phone Number: {customer.PhoneNumber}");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        public static void ListLeaseHistory()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Lease                                              ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                List<Lease> listLeaseHistory = carLeaseRepository.ListLeaseHistory();
                foreach (Lease lease in listLeaseHistory)
                {
                    Console.WriteLine($"ID: {lease.LeaseID}");
                    Console.WriteLine($"Customer ID: {lease.CustomerID}");
                    Console.WriteLine($"Vehicle ID: {lease.VehicleID}");
                    Console.WriteLine($"Start Date: {lease.StartDate.ToString("yyyy-MM-dd")}");
                    Console.WriteLine($"End Date: {(lease.EndDate.ToString("yyyy-MM-dd"))}");
                    Console.WriteLine();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
            }
        }

        public static void ListRentedCars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Rented Cars                                              ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                List<Vehicle> rentedCars = carLeaseRepository.ListRentedCars();
                foreach (Vehicle car in rentedCars)
                {
                    Console.WriteLine($"ID: {car.VehicleID}");
                    Console.WriteLine($"Make: {car.Make}");
                    Console.WriteLine($"Model: {car.Model}");
                    Console.WriteLine($"Year: {car.Year}");
                    Console.WriteLine($"Daily Rate: {car.DailyRate}");
                    Console.WriteLine($"Status: {car.Status}");
                    Console.WriteLine($"Passenger Capacity: {car.PassengerCapacity}");
                    Console.WriteLine($"Engine Capacity: {car.EngineCapacity}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void RecordPayment()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                       Enter Data for Recording Payment                      ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");

                Console.WriteLine("Please Enter the existing Lease id which you want to insert in payment record: ");
                int leaseid = int.Parse(Console.ReadLine());
                Lease lease1 = carLeaseRepository.FindLeaseById(leaseid);
                Console.WriteLine("----------------Now Enter Amount to insert data in Payment table-------------------\n");
                double paymentAmount1 = double.Parse(Console.ReadLine());
                carLeaseRepository.RecordPayment(lease1, paymentAmount1);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void RemoveCar()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       Enter Car Id to Remove From Vehicle Table                  ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                int carIDToRemove = int.Parse(Console.ReadLine());
                carLeaseRepository.RemoveCar(carIDToRemove);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void RemoveCustomer()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       Enter Customer Id to Remove From Customer Table             ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                int customerIDToRemove = int.Parse(Console.ReadLine());
                carLeaseRepository.RemoveCustomer(customerIDToRemove);
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine("Sorry: " + ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public static void ReturnCar()
        {
            ICarLeaseRepository carLeaseRepository = new ICarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                  Enter the Lease id for which you want to return car:                               ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                int leaseIDToReturn = int.Parse(Console.ReadLine());
                carLeaseRepository.ReturnCar(leaseIDToReturn);
                List<Vehicle> vehicles = carLeaseRepository.ListAllCars();
                foreach (Vehicle car in vehicles)
                {
                    Console.WriteLine($"ID: {car.VehicleID}");
                    Console.WriteLine($"Make: {car.Make}");
                    Console.WriteLine($"Model: {car.Model}");
                    Console.WriteLine($"Year: {car.Year}");
                    Console.WriteLine($"Daily Rate: {car.DailyRate}");
                    Console.WriteLine($"Status: {car.Status}");
                    Console.WriteLine($"Passenger Capacity: {car.PassengerCapacity}");
                    Console.WriteLine($"Engine Capacity: {car.EngineCapacity}");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

    }
}
