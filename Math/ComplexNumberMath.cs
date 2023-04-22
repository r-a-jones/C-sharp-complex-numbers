using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers.Mathematics
{
	public static class Math
	{
		private static ComplexNumber i = ComplexNumber.i;
		public static double Abs(ComplexNumber z)
		{
			return z.Modulus;
		}

		public static ComplexNumber Acos(ComplexNumber z)
		{
			return System.Math.PI / 2 + i * Log(Sqrt(1-z*z) + i * z);
		}

		public static ComplexNumber Acosh(ComplexNumber z)
		{
			return Log(Sqrt(-1 + z * z) + z);
		}

		public static ComplexNumber Asin(ComplexNumber z)
		{
			return -1 * i * Log(Sqrt(1 - z * z) + i * z);
		}

		public static ComplexNumber Asinh(ComplexNumber z)
		{
			return Log(Sqrt(1 + z * z) + z);
		}

		public static ComplexNumber Atan(ComplexNumber z)
		{
			return 0.5 * i * Log(1 - i * z) - 0.5 * i * Log(1 + i * z);
		}

		public static ComplexNumber Atanh(ComplexNumber z)
		{
			return 0.5 * Log(1 + z) - 0.5 * Log(1 - z);
		}

		public static ComplexNumber Cbrt(ComplexNumber z)
		{
			return Pow(z, 1 / 3);
		}

		public static ComplexNumber Cos(ComplexNumber z)
		{
			return (Exp(i * z) + Exp(-1 * i * z)) / (2);
		}

		public static ComplexNumber Cosh(ComplexNumber z)
		{
			return (Exp(z) + Exp(-1 * z)) / (2);
		}

		public static ComplexNumber Exp(ComplexNumber z)
		{
			return CalculateTaylorSeries(new Func<int, ComplexNumber>(n => 1 / (double)Factorial(n)), z);
		}

		public static ComplexNumber Log(ComplexNumber z)
		{
			return System.Math.Log(z.Modulus) + i * z.Argument;
		}

		public static ComplexNumber Log10(ComplexNumber z)
		{
			return Log(z) / Log(10);
		}

		public static ComplexNumber Log2(ComplexNumber z)
		{
			return Log(z) / Log(2);
		}

		public static ComplexNumber Ln(ComplexNumber z)
		{
			return Log(z);
		}

		public static ComplexNumber Pow(ComplexNumber z, ComplexNumber w)
		{
			return Exp(w.Real / 2 * Log(z.ModulusSquared) - w.Imaginary * z.Argument) * Exp(i*(w.Imaginary / 2 * Log(z.ModulusSquared) + w.Real * z.Argument));
		}

		public static ComplexNumber Sin(ComplexNumber z)
		{
			return (Exp(i * z) - Exp(-1 * i * z)) / (2 * i);
		}

		public static ComplexNumber Sinh(ComplexNumber z)
		{
			return (Exp(z) - Exp(-1 * z)) / (2);
		}

		public static ComplexNumber Sqrt(ComplexNumber z)
		{
			return Pow(z, 1 / 2);
		}

		public static ComplexNumber Tan(ComplexNumber z)
		{
			return Sin(z)/Cos(z);
		}

		public static ComplexNumber Tanh(ComplexNumber z)
		{
			return Sinh(z) / Cosh(z);
		}

		private static ComplexNumber CalculateTaylorSeries(Func<int, ComplexNumber> coefficient, ComplexNumber at)
		{
			ComplexNumber result = 0;

			ComplexNumber currentPow = 1;

			for (int i = 0; i < 30; i++)
			{
				result += coefficient(i) * currentPow;

				currentPow *= at;
			}

			return result;

		}

		private static int Factorial(int x)
		{
			if (x == 0)
			{
				return 1;
			}
			else
			{
				return x * Factorial(x - 1);
			}
		}

		/// <summary>
		/// Represents the natural logarithmic base, specified by the constant, e.
		/// </summary>
		public static readonly ComplexNumber E = ComplexNumber.FromCartesian(System.Math.E, 0);
		/// <summary>
		/// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
		/// </summary>
		public static readonly ComplexNumber PI = ComplexNumber.FromCartesian(System.Math.PI, 0);
		/// <summary>
		/// Represents the number of radians in one turn, specified by the constant, τ.
		/// </summary>
		public static readonly ComplexNumber Tau = ComplexNumber.FromCartesian(System.Math.Tau, 0);
	}


}
