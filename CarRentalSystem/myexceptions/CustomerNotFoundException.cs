using System;
using System.Runtime.Serialization;

namespace CarRentalSystem.DAO
{
    [Serializable]
    internal class CustomerNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "Customer not found with the entered customer id";
            }
        }

    }
}