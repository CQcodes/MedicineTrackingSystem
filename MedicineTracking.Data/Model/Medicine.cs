using System;

namespace MedicineTracking.Data.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Notes { get; set; }
    }
}
