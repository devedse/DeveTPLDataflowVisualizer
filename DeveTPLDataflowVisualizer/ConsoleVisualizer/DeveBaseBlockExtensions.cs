using DLPR.Detection.WholeFlow.TPLDataflowWrappers;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;

namespace DLPR.Detection.WholeFlow.ConsoleVisualizer
{
    public static class DeveBaseBlockExtensions
    {
        public static IRenderable VisualizeProgress(this IDeveBaseBlock block, int totalItems)
        {
            //var visualizer = new BarChart()
            //    .Width(130)
            //    .Label($"[yellow]{block.BlockName}[/]")
            //    .AddItem(new BarChartItem("Processing", block.ProcessingCount, Color.Yellow))
            //    .AddItem(new BarChartItem("Processed", block.ProcessedCount, Color.Green))
            //    .AddItem(new BarChartItem("Input", block.InputCount ?? 0, Color.Blue))
            //    .AddItem(new BarChartItem("Output", block.OutputCount ?? 0, Color.Blue));



            var breakDownChart = new BreakdownChart()
                .Width(60)
                .AddItem("Input", block.InputCount ?? 0, Color.Blue)
                .AddItem("Processed", block.ProcessedCount, Color.Green)
                .AddItem("Processing", block.ProcessingCount, Color.Orange1)
                .AddItem("Remaining", Math.Max(0, totalItems - block.ProcessingCount - block.ProcessedCount - (block.InputCount ?? 0)), Color.Grey);

            var visualizer = new Rows(
                new Text(block.BlockName, new Style(Color.Yellow, Color.Black)),
                breakDownChart
                );

            return visualizer;
        }
    }
}
