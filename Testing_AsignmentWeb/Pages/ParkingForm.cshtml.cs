using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Testing_Asignment;

namespace Testing_Asignment.Pages
{
    public class ParkingFormModel : PageModel
    {
        private readonly ParkingService _parkingService;

        // Simple discount service for the web layer (always returns 0.9 = 10% off)
        private class DefaultDiscountService : IDiscountService
        {
            public double GetDiscount() => 0.9;
        }

        public ParkingFormModel()
        {
            _parkingService = new ParkingService(new DefaultDiscountService());
        }

        [BindProperty(SupportsGet = true)]
        public int Hours { get; set; }

        [BindProperty(SupportsGet = true)]
        public string VehicleType { get; set; } = "standard";

        public double? FeeResult { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            FeeResult = _parkingService.CalculateFee(Hours, VehicleType);
        }
    }
}