using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

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
            var blobClients = await _getBlob.BlobGetterAsync("images");

            var imgBase64List = new List<string>();

            if (blobClients == null || blobClients.Count < 1)
            {
                imgBase64List.Add("https://img.drivemag.net/media/default/0001/98/urus-pickup-truck-1-4731-default-large.jpeg");
            }
            else
            {
                foreach (var blobClient in blobClients)
                {
                    byte[] bytes = new byte[] { };
                    using (var ms = new MemoryStream())
                    {
                        if (blobClient.Exists())
                        {
                            await blobClient.DownloadToAsync(ms);
                            bytes = ms.ToArray();
                        }
                    }
                    var img64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    imgBase64List.Add(img64String);
                }
            }

            return new OkObjectResult(imgBase64List);
        }
    }
}
