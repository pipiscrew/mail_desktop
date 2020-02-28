[![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/pipiscrew/mail_desktop.svg)](https://github.com/pipiscrew/mail_desktop/releases)
[![GitHub All Releases](https://img.shields.io/github/downloads/pipiscrew/mail_desktop/total.svg)](https://github.com/pipiscrew/mail_desktop/releases)

# mail_desktop - keep privacy to mails & messengers

Today I’m thinking, why not creating a tabbed application that is isolated from any other website (each tab has its own cookies) and keep there all the mails / messengers (?).. of course you can use a second browser for that but you have to check it, plus is not providing tab isolation.. This application stays at tray, upon mail and what user advised as notification_keyword, getting an icon/sound alert… Inspired by [Kiwi for Gmail](https://www.kiwiforgmail.com/).

The application is for **advanced users** as the settings have to described on the **settings.ini** file.

With the help of the [CefSharp](https://github.com/cefsharp/CefSharp/) library common known as .NET bindings for the Chromium Embedded Framework.

![0](https://user-images.githubusercontent.com/3852762/75326046-95c61180-5882-11ea-8bad-a8948f7a0475.jpg)

![1](https://user-images.githubusercontent.com/3852762/75325763-0de00780-5882-11ea-9b22-a838b99f5b98.png)

<br>

Read the included PDF for details, user has to **dowload manualy** the dependencies from the following **nuget links** : <br>
[cef.redist.x64](https://www.nuget.org/packages/cef.redist.x64/79.1.36)<br> 
[CefSharp.Common](https://www.nuget.org/packages/CefSharp.Common/79.1.360)<br> 
[CefSharp.WinForms](https://www.nuget.org/packages/CefSharp.WinForms/79.1.360)<br> 


## This project uses the following 3rd-party dependency :<br>
-[Cefsharp](https://github.com/cefsharp/CefSharp)<br>
-[MS - VC++ 2015](https://www.microsoft.com/en-us/download/details.aspx?id=52685)
<br>
## This project is no longer maintained
Copyright (c) 2020 [PipisCrew](http://pipiscrew.com)

Licensed under the [MIT license](http://www.opensource.org/licenses/mit-license.php).
