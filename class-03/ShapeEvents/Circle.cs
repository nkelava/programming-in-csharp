using System;
using System.Collections;

namespace ShapeEvents
{
	public class Circle:Shape
	{
		private double radius;	
		private static double PI = 3.14159;

		public Circle(double r)
		{
			radius = r;
			xPos = DataModel.getNewXPos();
			yPos = DataModel.getNewYPos();
		}

		public override double getArea()
		{
			return radius*radius*PI;
		}

		public override double getPerimeter()
		{
			return 2*radius*PI;
		}

		public override void printData()
		{
			Console.WriteLine($"\nMy type: {this.GetType().Name}");
			Console.WriteLine($"Radius = {radius}");
			Console.WriteLine($"X position = {xPos}");
			Console.WriteLine($"Y position = {yPos}");
		}
	}
}
