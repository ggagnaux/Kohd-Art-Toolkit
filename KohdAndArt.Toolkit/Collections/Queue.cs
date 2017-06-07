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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KohdAndArt.Toolkit.Collections
{
    /// <summary>
    /// Generic Version of Queue class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>
    {
        #region Constants
        private const int DefaultInitialCapacity = 10;
        private const int DefaultCapacityIncrease = 10;
        #endregion

        #region Error Messages
        private const string ErrorQueueEmpty = "The queue is empty.";
        private const string ErrorCapacityExceeded = "Size of queue exceeds it's capacity.";
        #endregion

        #region Private Member Variables
        private T[] _queueArray;
        #endregion

        #region Properties
        /// <summary>
        /// Track the number of entries in the stack
        /// </summary>
        /// 
        private int _size { get; set; }
        public int Count { get { return this._size; } private set { } }

        /// <summary>
        /// Track the overall size of the internal array 
        /// This is NOT the same thing as size.
        /// </summary>
        public int Capacity { get { return _queueArray.Length; } }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initialSize"></param>
        public Queue(int initialCapacity = DefaultInitialCapacity)
        {
            this._queueArray = new T[initialCapacity];
            this._size = 0;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a new object to the queue
        /// </summary>
        /// <param name="val">the object to add</param>
        public void Enqueue(T val)
        {
            // Get the current and size
            int capacity = Capacity;
            int size = this._size;

            if (size < capacity)
            {
                // Just assign the value.
                _queueArray[this._size++] = val;
            }
            else if (size == capacity)
            {
                // Allocate more space and assign value
                Resize(Capacity + DefaultCapacityIncrease);
                _queueArray[this._size++] = val;
            }
            else if (size > capacity)
            {
                throw new ArgumentOutOfRangeException(ErrorCapacityExceeded);
            }
        }

        /// <summary>
        /// Get the next value out of the queue
        /// </summary>
        /// <returns>T</returns>
        public T Dequeue()
        {
            if (this._size == 0) { throw new InvalidOperationException(ErrorQueueEmpty); }

            // Get the next value to remove from queue
            T currentVal = Peek();
            this._size--;

            // Now, shift all remaining values in array down by one slot
            for (int i = 1; i <= this._size; i++)
            {
                _queueArray[i - 1] = _queueArray[i];
            }

            return currentVal;
        }

        /// <summary>
        /// Empty out the queue
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Capacity; i++)
            {
                this._queueArray[i] = default(T);
            }
            this._size = 0;
        }

        /// <summary>
        /// Determine the next value to be read from the queue
        /// without dequeuing it.
        /// </summary>
        /// <returns>T</returns>
        public T Peek()
        {
            if (this._size == 0) { throw new InvalidOperationException(ErrorQueueEmpty); }

            return _queueArray[0];
        }

        /// <summary>
        /// Display the current queue contents
        /// </summary>
        public void Display()
        {
            if (this._size > 0)
            {
                for (int i = 0; i < this._size; i++)
                {
                    System.Console.WriteLine($"[{i}] = {_queueArray[i].ToString()}");
                }
            }
            else
            {
                System.Console.WriteLine(ErrorQueueEmpty);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Increase the size of the queue
        /// </summary>
        /// <param name="newCapacity">The new capacity for queue</param>
        private void Resize(int newCapacity)
        {
            if (newCapacity > Capacity)
            {
                Array.Resize<T>(ref _queueArray, newCapacity);

                //// New requested capacity exceeds current capacity.
                //// We need to allocate more space and copy
                //// data over to newly allocated space

                //// Duplicate the current array
                //var temp = new T[Capacity];
                //temp = _queueArray.ToArray<T>();

                //// Create a new, larger array
                //var newTemp = new T[newCapacity];
                //_queueArray = newTemp;

                //// Copy over original data to new array
                //for (int i = 0; i < temp.Length; i++)
                //{
                //    _queueArray[i] = temp[i];
                //}
            }

            // No need to shrink queue.
        }

        


        #endregion
    }

}
