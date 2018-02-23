using System;
using MatrixOperations; 

namespace Homework3
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix;
            Console.WriteLine("Please, input sizes of rows and colums.");
            int rows = Convert.ToInt32(Console.ReadLine());
            int cols = Convert.ToInt32(Console.ReadLine());
            matrix = new double[rows, cols];
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    matrix[i, j] = Math.Round(rand.NextDouble() * 100, 2);
                    Console.Write(matrix[i, j] + "    ");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");


            Matrix mat = new Matrix(matrix);




            Console.WriteLine("The inversion of matrix.");
            mat.Inverse().Print();

            Console.WriteLine("The transposition of matrix.");
            mat.Transpose().Print();

            Console.WriteLine("The  Multiplication of the matrix and it's inversion.");
            (mat.Inverse() * mat).Print();

            Console.WriteLine("The scalar multiplication.");
            (5 * mat).Print();

            Console.WriteLine("Please, enter " + mat.cols + " distance numbers.");
            double[] dictances = new double[mat.cols];
            for(int i = 0; i < mat.cols; i++)
            {
                dictances[i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("The matrix translation.");
            mat.Translation(dictances).Print();

            Console.WriteLine("Please enter the axis the shape will be rotated around");
            String axis = Console.ReadLine();
            Console.WriteLine("The number of degrees the shape will be rotated through");
            double angle = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("The matrix rotation.");
            mat.Rotate3D(axis, angle).Print();


            double[] factor = { 4, 1, 3};
            Console.WriteLine("The matrix scaling.");
            mat.Scale(factor).Print();

            Console.WriteLine("The largest element of matrix.");
            Console.WriteLine(mat.LargestElement());

            Console.WriteLine("The smallest element of matrix.");
            Console.WriteLine(mat.SmallestElement());

            
            //ortogonal matrix to test Ortogonal() method.
            double[,] ortogonal =
            {
                {2.0/3,1.0/3,2.0/3},
                {-2.0/3,2.0/3,1.0/3},
                {1.0/3,2.0/3,-2.0/3},
            };
            Console.WriteLine("\n");
            Matrix ort = new Matrix(ortogonal);
            ort.Print();
            if (ort.Ortogonal())
            {
                Console.WriteLine("This matrix is ortogonal.");
            }
            else
            {
                Console.WriteLine("This matrix isn't ortogonal.");
            }

            Console.WriteLine("The addition of two matrices.");
            (ort + mat).Print();
        }
    }
}
