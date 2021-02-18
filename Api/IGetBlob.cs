using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Api
{
    public interface IGetBlob
    {
        Task<List<string>> BlobGetterAsync(string containerName);
    }
}