using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Client
{
    public class PubSubCls
    {
        public async Task PublishMessageWithCustomAttributesAsync(string projectId, string topicId, byte[] bytes)
        {
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            //var start = ByteString.CopyFrom("{ \"TestData\": \"", System.Text.Encoding.Unicode);
            //var end = ByteString.CopyFrom("\"}", System.Text.Encoding.Unicode);

            //using var tempStream = new MemoryStream();
            //start.WriteTo(tempStream);
            //ByteString.CopyFrom(bytes).WriteTo(tempStream);
            //end.WriteTo(tempStream);
            //{ Data = "DFWERSDFADSFEDGERGSFEFGF" }
            var pubsubMessage = new PubsubMessage
            {
                // The data is any arbitrary ByteString. Here, we're using text.
                //Data = ByteString.CopyFromUtf8("{ \"TestData\":\"Hello Kevin Here \" }")//,
                Data = ByteString.CopyFrom(bytes)
            // The attributes provide metadata in a string-to-string dictionary.
            //Attributes =
            //{
            //    { "year", "2020" },
            //    { "author", "unknown" }
            //}
            };
            string message = await publisher.PublishAsync(pubsubMessage);
            Console.WriteLine($"Published message {message}");
        }

        public async Task PublishMessageWithCustomAttributesAsync(string projectId, string topicId, string bytes)
        {
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            //var start = ByteString.CopyFrom("{ \"TestData\": \"", System.Text.Encoding.Unicode);
            //var end = ByteString.CopyFrom("\"}", System.Text.Encoding.Unicode);

            //using var tempStream = new MemoryStream();
            //start.WriteTo(tempStream);
            //ByteString.CopyFrom(bytes).WriteTo(tempStream);
            //end.WriteTo(tempStream);
            //{ Data = "DFWERSDFADSFEDGERGSFEFGF" }
            var pubsubMessage = new PubsubMessage
            {
                // The data is any arbitrary ByteString. Here, we're using text.
                //Data = ByteString.CopyFromUtf8("{ \"TestData\":\"Hello Kevin Here \" }")//,
                Data = ByteString.CopyFromUtf8(bytes)
                // The attributes provide metadata in a string-to-string dictionary.
                //Attributes =
                //{
                //    { "year", "2020" },
                //    { "author", "unknown" }
                //}
            };
            string message = await publisher.PublishAsync(pubsubMessage);
            Console.WriteLine($"Published message {message}");
        }
    }
}
