using BookingVault156.Models;
using Microsoft.IdentityModel.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingVault156
{
    internal class Methods
    {
        
        internal static void Running()
        {
            while (true)
            {
                // 1 - Rita upp dagar och rum och om dem är bokade eller inte
                // 2 - Menyval , Boka rum, lägg till nytt rum,lägg till ny dweller
                
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {

                }
            }
        }

        internal static void DrawTimes()
        {

        }

        internal static void BookRoom()
        {
            Console.WriteLine("\nWhat dweller do you want to book? [ID Number]");
            int dwellerId = Convert.ToInt32(Console.ReadKey(true));
            Console.WriteLine("What week do you want to book?");
            int week = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What day do you want to book?");
            int day = Convert.ToInt32(Console.ReadLine());


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
                    Console.Write($"{d.Id} {d.Name} | ");
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
