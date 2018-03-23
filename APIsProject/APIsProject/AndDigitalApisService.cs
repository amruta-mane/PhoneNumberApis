using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AndDigitalApis.Entities;

namespace AndDigitalApis
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ErrorHandler()]
    public class AndDigitalApisService : IAndDigitalApis
    {
        /// <summary>
        /// Static list of phone numbers. Will be maintained throughout a session
        /// </summary>
        public static List<PhoneNumber> phoneNumbers = new List<PhoneNumber>() { new PhoneNumber { Identity = 1, Number = "1111", Description = "Number 1", CustomerId = 1,},
                                      new PhoneNumber { Identity = 2, Number = "2222", Description = "Number 2", CustomerId = 1,},
                                      new PhoneNumber { Identity = 3, Number = "3333", Description = "Number 3", CustomerId = 1 },
                                      new PhoneNumber { Identity = 4, Number = "4444", Description = "Number 4", CustomerId = 2 },
                                      new PhoneNumber { Identity = 5, Number = "5555", Description = "Number 5", CustomerId = 2 },
                                      new PhoneNumber { Identity = 6, Number = "6666", Description = "Number 6", CustomerId = 2 },
                                      new PhoneNumber { Identity = 7, Number = "7777", Description = "Number 7", CustomerId = 3 },
                                      new PhoneNumber { Identity = 8, Number = "8888", Description = "Number 8", CustomerId = 3 },
                                      new PhoneNumber { Identity = 9, Number = "9999", Description = "Number 9", CustomerId = 3 },
                                      new PhoneNumber { Identity = 10, Number = "10101010", Description = "Number 10", CustomerId = 4 },
                                      new PhoneNumber { Identity = 11, Number = "11111111", Description = "Number 11", CustomerId = 4 },
                                      new PhoneNumber { Identity = 12, Number = "12121212", Description = "Number 12", CustomerId = 4 },
                                      new PhoneNumber { Identity = 13, Number = "13131313", Description = "Number 13", CustomerId = 5 },
                                      new PhoneNumber { Identity = 14, Number = "14141414", Description = "Number 14", CustomerId = 5 },
                                      new PhoneNumber { Identity = 15, Number = "15151515", Description = "Number 15", CustomerId = 5 }
         };

        /// <summary>
        /// Static list of customers
        /// </summary>
        public static List<Customer> customers = new List<Customer> { new Customer { Identity = 1, Name = "Alpha"},
                                                                      new Customer { Identity = 2, Name = "Beta"},
                                                                      new Customer { Identity = 3, Name = "Gamma"},
                                                                      new Customer { Identity = 4, Name = "Delta"},
                                                                      new Customer { Identity = 5, Name = "Epsilon"}

        };

        /// <summary>
        /// Service method to activate existing phone number
        /// </summary>
        /// <param name="phoneId"></param>
        public void ActivatePhoneNumber(string phoneId)
        {
            if (string.IsNullOrWhiteSpace(phoneId))
            {
                throw new ArgumentException("PhoneId not passed in request");
            }

            int phoneNumberId = 0;
            if (Int32.TryParse(phoneId, out phoneNumberId))
            {
                PhoneNumber numbers = phoneNumbers.FirstOrDefault(pn => pn.Identity == phoneNumberId);
                if(numbers != null)
                {
                    numbers.ActivatePhoneNumber(true);
                }
                else
                {
                    throw new Exception($"Provided phone number {phoneId} does not exist on the system. Activation Failed");
                }
            }
        }

        /// <summary>
        /// Service method to get all phone numbers in a system
        /// </summary>
        /// <param name="startIndex">Used to get data from a specifc indes. Specially used for paging</param>
        /// <param name="resultsPerPage">Used to get specific number of items. Used for paging</param>
        /// <returns></returns>
        public PhoneNumbersCollection GetAllPhoneNumbers(string startIndex, string resultsPerPage)
        {
            List<PhoneNumber> pns = new List<PhoneNumber>();

            // Only allow paging of both the startIndex and results per page are provided
            if(!string.IsNullOrWhiteSpace(startIndex) && !string.IsNullOrWhiteSpace(resultsPerPage))
            {
                int intStartIndex = StringToInt(startIndex, "StartIndex");
                int intResultsPerPage = StringToInt(resultsPerPage, "ResultsPerPage");
                phoneNumbers.Sort();

                // check how many items are remaining in the list 
                if(phoneNumbers.Count - intStartIndex < intResultsPerPage)
                {
                    // adjust range if these are less items remaining in the list 
                    intResultsPerPage = phoneNumbers.Count - intStartIndex;
                }

                if(phoneNumbers.ElementAtOrDefault(intStartIndex) == null)
                {
                    // Return empty collection of start index does not exist
                    return new PhoneNumbersCollection();
                }

                pns = phoneNumbers.GetRange(intStartIndex, intResultsPerPage);
            }
            else
            {
                pns = phoneNumbers;
            }

            return new PhoneNumbersCollection(pns);
        }

        /// <summary>
        /// Service method to get all phone numbers for specified customer
        /// </summary>
        /// <param name="customerId">Id of customer who's numbers are to be returned</param>
        /// <returns></returns>
        public PhoneNumbersCollection GetAllPhoneNumbersForCustomer(string customerId)
        {
            int intCustomerId = StringToInt(customerId, "CustomerId");

            if (customers.Any(c => c.Identity == intCustomerId))
            {
                return new PhoneNumbersCollection(phoneNumbers.Where(pn => pn.CustomerId == intCustomerId));
            }
            else
            {
                throw new Exception($"Customer with Identity {customerId} does not exist on this system.");
            }
        }

        private int StringToInt(string stringValue, string fieldName)
        {
            int intValue;
            try
            {
                intValue = Convert.ToInt32(stringValue);
            }
            catch
            {
                throw new Exception($"{fieldName} contains invalid value. Only integers are allowed.");
            }

            return intValue;
        }
    }    
}
