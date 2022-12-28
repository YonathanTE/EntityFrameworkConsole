using EFConsoleUI.DataAccess;
using EFConsoleUI.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateYonathan();
            //ReadAll();
            //ReadById(1);
            //CreateC();
            //UpdateFirstName(1, "Yonathan");
            //ReadAll();

            //RemovePhoneNumber(1, "555-1212");

            //RemoveUser(3);
            //ReadAll();

            Console.WriteLine("Done Processing");
            Console.ReadLine();
        }

        public static void RemoveUser(int id)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts
                    .Include(e => e.EmailAddresses)
                    .Include(p => p.PhoneNumbers)
                    .Where(c => c.Id == id).First();
                db.Contacts.Remove(user);
                db.SaveChanges();
            }
        }

        public static void RemovePhoneNumber(int id, string phoneNumber)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts
                    .Include(p => p.PhoneNumbers)
                    .Where(c => c.Id == id).First();

                user.PhoneNumbers.RemoveAll(p => p.PhoneNumber == phoneNumber);

                db.SaveChanges();
            }
        }

        private static void UpdateFirstName(int id, string firstName)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts.Where(c => c.Id == id).First();

                user.FirstName = firstName;

                db.SaveChanges();
            }
        }

        private static void CreateYonathan()
        {
            var c = new Contact
            {
                FirstName = "Yonathan",
                LastName = "E"
            };
            c.EmailAddresses.Add(new Email { EmailAddress = "Yonathan@iamYonathanE.com" });
            c.EmailAddresses.Add(new Email { EmailAddress = "me@YonathanothyE.com" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "555-1212" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "555-1234" });

            using (var db = new ContactContext())
            {
                db.Contacts.Add(c);
                db.SaveChanges();
            }
        }

        private static void CreateC()
        {
            var c = new Contact
            {
                FirstName = "C",
                LastName = "E"
            };
            c.EmailAddresses.Add(new Email { EmailAddress = "C@aol.com" });
            c.EmailAddresses.Add(new Email { EmailAddress = "me@CE.com" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "555-1212" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "555-9876" });

            using (var db = new ContactContext())
            {
                db.Contacts.Add(c);
                db.SaveChanges();
            }
        }

        private static void ReadAll()
        {
            using (var db = new ContactContext())
            {
                var records = db.Contacts
                    //.Include(e => e.EmailAddresses)
                    //.Include(p => p.PhoneNumbers)
                    .ToList();

                foreach (var c in records)
                {
                    Console.WriteLine($"{ c.FirstName } { c.LastName }");
                }
            }
        }

        private static void ReadById(int id)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts.Where(c => c.Id == id).First();

                Console.WriteLine($"{ user.FirstName } { user.LastName }");
            }
        }
    }
}
