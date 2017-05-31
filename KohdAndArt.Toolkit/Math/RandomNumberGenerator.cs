using System;

namespace KohdAndArt.Toolkit
{
    public class RandomNumberGenerator
    {
        int _min, _max;
        Random _random = new Random();
        public RandomNumberGenerator(int min, int max)
        {
            this._min = min;
            this._max = max;
        }

        public int Generate()
        {
            int i = -1;
            while (i < _min || i > _max) {
                i = _random.Next();
            }
            return i;
        }
    }
}
