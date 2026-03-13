[![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/pipiscrew/mail_desktop.svg)](https://github.com/pipiscrew/mail_desktop/releases)
[![GitHub All Releases](https://img.shields.io/github/downloads/pipiscrew/mail_desktop/total.svg)](https://github.com/pipiscrew/mail_desktop/releases)

# mail_desktop - keep privacy to mails & messengers

**02 JAN 2024** - [mail_desktop_electron](https://github.com/pipiscrew/mail_desktop_electron/) born  

Today I’m thinking, why not creating a tabbed application that is isolated from any other website (each tab has its own cookies) and keep there all the mails / messengers (?).. of course you can use a second browser for that but you have to check it, plus is not providing tab isolation.. This application stays at tray, upon mail and what user advised as notification_keyword, getting an icon/sound alert… Inspired by [Kiwi for Gmail](https://www.kiwiforgmail.com/) (Kiwi 28/08/2023 turn to Electron).

For gmail login, for one time, user has to set the **agent alternative** then after success login, restore it to **agent chrome**.

With the help of the [CefSharp](https://github.com/cefsharp/CefSharp/) library common known as .NET bindings for the Chromium Embedded Framework.

![0](https://user-images.githubusercontent.com/3852762/75326046-95c61180-5882-11ea-8bad-a8948f7a0475.jpg)

![1](https://user-images.githubusercontent.com/3852762/75325763-0de00780-5882-11ea-9b22-a838b99f5b98.png)

<br>

Read the included PDF for details, user has to **download manually** the dependencies from the following **nuget links** :   
* [chromiumembeddedframework.runtime.win-x64](https://www.nuget.org/packages/chromiumembeddedframework.runtime.win-x64/)   
* [CefSharp.Common](https://www.nuget.org/packages/CefSharp.Common/145.0.260)  
* [CefSharp.WinForms](https://www.nuget.org/packages/CefSharp.WinForms/145.0.260)  
* [CefSharp source](https://github.com/cefsharp/CefSharp/releases/tag/v145.0.260) - no needed, left for reference  

&nbsp;

## This project uses the following 3rd-party dependencies :  
* [Cefsharp](https://github.com/cefsharp/CefSharp) - ( [howto](https://github.com/cefsharp/CefSharp/wiki/Quick-Start) )
* [VC++ 2022](https://docs.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022)  

&nbsp;
## This project is no longer maintained
Copyright (c) 2021 [PipisCrew](http://pipiscrew.com)  

Licensed under the [MIT license](http://www.opensource.org/licenses/mit-license.php).

