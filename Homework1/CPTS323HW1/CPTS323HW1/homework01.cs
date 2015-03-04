/* Author: Michael Wilkins
 * Class: CPTS323
 * Assignment: Homework 01
 * Purpose: A C# program that reads input files and provides commands to print, convert,
 *           and show if a target is a friend.
 * File: This is the Main method.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CPTS323HW1
{
    class homework01
    {
        static void Main(string[] args)
        {

            try //check to see if file was passed in and exists
            {
                string fileTest = args[0];
                var fileExists = System.IO.File.Exists(fileTest);
                if(args.Length == 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else if (!fileExists)
                {
                    throw new FileNotFoundException();
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error file was not passed in!");
                return;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Error the file does not exist!");
                return;
            }

            string filePath = args[0];
            FileReader reader = new INIReader(filePath); //create an ini reader
            string command = "";
            char[] delimiterChar = { ' ' };
            while (command.ToUpper() != "EXIT") //while the command is not exit, enter commands
            {
                Console.WriteLine("Please enter a command: ");
                command = Console.ReadLine();
                command = command.ToUpper();
                if (command.StartsWith("PRINT")) //if command starts with print, its either print,
                {                                //print sort, or print target  
                    string[] commandName = command.Split(delimiterChar); // split the command with space
                    if (commandName.Length <= 1) // if the command is just print
                    {
                        reader.print();
                    }
                    else if (commandName[1].ToUpper() == "SORT") //if command is print sort
                    {
                        reader.printSort();
                    }
                    else
                    {
                        //if it wasn't print sort
                        reader.printTarget(commandName[1]);
                    }
                }
                else if (command.StartsWith("CONVERT"))
                {
                    string[] commandName = command.Split(delimiterChar); // split the command with space
                    string convertName = commandName[1];
                    reader.convert(convertName);
                }
                else if (command.StartsWith("ISFRIEND"))
                {
                    string[] commandName = command.Split(delimiterChar); // split the command with space
                    string friendName = commandName[1];
                    reader.isFriend(friendName);
                }
            }
            Console.WriteLine("Exiting program!");
            return;
        }

    }
}
