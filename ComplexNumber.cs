namespace ComplexNumbers
{
	public class ComplexNumber
	{
		private (double, double, ComplexNumberStoreMode) data;

		#region Initialise complex numbers
		public static ComplexNumber FromCartesian(double real, double imaginary)
		{
			ComplexNumber z = new ComplexNumber();

			z.data = (real, imaginary, ComplexNumberStoreMode.cartesian);

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


			return z;
		}

		public ComplexNumber()
		{
		}


		public ComplexNumber(double d)
		{
			data = (d, 0, ComplexNumberStoreMode.cartesian);
		}

		public ComplexNumber(int i)
		{
			data = (i, 0, ComplexNumberStoreMode.cartesian);
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
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return data.Item1;
				}
				else
				{
					return Modulus * Math.Cos(Imaginary);
				}
			}

		}

		public double Imaginary
		{
			get
			{
				if (data.Item3 == ComplexNumberStoreMode.cartesian)
				{
					return data.Item2;
				}
				else
				{
					return Real * Math.Sin(Imaginary);
				}
			}

		}
		#endregion

		#region Modulus and argument
		public double Modulus
		{
			get
			{
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
			return z.Real == d && z.Imaginary == 0;
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
			return z.Real == i && z.Imaginary == 0;
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

		#region Add

		private static ComplexNumber Add(ComplexNumber z1, ComplexNumber z2)
		{
			return ComplexNumber.FromCartesian(z1.Real + z2.Real, z1.Imaginary + z2.Imaginary);
		}

		private static ComplexNumber Add(ComplexNumber z, double d)
		{
			return ComplexNumber.FromCartesian(z.Real + d, z.Imaginary);
		}
		private static ComplexNumber Add(double d, ComplexNumber z)
		{
			return Add(z, d);
		}
		private static ComplexNumber Add(ComplexNumber z, int i)
		{
			return Subtract(z, (double)i);
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
			return ComplexNumber.FromCartesian(z1.Real - z2.Real, z1.Imaginary - z2.Imaginary);
		}

		private static ComplexNumber Subtract(ComplexNumber z, double d)
		{
			return ComplexNumber.FromCartesian(z.Real - d, z.Imaginary);
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
			if (z1.data.Item3 == z2.data.Item3 && z1.data.Item3 == ComplexNumberStoreMode.polar) //both are in polar form
			{
				return ComplexNumber.FromPolar(z1.Modulus * z2.Modulus, z1.Argument + z2.Argument);
			}
			return ComplexNumber.FromCartesian(z1.Real * z2.Real - z1.Imaginary * z2.Imaginary, z1.Real * z2.Imaginary + z1.Imaginary * z2.Real);
		}

		private static ComplexNumber Multiply(ComplexNumber z, double d)
		{
			if (z.data.Item3 == ComplexNumberStoreMode.polar) //is in polar form
			{
				return ComplexNumber.FromPolar(z.Modulus * d, z.Argument);
			}
			return ComplexNumber.FromCartesian(z.Real * d, z.Imaginary * d);
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
			if (z2 == 0) //both are in polar form
			{
				throw new DivideByZeroException();
			}
			return (z1 * z2.Conjugate) / z2.ModulusSquared;
		}

		private static ComplexNumber Divide(ComplexNumber z, double d)
		{
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

		public ComplexNumber Conjugate
		{
			get
			{
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
	}

	internal enum ComplexNumberStoreMode
	{
		cartesian, polar
	}
}