using System;
using System.Numerics;
using Phantasma.Core.Numerics;

class BigIntTestGeneratorClass
{
    static void PrintBytes(string n, byte[] csharpBytes, byte[] phaBytes)
    {
        Console.Write("    {\"" + n + "\", []byte{");
        for (var i = 0; i < phaBytes.Length; i++)
        {
            Console.Write(phaBytes[i]);

            if (i != phaBytes.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.Write("}, []byte{");
        
        for (var i = 0; i < csharpBytes.Length; i++)
        {
            Console.Write(csharpBytes[i]);
            if (i != csharpBytes.Length - 1)
            {
                Console.Write(", ");
            }
        }
        
        Console.Write("}},\n");
    }

    static void PrintBytes(BigInteger n)
    {
        PrintBytes(n.ToString(), n.ToByteArray(),n.ToSignedByteArray());
    }

    
    static void Main(string[] args)
    {
        for (var i = 0; i < 256; i++)
        {
            var n = new BigInteger(2);
            n = BigInteger.Pow(n, i);
            
            PrintBytes(n-1);
            if (n != 1)
            {
                PrintBytes((n - 1) * -1);
            }

            PrintBytes(n);
            PrintBytes(n * -1);
            
            PrintBytes(n+1);
            PrintBytes((n+1) * -1);
        }
        
        for (var i = 0; i < 10000; i++)
        {
            var n = new BigInteger(i);
            
            PrintBytes(n);
            if (n != 0)
            {
                PrintBytes(n * -1);
            }
        }

        PrintBytes(BigInteger.Parse("783269426398462946992340273"));
        PrintBytes(BigInteger.Parse("-783269426398462946992340273"));
        PrintBytes(BigInteger.Parse("99999999999999999999999999999999999999999999999999"));
        PrintBytes(BigInteger.Parse("-99999999999999999999999999999999999999999999999999"));
    }
}