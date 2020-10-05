using System;
using System.Net;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using WebCarRental.Api.Controllers;
using WebCarRental.Core.Repository;
using WebCarRental.Core.Services;
using Assert = NUnit.Framework.Assert;

namespace WebCarRental.Test
{
    [TestFixture]
    public class CustomerControler_Test
    {
        private CustomersController sut;
        private ICustomerService service;

        
        [SetUp]
        public void SetUp()
        {
            service = A.Fake<ICustomerService>();

            sut = new CustomersController(service);

            sut.Request = new System.Net.Http.HttpRequestMessage();
        }

        [Test]
        public void RentStart_when_car_is_already_rent_is_rented_return_ok()
        {
            // Arrange
            int carId = 5;
            A.CallTo(() => service.RentStart(A<int>.Ignored, A<int>.Ignored)).Returns(true);

            // Act
            var result = sut.RentStart(1, carId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void RentEnd_when_car_is_already_finish_is_back_return_ok()
        {
            // Arrange
            int carId = 5;
            A.CallTo(() => service.RentEnd(A<int>.Ignored, A<int>.Ignored)).Returns(true);

            // Act
            var result = sut.RentEnd(1, carId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }



    }
}
