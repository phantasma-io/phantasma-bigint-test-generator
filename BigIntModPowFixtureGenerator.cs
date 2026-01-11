using System;
using System.Collections.Generic;
using System.Numerics;

// Emits modular exponentiation fixtures using .NET semantics.
// Columns: value, exponent, modulus, modpow.
static class BigIntModPowFixtureGenerator
{
    public static void WriteModPow()
    {
        Console.WriteLine("a\texp\tmod\tmodpow");

        foreach (var item in GetCases())
        {
            var value = item.Value;
            var exp = item.Exponent;
            var mod = item.Modulus;
            var result = BigIntModHelpers.ModPow(value, exp, mod);

            Console.Write(value.ToString());
            Console.Write('\t');
            Console.Write(exp.ToString());
            Console.Write('\t');
            Console.Write(mod.ToString());
            Console.Write('\t');
            Console.Write(result.ToString());
            Console.Write('\n');
        }
    }

    static IEnumerable<ModPowCase> GetCases()
    {
        var two = new BigInteger(2);
        var pow31 = BigInteger.Pow(two, 31);
        var pow32 = BigInteger.Pow(two, 32);

        return new[]
        {
            new ModPowCase(2, 0, 13),
            new ModPowCase(2, 5, 13),
            new ModPowCase(10, 1, 17),
            new ModPowCase(10, 2, 17),
            new ModPowCase(123456789, 3, 97),
            new ModPowCase(-2, 5, 13),
            new ModPowCase(-2, 4, 13),
            new ModPowCase(pow31 - 1, 2, 65537),
            new ModPowCase(pow32 + 1, 2, 65537),
            // Negative exponent exercises ModInverse path.
            new ModPowCase(2, -1, 13),
            new ModPowCase(5, -2, 97)
        };
    }

    readonly struct ModPowCase
    {
        public BigInteger Value { get; }
        public BigInteger Exponent { get; }
        public BigInteger Modulus { get; }

        public ModPowCase(BigInteger value, BigInteger exponent, BigInteger modulus)
        {
            Value = value;
            Exponent = exponent;
            Modulus = modulus;
        }
    }
}
