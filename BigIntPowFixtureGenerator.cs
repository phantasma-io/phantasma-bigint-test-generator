using System;
using System.Collections.Generic;
using System.Numerics;

// Emits exponentiation fixtures using .NET BigInteger.Pow.
// Columns: base, exponent, pow.
static class BigIntPowFixtureGenerator
{
    public static void WritePow()
    {
        Console.WriteLine("a\texp\tpow");

        foreach (var item in GetCases())
        {
            var value = item.Value;
            var exp = item.Exponent;
            var result = BigInteger.Pow(value, exp);

            Console.Write(value.ToString());
            Console.Write('\t');
            Console.Write(exp);
            Console.Write('\t');
            Console.Write(result.ToString());
            Console.Write('\n');
        }
    }

    static IEnumerable<PowCase> GetCases()
    {
        var two = new BigInteger(2);
        var pow31 = BigInteger.Pow(two, 31);
        var pow32 = BigInteger.Pow(two, 32);

        return new[]
        {
            new PowCase(0, 0),
            new PowCase(0, 1),
            new PowCase(0, 2),
            new PowCase(1, 0),
            new PowCase(1, 5),
            new PowCase(-1, 0),
            new PowCase(-1, 1),
            new PowCase(-1, 2),
            new PowCase(-1, 3),
            new PowCase(2, 0),
            new PowCase(2, 1),
            new PowCase(2, 2),
            new PowCase(2, 5),
            new PowCase(2, 10),
            new PowCase(-2, 1),
            new PowCase(-2, 2),
            new PowCase(-2, 3),
            new PowCase(-2, 4),
            new PowCase(3, 5),
            new PowCase(10, 3),
            new PowCase(12345, 2),
            new PowCase(pow31 - 1, 2),
            new PowCase(pow32, 2),
            new PowCase(pow32 + 1, 2)
        };
    }

    readonly struct PowCase
    {
        public BigInteger Value { get; }
        public int Exponent { get; }

        public PowCase(BigInteger value, int exponent)
        {
            Value = value;
            Exponent = exponent;
        }
    }
}
