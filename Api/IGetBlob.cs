using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace Api
{
    public interface IGetBlob
    {
        Task<List<BlobClient>> BlobGetterAsync(string containerName);
    }
}