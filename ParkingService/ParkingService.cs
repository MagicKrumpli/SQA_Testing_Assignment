using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Asignment
{
    // Interface for mocking the DiscountService
    public interface IDiscountService
    {
        double GetDiscount();
    }

    public class ParkingService
    {
        // Injecting the discount service via constructor
        private readonly IDiscountService _discountService;

        public ParkingService(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        // Calculate parking fee based on hours and vehicle type
        public double CalculateFee(int hours, string vehicleType)
        {
            double fee;

            if (vehicleType == "standard")
                if ((hours >= 1) && (hours <= 3))
                    fee = hours * 4.0;
                else if (hours >= 4)
                    fee = hours * 3.0;
                else
                    fee = 0.0;

            else if (vehicleType == "electric")
                if ((hours >= 1) && (hours <= 5))
                    fee = hours * 3.0;
                else if (hours >= 6)
                    fee = hours * 2.0;
                else
                    fee = 0.0;
            else
                fee = 0.0;

            if (hours >= 10)
            {
                double discount = _discountService.GetDiscount();
                fee = fee * discount;
            }

            return fee;
        }
    }
}
