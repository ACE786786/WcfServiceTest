using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WcfServiceTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
       
        public string GetData(string FirstName, String Surname, int Age)
        {
            
            Customer c = new Customer();
            c.FirstName = FirstName;
            c.Surname = Surname;
            c.Age = Age;
                   
            return string.Format("You entered: {0} {1} {2} ", FirstName, Surname, Age);
            
        }

        public string SendDataToQueue(string FirstName, String Surname, int Age)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            Customer c = new Customer();
            c.FirstName = FirstName;
            c.Surname = Surname;
            c.Age = Age;

            
            string customer = new JavaScriptSerializer().Serialize(c);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();


            //Insert message into Queue
            CloudQueueMessage message = new CloudQueueMessage(customer);
            queue.AddMessage(message);

            return string.Format("Added The following to the queue: {0} {1} {2} ", FirstName, Surname, Age);

        }


        public string PeakDataInQueue()
        {
                        // Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the queue client
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference("myqueue");
            CloudQueueMessage peekedMessage = queue.PeekMessage();

            return string.Format("Peaked at queue message: {0}", peekedMessage.AsString);
        }

        public Customer GetDataUsingDataContract(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
           
            return customer;
        }

        public string SendDataToBlob(string FirstName, string Surname, int Age)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            Customer c = new Customer();
            c.FirstName = FirstName;
            c.Surname = Surname;
            c.Age = Age;


            string customer = new JavaScriptSerializer().Serialize(c);

            // Create the blob client.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // create container a reference to a container
            CloudBlobContainer cblobcontainer = cloudBlobClient.GetContainerReference("myblobcontainer");


            // Create the blob if it doesn't already exist
            cblobcontainer.CreateIfNotExists();
            
            Guid id = Guid.NewGuid();

            var blob = cblobcontainer.GetBlockBlobReference("mycustomerBlob" + id);

            blob.UploadText(customer);

          
            return string.Format("Added The following to the blob: {0} {1} {2} ", FirstName, Surname, Age);

        }



    }
}
