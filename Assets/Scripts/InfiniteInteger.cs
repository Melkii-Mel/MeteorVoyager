using System;

namespace MeteorVoyager.Assets.Scripts
{
    [Serializable]
    public struct InfiniteInteger
    {
        public static readonly InfiniteInteger Zero = 0;
        public static readonly InfiniteInteger One = 1;
        public static readonly InfiniteInteger Ten = 10;
        public static readonly InfiniteInteger Hundred = 100;
        public static readonly InfiniteInteger Thousand = 1000;
        public static readonly InfiniteInteger Million = 1000000;
        public static readonly InfiniteInteger Billion = 1000000000;
        public static readonly InfiniteInteger Trillion = Billion * 1000;
        enum Operators
        {
            Plus,
            Minus,
            Multiplication,
            Division,
        }

        public InfiniteInteger(double value)
        {
            _base = value;
            _exponent = 0;
            Compress(ref this);
        }

        public InfiniteInteger(double @base = 0, int exponent = 0)
        {
            _base = @base;
            _exponent = exponent;
        }

        #region fields that represents a number
        private double _base;
        private float _exponent;
        #endregion

        public static implicit operator InfiniteInteger(int value)
        {
            return new InfiniteInteger(value);
        }
        public static explicit operator double(InfiniteInteger value)
        {
            return value._base * MathF.Pow(10, value._exponent);
        }
        public static InfiniteInteger operator +(InfiniteInteger a, int b)
        {
            return PerformArithmeticOperation(a, (float)b, Operators.Plus);
        }
        public static InfiniteInteger operator +(InfiniteInteger a, double b)
        {
            return PerformArithmeticOperation(a, b, Operators.Plus);
        }
        public static InfiniteInteger operator +(InfiniteInteger a, InfiniteInteger b)
        {
            return PerformArithmeticOperation(a, b, Operators.Plus);
        }
        public static InfiniteInteger operator -(InfiniteInteger a, int b)
        {
            return PerformArithmeticOperation(a, (double)b, Operators.Minus);
        }
        public static InfiniteInteger operator -(InfiniteInteger a, double b)
        {
            return PerformArithmeticOperation(a, b, Operators.Minus);
        }
        public static InfiniteInteger operator -(InfiniteInteger a, InfiniteInteger b)
        {
            return PerformArithmeticOperation(a, b, Operators.Minus);
        }
        public static InfiniteInteger operator *(InfiniteInteger a, int b)
        {
            return PerformArithmeticOperation(a, (double)b, Operators.Multiplication);
        }
        public static InfiniteInteger operator *(InfiniteInteger a, double b)
        {
            return PerformArithmeticOperation(a, b, Operators.Multiplication);
        }
        public static InfiniteInteger operator *(InfiniteInteger a, InfiniteInteger b)
        {
            return PerformArithmeticOperation(a, b, Operators.Multiplication);
        }
        public static InfiniteInteger operator /(InfiniteInteger a, int b)
        {
            return PerformArithmeticOperation(a, (double)b, Operators.Division);
        }
        public static InfiniteInteger operator /(InfiniteInteger a, double b)
        {
            return PerformArithmeticOperation(a, b, Operators.Division);
        }
        public static InfiniteInteger operator /(InfiniteInteger a, InfiniteInteger b)
        {
            return PerformArithmeticOperation(a, b, Operators.Division);
        }
        public double GetBase()
        {
            return _base;
        }

        public int GetExponent()
        {
            return (int)_exponent;
        }

        public static bool operator >(InfiniteInteger a, InfiniteInteger b)
        {
            if (a._exponent < b._exponent) return false;
            if (a._exponent > b._exponent) return true;
            return a._base > b._base;
        }
        public static bool operator <(InfiniteInteger a, InfiniteInteger b)
        {
            if (a._exponent < b._exponent) return true;
            if (a._exponent > b._exponent) return false;
            return a._base < b._base;
        }
        public static bool operator >=(InfiniteInteger a, InfiniteInteger b)
        {
            if (a._exponent > b._exponent) return true;
            if (a._exponent < b._exponent) return false;
            return a._base >= b._base;
        }
        public static bool operator <=(InfiniteInteger a, InfiniteInteger b)
        {
            if (a._exponent < b._exponent) return true;
            if (a._exponent > b._exponent) return false;
            return a._base <= b._base;
        }
        public static bool operator ==(InfiniteInteger a, InfiniteInteger b)
        {
            return a._exponent == b._exponent && a._base == b._base;
        }
        public static bool operator !=(InfiniteInteger a, InfiniteInteger b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Note this operation is inaccurate after the 9th digit in the decimal representation
        /// </summary>
        public static bool operator ==(InfiniteInteger a, double b)
        {
            const double TOLERANCE = 0.00000001;
            const double DENOMINATOR = 1000000000;
            return Math.Abs((double)a / DENOMINATOR - b / DENOMINATOR) <= TOLERANCE;
        }
        public static bool operator !=(InfiniteInteger a, double b)
        {
            return !(a == b);
        }
        public static bool operator ==(InfiniteInteger a, int b)
        {
            InfiniteInteger iib = b;
            return a == iib;
        }
        public static bool operator !=(InfiniteInteger a, int b)
        {
            return !(a == b);
        }
        private static InfiniteInteger PerformArithmeticOperation(InfiniteInteger a, double b, Operators @operator)
        {
            double normalizedB;
            switch (@operator)
            {
                case Operators.Plus:
                    normalizedB = b / Math.Pow(10, a._exponent);
                    a._base += normalizedB; break;
                case Operators.Minus:
                    normalizedB = b / Math.Pow(10, a._exponent);
                    a._base -= normalizedB; break;
                case Operators.Multiplication:
                    normalizedB = b;
                    a._base *= normalizedB; break;
                case Operators.Division:
                    normalizedB = b;
                    a._base /= normalizedB; break;
            }
            Compress(ref a);
            return a;
        }
        private static InfiniteInteger PerformArithmeticOperation(InfiniteInteger a, InfiniteInteger b, Operators @operator)
        {
            void LocalCompress()
            {
                if (a > b)
                {
                    Compress(ref b, (int)a._exponent);
                }
                else if (a < b)
                {
                    Compress(ref a, (int)b._exponent);
                }
            }
            switch (@operator)
            {
                case Operators.Plus:
                    LocalCompress();
                    a._base += b._base; break;
                case Operators.Minus:
                    LocalCompress();
                    a._base -= b._base; break;
                case Operators.Multiplication:
                    a._base *= b._base; a._exponent += b._exponent; break;
                case Operators.Division:
                    a._base /= b._base; a._exponent -= b._exponent; break;
            }
            Compress(ref a);
            return a;
        }

        public static InfiniteInteger Pow(InfiniteInteger a, float power)
        {
            a._base = Math.Pow(a._base, power);
            a._exponent *= power;
            Compress(ref a);
            return a;
        }

        public InfiniteInteger Pow(float power)
        {
            _base = Math.Pow(_base, power);
            _exponent *= power;
            Compress(ref this);
            return this;
        }

        private static InfiniteInteger Compress(ref InfiniteInteger ii)
        {
            if (ii._base == 0)
            {
                ii._exponent = 0;
                return ii;
            }
            double absBase = Math.Abs(ii._base);
            while (absBase >= 10)
            {
                ii._base /= 10;
                absBase /= 10;
                ii._exponent++;
            }
            while (absBase < 1)
            {
                ii._base *= 10;
                absBase *= 10;
                ii._exponent--;
            }
            while (ii._exponent < 0)
            {
                ii._exponent++;
                absBase /= 10;
                ii._base /= 10;
            }
            ii._base = Math.Round(ii._base, 9);
            float mod = ii._exponent % 1;
            if (Math.Abs(ii._base) == 10)
            {
                ii._exponent++;
                ii._base = 1;
            }
            if (mod != 0)
            {
                ii._base *= MathF.Pow(10, mod);
                ii._exponent -= mod;
                Compress(ref ii);
            }
            return ii;
        }

        private static InfiniteInteger Compress(ref InfiniteInteger ii, int exponent)
        {
            while (ii._exponent < exponent)
            {
                ii._exponent++;
                ii._base /= 10;
            }
            while (ii._exponent > exponent)
            {
                ii._exponent--;
                ii._base *= 10;
            }
            return ii;
        }
#nullable enable
        public override bool Equals(object? obj)
        {
            return obj is InfiniteInteger integer &&
                    _base == integer._base &&
                    _exponent == integer._exponent;
        }
#nullable disable
        public override int GetHashCode()
        {
            return HashCode.Combine(_base, _exponent);
        }
        public override string ToString()
        {
            if (_exponent < 3)
            {
                return ((int)(_base * Math.Pow(10, _exponent))).ToString();
            }
            else
            {
                return $"{Math.Round(_base, 3)}e{_exponent}";
            }
        }
    }
}