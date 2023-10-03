using System;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Threading;


class BluetoothDeviceScanner
{
    

   

    private bool _scanning = false;
    private bool _connected = false;
    private List<string> _deviceAddresses = new List<string>();
    private string targetDeviceName = "PCA004"; 
    private string serviceUUIDName = "8aad8cd4-3830-45ee-a13e-74f0b01013ce"; //NOT SURE IF THIS IS THE CORRECT
    private string characteristicUUIDName = "8aad46ab-3830-45ee-a13e-74f0b01013ce"; // NOTSURE IF THIS IS CORRECT
    void Start()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            Debug.Log("BluetoothLE Initialized.");
            StartScan();
        }, (error) =>
        {
            Debug.LogError($"BluetoothLE Initialization Error: {error}");
        });
    }

    void Update()
    {
        if (!_connected && !_scanning)
        {
            StartScan();
        }
    }

    void StartScan()
    {
        _scanning = true;
        Debug.Log("Starting Scan...");

        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
        {
            if (name == targetDeviceName)
            {
                _scanning = false;
                BluetoothLEHardwareInterface.StopScan();
                Debug.Log("YES");

                BluetoothLEHardwareInterface.ConnectToPeripheral(address, null, null, (address, serviceUUID, characteristicUUID) =>
                {
                    _connected = true;
                  //  AddDeviceToList($"{address} (connected)"); //LIST all devices nearby 
                    Debug.Log("YES2");
                    Debug.Log(address);
                    Debug.Log(serviceUUID);
                    Debug.Log(characteristicUUID);

                    if (serviceUUID == serviceUUIDName && characteristicUUID == characteristicUUIDName)
                        Debug.Log("??");
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            // Reading data
                            BluetoothLEHardwareInterface.ReadCharacteristic(address, serviceUUIDName, characteristicUUIDName, (characteristicUUIDName, data) =>
                            {
                            Debug.Log("HERE");
                                // Here you can process the data
                                Debug.Log("Read: " + BitConverter.ToString(data).Replace("-", ""));
                             });

                        }
                    }
                });
            }
        });
    }

    
}