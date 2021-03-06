﻿#region Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit
/*
Kohd & Art Toolkit - A toolkit of general classes/methods for .NET and C#

Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the 
following conditions:

The above copyright notice and this permission notice shall be included in 
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
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
