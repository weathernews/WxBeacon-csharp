﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WxBeacon;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WxBeaconApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		private WxBeaconWatcher wxBeaconWatcher = new WxBeaconWatcher();

		public MainPage()
        {
            this.InitializeComponent();
			wxBeaconWatcher.Received += WxBeaconWatcher_Found;
        }

		private void WxBeaconWatcher_Found(object sender, WxBeaconInfo beacon) {
			System.Diagnostics.Debug.WriteLine(beacon.ToString());
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			wxBeaconWatcher.Start();
		}

		private void Page_Unloaded(object sender, RoutedEventArgs e) {
			wxBeaconWatcher.Stop();
		}
	}
}
