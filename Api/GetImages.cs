using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class GetImages
    {
        private readonly IGetBlob _getBlob;

        public GetImages(IGetBlob getBlob)
        {
            _getBlob = getBlob;
        }

        [FunctionName("GetImages")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var imageUrls = await _getBlob.BlobGetterAsync("images");

            return new OkObjectResult(imageUrls);
        }
    }
}
