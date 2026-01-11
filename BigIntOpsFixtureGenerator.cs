using System;
using System.Collections.Generic;
using System.Numerics;
using Phantasma.Core.Numerics;

// Emits arithmetic operation fixtures using .NET BigInteger semantics.
// Columns: a, b, shift, compare, add, sub, mul, div, mod, shl, shr.
static class BigIntOpsFixtureGenerator
{
    public static void WriteOps()
    {
        Console.WriteLine("a\tb\tshift\tcmp\tadd\tsub\tmul\tdiv\tmod\tshl\tshr");

        foreach (var item in GetCases())
        {
            var a = item.A;
            var b = item.B;
            var shift = item.Shift;

            if (b == 0)
            {
                // Avoid division by zero; the test vectors are for normal arithmetic paths.
                continue;
            }

            var cmp = a.CompareTo(b);
            var add = a + b;
            var sub = a - b;
            var mul = a * b;
            var div = a / b;
            var mod = a % b;
            var shl = a << shift;
            var shr = a >> shift;

            Console.Write(a.ToString());
            Console.Write('\t');
            Console.Write(b.ToString());
            Console.Write('\t');
            Console.Write(shift);
            Console.Write('\t');
            Console.Write(cmp);
            Console.Write('\t');
            Console.Write(add.ToString());
            Console.Write('\t');
            Console.Write(sub.ToString());
            Console.Write('\t');
            Console.Write(mul.ToString());
            Console.Write('\t');
            Console.Write(div.ToString());
            Console.Write('\t');
            Console.Write(mod.ToString());
            Console.Write('\t');
            Console.Write(shl.ToString());
            Console.Write('\t');
            Console.Write(shr.ToString());
            Console.Write('\n');
        }
    }

    static IEnumerable<OpCase> GetCases()
    {
        var two = new BigInteger(2);
        var pow31 = BigInteger.Pow(two, 31);
        var pow63 = BigInteger.Pow(two, 63);
        var pow64 = BigInteger.Pow(two, 64);
        var pow127 = BigInteger.Pow(two, 127);
        var pow128 = BigInteger.Pow(two, 128);

        return new[]
        {
            // Small signed values + power-of-two edges to hit carry/sign transitions.
            new OpCase(0, 1, 0),
            new OpCase(1, 1, 1),
            new OpCase(2, 3, 1),
            new OpCase(2, -3, 1),
            new OpCase(-2, 3, 1),
            new OpCase(7, 5, 2),
            new OpCase(-7, 5, 2),
            new OpCase(7, -5, 2),
            new OpCase(123456789, 1000, 5),
            new OpCase(123456789, -1000, 5),
            new OpCase(-123456789, 1000, 5),
            new OpCase(-123456789, -1000, 5),
            new OpCase(pow31 - 1, 12345, 16),
            new OpCase(pow31, 12345, 16),
            new OpCase(pow63 - 1, pow31 - 1, 31),
            new OpCase(pow63, 97, 32),
            new OpCase(-pow63, 97, 32),
            new OpCase(pow64, 5, 33),
            new OpCase(pow127 - 1, 17, 64),
            new OpCase(pow128, BigInteger.Parse("12345678901234567890"), 65),
            new OpCase(BigInteger.Parse("783269426398462946992340273"), 1234567, 17),
            new OpCase(BigInteger.Parse("-783269426398462946992340273"), 1234567, 17),
            new OpCase(BigInteger.Parse("99999999999999999999999999999999999999999999999999"), 314159, 19),
            new OpCase(BigInteger.Parse("-99999999999999999999999999999999999999999999999999"), 314159, 19)
        };
    }

    readonly struct OpCase
    {
        public BigInteger A { get; }
        public BigInteger B { get; }
        public int Shift { get; }

        public OpCase(BigInteger a, BigInteger b, int shift)
        {
            A = a;
            B = b;
            Shift = shift;
        }
    }
}
