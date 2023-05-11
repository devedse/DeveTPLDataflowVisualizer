using System;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public interface IDeveBaseSourceBlock<TOutput> : IDeveBaseBlock
    {
        ISourceBlock<TOutput> SourceBlock { get; }

        public void LinkTo(IDeveBaseTargetBlock<TOutput> target)
        {
            SourceBlock.LinkTo(target.TargetBlock);
            Targets.Add(target);
        }

        public void LinkTo(IDeveBaseTargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            SourceBlock.LinkTo(target.TargetBlock, linkOptions);
            Targets.Add(target);
        }

        public void LinkTo(IDeveBaseTargetBlock<TOutput> target, Predicate<TOutput> predicate)
        {
            SourceBlock.LinkTo(target.TargetBlock, predicate);
            Targets.Add(target);
        }

        public void LinkTo(IDeveBaseTargetBlock<TOutput> target, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            SourceBlock.LinkTo(target.TargetBlock, linkOptions, predicate);
            Targets.Add(target);
        }
    }
}
