using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public interface IDeveBaseTargetBlock<TInput> : IDeveBaseBlock
    {
        ITargetBlock<TInput> TargetBlock { get; }
    }
}
