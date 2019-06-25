//******************************************************
// File: Program.cs
//
// Purpose: This program will present a menu to the user
//          and then perform an action depending on what
//          the user chooses to do. It creates one instance
//          of each class in main. When the program runs
//          it displays the menu to the user and gives
//          them a chance to input a choice. Depending
//          on what choice the user makes, an action is
//          performed. The menu actions manipulate and
//          use the appropriate class instance that are
//          declared at the top of main. 
//
// Written By: Jason Diaz 
//
// Compiler: Visual Studio 2017
//
//******************************************************

using System;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using CountryDataLibrary;

namespace jasonDiaz_Assign2
{
    class Program
    {

        static void Main(string[] args)
        {
            Country country = new Country();
            Currency currency = new Currency();
            Language language = new Language();
            string userInput; // Menu user input
            int intVal; // Converted user input

            //serialize<Language>(l, true);
            //l = deserialize<Language>(l, true);
            //Console.WriteLine(l.ToString());

            do
            {
                // Main Menu
                Console.WriteLine("Country Menu");
                Console.WriteLine("------------");
                Console.WriteLine("1  - Read Currency from JSON file");
                Console.WriteLine("2  - Read Currency from XML file");
                Console.WriteLine("3  - Write Currency to JSON file");
                Console.WriteLine("4  - Write Currency to XML file");
                Console.WriteLine("5  - Display Currency data on screen");
                Console.WriteLine("6  - Read Language from JSON file");
                Console.WriteLine("7  - Read Language from XML file");
                Console.WriteLine("8  - Write Language to JSON file");
                Console.WriteLine("9  - Write Language to XML file");
                Console.WriteLine("10 - Display Language data on screen");
                Console.WriteLine("11 - Read Country from JSON file");
                Console.WriteLine("12 - Read Country from XML file");
                Console.WriteLine("13 - Write Country to JSON file");
                Console.WriteLine("14 - Write Country to XML file");
                Console.WriteLine("15 - Display Country data on screen");
                Console.WriteLine("16 - Exit");
                Console.Write("Enter Choice: ");
                userInput = Console.ReadLine();
                Console.WriteLine();

                // Convert string to integer type
                intVal = Convert.ToInt32(userInput);

                #region Serialization/Deserialization switch statments
                switch (intVal)
                {
                    case 1: currency = deserialize<Currency>(currency, true);
                        break;
                    case 2: currency = deserialize<Currency>(currency, false);
                        break;
                    case 3: serialize<Currency>(currency, true);
                        break;
                    case 4: serialize<Currency>(currency, false);
                        break;
                    case 5: Console.WriteLine(currency.ToString());
                            Console.WriteLine();
                        break;
                    case 6:
                        language = deserialize<Language>(language, true);
                        break;
                    case 7:
                        language = deserialize<Language>(language, false);
                        break;
                    case 8:
                        serialize<Language>(language, true);
                        break;
                    case 9:
                        serialize<Language>(language, false);
                        break;
                    case 10:
                        Console.WriteLine(language.ToString());
                        Console.WriteLine();
                        break;
                    case 11:
                        country = deserialize<Country>(country, true);
                        break;
                    case 12:
                        country = deserialize<Country>(country, false);
                        break;
                    case 13:
                        serialize<Country>(country, true);
                        break;
                    case 14:
                        serialize<Country>(country, false);
                        break;
                    case 15:
                        Console.WriteLine(country.ToString());
                        Console.WriteLine();
                        break;
                    case 16:
                        break;

                    default:
                        Console.WriteLine("\nPlease enter valid input!\n");
                        break;
                }
                #endregion
            } while(intVal != 16);
        }

        #region deserialize Method
        static T deserialize<T>(T obj, bool isJson)
        {
            Console.Write("Enter filename: ");
            string filename = Console.ReadLine();
            Console.WriteLine();

            FileStream reader = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(reader, Encoding.UTF8);
            string streamString = streamReader.ReadToEnd();

            byte[] byteArray = Encoding.UTF8.GetBytes(streamString);
            MemoryStream stream = new MemoryStream(byteArray);

            if (isJson)
            {
                DataContractJsonSerializer inputSerializer;
                inputSerializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)inputSerializer.ReadObject(stream);
            }
            else
            {
                DataContractSerializer inputSerializer;
                inputSerializer = new DataContractSerializer(typeof(T));
                obj = (T)inputSerializer.ReadObject(stream);
            }
            stream.Close();
            return obj;
        }
        #endregion

        #region serialize Method
        static void serialize<T>(T obj, bool isJson)
        {
            Console.Write("Enter filename: ");
            string filename = Console.ReadLine();
            Console.WriteLine();

            if (isJson)
            {
                DataContractJsonSerializer ser;
                ser = new DataContractJsonSerializer(typeof(T));

                MemoryStream memoryStream = new MemoryStream();
                ser.WriteObject(memoryStream, obj);

                byte[] data = memoryStream.ToArray();
                string utf8String = Encoding.UTF8.GetString(data, 0, data.Length);

                StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
                streamWriter.Write(utf8String);
                streamWriter.Close();
            }
            else
            {
                DataContractSerializer ser;
                ser = new DataContractSerializer(typeof(T));

                MemoryStream memoryStream = new MemoryStream();
                ser.WriteObject(memoryStream, obj);

                byte[] data = memoryStream.ToArray();
                string utf8String = Encoding.UTF8.GetString(data, 0, data.Length);

                StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
                streamWriter.Write(utf8String);
                streamWriter.Close();
            }

        }
        #endregion
    }
}
