using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceTest
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Customer
    {
        private string firstname;
        private string surnamename;
        private int age;

        [DataMember]
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        [DataMember]
        public string Surname
        {
            get { return surnamename; }
            set { surnamename = value; }
        }

        [DataMember]
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}