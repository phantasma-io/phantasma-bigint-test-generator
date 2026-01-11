using System;

// CLI parsing for fixture generation modes and output formats.
// Defaults: vectors + TSV. Accepts short aliases and --mode/--format forms.

enum OutputFormat
{
    Tsv,
    Go
}

enum FixtureMode
{
    Vectors,
    Ops,
    Bitwise,
    Pow,
    ModPow,
    ModInverse
}

readonly struct GeneratorOptions
{
    public OutputFormat Format { get; }
    public FixtureMode Mode { get; }

    public GeneratorOptions(OutputFormat format, FixtureMode mode)
    {
        Format = format;
        Mode = mode;
    }

    public static GeneratorOptions Parse(string[] args)
    {
        var format = OutputFormat.Tsv;
        var mode = FixtureMode.Vectors;

        foreach (var arg in args)
        {
            // Allow both --flag and plain token forms to keep CLI usage short.
            if (arg.Equals("--ops", StringComparison.OrdinalIgnoreCase) || arg.Equals("ops", StringComparison.OrdinalIgnoreCase))
            {
                mode = FixtureMode.Ops;
            }
            else if (arg.Equals("--bitwise", StringComparison.OrdinalIgnoreCase) || arg.Equals("bitwise", StringComparison.OrdinalIgnoreCase))
            {
                mode = FixtureMode.Bitwise;
            }
            else if (arg.Equals("--pow", StringComparison.OrdinalIgnoreCase) || arg.Equals("pow", StringComparison.OrdinalIgnoreCase))
            {
                mode = FixtureMode.Pow;
            }
            else if (arg.Equals("--modpow", StringComparison.OrdinalIgnoreCase) || arg.Equals("modpow", StringComparison.OrdinalIgnoreCase))
            {
                mode = FixtureMode.ModPow;
            }
            else if (arg.Equals("--modinv", StringComparison.OrdinalIgnoreCase) || arg.Equals("modinv", StringComparison.OrdinalIgnoreCase))
            {
                mode = FixtureMode.ModInverse;
            }
            else if (arg.Equals("--vectors", StringComparison.OrdinalIgnoreCase) || arg.Equals("vectors", StringComparison.OrdinalIgnoreCase))
            {
                mode = FixtureMode.Vectors;
            }
            else if (arg.StartsWith("--mode=", StringComparison.OrdinalIgnoreCase))
            {
                var value = arg.Substring("--mode=".Length);
                if (value.Equals("ops", StringComparison.OrdinalIgnoreCase))
                {
                    mode = FixtureMode.Ops;
                }
                else if (value.Equals("bitwise", StringComparison.OrdinalIgnoreCase))
                {
                    mode = FixtureMode.Bitwise;
                }
                else if (value.Equals("pow", StringComparison.OrdinalIgnoreCase))
                {
                    mode = FixtureMode.Pow;
                }
                else if (value.Equals("modpow", StringComparison.OrdinalIgnoreCase))
                {
                    mode = FixtureMode.ModPow;
                }
                else if (value.Equals("modinv", StringComparison.OrdinalIgnoreCase))
                {
                    mode = FixtureMode.ModInverse;
                }
                else if (value.Equals("vectors", StringComparison.OrdinalIgnoreCase))
                {
                    mode = FixtureMode.Vectors;
                }
            }
            else if (arg.Equals("--go", StringComparison.OrdinalIgnoreCase) || arg.Equals("go", StringComparison.OrdinalIgnoreCase))
            {
                format = OutputFormat.Go;
            }
            else if (arg.Equals("--tsv", StringComparison.OrdinalIgnoreCase) || arg.Equals("tsv", StringComparison.OrdinalIgnoreCase))
            {
                format = OutputFormat.Tsv;
            }
            else if (arg.StartsWith("--format=", StringComparison.OrdinalIgnoreCase))
            {
                var value = arg.Substring("--format=".Length);
                if (value.Equals("go", StringComparison.OrdinalIgnoreCase))
                {
                    format = OutputFormat.Go;
                }
                else if (value.Equals("tsv", StringComparison.OrdinalIgnoreCase))
                {
                    format = OutputFormat.Tsv;
                }
            }
        }

        return new GeneratorOptions(format, mode);
    }
}
