using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers
{
	public class Quarternion
	{

		private double realPart;
		private double iPart;
		private double jPart;
		private double kPart;

		public Quarternion(double realPart, double iPart, double jPart, double kPart)
		{
			this.realPart = realPart;
			this.iPart = iPart;
			this.jPart = jPart;
			this.kPart = kPart;
		}
		public Quarternion(ComplexNumber z)
		{
			this.realPart = z.Real;
			this.iPart = z.Imaginary;
			this.jPart = 0;
			this.kPart = 0;
		}
		public Quarternion(double realPart)
		{
			this.realPart = realPart;
			this.iPart = 0;
			this.jPart = 0;
			this.kPart = 0;
		}
		public Quarternion(int realPart)
		{
			this.realPart = realPart;
			this.iPart = 0;
			this.jPart = 0;
			this.kPart = 0;
		}

		public static implicit operator Quarternion(int i) => new Quarternion(i);
		public static implicit operator Quarternion(double d) => new Quarternion(d);
		public static implicit operator Quarternion(ComplexNumber z) => new Quarternion(z);

		#region Real, I, J, K parts

		public double Real
		{
			get
			{
				return realPart;
			}
		}

		public double I
		{
			get
			{
				return iPart;
			}
		}

		public double J
		{
			get
			{
				return jPart;
			}
		}

		public double K
		{
			get
			{
				return kPart;
			}
		}
		#endregion

		#region Add
		private static Quarternion Add(Quarternion q1, Quarternion q2)
		{
			return new Quarternion(q1.Real + q2.Real, q1.I + q2.I, q1.J + q2.J, q1.K + q2.K);
		}
		public static Quarternion operator +(Quarternion q1, Quarternion q2)
		{
			return Add(q1, q2);
		}
		#endregion

		#region Subtract
		private static Quarternion Subtract(Quarternion q1, Quarternion q2)
		{
			return new Quarternion(q1.Real - q2.Real, q1.I - q2.I, q1.J - q2.J, q1.K - q2.K);
		}
		public static Quarternion operator -(Quarternion q1, Quarternion q2)
		{
			return Subtract(q1, q2);
		}
		#endregion

		#region Multiply
		private static Quarternion Multiply(Quarternion q1, Quarternion q2)
		{
			double realPart = q1.Real * q2.Real - q1.I * q2.I - q1.J * q2.J - q1.K * q2.K;
			double iPart = q1.Real * q2.I - q1.I * q2.Real - q1.J * q2.K - q1.K * q2.J;
			double jPart = q1.Real * q2.J - q1.I * q2.K - q1.J * q2.Real - q1.K * q2.I;
			double kPart = q1.Real * q2.K - q1.I * q2.J - q1.J * q2.I - q1.K * q2.Real;

			return new Quarternion(realPart, iPart, jPart, kPart);
		}
		public static Quarternion operator *(Quarternion q1, Quarternion q2)
		{
			return Multiply(q1, q2);
		}

		private static Quarternion Multiply(Quarternion q1, int q2)
		{
			return q1 * new Quarternion(q2);
		}
		public static Quarternion operator *(Quarternion q1, int q2)
		{
			return Multiply(q1, q2);
		}

		private static Quarternion Multiply(int q1, Quarternion q2)
		{
			return new Quarternion(q1) * q2;
		}
		public static Quarternion operator *(int q1, Quarternion q2)
		{
			return Multiply(q1, q2);
		}
		#endregion

		#region Conjugate

		public Quarternion Conjugate
		{
			get
			{
				return new Quarternion(Real, -I, -J, -K);
			}
		}
		#endregion

		#region Norm
		public double NormSquared
		{
			get
			{
				return Real * Real + I * I + J * J + K * K;
			}
		}

		public double Norm
		{
			get
			{
				return Math.Sqrt(NormSquared);
			}
		}
		#endregion

		#region ToString()

		public override string ToString()
		{
			if (this == 0)
			{
				return "0";
			}
			if (K == 0)
			{
				if (J == 0)
				{
					if (I == 0)
					{
						if (Real == 0)
						{
							return "";
						}
						else
						{
							return Real.ToString();
						}
						
					}
					else
					{
						string iString = NumberStringPart(I);

						return new Quarternion(Real, 0, 0, 0).ToString() + iString + "i";
					}
				}
				else
				{
					string jString = NumberStringPart(J);

					return new Quarternion(Real, I, 0, 0).ToString() + jString + "j";
				}
			}
			else
			{
				string kString = NumberStringPart(K);

				return new Quarternion(Real, I, J, 0).ToString() + kString + "k";
			}
		}

		private string NumberStringPart(double num)
		{
			string str = num.ToString();

			if (num > 0)
			{
				str = "+" + str;
			}
			if (num == 1)
			{
				str = "+";
			}
			else if (num == -1)
			{
				str = "-";
			}

			return str;
		}

		#endregion

		#region Equality

		public override bool Equals(object? obj)
		{
			if (obj is null)
			{
				return this is null;
			}
			else if (obj is Quarternion)
			{
				return (Quarternion)obj == this;
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

		public static bool operator ==(Quarternion q1, Quarternion q2)
		{
			return ((q1.Real == q2.Real) && (q1.I == q2.I) && (q1.J == q2.J) && (q1.K == q2.K));
		}

		public static bool operator !=(Quarternion q1, Quarternion q2)
		{
			return (q1 == q2 == false);
		}

		public static bool operator ==(Quarternion q, ComplexNumber z)
		{
			return (q.Real == z.Real && q.I == z.Imaginary && q.J == 0 && q.K == 0);
		}
		public static bool operator !=(Quarternion q, ComplexNumber z)
		{
			return (q == z == false);
		}

		public static bool operator ==(ComplexNumber z, Quarternion q)
		{
			return (q == z);
		}
		public static bool operator !=(ComplexNumber z, Quarternion q)
		{
			return (q != z);
		}

		public static bool operator ==(Quarternion z, double d)
		{
			return (z.Real == d && z.I == 0 && z.J == 0 && z.K == 0);
		}
		public static bool operator !=(Quarternion z, double d)
		{
			return (z == d == false);
		}
		public static bool operator ==(double d, Quarternion z)
		{
			return (z == d);
		}
		public static bool operator !=(double d, Quarternion z)
		{
			return (z != d);
		}
		public static bool operator ==(Quarternion z, int i)
		{
			return (z.Real == i && z.I == 0 && z.J == 0 && z.K == 0);
		}
		public static bool operator !=(Quarternion z, int i)
		{
			return (z == i == false);
		}
		public static bool operator ==(int i, Quarternion z)
		{
			return (z == i);
		}
		public static bool operator !=(int i, Quarternion z)
		{
			return (z != i);
		}
		#endregion

		#region Standard quarternions

		public static Quarternion i
		{
			get
			{
				return new Quarternion(0, 1, 0, 0);
			}
		}
		public static Quarternion j
		{
			get
			{
				return new Quarternion(0, 0, 1, 0);
			}
		}
		public static Quarternion k
		{
			get
			{
				return new Quarternion(0, 0, 0, 1);
			}
		}
		#endregion
	}
}
