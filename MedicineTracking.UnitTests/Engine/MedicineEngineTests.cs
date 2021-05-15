using System;
using System.Collections.Generic;
using FakeItEasy;
using MedicineTracking.Data.Model;
using MedicineTracking.Data.Repository;
using MedicineTracking.Engine;
using Xunit;

namespace MedicineTracking.UnitTests.Engine
{
    public class MedicineEngineTests
    {
        #region GetMedicines

        [Fact]
        public void Calling_MedicationEngine_GetMedications_Method_Invokes_Repository_GetMedicinesMethod_And_Returns_The_List_Of_Medicines()
        {
            var repo = A.Fake<IMedicineRepository>();
            A.CallTo(() => repo.GetMedicines()).Returns(GenerateADummyListOfMedicines());
            using (var engine = GenerateMedicineEngine(repo))
            {
                var result = engine.GetMedicines();
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
            A.CallTo(() => repo.GetMedicines()).MustHaveHappened();
        }

        #endregion

        #region private methods

        private IMedicineEngine GenerateMedicineEngine(IMedicineRepository repo)
        {
            return new MedicineEngine(repo);
        }

        private List<Medicine> GenerateADummyListOfMedicines()
        {
            return new List<Medicine> {
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
                }
            };
        }

        #endregion
    }
}
