# VSMac-CodeDistribution

A Visual Studio for Mac extension that visually displays the distribution of code amongst all the projects in a given solution.

I have found this to be particularly useful when working with Xamarin projects in order to determine the volume of code that is shared between the target platforms.

![](https://raw.githubusercontent.com/ademanuele/vsmac-codedistribution/master/doc/preview.png "Preview")

## Installing

1. Download the extension from the [Releases](https://github.com/ademanuele/VSMac-CodeDistribution/releases) section.

2. In Visual Studio for Mac, open `Extension Manager -> Install from file...` and install the downloaded file.

![](https://raw.githubusercontent.com/ademanuele/vsmac-codecoverage/master/doc/extension_manager.png "Extension Manager")

3. Restart Visual Studio for Mac

4. Done.

In order access the code code distribution pad, navigate to `View -> Pads -> Code Distribution`.

## Planned TODOs

- Fix and release a console version
	- Enable code distribution to be gathered as part of a CI build

## Authors

* **Arthur Demanuele**

## License

This project is licensed under the MIT License - [full details](LICENSE.md).
