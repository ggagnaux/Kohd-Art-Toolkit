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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kohd = KohdAndArt.Toolkit;

namespace KohdAndArt.Toolkit.Tests.Collections
{
    [TestClass]
    public class Stack
    {
        [TestMethod]
        public void Stack_PushMultiple_Count()
        {
            // Arrange
            var stack = new Kohd.Collections.Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(20);
            stack.Push(35);
            int size = stack.Count;

            // Assert
            Assert.IsTrue(size == 3);
        }

        [TestMethod]
        public void Stack_PushMultiple_PopSingle_Count()
        {
            // Arrange
            var stack = new Kohd.Collections.Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(20);
            stack.Push(35);
            int a = stack.Pop();

            int size = stack.Count;

            // Assert
            Assert.IsTrue(size == 2);
        }

        [TestMethod]
        public void Stack_PushMultiple_PopSingle_GetValue()
        {
            // Arrange
            var stack = new Kohd.Collections.Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(20);
            stack.Push(35);
            int a = stack.Pop();

            // Assert
            Assert.IsTrue(a == 35);
        }

        [TestMethod]
        public void Stack_PushMultiple_PopMultiple_GetValue()
        {
            // Arrange
            var stack = new Kohd.Collections.Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(20);
            stack.Push(35);

            // Pop twice
            int actualValue = stack.Pop();
            actualValue = stack.Pop();
            int expectedValue = 20;

            // Assert
            Assert.IsTrue(actualValue == expectedValue);
        }

        [TestMethod]
        public void Stack_PushMultiple_Clear()
        {
            // Arrange
            var stack = new Kohd.Collections.Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(20);
            stack.Push(35);
            stack.Clear();
            int size = stack.Count;

            // Assert
            Assert.IsTrue(size == 0);
        }

        [TestMethod]
        public void Stack_PushMultiple_Peek()
        {
            // Arrange
            var stack = new Kohd.Collections.Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(20);
            stack.Push(35);

            int actualValue = stack.Peek();
            int expectedValue = 35;

            int actualSize = stack.Count;
            int expectedSize = 3;

            // Assert
            Assert.IsTrue(actualValue == expectedValue && 
                          actualSize == expectedSize);
        }
    }
}
