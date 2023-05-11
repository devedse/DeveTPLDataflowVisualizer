using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DLPR.Detection.WholeFlow.TPLDataflowWrappers
{
    public class DeveTransformManyBlock<TInput, TOutput> : DeveBaseBlock<TInput, TOutput>, IDeveBaseSourceBlock<TOutput>, IDeveBaseTargetBlock<TInput>
    {
        private TransformManyBlock<TInput, TOutput> _innerBlock;

        public TransformManyBlock<TInput, TOutput> InnerBlock => _innerBlock;

        public ISourceBlock<TOutput> SourceBlock => _innerBlock;

        public ITargetBlock<TInput> TargetBlock => _innerBlock;

        public override int? InputCount => _innerBlock.InputCount;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveTransformManyBlock(string blockName, Func<TInput, IEnumerable<TOutput>> transform) : base(blockName)
        {
            _innerBlock = new TransformManyBlock<TInput, TOutput>(WrapTransform(transform));
        }

        public DeveTransformManyBlock(string blockName, Func<TInput, IEnumerable<TOutput>> transform, ExecutionDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new TransformManyBlock<TInput, TOutput>(WrapTransform(transform), dataflowBlockOptions);
        }

        public DeveTransformManyBlock(string blockName, Func<TInput, Task<IEnumerable<TOutput>>> transform) : base(blockName)
        {
            _innerBlock = new TransformManyBlock<TInput, TOutput>(WrapTransformAsync(transform));
        }

        public DeveTransformManyBlock(string blockName, Func<TInput, Task<IEnumerable<TOutput>>> transform, ExecutionDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new TransformManyBlock<TInput, TOutput>(WrapTransformAsync(transform), dataflowBlockOptions);
        }
    }
}
