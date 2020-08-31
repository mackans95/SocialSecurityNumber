using System;
using System.Globalization;


namespace SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Social Security Number (YYMMDD-XXXX) please: ");

            string socialSecurityNumber = Console.ReadLine();
            
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));

            bool isFemale = genderNumber % 2 == 0;

            string gender = isFemale ? "Female" : "Male;";
            
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


