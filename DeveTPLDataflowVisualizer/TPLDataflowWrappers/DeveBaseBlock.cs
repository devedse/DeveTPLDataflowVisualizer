using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public abstract class DeveBaseBlock<TInput, TOutput> : IDeveBaseBlock
    {
        private int _processingCount;
        private int _processedCount;

        public virtual int ProcessingCount => _processingCount;
        public virtual int ProcessedCount => _processedCount;
        public abstract int? InputCount { get; }
        public abstract int? OutputCount { get; }


        protected List<IDeveBaseBlock> _targets = new List<IDeveBaseBlock>();
        public IList<IDeveBaseBlock> Targets => _targets;

        public DeveBaseBlock(string blockName)
        {
            BlockName = blockName;
        }

        public IEnumerable<IDeveBaseBlock> RecursivelyGetAllTargetsDistincted()
        {
            return RecursivelyGetAllTargets().Distinct();
        }

        public IEnumerable<IDeveBaseBlock> RecursivelyGetAllTargets()
        {
            yield return this;
            foreach (var target in Targets)
            {
                foreach (var t in target.RecursivelyGetAllTargets())
                {
                    yield return t;
                }
            }
        }


        public string ProgressString => DeveTPLDataFlowStringCreator.GenerateProgressString(this);

        public string BlockName { get; }

        protected Action<TInput> WrapAction(Action<TInput> action)
        {
            return input =>
            {
                Interlocked.Increment(ref _processingCount);
                action(input);
                Interlocked.Decrement(ref _processingCount);
                Interlocked.Increment(ref _processedCount);
            };
        }

        protected Func<TInput, Task> WrapActionAsync(Func<TInput, Task> action)
        {
            return async input =>
            {
                Interlocked.Increment(ref _processingCount);
                await action(input);
                Interlocked.Decrement(ref _processingCount);
                Interlocked.Increment(ref _processedCount);
            };
        }


        protected Func<TInput, TOutput> WrapTransform(Func<TInput, TOutput> transform)
        {
            return input =>
            {
                Interlocked.Increment(ref _processingCount);
                TOutput output = transform(input);
                Interlocked.Decrement(ref _processingCount);
                Interlocked.Increment(ref _processedCount);

                return output;
            };
        }

        protected Func<TInput, Task<TOutput>> WrapTransformAsync(Func<TInput, Task<TOutput>> transform)
        {
            return async input =>
            {
                Interlocked.Increment(ref _processingCount);
                TOutput output = await transform(input);
                Interlocked.Decrement(ref _processingCount);
                Interlocked.Increment(ref _processedCount);

                return output;
            };
        }



        protected Func<TInput, IEnumerable<TOutput>> WrapTransform(Func<TInput, IEnumerable<TOutput>> transform)
        {
            return input =>
            {
                var output = WrapIEnumerable(transform(input));
                return output;
            };
        }

        protected Func<TInput, Task<IEnumerable<TOutput>>> WrapTransformAsync(Func<TInput, Task<IEnumerable<TOutput>>> transform)
        {
            return async input =>
            {
                var output = WrapIEnumerable(await transform(input));
                return output;
            };
        }

        private IEnumerable<TOutput> WrapIEnumerable(IEnumerable<TOutput> input)
        {
            Interlocked.Increment(ref _processingCount);
            foreach (var item in input)
            {
                yield return item;
            }
            Interlocked.Decrement(ref _processingCount);
            Interlocked.Increment(ref _processedCount);
        }
    }
}
