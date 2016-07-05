using System.Runtime.Serialization;

namespace WebAPIClient
{
    [DataContract(Name="repo")]
    public class Repository
    {
        [DataMember(Name="name")]
        private string _name;
        public string Name
        {
            get { return _name;}
            set { this._name = value;}
        }
        
    }
}