using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClass
{
    public class PersonInformation
    {
        /// <summary>
        /// Private variables
        /// </summary>
        private string name;
        private string age;
        private string height;
        private string weight;
        private string gender;
        private string folderPath;
        private string kinectVersion;

        //Store name of the person
        public string NameValue
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        // Store age of the person
        public string AgeValue
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }

        // Store height of the person
        public string HeightValue
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        // Store Weight of the person
        public string WeightValue
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        // Store gender of the person
        public string GenderValue
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        // Store folderPath of the person
        public string FolderPathValue
        {
            get
            {
                return this.folderPath;
            }
            set
            {
                this.folderPath = value;
            }
        }

        // Store Kinect Vesrion of the person
        public string KinectVersionValue
        {
            get
            {
                return this.kinectVersion;
            }
            set
            {
                this.kinectVersion = value;
            }
        }


    }
}
