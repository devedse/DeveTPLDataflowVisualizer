using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks.Dataflow;

namespace DLPR.Detection.WholeFlow.TPLDataflowWrappers
{
    public static class DeveDataflowExtensions
    {
        public static Dictionary<ITargetBlock<TInput>, object> GetTargetInformation<TInput, TOutput>(ISourceBlock<TOutput> sourceBlock)
        {
            var sourceCoreField = sourceBlock.GetType().GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
            if (sourceCoreField != null)
            {
                var sourceCoreValue = sourceCoreField.GetValue(sourceBlock);

                if (sourceCoreValue != null)
                {
                    var targetRegistryField = sourceCoreValue.GetType().GetField("_targetRegistry", BindingFlags.NonPublic | BindingFlags.Instance);

                    if (targetRegistryField != null)
                    {
                        var targetRegistryValue = targetRegistryField.GetValue(sourceCoreValue);

                        if (targetRegistryValue != null)
                        {
                            var targetInformationField = targetRegistryValue.GetType().GetField("_targetInformation", BindingFlags.NonPublic | BindingFlags.Instance);

                            if (targetInformationField != null)
                            {
                                var targetInformationValue = targetInformationField.GetValue(targetRegistryValue);

                                if (targetInformationValue is Dictionary<ITargetBlock<TInput>, object> targetInformation)
                                {
                                    return targetInformation;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
