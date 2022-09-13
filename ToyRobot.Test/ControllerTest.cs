using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ToyRobot.Controller;

namespace ToyRobot.Test
{
	public class ControllerTest
	{
		public static IEnumerable<object[]> DataValidPosition =>
			new List<object[]>
			{
				new object[] { new Point(1, 3), Direction.North },
				new object[] { new Point(3, 4), Direction.East },
				new object[] { new Point(4, 1), Direction.South },
				new object[] { new Point(5, 5), Direction.West }
			};

		public static IEnumerable<object[]> DataInValidPosition =>
			new List<object[]>
			{
				new object[] { new Point(-1, 3), Direction.North },
				new object[] { new Point(3, -4), Direction.East },
				new object[] { new Point(6, 1), Direction.South },
				new object[] { new Point(5, 8), Direction.West }
			};

		public static IEnumerable<object[]> DataAction =>
			new List<object[]>
			{
				new object[] { new Point(1, 3), Direction.North, Actions.MOVE, new Point(1, 4), Direction.North},
				new object[] { new Point(3, 4), Direction.East, Actions.LEFT, new Point(3, 4), Direction.North},
				new object[] { new Point(4, 1), Direction.South, Actions.RIGHT, new Point(4, 1), Direction.West},
				new object[] { new Point(5, 5), Direction.West, Actions.REPORT, new Point(5, 5), Direction.West},
				new object[] { new Point(0, 5), Direction.North, Actions.MOVE, new Point(0, 5), Direction.North},
				new object[] { new Point(5, 5), Direction.East, Actions.MOVE, new Point(5, 5), Direction.East },
				new object[] { new Point(5, 0), Direction.South, Actions.MOVE, new Point(5, 0), Direction.South},
				new object[] { new Point(0, 0), Direction.West, Actions.MOVE, new Point(0, 0), Direction.West }
			};

		[Theory]
		[MemberData(nameof(DataValidPosition))]
		public void BuildConroller_ValidPosition_SuccessfullyBuildTheController(Point point, Direction direction)
		{
			//Arrange & Act
			var controller = new ControllerBuilder()
									.WithRobot(point, direction)
									.WithTable(5, 5)
									.Build();

			//Assert
			Assert.NotNull(controller.Robot);
		}

		[Theory]
		[MemberData(nameof(DataInValidPosition))]
		public void BuildConroller_InValidPosition_ShouldReturnException(Point point, Direction direction)
		{
			//Arrange, Act & Assert
			Exception result = Assert.Throws<Exception>(() => new ControllerBuilder()
																	.WithRobot(point, direction)
																	.WithTable(5, 5)
																	.Build());

			Assert.Equal("Invalid Position", result.Message);
		}

		[Theory]
		[MemberData(nameof(DataAction))]
		public void Action_ValidAction_SuccessfullyInvokeActionForRobot(Point point, Direction direction, Actions actions, Point expectedPoint, Direction expectedDirection)
		{
			//Arrange
			var controller = new ControllerBuilder()
									.WithRobot(point, direction)
									.WithTable(5, 5)
									.Build();

			//Act
			controller.Action(actions);

			//Assert
			Assert.Equal(expectedPoint, controller.Robot.CurrentLocation);
			Assert.Equal(expectedDirection, controller.Robot.CurrentDirection);
		}
	}
}
