using CarRentalSystem.Entity;
using CarRentalSystem.myexceptions;
using CarRentalSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarRentalSystem.DAO
{

    public class CarLeaseRepositoryImpl : ICarLeaseRepository
    {
        SqlConnection conn;

        /// <summary>
        /// Adds a new car to the database.
        /// </summary>
        /// <param name="car">The Vehicle object representing the car to be added.</param>
        /// <remarks>
        /// This method inserts a new record into the Vehicle table in the database with the provided car details.
        /// After insertion, it retrieves the generated identity (VehicleID) for the newly added car.
        /// If the insertion is successful (VehicleID > 0), a success message is displayed.
        /// If the insertion fails, an error message is displayed.
        /// </remarks>

        public void AddCar(Vehicle car)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Vehicle (make, model, year, dailyRate, status, passengerCapacity, engineCapacity) VALUES (@make, @model, @year, @dailyRate, @status, @passengerCapacity, @engineCapacity); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@make", car.Make);
                        cmd.Parameters.AddWithValue("@model", car.Model);
                        cmd.Parameters.AddWithValue("@year", car.Year);
                        cmd.Parameters.AddWithValue("@dailyRate", car.DailyRate);
                        cmd.Parameters.AddWithValue("@status", car.Status);
                        cmd.Parameters.AddWithValue("@passengerCapacity", car.PassengerCapacity);
                        cmd.Parameters.AddWithValue("@engineCapacity", car.EngineCapacity);
                        car.VehicleID = Convert.ToInt32(cmd.ExecuteScalar());

                        if (car.VehicleID > 0)
                        {
                            Console.WriteLine("----------------------------------Car added successfully------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------Failed to add the car---------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Retrieves a list of all cars from the database.
        /// </summary>
        /// <returns>A list of Vehicle objects representing all cars in the database.</returns>
        /// <remarks>
        /// This method executes a SQL query to retrieve all car records from the Vehicle table in the database.
        /// It creates a list of Vehicle objects, populates each object with the retrieved data, and adds them to the list.
        /// The method then returns the list of all cars.
        /// </remarks>
        public List<Vehicle> ListAllCars()
        {

            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT * FROM Vehicle"; // Retrieve all cars
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        List<Vehicle> allCarsList = new List<Vehicle>();
                        while (dr.Read())
                        {
                            Vehicle car = new Vehicle
                            {
                                VehicleID = dr.GetInt32(0),
                                Make = dr.GetString(1),
                                Model = dr.GetString(2),
                                Year = dr.GetInt32(3),
                                DailyRate = (double)dr.GetDecimal(4),
                                Status = dr.GetString(5),
                                PassengerCapacity = dr.GetInt32(6),
                                EngineCapacity = dr.GetInt32(7)

                            };

                            allCarsList.Add(car);
                        }

                        return allCarsList;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw;
            }

        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">The Customer object representing the customer to be added.</param>
        /// <remarks>
        /// This method inserts a new record into the Customer table in the database with the provided customer details.
        /// It uses a parameterized SQL query to ensure data integrity and prevent SQL injection.
        /// After insertion, it checks the number of rows affected to determine the success of the operation.
        /// If the insertion is successful (rowsAffected > 0), a success message is displayed.
        /// If the insertion fails, an error message is displayed.
        /// </remarks>
        public void AddCustomer(Customer customer)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();

                    string query = "INSERT INTO Customer (firstName, lastName, email, phoneNumber) VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("---------------------------Customer added successfully----------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("--------------------------Failed to add customer-------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        /// <summary>
        /// Creates a new lease by inserting a record into the Lease table in the database.
        /// </summary>
        /// <param name="customerID">The ID of the customer associated with the lease.</param>
        /// <param name="carID">The ID of the car associated with the lease.</param>
        /// <param name="startDate">The start date of the lease.</param>
        /// <param name="endDate">The end date of the lease.</param>
        /// <returns>A Lease object representing the created lease, or null if the creation fails.</returns>
        /// <remarks>
        /// This method executes a parameterized SQL query to insert a new lease record into the Lease table.
        /// It retrieves the generated identity (LeaseID) for the newly created lease.
        /// If the creation is successful (LeaseID > 0), a success message is displayed, and a Lease object is returned.
        /// If the creation fails, an error message is displayed, and null is returned or an exception is thrown based on business logic.
        /// </remarks>

        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Lease (customerID, vehicleID, startDate, endDate) VALUES (@CustomerID, @CarID, @StartDate, @EndDate); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.Parameters.AddWithValue("@CarID", carID);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        Lease generatedLeaseID = new Lease();
                        generatedLeaseID.LeaseID = Convert.ToInt32(cmd.ExecuteScalar());

                        if (generatedLeaseID.LeaseID > 0)
                        {
                            Console.WriteLine("---------------------------------Lease created successfully--------------------------------------");
                            return new Lease
                            {
                                LeaseID = generatedLeaseID.LeaseID,
                                CustomerID = customerID,
                                VehicleID = carID,
                                StartDate = startDate,
                                EndDate = endDate
                            };
                        }
                        else
                        {
                            Console.WriteLine("--------------------------------Failed to create lease--------------------------------------------");
                            return null; // Return null or throw an exception based on your business logic
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Finds and retrieves a car by its ID from the database.
        /// </summary>
        /// <param name="carID">The ID of the car to be found.</param>
        /// <returns>A Vehicle object representing the found car.</returns>
        /// <exception cref="CarNotFoundException">Thrown if the car with the specified ID is not found.</exception>
        /// <remarks>
        /// This method executes a SQL query to retrieve a car record from the Vehicle table based on the provided car ID.
        /// If a matching record is found, it creates a Vehicle object with the retrieved data and returns it.
        /// If no matching record is found, a CarNotFoundException is thrown.
        /// </remarks>

        public Vehicle FindCarById(int carID)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT * FROM Vehicle WHERE vehicleID = {carID};";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            return new Vehicle
                            {
                                VehicleID = dr.GetInt32(0),
                                Make = dr.GetString(1),
                                Model = dr.GetString(2),
                                Year = dr.GetInt32(3),
                                DailyRate = (double)dr.GetDecimal(4),
                                Status = dr.GetString(5),
                                PassengerCapacity = dr.GetInt32(6),
                                EngineCapacity = dr.GetInt32(7)
                            };
                        }
                        else
                        {
                            throw new CarNotFoundException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"Exception: {ex.Message}");
                throw;

            }

        }

        /// <summary>
        /// Finds and retrieves a customer by their ID from the database.
        /// </summary>
        /// <param name="customerID">The ID of the customer to be found.</param>
        /// <returns>A Customer object representing the found customer.</returns>
        /// <exception cref="CustomerNotFoundException">Thrown if the customer with the specified ID is not found.</exception>
        /// <remarks>
        /// This method executes a SQL query to retrieve a customer record from the Customer table based on the provided customer ID.
        /// If a matching record is found, it creates a Customer object with the retrieved data and returns it.
        /// If no matching record is found, a CustomerNotFoundException is thrown.
        /// </remarks>
        public Customer FindCustomerById(int customerID)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = $"SELECT * FROM Customer WHERE CustomerID = {customerID}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            return new Customer
                            {
                                CustomerID = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Email = dr.GetString(3),
                                PhoneNumber = dr.GetString(4)
                            };
                        }
                        else
                        {
                            throw new CustomerNotFoundException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Updates the information of an existing customer in the database.
        /// </summary>
        /// <param name="customer">The Customer object containing the updated information.</param>
        /// <returns>True if the customer information is successfully updated; otherwise, false.</returns>
        /// <remarks>
        /// This method executes a SQL query to update the information of an existing customer in the Customer table.
        /// The update is based on the provided Customer object, and it includes fields such as FirstName, LastName, Email, and PhoneNumber.
        /// The method returns true if the update is successful (rowsAffected > 0), otherwise, it returns false.
        /// </remarks>
        public bool UpdateCustomerInformation(Customer customer)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();

                    // SQL query to delete the customer by ID
                    string query = $@"UPDATE Customer 
                                    SET FirstName = '{customer.FirstName}', 
                                    LastName = '{customer.LastName}', 
                                    Email = '{customer.Email}', 
                                    PhoneNumber = '{customer.PhoneNumber}' 
                                    WHERE CustomerID = {customer.CustomerID}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw; // Rethrow to allow handling in calling code
            }

        }


        public Lease FindLeaseById(int leaseId)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT * FROM Lease WHERE LeaseID = {leaseId}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            return new Lease
                            {
                                LeaseID = dr.GetInt32(0),
                                VehicleID = dr.GetInt32(1),
                                CustomerID = dr.GetInt32(2),
                                StartDate = dr.GetDateTime(3),
                                EndDate = dr.GetDateTime(4),
                                Type = dr.GetString(5)
                            };
                        }
                        else
                        {
                            throw new LeaseNotFoundException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Finds and retrieves a lease by its ID from the database.
        /// </summary>
        /// <param name="leaseId">The ID of the lease to be found.</param>
        /// <returns>A Lease object representing the found lease.</returns>
        /// <exception cref="LeaseNotFoundException">Thrown if the lease with the specified ID is not found.</exception>
        /// <remarks>
        /// This method executes a SQL query to retrieve a lease record from the Lease table based on the provided lease ID.
        /// If a matching record is found, it creates a Lease object with the retrieved data and returns it.
        /// If no matching record is found, a LeaseNotFoundException is thrown.
        /// </remarks>
        public List<Lease> ListActiveLeases()
        {
            List<Lease> activeLeasesList = new List<Lease>();
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Lease WHERE StartDate <= GETDATE() AND EndDate >= GETDATE()"; // Assuming leases within start and end dates are considered active
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Lease lease = new Lease
                            {
                                LeaseID = dr.GetInt32(0),
                                VehicleID = dr.GetInt32(1),
                                CustomerID = dr.GetInt32(2),
                                StartDate = dr.GetDateTime(3),
                                EndDate = dr.GetDateTime(4),
                                Type = dr.GetString(5)
                            };
                            activeLeasesList.Add(lease);
                        }
                        return activeLeasesList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;// Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Calculates and displays the total cost of a lease based on its ID.
        /// </summary>
        /// <param name="leaseID">The ID of the lease for which to calculate the total cost.</param>
        /// <remarks>
        /// This method executes a SQL query to retrieve lease information (LeaseId, Type, StartDate, EndDate) based on the provided lease ID.
        /// It then calls the CalculateLeaseCost method to calculate the total cost of the lease.
        /// The calculated total cost is displayed along with other lease details using Console.WriteLine.
        /// </remarks>
        public void LeaseCalculator(int leaseID)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT LeaseId, Type, StartDate, EndDate FROM Lease  WHERE LeaseID = {leaseID}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int leaseId = reader.GetInt32(0);
                            string type = reader.GetString(1);
                            DateTime startDate = reader.GetDateTime(2);
                            DateTime endDate = reader.GetDateTime(3);
                            double totalCost = CalculateLeaseCost(type, startDate, endDate);
                            Console.WriteLine($"Total Cost of Lease ID: {leaseId}, Type: {type}, Total Cost: {totalCost}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }


        //Concrete Method

        /// <summary>
        /// Calculates the total cost of a lease based on the lease type, start date, and end date.
        /// </summary>
        /// <param name="type">The type of the lease (e.g., "Daily" or "Monthly").</param>
        /// <param name="startDate">The start date of the lease.</param>
        /// <param name="endDate">The end date of the lease.</param>
        /// <returns>The total cost of the lease.</returns>
        /// <exception cref="ArgumentException">Thrown if the provided lease type is invalid.</exception>
        /// <remarks>
        /// This method calculates the total cost of a lease based on its type and duration.
        /// If the type is "Daily," it calculates the cost by multiplying the daily rate by the number of days.
        /// If the type is "Monthly," it calculates the cost by multiplying the monthly rate by the number of months.
        /// An ArgumentException is thrown if an invalid lease type is provided.
        /// </remarks>

        //concrete method
        public double CalculateLeaseCost(string type, DateTime startDate, DateTime endDate)
        {
            double dailyRate = 50.00;  // Adjust as needed
            double monthlyRate = 1200.00;  // Adjust as needed

            if (type == "Daily")
            {
                int duration = endDate.Day - startDate.Day;
                return dailyRate * duration;
            }
            else if (type == "Monthly")
            {
                int months = endDate.Month - startDate.Month + 1;
                return monthlyRate * months;
            }
            else
            {
                throw new ArgumentException("Invalid lease type");
            }
        }

        /// <summary>
        /// Retrieves the payment history for a customer based on their customer ID.
        /// </summary>
        /// <param name="custId">The ID of the customer for whom to retrieve the payment history.</param>
        /// <returns>A List of Payment objects representing the payment history for the customer.</returns>
        /// <exception cref="CustomerNotFoundException">Thrown if the customer with the specified ID is not found.</exception>
        /// <remarks>
        /// This method executes a SQL query to retrieve payment information (PaymentID, PaymentDate, Amount) for a customer
        /// based on the provided customer ID. It joins the Payment, Lease, and Customer tables to gather relevant data.
        /// The retrieved payment information is used to create Payment objects, which are added to a List. If no payment history
        /// is found for the customer, a CustomerNotFoundException is thrown.
        /// </remarks>
        public List<Payment> RetrievePaymentHistory(int custId)
        {
            List<Payment> paymentList = new List<Payment>();
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT p.PaymentID,p.PaymentDate, p.Amount, l.LeaseId, c.FirstName, c.LastName
                            FROM Payment p
                            INNER JOIN Lease l ON p.LeaseId = l.LeaseId
                            INNER JOIN Customer c ON l.CustomerId = c.CustomerID
                            WHERE l.CustomerId = {custId}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Payment payment = new Payment
                                {
                                    PaymentID = reader.GetInt32(0),
                                    LeaseID = reader.GetInt32(3),
                                    PaymentDate = reader.GetDateTime(1),
                                    Amount = Convert.ToDouble(reader.GetDecimal(2))
                                };
                                paymentList.Add(payment);
                            }
                            return paymentList;
                        }
                        else
                        {
                            Console.WriteLine($"Customer with customer id : {custId} has no payment history");
                            return paymentList;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }


        }

        /// <summary>
        /// Calculates the total revenue generated from all payments in the database.
        /// </summary>
        /// <returns>The total revenue as a decimal.</returns>
        /// <remarks>
        /// This method executes a SQL query to sum up the 'Amount' column from the 'Payment' table,
        /// representing the total revenue generated from all payments. If the result is not DBNull.Value,
        /// it converts the result to a decimal and assigns it to the 'totalRevenue' variable.
        /// The method then returns the calculated total revenue.
        /// </remarks>
        public decimal CalculateTotalRevenue()
        {
            decimal totalRevenue = 0;
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT SUM(Amount) FROM Payment";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalRevenue = (decimal)result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return totalRevenue;

        }

        /// <summary>
        /// Retrieves a list of available cars from the database.
        /// </summary>
        /// <returns>A List of Vehicle objects representing available cars.</returns>
        /// <remarks>
        /// This method executes a SQL query to retrieve all vehicle records from the 'Vehicle' table
        /// where the status is set to 'available'. The retrieved data is used to create Vehicle objects,
        /// which are added to a List. The method then returns the list of available cars.
        /// </remarks>
        public List<Vehicle> ListAvailableCars()
        {
            List<Vehicle> listAvailableCarsList = new List<Vehicle>();
            try
            {

                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Vehicle WHERE Status = 'available'"; // Assuming leases within start and end dates are considered active
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Vehicle lease = new Vehicle
                            {
                                VehicleID = dr.GetInt32(0),
                                Make = dr.GetString(1),
                                Model = dr.GetString(2),
                                Year = dr.GetInt32(3),
                                DailyRate = (double)dr.GetDecimal(4),
                                Status = dr.GetString(5),
                                PassengerCapacity = dr.GetInt32(6),
                                EngineCapacity = dr.GetInt32(7)
                            };

                            listAvailableCarsList.Add(lease);

                        }


                        return listAvailableCarsList;
                    }
                }
            }

            catch (Exception ex)
            {
                throw;// Rethrow to allow handling in calling code
            }
        }


        /// <summary>
        /// Retrieves a list of all customers from the database.
        /// </summary>
        /// <returns>A List of Customer objects representing all customers.</returns>
        /// <remarks>
        /// This method executes a SQL query to retrieve all customer records from the 'Customer' table.
        /// The retrieved data is used to create Customer objects, which are added to a List.
        /// The method then returns the list of all customers in the database.
        /// </remarks>
        public List<Customer> ListCustomers()
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Customer";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataReader dr = cmd.ExecuteReader();
                        List<Customer> customerList = new List<Customer>();
                        while (dr.Read())
                        {

                            Customer customer = new Customer
                            {
                                CustomerID = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Email = dr.GetString(3),
                                PhoneNumber = dr.GetString(4)
                            };
                            customerList.Add(customer);

                        }
                        return customerList;
                    }
                }

            }
            catch (Exception ex)
            {
                throw; // Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Retrieves a list of all lease records from the database.
        /// </summary>
        /// <returns>A List of Lease objects representing the lease history.</returns>
        /// <remarks>
        /// This method executes a SQL query to retrieve all lease records from the 'Lease' table.
        /// The retrieved data is used to create Lease objects, which are added to a List.
        /// The method then returns the list of all lease records, representing the lease history.
        /// </remarks>
        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leaseHistoryList = new List<Lease>();
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Lease ";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            Lease lease = new Lease
                            {
                                LeaseID = dr.GetInt32(0),
                                VehicleID = dr.GetInt32(1),
                                CustomerID = dr.GetInt32(2),
                                StartDate = dr.GetDateTime(3),
                                EndDate = dr.GetDateTime(4),
                                Type = dr.GetString(5)
                            };

                            leaseHistoryList.Add(lease);

                        }

                        return leaseHistoryList;
                    }
                }
            }

            catch (Exception ex)
            {
                throw;// Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Retrieves a list of rented cars from the database.
        /// </summary>
        /// <returns>A List of Vehicle objects representing rented cars.</returns>
        /// <remarks>
        /// This method executes a SQL query to retrieve all vehicle records from the 'Vehicle' table
        /// where the status is set to 'notAvailable', indicating that the cars are currently rented out.
        /// The retrieved data is used to create Vehicle objects, which are added to a List.
        /// The method then returns the list of rented cars (cars with 'notAvailable' status).
        /// </remarks>
        public List<Vehicle> ListRentedCars()
        {
            List<Vehicle> rentedCarsList = new List<Vehicle>();
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Vehicle WHERE status = 'notAvailable'"; // Fetch all cars with status as 'notAvailable' (i.e., rented out)
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            Vehicle car = new Vehicle
                            {
                                VehicleID = dr.GetInt32(0),
                                Make = dr.GetString(1),
                                Model = dr.GetString(2),
                                Year = dr.GetInt32(3),
                                DailyRate = (double)dr.GetDecimal(4),
                                Status = dr.GetString(5),
                                PassengerCapacity = dr.GetInt32(6),
                                EngineCapacity = dr.GetInt32(7)
                            };

                            rentedCarsList.Add(car);
                        }

                        return rentedCarsList;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }

        }


        /// <summary>
        /// Records a payment for a specific lease in the database.
        /// </summary>
        /// <param name="lease">The Lease object for which the payment is recorded.</param>
        /// <param name="amount">The payment amount to be recorded.</param>
        /// <remarks>
        /// This method executes a SQL query to insert a new payment record into the 'Payment' table,
        /// associating it with a specific lease identified by the LeaseID.
        /// The payment amount is provided as a parameter. The method prints a success message if the
        /// payment is recorded successfully, or an error message if the operation fails.
        /// </remarks>
        public void RecordPayment(Lease lease, double amount)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();

                    string query = "INSERT INTO Payment (LeaseID, Amount) VALUES (@LeaseID, @Amount);SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        // Add parameters
                        cmd.Parameters.AddWithValue("@LeaseID", lease.LeaseID);
                        cmd.Parameters.AddWithValue("@Amount", amount);

                        // Execute the query
                        int generatedid = Convert.ToInt32(cmd.ExecuteScalar());
                        if (generatedid > 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------------------------------------------------------------");
                            Console.WriteLine("                                     Payment Recorded Successfully                                 ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------------Failed to create Payment-----------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Removes a specific car from the database based on the provided car ID.
        /// </summary>
        /// <param name="carID">The unique identifier of the car to be removed.</param>
        /// <remarks>
        /// This method executes a SQL query to delete a car record from the 'Vehicle' table
        /// based on the provided car ID. If the deletion is successful, a success message is printed.
        /// If the specified car ID is not found, a CarNotFoundException is thrown.
        /// </remarks>
        public void RemoveCar(int carID)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Vehicle WHERE VehicleID = @CarID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarID", carID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("--------------------------------------------------------------------------------------------------");
                            Console.WriteLine("                                       Car Removed Successfully                                  ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                        }
                        else
                        {
                            throw new CarNotFoundException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Removes a specific customer from the database based on the provided customer ID.
        /// </summary>
        /// <param name="customerID">The unique identifier of the customer to be removed.</param>
        /// <remarks>
        /// This method executes a SQL query to delete a customer record from the 'Customer' table
        /// based on the provided customer ID. If the deletion is successful, a success message is printed.
        /// If the specified customer ID is not found, a CustomerNotFoundException is thrown.
        /// </remarks>
        public void RemoveCustomer(int customerID)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("--------------------------------------------------------------------------------------------------");
                            Console.WriteLine("                                Customer removed successfully                                    ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                        }
                        else
                        {
                            throw new CustomerNotFoundException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw; // Rethrow to allow handling in calling code
            }

        }

        /// <summary>
        /// Updates the status of a leased car to 'returned' based on the provided lease ID.
        /// </summary>
        /// <param name="leaseID">The unique identifier of the lease associated with the car to be returned.</param>
        /// <remarks>
        /// This method checks if the car has already been returned by querying the 'Vehicle' and 'Lease' tables.
        /// If the car is already marked as 'returned', a message is printed, and no further action is taken.
        /// If the car is not returned, the car status is updated to 'returned', indicating that it has been returned.
        /// If the update is successful, a success message is printed. If the specified lease ID is not found, a LeaseNotFoundException is thrown.
        /// </remarks>
        public void ReturnCar(int leaseID)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();

                    // Check if the car is already returned
                    string checkQuery = $@"SELECT Vehicle.Status FROM Vehicle
                                        INNER JOIN Lease ON Vehicle.VehicleID = Lease.VehicleID
                                        WHERE Lease.LeaseID = {leaseID}";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        object statusResult = checkCmd.ExecuteScalar();

                        if (statusResult != null && statusResult.ToString().ToLower() == "returned")
                        {
                            Console.WriteLine("\nCar has already been returned.\n");
                            return;
                        }

                        // Update the car status
                        string updateQuery = $@" UPDATE Vehicle
                                             SET Status = 'returned' 
                                             FROM Vehicle
                                             INNER JOIN Lease ON Vehicle.VehicleID = Lease.VehicleID
                                             WHERE Lease.LeaseID = {leaseID}  AND Vehicle.Status = 'notAvailable'";

                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("\nCar returned successfully.\n");
                        }
                        else
                        {
                            throw new LeaseNotFoundException();
                        }
                    }
                }
            }
            catch (LeaseNotFoundException l)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;

            }

        }

    }



}
