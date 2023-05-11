using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveActionBlock<T> : DeveBaseBlock<T, T>, IDeveBaseTargetBlock<T>
    {
        private ActionBlock<T> _innerBlock;

        public ActionBlock<T> InnerBlock => _innerBlock;

        public ITargetBlock<T> TargetBlock => _innerBlock;

        public override int? InputCount => _innerBlock.InputCount;

        public override int? OutputCount => null;

        public DeveActionBlock(string blockName, Action<T> action) : base(blockName)
        {
            _innerBlock = new ActionBlock<T>(WrapAction(action));
        }

        public DeveActionBlock(string blockName, Action<T> action, ExecutionDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new ActionBlock<T>(WrapAction(action), dataflowBlockOptions);
        }

        public DeveActionBlock(string blockName, Func<T, Task> action) : base(blockName)
        {
            _innerBlock = new ActionBlock<T>(WrapActionAsync(action));
        }

        public DeveActionBlock(string blockName, Func<T, Task> action, ExecutionDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new ActionBlock<T>(WrapActionAsync(action), dataflowBlockOptions);
        }
    }
}
