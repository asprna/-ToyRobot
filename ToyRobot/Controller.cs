using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToyRobot.Robot;
using static ToyRobot.Table;

namespace ToyRobot
{
	public enum Actions
	{
		NOACTION = -1,
		MOVE = 0,
		LEFT = 1,
		RIGHT = 2,
		REPORT = 3
	}

	public class Controller
	{
		public Robot Robot { get; set; }
		public Table Table { get; set; }

		protected Controller() { }

		/// <summary>
		/// Trigger the user action.
		/// </summary>
		/// <param name="action"></param>
		public void Action(Actions action)
		{
			//Prevent the robot falling off the tale
			if (action.Equals(Actions.MOVE) && !Table.IsValidLocation(Robot.NextPosition()))
			{
				return;
			}

			switch(action)
			{
				case Actions.MOVE:
					Robot.Move();
					break;
				case Actions.RIGHT:
					Robot.TurnRight();
					break;
				case Actions.LEFT:
					Robot.TurnLeft();
					break;
				case Actions.REPORT:
					Robot.Report();
					break;
				default:
					break;
			}
		}

		public class ControllerBuilder
		{
			private readonly Controller _controller = new();
			private readonly TableBuilder _tableBuilder = new();
			private readonly RobotBuilder _robotBuilder = new();


			public Controller Build() 
			{
				if (_controller.Table.IsValidLocation(_controller.Robot.CurrentLocation))
				{
					return _controller;
				}
				else
				{
					throw new Exception("Invalid Position");
				}

			}

			public ControllerBuilder WithTable(int width, int length)
			{
				_controller.Table = _tableBuilder.WithWidth(width)
												.WithLength(length)
												.Build();
				return this;
			}

			public ControllerBuilder WithRobot(Point point, Direction direction)
			{
				_controller.Robot = _robotBuilder.WithLocation(point)
												.WithDirection(direction)
												.Build();
				return this;
			}
		}
	}
}
