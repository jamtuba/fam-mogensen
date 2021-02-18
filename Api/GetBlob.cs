using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api
{
    public class GetBlob : IGetBlob
    {
        public async Task<List<string>> BlobGetterAsync(string containerName)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync();

            var blobUrls = new List<string>();

            await foreach (var blob in blobs)
            {
                var blobClient = containerClient.GetBlobClient(blob.Name);
                blobUrls.Add(blobClient.Uri.AbsoluteUri);
            }

            return blobUrls;
        }
    }
}
