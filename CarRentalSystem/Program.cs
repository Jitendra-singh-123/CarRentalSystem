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
                           
                            FindCarById();
                            break;
                        case 5:
                            FindLeaseById();
                            break;
                        case 6:
                            FindCustomerById();
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


        /// <summary>
        /// Calculates and displays the total revenue generated from all payments in the car leasing system.
        /// </summary>
        /// <remarks>
        /// This method utilizes an instance of the 'ICarLeaseRepository' interface to access the 'CalculateTotalRevenue' method
        /// from the 'CarLeaseRepositoryImpl' class, which interacts with the data source to calculate the total revenue.
        /// The total revenue is then displayed in the console. In case of any exceptions during the process, the error message
        /// is caught and printed to the console.
        /// </remarks>
        public static void CalculateTotalRevenue()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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


        /// <summary>
        /// Retrieves and displays the payment history for a specific customer based on the entered Customer ID.
        /// </summary>
        /// <remarks>
        /// This method uses an instance of the 'ICarLeaseRepository' interface and the 'CarLeaseRepositoryImpl' class
        /// to interact with the data source. It prompts the user to enter a Customer ID and then retrieves the payment
        /// history for that customer. The retrieved payment details, including Payment ID, Lease ID, Payment Date, and Amount,
        /// are displayed in the console. If the customer is not found or any other exceptions occur during the process,
        /// appropriate error messages are displayed.
        /// </remarks>
        public static void RetrievePaymentHistory()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("           Enter the Customer ID for which you want to retreive payment history:                  ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                int custId = int.Parse(Console.ReadLine());
                carLeaseRepository.FindCustomerById(custId);
                List<Payment> payment = carLeaseRepository.RetrievePaymentHistory(custId);
                foreach (Payment payments in payment)
                {
                    Console.WriteLine($"\nCustomerID : {custId}\nPaymentID : {payments.PaymentID}\nLeaseID : {payments.LeaseID}\nPaymentDate : {payments.PaymentDate}\nAmount: {payments.Amount}");
                    Console.WriteLine();
                }
            }
            catch (CustomerNotFoundException c)
            {
                //error Already shown in findcarbyid method
                Console.WriteLine(c.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        /// <summary>
        /// Calculates and displays the total cost of a lease based on the entered Lease ID.
        /// </summary>
        /// <remarks>
        /// This method uses an instance of the 'ICarLeaseRepository' interface and the 'CarLeaseRepositoryImpl' class
        /// to interact with the data source. It prompts the user to enter a Lease ID and then calculates and displays
        /// the total cost of the lease, considering the lease type, start date, and end date. If the lease is not found
        /// or any other exceptions occur during the process, appropriate error messages are displayed.
        /// </remarks>
        public static void LeaseCalculator()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                               Enter Lease Id for which you want to find total cost               ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                int leaseId = int.Parse(Console.ReadLine());
                carLeaseRepository.FindLeaseById(leaseId);
                carLeaseRepository.LeaseCalculator(leaseId);
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

        /// <summary>
        /// Finds and displays details of a lease based on the entered Lease ID.
        /// </summary>
        /// <remarks>
        /// This method uses an instance of the 'ICarLeaseRepository' interface and the 'CarLeaseRepositoryImpl' class
        /// to interact with the data source. It prompts the user to enter a Lease ID and then finds and displays the
        /// details of the lease, including Lease ID, Customer ID, Vehicle ID, Start Date, and End Date. If the lease is
        /// not found or any other exceptions occur during the process, appropriate error messages are displayed.
        /// </remarks>
        public static void FindLeaseById()
        {
            ICarLeaseRepository CarLeaseRepository = new CarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Enter Lease Id to find                                          ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
           
            try
            {
                int leaseid = int.Parse(Console.ReadLine());
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

        /// <summary>
        /// Adds a new car to the system based on user input and displays the list of cars after insertion.
        /// </summary>
        /// <remarks>
        /// This method prompts the user to enter details of a new car, including Make, Model, Year, Daily Rate,
        /// Status, Passenger Capacity, and Engine Capacity. It then creates a new 'Vehicle' object with the entered
        /// details and adds it to the data source using an instance of the 'ICarLeaseRepository' interface and the
        /// 'CarLeaseRepositoryImpl' class. After insertion, it fetches and displays the list of all cars in the system.
        /// If any errors occur during the process, appropriate error messages are displayed.
        /// </remarks>
        public static void AddCar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Enter Details of car                                            ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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


        /// <summary>
        /// Adds a new customer to the system based on user input and displays the list of customers after insertion.
        /// </summary>
        /// <remarks>
        /// This method prompts the user to enter details of a new customer, including First Name, Last Name, Email,
        /// and Phone Number. It then creates a new 'Customer' object with the entered details and adds it to the data 
        /// source using an instance of the 'ICarLeaseRepository' interface and the 'CarLeaseRepositoryImpl' class. 
        /// After insertion, it fetches and displays the list of all customers in the system. If any errors occur during 
        /// the process, appropriate error messages are displayed.
        /// </remarks>
        public static void AddCustomer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Enter Details of Customer                                      ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            try
            {
     
               
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

        /// <summary>
        /// Creates a new lease based on user input, validates customer and car availability, and displays the list of leases after insertion.
        /// </summary>
        /// <remarks>
        /// This method prompts the user to enter details of a new lease, including Customer ID, Car ID, Start Date, and End Date.
        /// It then checks the availability of the specified customer and car using the 'FindCustomerById' and 'FindCarById' methods.
        /// If both customer and car are available, it creates a new 'Lease' object using the 'ICarLeaseRepository' interface and 
        /// the 'CarLeaseRepositoryImpl' class. After lease creation, it fetches and displays the list of all leases in the system.
        /// If any errors occur during the process, appropriate error messages are displayed.
        /// </remarks>
        public static void CreateLease()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

                if (endDate <= startDate)
                {
                    throw new Exception("End date should be greater than the start date.");
                }
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

        /// <summary>
        /// Finds and displays details of a vehicle based on user input of the Vehicle ID.
        /// </summary>
        /// <remarks>
        /// This method prompts the user to enter a Vehicle ID and uses the 'ICarLeaseRepository' interface and 
        /// the 'CarLeaseRepositoryImpl' class to find the corresponding vehicle using the 'FindCarById' method.
        /// If the vehicle is found, it displays its details, including Make, Model, Year, Daily Rate, Status,
        /// Passenger Capacity, and Engine Capacity. If the specified vehicle is not found, a 'CarNotFoundException'
        /// is caught and an appropriate error message is displayed. Other exceptions are also caught and handled
        /// with corresponding error messages.
        /// </remarks>
        public static void FindCarById()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Enter Car Id to find                                          ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            
            try
            {
                int carId = int.Parse(Console.ReadLine());
                Vehicle foundCar = carLeaseRepository.FindCarById(carId);
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                            Car Found                                                   ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                Console.WriteLine($"Car Id: {carId}\nMake: {foundCar.Make}\nModel: {foundCar.Model}\nYear: {foundCar.Year}\nDaily Rate: {foundCar.DailyRate}\nStatus: {foundCar.Status}\nPassenger Capacity: {foundCar.PassengerCapacity}\nEngine Capacity: {foundCar.EngineCapacity}");
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

        /// <summary>
        /// Displays a list of all vehicles, including details such as Vehicle ID, Make, Model, Year,
        /// Daily Rate, Status, Passenger Capacity, and Engine Capacity.
        /// </summary>
        /// <remarks>
        /// This method uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to retrieve
        /// the list of all vehicles using the 'ListAllCars' method. It then iterates through the list and
        /// displays the details of each vehicle. If an exception occurs during the process, it is caught,
        /// and an error message is displayed.
        /// </remarks>
        public static void ListAllCars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Customers                                        ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

        /// <summary>
        /// Finds and displays details of a customer based on the provided customer ID.
        /// </summary>
        /// <remarks>
        /// This method uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to retrieve
        /// customer details using the 'FindCustomerById' method. It prompts the user to enter a customer ID,
        /// attempts to find the customer, and displays the customer's information if found. If the customer is
        /// not found, a 'CustomerNotFoundException' is caught and an appropriate error message is displayed.
        /// Any other exceptions are caught and displayed as error messages.
        /// </remarks>
        public static void FindCustomerById()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Enter Customer Id to find                                          ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            int custId = int.Parse(Console.ReadLine());
            try
            {
                Customer foundCustomer = carLeaseRepository.FindCustomerById(custId);
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                   Customer Found                                                 ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                Console.WriteLine($"\nCustomer Id: {custId}\nFirst Name: {foundCustomer.FirstName}\nLast Name: {foundCustomer.LastName}\nEmail: {foundCustomer.Email}\nPhone Number: {foundCustomer.PhoneNumber}");
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

        /// <summary>
        /// Updates the information of a customer based on the provided customer ID.
        /// </summary>
        /// <remarks>
        /// This method uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to retrieve
        /// customer details using the 'FindCustomerById' method. It then prompts the user to enter updated
        /// information for the customer, including first name, last name, email, and phone number. It creates
        /// a new 'Customer' object with the updated information and calls the 'UpdateCustomerInformation' method
        /// of the repository to perform the update. If the update is successful, it displays a success message,
        /// updates the customer list, and shows the updated list. If the customer is not found, a
        /// 'CustomerNotFoundException' is caught, and an appropriate error message is displayed. Any other
        /// exceptions are caught and displayed as error messages.
        /// </remarks>
        public static void UpdateCustomerInformation()
        {

            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            try
            {
                ListCustomers();
                Console.WriteLine("Now Enter the Details of customerId which you want to Update: ");
                Console.WriteLine("\nEnter the Customer id:");
                int custId = int.Parse(Console.ReadLine());
                carLeaseRepository.FindCustomerById(custId);
                Console.WriteLine("\nNow please Enter the other details:");
                Console.WriteLine("\nEnter the FirstName");
                string firstName = Console.ReadLine();
                Console.WriteLine("\nEnter the LastName");
                string lastName = Console.ReadLine();
                Console.WriteLine("\nEnter the Email");
                string email = Console.ReadLine();
                Console.WriteLine("\nEnter the Phone Number");
                string phoneNumber = Console.ReadLine();
                Customer newCustomer = new Customer
                {
                    CustomerID = custId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber
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


        /// <summary>
        /// Displays a list of active leases, showing lease details such as ID, customer ID, vehicle ID, start date,
        /// and end date (or "Ongoing" for ongoing leases). It uses the 'ICarLeaseRepository' interface and
        /// 'CarLeaseRepositoryImpl' class to retrieve the list of active leases using the 'ListActiveLeases' method.
        /// The active leases are then displayed to the console. If no active leases are found, an appropriate message
        /// is displayed. Any exceptions encountered during the process are caught and displayed as error messages.
        /// </summary>
        public static void CheckActiveLeases()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                        Active Leases List                                          ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                List<Lease> ActiveLeaseList = carLeaseRepository.ListActiveLeases();
                
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



        /// <summary>
        /// Displays a list of available cars, showing details such as vehicle ID, make, model, year, daily rate, status,
        /// passenger capacity, and engine capacity. It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' 
        /// class to retrieve the list of available cars using the 'ListAvailableCars' method. The available cars are then 
        /// displayed to the console. If no cars are available, an appropriate message is displayed. Any exceptions encountered 
        /// during the process are caught and displayed as error messages.
        /// </summary>
        public static void ListAvailableCars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Available Cars:                                         ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            try
            {
                List<Vehicle> listAvailableCars = carLeaseRepository.ListAvailableCars();
                
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

        /// <summary>
        /// Displays a list of customers, showing details such as customer ID, first name, last name, email, and phone number.
        /// It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to retrieve the list of customers
        /// using the 'ListCustomers' method. The customer details are then displayed to the console. Any exceptions encountered
        /// during the process are caught and displayed as error messages.
        /// </summary>
        public static void ListCustomers()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       List of Customers                                        ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

        /// <summary>
        /// Displays a list of leases, showing details such as lease ID, customer ID, vehicle ID, start date, and end date.
        /// It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to retrieve the list of leases
        /// using the 'ListLeaseHistory' method. The lease details are then displayed to the console. Any exceptions encountered
        /// during the process are caught and displayed as error messages.
        /// </summary>
        public static void ListLeaseHistory()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

        /// <summary>
        /// Displays a list of rented cars, showing details such as vehicle ID, make, model, year, daily rate, status, passenger capacity, and engine capacity.
        /// It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to retrieve the list of rented cars
        /// using the 'ListRentedCars' method. The vehicle details are then displayed to the console. Any exceptions encountered
        /// during the process are caught and displayed as error messages.
        /// </summary>
        public static void ListRentedCars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

        /// <summary>
        /// Records a payment by prompting the user to enter an existing lease ID and payment amount.
        /// It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to find the lease by ID
        /// using the 'FindLeaseById' method and then records the payment using the 'RecordPayment' method.
        /// Any exceptions encountered during the process are caught and displayed as error messages.
        /// </summary>
        public static void RecordPayment()
        {

            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       Enter Data for Recording Payment                      ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");

            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                       Enter Data for Recording Payment                      ");
                Console.WriteLine("-------------------------------------------------------------------------------------------------\n");

                
                Console.WriteLine("Please Enter the existing Lease id which you want to insert in payment record: ");
                int leaseId = int.Parse(Console.ReadLine());
                Lease lease1 = carLeaseRepository.FindLeaseById(leaseId);
                Console.WriteLine("----------------Now Enter Amount to insert data in Payment table-------------------\n");
                double paymentAmount1 = double.Parse(Console.ReadLine());
                carLeaseRepository.RecordPayment(lease1, paymentAmount1);


            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Removes a car from the vehicle table by prompting the user to enter the car ID.
        /// It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to remove the car
        /// using the 'RemoveCar' method. Any exceptions encountered during the process are caught and displayed as error messages.
        /// </summary>
        public static void RemoveCar()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

        /// <summary>
        /// Removes a customer from the customer table by prompting the user to enter the customer ID.
        /// It uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to remove the customer
        /// using the 'RemoveCustomer' method. CustomerNotFoundException is caught separately, and other exceptions are displayed as error messages.
        /// </summary>
        public static void RemoveCustomer()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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

        /// <summary>
        /// Handles the process of returning a leased car by taking the Lease ID as user input.
        /// Uses the 'ICarLeaseRepository' interface and 'CarLeaseRepositoryImpl' class to return the car
        /// through the 'ReturnCar' method. Displays the updated list of cars after the return operation.
        /// Catches LeaseNotFoundException and other exceptions separately and displays error messages.
        /// </summary>
        public static void ReturnCar()
        {
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepositoryImpl();
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
