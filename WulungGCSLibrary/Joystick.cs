/******************************************************************************
 * C# Joystick Library - Copyright (c) 2006 Mark Harris - MarkH@rris.com.au
 ******************************************************************************
 * You may use this library in your application, however please do give credit
 * to me for writing it and supplying it. If you modify this library you must
 * leave this notice at the top of this file. I'd love to see any changes you
 * do make, so please email them to me :)
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Diagnostics;
using System.Collections;
//using System.Windows.Documents;

namespace WulungGCSLibrary
{
    /// <summary>
    /// Class to interface with a joystick device.
    /// </summary>
    public class Joystick
    {
        private Device joystickDevice;
        private JoystickState state;
        public int ButtonCount;

        private List<string> avaAxis = new List<string>();
        public string[] availableAxis;
        //public int AxesCount;
        //public string[] availableAxis;
        //private joy
        private int axisCount;
        /// <summary>
        /// Number of axes on the joystick.
        /// </summary>
        
        
        public int AxisCount
        {
            
            get { return axisCount; }
        }
        
        private int axisX;
        /// <summary>
        /// The first axis on the joystick.
        /// </summary>
        public int AxisX
        {
            get { return axisX; }
        }
        
        private int axisY;
        /// <summary>
        /// The second axis on the joystick.
        /// </summary>
        public int AxisY
        {
            get { return axisY; }
        }
        
        private int axisZ;
        /// <summary>
        /// The third axis on the joystick.
        /// </summary>
        public int AxisZ
        {
            get { return axisZ; }
        }
        
        private int axisRX;
        /// <summary>
        /// The fourth axis on the joystick.
        /// </summary>
        public int AxisRX
        {
            get { return axisRX; }
        }
        
        private int axisRY;
        /// <summary>
        /// The fifth axis on the joystick.
        /// </summary>
        public int AxisRY
        {
            get { return axisRY; }
        }
        
        private int axisRZ;
        /// <summary>
        /// The sixth axis on the joystick.
        /// </summary>
        public int AxisRZ
        {
            get { return axisRZ; }
        }

        private int slider1;

        public int Slider1
        {
            get { return slider1; }
        }

        private int slider2;

        public int Slider2
        {
            get { return slider2; }
        }

        //private IntPtr hWnd;

        private bool[] buttons;
        /// <summary>
        /// Array of buttons availiable on the joystick. This also includes PoV hats.
        /// </summary>
        public bool[] Buttons
        {
            get { return buttons; }
        }

        private string[] systemJoysticks;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="window_handle">Handle of the window which the joystick will be "attached" to.</param>
        public Joystick()//IntPtr window_handle)
        {
            //hWnd = window_handle;
            axisX = -1;
            axisY = -1;
            axisZ = -1;
            axisRX = -1;
            axisRY = -1;
            axisRZ = -1;
            slider1 = -1;
            slider2 = -1;
            axisCount = 0;
        }

        private void Poll()
        {
            try
            {
                // poll the joystick
                joystickDevice.Poll();
                // update the joystick state field
                state = joystickDevice.CurrentJoystickState;
            }
            catch (Exception err)
            {
                // we probably lost connection to the joystick
                // was it unplugged or locked by another application?
                Debug.WriteLine("Poll()");
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
                throw err;
            }

           // Console.WriteLine(joystickDevice.GetObjectInformation(3,ParameterHow.ByDevice).ToString());
        }

        /// <summary>
        /// Retrieves a list of joysticks attached to the computer.
        /// </summary>
        /// <example>
        /// [C#]
        /// <code>
        /// JoystickInterface.Joystick jst = new JoystickInterface.Joystick(this.Handle);
        /// string[] sticks = jst.FindJoysticks();
        /// </code>
        /// </example>
        /// <returns>A list of joysticks as an array of strings.</returns>
        public string[] FindJoysticks()
        {
            systemJoysticks = null;

            try
            {
                // Find all the GameControl devices that are attached.
                DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);

                // check that we have at least one device.
                if (gameControllerList.Count > 0)
                {
                    systemJoysticks = new string[gameControllerList.Count];
                    int i = 0;
                    // loop through the devices.
                    foreach (DeviceInstance deviceInstance in gameControllerList)
                    {
                        // create a device from this controller so we can retrieve info.
                        joystickDevice = new Device(deviceInstance.InstanceGuid);
                        //joystickDevice.SetCooperativeLevel(hWnd,
                        //    CooperativeLevelFlags.Background |
                        //    CooperativeLevelFlags.NonExclusive);

                        systemJoysticks[i] = joystickDevice.DeviceInformation.InstanceName;

                        i++;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("FindJoysticks()");
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
            }

            return systemJoysticks;
        }

        /// <summary>
        /// Acquire the named joystick. You can find this joystick through the <see cref="FindJoysticks"/> method.
        /// </summary>
        /// <param name="name">Name of the joystick.</param>
        /// <returns>The success of the connection.</returns>
        public bool AcquireJoystick(string name)
        {
            avaAxis.Clear();
            try
            {
                
                DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
                int i = 0;
                bool found = false;
                // loop through the devices.
                foreach (DeviceInstance deviceInstance in gameControllerList)
                {
                    if (deviceInstance.InstanceName == name)
                    {
                        found = true;
                        // create a device from this controller so we can retrieve info.
                        joystickDevice = new Device(deviceInstance.InstanceGuid);
                        //joystickDevice.SetCooperativeLevel(hWnd,
                        //    CooperativeLevelFlags.Background |
                        //    CooperativeLevelFlags.NonExclusive);
                        break;
                    }

                    i++;
                }

                if (!found)
                    return false;
                
                // Tell DirectX that this is a Joystick.
                joystickDevice.SetDataFormat(DeviceDataFormat.Joystick);

                // Finally, acquire the device.
                joystickDevice.Acquire();
                
                // How many axes?
                // Find the capabilities of the joystick
                DeviceCaps cps = joystickDevice.Caps;
                ButtonCount = cps.NumberButtons;

                if (joystickDevice.CurrentJoystickState.X != 0) avaAxis.Add("x");
                if (joystickDevice.CurrentJoystickState.Y != 0) avaAxis.Add("y");
                if (joystickDevice.CurrentJoystickState.Z != 0) avaAxis.Add("z");
                if (joystickDevice.CurrentJoystickState.Rx!= 0) avaAxis.Add("rx");
                if (joystickDevice.CurrentJoystickState.Ry != 0) avaAxis.Add("ry");
                if (joystickDevice.CurrentJoystickState.Rz != 0) avaAxis.Add("rz");
                if (joystickDevice.CurrentJoystickState.GetSlider()[0] != 0) avaAxis.Add("slider1");
                if (joystickDevice.CurrentJoystickState.GetSlider()[1] != 0) avaAxis.Add("slider2");

                availableAxis = avaAxis.ToArray();

                Debug.WriteLine("Joystick Axis: " + cps.NumberAxes);
                //Debug.WriteLine(joystickDevice.CurrentJoystickState.ToString());
                //Debug.WriteLine(joystickDevice.CurrentJoystickState.GetSlider()[0].ToString()+" "+
                //    joystickDevice.CurrentJoystickState.GetSlider()[1].ToString());

                axisCount = cps.NumberAxes;

                UpdateStatus();
                
            }
            catch (Exception err)
            {
                Debug.WriteLine("FindJoysticks()");
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
                //throw err;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Unaquire a joystick releasing it back to the system.
        /// </summary>
        public void ReleaseJoystick()
        {
            joystickDevice.Unacquire();
        }

        /// <summary>
        /// Update the properties of button and axis positions.
        /// </summary>
        public void UpdateStatus()
        {
            Poll();

            int[] extraAxis = state.GetSlider();
            //Rz Rx X Y Axis1 Axis2
            axisX = state.X;
            axisY = state.Y;
            axisZ = state.Z;
            axisRX = state.Rx;
            axisRY = state.Ry;
            axisRZ = state.Rz;
            slider1 = extraAxis[0];
            slider2 = extraAxis[1];
            
            // not using buttons, so don't take the tiny amount of time it takes to get/parse
            byte[] jsButtons = state.GetButtons();
            buttons = new bool[jsButtons.Length];

            int i = 0;
            foreach (byte button in jsButtons)
            {
                buttons[i] = button >= 128;
                i++;
            }
        }
    }
}
