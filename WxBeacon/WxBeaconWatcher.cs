using System;
using Windows.Devices.Bluetooth.Advertisement;

namespace WxBeacon {
	/// <summary>
	/// Detect and notify WxBeacon weather data
	/// </summary>
	public class WxBeaconWatcher: IDisposable {
		/// <summary>
		/// Watcher for WxBeacon Bluetooth LE Advertisement
		/// </summary>
		private BluetoothLEAdvertisementWatcher bluetoothLEAdvertisementWatcher;

		/// <summary>
		/// WxBeaconReceived Event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="beacon"></param>
		public delegate void WxBeaconReceivedEventHandler(object sender, WxBeaconInfo beacon);

		/// <summary>
		/// Gets/Sets event on WxBeacon weather data received
		/// </summary>
		public event WxBeaconReceivedEventHandler Received;

		/// <summary>
		/// Gets current runnning status of WxBeaconWatcher
		/// </summary>
		public bool Started
		{
			get
			{
				return bluetoothLEAdvertisementWatcher.Status == BluetoothLEAdvertisementWatcherStatus.Started;
			}
		}

		/// <summary>
		/// Creates new instance of WxBeaconWatcher
		/// </summary>
		public WxBeaconWatcher() {
			//Initializes iBeacon Watcher
			bluetoothLEAdvertisementWatcher = new BluetoothLEAdvertisementWatcher();
			bluetoothLEAdvertisementWatcher.AdvertisementFilter = WxBeacon.CreateAdvertisementFilter();
			bluetoothLEAdvertisementWatcher.ScanningMode = BluetoothLEScanningMode.Active;
			bluetoothLEAdvertisementWatcher.Received += BluetoothLEAdvertisementWatcher_Received;
		}

		/// <summary>
		/// Called when WxBeacon advertisement packet received
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void BluetoothLEAdvertisementWatcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args) {
			if (!WxBeacon.IsWxBeaconPacket(args.Advertisement)) {
				return;
			}
			if (Received != null) {
				//Raise event
				Received(this, WxBeacon.Parse(args.Advertisement));
			}
		}

		/// <summary>
		/// Start watching WxBeacon
		/// </summary>
		public void Start() {
			if (Started) {
				return;
			}
			bluetoothLEAdvertisementWatcher.Start();
		}

		/// <summary>
		/// Stop watching WxBeacon
		/// </summary>
		public void Stop() {
			if (Started) {
				bluetoothLEAdvertisementWatcher.Stop();
			}
		}

		/// <summary>
		/// Called when instance disposed
		/// </summary>
		public void Dispose() {
			Stop();
		}

	}
}
