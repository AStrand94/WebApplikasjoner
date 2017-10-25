using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Controllers;
using WebApplication3.BLL;
using WebApplication3.DAL;
using WebApplication3.Model;
using System.Web.Mvc;

namespace UnitTesting
{
    [TestClass]
    public class AirportBLLTest
    {
        [TestMethod]
        public void GetAllAirports()
        {
            //Arrange
            var bll = new AirportBLL(new AirportDAL());

            var expectedResult = new List<Airport>();
            var airport = new Airport()
            {
                Id = 1,
            };

            expectedResult.Add(airport);
            expectedResult.Add(airport);
            expectedResult.Add(airport);

            //Act
            var result = bll.GetAllAirports();
            var resultList = (List<Airport>)result;

            //Assert

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
            }
        }
    }
}
