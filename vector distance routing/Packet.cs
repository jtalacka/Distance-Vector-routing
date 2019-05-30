using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vector_distance_routing
{
    class Packet
    {
       public string sender;
       public string receiver;
       private string data;
        public Packet(string sender,string receiver,string data) {
            this.sender = sender;
            this.receiver = receiver;
            this.data = data;
        }
    }
}
