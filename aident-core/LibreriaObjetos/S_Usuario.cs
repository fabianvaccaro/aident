using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    [DataContract]
    [Serializable]
    public class S_Usuario
    {
        [DataMember]
        public Int32 Id { set; get; }
        [DataMember]
        public String Usuario { set; get; }
        [DataMember]
        public String Password { set; get; }

        public S_Usuario()
        {
            this.Id = 0;
            this.Usuario = String.Empty;
            this.Password = String.Empty;
        }
        public S_Usuario(Int32 id, String usuario, String password)
        {
            this.Id = id;
            this.Usuario = usuario;
            this.Password = password;
        }
    }
}
