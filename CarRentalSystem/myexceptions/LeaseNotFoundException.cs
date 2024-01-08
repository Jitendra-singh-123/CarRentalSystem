using System;
using System.Runtime.Serialization;

namespace CarRentalSystem.DAO
{
    [Serializable]
    internal class LeaseNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "Lease not found with the entered Lease id";
            }
        }
    }
}