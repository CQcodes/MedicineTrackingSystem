using MedicineTracking.Data.Model;
using MedicineTracking.Engine;
using Microsoft.AspNetCore.Mvc;

namespace MedicineTrackingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicinesController : ControllerBase
    {
        private IMedicineEngine m_MedicineEngine;
        public MedicinesController(IMedicineEngine medicineEngine)
        {
            m_MedicineEngine = medicineEngine;
        }

        [HttpGet]
        public IActionResult GetMedicines()
        {
            var medicines = m_MedicineEngine.GetMedicines();
            return Ok(medicines);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMedicineDetails(int id)
        {
            var medicine = m_MedicineEngine.GetMedicineDetails(id);

            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        [HttpPost]
        public IActionResult AddMedicine(Medicine request)
        {
            var medicineId = m_MedicineEngine.AddMedicine(request);

            return Created($"/medicines/{medicineId}",medicineId);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMedicine(int id, [FromBody]string notes)
        {
            if (m_MedicineEngine.UpdateMedicine(id, notes) == 0)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMedicine(int id)
        {
            if (m_MedicineEngine.DeleteMedicine(id) == 0)
                return NotFound();

            return Ok();
        }
    }
}
