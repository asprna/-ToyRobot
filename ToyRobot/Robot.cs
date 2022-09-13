using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	/// <summary>
	/// The direction of the robot facing.
	/// </summary>
	public enum Direction
	{
		North = 0,
		South = 1,
		East = 2,
		West = 3
	}

	public class Robot
	{
		//public Point CurrentLocation { get; private set; }
		public Point CurrentLocation { get; set; }
		//public Direction CurrentDirection { get; private set; }
		public Direction CurrentDirection { get; set; }

		private readonly Dictionary<Direction, Direction> _turnLeft = new()
		{
			{ Direction.North, Direction.West },
			{ Direction.West, Direction.South },
			{ Direction.South, Direction.East },
			{ Direction.East, Direction.North }
		};

		private readonly Dictionary<Direction, Direction> _turnRight = new()
		{
			{ Direction.North, Direction.East },
			{ Direction.East, Direction.South },
			{ Direction.South, Direction.West },
			{ Direction.West, Direction.North }
		};

		protected Robot()	{}

		public Point NextPosition() 
		{
			var newlocation = new Point();

			newlocation = CurrentDirection switch
			{
				Direction.North => MoveNorth(),
				Direction.South => MoveSouth(),
				Direction.East => MoveEast(),
				Direction.West => MoveWest(),
				_ => CurrentLocation,
			};
			return newlocation;
		}
		
		public void Move() => CurrentLocation = NextPosition();
		
		public void TurnLeft() => CurrentDirection = _turnLeft[CurrentDirection];
		
		public void TurnRight() => CurrentDirection = _turnRight[CurrentDirection];
		
		public void Report() => Console.WriteLine($"Output: {CurrentLocation.X},{CurrentLocation.Y},{CurrentDirection.ToString().ToUpper()}");

		private Point MoveNorth() => Point.Add(CurrentLocation, new Size(0, 1));
		
		private Point MoveSouth() => Point.Subtract(CurrentLocation, new Size(0, 1));
		
		private Point MoveEast() => Point.Add(CurrentLocation, new Size(1, 0));
		
		private Point MoveWest() => Point.Subtract(CurrentLocation, new Size(1, 0));

		public class RobotBuilder
		{
			private readonly Robot _robot = new();

			public Robot Build() => _robot;

			public RobotBuilder WithLocation(Point point)
			{
				_robot.CurrentLocation = point;
				return this;
			}

			public RobotBuilder WithDirection(Direction direction)
			{
				_robot.CurrentDirection = direction;
				return this;
			}
		}
	}
}
