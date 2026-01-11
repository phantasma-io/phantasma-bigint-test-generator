// Fixture generator entry point.
// - Default mode emits BigInteger byte vectors (Phantasma/C# encodings).
// - Ops mode emits arithmetic results (compare/add/sub/mul/div/mod/shl/shr).
// Output is written to stdout so callers can redirect into fixture files.
class BigIntTestGeneratorClass
{
    static void Main(string[] args)
    {
        var options = GeneratorOptions.Parse(args);
        if (options.Mode == FixtureMode.Ops)
        {
            BigIntOpsFixtureGenerator.WriteOps();
        }
        else
        {
            BigIntVectorFixtureGenerator.WriteVectors(options.Format);
        }
    }
}
