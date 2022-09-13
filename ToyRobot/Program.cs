using System;
using System.IO;

namespace ToyRobot
{
	class Program
	{
		static void Main(string[] args)
		{
			string command = "";
			Console.WriteLine("Do you have a text file that contains all the commands (Y/N)?");
			command = Console.ReadLine();

			if (command.Equals("Y", StringComparison.OrdinalIgnoreCase))
			{
				Console.WriteLine("Please enter the text file path (only .txt file)?"); 
				command = Console.ReadLine();

				try
				{
					FileInfo fi = new FileInfo(command);

					if (fi.Exists && fi.Extension == ".txt" && (fi.Length / (1024 * 1024)) < 10)
					{
						var commands = File.ReadAllLines(fi.FullName);
						if (commands.Length > 0)
						{
							var program = new Command();
							program.Process(commands);
						}
						else
						{
							Console.WriteLine("File is empty");
						}
					}
				}
				catch (Exception)
				{
					Console.WriteLine("Unable to process your file");
				}

				Console.ReadLine();
			}
			else
			{
				Console.WriteLine("Place the robot first on the 5*5 square table top and then commands in the below format.");
				Console.WriteLine("PLACE X, Y,DIRECTION");
				Console.WriteLine("MOVE");
				Console.WriteLine("LEFT");
				Console.WriteLine("RIGHT");
				Console.WriteLine("REPORT");
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("PLACE will put the robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.");
				Console.WriteLine("MOVE will move the robot one unit forward in the direction it is currently facing.");
				Console.WriteLine("LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.");
				Console.WriteLine("REPORT will announce the X,Y and orientation of the robot.");
				Console.WriteLine();
				Console.WriteLine("Press EXIT to exit the program");

				command = "";

				var program = new Command();

				//Exit the loop when user enter EXIT
				while (!command.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						command = Console.ReadLine();

						if (!command.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
						{
							program.Process(command);
						}
					}
					catch
					{
						Console.WriteLine("---------- An error occurred!!! ----------");
					}
				}
			}
		}
	}
}
