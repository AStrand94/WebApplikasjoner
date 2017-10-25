using System;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Controllers;
using WebApplication3.BLL;
using WebApplication3.DAL;
using WebApplication3.Model;
using System.Web.Mvc;

namespace WebApplication3.UnitTesting
{
    [TestClass]
    public class AirplaneBLLTest
    {

        [TestMethod]
        public void GetAllAirplanes()
        {
            //Arrange
            var bll = new AirplaneBLL(new AirplaneStub());

            var expectedResult = new List<Airplane>();
            var airplane = new Airplane()
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            expectedResult.Add(airplane);
            expectedResult.Add(airplane);
            expectedResult.Add(airplane);

            //Act
            var result = bll.GetAllAirplanes();
            var resultList = (List<Airplane>)result;

            //Assert

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Model, resultList[i].Model);
                Assert.AreEqual(expectedResult[i].Seats, resultList[i].Seats);
            }
        }

        [TestMethod]
        public void InsertAirplane()
        {
            //Arrange
            var bll = new AirplaneBLL(new AirplaneStub());

            var expectedResult = 0;

            Airplane airplane0 = new Airplane();
            airplane0.Id = 0;

            Airplane airplane1 = new Airplane();
            airplane1.Id = 1;



            //Act
            var actualResult0 = bll.InsertAirplane(airplane0);
            var actualResult1 = bll.InsertAirplane(airplane1);

            //Assert
            Assert.AreEqual(expectedResult, airplane0.Id);
            Assert.AreNotEqual(expectedResult, actualResult1);
        }

        [TestMethod]
        public void CanDeleteAirplane()
        {

        }
    }
}
