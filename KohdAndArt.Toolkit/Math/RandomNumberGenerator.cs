#region Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit
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
using System.Collections.Generic;
using System.Security.Cryptography;

namespace KohdAndArt.Toolkit
{
    public class RandomNumberGenerator
    {
        int _min, _max;
        Random _random = new Random();
        RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        /// <summary>
        /// RandomNumberGenerator constructor
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public RandomNumberGenerator(int min, int max)
        {
            this._min = min;
            this._max = max;
        }

        public int Generate()
        {
            var bytes = new byte[8];
            rand.GetBytes(bytes);
            var randomInt = BitConverter.ToUInt32(bytes, 0);
            var randomFloat = randomInt / ((float)(1.0 + UInt32.MaxValue));
            var randomInRange = (int)(_min + (randomFloat * (_max - _min)));
            return randomInRange;
        }

        /// <summary>
        /// Generate a list of random numbers, uniqueness is not guaranteed.  Default list size = 1
        /// </summary>
        /// <param name="size"></param>
        /// <param name="_unique"></param>
        /// <returns></returns>
        public IEnumerable<int> GenerateList(int size = 1, bool _unique = false)
        {
            var theList = new List<int>();
            int num = 0;
            for (int x = 0; x < size; x++)
            {
                num = Generate();
                if (!theList.Contains(num))
                {
                    theList.Add(num);
                }
                else
                {
                    while (theList.Contains(num))
                    {
                        num = Generate();
                    }
                    theList.Add(num);
                }
            }
            return theList;
        }

        /// <summary>
        /// Generate a list of random unique integers. The default list size is 1
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<int> GenerateListUnique(int size = 1)
        {
            return GenerateList(size, _unique: true);
        }
    }
}
