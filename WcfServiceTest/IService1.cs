using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WcfServiceTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(string FirstName, String Surname, int Age);

        [OperationContract]
        string SendDataToQueue(string FirstName, String Surname, int Age);

        [OperationContract]
        string SendDataToBlob(string FirstName, String Surname, int Age);


        [OperationContract]
        string PeakDataInQueue();

        [OperationContract]
        Customer GetDataUsingDataContract(Customer customer);

        // TODO: Add your service operations here
    }


  
}
