﻿namespace ComplexNumbers
{
	public class ComplexNumber
	{
		private (double, double, ComplexNumberStoreMode) data;

		private bool isInfinity; //is the complex number infinity?

		/// <summary>
		/// How should division by zero of complex numbers be handled?
		/// </summary>
		public static ComplexNumberDivideByZeroMode divideByZeroMode = ComplexNumberDivideByZeroMode.throwException;

		#region Initialise complex numbers
		public static ComplexNumber FromCartesian(double real, double imaginary)
		{
			ComplexNumber z = new ComplexNumber();

			z.data = (real, imaginary, ComplexNumberStoreMode.cartesian);

			z.isInfinity = false;
			return z;
		}

		public static ComplexNumber FromPolar(double modulus, double argument)
		{
			if (modulus < 0)
			{
				throw new ArgumentOutOfRangeException("Modulus must be greater than or equal to 0. Received " + modulus + ".");
			}
			ComplexNumber z = new ComplexNumber();

			if (modulus == 0)
			{
				z.data = (modulus, 0, ComplexNumberStoreMode.polar);
			}
			else
			{
				z.data = (modulus, argument % (2 * System.Math.PI), ComplexNumberStoreMode.polar);
			}
			z.isInfinity = false;

			return z;
		}

		public ComplexNumber()
		{
		}

		public ComplexNumber(double real, double imaginary)
		{
			data = (real, imaginary, ComplexNumberStoreMode.cartesian);
			isInfinity = false;
		}


		public ComplexNumber(double d)
		{
			data = (d, 0, ComplexNumberStoreMode.cartesian);
			isInfinity = false;
		}

		public ComplexNumber(int i)
		{
			data = (i, 0, ComplexNumberStoreMode.cartesian);
			isInfinity = false;
		}

		//public static implicit operator int(ComplexNumber d) => d.digit;
		public static implicit operator ComplexNumber(int i) => new ComplexNumber(i);
		public static implicit operator ComplexNumber(double d) => new ComplexNumber(d);

		#endregion

		#region Real and imaginary parts
		public double Real
		{
			get
			{
				if (isInfinity)
				{
					throw new Exception("The given complex number was infinity and thus does not have an imaginary part.");
				}
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return data.Item1;
				}
				else
				{
					return Modulus * Math.Cos(Argument);
				}
			}

		}

		public double Imaginary
		{
			get
			{
				if (isInfinity)
				{
					throw new Exception("The given complex number was infinity and thus does not have an imaginary part.");
				}
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return data.Item2;
				}
				else
				{
					return Real * Math.Sin(Argument);
				}
			}

		}
		#endregion

		#region Modulus and argument
		public double Modulus
		{
			get
			{
				if (isInfinity)
				{
					return double.PositiveInfinity;
				}
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return Math.Sqrt(ModulusSquared);
				}
				else
				{
					return data.Item1;
				}
			}

		}

		public double ModulusSquared
		{
			get
			{
				if (isInfinity)
				{
					return double.PositiveInfinity;
				}
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return Real * Real + Imaginary * Imaginary;
				}
				else
				{
					return Modulus * Modulus;
				}
			}

		}

		public double Argument
		{
			get
			{
				if (isInfinity)
				{
					throw new Exception("The given complex number was infinity and thus the argument is not defined.");
				}
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return Math.Atan2(Imaginary, Real);
				}
				else
				{
					return data.Item2;
				}
			}

		}
		#endregion

		#region ToString()

		public override string ToString()
		{
			return ToStringCartesian();
		}

		public string ToStringCartesian()
		{
			if (isInfinity)
			{
				return "∞";
			}
			if (Imaginary == 0)
			{
				return Real.ToString();
			}
			if (Real == 0)
			{
				if (Imaginary == 1)
				{
					return "i";
				}
				else if (Imaginary == -1)
				{
					return "-i";
				}
				return Imaginary.ToString() + "i";
			}

			if (Imaginary == 1)
			{
				return Real.ToString() + "+i";
			}
			else if (Imaginary == -1)
			{
				return Real.ToString() + "-i";
			}
			else if (Imaginary > 0)
			{
				return Real.ToString() + "+" + Imaginary.ToString() + "i";
			}
			else
			{
				return Real.ToString() + "-" + (-Imaginary).ToString() + "i";
			}

		}

		public string ToStringPolar()
		{
			if (isInfinity)
			{
				return "∞";
			}
			if (Modulus == 0)
			{
				return "0";
			}
			if (Modulus == 1)
			{
				return "exp(" + Argument + ")";
			}
			else
			{
				return Modulus.ToString() + "*" + "exp(" + Argument + ")";
			}
		}
		#endregion

		#region Equality

		public override bool Equals(object? obj)
		{
			if (obj is null)
			{
				return this is null;
			}
			else if (obj is ComplexNumber)
			{
				return (ComplexNumber)obj == this;
			}
			else if (obj is double)
			{
				return (double)obj == this;
			}
			else if (obj is int)
			{
				return (int)obj == this;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(ComplexNumber z1, ComplexNumber z2)
		{
			if (IsInfinity(z1) || IsInfinity(z2))
			{
				return (IsInfinity(z1) && IsInfinity(z2));
			}
			//ruled out the possibility either are infinity
			if (z1.data.Item3 == z2.data.Item3)
			{
				return (z1.data.Item1 == z2.data.Item1) && (z1.data.Item2 == z2.data.Item2);
			}
			else
			{
				return (z1.Real == z2.Real && z1.Imaginary == z2.Imaginary);
			}
		}

		public static bool operator !=(ComplexNumber z1, ComplexNumber z2)
		{
			return ((z1 == z2) == false);
		}

		public static bool operator ==(ComplexNumber z, double d)
		{
			return z == (ComplexNumber)d;
		}

		public static bool operator !=(ComplexNumber z, double d)
		{
			return ((z == d) == false);
		}

		public static bool operator ==(double d, ComplexNumber z)
		{
			return z == d;
		}

		public static bool operator !=(double d, ComplexNumber z)
		{
			return z != d;
		}

		public static bool operator ==(ComplexNumber z, int i)
		{
			return z == (ComplexNumber)i;
		}

		public static bool operator !=(ComplexNumber z, int i)
		{
			return ((z == i) == false);
		}

		public static bool operator ==(int i, ComplexNumber z)
		{
			return z == i;
		}

		public static bool operator !=(int i, ComplexNumber z)
		{
			return z != i;
		}
		#endregion

		#region Infinity

		/// <summary>
		/// Returns a value indicating whether the specified complex number evaluates to infinity.
		/// </summary>
		public static bool IsInfinity(ComplexNumber z)
		{
			if (z is null)
			{
				return false;
			}
			return z.isInfinity;
		}

		/// <summary>
		/// Returns a value indicating whether the specified complex number is finite.
		/// </summary>
		public static bool IsFinite(ComplexNumber z)
		{
			if (z is null)
			{
				return false;
			}
			return !(IsInfinity(z));
		}

		public static ComplexNumber Infinity
		{
			get
			{
				ComplexNumber z = new ComplexNumber();
				z.isInfinity = true;
				return z;
			}
		}
		#endregion

		#region Add

		private static ComplexNumber Add(ComplexNumber z1, ComplexNumber z2)
		{
			if (IsInfinity(z1) || IsInfinity(z2))
			{
				return ComplexNumber.Infinity;
			}
			return ComplexNumber.FromCartesian(z1.Real + z2.Real, z1.Imaginary + z2.Imaginary);
		}

		private static ComplexNumber Add(ComplexNumber z, double d)
		{
			if (IsInfinity(z) || IsInfinity(d))
			{
				return ComplexNumber.Infinity;
			}
			return ComplexNumber.FromCartesian(z.Real + d, z.Imaginary);
		}
		private static ComplexNumber Add(double d, ComplexNumber z)
		{
			return Add(z, d);
		}
		private static ComplexNumber Add(ComplexNumber z, int i)
		{
			return Add(z, (double)i);
		}
		private static ComplexNumber Add(int i, ComplexNumber z)
		{
			return Add(z, i);
		}

		public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
		{
			return Add(z1, z2);
		}
		public static ComplexNumber operator +(ComplexNumber z, double d)
		{
			return Add(z, d);
		}
		public static ComplexNumber operator +(ComplexNumber z, int i)
		{
			return Add(z, i);
		}

		public static ComplexNumber operator +(double d, ComplexNumber z)
		{
			return Add(z, d);
		}
		public static ComplexNumber operator +(int i, ComplexNumber z)
		{
			return Add(z, i);
		}
		#endregion

		#region Subtract

		private static ComplexNumber Subtract(ComplexNumber z1, ComplexNumber z2)
		{
			if (IsInfinity(z1) ^ IsInfinity(z2)) //either is infinity but not both. infinity - infinity is indeterminant
			{
				return ComplexNumber.Infinity;
			}
			if (IsInfinity(z1) && IsInfinity(z2))
			{
				throw new Exception("Indeterminate form, cannot calculate ∞-∞.");
			}
			return ComplexNumber.FromCartesian(z1.Real - z2.Real, z1.Imaginary - z2.Imaginary);
		}

		private static ComplexNumber Subtract(ComplexNumber z, double d)
		{
			return Subtract(z, (ComplexNumber)d);
		}
		private static ComplexNumber Subtract(double d, ComplexNumber z)
		{
			return Subtract(z, d);
		}
		private static ComplexNumber Subtract(ComplexNumber z, int i)
		{
			return Subtract(z, (double)i);
		}
		private static ComplexNumber Subtract(int i, ComplexNumber z)
		{
			return Subtract(z, i);
		}

		public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2)
		{
			return Subtract(z1, z2);
		}
		public static ComplexNumber operator -(ComplexNumber z, double d)
		{
			return Subtract(z, d);
		}
		public static ComplexNumber operator -(ComplexNumber z, int i)
		{
			return Subtract(z, i);
		}

		public static ComplexNumber operator -(double d, ComplexNumber z)
		{
			return Subtract(z, d);
		}
		public static ComplexNumber operator -(int i, ComplexNumber z)
		{
			return Subtract(z, i);
		}
		#endregion

		#region Multiply

		private static ComplexNumber Multiply(ComplexNumber z1, ComplexNumber z2)
		{
			if (IsInfinity(z1) || IsInfinity(z2))
			{
				if (z1 == 0|| z2 == 0)
				{
					throw new Exception("Indeterminate form, cannot calculate ∞*0.");
				}
				return ComplexNumber.Infinity;
			}
			if (z1.data.Item3 == z2.data.Item3 && z1.data.Item3 == ComplexNumberStoreMode.polar) //both are in polar form
			{
				return ComplexNumber.FromPolar(z1.Modulus * z2.Modulus, z1.Argument + z2.Argument);
			}
			return ComplexNumber.FromCartesian(z1.Real * z2.Real - z1.Imaginary * z2.Imaginary, z1.Real * z2.Imaginary + z1.Imaginary * z2.Real);
		}

		private static ComplexNumber Multiply(ComplexNumber z, double d)
		{
			return Multiply(z, (ComplexNumber)d);
		}
		private static ComplexNumber Multiply(double d, ComplexNumber z)
		{
			return Multiply(z, d);
		}
		private static ComplexNumber Multiply(ComplexNumber z, int i)
		{
			return Multiply(z, (double)i);
		}
		private static ComplexNumber Multiply(int i, ComplexNumber z)
		{
			return Multiply(z, i);
		}

		public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
		{
			return Multiply(z1, z2);
		}
		public static ComplexNumber operator *(ComplexNumber z, double d)
		{
			return Multiply(z, d);
		}
		public static ComplexNumber operator *(ComplexNumber z, int i)
		{
			return Multiply(z, i);
		}

		public static ComplexNumber operator *(double d, ComplexNumber z)
		{
			return Multiply(z, d);
		}
		public static ComplexNumber operator *(int i, ComplexNumber z)
		{
			return Multiply(z, i);
		}
		#endregion

		#region Divide

		private static ComplexNumber Divide(ComplexNumber z1, ComplexNumber z2)
		{
			if (z2 == 0) //division by zero
			{
				if (divideByZeroMode == ComplexNumberDivideByZeroMode.throwException)
				{
					throw new DivideByZeroException();
				}
				else
				{
					if (z1 == 0)
					{
						throw new Exception("Indeterminate form: 0/0");
					}
					else
					{
						return ComplexNumber.Infinity;
					}
				}
				
			}
			else if (IsInfinity(z2)) //division by infinity
			{
				if (IsInfinity(z1))
				{
					throw new Exception("Indeterminate form: ∞/∞");
				}
				else
				{
					return 0;
				}

			}
			return (z1 * z2.Conjugate) / z2.ModulusSquared;
		}

		private static ComplexNumber Divide(ComplexNumber z, double d)
		{
			if (d == 0) //division by zero
			{
				if (divideByZeroMode == ComplexNumberDivideByZeroMode.throwException)
				{
					throw new DivideByZeroException();
				}
				else
				{
					if (z == 0)
					{
						throw new Exception("Indeterminate form: 0/0");
					}
					else
					{
						return ComplexNumber.Infinity;
					}
				}

			}
			else if (double.IsInfinity(d)) //division by infinity
			{
				if (IsInfinity(z))
				{
					throw new Exception("Indeterminate form: ∞/∞");
				}
				else
				{
					return 0;
				}

			}
			if (z.data.Item3 == ComplexNumberStoreMode.polar) //is in polar form
			{
				return ComplexNumber.FromPolar(z.Modulus / d, z.Argument);
			}
			return ComplexNumber.FromCartesian(z.Real / d, z.Imaginary / d);
		}
		private static ComplexNumber Divide(double d, ComplexNumber z)
		{
			return Divide(z, d);
		}
		private static ComplexNumber Divide(ComplexNumber z, int i)
		{
			return Divide(z, (double)i);
		}
		private static ComplexNumber Divide(int i, ComplexNumber z)
		{
			return Divide(z, i);
		}

		public static ComplexNumber operator /(ComplexNumber z1, ComplexNumber z2)
		{
			return Divide(z1, z2);
		}

		public static ComplexNumber operator /(ComplexNumber z, double d)
		{
			return Divide(z, d);
		}
		public static ComplexNumber operator /(ComplexNumber z, int i)
		{
			return Divide(z, i);
		}

		public static ComplexNumber operator /(double d, ComplexNumber z)
		{
			return Divide(z, d);
		}
		public static ComplexNumber operator /(int i, ComplexNumber z)
		{
			return Divide(z, i);
		}
		#endregion

		#region Conjugate

		/// <summary>
		/// The complex conjugate of the complex number.
		/// </summary>
		public ComplexNumber Conjugate
		{
			get
			{
				if (isInfinity)
				{
					throw new Exception("Cannot find the conjugate of ∞.");
				}
				if (this.data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return ComplexNumber.FromCartesian(Real, -Imaginary);
				}
				else
				{
					return ComplexNumber.FromCartesian(Modulus, -Argument);
				}
			}
		}
		#endregion

		#region i
		/// <summary>
		/// The complex number i.
		/// </summary>
		public static ComplexNumber i
		{
			get
			{
				return ComplexNumber.FromCartesian(0, 1);
			}
		}
		#endregion

		#region Roots of unity
		/// <summary>
		/// Returns the nth roots of unity.
		/// </summary>
		/// <param name="n"></param>
		/// <returns>Returns a <c>ComplexNumber[]</c> of length <c>n</c> containing the roots of unity.</returns>
		public static ComplexNumber[] RootsOfUnity(int n)
		{
			List<ComplexNumber> roots = new List<ComplexNumber>();

			for (int i = 0; i < n; i++)
			{
				roots.Add(ComplexNumber.FromPolar(1, 2 * Math.PI / n));
			}

			return roots.ToArray();
		}
		#endregion

		#region Gaussian integers
		public bool IsGaussianInteger()
		{
			if (isInfinity)
			{
				return false;
			}
			return (Real % 1 == 0 && Imaginary % 1 == 0);
		}

		public ComplexNumber NearestGaussianInteger()
		{
			if (isInfinity)
			{
				throw new Exception("Cannot find nearest Gaussian integer to ∞.");
			}
			return ComplexNumber.FromCartesian(Math.Round(Real), Math.Round(Imaginary));
		}
		#endregion
	}

	internal enum ComplexNumberStoreMode
	{
		cartesian, polar
	}

	public enum ComplexNumberDivideByZeroMode
	{
		throwException, infinity
	}
}