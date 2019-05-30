using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vector_distance_routing
{
    class Router
    {
        public int id;
        public bool active = true;
        public string[] routers;
        public string name;
        List<int> tempVector;
        List<string> tempNames;
        public List<int> vector;
        public List<string> vectornames;
        public List<Connection> connections;
        public Router(int id,string name,string[] routers) {
            this.id = id;
            this.name = name;
            this.routers = routers;
            vector = firstVector();
            vectornames = firstDistance();
            connections = new List<Connection>();

        }
        public void addConnection(Connection connection){
            connections.Add(connection);
        }
        public List<int> firstVector() {
            List<int> tempVector = new List<int>();
            for (int i = 0; i < routers.Length; i++)
            {
                tempVector.Add(-1);
            }

            tempVector[id] = 0;
            return tempVector;
        }
        public List<string> firstDistance() {
            List<string> tempNames = new List<string>();
            for (int i = 0; i < routers.Length; i++)
            {
                tempNames.Add("-");
            }
            tempNames[id] = name;
            return tempNames;
        }
        public void Display() {
            Console.WriteLine(routers[id]+" router is "+active);
            for (int i = 0; i < routers.Length; i++) {
                Console.WriteLine("{0} {1} {2}", routers[i], vector[i], vectornames[i]);
            }
        }
        public void routerState() {
            if (active) { active = false; } else { active=true; }
        }
        public void sendPacket(Packet packet) {
            Console.WriteLine("Packet in " + name);
            if (packet.sender == name && active == false)
            {
                Console.WriteLine("Sender is not available");
            }
            else
            {
                if (packet.receiver != name)
                {
                    string nextRoute = null;
                    for (int i = 0; i < routers.Length; i++)
                    {
                        if (routers[i] == packet.receiver)
                        {
                            nextRoute = vectornames[i];
                        }
                    }
                    if (nextRoute == null)
                    {
                        Console.WriteLine("Packet could not be sent");
                    }

                    foreach (Connection c in connections)
                    {
                        if (c.router1.name == nextRoute || c.router2.name == nextRoute)
                        {
                            c.sendPacket(this, packet);
                            break;
                        }
                    }



                }
                else
                {
                    Console.WriteLine(name + " received a packet from " + packet.sender);
                }
            }
        }


        public void calculateTable() {
            tempVector = firstVector();
            tempNames = firstDistance();
            if (active)
            {
                connections.ForEach(delegate (Connection con)
                {
                    for (int i = 0; i < routers.Length; i++)
                    {

                        if (con.getVector(this).ElementAt(i) != -1)
                        {
                            if (tempVector.ElementAt(i) != -1)
                            {

                                if (tempVector.ElementAt(i) > con.getVector(this).ElementAt(i))
                                {
                                    if (con.getVector(this).ElementAt(i) >= 16)
                                    {
                                        tempVector[i] = -1;
                                        tempNames[i] = "-";
                                    }
                                    else
                                    {
                                        tempVector[i] = con.getVector(this).ElementAt(i);
                                        tempNames[i] = routers[con.getRouterId(this)];
                                    }
                                }

                            }
                            else
                            {

                                if (con.getVector(this).ElementAt(i) >= 16)
                                {
                                    tempVector[i] = -1;
                                    tempNames[i] = "-";
                                }
                                else
                                {


                                    tempVector[i] = con.getVector(this).ElementAt(i);
                                    tempNames[i] = routers[con.getRouterId(this)];
                                }
                            }
                        }
                        else {
                            if (i == con.getRouterId(this)) {
                                tempVector[i] = -1;
                                tempNames[i] = "-";
                            }
                        }

                    }
                });
                tempVector[id] = 0;
                tempNames[id] = name;
            }

        }
        public void updateTable() {
            vector = tempVector;
            vectornames = tempNames;

        }
        public List<int> getVector() {
            return vector;
        }
    }
}
