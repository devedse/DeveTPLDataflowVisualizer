using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public class DeveTransformBlock<TInput, TOutput> : DeveBaseBlock<TInput, TOutput>, IDeveBaseSourceBlock<TOutput>, IDeveBaseTargetBlock<TInput>
    {
        private TransformBlock<TInput, TOutput> _innerBlock;

        public TransformBlock<TInput, TOutput> InnerBlock => _innerBlock;

        public ISourceBlock<TOutput> SourceBlock => _innerBlock;

        public ITargetBlock<TInput> TargetBlock => _innerBlock;

        public override int? InputCount => _innerBlock.InputCount;

        public override int? OutputCount => _innerBlock.OutputCount;

        public DeveTransformBlock(string blockName, Func<TInput, TOutput> transform) : base(blockName)
        {
            _innerBlock = new TransformBlock<TInput, TOutput>(WrapTransform(transform));
        }

        public DeveTransformBlock(string blockName, Func<TInput, TOutput> transform, ExecutionDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new TransformBlock<TInput, TOutput>(WrapTransform(transform), dataflowBlockOptions);
        }

        public DeveTransformBlock(string blockName, Func<TInput, Task<TOutput>> transform) : base(blockName)
        {
            _innerBlock = new TransformBlock<TInput, TOutput>(WrapTransformAsync(transform));
        }

        public DeveTransformBlock(string blockName, Func<TInput, Task<TOutput>> transform, ExecutionDataflowBlockOptions dataflowBlockOptions) : base(blockName)
        {
            _innerBlock = new TransformBlock<TInput, TOutput>(WrapTransformAsync(transform), dataflowBlockOptions);
        }
    }
}
