using System;
using System.Collections.Generic;
using System.Numerics;

// Emits modular inverse fixtures using the same algorithm as C# SDK (extended Euclid).
// Columns: value, modulus, inverse.
static class BigIntModInverseFixtureGenerator
{
    public static void WriteModInverse()
    {
        Console.WriteLine("a\tmod\tinv");

        foreach (var item in GetCases())
        {
            var value = item.Value;
            var mod = item.Modulus;
            var inv = BigIntModHelpers.ModInverse(value, mod);

            Console.Write(value.ToString());
            Console.Write('\t');
            Console.Write(mod.ToString());
            Console.Write('\t');
            Console.Write(inv.ToString());
            Console.Write('\n');
        }
    }

    static IEnumerable<ModInverseCase> GetCases()
    {
        return new[]
        {
            new ModInverseCase(2, 13),
            new ModInverseCase(3, 11),
            new ModInverseCase(10, 17),
            new ModInverseCase(42, 2017),
            new ModInverseCase(-3, 11),
            new ModInverseCase(123456789, 97),
            new ModInverseCase(123456789, 65537),
            new ModInverseCase(987654321, 4294967291)
        };
    }

    readonly struct ModInverseCase
    {
        public BigInteger Value { get; }
        public BigInteger Modulus { get; }

        public ModInverseCase(BigInteger value, BigInteger modulus)
        {
            Value = value;
            Modulus = modulus;
        }
    }
}
