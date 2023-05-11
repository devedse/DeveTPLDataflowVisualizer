using System;
using System.Threading.Tasks.Dataflow;

namespace DeveTPLDataflowVisualizer.TPLDataflowWrappers
{
    public static class DeveBaseSourceBlockExtensions
    {
        public static void LinkTo<TOutput>(this IDeveBaseSourceBlock<TOutput> source, IDeveBaseTargetBlock<TOutput> target)
        {
            source.LinkTo(target);
        }

        public static void LinkTo<TOutput>(this IDeveBaseSourceBlock<TOutput> source, IDeveBaseTargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            source.LinkTo(target, linkOptions);
        }

        public static void LinkTo<TOutput>(this IDeveBaseSourceBlock<TOutput> source, IDeveBaseTargetBlock<TOutput> target, Predicate<TOutput> predicate)
        {
            source.LinkTo(target, predicate);
        }

        public static void LinkTo<TOutput>(this IDeveBaseSourceBlock<TOutput> source, IDeveBaseTargetBlock<TOutput> target, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            source.LinkTo(target, linkOptions, predicate);
        }
    }
}
