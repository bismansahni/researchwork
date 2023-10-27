using System;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        BluetoothClient bluetoothClient = new BluetoothClient();

        // Convert the result of DiscoverDevices() to an array.
        BluetoothDeviceInfo[] devices = bluetoothClient.DiscoverDevices().ToArray();

        // Find the device with the name "PCA004".
        BluetoothDeviceInfo? targetDevice = Array.Find(devices, device => device.DeviceName == "PCAO04");
        

        if (targetDevice != null) 
        {
            try
            {
                // Establish a Bluetooth connection.
                BluetoothClient client = new BluetoothClient();
                

                BluetoothSecurity.PairRequest(targetDevice.DeviceAddress, "8aad8cd4-3830-45ee-a13e-74f0b01013ce"); // Replace with the correct PIN
                Console.WriteLine("Trying to connect");
                while (true)
                {
                    try
                    {
                        // Try to connect using the device address.
                        client.Connect(targetDevice.DeviceAddress, BluetoothService.SerialPort);
                        

                        // If the connection is successful, communicate with the device.
                        Console.WriteLine("Connected to PCA004");

                        // If you are communicating with the device, add your communication logic here.

                        // Close the Bluetooth connection when done.
                        client.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Connection error: " + ex.Message);
                    }

                    // Wait for 10 seconds before attempting to connect again.
                    Thread.Sleep(10000);
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