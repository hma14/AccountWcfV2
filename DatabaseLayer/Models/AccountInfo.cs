using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    [DataContract]
    public class AccountInfo
    {
        [DataMember]
        [Key]
        public string userid { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string firstname { get; set; }

        [DataMember]
        public string lastname { get; set; }
    }
}
