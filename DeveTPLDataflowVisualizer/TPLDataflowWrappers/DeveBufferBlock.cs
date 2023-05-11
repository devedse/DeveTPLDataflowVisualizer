using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveBufferBlock<T> : DeveBaseBlock<T, T>, IDeveBaseSourceBlock<T>, IDeveBaseTargetBlock<T>
    {
        private BufferBlock<T> _innerBlock;

        public BufferBlock<T> InnerBlock => _innerBlock;

        public override int ProcessingCount => _innerBlock.Count;

        public ISourceBlock<T> SourceBlock => _innerBlock;

        public ITargetBlock<T> TargetBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => null;

        public DeveBufferBlock(string blockName) : base(blockName)
        {
            _innerBlock = new BufferBlock<T>();
        }

        public DeveBufferBlock(string blockName, DataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new BufferBlock<T>(dataflowBlockOptions);
        }
    }
}
