using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexNumbers.Algebra;

namespace ComplexNumbers.Geometry.Mobius_Transformations
{
	internal class MobiusTransformation

	{

		private ComplexNumber a;
		private ComplexNumber b;
		private ComplexNumber c;
		private ComplexNumber d;

		/// <summary>
		/// Initialise a Möbius transformation f(z)=(az+b)/(cz+d).
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="c"></param>
		/// <param name="d"></param>
		public MobiusTransformation(ComplexNumber a, ComplexNumber b, ComplexNumber c, ComplexNumber d)
		{
			if (a == ComplexNumber.Infinity || b == ComplexNumber.Infinity || c == ComplexNumber.Infinity || d == ComplexNumber.Infinity)
			{
				throw new Exception("Cannot create a Möbius transformation with an infinite coefficient.");
			}
			ComplexNumber determinant = a * d - b * c;

			if (determinant == 0)
			{
				throw new Exception("A Möbius transformation must satisfy ad-bc≠0.");
			}
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		/// <summary>
		/// Returns the value of the Möbius transformation at <c>z</c>.
		/// </summary>
		/// <param name="z">The complex number to evaluate the Möbius transformation at.</param>
		/// <returns></returns>
		public ComplexNumber valueAt(ComplexNumber z)
		{
			if (c*z+d == 0)
			{
				return ComplexNumber.Infinity;
			}
			if (ComplexNumber.IsInfinity(z) && c == 0)
			{
				return ComplexNumber.Infinity;
			}
			else if (ComplexNumber.IsInfinity(z))
			{
				return a / c;
			}
			return (a * z + b) / (c * z + d);
		}

		/// <summary>
		/// Returns the fixed points of the Möbius transformation (with multiplicity)
		/// </summary>
		/// <returns>A list of 2 (potentially non-unique) complex numbers fixed by the Möbius transformation.</returns>
		public ComplexNumber[] FixedPoints()
		{
			if (c != 0)
			{
				return ComplexNumberAlgebra.RootsOfQuadraticEquation(c, d - a, -1*b);
			}
			else
			{
				if (a == d)
				{
					return new ComplexNumber[] { ComplexNumber.Infinity, ComplexNumber.Infinity };
				}
				else
				{
					return new ComplexNumber[] { b/(d-a), ComplexNumber.Infinity };
				}
			}
		}

		/// <summary>
		/// The inverse of the Möbius transformation (which is itself a Möbius transformation.)
		/// </summary>
		public MobiusTransformation inverse
		{
			get
			{
				return new MobiusTransformation(d, -1 * b, -1 * c, a);
			}
		}


	}
}
