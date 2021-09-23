
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

var serverInfo = new ConnectionInformation(baseUrl, subscriptionKey);

FaceClient faceClient = new FaceClient(serverInfo);

string base64Marcelo1 = ImageToBase64String(Marcelo1stPhotoPath);
string base64Marcelo2 = ImageToBase64String(Marcelo2ndPhotoPath);

VerifyImagesResponse verifyImages = await faceClient.VerifyImagesAsync(base64Marcelo1, base64Marcelo2);
Console.WriteLine($"Similarity Score: { verifyImages.Score }");

List<ProcessResponse> process = await faceClient.ProcessAync(base64Marcelo1);
string Marcelo1Template = process.Count == 1 ? process[0].Template : null;

List<ProcessResponse> process2 = await faceClient.ProcessAync(base64Marcelo2);
string Marcelo2Template = process2.Count == 1 ? process2[0].Template : null;

VerifyResponse verify = await faceClient.VerifyAsync(Marcelo1Template, Marcelo2Template);
Console.WriteLine($"Similarity Score (w/Template): {verify.Score}");

string galleryGuid = Guid.NewGuid().ToString();
string personGuid = Guid.NewGuid().ToString();
string personGuid2 = Guid.NewGuid().ToString();

await faceClient.AddGalleryAsync(galleryGuid);
await faceClient.AddPersonToGalleryAsync(galleryGuid, personGuid, Marcelo1Template);
await faceClient.AddPersonToGalleryAsync(galleryGuid, personGuid2, Marcelo2Template);

TemplateResponse template = await faceClient.GetPersonTemplateFromGalleryAsync(galleryGuid, personGuid);
Console.WriteLine(template.Template);

EnrolledIdsResponse enrolledIds = await faceClient.GetEnrolledPersonsAsync(galleryGuid);
foreach (string enrolledId in enrolledIds)
    Console.Write(enrolledId);

await faceClient.RemovePersonFromGalleryAsync(galleryGuid, personGuid);
await faceClient.RemoveGalleryAsync(galleryGuid);

```
