using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kohd = KohdAndArt.Toolkit;

namespace KohdAndArt.Toolkit.Tests
{
    [TestClass]
    public class Math
    {
        [TestMethod]
        public void DegreeToRadian_0Degreesf_Equals_0Radians()
        {
            // Arrange
            var degree = 0;
            var radian = 0D;

            // Act
            radian = Kohd.Math.Math.DegreeToRadian(degree);

            // Assert
            Assert.AreEqual(radian, 0);
        }

        [TestMethod]
        public void CalcHypotenuse_2by2()
        {
            // Arrange
            double delta =    0.00000000000001D;
            double expected = 2.82842712474619D;
            var x = 2;
            var y = 2;
            double result;

            // Act
            result = Kohd.Math.Math.CalcHypotenuse(x, y);

            // Assert
            Assert.AreEqual(result, expected, delta);
        }

        [TestMethod]
        public void CalcGravity_10kg_10kg_10m()
        {
            // Arrange
            double delta = 0.00000000000001D;
            double expected = 6.6740831E-10;
            var mass1 = 10;
            var mass2 = 10;
            var radius = 10;
            double result;

            // Act
            result = Kohd.Math.Math.CalcGravity(mass1, mass2, radius);

            // Assert
            Assert.AreEqual(result, expected, delta);
        } 
    }
}
