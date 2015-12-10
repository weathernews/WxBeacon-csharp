using System.Text;

namespace WxBeacon {
	/// <summary>
	/// Describes WxBeacon weather data
	/// </summary>
	public class WxBeaconInfo {

		/// <summary>
		/// Gets temperature
		/// </summary>
		public double Temperature
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets Humidity
		/// </summary>
		public double Humidity
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets Pressure
		/// </summary>
		public double Pressure
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets RSSI
		/// </summary>
		public int Rssi
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates new instance of WxBeaconInfo
		/// </summary>
		/// <param name="temperature"></param>
		/// <param name="humidity"></param>
		/// <param name="pressure"></param>
		/// <param name="rssi"></param>
		public WxBeaconInfo(double temperature, double humidity, double pressure, int rssi) {
			Temperature = temperature;
			Humidity = humidity;
			Pressure = pressure;
			Rssi = rssi;
		}

		public override string ToString() {
			return new StringBuilder()
				.Append("{ ")
				.Append(" Temperature = ").Append(Temperature).Append(", ")
				.Append(" Humidity = ").Append(Humidity).Append(", ")
				.Append(" Pressure = ").Append(Pressure)
				.Append(" RSSI = ").Append(Rssi)
				.Append(" }")
				.ToString();
		}
	}
}
