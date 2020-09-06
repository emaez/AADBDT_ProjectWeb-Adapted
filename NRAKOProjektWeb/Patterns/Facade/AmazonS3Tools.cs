using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.Facade
{
    public class AmazonS3Tools
    {
        private readonly string ACCESS_KEY_ID = "AKIAINSGF42Z4EKHRD2Q";
        private readonly string SECRET_ACCESS_KEY = "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";
        private readonly string BUCKET_NAME = "nrakoprojekt";

        private readonly RegionEndpoint REGION_ENDPOINT = RegionEndpoint.EUCentral1;


        public long UploadToS3(MemoryStream ms, string filename)
        {
            long size;
            using (var client = new AmazonS3Client(ACCESS_KEY_ID, SECRET_ACCESS_KEY, REGION_ENDPOINT))
            {

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = ms,
                    Key = filename,
                    BucketName = BUCKET_NAME,
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(client);
                fileTransferUtility.Upload(uploadRequest);

                size = client.GetObjectMetadataAsync(BUCKET_NAME, filename).Result.ContentLength;
            }

            return size;
        }
    }
}
