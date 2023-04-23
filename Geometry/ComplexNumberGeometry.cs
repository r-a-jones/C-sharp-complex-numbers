using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexNumbers.Mathematics;

namespace ComplexNumbers.Geometry
{
	public static class ComplexNumberGeometry
	{
		#region Distance
		public static double Distance(ComplexNumber a, ComplexNumber b)
		{
			return (b - a).Modulus;
		}
		public static double DistanceSquared(ComplexNumber a, ComplexNumber b)
		{
			return (b - a).ModulusSquared;
		}
		#endregion

		public static double AreaOfTriangleWithVertices(ComplexNumber a, ComplexNumber b, ComplexNumber c)
		{
			ComplexNumber diff1 = c - a;
			ComplexNumber diff2 = b - a;

			double argDiff = Mathematics.Math.Abs(diff2.Argument - diff1.Argument);

			return 0.5 * diff1.Modulus * diff2.Modulus * System.Math.Sin(argDiff);
		}

		#region Lines
		/// <summary>
		/// Returns whether the lines defined by the points (<c>line1A</c>, <c>line1B</c>) and (<c>line2A</c>, <c>line2B</c>) are parallel.
		/// </summary>
		/// <param name="line1A">A point on the first line.</param>
		/// <param name="line1B">A point on the first line.</param>
		/// <param name="line2A">A point on the second line.</param>
		/// <param name="line2B">A point on the second line.</param>
		/// <returns>Boolean - <c>true</c> if the lines are parallel, <c>false</c> if they are not.</returns>
		/// <exception cref="Exception">Throws an exception if the points to define a line are not distinct.</exception>
		public static bool LinesAreParallel(ComplexNumber line1A, ComplexNumber line1B, ComplexNumber line2A, ComplexNumber line2B)
		{
			if ((line1A == line1B) || (line2A == line2B))
			{
				throw new Exception("Points defining lines need to be distinct.");
			}

			return ((line1B - line1A).Argument == (line2B - line2A).Argument);
		}

		/// <summary>
		/// Returns whether the lines defined by the points (<c>line1A</c>, <c>line1B</c>) and (<c>line2A</c>, <c>line2B</c>) intersect.
		/// </summary>
		/// <param name="line1A">A point on the first line.</param>
		/// <param name="line1B">A point on the first line.</param>
		/// <param name="line2A">A point on the second line.</param>
		/// <param name="line2B">A point on the second line.</param>
		/// <returns>Boolean - <c>true</c> if the lines intersect, <c>false</c> if they do not.</returns>
		/// <exception cref="Exception">Throws an exception if the points to define a line are not distinct.</exception>

		public static bool LinesIntersect(ComplexNumber line1A, ComplexNumber line1B, ComplexNumber line2A, ComplexNumber line2B)
		{
			if ((line1A == line1B) || (line2A == line2B))
			{
				throw new Exception("Points defining lines need to be distinct.");
			}
			return !(LinesAreParallel(line1A, line1B, line2A, line2B));
		}
		#endregion

		#region Circles
		#endregion
	}
}
