using System;
using System.Linq;

namespace KohdAndArt.Toolkit.Collections
{
    /// <summary>
    /// Generic version of Stack class
    /// </summary>
    public class Stack<T> 
    {
        #region Constants
        private const int DefaultInitialCapacity = 10;
        private const int DefaultCapacityIncrease = 10;
        #endregion

        #region Error Messages
        private const string ErrorStackEmpty = "The stack is empty.";
        private const string ErrorCapacityExceeded = "Size of stack exceeds it's capacity.";
        #endregion

        #region Private Member Variables
        private T[] _stackArray;
        #endregion

        #region Properties
        /// <summary>
        /// Track the number of entries in the stack
        /// </summary>
        private int _size;        
        public int Count { get { return _size; } private set { } }

        /// <summary>
        /// Track the overall size of the internal array 
        /// This is NOT the same thing as Count.
        /// </summary>
        public int Capacity { get { return _stackArray.Length; } }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initialCapacity"></param>
        public Stack(int initialCapacity = DefaultInitialCapacity)
        {
            this._stackArray = new T[initialCapacity];
            this._size = 0;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Does the stack contain the target value?
        /// </summary>
        /// <param name="val"></param>
        public bool Contains(T val)
        {
            bool rval = false;
            for (int i = 0; i < this._size; i++)
            {
                if (_stackArray[i].Equals(val))
                {
                    rval = true;
                    break;
                }
            }
            return rval;
        }

        /// <summary>
        /// Push a new value onto the stack
        /// </summary>
        /// <param name="val"></param>
        public void Push(T val)
        {
            // Get the current and size
            int capacity = Capacity;
            int size = this._size;

            if (this._size < Capacity)
            {
                // Just assign the value.
                _stackArray[this._size++] = val;
            }
            else if (this._size == Capacity)
            {
                // Allocate more space and assign value
                Resize(Capacity + DefaultCapacityIncrease);
                _stackArray[this._size++] = val;
            }
            else if (this._size > Capacity)
            {
                throw new ArgumentOutOfRangeException(ErrorCapacityExceeded);
            }
        }

        /// <summary>
        /// Pop the current object off the stack
        /// </summary>
        /// <returns>T</returns>
        public T Pop()
        {
            if (this._size == 0) {  throw new InvalidOperationException(ErrorStackEmpty); }

            // Get the current value at top of stack 
            // and put a null value in it's place
            T currentVal = Peek();
            _stackArray[this._size - 1] = default(T);

            // Reduce the size of the stack by one.
            this._size--;

            return currentVal;
        }

        /// <summary>
        /// Empty out the stack
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Capacity; i++)
            {
                this._stackArray[i] = default(T); 
            }
            this._size = 0;
        }

        /// <summary>
        /// Determine the value currently at the top of the stack
        /// without Popping it.
        /// </summary>
        /// <returns>T</returns>
        public T Peek()
        {
            if (this._size == 0) { throw new InvalidOperationException(ErrorStackEmpty); }
             
            return _stackArray[this._size - 1];
        }

        /// <summary>
        /// Display the current stack contents
        /// </summary>
        public void Display()
        {
            if (this._size > 0)
            {
                for (int i = this._size - 1; i >= 0; i--)
                {
                    System.Console.WriteLine($"[{i}] = {_stackArray[i].ToString()}");
                }
            }
            else
            {
                System.Console.WriteLine(ErrorStackEmpty);
            }
        }

        /// <summary>
        /// Create an identical copy of the existing stack
        /// </summary>
        /// <returns>A cloned copy of the current stack</returns>
        public Stack<T> Clone()
        {
            // Get the capacity of the current stack
            int size = this._size;
            var s = new Stack<T>(size);
            Array.Copy(this._stackArray, s._stackArray, size);
            return s;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Increase the size of the stack
        /// </summary>
        /// <param name="newCapacity">The new capacity for stack</param>
        private void Resize(int newCapacity)
        {
            if (newCapacity > Capacity)
            {
                Array.Resize<T>(ref _stackArray, newCapacity);

                //// New requested capacity exceeds current capacity.
                //// We need to allocate more space and copy
                //// data over to newly allocated space

                //// Duplicate the current array
                //var temp = new T[Capacity];
                //temp = _stackArray.ToArray<T>();

                //// Create a new, larger array
                //var newTemp = new T[newCapacity];
                //_stackArray = newTemp;

                //// Copy over original data to new array
                //Array.Copy(temp, _stackArray, temp.Length);
            }

            // No need to shrink stack.
        }
        #endregion
    }
}
