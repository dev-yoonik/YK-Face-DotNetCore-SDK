# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## v2.0.0

### Added

- FaceClient empty constructor that automatically reads the environment variables

### Changed

- ProcessAync function name to ProcessAsync
- VerifyAsync and VerifyImagesAsync return type to MatchingScore
- VerifyImages to use the Process and Verify instead of API call
- README.md badges
- FaceClient argument type from ConnectionInformation to IConnectionInformation

### Removed

- VerifyResponse class
- VerifyIdResponse class
- VerifyImagesResponse class
- VerifyImagesRequest class
- VerifyImages from the FaceEndpoints

## v1.0.1 - 2021-10-08

### Changed

- Service.Client dependency to newer version
- Face API base url and x-api-key are environment variables

## v1.0.0 - 2021-09-27

### Added

- Coverage of all Face API endpoints
- Sample Project
- Test Project