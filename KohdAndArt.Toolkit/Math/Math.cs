namespace KohdAndArt.Toolkit.Math
{
    public static class Math
    {
        private const double GravitationalConstant = 6.6740831E-11;

        public static double CalcHypotenuse(int x, int y)
        { 
            var z = System.Math.Sqrt(x*x + y*y);
            return z;
        }

        public static double CalcGravity(double massKg1, double massKg2, double radius)
        {
            var force = GravitationalConstant * ((massKg1 * massKg2) / radius);
            return force;
        }

        public static double DegreeToRadian(double angle)
        {
            return System.Math.PI * angle / 180.0;
        }
    }
}
