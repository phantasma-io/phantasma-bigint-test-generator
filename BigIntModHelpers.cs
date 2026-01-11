using System;
using System.Numerics;

// Shared helpers for modular arithmetic fixtures.
// Uses .NET BigInteger semantics to mirror C# SDK behavior.
static class BigIntModHelpers
{
    public static BigInteger PositiveMod(BigInteger value, BigInteger modulus)
    {
        var result = value % modulus;
        return result < 0 ? result + modulus : result;
    }

    public static BigInteger ModInverse(BigInteger value, BigInteger modulus)
    {
        // Extended Euclidean algorithm with positive result in [0, modulus).
        var t = BigInteger.Zero;
        var newT = BigInteger.One;
        var r = modulus;
        var newR = PositiveMod(value, modulus);

        while (newR != BigInteger.Zero)
        {
            var quotient = r / newR;
            var tempT = t - quotient * newT;
            t = newT;
            newT = tempT;

            var tempR = r - quotient * newR;
            r = newR;
            newR = tempR;
        }

        if (r != BigInteger.One)
        {
            throw new InvalidOperationException("No modular inverse for non-coprime values.");
        }

        if (t < 0)
        {
            t += modulus;
        }

        return t;
    }

    public static BigInteger ModPow(BigInteger value, BigInteger exponent, BigInteger modulus)
    {
        if (exponent < 0)
        {
            var inverse = ModInverse(value, modulus);
            return BigInteger.ModPow(inverse, BigInteger.Negate(exponent), modulus);
        }

        return BigInteger.ModPow(value, exponent, modulus);
    }
}
