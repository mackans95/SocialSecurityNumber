using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace SocialSecurityNumber
{

    class Program
    {
        static void Main(string[] args)
        {
            string socialSecurityNumber;
            string firstName;
            string lastName;
            string generation;

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

            var gender = GetGender(socialSecurityNumber);

            //This regex below will now make sure only SSN:s that match it (8 digits with or without 
            //a hyphen, then the last 4) will work, all else is invalid
            var ssnFormat = new Regex(@"^\d{8}[-\s]{0,1}\d{4}$");
            var isNotCorrectFormat = !ssnFormat.IsMatch(socialSecurityNumber);
            while (isNotCorrectFormat)
            {
                Console.Clear();
                Console.Write("Invalid SSN format, please try again (YYYYMMDD-XXXX): ");
                socialSecurityNumber = Console.ReadLine();

                if (ssnFormat.IsMatch(socialSecurityNumber))
                {
                    break;
                }
            }

            var birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            var age = CalculateAge(socialSecurityNumber);

            const int silentGenCutoff = 1945;
            const int babyBoomerCutoff = 1964;
            const int generationXCutoff = 1980;
            const int millenialCutoff = 1996;
            const int generationZCutoff = 2012;

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

        private static int CalculateAge(string socialSecurityNumber)
        {
            DateTime birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            var age = DateTime.Now.Year - birthDate.Year;

            if ((birthDate.Month > DateTime.Now.Month) ||
                (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
            {
                age--;
            }

            return age;
        }

        private static string GetGender(string socialSecurityNumber)
        {
            var genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));

            var isFemale = genderNumber % 2 == 0;
            
            var gender = isFemale ? "Female" : "Male";
            
            return gender;
        }
    }
}
    


