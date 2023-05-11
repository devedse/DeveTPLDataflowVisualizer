using System.Collections.Generic;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public interface IDeveBaseBlock
    {
        string BlockName { get; }

        int ProcessingCount { get; }
        int ProcessedCount { get; }
        int? InputCount { get; }
        int? OutputCount { get; }

        string ProgressString { get; }
        IList<IDeveBaseBlock> Targets { get; }


        IEnumerable<IDeveBaseBlock> RecursivelyGetAllTargetsDistincted();
        IEnumerable<IDeveBaseBlock> RecursivelyGetAllTargets();
    }
}
