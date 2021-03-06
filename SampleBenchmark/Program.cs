using CsharpRAPL.Analysis;
using CsharpRAPL.Benchmarking;
using CsharpRAPL.CommandLine;
using CsharpRAPL.Data.Export;
using CsharpRAPL.Plotting;
using CsvHelper.Configuration;
using CsvHelper;
using System.Drawing;
using System.Globalization;
using System.IO.Compression;
using ScottPlot.Drawing;

namespace SampleBenchmark
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Options options = CsharpRAPLCLI.Parse(args);

            var suite = new BenchmarkCollector
            {
                PlotOptions = new PlotOptions
                {
                    GrayScale = false,
                    FillColor = Color.LightPink,
                    HatchStyle = HatchStyle.DottedDiamond
                }
            };

            suite.RunAll();

            foreach ((string group, List<IBenchmark> benchmarks) in suite.GetBenchmarksByGroup())
            {
                Dictionary<string, double> result = Analysis.CalculatePValueForGroup(benchmarks);
                DateTime dateTime = DateTime.Now;
                string time = $"{dateTime.ToString("s").Replace(":", "-")}-{dateTime.Millisecond}";
                Directory.CreateDirectory(Path.Join(options.OutputPath, $"_pvalues/{group}/"));
                using var writer = new StreamWriter(Path.Join(options.OutputPath, $"_pvalues/{group}/{time}.csv"));
                using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" });
                csv.WriteRecords(result);
            }


            if (File.Exists("results.zip"))
            {
                using var zipToOpen = new FileStream("results.zip", FileMode.Open);
                using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
                foreach (string file in Directory.EnumerateFiles(Path.Join(options.OutputPath, "_pvalues/"), "*.csv",
                    SearchOption.AllDirectories))
                {
                    archive.CreateEntryFromFile(file, file);
                }

                archive.Dispose();
                zipToOpen.Dispose();
            }

            LatexExporter.GenerateAverageTable();
            LatexExporter.GenerateTexImages();
            LatexExporter.GeneratePValueTables();
        }
    }
}