using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ToyRobot.Controller;

namespace ToyRobot
{
	public class Command
	{
		/// <summary>
		/// Regex to validate placement.
		/// </summary>
		private const string placement = @"(PLACE)\s(\d+),(\d+),(NORTH|SOUTH|EAST|WEST)$";
		/// <summary>
		/// Regex to validate user moments.
		/// </summary>
		private const string action = @"\b(MOVE|LEFT|RIGHT|REPORT)\b$";

		private Regex placementRegex = new Regex(placement, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private Regex actionRegex = new Regex(action, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		private static Controller _controller;

		public bool Placement { get; private set; }
		public Actions LastAction { get; private set; }

		public Command()
		{
			Placement = false;
			LastAction = Actions.NOACTION;
		}

		public void Process(string[] commands)
		{
			foreach (string command in commands)
			{
				Process(command);
			}
		}

		public void Process(string command)
		{
			try
			{
				//Validation for the placement.
				var placementMatch = placementRegex.Match(command);
				if (placementMatch.Success)
				{
					if (int.TryParse(placementMatch.Groups[2].Value, out int x)
						&& int.TryParse(placementMatch.Groups[3].Value, out int y)
						&& Enum.TryParse(placementMatch.Groups[4].Value, true, out Direction direction))
					{
						try
						{
							//Build the controller, It will return an error if the position is invalid
							_controller = new ControllerBuilder()
										.WithRobot(new Point(x, y), direction)
										.WithTable(5, 5)
										.Build();
							Placement = true;
						}
						catch
						{
							Console.WriteLine("Unable to place the Robot, Please check if the position is valid");
						}
					}
					return;
				}

				if (Placement)
				{
					//Validation for the actions.
					var cmd = Regex.Replace(command, @"\s+", ""); //Remove any whitespaces
					var actionMatch = actionRegex.Match(cmd);
					if (actionMatch.Success)
					{
						if (Enum.TryParse(actionMatch.Value, true, out Actions action))
						{
							_controller.Action(action);
							LastAction = action;
						}
						return;
					}
				}
			}
			catch
			{
				Console.WriteLine("Unexpected Error");
			}
		}
	}
}
