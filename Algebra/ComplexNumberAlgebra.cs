using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexNumbers.Mathematics;

namespace ComplexNumbers.Algebra
{
	public static class ComplexNumberAlgebra
	{
		/// <summary>
		/// Returns the root of the linear equation az+b=0.
		/// </summary>
		/// <param name="a">The coefficient of the z term.</param>
		/// <param name="b">The coefficient of the constant term.</param>
		/// <returns>A <c>ComplexNumber[]</c> of length 1 containing root of the linear equation, if such a root exists. An empty <c>ComplexNumber[]</c> otherwise.</returns>
		public static ComplexNumber[] RootOfLinearEquation(ComplexNumber a, ComplexNumber b)
		{
			if (a == 0)
			{
				if (b == 0)
				{
					throw new Exception("Given equation was 0=0. All complex numbers are roots.")
				}
				return new ComplexNumber[0];
			}
			else
			{
				return new ComplexNumber[] { -1 * b / a };
			}
		}
		/// <summary>
		/// Returns the discriminant of the quadratic equation az²+bz+c=0, b²-4ac.
		/// </summary>
		/// <param name="a">The coefficient of the z² term.</param>
		/// <param name="b">The coefficient of the z term.</param>
		/// <param name="c">The coefficient of the constant term.</param>
		/// <returns>A complex number representing the discriminant of this quadratic equation.</returns>
		public static ComplexNumber DistriminantOfQuadraticEquation(ComplexNumber a, ComplexNumber b, ComplexNumber c)
		{
			return b * b - 4 * a * c;
		}

		/// <summary>
		/// Returns the roots of the quadratic equation az²+bz+c=0.
		/// </summary>
		/// <param name="a">The coefficient of the z² term.</param>
		/// <param name="b">The coefficient of the z term.</param>
		/// <param name="c">The coefficient of the constant term.</param>
		/// <returns>The roots of the quadratic equation.</returns>
		public static ComplexNumber[] RootsOfQuadraticEquation(ComplexNumber a, ComplexNumber b, ComplexNumber c)
		{
			if (a == 0) //is just a linear equation
			{
				return RootOfLinearEquation(b, c);
			}
			ComplexNumber root1 = (-1 * b + Mathematics.Math.Sqrt(DistriminantOfQuadraticEquation(a,b,c))) / (2 * a);
			ComplexNumber root2 = (-1 * b - Mathematics.Math.Sqrt(DistriminantOfQuadraticEquation(a, b, c))) / (2 * a);

			return new ComplexNumber[] { root1, root2 };
		}

		/// <summary>
		/// Returns the discriminant of the quadratic equation az²+bz+c=0, b²-4ac.
		/// </summary>
		/// <param name="a">The coefficient of the z² term.</param>
		/// <param name="b">The coefficient of the z term.</param>
		/// <param name="c">The coefficient of the constant term.</param>
		/// <returns>A double representing the discriminant of this quadratic equation.</returns>
		public static double DistriminantOfQuadraticEquation(double a, double b, double c)
		{
			return b * b - 4 * a * c;
		}

		/// <summary>
		/// Returns the roots of the quadratic equation az²+bz+c=0.
		/// </summary>
		/// <param name="a">The coefficient of the z² term.</param>
		/// <param name="b">The coefficient of the z term.</param>
		/// <param name="c">The coefficient of the constant term.</param>
		/// <returns>The roots of the quadratic equation.</returns>
		public static ComplexNumber[] RootsOfQuadraticEquation(double a, double b, double c)
		{
			if (a == 0) //is just a linear equation
			{
				return RootOfLinearEquation(b, c);
			}

			ComplexNumber root1 = (-1 * b + Mathematics.Math.Sqrt(DistriminantOfQuadraticEquation(a, b, c))) / (2 * a);
			ComplexNumber root2 = (-1 * b - Mathematics.Math.Sqrt(DistriminantOfQuadraticEquation(a, b, c))) / (2 * a);

			return new ComplexNumber[] { root1, root2 };
		}
	}
}
