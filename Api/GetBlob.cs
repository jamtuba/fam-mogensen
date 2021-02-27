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
        public async Task<List<BlobClient>> BlobGetterAsync(string containerName)
        {
            string connectionString = Environment.GetEnvironmentVariable("BlobEndPoint");

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync();

            var blobClients = new List<BlobClient>();

            await foreach (var blob in blobs)
            {
                var blobClient = containerClient.GetBlobClient(blob.Name);
                blobClients.Add(blobClient);
            }

            return blobClients;
        }
    }
}
