using MedicineTracking.Data.Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MedicineTracking.Data.Repository
{
    public interface IMedicineRepository : IDisposable
    {
        int GetMaxId();
        List<Medicine> GetMedicines();
        Medicine GetMedicine(int id);
        void AddMedicine(Medicine newMedicine);
        int UpdateMedicine(Medicine updatedMedicine);
        int DeleteMedicine(int id);
    }

    public class MedicineRepository: IMedicineRepository
    {
        private List<Medicine> m_Medicines;

        public MedicineRepository()
        {
            LoadMedicines();
        }

        public int GetMaxId()
        {
            return m_Medicines.Max(m => m.Id);
        }

        public List<Medicine> GetMedicines()
        {
            return m_Medicines;
        }

        public Medicine GetMedicine(int id)
        {
            return m_Medicines.FirstOrDefault(a=>a.Id == id);
        }

        public void AddMedicine(Medicine newMedicine)
        {
            m_Medicines.Add(newMedicine);
        }

        public int UpdateMedicine(Medicine updatedMedicine)
        {
            var medicine = m_Medicines.FirstOrDefault(f => f.Id == updatedMedicine.Id);
            if (medicine != null)
            {
                var index = m_Medicines.IndexOf(medicine);
                m_Medicines[index] = updatedMedicine;

                return 1;
            }

            return 0;
        }

        public int DeleteMedicine(int id)
        {
            var medicine = m_Medicines.FirstOrDefault(f => f.Id == id);
            if (medicine != null)
            {
                m_Medicines.Remove(medicine);
                return 1;
            }

            return 0;
        }

        #region Private Methods

        private void LoadMedicines()
        {
            m_Medicines = new List<Medicine> {
                new Medicine
                {
                    Id = 1,
                    FullName = "Covicare",
                    Brand = "Pfizer",
                    Price = 15.25m,
                    Quantity = 100,
                    ExpiryDate = DateTime.Today.AddDays(50),
                    Notes = "Do not sell it without doctor's prescription."
                },
                new Medicine
                {
                    Id = 2,
                    FullName = "Covishield",
                    Brand = "Indian Pharmacy",
                    Price = 9.25m,
                    Quantity = 550,
                    ExpiryDate = DateTime.Today.AddDays(70),
                    Notes = "Do not sell it without doctor's prescription."
                },
                new Medicine
                {
                    Id = 3,
                    FullName = "Namcold",
                    Brand = "cipla",
                    Price = 1.75m,
                    Quantity = 30,
                    ExpiryDate = DateTime.Today.AddDays(15),
                    Notes = "Do not sell it without doctor's prescription."
                },
                new Medicine
                {
                    Id = 4,
                    FullName = "Cough syrup",
                    Brand = "Vicks",
                    Price = 2.45m,
                    Quantity = 10,
                    ExpiryDate = DateTime.Today.AddDays(25),
                    Notes = "Do not sell it without doctor's prescription."
                }
            };
        }

        #endregion

        #region Dispose method

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            // dispose unamanaged code here.
        }

        #endregion
    }
}
