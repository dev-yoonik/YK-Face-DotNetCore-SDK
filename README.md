
![https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png](https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png)

# YooniK Face API Client DotNet SDK

[![License](https://img.shields.io/pypi/l/yk_face.svg)](https://github.com/dev-yoonik/YK-Face-DotNetCore-SDK/blob/master/LICENSE)

This repository contains the necessary infrastructure to communicate with our FaceAPI in a very simple plug and play way, an [YooniK Services](https://yoonik.me) offering.

For more information please [contact us](mailto:info@yoonik.me).

## Getting started

To import the latest this solution into your project, enter the following command in the NuGet Package Manager Console in Visual Studio:

For other installation methods, see [YooniK Face Client Nuget](https://www.nuget.org/packages/YooniK.Face.Client/)

```
PM> Install-Package YooniK.Face.Client
```



## Example


Keep in mind that the following FaceClient methods use HttpClient to handle its API calls. We use 'EnsureSuccessStatusCode()', so in case an HTTP Response fails an exception is thrown and can be caught right here at this abstraction level.

For more information feel free to dig around at [YooniK Services Client](https://github.com/dev-yoonik/YK-Services-Client-DotNetCore/)

Use it:

```csharp
// Example function that parses an file image to base 64 string
public static string ImageToBase64String(string filepath)
{
    byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
    return Convert.ToBase64String(imageArray);
}

// (....)

// Example data
string baseUrl = "YOUR-API-ENDPOINT";
string subscriptionKey = "YOUR-X-API-KEY-ENDPOINT";
string firstPhoto = "YOUR-FIRST-PHOTO-FILEPATH";
string secondPhoto = "YOUR-SECOND-PHOTO-FILEPATH";

var faceConnectionInformation = new ConnectionInformation(baseUrl, subscriptionKey);

// Instantiates the FaceClient, passing its server information to establish a connection
FaceClient faceClient = new FaceClient(faceConnectionInformation);

// Represents the photo files in base 64 string
string firstPhotoInBase64 = ImageToBase64String(firstPhoto);
string secondPhotoInBase64 = ImageToBase64String(secondPhoto);

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

```
