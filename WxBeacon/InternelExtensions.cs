using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;

namespace WxBeacon {
	/// <summary>
	/// Internal Extension Methods
	/// </summary>
	static internal class InternelExtensions {
		/// <summary>
		/// Returns Blueooth LE Advertisement Data Section starts with specified bytes array
		/// </summary>
		/// <param name="data"></param>
		/// <param name="bytes"></param>
		/// <returns>true if starts with specified bytes</returns>
		public static bool StartsWith(this BluetoothLEAdvertisementDataSection data, byte[] bytes) {
			//Convert to array
			var origin = data.Data.ToArray();
			if (origin.Length < bytes.Length) {
				//Length mismatch
				return false;
			}
			for (var i = 0; i < bytes.Length; i++) {
				if (origin[i] != bytes[i]) {
					//Not match
					return false;
				}
			}
			//Match
			return true;
		}
	}
}
