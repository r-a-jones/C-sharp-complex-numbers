using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers.Geometry.Riemann_Sphere
{
	public static class ComplexNumberRiemannSphereGeometry
	{
		public static (double, double, double) ToRiemannSphere(ComplexNumber z)
		{
			double projectDenominator = 1 + z.Real * z.Real + z.Imaginary * z.Imaginary;
			double sphereX = 2 * z.Real / projectDenominator;
			double sphereY = 2 * z.Imaginary / projectDenominator;
			double sphereZ = (-1 + z.Real * z.Real + z.Imaginary * z.Imaginary) / projectDenominator;

			return (sphereX, sphereY, sphereZ);
		}
		public static (double, double, double) ToRiemannSphere(ComplexNumber z, RiemannSpherePole poleToProjectInfinityTo)
		{
			switch (poleToProjectInfinityTo)
			{
				case RiemannSpherePole.north:
					return ToRiemannSphere(z);
				case RiemannSpherePole.south:
					return ToRiemannSphere(1 / z.Conjugate);
				default:
					throw new Exception("Could not parse RiemannSpherePole" + poleToProjectInfinityTo.ToString() + ".");
			}
		}

		public enum RiemannSpherePole
		{
			north, south
		}
	}
}
