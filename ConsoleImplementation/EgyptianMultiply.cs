using System;

namespace ConsoleImplementation
{
    public class EgyptianMultiply
    {
        public int Multiply(int n, int a)
        {
            if (n == 1) return a;
            int result = Multiply(Half(n), a + a);
            if (Odd(n)) result = result + a;
            return result;
        }

        private bool Odd(int n)
        {
            return Convert.ToBoolean(n & 1);
        }

        private int Half(int n)
        {
            return n >> 1;
        }
    }
}
