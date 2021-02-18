using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
            //// Setup the connection to the storage account
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            //// Connect to the blob storage
            //CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
            //// Connect to the blob container
            //CloudBlobContainer container = serviceClient.GetContainerReference($"{containerName}");
            //// Connect to the blob file
            ////CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");

            //var result = container.l ListBlobsSegmentedAsync(string.Empty, )

            //return container;


            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            //string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
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
