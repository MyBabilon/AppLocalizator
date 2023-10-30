# App Localizator

A powerful tool that provides the ability to translate text on the fly, which can be useful when developing RestAPI with multilingualism

## Instalation
```nuget
dotnet add package OftobTech.AppLocalizator
```
or via Nuget tools in Visual Studio

## Basic usage

Create `Lang` folder in root of published project

Inside this folder you can create translation files, for example
```
en.txt
jp.txt
ar.txt
```

Then you cant use this tools in code

```C#
using OftobTech.AppLocalizator;

OftobTech.AppLocalizator.Reader.UpdateLangs();

var message = T.Compile("[Text to transalete]");
Console.WriteLine( message );
```

### Language file format

```txt
[sentence for translate] : [translated sentence]
[sentence for translate] : [translated sentence]
[sentence for translate] : [translated sentence]
....
```

Each translated sentence must end with a transition to a new line, the text separator for the translation and the translated text is a colon

## Configuretion
The library also supports minimal configuration, which allows you to configure the default language and the location of the folder with translation files relative to the project.

If the config file has not been created, the default one is taken:

```PHP
# The default language will be used if it 
# was not passed an additional parameter when using the library
DefaultLang: en

# The address of the language files can be either relative or absolute
# `App` - the root directory of the application
LangsFilesPath: Langs
```

You can manually create the file data using the following path ``config/lang.conf``

Or call the following construction:

```C#
using OftobTech.AppLocalizator;

OftobTech.AppLocalizator.Config.PublishConfig();
```
Which will automatically create the default config

## Changing the language during execution

To change the language during the execution of the program, you need to call the ``setLang(string lang)`` method

```C#
using OftobTech.AppLocalizator;

T.setLang(item.DeviceLanguage);
```

It can also be called every time before translation, (it is worth considering that the last exposed language is remembered)

```C#
using OftobTech.AppLocalizator;

T.setLang(item.DeviceLanguage).Compile("[Text to transalete]");
```

## Line translations with parameters

To translate strings with parameters in the translation file, you need to embed parameters into the translation string, this is done by wrapping some parameter identifier in curly brackets
Example:

```PHP
Hello : Hello {parametr_1}  {parametr_2}
```

To translate such a string into the Compile method, the for Replace attribute passes an object with the ```Dictionary<string, string>``` type, where the key is the parameter identifier (without curly brackets) and the value needs to be replaced with

Example
```C#
using OftobTech.AppLocalizator;

var params = new Dictionary<string, string>(){
    {"parametr_1", "beautiful"},
    {"parametr_2", "world!"},
}

T.setLang(item.DeviceLanguage).Compile("Hello", params);
```


## Strict mode
The library supports 2 translation modes normal mode and timing, in normal mode, if no suitable strings were found in the translation files, the string that was passed is returned, in strict mode, if the translation string is not found, NULL will be returned

Example
```C#
using OftobTech.AppLocalizator;

T.Compile("[Text to transalete]", true);
/// OR
T.Compile("[Text to transalete]", params ,true);
```

## Updating language files

There are cases when there is a need to add or update language files, but you cannot restart the application, then the UpdateLangs() method can come to your aid, which allows you to update translations in the application's memory on the fly
```C#
using OftobTech.AppLocalizator;

OftobTech.AppLocalizator.Reader.UpdateLangs();
```
