using System;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveBroadcastBlock<T> : DeveBaseBlock<T, T>, IDeveBaseSourceBlock<T>, IDeveBaseTargetBlock<T>
    {
        private BroadcastBlock<T> _innerBlock;

        public BroadcastBlock<T> InnerBlock => _innerBlock;

        public ISourceBlock<T> SourceBlock => _innerBlock;

        public ITargetBlock<T> TargetBlock => _innerBlock;

        //Divide by targets because a broadcast block calls processed for every target
        public override int ProcessedCount => base.ProcessedCount / Targets.Count;

        public override int? InputCount => null;

        public override int? OutputCount => null;

        public DeveBroadcastBlock(string blockName, Func<T, T> cloningFunction) : base(blockName)
        {
            var wrappedCloningFunction = WrapTransform(cloningFunction ?? DefaultCloningFunction);
            _innerBlock = new BroadcastBlock<T>(wrappedCloningFunction);
        }

        public DeveBroadcastBlock(string blockName, Func<T, T> cloningFunction, DataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            var wrappedCloningFunction = WrapTransform(cloningFunction ?? DefaultCloningFunction);
            _innerBlock = new BroadcastBlock<T>(wrappedCloningFunction, dataflowBlockOptions);
        }

        private static T DefaultCloningFunction(T input)
        {
            return input;
        }
    }
}
