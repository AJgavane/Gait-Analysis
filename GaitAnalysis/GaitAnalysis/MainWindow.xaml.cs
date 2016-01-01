#region standardImports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#endregion

#region essentialImports
using Microsoft.Kinect;
using System.IO;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using DataClass;
using XMLWrapper;
using System.Windows.Forms;
using AForge.Video;
using KinectV1;
#endregion


namespace GaitAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region KinectVariables

            KinectSensor sensor;
            MultiSourceFrameReader msfReader;
            IList<Body> bodies;

            CameraMode mode = CameraMode.Color;

            WriteableBitmap bmap = new WriteableBitmap(1920, 1080, 96, 96, PixelFormats.Bgra32, null);
            int bytesPerPixel = (PixelFormats.Bgra32.BitsPerPixel + 7) / 8;
            Byte[] frameData = null;

            FileStream fs_jointData = new FileStream(@"..\JointData.csv", FileMode.Create, FileAccess.Write);
            FileStream fs_angleData = new FileStream(@"..\AngleData.csv", FileMode.Create, FileAccess.Write);
            StreamWriter writeCoordinates;
            StreamWriter writeAngleData;
            bool enableRecord = false;
            int frameNum = 0;

            float focalHorizontal, focalVertical;
        #endregion         

        FrameRateCounter frc = new FrameRateCounter(2);


        #region Recording variables
           // byte[] colorData;
            //BitmapSource bmpSource;
            int imageSerial;
        #endregion

        #region DataClass variables

        public static DataClass.PersonInformation personInformation;
        public static DataClass.StdGaitParameters stdGaitParametes;
        private static DataClass.ObservedGaitParameters obsvGaitParamters;
        private string personInformationFileName = "PersonInformationData.xml";
        private string standardGaitParamterFileName = "StandardGaitParameter.xml";

        #endregion

        #region Joint Variables
            //1. spineBase
            float[] spineBase = new float[3];
            //2. Head
            float[] head = new float[3];
            //3. SPineMid
            float[] spineMid = new float[3];
            //4. Neck
            float[] neck = new float[3];
            //5. Shoulder left
            float[] shoulderLeft = new float[3];
            //6. Elbow Left
            float[] elbowLeft = new float[3];
            //7. Wrist Left
            float[] wristLeft = new float[3];
            //8. Hand Left
            float[] handLeft = new float[3];
            //9. Shoulder right
            float[] shoulderRight = new float[3];
            //10. Elbow Right
            float[] elbowRight = new float[3];
            //11. wrist right
            float[] wristRight = new float[3];
            //12. Hand Right
            float[] handRight = new float[3];
            //13. Hip Left
            float[] hipLeft = new float[3];
            //14. Knee Left
            float[] kneeLeft = new float[3];
            //15. Ankle Left
            float[] ankleLeft = new float[3];
            //16. Foot Left
            float[] footLeft = new float[3];
            //17. Hip Right
            float[] hipRight = new float[3];
            //18. Knee Right
            float[] kneeRight = new float[3];
            //19. Ankle Right
            float[] ankleRight = new float[3];
            //20. Foot Right
            float[] footRight = new float[3];
            //21. Spine shoulder
            float[] spineShoulder = new float[3];
            //22. Hand Tip Left
            float[] handTipLeft  = new float[3];
            //23. Thumb Left
            float[] thumbLeft = new float[3];
            //24. Hand Tip Right
            float[] handTipRight = new float[3];
            //25. Thumb Right
            float[] thumbRight = new float[3];
        #endregion

        #region Angle Variables
            float kneeRight_FlexExt;
            float kneeLeft_FlexExt;
        #endregion

        #region Load Window
            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                #region Read XML Files
                // creating XML reader object and reading person infromation from the correspoinding XML file
                personInformation = new DataClass.PersonInformation();
                XmlReader xmlPersonInfoReaderObj = new XmlReader(personInformationFileName);
                xmlPersonInfoReaderObj.ReadPersonInforamtionFile(personInformation);

                stdGaitParametes = new DataClass.StdGaitParameters();
                obsvGaitParamters = new DataClass.ObservedGaitParameters();
                XmlReader xmlGaitPramReaderObj = new XmlReader(standardGaitParamterFileName);
                xmlGaitPramReaderObj.ReadStandardGaitPramFile(stdGaitParametes);
                xmlGaitPramReaderObj.InitializeObservedGaitParameters(obsvGaitParamters);
                #endregion

                #region Load Information Window
                Information infoForm = new Information();
                infoForm.ShowDialog();
                SavePersonInfromationFile();
                #endregion

               


                // Initialize the sensor
                if (personInformation.KinectVersionValue == "Kinect XBOX One (Kinect v2)" )
                {
                    sensor = KinectSensor.GetDefault();

                    if (sensor != null)
                    {
                        sensor.Open();
                        msfReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth |
                            FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                        msfReader.MultiSourceFrameArrived += msfReader_MultiSourceFrameArrived;
                    }
                    //calculate focal lengths
                    focalHorizontal = (float)(bmap.Width / (2 * Math.Tan(30 * Math.PI / 180)));
                    focalVertical = (float)(bmap.Height / (2 * Math.Tan(35 * Math.PI / 180)));
                }

                if (personInformation.KinectVersionValue == "Kinect Xbox 360 (Kinect v1)")
                {
                    KinectV1Sensor v1Sensor;
                    v1Sensor = KinectV1.KV1.GetDefault();
                    if (v1Sensor != null)
                    {
                        // Add the code for kinect v1
                        System.Windows.Forms.MessageBox.Show("V1 Detected!!");
                        
                    }

                }

                writeCoordinates = new StreamWriter(fs_jointData, System.Text.Encoding.ASCII);
                writeCoordinates.Write("Frame# , SpineBase_X, SpineBase_Y, SpineBase_Z , " +
                "SpineMid_X, SpineMid_Y, SpineMid_Z , Neck_X, Neck_Y, Neck_Z, Head_X, Head_Y, Head_Z," +
                "ShoulderLeft_X, ShoulderLeft_Y, ShoulderLeft_Z, EllowLeft_X, EllowLeft_Y, EllowLeft_Z," +
                "WristLeft_X, WristLeft_Y, WristLeft_Z,  HandLeft_X, HandLeft_Y, HandLeft_Z, "+
                "ShoulderRight_X, ShoulderRight_Y, ShoulderRight_Z, ElbowRight_X, ElbowRight_Y, ElbowRight_Z,"+
                "WristRight_X, WristRight_Y, WristRight_Z, HandRight_X, HandRight_Y, HandRight_Z,"+
                "HipLeft_X, HipLeft_Y, HipLeft_Z, KneeLeft_X, KneeLeft_Y, KneeLeft_Z, " +
                "AnkleLeft_X, AnkleLeft_Y, AnkleLeft_Z, FootLeft_X, FootLeft_Y, FootLeft_Z," +
                "HipRight_X, HipRight_Y, HipRight_Z, KneeRight_X, KneeRight_Y, KneeRight_Z, "+
                "AnkleRight_X, AnkleRight_Y, AnkleRight_Z, FootRight_X, FootRight_Y, FootRight_Z,"+
                "SpineShoulder_X, SpineShoulder_Y, SpineShoulder_Z, HandTipLeft_X, HandTipLeft_Y, HandTipLeft_Z," +
                "ThumbLeft_X, ThumbLeft_Y, ThumbLeft_Z, HandTipRight_X, HandTipRight_Y, HandTipRight_Z,"+
                "ThumbRight_X, ThumbRight_Y, ThumbRight_Z," + "," + bmap.Width + "," + bmap.Height + "," +
                focalHorizontal + "," + focalVertical + "," + Math.Tan(45 * Math.PI / 180));
                writeCoordinates.Write(Environment.NewLine);

                writeAngleData = new StreamWriter(fs_angleData, System.Text.Encoding.ASCII);
                writeAngleData.Write("Frame#, LeftKnee_FlexExt, RightKnee_FlexExt");
                writeAngleData.Write(Environment.NewLine);

            }

            private void SavePersonInfromationFile()
            {
                XmlWriter xmlWriterObj = new XmlWriter(personInformationFileName);
                xmlWriterObj.WritePersonInformationFile(personInformation);
            }

            private void SaveStandardGaitParameterFile()
            {
                XmlWriter xmlWriterObj = new XmlWriter(standardGaitParamterFileName);
                xmlWriterObj.WriteStdGaitPrameterFile(stdGaitParametes);
            }
            #endregion

        #region Frame Reader
            void msfReader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
            {
                // get the acquired frame reference
                var reference = e.FrameReference.AcquireFrame();

                #region Color Frame
                using (var frame = reference.ColorFrameReference.AcquireFrame())
                {
                    if (frame != null)
                    {
                        if (mode == CameraMode.Color)
                        {
                            FrameDescription fd = frame.FrameDescription;
                            var size = fd.Width * fd.Height;
                            if (frameData == null)
                            {
                                bmap = new WriteableBitmap(1920, 1080, 96, 96, PixelFormats.Bgra32, null);
                                frameData = new byte[size * bytesPerPixel];
                            }

                            frame.CopyConvertedFrameDataToArray(frameData, ColorImageFormat.Bgra);
                            bmap.WritePixels(
                               new Int32Rect(0, 0, fd.Width, fd.Height),
                                               frameData,
                                               fd.Width * bytesPerPixel,
                                                0);
                            imgCamera.Source = bmap;

                            //// Print frame rate
                            //frc.gotFrame();
                            //tbFrameRate.Text = "Frame Rate: " + frc.frameRate;

                            //Video recording
                            if (enableRecord)
                            {
                                //Skeleton Data
                                RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvas.RenderSize.Width, (int)canvas.RenderSize.Height,
                                    96d, 96d, PixelFormats.Default);
                                rtb.Render(canvas);
                                var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, 960, 540));
                                JpegBitmapEncoder encoderBody = new JpegBitmapEncoder();                              

                                    // Record Skeleton
                                encoderBody.Frames.Add(BitmapFrame.Create(crop));                                    
                                using (var fs = new FileStream("./img/Body_" + (imageSerial++) + ".jpeg", FileMode.Create, FileAccess.Write))
                                {
                                    encoderBody.Save(fs);
                                }

                                ////Color Image
                                //var stride = bytesPerPixel * frame.FrameDescription.Width;
                                //bmpSource = BitmapSource.Create(fd.Width, fd.Height, 96.0, 96.0, PixelFormats.Bgra32, null, frameData, stride);
                                //JpegBitmapEncoder encoderColor = new JpegBitmapEncoder();
                                ////Record ColorImage
                                //encoderColor.Frames.Add(BitmapFrame.Create(bmpSource));
                                //using (var fs = new FileStream("./img/Color_" + (imageSerial++) + ".jpeg", FileMode.Create, FileAccess.Write))
                                //{
                                //    encoderColor.Save(fs);
                                //}

                            }

                            // End Video Recording
                        } // end if

                    } // endif frame != null 

                } // endusing color frame reference
                #endregion      // Color

                #region Body Frame

                using (var frame = reference.BodyFrameReference.AcquireFrame())
                {
                    if (frame != null)
                    {
                        // Print frame rate
                        frc.gotFrame();
                        tbFrameRate.Text = "Frame Rate: " + frc.frameRate;

                        canvas.Children.Clear();
                        bodies = new Body[frame.BodyFrameSource.BodyCount];
                        frame.GetAndRefreshBodyData(bodies);

                        foreach (var body in bodies)
                        {
                            if (body.IsTracked)
                            {
                                #region StoreJointData
                                //1. spineBase
                                spineBase[0] = body.Joints[JointType.SpineBase].Position.X;
                                spineBase[0] = spineBase[2] * spineBase[0] *100 / focalHorizontal;
                                spineBase[1] = body.Joints[JointType.SpineBase].Position.Y;
                                spineBase[1] = spineBase[2] * spineBase[1] * 100 / focalVertical;
                                spineBase[2] = body.Joints[JointType.SpineBase].Position.Z;
                                //2. Head
                                head[0] = body.Joints[JointType.Head].Position.X ;
                                head[1] = body.Joints[JointType.Head].Position.Y ;
                                head[2] = body.Joints[JointType.Head].Position.Z ;
                                //3. SPineMid
                                spineMid[0] =body.Joints[JointType.SpineMid].Position.X;
                                spineMid[1] =body.Joints[JointType.SpineMid].Position.Y;
                                spineMid[2] =body.Joints[JointType.SpineMid].Position.Z;
                                //4. Neck
                                neck[0] = body.Joints[JointType.Neck].Position.X;
                                neck[1] = body.Joints[JointType.Neck].Position.Y;
                                neck[2] = body.Joints[JointType.Neck].Position.Z;
                                //5. Shoulder left
                                shoulderLeft[0] = body.Joints[JointType.ShoulderLeft].Position.X;
                                shoulderLeft[1] = body.Joints[JointType.ShoulderLeft].Position.Y;
                                shoulderLeft[2] = body.Joints[JointType.ShoulderLeft].Position.Z;
                                //6. Elbow Left
                                elbowLeft[0] = body.Joints[JointType.ElbowLeft].Position.X ;
                                elbowLeft[1] = body.Joints[JointType.ElbowLeft].Position.Y ;
                                elbowLeft[2] = body.Joints[JointType.ElbowLeft].Position.Z ;
                                //7. Wrist Left
                                wristLeft[0] = body.Joints[JointType.WristLeft].Position.X ;
                                wristLeft[1] = body.Joints[JointType.WristLeft].Position.Y ;
                                wristLeft[2] = body.Joints[JointType.WristLeft].Position.Z ;
                                //8. Hand Left
                                handLeft[0] = body.Joints[JointType.HandLeft].Position.X ;
                                handLeft[1] = body.Joints[JointType.HandLeft].Position.X ;
                                handLeft[2] = body.Joints[JointType.HandLeft].Position.X ;
                                //9. Shoulder right
                                shoulderRight[0] = body.Joints[JointType.ShoulderRight].Position.X ;
                                shoulderRight[1] = body.Joints[JointType.ShoulderRight].Position.Y ;
                                shoulderRight[2] = body.Joints[JointType.ShoulderRight].Position.Z ;
                                //10. Elbow Right
                                elbowRight[0] = body.Joints[JointType.ElbowRight].Position.X ;
                                elbowRight[1] = body.Joints[JointType.ElbowRight].Position.Y ;
                                elbowRight[2] = body.Joints[JointType.ElbowRight].Position.Z ;
                                //11. wrist right
                                wristRight[0] = body.Joints[JointType.WristRight].Position.X ;
                                wristRight[1] = body.Joints[JointType.WristRight].Position.Y ;
                                wristRight[2] = body.Joints[JointType.WristRight].Position.Z ;
                                //12. Hand Right
                                handRight[0] = body.Joints[JointType.HandRight].Position.X ;
                                handRight[1] = body.Joints[JointType.HandRight].Position.Y ;
                                handRight[2] = body.Joints[JointType.HandRight].Position.Z ;
                                //13. Hip Left
                                hipLeft[0] = body.Joints[JointType.HipLeft].Position.X ;
                                hipLeft[1] = body.Joints[JointType.HipLeft].Position.Y ;
                                hipLeft[2] = body.Joints[JointType.HipLeft].Position.Z ;
                                //14. Knee Left
                                kneeLeft[0] = body.Joints[JointType.KneeLeft].Position.X ;
                                kneeLeft[1] = body.Joints[JointType.KneeLeft].Position.Y ;
                                kneeLeft[2] = body.Joints[JointType.KneeLeft].Position.Z ;
                                //15. Ankle Left
                                ankleLeft[0] = body.Joints[JointType.AnkleLeft].Position.X ;
                                ankleLeft[1] = body.Joints[JointType.AnkleLeft].Position.Y ;
                                ankleLeft[2] = body.Joints[JointType.AnkleLeft].Position.Z ;
                                //16. Foot Left
                                footLeft[0] = body.Joints[JointType.FootLeft].Position.X ;
                                footLeft[1] = body.Joints[JointType.FootLeft].Position.Y ;
                                footLeft[2] = body.Joints[JointType.FootLeft].Position.Z ;
                                //17. Hip Right
                                hipRight[0] = body.Joints[JointType.HipRight].Position.X ;
                                hipRight[1] = body.Joints[JointType.HipRight].Position.Y ;
                                hipRight[2] = body.Joints[JointType.HipRight].Position.Z ;
                                //18. Knee Right
                                kneeRight[0] = body.Joints[JointType.KneeRight].Position.X ;
                                kneeRight[1] = body.Joints[JointType.KneeRight].Position.Y ;
                                kneeRight[2] = body.Joints[JointType.KneeRight].Position.Z ;
                                //19. Ankle Right
                                ankleRight[0] = body.Joints[JointType.AnkleRight].Position.X ;
                                ankleRight[1] = body.Joints[JointType.AnkleRight].Position.Y ;
                                ankleRight[2] = body.Joints[JointType.AnkleRight].Position.Z ;
                                //20. Foot Right
                                footRight[0] = body.Joints[JointType.FootRight].Position.X ;
                                footRight[1] = body.Joints[JointType.FootRight].Position.Y ;
                                footRight[2] = body.Joints[JointType.FootRight].Position.Z ;
                                //21. Spine shoulder
                                spineShoulder[0] = body.Joints[JointType.SpineShoulder].Position.X ;
                                spineShoulder[1] = body.Joints[JointType.SpineShoulder].Position.Y ;
                                spineShoulder[2] = body.Joints[JointType.SpineShoulder].Position.Z ;
                                //22. Hand Tip Left
                                handTipLeft[0] = body.Joints[JointType.HandTipLeft].Position.X ;
                                handTipLeft[1] = body.Joints[JointType.HandTipLeft].Position.Y ;
                                handTipLeft[2] = body.Joints[JointType.HandTipLeft].Position.Z ;
                                //23. Thumb Left
                                thumbLeft[0] = body.Joints[JointType.ThumbLeft].Position.X ;
                                thumbLeft[1] = body.Joints[JointType.ThumbLeft].Position.Y ;
                                thumbLeft[2] = body.Joints[JointType.ThumbLeft].Position.Z ;
                                //24. Hand Tip Right
                                handTipRight[0] = body.Joints[JointType.HandTipRight].Position.X ;
                                handTipRight[1] = body.Joints[JointType.HandTipRight].Position.Y ;
                                handTipRight[2] = body.Joints[JointType.HandTipRight].Position.Z ;
                                //25. Thumb Right
                                thumbRight[0] = body.Joints[JointType.ThumbRight].Position.X;
                                thumbRight[1] = body.Joints[JointType.ThumbRight].Position.Y;
                                thumbRight[2] = body.Joints[JointType.ThumbRight].Position.Z;
                                #endregion

                                #region Calculate Angles
                                kneeLeft_FlexExt = 180 - CalculateAngle(hipLeft, kneeLeft, ankleLeft, 1, 2);
                                kneeRight_FlexExt = 180 - CalculateAngle(hipRight, kneeRight, ankleRight, 1, 2);
                                #endregion

                                DrawSkeleton(body);                               
                            }
                        } //endforeach 
                    } //endif
                } //endusing
                #endregion  // body
            }

           

           
        #endregion // frame reader
            
        #region Window Closing event
            private void Window_Closed(object sender, EventArgs e)
            {
                if (msfReader != null)
                {
                    msfReader.Dispose();
                }
                if (sensor != null)
                {
                    sensor.Close();
                }
                writeCoordinates.Close();
            }
            #endregion
        
        #region Draw Skeleton
            private void DrawSkeleton(Body body)
            {
                if (body == null)
                {
                    return;
                }
                float[] point;
                point = new float[3];
                if (enableRecord)
                {
                    frameNum++;
                    writeCoordinates.Write(frameNum);
                    writeAngleData.Write(frameNum + "," + kneeLeft_FlexExt + "," + kneeRight_FlexExt + "," +
                        spineBase[0] + "," + spineBase[1]);
                    writeAngleData.Write(Environment.NewLine);
                }
                foreach (Joint joint in body.Joints.Values)
                {
                    DrawPoint(joint);
                    if (enableRecord)
                    {
                        writeCoordinates.Write("," + joint.Position.X + "," + joint.Position.Y
                      + "," + joint.Position.Z);
                    }                  
                }
                if (enableRecord)
                {
                    writeCoordinates.Write(Environment.NewLine); 
                }
                //Upper
                DrawLine(body.Joints[JointType.Head], body.Joints[JointType.Neck]);
                DrawLine(body.Joints[JointType.Neck], body.Joints[JointType.SpineShoulder]);
                DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.ShoulderLeft]);
                DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.ShoulderRight]);
                DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.SpineMid]);

                //Left Arm
                DrawLine(body.Joints[JointType.ShoulderLeft], body.Joints[JointType.ElbowLeft]);
                DrawLine(body.Joints[JointType.ElbowLeft], body.Joints[JointType.WristLeft]);
                DrawLine(body.Joints[JointType.WristLeft], body.Joints[JointType.HandLeft]);
                DrawLine(body.Joints[JointType.WristLeft], body.Joints[JointType.ThumbLeft]);
                DrawLine(body.Joints[JointType.HandLeft], body.Joints[JointType.HandTipLeft]);
               // DrawLine(body.Joints[JointType.HandTipLeft], body.Joints[JointType.ThumbLeft]);

                // Right Arm
                DrawLine(body.Joints[JointType.ShoulderRight], body.Joints[JointType.ElbowRight]);                
                DrawLine(body.Joints[JointType.ElbowRight], body.Joints[JointType.WristRight]);               
                DrawLine(body.Joints[JointType.WristRight], body.Joints[JointType.HandRight]);
                DrawLine(body.Joints[JointType.WristRight], body.Joints[JointType.ThumbRight]);                
                DrawLine(body.Joints[JointType.HandRight], body.Joints[JointType.HandTipRight]);               
               // DrawLine(body.Joints[JointType.HandTipRight], body.Joints[JointType.ThumbRight]);

                //Middle
                DrawLine(body.Joints[JointType.SpineMid], body.Joints[JointType.SpineBase]);
                DrawLine(body.Joints[JointType.SpineBase], body.Joints[JointType.HipLeft]);
                DrawLine(body.Joints[JointType.SpineBase], body.Joints[JointType.HipRight]);

                // Left Leg
                DrawLine(body.Joints[JointType.HipLeft], body.Joints[JointType.KneeLeft]);
                DrawLine(body.Joints[JointType.KneeLeft], body.Joints[JointType.AnkleLeft]);
                DrawLine(body.Joints[JointType.AnkleLeft], body.Joints[JointType.FootLeft]);

                // Right Leg
                DrawLine(body.Joints[JointType.HipRight], body.Joints[JointType.KneeRight]);                
                DrawLine(body.Joints[JointType.KneeRight], body.Joints[JointType.AnkleRight]);                
                DrawLine(body.Joints[JointType.AnkleRight], body.Joints[JointType.FootRight]);
            }

            private void DrawLine(Joint firstJoint, Joint secondJoint)
            {
                if (firstJoint.TrackingState == TrackingState.NotTracked || secondJoint.TrackingState == TrackingState.NotTracked)
                {
                    return;
                }
                Point first = new Point();
                Point second = new Point();
                first = ScaleTo(firstJoint);
                second = ScaleTo(secondJoint);
                Line line = new Line
                {
                    X1 = first.X,
                    Y1 = first.Y,
                    X2 = second.X,
                    Y2 = second.Y,
                    StrokeThickness = 4,
                    Stroke = new SolidColorBrush(Colors.Coral)
                };
                canvas.Children.Add(line);
            }

            private void DrawPoint(Joint joint) 
            {
                if (joint.TrackingState == TrackingState.NotTracked)
                {
                    return;
                }
                
                //2D sapce point
                Point point = new Point();
                point = ScaleTo(joint);

                //Draw joints
                Ellipse ellipse = new Ellipse
                {
                    Fill = Brushes.Aqua,
                    Width = 10,
                    Height = 10
                };

                Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);

                canvas.Children.Add(ellipse);               
            }

            private Point ScaleTo(Joint joint)
            {
                //3D space point
                CameraSpacePoint jointPosition = joint.Position;
                //2D sapce point
                Point point = new Point();

                if (mode == CameraMode.Color)
                {
                    ColorSpacePoint colorPoint = sensor.CoordinateMapper.MapCameraPointToColorSpace(jointPosition);
                    point.X = float.IsInfinity(colorPoint.X) ? 0 : colorPoint.X;
                    point.Y = float.IsInfinity(colorPoint.Y) ? 0 : colorPoint.Y;
                }
                else if (mode == CameraMode.Depth || mode == CameraMode.Infrared)
                {
                    DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(jointPosition);
                    point.X = float.IsInfinity(depthPoint.X) ? 0 : depthPoint.X;
                    point.Y = float.IsInfinity(depthPoint.Y) ? 0 : depthPoint.Y;
                }

                //Scale down the points to current canvas size
                point.X = point.X * canvas.ActualWidth / 1920;
                point.Y = point.Y * canvas.ActualHeight / 1080;
                return point;
            }

           

        #endregion // skeleton

        private float CalculateAngle(float[] pointA, float[] pointB, float[] pointC, int s, int t)
        {
            float angle = 0;
            float[] point1 = new float[3];
            float[] point2 = new float[3];
            float[] point3 = new float[3];
            for (int i = 0; i < 3; i++)
            {
                point1[i] = 0;
                point2[i] = 0;
                point3[i] = 0;
            }
            point1[s] = pointA[s];
            point1[t] = pointA[t];
            point2[s] = pointB[s];
            point2[t] = pointB[t];
            point3[s] = pointC[s];
            point3[t] = pointC[t];
            angle = CalculateAngle(point2, point1, point3);
            return angle;
        }

        private float CalculateAngle(float[] pointA, float[] pointB, float[] pointC)
        {
            //Using cosine rule to calculate the angle
            float a, b, c, angle, cosineValue;
            a = CaculateDistance(pointB, pointC);
            b = CaculateDistance(pointA, pointC);
            c = CaculateDistance(pointA, pointB);
            cosineValue = (b * b + c * c - a * a) / (2 * b * c);
            angle = (float)Math.Acos(cosineValue);
            angle = (angle * 180) / (float)Math.PI;
            return angle;
        }

        private float CaculateDistance(float[] point1, float[] point2)
        {
            float distance, x1_minus_x2_square, y1_minus_y2_square, z1_minus_z2_square;
            x1_minus_x2_square = (float)Math.Pow(point1[0] - point2[0], 2);
            y1_minus_y2_square = (float)Math.Pow(point1[1] - point2[1], 2);
            z1_minus_z2_square = (float)Math.Pow(point1[2] - point2[2], 2);
            distance = (float)Math.Sqrt(x1_minus_x2_square + y1_minus_y2_square+ z1_minus_z2_square);
            distance = (float)Math.Abs(distance);
            return distance;
        }

        #region FileMenu
            private void File_Menu_New_Click(object sender, RoutedEventArgs e)
        {

        }

        private void File_Menu_Open_Click(object sender, RoutedEventArgs e)
        {

        }

        private void File_Menu_ReportBug_Click(object sender, RoutedEventArgs e)
        {

        }

        private void File_Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region EditMenu

            private void Edit_Menu_GaitStdParam_Click(object sender, RoutedEventArgs e)
            {
                StandardGaitParameters standardGaitPramForm = new StandardGaitParameters();
                standardGaitPramForm.ShowDialog();
                SaveStandardGaitParameterFile();
            }

            private void Edit_Menu_PersonInfo_Click(object sender, RoutedEventArgs e)
            {
                Information infoForm = new Information();
                infoForm.ShowDialog();
                SavePersonInfromationFile();
            }

        #endregion

        #region CameraMode
            enum CameraMode { 
                Color,
                Depth,
                Infrared
            }
        #endregion

        #region Buttons
            private void btnReport_Click(object sender, RoutedEventArgs e)
            {
               // string reprotFileName = "Report.xml";
                XmlWriter writeReport = new XmlWriter();
                writeReport.WriteReport(personInformation, stdGaitParametes, obsvGaitParamters);
                System.Windows.Forms.MessageBox.Show("ReportGenerated!!");
            }

            private void btnRecord_Click(object sender, RoutedEventArgs e)
            {
                enableRecord = enableRecord ^ true;
                if (enableRecord)
                {
                    btnRecord.Content = "Stop";
                    enableRecord = true;
                    // Start Recording
                    DirectoryInfo di = Directory.CreateDirectory("./img/");
                }
                if (!enableRecord)
                {
                    //writeCoordinates.Write("StopReading");
                    //writeCoordinates.Write(Environment.NewLine);
                    writeCoordinates.Close();
                    writeAngleData.Close();
                    writeCoordinates.Dispose();
                    writeAngleData.Dispose();
                    enableRecord = false;
                    btnRecord.Content = "Record";
                    //Stop Recording                    
                }    
                
                    
                                

            }

            private void btnCancel_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        #endregion
    }
}

class FrameRateCounter
{
    private readonly int averageOverSeconds;
    public int framesAccumulated { get; set; }
    public double frameRate { get; set; }
    public List<DateTime> timestamp = new List<DateTime>();

    public FrameRateCounter(int averageOverSeconds)
    {
        this.averageOverSeconds = averageOverSeconds;
    }
    public void gotFrame()
    {
        DateTime t = DateTime.UtcNow;
        timestamp.RemoveAll(item => (t - item).TotalMilliseconds > averageOverSeconds * 1000);
        timestamp.Add(t);
        this.frameRate = ((double)timestamp.Count / averageOverSeconds);
    }
}