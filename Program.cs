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

            const int silentGenCutoff = 1945;
            const int babyBoomerCutoff = 1964;
            const int generationXCutoff = 1980;
            const int millenialCutoff = 1996;
            const int generationZCutoff = 2012;

            string generation;
            if (birthDate.Year >= 1977 && birthDate.Year <= 1983)
            {
                generation = "Xennial";
            }
            else
            {
                generation = (birthDate.Year <= silentGenCutoff ? "isSilentGen" :
                              birthDate.Year <= babyBoomerCutoff ? "isBabyBoomer" :
                              birthDate.Year <= generationXCutoff ? "isGenX" :
                              birthDate.Year <= millenialCutoff ? "isMillenial" :
                              birthDate.Year <= generationZCutoff ? "isGenZ" : default) switch
                {
                    "isSilentGen" => "Silent Generation",
                    "isBabyBoomer" => "Baby Boomer",
                    "isGenX" => "Generation X",
                    "isMillenial" => "Millenial",
                    "isGenZ" => "Generation Z",
                    _ => "Generation Alpha",
                };
            }
            // -25 in the rows below indicates the amount of spaces to print before the other variables
            Console.Clear();
            Console.WriteLine($"{"Name: ",-25}{firstName} {lastName}");
            Console.WriteLine($"{"Social Security Number: ",-25}{socialSecurityNumber}");
            Console.WriteLine($"{"Gender: ",-25}{gender}");
            Console.WriteLine($"{"Age: ",-25}{age}");
            Console.WriteLine($"{"Generation: ",-25}{generation}");
            Console.ReadKey();
        }
    }
}
    


