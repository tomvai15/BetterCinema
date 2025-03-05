using CommandLine;

namespace BetterCinema.Tools.App;

internal class Options
{
    [Option('i', "input", Required = true, HelpText = "Input assembly (.dll) file path.")]
    public required string Input { get; set; }
    
    [Option('n', "namespace", Required = true, HelpText = "Namespace")]
    public required string Namespace { get; set; }

    [Option('o', "output", Required = false, HelpText = "Output file (JSON contract).")]
    public required string Output { get; set; }
}