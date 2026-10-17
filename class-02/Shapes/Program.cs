using System;


namespace Shapes
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var dm = new DataModel();
            Console.WriteLine("Program started");
            Console.WriteLine();
            doMainMenu();
        }

		private static void printMainMenu()
		{
			Console.WriteLine("You are in main menu - choose action\n");
			Console.WriteLine("1 - Insert new circle");
			Console.WriteLine("2 - Insert new square");
			Console.WriteLine("3 - Insert new triangle");
			Console.WriteLine("4 - Print total area of all inserted elements");
			Console.WriteLine("5 - Print total perimeter of all inserted elements");
			Console.WriteLine("6 - Print properties of all inserted elements");
			Console.WriteLine("Q - Exit");
			Console.Write("\nAction: ");

		}
		private static void doMainMenu()
		{
			printMainMenu();

			string s;
			while (true)
			{
				s = Console.ReadLine().Trim();

				if (s == "1")
				{
					doSubMenuCircle();
				}
				else if (s == "2")
				{
					doSubMenuSquare();
				}
				else if (s == "3")
				{
					doSubMenuTriangle();
				}
				else if (s == "4")
				{
					printTotalArea();
				}
				else if (s == "5")
				{
					printTotalPerimeter();
				}
				else if (s == "6")
				{
					printAllData();
				}
				else if (s.ToUpper() == "Q")
				{
					Console.WriteLine("\n\nExit of application");
					break;
				}
			}
		}

		private static void doSubMenuCircle()
		{
			Console.Clear();

			Console.WriteLine("You are in sub menu for circle - insert the value of radius");
			Console.Write("\nRadius = ");
			string input;
			while (true)
			{
				input = Console.ReadLine();
				if (input.Trim() != "")
				{
					try
					{
						var r = System.Convert.ToDouble(input);
						var myCircle = new Circle(r);
						DataModel.getAllElementsList().Add(myCircle);
						Console.WriteLine("\nNew circle inserted!");
						Console.Write("Do you want to insert one more circle(y/n): ");

						string s;
						while (true)
						{
							s = Console.ReadLine().Trim();
							if (s == "y")
							{
								Console.Write("\nRadius = ");
								break;
							}
							else if (s == "n")
							{
								Console.WriteLine();
								Console.Clear();
								printMainMenu();
								return;
							}
						}

					}
					catch
					{
						Console.WriteLine("\nValue for radius is not correct!");
						Console.Write("\nRadius = ");
					}
				}
			}
		}

		private static void doSubMenuSquare()
		{
			Console.Clear();

			Console.WriteLine("You are in sub menu for square - insert the value for the side of square");
			Console.Write("\nSide of square = ");
			string input;
			while (true)
			{
				input = Console.ReadLine();
				if (input.Trim() != "")
				{
					try
					{
						var a = System.Convert.ToDouble(input);
						var mySquare = new Square(a);
						DataModel.getAllElementsList().Add(mySquare);
						Console.WriteLine("\nNew square inserted!");
						Console.Write("Do you want to insert one more square(y/n): ");

						string s;
						while (true)
						{
							s = Console.ReadLine().Trim();
							if (s == "y")
							{
								Console.Write("\nSide of square = ");
								break;
							}
							else if (s == "n")
							{
								Console.WriteLine();
								Console.Clear();
								printMainMenu();
								return;
							}
						}

					}
					catch
					{
						Console.WriteLine("\nValue for the side of square is not correct!");
						Console.Write("\nSide of square = ");
					}
				}
			}
		}

		private static void doSubMenuTriangle()
		{
			Console.Clear();

			Console.WriteLine("You are in sub menu for triangle - insert the value for the side of triangle");
			Console.Write("\nSide of triangle = ");
			string input;

			while (true)
			{
				input = Console.ReadLine();
				if (input.Trim() != "")
				{
					try
					{
						var a = System.Convert.ToDouble(input);
						var myTriangle = new Triangle(a);
						DataModel.getAllElementsList().Add(myTriangle);
						Console.WriteLine("\nNew triangle inserted!");
						Console.Write("Do you want to insert one more triangle(y/n): ");

						string s;

						while (true)
						{
							s = Console.ReadLine().Trim();
							if (s == "y")
							{
								Console.Write("\nSide of triangle = ");
								break;
							}
							else if (s == "n")
							{
								Console.WriteLine();
								Console.Clear();
								printMainMenu();
								return;
							}
						}

					}
					catch
					{
						Console.WriteLine("\nValue for the side of triangle is not correct!");
						Console.Write("\nSide of triangle = ");
					}
				}
			}
		}

		private static void printTotalArea()
		{
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine("Total area of all inserted elements is: " + DataModel.getTotalArea());
			BackOption();
		}

		private static void printTotalPerimeter()
		{
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine("Total perimeter of all inserted elements is: " + DataModel.getTotalPerimeter());
			BackOption();
		}

		private static void printAllData()
		{
			Console.Clear();
			Console.WriteLine("DATA OF ALL ELEMENTS IN LIST");

			foreach (Shape shape in DataModel.getAllElementsList())
			{
				shape.printData();
			}

			BackOption();
		}

		private static void BackOption()
		{
			Console.WriteLine("\n\nPress anything to go back...");
			Console.ReadKey();
			Console.Clear();
			printMainMenu();
		}
	}
}
