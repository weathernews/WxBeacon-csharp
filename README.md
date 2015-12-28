# WxBeacon-csharp

This is .NET library for Universal Windows Platform reading weather data from WxBeacon.

## Pre Requirements

* Windows 10 (PC/Phone/IoT)
* Visual Studio 2015 (with Universal Windows App support)
* Bluetooth 4.0 Adapter (with Bluetooth LE support)
* WxBeacon weather sensor

## Basic Usage

### for Begginers, Phone and IoT

- Clone this repository
- Open WxBeacon.sln
- Edit WxBeaconApp project

### for PC (x64)

- Download pre-built wxbeacon.dll binary from releases
- Create new "Universal Windows App" project/solution.
- Add reference to wxbeacon.dll
- Add ```using WxBeacon;``` directive.
- Create ```WxBeaconWatcher``` instance field in your application.
- Call ```WxBeaconWatcher#Start()```, ```WxBeaconWatcher#Stop()``` method
- Implements ```WxBeaconWatcher.Received``` event handler.

