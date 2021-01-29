using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitalinoCon
{
    class DeviceSingletone
    {
        Bitalino.DevInfo device;
        public Bitalino.DevInfo Device
        {
            get
            {
                return device;
            }

            set
            {
                device = value;
            }
        }
        public Boolean connected = false;

        public void disconnect() {
            connected = false;
        }


        private static DeviceSingletone instance;

        private DeviceSingletone()
        {

        }

        public static DeviceSingletone Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DeviceSingletone();
                }
                return instance;
            }
        }


        public delegate void EventSearchDeviceHandler(Bitalino.DevInfo[] devs);
        public event EventSearchDeviceHandler EnconteredDevices;

        public async Task SearchDevices() {
            await Task.Run(() => {
                Bitalino.DevInfo[] devs = Bitalino.find();
                EventSearchDeviceHandler handler = EnconteredDevices;
                if (handler != null) handler(devs);
            });
        }

        public delegate void EventDataDeviceHandler(String data);
        public event EventDataDeviceHandler NewData;

        public ArrayList dados = new ArrayList();

        public async Task ReadDevice() {
            await Task.Run(() => {
                try
                {
                
                    Console.WriteLine("Connecting to device...");
                    
                    Bitalino dev = new Bitalino(device.macAddr);  // device MAC address
                                                                       //Bitalino dev = new Bitalino("COM7");  // Bluetooth virtual COM port or USB-UART COM port
                    Console.WriteLine("Connected to device. Press Enter to exit.");

                    string ver = dev.version();    // get device version string
                    Console.WriteLine("BITalino version: {0}", ver);

                    dev.battery(10);  // set battery threshold (optional)

                    dev.start(100, new int[] {1});   // start acquisition of all channels at 1000 Hz

                    bool ledState = false;

                    Bitalino.Frame[] frames = new Bitalino.Frame[100];
                    for (int i = 0; i < frames.Length; i++)
                        frames[i] = new Bitalino.Frame();   // must initialize all elements in the array

                    connected = true;

                    do
                    {
        
                        ledState = !ledState;   // toggle LED state
                        dev.trigger(new bool[] { false, false, ledState, false });

                        dev.read(frames); // get 100 frames from device
                        for (int j = 0; j < 100; j++)
                        {
                            Bitalino.Frame f = frames[j];  // get a reference to the first frame of each 100 frames block



                            String dataStr = String.Format("{0}",   // dump the first frame
                                            
                                              f.analog[0]);

                            Console.WriteLine(dataStr);
                            

                            EventDataDeviceHandler handle = NewData;
                            if (handle != null) NewData(dataStr);

                        }
                    } while (connected); // until a key is pressed

                    dev.stop(); // stop acquisition

                    dev.Dispose(); // disconnect from device
                }
                catch (Bitalino.Exception e)
                {
                    Console.WriteLine("BITalino exception: {0}", e.Message);
                }
            });
        }


    }
}
