using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AndDigitalApis.Tests
{
    [TestClass]
    public class ApisTestClass
    {
        public AndDigitalApisService service;

        #region Test Setup

        [TestInitialize()]
        public void TestSetUp()
        {
            service = new AndDigitalApisService();
        }

        #endregion

        #region GetAllPhoneNumbers tests

        // All tests are on assumption that the phone list is static 
        [TestMethod]
        public void TestGetAllPhoneNumbers()
        {
            var result = service.GetAllPhoneNumbers(string.Empty, string.Empty);

            Assert.AreEqual(result.Count, 15);
        }

        [TestMethod]
        public void TestGetAllPhoneNumbersFromStartIndex()
        {
            var results = service.GetAllPhoneNumbers("14", "1");
            Assert.AreEqual(results.First().Identity, 15);
        }

        [TestMethod]
        public void TestGetAllPhoneNumbersFromInvalidStartIndex()
        {
            try
            {
                var results = service.GetAllPhoneNumbers("a", "1");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "StartIndex contains invalid value. Only integers are allowed.");
            }
        }

        [TestMethod]
        public void TestGetAllPhoneNumbersWithInvalidResultsPerPage()
        {
            try
            {
                var results = service.GetAllPhoneNumbers("1", "a");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "ResultsPerPage contains invalid value. Only integers are allowed.");
            }
        }

        // Expecting blank list 
        [TestMethod]
        public void TestGetAllPhoneNumbersWithNonExistenceStartIndex()
        {
            var results = service.GetAllPhoneNumbers("20", "1");

            Assert.AreEqual(results.Count, 0);
        }

        #endregion

        #region GetAllPhoneNumbersForCustomer

        [TestMethod]
        public void TestInvalidCustomerId()
        {
            try
            {
                var result = service.GetAllPhoneNumbersForCustomer("a");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "CustomerId contains invalid value. Only integers are allowed.");
            }
        }

        [TestMethod]
        public void TestNonExistingCustomer()
        {
            try
            {
                var results = service.GetAllPhoneNumbersForCustomer("10");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Customer with Identity 10 does not exist on this system.");
            }
        }

        #endregion

        #region ActivatePhoneNumber

        [TestMethod]
        public void ActivatePhoneNumberInvalidPhoneId()
        {
            try
            {
                service.ActivatePhoneNumber(string.Empty);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "PhoneId not passed in request");
            }
        }

        [TestMethod]
        public void ActivatePhoneNumber()
        {
            try
            {
                service.ActivatePhoneNumber("2");
            }
            catch (Exception)
            {
                // Any exception is fail
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ActivatePhoneNumberWhichDoesNotExist()
        {
            try
            {
                service.ActivatePhoneNumber("20");
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, "Provided phone number 20 does not exist on the system. Activation Failed");
            }
        }
        #endregion
    }
}
