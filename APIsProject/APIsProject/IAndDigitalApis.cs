using AndDigitalApis.Entities;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AndDigitalApis
{
    [ServiceContract]
    public interface IAndDigitalApis
    {
      /// <summary>
      /// GetAllPhoneNumbers : Use GET API to get all phone numbers in the system.
      /// </summary>
      /// <param name="startIndex">Specifes the index from where to start the results.</param>
      /// <param name="resultsPerPage">Specifies number of results to be returned from the above index.</param>
      /// <returns></returns>
        [WebInvoke(Method = "GET",
            UriTemplate = "api/getallphonenumbers?startIndex={startIndex}&resultsPerPage={resultsPerPage}",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [FaultContract(typeof(ErrorDetails))]
        PhoneNumbersCollection GetAllPhoneNumbers(string startIndex, string resultsPerPage);

        /// <summary>
        /// GetAllPhoneNumbersForCustomer : Use GET API to get all phones numbers associated with a customer
        /// </summary>
        /// <param name="customerId">Identfier of the customer</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET",
           UriTemplate = "api/getallphonenumbers/{customerId}",
           BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [FaultContract(typeof(ErrorDetails))]
        PhoneNumbersCollection GetAllPhoneNumbersForCustomer(string customerId);

        /// <summary>
        /// ActivatePhoneNumber : Use PUT API to activate a phone number
        /// </summary>
        /// <param name="phoneid"></param>
        [WebInvoke(Method = "PUT",
           UriTemplate = "api/activatephonenumber/{phoneId}",
           BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [FaultContract(typeof(ErrorDetails))]
        void ActivatePhoneNumber(string phoneId);
    }
}
