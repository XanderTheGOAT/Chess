using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void FileRead(string path)
        {
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                try
                {

                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine(path + " has been loaded");
                }
                stream.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File doesn't exist \n\n");
            }
            catch (SerializationException)
            {
                Console.WriteLine("Failed to Load \n\n");
            }
            catch (IOException)
            {
                Console.WriteLine("There was an error with your path \n\n");
            }
        }
    }
}
