using System;


namespace Shapes
{
    public class Triangle : Shape
    {
		double side;

		public Triangle(double side)
		{
			this.side = side;
			xPos = DataModel.getNewXPos();
			yPos = DataModel.getNewYPos();
		}
		public override double getArea()
		{
			return Math.Sqrt(3) * Math.Pow(this.side, 2) / 4;
		}

		public override double getPerimeter()
		{
			return 3 * this.side;
		}

		public override void printData()
		{
			Console.WriteLine();
			Console.WriteLine($"My type: {this.GetType()}");
			Console.WriteLine($"Side of triangle = {this.side}");
			Console.WriteLine($"X position = {xPos}");
			Console.WriteLine($"Y position = {yPos}");
		}
	}
}
