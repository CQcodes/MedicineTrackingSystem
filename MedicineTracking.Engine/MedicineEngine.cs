using System;
using System.Collections.Generic;
using MedicineTracking.Data.Model;
using MedicineTracking.Data.Repository;

namespace MedicineTracking.Engine
{
    public interface IMedicineEngine : IDisposable
    {
        List<Medicine> GetMedicines();
        Medicine GetMedicineDetails(int id);
        int AddMedicine(Medicine request);
        int UpdateMedicine(int id, string note);
        int DeleteMedicine(int id);
    }

    public class MedicineEngine : IMedicineEngine
    {
        private IMedicineRepository m_MedicineRepository;

        public MedicineEngine(IMedicineRepository medicineRepository)
        {
            m_MedicineRepository = medicineRepository;
        }

        public List<Medicine> GetMedicines()
        {
            return m_MedicineRepository.GetMedicines();
        }

        public Medicine GetMedicineDetails(int id)
        {
            return m_MedicineRepository.GetMedicine(id);
        }

        public int AddMedicine(Medicine request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
                throw new InvalidOperationException("Medicine Full name is a required field.");
            if (string.IsNullOrWhiteSpace(request.Brand))
                throw new InvalidOperationException("Medicine Brand is a required field.");
            if (request.ExpiryDate <= DateTime.Today.AddDays(15))
                throw new InvalidOperationException("Medicines with expiry date of 15 days or less can not be added.");
            if (request.Quantity <= 0)
                throw new InvalidOperationException("Medicines with quantity less than zero can not be added");
            if (request.Price < 0)
                throw new InvalidOperationException("Medicine price should be non-negative number.");

            request.Id = m_MedicineRepository.GetMaxId() + 1;
            m_MedicineRepository.AddMedicine(request);

            return request.Id;
        }

        public int UpdateMedicine(int id, string note)
        {
            var medicine = m_MedicineRepository.GetMedicine(id);
            if (medicine == null)
                return 0;
            medicine.Notes = note;
            return m_MedicineRepository.UpdateMedicine(medicine);
        }

        public int DeleteMedicine(int id)
        {
            return m_MedicineRepository.DeleteMedicine(id);
        }

        #region dispose method

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool diposing)
        {
            if (!diposing)
                return;

            // dispose all unmanaged code here
            if(m_MedicineRepository != null)
            {
                m_MedicineRepository.Dispose();
                m_MedicineRepository = null;
            }
        }

        #endregion
    }
}
