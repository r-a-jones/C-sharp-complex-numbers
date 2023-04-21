using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers.Math
{
	public static class ComplexNumberMath
	{
		private static ComplexNumber i = ComplexNumber.i;
		public static double Abs(ComplexNumber z)
		{
			return z.Modulus;
		}

		public static ComplexNumber Acos(ComplexNumber z)
		{
			throw new NotImplementedException();
		}

		public static ComplexNumber Acosh(ComplexNumber z)
		{
			throw new NotImplementedException();
		}

		public static ComplexNumber Asin(ComplexNumber z)
		{
			throw new NotImplementedException();
		}

		public static ComplexNumber Asinh(ComplexNumber z)
		{
			throw new NotImplementedException();
		}

		public static ComplexNumber Atan(ComplexNumber z)
		{
			throw new NotImplementedException();
		}

		public static ComplexNumber Atanh(ComplexNumber z)
		{
			throw new NotImplementedException();
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
	}
}
