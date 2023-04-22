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

		public static double Distance(ComplexNumber a, ComplexNumber b)
		{
			return (b - a).Modulus;
		}
		public static double DistanceSquared(ComplexNumber a, ComplexNumber b)
		{
			return (b - a).ModulusSquared;
		}

		public static double AreaOfTriangleWithVertices(ComplexNumber a, ComplexNumber b, ComplexNumber c)
		{
			ComplexNumber diff1 = c - a;
			ComplexNumber diff2 = b - a;

			double argDiff = Mathematics.Math.Abs(diff2.Argument - diff1.Argument);

			return 0.5 * diff1.Modulus * diff2.Modulus * System.Math.Sin(argDiff);
		}
	}
}
