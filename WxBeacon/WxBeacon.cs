using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace WxBeacon {
	/// <summary>
	/// Utility for operating WxeBeacon
	/// </summary>
	public class WxBeacon {
		private WxBeacon() {
			//Private constructor ignores creating instance
		}

		/// <summary>
		/// Beggining of raw bytes array at WxBeacon's Bluetooth LE advertisement packet
		/// </summary>
		private static readonly byte[] DataFilter = new byte[] {
			//Apple
			0x4c, 0x00,
			//iBeacon
			0x02, 0x15,
			//Proximity UUID
			0xc7, 0x22, 0xdb, 0x4c, 0x5d, 0x91, 0x18, 0x01, 0xbe, 0xb5, 0x00, 0x1c, 0x4d, 0xe7, 0xb3, 0xfd
		};

		/// <summary>
		/// Extracts WxBeacon raw packet data from BLE Advertisement packet
		/// </summary>
		/// <param name="advertisement"></param>
		/// <returns>null if packet does not contains WxBeacon data</returns>
		private static byte[] ExtractIBeaconData(BluetoothLEAdvertisement advertisement) {
			return (
				from
					d in advertisement.DataSections
				where
					d.StartsWith(DataFilter)
					&& (DataFilter.Length + 5) <= d.Data.Length
				select d
			).DefaultIfEmpty(null).FirstOrDefault()?.Data?.ToArray();
		}

		/// <summary>
		/// Returns specified advertisement is WxBeacon's data or not
		/// </summary>
		/// <param name="advertisement">Bluetooth LE Advertisement data</param>
		/// <returns>true if specified is WxBeacon data</returns>
		public static bool IsWxBeaconPacket(BluetoothLEAdvertisement advertisement) {
			return ExtractIBeaconData(advertisement) != null;
		}

		/// <summary>
		/// Extract WxBeacon data from specified Bluetooth LE Advertisement packet
		/// </summary>
		/// <param name="advertisement"></param>
		/// <returns>New instance of WxBeaconInfo</returns>
		public static WxBeaconInfo Parse(BluetoothLEAdvertisement advertisement) {
			var raw = ExtractIBeaconData(advertisement);
			if (raw == null) {
				throw new FormatException("Specified advertisement is not a WxBeacon's packet");
			}
			//Extracting major/minor/power from packet
			var major = raw[DataFilter.Length + 0] * 0xff + raw[DataFilter.Length + 1];
			var minor = raw[DataFilter.Length + 2] * 0xff + raw[DataFilter.Length + 3];
			int power = raw[DataFilter.Length + 4];
			//Calcurates WxBeacon Data
			var temperature = (((major >> 4) & 0x03ff) * 100 - 30000) / 1000.0;
			var humidity = ((major << 3) & 0x0078) | ((minor >> 13) & 0x0007);
			var pressure = Math.Floor(((minor & 0x1fff) * 0.1 + 300) * 10) / 10;
			return new WxBeaconInfo(temperature, humidity, pressure, power);
		}

		/// <summary>
		/// Creates Bluetooth LE Advertisement packet filter for WxBeacon
		/// </summary>
		/// <returns>New instance of BluetoothLEAdvertisementFilter</returns>
		public static BluetoothLEAdvertisementFilter CreateAdvertisementFilter() {
			var dw = new DataWriter();
			dw.WriteBytes(DataFilter);
			var filter = new BluetoothLEAdvertisementFilter();
			filter.Advertisement.DataSections.Add(new BluetoothLEAdvertisementDataSection(0xff, dw.DetachBuffer()));
			return filter;
		}

	}
}
