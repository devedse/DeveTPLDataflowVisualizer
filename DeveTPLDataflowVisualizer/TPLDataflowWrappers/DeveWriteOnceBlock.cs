using System;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveWriteOnceBlock<T> : DeveBaseBlock<T, T>, IDeveBaseSourceBlock<T>, IDeveBaseTargetBlock<T>
    {
        private WriteOnceBlock<T> _innerBlock;

        public WriteOnceBlock<T> InnerBlock => _innerBlock;

        public ISourceBlock<T> SourceBlock => _innerBlock;

        public ITargetBlock<T> TargetBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => null;

        public DeveWriteOnceBlock(string blockName, Func<T, T> cloningFunction) : base(blockName)
        {
            var wrappedCloningFunction = WrapTransform(cloningFunction ?? DefaultCloningFunction);
            _innerBlock = new WriteOnceBlock<T>(wrappedCloningFunction);
        }

        public DeveWriteOnceBlock(string blockName, Func<T, T> cloningFunction, DataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            var wrappedCloningFunction = WrapTransform(cloningFunction ?? DefaultCloningFunction);
            _innerBlock = new WriteOnceBlock<T>(wrappedCloningFunction, dataflowBlockOptions);
        }

        private static T DefaultCloningFunction(T input)
        {
            return input;
        }
    }
}
