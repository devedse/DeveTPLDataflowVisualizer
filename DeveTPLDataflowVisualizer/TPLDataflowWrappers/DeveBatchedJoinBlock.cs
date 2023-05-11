using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace DLPR.Detection.WholeFlow.TPLDataflowWrappers
{
    public class DeveBatchedJoinBlock<T1, T2> : DeveBaseBlock<Tuple<IList<T1>, IList<T2>>, Tuple<IList<T1>, IList<T2>>>, IDeveBaseSourceBlock<Tuple<IList<T1>, IList<T2>>>
    {
        private BatchedJoinBlock<T1, T2> _innerBlock;

        public BatchedJoinBlock<T1, T2> InnerBlock => _innerBlock;

        public ISourceBlock<Tuple<IList<T1>, IList<T2>>> SourceBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveBatchedJoinBlock(string blockName, int batchSize) : base(blockName)
        {
            _innerBlock = new BatchedJoinBlock<T1, T2>(batchSize);
        }

        public DeveBatchedJoinBlock(string blockName, int batchSize, GroupingDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new BatchedJoinBlock<T1, T2>(batchSize, dataflowBlockOptions);
        }
    }

    public class DeveBatchedJoinBlock<T1, T2, T3> : DeveBaseBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>, Tuple<IList<T1>, IList<T2>, IList<T3>>>
    {
        private BatchedJoinBlock<T1, T2, T3> _innerBlock;

        public BatchedJoinBlock<T1, T2, T3> InnerBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveBatchedJoinBlock(string blockName, int batchSize) : base(blockName)
        {
            _innerBlock = new BatchedJoinBlock<T1, T2, T3>(batchSize);
        }

        public DeveBatchedJoinBlock(string blockName, int batchSize, GroupingDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new BatchedJoinBlock<T1, T2, T3>(batchSize, dataflowBlockOptions);
        }
    }
}
