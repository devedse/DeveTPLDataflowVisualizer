using System;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveJoinBlock<T1, T2> : DeveBaseBlock<Tuple<T1, T2>, Tuple<T1, T2>>, IDeveBaseSourceBlock<Tuple<T1, T2>>
    {
        private JoinBlock<T1, T2> _innerBlock;

        public JoinBlock<T1, T2> InnerBlock => _innerBlock;

        public ISourceBlock<Tuple<T1, T2>> SourceBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveJoinBlock(string blockName) : base(blockName)
        {
            _innerBlock = new JoinBlock<T1, T2>();
        }

        public DeveJoinBlock(string blockName, GroupingDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new JoinBlock<T1, T2>(dataflowBlockOptions);
        }
    }

    public class DeveJoinBlock<T1, T2, T3> : DeveBaseBlock<Tuple<T1, T2, T3>, Tuple<T1, T2, T3>>, IDeveBaseSourceBlock<Tuple<T1, T2, T3>>
    {
        private JoinBlock<T1, T2, T3> _innerBlock;

        public JoinBlock<T1, T2, T3> InnerBlock => _innerBlock;

        public ISourceBlock<Tuple<T1, T2, T3>> SourceBlock => _innerBlock;

        public override int? InputCount => null;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveJoinBlock(string blockName) : base(blockName)
        {
            _innerBlock = new JoinBlock<T1, T2, T3>();
        }

        public DeveJoinBlock(string blockName, GroupingDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new JoinBlock<T1, T2, T3>(dataflowBlockOptions);
        }
    }
}
