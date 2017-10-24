using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Controllers;
using WebApplication3.BLL;
using WebApplication3.DAL;
using WebApplication3.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UnitTesting
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void Airplanes_show_view()
        {

            //Arrange
            var controller = new AdminController(new AirplaneBLL(new AirplaneStub()), new AirportBLL(new AirportStub()));

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
            var result = (ViewResult)controller.Airplanes();
            var resultList = (List<Airplane>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Model, resultList[i].Model);
                Assert.AreEqual(expectedResult[i].Seats, resultList[i].Seats);
            }

        }
    }
}
