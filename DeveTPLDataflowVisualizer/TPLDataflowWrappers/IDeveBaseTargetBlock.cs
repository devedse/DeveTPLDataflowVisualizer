using System.Threading.Tasks.Dataflow;

namespace DLPR.Detection.WholeFlow.TPLDataflowWrappers
{
    public interface IDeveBaseTargetBlock<TInput> : IDeveBaseBlock
    {
        ITargetBlock<TInput> TargetBlock { get; }
    }
}
