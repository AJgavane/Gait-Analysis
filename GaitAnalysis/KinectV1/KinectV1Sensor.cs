using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectV1
{
    using Microsoft.Kinect;

    public class KinectV1Sensor
    {
        private KinectSensor sensor;

        public KinectV1Sensor()
        {
            foreach (var sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    this.sensor = sensor;
                    break;
                }
            }

            if (this.sensor != null)
            {
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.sensor.SkeletonStream.Enable();
                this.sensor.Start();
            }

        }
    }

    public static class KV1
    {
        public static KinectV1Sensor GetDefault()
        {
            return new KinectV1Sensor();
        }
    }


}
