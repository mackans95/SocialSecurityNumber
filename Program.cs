using System;
using System.Globalization;


namespace SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args)
        {

            string socialSecurityNumber;
            if (args.Length > 0)
            {
                Console.Write($"You provided: {args[0]}");
                socialSecurityNumber = args[0];
            }
            else
            {
                Console.Write("Enter Social Security Number (YYMMDD-XXXX) please: ");
                socialSecurityNumber = Console.ReadLine();
            }
            
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));

            bool isFemale = genderNumber % 2 == 0;

            string gender = isFemale ? "Female" : "Male";

            while (socialSecurityNumber.Length != 11)
            {
                Console.Write("Invalid input, please try again: ");
                socialSecurityNumber = null;
                socialSecurityNumber = Console.ReadLine();

            }
            
            DateTime birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);

            int age = DateTime.Now.Year - birthDate.Year;
            
            if ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
                {
                    age--;
                }
            
            Console.Clear();
            Console.Write($"This is a {gender}, age {age}");
            Console.ReadKey();
                
            }    
        }
    }


