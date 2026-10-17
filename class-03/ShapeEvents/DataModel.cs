using System;
using System.Collections;

namespace ShapeEvents
{
	/// <summary>
	/// Summary description for DataModel.
	/// </summary>
	public class DataModel
	{
		private static ArrayList ALL_ELEMENTS;
		public delegate void CircleAddedHandler(string text);
		public static event CircleAddedHandler CircleAdded;

		public static void Subscribe(CircleAddedHandler circleAddedHandler)
        {
			CircleAdded += circleAddedHandler;
		}

		public static void Unsubscribe(CircleAddedHandler circleAddedHandler)
        {
			CircleAdded -= circleAddedHandler;
		}

        public static void Add(object newElement)
        {
            if (newElement.GetType() == typeof(Circle))
            {
                var circle = (Circle)newElement;
                CircleAdded?.Invoke($"\nEVENT RAISED:  x-{circle.getXPos()}, y-{circle.getYPos()}");
            }

            ALL_ELEMENTS.Add(newElement);
        }

        public static void OnCircleAdded(string text)
		{
			Console.WriteLine(text);
		}

		public DataModel()
		{
			ALL_ELEMENTS = new ArrayList();
		}
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public static double getNewXPos()
		{
			if(ALL_ELEMENTS.Count == 0)
			{
				return 1;
			}
			else
			{			
				Shape lastShapeInList = (Shape)ALL_ELEMENTS[ALL_ELEMENTS.Count-1];
				return lastShapeInList.getXPos()+1;				
			}
		}

		public static double getNewYPos()
		{
			if(ALL_ELEMENTS.Count == 0)
			{
				return 1;
			}
			else
			{			
				Shape lastShapeInList = (Shape)ALL_ELEMENTS[ALL_ELEMENTS.Count-1];
				return lastShapeInList.getYPos() + 2;
			}
		}

		public static ArrayList getAllElementsList()
		{
			return ALL_ELEMENTS;
		}

		public static double getTotalArea()
		{
			double totalArea = 0;

			foreach(Shape shape in ALL_ELEMENTS)
			{
				totalArea += shape.getArea();
			}

			return totalArea;
		}

		public static double getTotalPerimeter()
		{
			double totalPerimeter = 0;

			foreach(Shape shape in ALL_ELEMENTS)
			{
				totalPerimeter += shape.getPerimeter();
			}

			return totalPerimeter;
		}
	}
}
