using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public static class DeveTPLDataFlowStringCreator
    {
        public static string GenerateProgressString(IDeveBaseBlock baseBlock)
        {
            bool useEntersRatherThenSpaces = false;

            var separator = useEntersRatherThenSpaces ? Environment.NewLine : "";

            var totalString = new List<string>
            {
                $"Items Processed:  {baseBlock.ProcessedCount}",
                $"Items Processing: {baseBlock.ProcessingCount}",
                $"Input count: {baseBlock.InputCount}",
                $"Output count: {baseBlock.OutputCount}"
            };

            if (!useEntersRatherThenSpaces)
            {
                var longestStringLength = totalString.Max(t => t.Length);
                totalString = totalString.Select(t => t.PadRight(longestStringLength + 5)).ToList();
            }

            var str = string.Join(separator, totalString);
            return baseBlock.GetType().Name.PadRight(35) + str;
        }
    }
}
