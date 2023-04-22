using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers
{
	
	static class ComplexNumberExtensions
	{

		#region Random complex numbers in regions of the plane
		public static ComplexNumber NextComplexNumberInSquare(this Random r, double minReal, double maxReal, double minImaginary, double maxImaginary)
		{
			if (minReal > maxReal)
			{
				throw new Exception("Minimum real part given was greater than maximum real part given.");
			}
			if (minImaginary > maxImaginary)
			{
				throw new Exception("Minimum imaginary part given was greater than maximum imaginary part given.");
			}
			double realPart = minReal + r.NextDouble() * (maxReal-minReal);
			double imaginaryPart = minImaginary + r.NextDouble() * (maxImaginary - minImaginary);

			return ComplexNumber.FromCartesian(realPart, imaginaryPart);
		}

		public static ComplexNumber NextComplexNumberInAnnularSector(this Random r, ComplexNumber center, double minRadius, double maxRadius, double minArgument, double maxArgument)
		{
			if (maxArgument - minArgument > System.Math.Tau)
			{
				throw new Exception("The maximum and minimum argument differ by more than 2pi.");
			}
			if (minArgument > maxArgument)
			{
				throw new Exception("Minimum argument given was greater than maximum argument given.");
			}
			if (minRadius > maxRadius)
			{
				throw new Exception("Minimum radius given was greater than maximum radius given.");
			}
			if (maxRadius < 0)
			{
				throw new Exception("Maximum radius given was less than 0.");
			}
			double argument = minArgument + r.NextDouble() * (maxArgument - minArgument);
			double radius = minRadius + r.NextDouble() * (maxRadius - minRadius);

			return center + radius * ComplexNumber.FromPolar(1, argument);
		}

		public static ComplexNumber NextComplexNumberInSector(this Random r, ComplexNumber center, double maxRadius, double minArgument, double maxArgument)
		{
			return NextComplexNumberInAnnularSector(r, center, 0, maxRadius, minArgument, maxArgument);
		}

		public static ComplexNumber NextComplexNumberInAnnulus(this Random r, ComplexNumber center, double minRadius, double maxRadius)
		{
			return NextComplexNumberInAnnularSector(r, center, minRadius, maxRadius, 0, 2 * Math.PI);
		}

		public static ComplexNumber NextComplexNumberInCircle(this Random r, ComplexNumber center, double maxRadius)
		{
			return NextComplexNumberInAnnularSector(r, center, 0, maxRadius, 0, 2 * Math.PI);
		}
		#endregion
	}
}
