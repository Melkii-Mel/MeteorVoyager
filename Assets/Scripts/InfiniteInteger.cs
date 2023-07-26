using System;
using UnityEngine;

[Serializable]
public struct InfiniteInteger
{
    #region consts
    public static readonly InfiniteInteger Zero = 0;
    public static readonly InfiniteInteger One = 1;
    public static readonly InfiniteInteger Ten = 10;
    public static readonly InfiniteInteger Hundred = 100;
    public static readonly InfiniteInteger Thousand = 1000;
    public static readonly InfiniteInteger Million = 1000000;
    public static readonly InfiniteInteger Billion = 1000000000;
    public static readonly InfiniteInteger Trillion = Billion * 1000;
    #endregion

    private enum Operators
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

    
    public InfiniteInteger(double @base = 0, float exponent = 0)
    {
        _base = @base;
        _exponent = exponent;
        Compress(ref this);
    }

    #region fields that represents a number
    
    private double _base;
    private float _exponent;
    #endregion
    #region public properties
    
    public double Base
    {
        get => _base;
        set => _base = value;
    }
    public float Exponent
    {
        get => (int)_exponent;
        set => _exponent = value;
    }
    
    #endregion
    #region casting
    
    public static implicit operator InfiniteInteger(int value)
    {
        return new InfiniteInteger(value);
    }
    public static implicit operator InfiniteInteger((double, int) value)
    {
        return new InfiniteInteger(value.Item1, value.Item2);
    }
    public static explicit operator double(InfiniteInteger value)
    {
        return value._base * MathF.Pow(10, value._exponent);
    }
    
    #endregion
    #region arithmetic operators (+ - * /)
    
    public static InfiniteInteger operator +(InfiniteInteger a, int b)
    {
        return PerformArithmeticOperation(a, (float)b, Operators.Plus);
    }
    public static InfiniteInteger operator +(int a, InfiniteInteger b)
    {
        return b + a;
    }
    public static InfiniteInteger operator +(InfiniteInteger a, double b)
    {
        return PerformArithmeticOperation(a, b, Operators.Plus);
    }
    public static InfiniteInteger operator +(double a, InfiniteInteger b)
    {
        return b + a;
    }
    public static InfiniteInteger operator +(InfiniteInteger a, InfiniteInteger b)
    {
        return PerformArithmeticOperation(a, b, Operators.Plus);
    }
    public static InfiniteInteger operator -(InfiniteInteger a, int b)
    {
        return PerformArithmeticOperation(a, (double)b, Operators.Minus);
    }
    public static InfiniteInteger operator -(int a, InfiniteInteger b)
    {
        return -(b - a);
    }
    public static InfiniteInteger operator -(InfiniteInteger a, double b)
    {
        return PerformArithmeticOperation(a, b, Operators.Minus);
    }
    public static InfiniteInteger operator -(double a, InfiniteInteger b)
    {
        return -(b - a);
    }
    public static InfiniteInteger operator -(InfiniteInteger a, InfiniteInteger b)
    {
        return PerformArithmeticOperation(a, b, Operators.Minus);
    }

    public static InfiniteInteger operator -(InfiniteInteger a)
    {
        a._base = -a._base;
        return a;
    }
    public static InfiniteInteger operator *(InfiniteInteger a, int b)
    {
        return PerformArithmeticOperation(a, (double)b, Operators.Multiplication);
    }
    public static InfiniteInteger operator *(int a, InfiniteInteger b)
    {
        return b * a;
    }
    public static InfiniteInteger operator *(InfiniteInteger a, double b)
    {
        return PerformArithmeticOperation(a, b, Operators.Multiplication);
    }

    public static InfiniteInteger operator *(double a, InfiniteInteger b)
    {
        return b * a;
    }
    public static InfiniteInteger operator *(InfiniteInteger a, InfiniteInteger b)
    {
        return PerformArithmeticOperation(a, b, Operators.Multiplication);
    }
    public static InfiniteInteger operator /(InfiniteInteger a, int b)
    {
        return PerformArithmeticOperation(a, (double)b, Operators.Division);
    }

    public static InfiniteInteger operator /(int a, InfiniteInteger b)
    {
        return PerformArithmeticOperation(a, b, Operators.Division);
    }
    public static InfiniteInteger operator /(InfiniteInteger a, double b)
    {
        return PerformArithmeticOperation(a, b, Operators.Division);
    }

    public static InfiniteInteger operator /(double a, InfiniteInteger b)
    {
        return 1 / PerformArithmeticOperation(b, a, Operators.Division);
    }

    public static InfiniteInteger operator /(InfiniteInteger a, InfiniteInteger b)
    {
        return PerformArithmeticOperation(a, b, Operators.Division);
    }
    
    #endregion
    #region logic operators (> < >= <= == !=)
    
    public static bool operator >(InfiniteInteger a, InfiniteInteger b)
    {
        if (a.Base <= 0 && b.Base >= 0) return false;
        if (a.Base > 0 && b.Base <= 0) return true;
        if (a.Base >= 0 && b.Base < 0) return true;
        if (a._exponent < b._exponent) return false;
        if (a._exponent > b._exponent) return true;
        return a._base > b._base;
    }
    public static bool operator <(InfiniteInteger a, InfiniteInteger b)
    {
        if (a.Base >= 0 && b.Base <= 0) return false;
        if (a.Base < 0 && b.Base >= 0) return true;
        if (a.Base <= 0 && b.Base > 0) return true;
        if (a._exponent > b._exponent) return false;
        if (a._exponent < b._exponent) return true;
        return a._base < b._base;
    }
    public static bool operator >=(InfiniteInteger a, InfiniteInteger b)
    {
        return !(a < b);
    }
    public static bool operator <=(InfiniteInteger a, InfiniteInteger b)
    {
        return !(a > b);
    }
    public static bool operator ==(InfiniteInteger a, InfiniteInteger b)
    {
        return (int)a._exponent == (int)b._exponent && Math.Abs(a._base - b._base) < 0.00001f;
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
        const double tolerance = 0.00000001;
        const double denominator = 1000000000;
        return Math.Abs((double)a / denominator - b / denominator) <= tolerance;
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
    
    #endregion
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
        int exponentPlus = 0;
        double startingBase = a._base;
        a._base = 1;
        for (int i = 0; i < power; i++)
        {
            a._base *= startingBase;
            if (a._base > 10)
            {
                a._base /= 10;
                exponentPlus++;
            }
        }
        a._exponent *= power;
        a._exponent += exponentPlus;
        Compress(ref a);
        return a;
    }

    /// <summary>
    /// Inaccurate
    /// </summary>
    public InfiniteInteger Pow(float power)
    {

        int exponentPlus = 0;
        double startingBase = _base;
        _base = 1;
        for (int i = 0; i < power; i++)
        {
            _base *= startingBase;
            if (_base > 10)
            {
                _base /= 10;
                exponentPlus++;
            }
        }
        _exponent *= power;
        _exponent += exponentPlus;
        Compress(ref this);
        return this;
    }

    #region log
    
    public double Log10()
    {
        return _exponent + Math.Log10(_base);
    }
    
    /// <summary>
    /// Inaccurate
    /// </summary>
    public double Log(float logBase)
    {
        return Mathf.Log(10) / Math.Log(logBase) * Log10();
    }
    
    #endregion

    public InfiniteInteger Round(int digit)
    {
        _base = Math.Round(_base, digit);
        return this;
    }
    
    [Obsolete("Method doesnt support high exponent values")]
    public InfiniteInteger OldPow(float power)
    {
        _base = Math.Pow(_base, power);
        _exponent *= power;
        Compress(ref this);
        return this;
    }
    [Obsolete("Method doesnt support high exponent values")]
    public static InfiniteInteger OldPow(InfiniteInteger a, float exponent)
    {
        a._base = Math.Pow(a._base, exponent);
        a._exponent *= exponent;
        Compress(ref a);
        return a;
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
        float mod = ii._exponent % 1;
        if (Math.Abs(Math.Abs(ii._base) - 10) < 0.0000009f)
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
               this == integer;
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