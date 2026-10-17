using System;
using System.Collections;


namespace ShapeEvents
{
	/// <summary>
	/// Summary description for Square.
	/// </summary>
	public class Square:Shape
	{
		private double oneSide;		

		public Square(double p)
		{
			oneSide = p;
			xPos = DataModel.getNewXPos();
			yPos = DataModel.getNewYPos();			
		}

		public override double getArea()
		{
			return oneSide*oneSide;
		}

		public override double getPerimeter()
		{
			return 4*oneSide;
		}		

		public override void printData()
		{
			Console.WriteLine($"\nMy type: {this.GetType().Name}");
			Console.WriteLine($"Side of the square = {oneSide}");
			Console.WriteLine($"X position = {xPos}");
			Console.WriteLine($"Y position = {yPos}");
		}
	}
}
