using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	public class Table
	{
		public int Width { get; set; }
		public int Length { get; set; }

		protected Table() { }
		/// <summary>
		/// Check if the given location is within the table.
		/// </summary>
		/// <param name="point">(X,Y) coordination of the new location.</param>
		/// <returns></returns>
		public bool IsValidLocation(Point point) => point.X >= 0 && point.X <= Width && point.Y >= 0 && point.Y <= Length;

		public class TableBuilder
		{
			private readonly Table _table = new();

			public Table Build() => _table;

			public TableBuilder WithWidth(int width)
			{
				_table.Width = width;
				return this;
			}

			public TableBuilder WithLength(int length)
			{
				_table.Length = length;
				return this;
			}
		}
	}
}
