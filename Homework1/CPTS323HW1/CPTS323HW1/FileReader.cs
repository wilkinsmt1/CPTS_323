﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CPTS323HW1
{
    //abstract file reader class
    public abstract class FileReader
    {
        // This is the file reader class, it has abstract print, printTarget, convert, isFriend, 
        // and printSort methods. Specific file readers will inherit from this class.
        //
        public FileReader()
        {
            Console.WriteLine("Creating filereader object.\n");
        }
        public abstract void print();

        public abstract void printTarget(string inputName);
        public abstract void convert(string fileName);
        public abstract void isFriend(string inputName);
        public abstract void printSort();
    }

    //derived INIReader class
    public class INIReader : FileReader
    {
        private List<Targets> targetList = new List<Targets>();   //list to hold target info
        private string filePassedIn;
        private string[] names = { "0", "0", "0" };
        private string[] xVal = { "0", "0", "0" };
        private string[] yVal = { "0", "0", "0" };
        private string[] zVal = { "0", "0", "0" };
        private string[] fVal = { "0", "0", "0" };
        private string[] pVal = { "0", "0", "0" };
        private string[] flashVal = { "0", "0", "0" };
        private string[] spawnVal = { "0", "0", "0" };
        private string[] swapVal = { "0", "0", "0" };
        public INIReader(string filePath) // Start of constructor
        {
            filePassedIn = filePath;
            Console.WriteLine("Creating INIReader object.");
            Console.WriteLine("Reading and extracting file data.");

            //put all lines of the file into a string[]
            string[] lines = System.IO.File.ReadAllLines(filePath);

            string inputString = string.Empty;
            char[] delimiterChar = { '=', '#' };

            //iterate throught the string[]
            foreach (string line in lines)
            {
                try
                {
                    if (line.Contains("[") && line != "[Target]") //check to make sure target file is valid
                    {
                        throw new System.ArgumentException();
                    }
                }
                catch
                {
                    Console.WriteLine("Error! Invalid format tags! Please exit the program and fix the file!");
                    Environment.Exit(1);
                }
                if (!line.Contains("[Target]")) //When the line does not contain target
                {                               //they will be read and put into the list
                    if (line.StartsWith("Name="))
                    { //when line stats with "Name=", delimit, and add the Name to a list
                        inputString = line;
                        names = inputString.Split(delimiterChar); //split the string by =.
                        if (names[1] == "") // if no name is specified thats an error
                        {
                            Console.WriteLine("Error! Target name not found! Please exit the program and fix the file!");
                            Environment.Exit(1);
                        }
                        else
                        {
                            //check to make sure there are no spaces
                            try
                            {
                                string nameCheck = names[1];
                                bool fHasSpace = nameCheck.Contains(" ");
                                if (fHasSpace == true) //if the name has a space throw an exception
                                {
                                    throw new System.ArgumentException();
                                }
                                else
                                {
                                    //Console.WriteLine("Target Name successfully extracted.");
                                }
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Error! Target names cannot contain spaces.");
                                return;
                            }

                        }

                    }
                    else if (line.StartsWith("X="))
                    { //when line stats with "X=", and delimit it
                        inputString = line;
                        xVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("X coordinate successfully extracted.");
                    }
                    else if (line.StartsWith("Y="))
                    { //when line stats with "Y=", and delimit it
                        inputString = line;
                        yVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Y coordinate successfully extracted.");
                    }
                    else if (line.StartsWith("Z="))
                    { //when line stats with "Z=", and delimit it
                        inputString = line;
                        zVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Z coordinate successfully extracted.");
                    }
                    else if (line.StartsWith("Friend="))
                    { //when line stats with "Friend=", and delimit it
                        inputString = line;
                        fVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Friend value successfully extracted.");
                    }
                    else if (line.StartsWith("Points="))
                    { //when line stats with "Points=", and delimit it
                        inputString = line;
                        pVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Point value successfully extracted.");
                    }
                    else if (line.StartsWith("FlashRate="))
                    { //when line stats with "Points=", and delimit it
                        inputString = line;
                        flashVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Flash Rate successfully extracted.");
                    }
                    else if (line.StartsWith("SpawnRate="))
                    { //when line stats with "Points=", and delimit it
                        inputString = line;
                        spawnVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Spawn Rate successfully extracted.");
                    }
                    else if (line.StartsWith("CanSwapSidesWhenHit="))
                    { //when line stats with "CanSwapSidesWhenHit=", and delimit it
                        inputString = line;
                        swapVal = inputString.Split(delimiterChar);
                        //Console.WriteLine("Swap value successfully extracted.");
                    }
                    else if (line.StartsWith("#"))
                    { //when line starts with "#"
                        //it is a comment, ignore
                    }
                    else //when line is a tag, put all the data for the previous target into a Targets object.
                    {
                        targetList.Add(new Targets() // add all the extracted data to the list
                        {
                            TargetName = names[1],
                            X = double.Parse(xVal[1]), //have to do this all at the same time
                            Y = double.Parse(yVal[1]), //so it is in the same Targets object
                            Z = double.Parse(zVal[1]),
                            IsFriend = Convert.ToBoolean(fVal[1]),
                            Points = int.Parse(pVal[1]),
                            FlashRate = int.Parse(flashVal[1]),
                            SpawnRate = int.Parse(spawnVal[1]),
                            CanSwapSidesWhenHit = Convert.ToBoolean(swapVal[1])
                        });
                    }
                }
            }
            //when done reading file we still have to add the last target
            targetList.Add(new Targets()
            {
                TargetName = names[1],
                X = double.Parse(xVal[1]),
                Y = double.Parse(yVal[1]),
                Z = double.Parse(zVal[1]),
                IsFriend = Convert.ToBoolean(fVal[1]),
                Points = int.Parse(pVal[1]),
                FlashRate = int.Parse(flashVal[1]),
                SpawnRate = int.Parse(spawnVal[1]),
                CanSwapSidesWhenHit = Convert.ToBoolean(swapVal[1])
            });
        } //end of constructor
        public override void print()
        {
            Console.WriteLine("Target Names from the file:");
            foreach (Targets targetname in targetList) //iterate through the list
            {
                string name = targetname.TargetName;
                Console.WriteLine(name);
            }


        }
        public override void printTarget(string inputName)
        {
            //target name came from main in upper case, so it has to be converted to upper case
            Targets result = targetList.Find(i => i.TargetName.ToUpper() == inputName);
            if (result == null) // if it did not find a match
            {
                Console.WriteLine("Target does not exist.");
            }
            else //if it did find a match print all this
            {
                string nVal = result.TargetName;
                double xVal = result.X;
                double yVal = result.Y;
                double zVal = result.Z;
                bool fVal = result.IsFriend;
                int pVal = result.Points;
                int flVal = result.FlashRate;
                int spVal = result.SpawnRate;
                bool swapVal = result.CanSwapSidesWhenHit;
                Console.WriteLine("Name=" + nVal);
                Console.WriteLine("X=" + xVal);
                Console.WriteLine("Y=" + yVal);
                Console.WriteLine("Z=" + zVal);
                Console.WriteLine("Friend=" + fVal);
                Console.WriteLine("Points=" + pVal);
                Console.WriteLine("FlashRate=" + flVal);
                Console.WriteLine("SpawnRate=" + spVal);
                Console.WriteLine("CanSwapSidesWhenHit=" + swapVal);
            }
        }
        public override void isFriend(string inputName)
        {
            //target name came from main in upper case, so it has to be converted to upper case
            Targets result = targetList.Find(i => i.TargetName.ToUpper() == inputName);
            if (result == null) // if it did not find a match
            {
                Console.WriteLine("Target does not exist.");
            }
            else
            {
                if (result.IsFriend == true)
                {
                    Console.WriteLine("Aye Captain!");
                }
                else
                {
                    Console.WriteLine("Nay, Scallywag!");
                }
            }


        }
        public override void convert(string fileName)
        {   //the fileName came from main in uppercase, you probably would rather it lower case
            fileName = fileName.ToLower();
            string argettay = "[argetTay]"; //target line string
            string ameNay = "ameNay="; //name string
            string targetName = "";
            string inputString = "";
            char[] delimiterChar = { '=' };
            string[] lines = System.IO.File.ReadAllLines(filePassedIn); //read the file into lines
            using (System.IO.StreamWriter file = File.AppendText(fileName)) //open and write file
            {
                foreach (string line in lines) //iterate though the file
                {
                    if (line.StartsWith("[")) //if line starts with [ it is the tag
                    {
                        file.WriteLine(argettay);
                    }
                    else if (line.StartsWith("Name"))
                    {
                        inputString = line;
                        names = inputString.Split(delimiterChar); // delimit string by =
                        targetName = names[1]; //put the name into a string
                        if (targetName[0] == 'A' || targetName[0] == 'E' || targetName[0] == 'I' ||
                            targetName[0] == 'O' || targetName[0] == 'U')
                        { //if first letter is a vowel, add way to the end
                            targetName = targetName + "way";
                        }
                        else //it is not a vowel
                        {
                            char firstChar = targetName[0];
                            targetName = targetName.Remove(0,1); //remove the first char
                            targetName = targetName + firstChar + "ay"; //then add first char and ay at the end
                        }
                        file.WriteLine(ameNay + targetName); //add the  ameNay=Pig Latinized Target name
                    }
                    else //don't care what other lines start with, so just write the line unchanged
                    {
                        file.WriteLine(line);
                    }

                }
           
              
            }

        }
        public override void printSort()
        {   //create an ordered list
            var orderedList = targetList.OrderBy(targetname => targetname.TargetName).ToList();
            foreach (Targets targetname in orderedList) //iterate through the list
            {
                string name = targetname.TargetName; //print names
                Console.WriteLine(name);
            }

        }

        public class Targets //This is the Targets class for the list of Targets objects
        {                    //The class has name, x, y, z, isfriend, points, flashrate, spawn rate
                             // and swap values as auto properties!
            public string TargetName { get;set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public bool IsFriend { get; set; }
            public int Points { get; set; }
            public int FlashRate { get; set; }
            public int SpawnRate { get; set; }
            public bool CanSwapSidesWhenHit { get; set; }
        }
    }

    //other file readers go here:
}
 

