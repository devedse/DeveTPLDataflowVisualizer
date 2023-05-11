using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveBatchBlock<T> : DeveBaseBlock<T, T[]>, IDeveBaseSourceBlock<T[]>, IDeveBaseTargetBlock<T>
    {
        private BatchBlock<T> _innerBlock;

        public BatchBlock<T> InnerBlock => _innerBlock;

        public ISourceBlock<T[]> SourceBlock => _innerBlock;

        public ITargetBlock<T> TargetBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveBatchBlock(string blockName, int batchSize) : base(blockName)
        {
            _innerBlock = new BatchBlock<T>(batchSize);
        }

        public DeveBatchBlock(string blockName, int batchSize, GroupingDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new BatchBlock<T>(batchSize, dataflowBlockOptions);
        }
    }
}
