﻿using BookingVault156.Models;
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
        internal static void StartPage()
        {
            while (true)
            {
                Console.Clear();
                DrawTimes();
                // 1 - Rita upp dagar och rum och om dem är bokade eller inte
                // 2 - Menyval , Boka rum, lägg till nytt rum,lägg till ny dweller
                Console.WriteLine("\n[C]hange week\t[B]ook a room\t[A]dmin menu");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'c':
                        ChangeWeek();
                        break;
                    case 'b':
                        BookRoom();
                        break;
                    case 'a':
                        AdminMenu();
                        break;
                }
            }
        }
        internal static void DrawTimes()
        {
            using (var db = new BookingContext())
            {
                Console.WriteLine($"Week: {Week}\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\tMon\tTue\tWen\tThu\tFri\tSat\tSun");
                Console.ResetColor();
                foreach (var room in db.Rooms.ToList())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(room.Id + ". " + room.Name);
                    Console.ResetColor();
                    for (int day = 1; day < 8; day++)
                    {

                        var bookedTimes = from b in db.Bookings
                                          join r in db.Rooms on b.RoomId equals r.Id
                                          where b.BookedWeek == Week
                                          select new { BookedDay = b.BookedDay, RoomId = r.Id };
                        bool alreadyBooked = false;
                        foreach (var bookedDay in bookedTimes.Where(x => x.BookedDay == day))
                        {
                            //if (bookedDay.RoomId != room.Id)
                            //{                           

                            //}
                            if (bookedDay.RoomId == room.Id)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\t B");
                                Console.ResetColor();
                                alreadyBooked = true;

                            }

                        }

                        if (alreadyBooked == false)
                        {
                            Console.Write($"\t A");
                        }

                    }
                    Console.WriteLine();

                }





            }
        }
        internal static void AdminMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Admin menu");
                Console.WriteLine("\n[1]. Add Dwellers\n[2]. Add Room\n[3]. Statistics\n[4]. Back to startpage");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        ShowDwellers();
                        AddDweller();
                        break;
                    case '2':
                        ShowRooms();
                        AddRoom();
                        break;
                    case '3':
                        Statistics();
                        break;
                    case '4':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("This is not an option, are you really an admin? Go back to work");
                        Console.ReadKey();
                        break;

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
                Week = week;


                Console.WriteLine("\nWhat day do you want to book? Mon - 1  |  Tue - 2  |  Wed - 3  |  Thu - 4  |  Fri - 5  |  Sat - 6  |  Sun - 7  ");
                int day = Convert.ToInt32(Console.ReadLine());

                ShowRooms();
                Console.WriteLine("\nWhat room do you want to book?");
                int room = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nDo you want to do a mutation checkup? [Y]es or [N]o");
                var choice = Console.ReadLine();
                bool mutationCheckup;
                if (choice == "y" || choice == "Y")
                {
                    mutationCheckup = true;
                }
                else if (choice == "n" || choice == "N")
                {
                    mutationCheckup = false;
                }
                else
                {
                    Console.WriteLine("\nApperantly you are not able to read, we will have to check for mutations");
                    mutationCheckup = true;
                    Console.ReadKey();
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("How many Ice Cold Nuka Cola would you like? MAX 5 BOTTLES");
                Console.ResetColor();
                var NukaColaAmount = Convert.ToInt32(Console.ReadLine());
                if (NukaColaAmount <= 5)
                {
                    Console.WriteLine();
                }
                else if (NukaColaAmount > 5)
                {
                    Console.WriteLine("You will only get 5, stop being greedy...");
                    NukaColaAmount = 5;
                    Console.ReadKey();
                }
                else if (NukaColaAmount < 5 && mutationCheckup == true)
                {
                    Console.WriteLine("You really cant read can you? Max amount is 5");
                    NukaColaAmount = 5;
                    Console.ReadKey();
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
                var bookedTimes = from b in db.Bookings
                                  join r in db.Rooms on b.RoomId equals r.Id
                                  where b.BookedWeek == Week
                                  select new { BookedDay = b.BookedDay, RoomId = r.Id, BookedWeek = b.BookedWeek};
                bool bookedStatus = false;
                foreach (var bookedTime in bookedTimes.Where(x => x.BookedDay == booking.BookedDay))
                {
                    if (bookedTime.RoomId == booking.RoomId)
                    {
                        Console.WriteLine("This time has already been booked");
                        Console.ReadKey();
                        bookedStatus = true;
                        break;
                    }
                }
                if (bookedStatus == false)
                {
                    var bookings = db.Bookings;
                    bookings.Add(booking);
                    db.SaveChanges();
                }

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
            Console.WriteLine("\nWe have managed to build a new room, what should we call it?");
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
        internal static void ChangeWeek()
        {
            Console.WriteLine("\n What week do you want to see?");
            int week = Convert.ToInt32(Console.ReadLine());
            if (week > 52 || week < 1)
            {
                Console.WriteLine("How many weeks do you think exist in a year? Comeback when youve gotten your exam!");
            }
            else
            {
                Week = week;
            }
        }
        internal static void Statistics()
        {
            bool running = true;
            while (running = true)
            {
                Console.Clear();
                Console.WriteLine("[1]. Most popular room\n[2]. Dweller who has booked the most rooms\n[3]. Go back to the start page");
                ConsoleKeyInfo key = Console.ReadKey(true);

                using (var db = new BookingContext())
                {

                    switch (key.KeyChar)
                    {
                        case '1':
                            var mostBookedRoom = (from r in db.Rooms
                                                  join b in db.Bookings on r.Id equals b.RoomId
                                                  select new { RoomId = r.Id, RoomName = r.Name, AmountBookedOfRoom = b.RoomId }).ToList().GroupBy(r => r.RoomName);
                            foreach (var room in mostBookedRoom.OrderByDescending(r => r.Count()).Take(1))
                            {
                                Console.WriteLine($"{room.Key} has been booked the most, it has been booked {room.Count()} times.");
                                Console.ReadKey();
                            }
                            break;

                        case '2':
                            var busiestDweller = (from d in db.Dwellers
                                                  join b in db.Bookings on d.Id equals b.DwellerId
                                                  select new { DwellerId = d.Id, DwellerName = d.Name, AmountOfBookedTimes = b.DwellerId }).ToList().GroupBy(d => d.DwellerName);
                            foreach (var dweller in busiestDweller.OrderByDescending(d => d.Count()).Take(1))
                            {
                                Console.WriteLine($"{dweller.Key} is up to something, he has booked {dweller.Count()} times, keep an eye out on him...");
                            }
                            Console.ReadKey();
                            break;
                        case '3':
                            running = false;
                            StartPage();
                            break;

                        default:
                            Console.WriteLine("This is not an option, are you really an admin? Go back to work");
                            Console.ReadKey();
                            break;
                    }

                }
            }
        }
        internal static void VaultBoy()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("                                          ,▄▄▄                              \r\n                                       ▄▓█▀▀▀▀▀█▄                           \r\n               ▄▄▓█`       ,▄▄▓▓▄▄▄▄▄@██▀!√√√√√└▀█▄                         \r");
            Thread.Sleep(500);
            Console.Write("\n            .▓█▀██       #█▀▀└:.!╙▀▀██▀:√√√√√√√√√!▀▀█▓▓▄▄                   \r\n           ╓█▀..▀█▓▄▄▄▄▓▀▀:√√√√√√√√√√√√√√√√√√√√√√√√√░░▀▀██▄                 \r\n           ██.√√√!▀▀▀▀▀:√√√√√√√√√√√√√√√√√√√√√√√√√√√√╠░░░░▀█▄                \r\n           █▌√√√√√√√√√√√√√▄▄▄▄▄.√√√√√√√╓▄▄▄.√√√√√√√√╠░░░░░╙█▄               \r\n           ██.√√√√√√√√√▄#█▀╙`╙▀█▓▄▄▄@▓██████▄.√√√√√╠░░░░░░░╙█▓▄             \r\n         ┌████:√√√√√(▄█▀╙       └▀▀▀▀└   └▀▀██,√√╓╢░░░░░░░░░░▀██▄           \r\n         ██:√╙▀▓▄▄▓▓▀▀                      └██▄░░░░░░░░░░░░░░░██▄          \r\n         █▌√√╓██▀  ▄▄@╕                       ▀▀█▓▀▀▀▀▀▀███▄░░░░██▄         \r\n         ██▄▓█▀  ╙▀▀▀▀▀                 ,▄               ▀███░░░░██▄        \r");
            Thread.Sleep(500);
            Console.Write("\n          ███`                         ▓███,     .        ███░░░░║██        \r\n         ▓█▀     ,▄                     └▀██▄            ▄██▀░░░░░██`       \r\n        ██▀     ███¼        ,              ▀▀        ╓@██▀▀░░░░░░░██        \r\n       ██▀     ▐███       ╓█▀        ▄▄,          .  ▄╙▀█░░░░░░░░╟██        \r\n      ▐█▌       ▀▀└     .▓█└        #███          .  ╙█▓,▀█░░░░░░██▌        \r\n      ██              ▄▓█▀          ███▌          . .▄,▀█▄╙█░░░░███         \r\n     ╟█▌            #██▀            ╙▀▀           .  ▀█▓,█▄╙█░░███          \r\n     ██─            ███                             ▓▄,▀█▄█,█░███`          \r\n     ██             ╙███                         .   ▀█▄╙█Ö█████            \r\n     ██    ,#         ╙╙                         . ╙█▄ ▀ ╙████▀             \r\n     ██  ╒███▄▄                  ▐█▄            .   ╙▀  .@███┘              \r\n     ██▌  ██▄ └╙▀▀#╦▄▄▄▄▄▄▄▄▄▄▄▄#████▄         .         ╙███               \r\n     ▐██   ▀ ▀▓▄,     `└╙└└ .      ███▌        .          ╟██               \r\n      ██▌      ╙▀█▓▄▄▄,   .,▄▄▄▓▓▀▀╙██        .          .███               \r");
            Thread.Sleep(500);
            Console.WriteLine("\n      └██▄        └▀▀▀███▀▀▀▀╙\"     ▀       ..          ▄███                \r\n       ╙██▄       Ñ▓▓▓▓µ                   ..    ▄▓▓▓▓███▀`                 \r\n        └██▄        `└└                  ..    ▄███▀└└                      \r\n          ▀██▄                          .   ▄▓██▀└                          \r\n            ▀█▓▄                     ..  ▄▓██▀╙                             \r\n              ╙▀█▄,                .╓▄▓██████                               \r\n                 ╙██▓▄         ...   '' ▄██▀                                \r\n                  ╙█████▓▓▄▄▄▄      .▄▄██▀'                                 \r\n                    ▀█████▄▄▄▄▄▄▄▄▓████▀                                    \r\n                       ╙▀▀▀██████▀▀▀╙           ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("- Welcome to VAULT TEC INDUSTRIES -");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            Console.ResetColor();
        }
    }
}
