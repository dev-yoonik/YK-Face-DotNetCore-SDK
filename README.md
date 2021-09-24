
![https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png](https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png)

# YooniK Face API Client DotNet SDK

[![License](https://img.shields.io/pypi/l/yk_face.svg)](https://github.com/dev-yoonik/YK-Face-DotNetCore-SDK/blob/master/LICENSE)

This repository implements an integration SDK to facilitate the consumption of the YooniK.Face API, an [YooniK Services](https://yoonik.me) offering.

For more information please [contact us](mailto:info@yoonik.me).

## Getting started

To import the latest this solution into your project, enter the following command in the NuGet Package Manager Console in Visual Studio:

For other installation methods, see [YooniK.Face.Client Nuget](https://www.nuget.org/packages/YooniK.Face.Client/)

```
PM> Install-Package YooniK.Face.Client
```



## Example


Keep in mind that the following FaceClient methods use Yoonik.Services.Client which uses HttpClient to handle the API calls. Here its used 'EnsureSuccessStatusCode()', in case an HTTP Response has a non success HTTP Status Code a exception is thrown and can be caught right here at this abstraction level.

For more information feel free to dig around at [YooniK.Services.Client](https://github.com/dev-yoonik/YK-Services-Client-DotNetCore/)

Use it:

```csharp

// Example data
string baseUrl = "YOUR-API-ENDPOINT";
string subscriptionKey = "YOUR-X-API-KEY-ENDPOINT";

var faceConnectionInformation = new ConnectionInformation(baseUrl, subscriptionKey);

// Instantiates the FaceClient, passing its server information to establish a connection
FaceClient faceClient = new FaceClient(faceConnectionInformation);

// Verifies the faces similarity between two images in base 64
VerifyImagesResponse verifyImages = await faceClient.VerifyImagesAsync(firstPhotoInBase64, secondPhotoInBase64);
Console.WriteLine($"Similarity Score: { verifyImages.Score }");

// Processes all the image containing faces, and returning them in a list. This photo only contains one face. 
List<ProcessResponse> process = await faceClient.ProcessAync(firstPhotoInBase64);
string firstPhotoTemplate = process.Count == 1 ? process[0].Template : null;

List<ProcessResponse> process2 = await faceClient.ProcessAync(secondPhotoInBase64);
string secondPhotoTemplate = process2.Count == 1 ? process2[0].Template : null;

// Verifies the faces similarity between the extracted biometric template from the processed images
VerifyResponse verify = await faceClient.VerifyAsync(firstPhotoTemplate, secondPhotoTemplate);
Console.WriteLine($"Similarity Score (w/Template): {verify.Score}");

// If you're interest in using YooniK.Face API for identification purposes, please visit our sample project.

```


### If you're interest in using YooniK.Face Api for identification purposes, please visit our sample project.