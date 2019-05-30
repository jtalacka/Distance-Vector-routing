using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vector_distance_routing
{
    class Connection
    {
        public Router router1;
        public Router router2;
        int weight;
        public Connection(Router router1,Router router2,int weight) {
            this.router1 = router1;
            this.router2 = router2;
            this.weight = weight;
            router1.addConnection(this);
            router2.addConnection(this);
        }


        public void changeWeight(int weight) {
            this.weight = weight;
        }
        public void sendPacket(Router router, Packet packet) {
            if (router == router1)
            {
                router2.sendPacket(packet);
            }
            else
            {
                router1.sendPacket(packet);
            }
        }

        public List<int> getVector(Router router) {
            Router r;
            if (router == router1)
            {
                r = router2;
            }
            else {
                r = router1;
            }


            return updateVector(r.getVector(),r.active);
        }
        public List<int >updateVector(List<int>vector,bool active) {

            List<int> newVector = new List<int>();
            for(int i = 0; i < vector.Count;i++)
            {
                if (active == true)
                {
                    if (vector.ElementAt(i) == -1)
                    {
                        newVector.Add(-1);
                    }
                    else
                    {
                        newVector.Add(vector.ElementAt(i) + weight);
                    }
                }
                else {
                    newVector.Add(-1);
                }

            }
            return newVector;

        }
        public int getRouterId(Router router) {
            Router r;
            if (router == router1)
            {
                r = router2;
            }
            else
            {
                r = router1;
            }
            return r.id;
        }
    }
}
