using System;
using System.Globalization;
using System.Transactions;

namespace SocialSecurityNumber
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string socialSecurityNumber;
            string firstName;
            string lastName;

            if (args.Length > 0)
            {
                firstName = args[0];
                lastName = args[1];
                socialSecurityNumber = args[2];
            }
            else
            {
                Console.Write("Enter First Name: ");
                firstName = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                lastName = Console.ReadLine();

                Console.Write("Enter Social Security Number (YYYYMMDD-XXXX) please: ");
                socialSecurityNumber = Console.ReadLine();
            }
            
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));
            bool isFemale = genderNumber % 2 == 0;
            string gender = isFemale ? "Female" : "Male";

            while (socialSecurityNumber.Length != 13)
            {
                Console.Write("Invalid SSN, please try again (YYYYMMDD-XXXX): ");
                socialSecurityNumber = Console.ReadLine();
            }

            DateTime birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            int age = DateTime.Now.Year - birthDate.Year;
            if ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
            {
                age--;
            }

            string generation;
            if (age >= 75)
            {
                generation = "Silent Generation";
            }
            else if (age >= 56 && age <= 74)
            {
                generation = "Baby Boomer";
            }
            else if (age >= 40 && age <= 55)
            {
                generation = "Generation X";
            }
            else if (age >= 24 && age <= 39)
            {
                generation = "Millenial";
            }
            else
            {
                generation = "Generation Z";
            }
            Console.Clear();
            Console.WriteLine($"Name:                   {firstName} {lastName}");
            Console.WriteLine($"Social Security Number: {socialSecurityNumber}");
            Console.WriteLine($"Gender:                 {gender}");
            Console.WriteLine($"Age:                    {age}");
            Console.WriteLine($"Generation:             {generation}");
            Console.ReadKey();
        }    
        }
    }


