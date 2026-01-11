using System;
using System.Collections.Generic;
using System.Numerics;

// Emits bitwise operation fixtures using .NET BigInteger semantics.
// Columns: a, b, and, or, xor, notA, notB.
static class BigIntBitwiseFixtureGenerator
{
    public static void WriteBitwise()
    {
        Console.WriteLine("a\tb\tand\tor\txor\tnotA\tnotB");

        foreach (var item in GetCases())
        {
            var a = item.A;
            var b = item.B;

            var and = a & b;
            var or = a | b;
            var xor = a ^ b;
            var notA = ~a;
            var notB = ~b;

            Console.Write(a.ToString());
            Console.Write('\t');
            Console.Write(b.ToString());
            Console.Write('\t');
            Console.Write(and.ToString());
            Console.Write('\t');
            Console.Write(or.ToString());
            Console.Write('\t');
            Console.Write(xor.ToString());
            Console.Write('\t');
            Console.Write(notA.ToString());
            Console.Write('\t');
            Console.Write(notB.ToString());
            Console.Write('\n');
        }
    }

    static IEnumerable<BitwiseCase> GetCases()
    {
        var two = new BigInteger(2);
        var pow31 = BigInteger.Pow(two, 31);
        var pow32 = BigInteger.Pow(two, 32);
        var pow63 = BigInteger.Pow(two, 63);
        var pow64 = BigInteger.Pow(two, 64);

        return new[]
        {
            // Zero/one and sign flips.
            new BitwiseCase(0, 0),
            new BitwiseCase(0, 1),
            new BitwiseCase(1, 0),
            new BitwiseCase(1, 1),
            new BitwiseCase(-1, 0),
            new BitwiseCase(-1, 1),
            new BitwiseCase(-1, 2),
            new BitwiseCase(1, -1),
            new BitwiseCase(-1, -1),
            // Small mixed values.
            new BitwiseCase(2, 3),
            new BitwiseCase(5, 20),
            new BitwiseCase(-2, 3),
            new BitwiseCase(-7, 5),
            new BitwiseCase(7, -5),
            // Power-of-two edges.
            new BitwiseCase(pow31 - 1, pow31),
            new BitwiseCase(pow32 - 1, pow32),
            new BitwiseCase(pow63 - 1, pow63),
            new BitwiseCase(pow64 - 1, pow64),
            // Large signed values.
            new BitwiseCase(BigInteger.Parse("783269426398462946992340273"), BigInteger.Parse("12345678901234567890")),
            new BitwiseCase(BigInteger.Parse("-783269426398462946992340273"), BigInteger.Parse("12345678901234567890"))
        };
    }

    readonly struct BitwiseCase
    {
        public BigInteger A { get; }
        public BigInteger B { get; }

        public BitwiseCase(BigInteger a, BigInteger b)
        {
            A = a;
            B = b;
        }
    }
}
