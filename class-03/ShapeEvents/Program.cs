using System;
using System.Collections;

namespace ShapeEvents
{
	/// <summary>
	/// </summary>
	class Program
	{	

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			DataModel dm = new DataModel();
			DataModel.CircleAddedHandler circleAddedDelegate = new DataModel.CircleAddedHandler(DataModel.OnCircleAdded);
			DataModel.Subscribe(circleAddedDelegate);

			Console.WriteLine("Program started");
			Console.WriteLine();
			doMainMenu();

			DataModel.Unsubscribe(circleAddedDelegate);
		}

		private static void printMainMenu()
		{
			Console.WriteLine("You are in the main menu - choose an action\n");
			Console.WriteLine("1 - Insert a new circle");
			Console.WriteLine("2 - Insert a new square");
			Console.WriteLine("3 - Insert a new triangle");
			Console.WriteLine("4 - Print the total area of all inserted elements");
			Console.WriteLine("5 - Print the total perimeter of all inserted elements");
			Console.WriteLine("6 - Print the properties of all inserted elements");
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
				
				if(s=="1")
				{
					doSubMenuCircle();
				}
				else if(s=="2")
				{
					doSubMenuSquare();
				}
				else if (s == "3")
                {
					doSubMenuTriangle();
				}
				else if(s=="4")
				{
					printTotalArea();
				}
				else if(s=="5")
				{
					printTotalPerimeter();
				}
				else if(s=="6")
				{
					printAllData();
				}
				else if(s.ToUpper()=="Q")
				{					
					Console.WriteLine ("\n\nExiting the application");
					break;
				}
			}			
		}

		private static void doSubMenuCircle()
		{
			Console.Clear();

			Console.WriteLine("You are in the circle submenu - insert the radius");
			Console.Write("\nRadius = ");
			string input;

			while (true)
			{				
				input = Console.ReadLine();
				if(input.Trim()!="")
				{
					try
					{
						double r = System.Convert.ToDouble(input);
						Circle myCircle = new Circle(r);

						DataModel.Add(myCircle);

						Console.WriteLine("\nNew circle inserted!");
						Console.Write("Do you want to insert one more circle (y/n): ");

						string s;						
						while (true)
						{							
							s = Console.ReadLine().Trim();
							if(s == "y")
							{
								Console.Write("\nRadius = ");
								break;								
							}							
							else if(s=="n")
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
						Console.WriteLine("\nThe value for the radius is not correct!");
						Console.Write("\nRadius = ");
					}
				}
			}
		}

		private static void doSubMenuSquare()
		{
			Console.Clear();

			Console.WriteLine("You are in the square submenu - insert the length of the side of the square");
			Console.Write("\nSide of the square = ");
			string input;
			while (true)
			{				
				input = Console.ReadLine();
				if(input.Trim() != "")
				{
					try
					{
						double a = System.Convert.ToDouble(input);
						Square mySquare = new Square(a);

						DataModel.Add(mySquare);

						Console.WriteLine("\nNew square inserted!");
						Console.Write("Do you want to insert one more square (y/n): ");

						string s;
						while (true)
						{
							s=Console.ReadLine().Trim();				
							if(s == "y")
							{
								Console.Write("\nSide of the square = ");
								break;								
							}
							else if(s == "n")
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
						Console.WriteLine("\nThe value for the side of the square is not correct!");
						Console.Write("\nSide of the square = ");
					}
				}
			}
		}

		private static void doSubMenuTriangle()
        {
			Console.Clear();

			Console.WriteLine("You are in the triangle submenu - insert the length of the side of the triangle");
			Console.Write("\nSide of the triangle = ");
			string input;

			while (true)
			{
				input = Console.ReadLine();
				if (input.Trim() != "")
				{
					try
					{
						double a = System.Convert.ToDouble(input);
						Triangle myTriangle = new Triangle(a);

						DataModel.Add(myTriangle);	

						Console.WriteLine("\nNew triangle inserted!");
						Console.Write("Do you want to insert one more triangle (y/n): ");

						string s;

						while (true)
						{
							s = Console.ReadLine().Trim();
							if (s == "y")
							{
								Console.Write("\nSide of the triangle = ");
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
						Console.WriteLine("\nThe value for the side of the triangle is not correct!");
						Console.Write("\nSide of the triangle = ");
					}
				}
			}
		}

		private static void printTotalArea()
		{
			Console.Clear();
			Console.WriteLine();			
			Console.WriteLine("The total area of all inserted elements is: "+DataModel.getTotalArea());
			BackOption();
		}

		private static void printTotalPerimeter()
		{
			Console.Clear();
			Console.WriteLine();			
			Console.WriteLine("The total perimeter of all inserted elements is: "+DataModel.getTotalPerimeter());
			BackOption();
		}

		private static void printAllData()
		{
			Console.Clear();
			Console.WriteLine("DATA OF ALL ELEMENTS IN THE LIST");			

			foreach(Shape shape in DataModel.getAllElementsList())
			{
				shape.printData();
			}

			BackOption();
		}

		private static void BackOption()
        {
            Console.WriteLine("\n\nPress any key to go back...");
			Console.ReadKey();
			Console.Clear();
			printMainMenu();
		}
	}
}
