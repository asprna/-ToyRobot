using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ToyRobot.Table;

namespace ToyRobot.Test
{
    public class TebleTest
	{
        [Theory]
        [InlineData(0, 4)]
        [InlineData(4, 0)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        public void IsValidLocation_LocationAreValid_True(int x, int y)
        {
            //Arrange
            Table table = new TableBuilder()
                                .WithWidth(5)
                                .WithLength(5)
                                .Build();

            Point point = new Point(x, y);

            //Act
            var result = table.IsValidLocation(point);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 6)]
        [InlineData(6, 8)]
        public void IsValidLocation_LocationAreInValid_False(int x, int y)
        {
            //Arrange
            Table table = new TableBuilder()
                                .WithWidth(5)
                                .WithLength(5)
                                .Build();

            Point point = new Point(x, y);

            //Act
            var result = table.IsValidLocation(point);

            //Assert
            Assert.False(result);
        }
    }
}
