using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.Entity;

namespace CarRentalSystem.DAO
{
    public interface ICarLeaseRepository
    {
        void AddCar(Vehicle car);
        void RemoveCar(int carID);
        List<Vehicle> ListAllCars();
        List<Vehicle> ListAvailableCars();
        List<Vehicle> ListRentedCars();
        Vehicle FindCarById(int carID);

        // Customer Management
        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerID);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);
        decimal CalculateTotalRevenue();
        bool UpdateCustomerInformation(Customer c);
        void LeaseCalculator(int leaseID);
        List<Payment> RetrievePaymentHistory(int custid);
        Lease FindLeaseById(int leaseID);
        // Lease Management
        Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate);
        void ReturnCar(int leaseID);
        List<Lease> ListActiveLeases();
        List<Lease> ListLeaseHistory();

        // Payment Handling
        void RecordPayment(Lease lease, double amount);
    }
}
