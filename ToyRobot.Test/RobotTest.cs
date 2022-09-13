using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ToyRobot.Robot;

namespace ToyRobot.Test
{
	public class RobotTest
	{
		public RobotTest()
		{

		}

		public static IEnumerable<object[]> DataMove => 
			new List<object[]>
			{
				new object[] { new Point(0, 0), Direction.North, new Point(0, 1) },
				new object[] { new Point(0, 0), Direction.East, new Point(1, 0) },
				new object[] { new Point(0, 5), Direction.South, new Point(0, 4) },
				new object[] { new Point(5, 5), Direction.West, new Point(4, 5) },
			};

		public static IEnumerable<object[]> DataTurnLeft =>
			new List<object[]>
			{
				new object[] { new Point(1, 0), Direction.North, new Point(0, 1) },
				new object[] { new Point(1, 0), Direction.East, new Point(2, 1) },
				new object[] { new Point(0, 5), Direction.South, new Point(1, 4) },
				new object[] { new Point(5, 5), Direction.West, new Point(4, 4) },
			};

		public static IEnumerable<object[]> DataTurnRight =>
			new List<object[]>
			{
				new object[] { new Point(1, 0), Direction.North, new Point(2, 1) },
				new object[] { new Point(1, 1), Direction.East, new Point(2, 0) },
				new object[] { new Point(1, 5), Direction.South, new Point(0, 4) },
				new object[] { new Point(5, 4), Direction.West, new Point(4, 5) },
			};

		public static IEnumerable<object[]> DataMoveReport =>
			new List<object[]>
			{
				new object[] { new Point(0, 0), Direction.North, "Output: 0,1,NORTH" },
				new object[] { new Point(0, 0), Direction.East, "Output: 1,0,EAST" },
				new object[] { new Point(0, 5), Direction.South, "Output: 0,4,SOUTH" },
				new object[] { new Point(5, 5), Direction.West, "Output: 4,5,WEST" },
			};

		public static IEnumerable<object[]> DataTurnLeftReport =>
			new List<object[]>
			{
				new object[] { new Point(1, 0), Direction.North, "Output: 0,1,WEST" },
				new object[] { new Point(1, 0), Direction.East, "Output: 2,1,NORTH" },
				new object[] { new Point(0, 5), Direction.South, "Output: 1,4,EAST" },
				new object[] { new Point(5, 5), Direction.West, "Output: 4,4,SOUTH" },
			};

		public static IEnumerable<object[]> DataTurnRightReport =>
			new List<object[]>
			{
				new object[] { new Point(1, 0), Direction.North, "Output: 2,1,EAST" },
				new object[] { new Point(1, 1), Direction.East, "Output: 2,0,SOUTH" },
				new object[] { new Point(1, 5), Direction.South, "Output: 0,4,WEST" },
				new object[] { new Point(5, 4), Direction.West, "Output: 4,5,NORTH" },
			};

		[Theory]
		[MemberData(nameof(DataMove))]
		public void NextPosition_ValidMove_ReturnNextPositionCorrectly(Point initialPoint, Direction direction, Point expected)
		{
			//Arrange
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			var result = robot.NextPosition();

			//Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[MemberData(nameof(DataMove))]
		public void Move_ValidMove_RobotMoved(Point initialPoint, Direction direction, Point expected)
		{
			//Arrange
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			robot.Move();
			var result = robot.CurrentLocation;

			//Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[MemberData(nameof(DataTurnLeft))]
		public void TurnLeft_ValidMove_RobotTurnedLeft(Point initialPoint, Direction direction, Point expected)
		{
			//Arrange
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			robot.Move();
			robot.TurnLeft();
			robot.Move();
			var result = robot.CurrentLocation;

			//Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[MemberData(nameof(DataTurnRight))]
		public void TurnRight_ValidMove_RobotTurnedRight(Point initialPoint, Direction direction, Point expected)
		{
			//Arrange
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			robot.Move();
			robot.TurnRight();
			robot.Move();
			var result = robot.CurrentLocation;

			//Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[MemberData(nameof(DataMoveReport))]
		public void Report_ValidMove_RobotSuccessfullyReportThePosition(Point initialPoint, Direction direction, string expected)
		{
			//Arrange
			var stringWriter = new StringWriter();
			Console.SetOut(stringWriter);
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			robot.Move();
			robot.Report();
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);

			//Assert
			Assert.Equal(expected, output);
		}

		[Theory]
		[MemberData(nameof(DataTurnLeftReport))]
		public void Report_TurnLeft_RobotSuccessfullyReportThePosition(Point initialPoint, Direction direction, string expected)
		{
			//Arrange
			var stringWriter = new StringWriter();
			Console.SetOut(stringWriter);
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			robot.Move();
			robot.TurnLeft();
			robot.Move();
			robot.Report();
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);

			//Assert
			Assert.Equal(expected, output);
		}

		[Theory]
		[MemberData(nameof(DataTurnRightReport))]
		public void Report_TurnRight_RobotSuccessfullyReportThePosition(Point initialPoint, Direction direction, string expected)
		{
			//Arrange
			var stringWriter = new StringWriter();
			
			Console.SetOut(stringWriter);
			//Console.Clear();
			var robot = new RobotBuilder()
								.WithLocation(initialPoint)
								.WithDirection(direction)
								.Build();

			//Act
			robot.Move();
			robot.TurnRight();
			robot.Move();
			robot.Report();
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);

			//Assert
			Assert.Equal(expected, output);
		}
	}
}
