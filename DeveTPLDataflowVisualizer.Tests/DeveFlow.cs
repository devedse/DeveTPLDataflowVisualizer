using DLPR.Detection.WholeFlow.TPLDataflowWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Xunit;

namespace DeveTPLDataflowVisualizer.Tests
{
    public class DeveFlow
    {
        [Fact]
        public async Task FlowWorksAsExpected()
        {
            //Arrange
            var b1_extractFramesFromVideo = new DeveTransformManyBlock<int, string>("Video -> Frame", (input) =>
            {
                return Enumerable.Range(0, input).Select(t => $"Super test: {t}");
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1,
                EnsureOrdered = true
            });
            var b1_broadcast = new DeveBroadcastBlock<string>("Broadcast Frame", frame => frame + "");

            var b2_findBoxes = new DeveTransformBlock<string, string>("Frame -> Frame + Boxes", input =>
            {
                return $"Blahblah...: {input}";
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 40,
                EnsureOrdered = true
            });

            var b2_broadcast = new DeveBroadcastBlock<string>("Broadcast Frame + Boxes", frame => frame);

            var b3_findLicensePlates = new DeveTransformBlock<string, string>("Frame + Boxes -> Frame + LP's", input =>
            {
                return input;
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1,
                EnsureOrdered = true
            });

            var b3_broadcast = new DeveBroadcastBlock<string>("Broadcast Frame + LP's", frame => frame);

            var b4_licensePlateExtraInfoDetector = new DeveTransformBlock<string, string>("Frame + LP's -> Frame + LP + Info", input =>
            {
                return input;
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 8,
                EnsureOrdered = false
            });

            var b4_broadcast = new DeveBroadcastBlock<string>("Broadcast Frame + LP + Info", frame => frame);

            var outputCollection = new List<string>();
            var b5_storeFramesToVideo = new DeveActionBlock<string>("Write Frame to Video", input =>
            {
                outputCollection.Add(input);
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1,
                EnsureOrdered = true
            });

            int frame1Count = 0;
            int frame2Count = 0;
            int frame3Count = 0;
            int frame4Count = 0;

            var b1_post_saveExtractedImages = new DeveActionBlock<string>("Write Frame", input =>
            {
                frame1Count++;
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1
            });

            var b2_post_saveImagesWithBoxes = new DeveActionBlock<string>("Write Frame + Boxes", input =>
            {
                frame2Count++;
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1
            });


            var b3_post_saveLicensePlatesToImage = new DeveActionBlock<string>("Write Frame + LP's", input =>
            {
                frame3Count++;
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1
            });


            var b4_post_saveLicensePlatesToImage = new DeveActionBlock<string>("Write Frame + LP's + Infos", input =>
            {
                frame4Count++;
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1
            });



            // Link the blocks
            b1_extractFramesFromVideo.LinkTo(b1_broadcast, new DataflowLinkOptions { PropagateCompletion = true });
            b1_broadcast.LinkTo(b2_findBoxes, new DataflowLinkOptions { PropagateCompletion = true });
            b1_broadcast.LinkTo(b1_post_saveExtractedImages, new DataflowLinkOptions { PropagateCompletion = true });
            b2_findBoxes.LinkTo(b2_broadcast, new DataflowLinkOptions { PropagateCompletion = true });
            b2_broadcast.LinkTo(b3_findLicensePlates, new DataflowLinkOptions { PropagateCompletion = true });
            b2_broadcast.LinkTo(b2_post_saveImagesWithBoxes, new DataflowLinkOptions { PropagateCompletion = true });
            b3_findLicensePlates.LinkTo(b3_broadcast, new DataflowLinkOptions { PropagateCompletion = true });
            b3_broadcast.LinkTo(b4_licensePlateExtraInfoDetector, new DataflowLinkOptions { PropagateCompletion = true });
            b3_broadcast.LinkTo(b3_post_saveLicensePlatesToImage, new DataflowLinkOptions { PropagateCompletion = true });
            b4_licensePlateExtraInfoDetector.LinkTo(b4_broadcast, new DataflowLinkOptions { PropagateCompletion = true });
            b4_broadcast.LinkTo(b5_storeFramesToVideo, new DataflowLinkOptions { PropagateCompletion = true });
            b4_broadcast.LinkTo(b4_post_saveLicensePlatesToImage, new DataflowLinkOptions { PropagateCompletion = true });

            //Act
            int outputCount = 65;
            b1_extractFramesFromVideo.InnerBlock.Post(outputCount);

            // Mark the first block as complete
            b1_extractFramesFromVideo.InnerBlock.Complete();

            await b5_storeFramesToVideo.InnerBlock.Completion;

            //Assert
            Assert.Equal(outputCount, outputCollection.Count);
            foreach (var output in outputCollection)
            {
                Assert.Contains("Blahblah", output);
            }

            Console.WriteLine("Completed");
        }
    }
}
