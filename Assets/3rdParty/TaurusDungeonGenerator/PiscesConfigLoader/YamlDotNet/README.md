# YamlDotNet

| Travis | Appveyor | NuGet |
|--------|----------|-------|
|[![Travis CI](https://travis-ci.org/aaubry/YamlDotNet.svg?branch=master)](https://travis-ci.org/aaubry/YamlDotNet/builds#)|[![Build status](https://ci.appveyor.com/api/projects/status/github/aaubry/yamldotnet?svg=true)](https://ci.appveyor.com/project/aaubry/yamldotnet/branch/master)|  [![NuGet](https://img.shields.io/nuget/v/YamlDotNet.svg)](https://www.nuget.org/packages/YamlDotNet/)


YamlDotNet is a .NET library for YAML. YamlDotNet provides low level parsing and emitting of YAML as well as a high level object model similar to XmlDocument. A serialization library is also included that allows to read and write objects from and to YAML streams.

Currently, YamlDotNet supports [version 1.1 of the YAML specification](http://yaml.org/spec/1.1/).

## What is YAML?

YAML, which stands for "YAML Ain't Markup Language", is described as "a human friendly data serialization standard for all programming languages". Like XML, it allows to represent about any kind of data in a portable, platform-independent format. Unlike XML, it is "human friendly", which means that it is easy for a human to read or produce a valid YAML document.

## The YamlDotNet library

The library has now been successfully used in multiple projects and is considered fairly stable.

## More information

More information can be found in the [project's wiki](https://github.com/aaubry/YamlDotNet/wiki).

## Installing

Just install the [YamlDotNet NuGet package](http://www.nuget.org/packages/YamlDotNet/):

```
PM> Install-Package YamlDotNet
```

If you need signed assemblies, install the [YamlDotNet.Signed NuGet package](http://www.nuget.org/packages/YamlDotNet.Signed/) instead:

```
PM> Install-Package YamlDotNet.Signed
```

If you do not want to use NuGet, you can [download binaries here](https://ci.appveyor.com/project/aaubry/yamldotnet).

YamlDotNet is also available on the [Unity Asset Store](https://www.assetstore.unity3d.com/en/#!/content/36292).

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## Release notes

[Release notes for the latest version](RELEASE_NOTES.md)
