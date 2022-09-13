using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ToyRobot.Test
{
	public class CommandTest
	{
		public static IEnumerable<object[]> DataValidPlacement =>
			new List<object[]>
			{
				new object[] { "PLACE 0,0,NORTH", new Point(0, 0),  Direction.North },
				new object[] { "PLACE 1,2,EAST", new Point(1, 2), Direction.East }
			};

		[Theory]
		[InlineData("PLACE 0,0,NORTH")]
		[InlineData("PLACE 1,2,EAST")]
		public void Process_ValidPlacement_ScuccessPlaceRobot(string command)
		{
			//Arrange
			var sut = new Command();

			//Act
			sut.Process(command);

			//Assert
			Assert.True(sut.Placement);

		}

		[Theory]
		[InlineData("PLACE")]
		[InlineData("PLACE ,EAST")]
		public void Process_InvalidPlacement_UnableToPlaceRobot(string command)
		{
			//Arrange
			var sut = new Command();

			//Act
			sut.Process(command);

			//Assert
			Assert.False(sut.Placement);

		}

		[Theory]
		[InlineData("MOVE", Actions.MOVE)]
		[InlineData("LEFT", Actions.LEFT)]
		[InlineData("RIGHT", Actions.RIGHT)]
		[InlineData("REPORT", Actions.REPORT)]
		public void Process_ValidAction_RobotSuccessfullyInvokeTheAction(string action, Actions expected)
		{
			//Arrange
			var sut = new Command();

			//Act
			sut.Process("PLACE 3,3,EAST");
			sut.Process(action);

			//Assert
			Assert.Equal(expected, sut.LastAction);
		}

		[Theory]
		[InlineData("MOVED")]
		[InlineData("LEFT RIGHT")]
		[InlineData("RIGHT RIGHT")]
		[InlineData("REPORT 1,3,NORTH")]
		public void Process_InvalidAction_UnableToInvokeTheAction(string action)
		{
			//Arrange
			var sut = new Command();

			//Act
			sut.Process("PLACE 3,3,EAST");
			sut.Process(action);

			//Assert
			Assert.Equal(Actions.NOACTION, sut.LastAction);
		}

		[Theory]
		[InlineData("MOVE")]
		[InlineData("LEFT")]
		[InlineData("RIGHT")]
		[InlineData("REPORT")]
		public void Process_MovingWithouPlacement_UnableToInvokeTheAction(string action)
		{
			//Arrange
			var sut = new Command();

			//Act
			sut.Process(action);

			//Assert
			Assert.Equal(Actions.NOACTION, sut.LastAction);
		}
	}
}
