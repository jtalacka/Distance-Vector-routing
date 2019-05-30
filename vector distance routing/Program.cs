using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vector_distance_routing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Router> routers = new List<Router>();
            Console.WriteLine("Create Routers");
            string s = Console.ReadLine();





            string[] router = s.Split(' ');

            for (int i = 0; i < router.Length; i++)
            {
                Console.WriteLine(router[i]);
                routers.Add(new Router(i, router[i], router));
            }
            Console.WriteLine("How many connections do you wish to create");
            s = Console.ReadLine();
            for (int i = 0; i < Int32.Parse(s); i++)
            {
                generateConnections(routers, router.Length);
            }


            Calculate(router.Length, routers);
            while (true)
            {
                Console.WriteLine("Select Command");
                Console.WriteLine("1.Enable/Disable router");
                Console.WriteLine("2.Change connection weight");
                Console.WriteLine("3.Send packet");
                s = Console.ReadLine();
                if (Int32.Parse(s) == 1)
                {
                    Console.WriteLine("Name a router to enable/disable");
                    string rout = Console.ReadLine();
                    foreach (Router r in routers)
                    {
                        if (r.name == rout)
                        {
                            r.routerState();
                            Calculate(router.Length, routers);
                            break;
                        }
                    }

                }
                else if (Int32.Parse(s) == 2)
                {
                    Console.WriteLine("Name a connection between routers and their weight like this A 3 B");
                    string rout = Console.ReadLine();
                    string[] routerArray = rout.Split(' ');
                    foreach (Router r in routers)
                    {
                        if (routerArray[0] == r.name)
                        {
                            foreach(Connection c in r.connections)
                            {
                                if ((c.router1.name == routerArray[0] || c.router1.name == routerArray[2])&& (c.router2.name == routerArray[0] || c.router2.name == routerArray[2])) {
                                    c.changeWeight(Int32.Parse(routerArray[1]));
                                    break;
                                }
                            }
                            
                            Calculate(router.Length, routers);
                            break;
                        }
                    }

                }else if(Int32.Parse(s) == 3)
                {
                    bool possible = false;
                    Router sender = null;
                    string receiver = null;
                    while (possible == false)
                    {
                        Console.WriteLine("Select source and destination routers");
                        string r = Console.ReadLine();
                        string[] r1 = r.Split(' ');
                        sender = null;
                        receiver = null;

                        for (int i = 0; i < routers.Count; i++) {
                            if (r1[0] == routers.ElementAt(i).name)
                            {
                                sender = routers[i];
                            }
                            if (r1[1] == routers.ElementAt(i).name)
                            {
                                receiver = routers[i].name;
                            }
                        }
                        if (sender != null && receiver != null)
                        {
                            possible = true;
                        }
                        else {
                            if (sender == null) { Console.WriteLine("Couldn't find a sender"); } else {
                                Console.WriteLine("Couldn't find a receiver");
                            }
                        }
                    }
                    Console.WriteLine("Please write a content of the packet");
                    string data = Console.ReadLine();
                    Packet packet = new Packet(sender.name, receiver, data);

                    sender.sendPacket(packet);
                }


            }
        }
        public static void generateConnections(List<Router> routers,int numberOfRouters) {
            Console.WriteLine("Define Connections");
            string s = Console.ReadLine();

            string[] con = s.Split(' ');
            Router tempRouter1 = null;
            Router tempRouter2 = null;
            for (int i = 0; i < numberOfRouters; i++)
            {
                if (con[0] == routers.ElementAt(i).name)
                {
                    tempRouter1 = routers.ElementAt(i);
                }
                if (con[2] == routers.ElementAt(i).name)
                {
                    tempRouter2 = routers.ElementAt(i);
                }
            }
            if (tempRouter1 != null || tempRouter2 != null)
            {
                new Connection(tempRouter1, tempRouter2, Int32.Parse(con[1]));
            }
        }
        public static void Calculate(int numberOfRouters, List<Router> routers)
        {
            for (int i = 0; i <= numberOfRouters*2; i++) {
                foreach (Router r in routers) {
                    r.calculateTable();
                }
                foreach (Router r in routers)
                {
                    r.updateTable();
                }
            }
            foreach (Router r in routers)
            {
                r.Display();
            }
        }

    }
}
