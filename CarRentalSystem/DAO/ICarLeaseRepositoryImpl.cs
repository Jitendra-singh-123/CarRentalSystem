using CarRentalSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CarRentalSystem.Util;

namespace CarRentalSystem.DAO
{
    class ICarLeaseRepositoryImpl : ICarLeaseRepository
    {
        SqlConnection conn;
        public void AddCar(Vehicle car)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();

                string query = "INSERT INTO Vehicle (make, model, year, dailyRate, status, passengerCapacity, engineCapacity) VALUES (@make, @model, @year, @dailyRate, @status, @passengerCapacity, @engineCapacity)";
                SqlCommand cmd = new SqlCommand(query, conn);


                cmd.Parameters.AddWithValue("@make", car.Make);
                cmd.Parameters.AddWithValue("@model", car.Model);
                cmd.Parameters.AddWithValue("@year", car.Year);
                cmd.Parameters.AddWithValue("@dailyRate", car.DailyRate);
                cmd.Parameters.AddWithValue("@status", car.Status);
                cmd.Parameters.AddWithValue("@passengerCapacity", car.PassengerCapacity);
                cmd.Parameters.AddWithValue("@engineCapacity", car.EngineCapacity);

                // Execute the query
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Car added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add the car!");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close(); // Ensure to close connection in the end
            }
        }

        public List<Vehicle> ListAllCars()
        {
            
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();

                string query = "SELECT * FROM Vehicle"; // Retrieve all cars
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<Vehicle> allCars = new List<Vehicle>();
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

                    allCars.Add(car);
                }

                return allCars;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();

                string query = "INSERT INTO Customer (firstName, lastName, email, phoneNumber) VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add customer.");
                }
            }
            catch (Exception ex)
            {
                throw; ;
                // Handle exceptions, log errors, etc.
            }
            finally
            {

                    conn.Close();

            }
        }

        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "INSERT INTO Lease (customerID, vehicleID, startDate, endDate) VALUES (@CustomerID, @CarID, @StartDate, @EndDate); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@CarID", carID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                int generatedLeaseID = Convert.ToInt32(cmd.ExecuteScalar());

                if (generatedLeaseID > 0)
                {
                    Console.WriteLine("Lease created successfully.");
                    return new Lease
                    {
                        LeaseID = generatedLeaseID,
                        CustomerID = customerID,
                        VehicleID = carID,
                        StartDate = startDate,
                        EndDate = endDate
                    };
                }
                else
                {
                    Console.WriteLine("Failed to create lease.");
                    return null; // Return null or throw an exception based on your business logic
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Vehicle FindCarById(int carID)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE vehicleID = @vehicleID";
                SqlCommand cmd = new SqlCommand(query,conn);
  

                // Add the parameter before opening the connection and executing the query
                cmd.Parameters.AddWithValue("@vehicleID", carID);

                
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
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }
        }

        public Customer FindCustomerById(int customerID)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Customer WHERE CustomerID = @CustID";
                SqlCommand cmd = new SqlCommand(query, conn);


                // Add the parameter before opening the connection and executing the query
                cmd.Parameters.AddWithValue("@CustID", customerID);


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
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }
        }

        public List<Lease> ListActiveLeases()
        {
            List<Lease> activeLeases = new List<Lease>();
            try
            {
                
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Lease WHERE StartDate <= GETDATE() AND EndDate >= GETDATE()"; // Assuming leases within start and end dates are considered active
                SqlCommand cmd = new SqlCommand(query, conn);

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

                    activeLeases.Add(lease);
                    
                }
              
                return activeLeases;
            }
  
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine(ex.Message); 

                throw ;// Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close(); // Ensure to close connection in the end
            }
        }

        public List<Vehicle> ListAvailableCars()
        {
            List<Vehicle> listavailableCars  = new List<Vehicle>();
            try
            { 

                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE Status = 'available'"; // Assuming leases within start and end dates are considered active
                SqlCommand cmd = new SqlCommand(query, conn);

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

                    listavailableCars.Add(lease);

                }

                return listavailableCars;
            }

            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine(ex.Message);

                throw;// Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close(); // Ensure to close connection in the end
            }
        }

        public List<Customer> ListCustomers()
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Customer";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader dr = cmd.ExecuteReader();
                List<Customer> customer = new List<Customer>();
                while (dr.Read())
                {

                    Customer cust =  new Customer
                    {
                        CustomerID = dr.GetInt32(0),
                        FirstName = dr.GetString(1),
                        LastName = dr.GetString(2),
                        Email = dr.GetString(3),
                        PhoneNumber = dr.GetString(4)
                    };
                    customer.Add(cust);
                    
                }
                return customer;

            }
       
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }
        }

        public List<Lease> ListLeaseHistory()
        {
            List<Lease> listLeaseHistory = new List<Lease>();
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Lease "; 
                SqlCommand cmd = new SqlCommand(query, conn);

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

                    listLeaseHistory.Add(lease);

                }

                return listLeaseHistory;
            }
 
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw;// Rethrow to allow handling in calling code
            }
        }

        public List<Vehicle> ListRentedCars()
        {
            List<Vehicle> rentedCars = new List<Vehicle>();
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE status = 'notAvailable'"; // Fetch all cars with status as 'notAvailable' (i.e., rented out)
                SqlCommand cmd = new SqlCommand(query, conn);

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

                    rentedCars.Add(car);
                }

                return rentedCars;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close(); // Ensure to close connection in the end
            }
        }

        public void RecordPayment(Lease lease, double amount)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();

                // SQL query to insert payment details into the Payment table
                string query = "INSERT INTO Payment (LeaseID, Amount) VALUES (@LeaseID, @Amount)";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Add parameters
                cmd.Parameters.AddWithValue("@LeaseID", lease.LeaseID);
                cmd.Parameters.AddWithValue("@Amount", amount);

                // Execute the query
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw; // Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close(); // Close connection
            }
        }

        public void RemoveCar(int carID)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();

                string query = "DELETE FROM Vehicle WHERE VehicleID = @CarID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CarID", carID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Car removed successfully.");
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw;
                // Handle exceptions as needed
            }
            finally
            {
                conn.Close();
            }
        }

        public void RemoveCustomer(int customerID)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();

                // SQL query to delete the customer by ID
                string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);

                // Execute the query
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer removed successfully.");
                }
                else
                {
                    throw new CustomerNotFoundException();
                }
            }
            catch (Exception ex)
            {
                
                throw; // Rethrow to allow handling in calling code
            }
            finally
            {
                conn.Close(); // Close connection
            }
        }

        public void ReturnCar(int leaseID)
        {
            try
            {
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = $"UPDATE Vehicle SET Status = 'Returned' WHERE {leaseID} IN (SELECT l.LeaseID FROM Vehicle v JOIN Lease l ON v.VehicleID = l.VehicleID)"; // Assuming 'Status' is a column indicating the lease status
                SqlCommand cmd = new SqlCommand(query, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Car returned successfully.");
                }
                else
                {
                    throw new LeaseNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw;
                // Handle exceptions, log errors, etc.
            }
            finally
            {
                conn.Close();
            }
        }

    }



}
