// See https://aka.ms/new-console-template for more information

using ReceiveMessage;

PullMessagesAsyncSample sample = new();
sample.PullMessagesAsync("g-npe-prj-rhmod-dev", "test-tpm-sub", true);
Console.Read();