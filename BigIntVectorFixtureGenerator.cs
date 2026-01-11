using System;
using System.Numerics;
using Phantasma.Core.Numerics;

// Emits BigInteger encoding vectors for Phantasma (signed) and C# (ToByteArray).
// Output format:
// - TSV: number<TAB>pha-bytes<TAB>csharp-bytes
// - Go:  { "number", []byte{pha}, []byte{csharp} }
static class BigIntVectorFixtureGenerator
{
    public static void WriteVectors(OutputFormat format)
    {
        if (format == OutputFormat.Tsv)
        {
            Console.WriteLine("number\tpha\tcsharp");
        }

        for (var i = 0; i <= 256; i++)
        {
            // Powers of two +/- 1 hit carry/size boundaries and sign edges.
            var n = new BigInteger(2);
            n = BigInteger.Pow(n, i);

            PrintBytes(n - 1, format);
            if (n != 1)
            {
                PrintBytes((n - 1) * -1, format);
            }

            PrintBytes(n, format);
            PrintBytes(n * -1, format);

            PrintBytes(n + 1, format);
            PrintBytes((n + 1) * -1, format);
        }

        for (var i = 0; i < 10000; i++)
        {
            // Small integers to cover dense ranges and sign flips.
            var n = new BigInteger(i);

            PrintBytes(n, format);
            if (n != 0)
            {
                PrintBytes(n * -1, format);
            }
        }

        // Additional sparse values for non-trivial byte shapes.
        PrintBytes(BigInteger.Parse("783269426398462946992340273"), format);
        PrintBytes(BigInteger.Parse("-783269426398462946992340273"), format);
        PrintBytes(BigInteger.Parse("99999999999999999999999999999999999999999999999999"), format);
        PrintBytes(BigInteger.Parse("-99999999999999999999999999999999999999999999999999"), format);

        var extraLarge = new[]
        {
            // Very large values to stress multi-word encoding paths.
            BigInteger.Parse("8506561706216989631643979852519097382101710309577344112488802686379299442766103097380081876411420150591842446706493979321986084418396377202684800540894741"),
            BigInteger.Parse("8029541867752090211644397925222479998136716829399072870341661927282019762158040586041391241233612794609110344878858108842603834440981839171295350666632300"),
            BigInteger.Parse("91241592099873378332767577914566580296102043684851426751188714976988144413092437902648637826323907941426224494323250086816433071027090131219630611085553691687186564930256986267580713898849494709022011774171013010538377940822694456323003590212837792749365858437206212710775969748761054365696645177537043859503"),
            BigInteger.Parse("136953728755247462378947750489598505112866807482135314373324333488112738606359854166030691487499126212487085194239151202794247055532419254520829472811214637010066164911769237097543568064213591079577889843925470480240320683964486463870945655187760999378945313702500605515620081967306036635596718220587338202593")
        };

        foreach (var n in extraLarge)
        {
            PrintBytes(n, format);
            PrintBytes(n * -1, format);
        }
    }

    static void PrintDecBytes(byte[] bytes)
    {
        for (var i = 0; i < bytes.Length; i++)
        {
            if (i > 0)
            {
                Console.Write(' ');
            }
            Console.Write(bytes[i]);
        }
    }

    static void PrintGoBytes(byte[] bytes)
    {
        for (var i = 0; i < bytes.Length; i++)
        {
            if (i > 0)
            {
                Console.Write(", ");
            }
            Console.Write(bytes[i]);
        }
    }

    static void PrintBytes(string n, byte[] csharpBytes, byte[] phaBytes, OutputFormat format)
    {
        if (format == OutputFormat.Go)
        {
            // Go fixture uses a struct literal per line.
            Console.Write("    {\"");
            Console.Write(n);
            Console.Write("\", []byte{");
            PrintGoBytes(phaBytes);
            Console.Write("}, []byte{");
            PrintGoBytes(csharpBytes);
            Console.Write("}},\n");
            return;
        }

        Console.Write(n);
        Console.Write('\t');
        PrintDecBytes(phaBytes);
        Console.Write('\t');
        PrintDecBytes(csharpBytes);
        Console.Write('\n');
    }

    static void PrintBytes(BigInteger n, OutputFormat format)
    {
        // Phantasma.Core uses ToSignedByteArray for VM number encoding.
        PrintBytes(n.ToString(), n.ToByteArray(), n.ToSignedByteArray(), format);
    }
}
