using System;
using System.Runtime.Serialization;

namespace CarRentalSystem.myexceptions
{

    public class CarNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "Car not found with the entered car id";
            }
        }
    }
}