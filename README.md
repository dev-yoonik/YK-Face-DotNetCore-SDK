
![https://yk-website-images.s3.eu-west-1.amazonaws.com/LogoV4_TRANSPARENT.png](https://yk-website-images.s3.eu-west-1.amazonaws.com/LogoV4_TRANSPARENT.png)

# YooniK Face API Client DotNetCore SDK

[![License](https://img.shields.io/github/license/dev-yoonik/YK-Face-DotNetCore-SDK)](https://github.com/dev-yoonik/YK-Face-SDK-DotNetCore/blob/main/LICENSE)
[![Version](https://img.shields.io/nuget/v/YooniK.Face.Client)](https://www.nuget.org/packages/YooniK.Face.Client/)

This repository implements an integration SDK to facilitate the consumption of the YooniK.Face API, an [YooniK Services](https://www.yoonik.me) offering.

## Getting started

To import the latest this solution into your project, enter the following command in the NuGet Package Manager Console in Visual Studio:

For other installation methods, see [YooniK.Face.Client Nuget](https://www.nuget.org/packages/YooniK.Face.Client/)

```
PM> Install-Package YooniK.Face.Client
```



## Example

FaceClient methods depend on Yoonik.Services.Client that uses HttpClient to handle the API calls.

For more information feel free to take a look at [YooniK.Services.Client](https://github.com/dev-yoonik/YK-Services-Client-DotNetCore/)

Use it:

You can also use environment variables (YK_FACE_BASE_URL and YK_FACE_X_API_KEY) to authenticate. Machine restart could be required.

```csharp

// Edit your access credentials
string baseUrl = "YOUR-API-ENDPOINT";
string subscriptionKey = "YOUR-X-API-KEY";
var faceInformation = new ConnectionInformation(baseUrl, subscriptionKey);

// If you have the environment variables set, remove the above and use "var faceClient = new FaceClient()"
var faceClient = new FaceClient(faceInformation);

// Verifies the faces similarity between two images in base 64
MatchingScore imagesMatchingScore = await faceClient.VerifyImagesAsync(firstPhotoInBase64, secondPhotoInBase64);
Console.WriteLine($"Similarity Score: { imagesMatchingScore.Score }");

// Processes all the image containing faces, and returning them in a list. This photo only contains one face. 
List<ProcessResponse> process = await faceClient.ProcessAync(firstPhotoInBase64);
string firstPhotoTemplate = process.Count == 1 ? process[0].Template : null;

List<ProcessResponse> process2 = await faceClient.ProcessAync(secondPhotoInBase64);
string secondPhotoTemplate = process2.Count == 1 ? process2[0].Template : null;

// Verifies the faces similarity between the extracted biometric template from the processed images
MatchingScore templatesMatchingScore = await faceClient.VerifyAsync(firstPhotoTemplate, secondPhotoTemplate);
Console.WriteLine($"Similarity Score (w/Template): {templatesMatchingScore.Score}");
```


 If you're interested in using YooniK.Face API for identification purposes, please visit our [sample project](https://github.com/dev-yoonik/YK-Face-SDK-DotNetCore/tree/main/YooniK.Face/YooniK.Face.Client.Sample).

## YooniK Face API Details

For a complete specification of our Face API please check the [swagger file](https://dev-yoonik.github.io/YK-Face-Documentation/).

## Contact & Support

For more information and trial licenses please [contact us](mailto:tech@yoonik.me) or join us at our [discord community](https://discord.gg/SqHVQUFNtN).
