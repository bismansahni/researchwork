using System;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        BluetoothClient bluetoothClient = new BluetoothClient();

        // Discover nearby Bluetooth devices
        BluetoothDeviceInfo[] devices = BluetoothClient.DiscoverDevices();

        // Find the device with the name "PCA004"
        BluetoothDeviceInfo targetDevice = Array.Find(devices, device => device.DeviceName == "PCA004");

        if (targetDevice != null)
        {
            try
            {
                // Establish a Bluetooth connection
                BluetoothClient client = new BluetoothClient();
                if (targetDevice != null)
                {
                    BluetoothSecurity.PairRequest(targetDevice.DeviceAddress, "1234"); // Replace with the correct PIN
                    BluetoothEndPoint endPoint = new BluetoothEndPoint(targetDevice.DeviceAddress, BluetoothService.SerialPort);

                    while (true)
                    {
                        try
                        {
                            client.Connect(endPoint);

                            // If the connection is successful,  communicate with the device.
                            Console.WriteLine("Connected to PCA004");

                            
                            // Close the Bluetooth connection  done.
                            client.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Connection error: " + ex.Message);
                        }

                        // Wait for 10 seconds before attempting to connect again
                        Thread.Sleep(10000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Device PCA004 not found.");
        }
    }
}
