using BookingVault156.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.IdentityModel.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookingVault156
{
    internal class Methods
    {
        static int Week = 1;
        internal static void Running()
        {
            while (true)
            {
                // 1 - Rita upp dagar och rum och om dem är bokade eller inte
                // 2 - Menyval , Boka rum, lägg till nytt rum,lägg till ny dweller


                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':

                        break;
                }
            }
        }

        internal static void DrawTimes()
        {
            using (var db = new BookingContext())
            {
                Console.WriteLine($"Week: {Week}\n");
                Console.WriteLine("\tMån\tTis\tOns\tTor\tFri\tSat\tSun");
                foreach (var room in db.Rooms.ToList())
                {
                    Console.Write(room.Name);
                    for (int day = 1; day < 8; day++)
                    {
                        //if()
                        Console.Write($"\t {day}");                                              
                    }
                    Console.ReadLine();

                }





            }
        }

        internal static void BookRoom()
        {
            using (var db = new BookingContext())
            {
                ShowDwellers();
                Console.WriteLine("\n\nWhat dweller do you want to book? [ID Number]");
                int dwellerId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nWhat week do you want to book?");
                int week = Convert.ToInt32(Console.ReadLine());


                Console.WriteLine("\nWhat day do you want to book?");
                int day = Convert.ToInt32(Console.ReadLine());

                ShowRooms();
                Console.WriteLine("\nWhat room do you want to book?");
                int room = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nDo you want to do a mutation checkup? [Y]es or [N]o");
                var choice = Console.ReadLine();
                bool mutationCheckup;
                if(choice == "y" || choice == "Y")
                {
                    mutationCheckup = true;
                }
                else if(choice == "n" || choice == "N")
                {
                    mutationCheckup = false;
                }
                else
                {
                    Console.WriteLine("\nApperantly you are not able to read, we will have to check for mutations");
                    mutationCheckup = true;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("How many Ice Cold Nuka Cola would you like? MAX 5 BOTTLES");
                Console.ResetColor();
                var NukaColaAmount = Convert.ToInt32(Console.ReadLine());
                if (NukaColaAmount > 5)
                {
                    Console.WriteLine("You will only get 5, stop being greedy...");
                    NukaColaAmount = 5;
                }
                else if (NukaColaAmount < 5 && mutationCheckup == true)
                {
                    Console.WriteLine("You really cant read can you? Max amount is 5");
                    NukaColaAmount = 5;
                }

                var booking = new BookingHistory
                {
                    DwellerId = dwellerId,
                    BookedWeek = week,
                    BookedDay = day,
                    RoomId = room,
                    MutationCheck = mutationCheckup,
                    NukaColaAmount = NukaColaAmount,

                };
                var bookings = db.Bookings;
                bookings.Add(booking);
                db.SaveChanges();
            }


        }

        internal static void ShowDwellers()
        {
            using (var db = new BookingContext())
            {
                var dwellers = db.Dwellers;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n- Dwellers -\n");
                Console.ResetColor();
                foreach (var d in dwellers)
                {
                    Console.Write($"{d.Id}. {d.Name} | ");
                }

            }
        }

        internal static void AddDweller()
        {
            Console.WriteLine("\n Whats the name of the new Dweller?");
            string name = Console.ReadLine();

            using (var db = new BookingContext())
            {
                var newDweller = new Dweller
                {
                    Name = name
                };
                var dwellers = db.Dwellers;
                dwellers.Add(newDweller);
                db.SaveChanges();
            }
        }
        internal static void ShowRooms()
        {
            using (var db = new BookingContext())
            {
                var rooms = db.Rooms;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n- Rooms -\n");
                Console.ResetColor();
                foreach (var r in rooms)
                {
                    Console.Write($"{r.Id}. {r.Name} | ");
                }
            }
        }

        internal static void AddRoom()
        {
            Console.WriteLine("We have managed to build a new room, what should we call it?");
            string roomName = Console.ReadLine();

            using (var db = new BookingContext())
            {
                var newRoom = new Room
                {
                    Name = roomName
                };
                var rooms = db.Rooms;
                rooms.Add(newRoom);
                db.SaveChanges();
            }
        }
    }
}
