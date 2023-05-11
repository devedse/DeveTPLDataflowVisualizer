using DLPR.Detection.WholeFlow.TPLDataflowWrappers;
using Spectre.Console;
using System;

namespace DLPR.Detection.WholeFlow.ConsoleVisualizer
{
    public static class SpectreConsoleRenderer
    {
        public static void SuperBaumenMacher(IDeveBaseBlock deveBaseBlock, TreeNode node, Func<IDeveBaseBlock, int> determineTotalItems)
        {
            // Add the bar chart for ProcessingCount and ProcessedCount            
            var progressVisualization = deveBaseBlock.VisualizeProgress(determineTotalItems(deveBaseBlock));
            var subNode = new TreeNode(progressVisualization);
            node.AddNode(subNode);

            // Recursively add child nodes
            foreach (var target in deveBaseBlock.Targets)
            {
                SuperBaumenMacher(target, subNode, determineTotalItems);
            }
        }
    }
}
