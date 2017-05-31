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
