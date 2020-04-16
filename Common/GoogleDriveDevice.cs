using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Net;

namespace Common
    {
    public class GoogleDriveDevice
        {
        private static string[] Scopes = { DriveService.Scope.Drive };
        private string ApplicationName = "Lab1";

        private DriveService service;

        public GoogleDriveDevice()
            {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                }

            service = new DriveService(new BaseClientService.Initializer()
                {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
                });
            }

        public void UploadFile(string fileName)
            {
            var file = new Google.Apis.Drive.v3.Data.File()
                {
                Name = fileName,
                };

            DeleteFile(fileName);

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(fileName, FileMode.Open))
                {
                request = service.Files.Create(
                    file, stream, "text/plain");
                request.Upload();
                }
            }

        public void UploadFile(string fileName, string fileBody)
            {
            var file = new Google.Apis.Drive.v3.Data.File()
                {
                Name = fileName,
                };

            DeleteFile(fileName);

            FilesResource.CreateMediaUpload request;
            request = service.Files.Create(
                file,
                new MemoryStream(Encoding.UTF8.GetBytes(fileBody)),
                "text/plain");
            request.Upload();
            }

        public void DeleteFile(string fileName)
            {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            if (files != null && files.Count > 0)
                {
                foreach (var f in files)
                    {
                    if (f.Name == fileName)
                        {
                        try
                            {
                            service.Files.Delete(f.Id).Execute();
                            }
                        catch (Exception e)
                            {
                            Console.WriteLine("An error occurred: " + e.Message);
                            }
                        break;
                        }
                    }
                }
            }
        public bool CheckForInternetConnection()
            {
            try
                {
                using (var client = new WebClient())
                    using (client.OpenRead("http://google.com/generate_204")) 
                        return true; 
                }
            catch
                {
                return false;
                }
            }
        }
    }