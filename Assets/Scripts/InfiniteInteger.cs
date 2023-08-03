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
    public const double TOLERANCE = 1e-10;
    public const int TOLERANCE_SIGNS = 10;
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
        get => Math.Round(_base, 9);
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

    public static explicit operator InfiniteInteger(double value)
    {
        return new InfiniteInteger(value);
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
        if (a._base <= 0 && b._base >= 0) return false;
        if (a._base > 0 && b._base <= 0) return true;
        if (a._base >= 0 && b._base < 0) return true;
        if (a._exponent < b._exponent) return false;
        if (a._exponent > b._exponent) return true;
        return a._base > b._base;
    }
    public static bool operator <(InfiniteInteger a, InfiniteInteger b)
    {
        if (a._base >= 0 && b._base <= 0) return false;
        if (a._base < 0 && b._base >= 0) return true;
        if (a._base <= 0 && b._base > 0) return true;
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
        return Math.Abs(a._exponent - b._exponent) < TOLERANCE && Math.Abs(a._base - b._base) < TOLERANCE;
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
        const double denominator = 1000000000;
        return Math.Abs((double)a / denominator - b / denominator) <= TOLERANCE;
    }
    public static bool operator !=(InfiniteInteger a, double b)
    {
        return !(a == b);
    }
    public static bool operator ==(InfiniteInteger a, int b)
    {
        return a == (InfiniteInteger)b;
    }
    public static bool operator !=(InfiniteInteger a, int b)
    {
        return !(a == b);
    }
    
    #endregion
    private static InfiniteInteger PerformArithmeticOperation(InfiniteInteger a, double b, Operators @operator)
    {
        double bNormalized;
        switch (@operator)
        {
            case Operators.Plus:
                bNormalized = b * Math.Pow(10, -a._exponent);
                a._base += bNormalized; break;
            case Operators.Minus:
                bNormalized = b * Math.Pow(10, -a._exponent);
                a._base -= bNormalized; break;
            case Operators.Multiplication:
                bNormalized = b;
                a._base *= bNormalized; break;
            case Operators.Division:
                bNormalized = b;
                a._base /= bNormalized; break;
        }
        Compress(ref a);
        return a;
    }
    private static InfiniteInteger PerformArithmeticOperation(InfiniteInteger a, InfiniteInteger b, Operators @operator)
    {
        void LocalCompress()
        {
            if (a._exponent > b._exponent)
            {
                CompressDown(ref b, a._exponent);
            }
            else if (a._exponent < b._exponent)
            {
                CompressDown(ref a, b._exponent);
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

    #region Pow
    public static InfiniteInteger Pow(InfiniteInteger input, float power)
    {
        int exponentPlus = 0;
        double startingBase = input._base;
        InfiniteInteger output = new InfiniteInteger(1, input._exponent);
        float whole = (int)power;
        float remainder = power % 1;
        for (int i = 0; i < whole; i++)
        {
            output._base *= startingBase;
            if (output._base > 10)
            {
                output._base /= 10;
                exponentPlus++;
            }
        }

        output._exponent *= whole;
        output._exponent += exponentPlus;
        Compress(ref output);
        if (remainder == 0)
        {
            return output;
        }
        output *= new InfiniteInteger(Math.Pow(input._base, remainder),  input._exponent * remainder);
        Compress(ref output);
        return output;
    }
    
    public InfiniteInteger Pow(float power)
    {
        return Pow(this, power);
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
    #endregion

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
    

    private static InfiniteInteger Compress(ref InfiniteInteger ii)
    {
        if (ii._base == 0)
        {
            ii._exponent = 0;
            return ii;
        }
        double absBase = Math.Abs(ii._base);
        while (absBase > 10 - TOLERANCE)
        {
            ii._base /= 10;
            absBase /= 10;
            ii._exponent++;
        }
        while (absBase < 1 - TOLERANCE)
        {
            ii._base *= 10;
            absBase *= 10;
            ii._exponent--;
        }
        if (Math.Abs(Math.Abs(ii._base) - 10) < TOLERANCE)
        {
            ii._exponent++;
            ii._base = 1;
        }
        float mod = ii._exponent % 1;
        if (mod != 0)
        {
            ii._base *= Math.Pow(10, mod);
            ii._exponent -= mod;
            Compress(ref ii);
        }
        return ii;
    }

    private static InfiniteInteger CompressDown(ref InfiniteInteger ii, double targetExponent)
    {
        if (targetExponent < ii._exponent)
            throw new Exception("Internal exception: targetExponent must not be lower than ii._exponent");
        if (targetExponent - ii._exponent > TOLERANCE_SIGNS + 1)
        {
            return 0;
        }
        
        while (ii._exponent < targetExponent)
        {
            ii._exponent++;
            ii._base /= 10;
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