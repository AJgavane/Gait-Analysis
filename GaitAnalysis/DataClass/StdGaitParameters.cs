using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClass
{
    public partial class StdGaitParameters
    {

        private string strideLength;
        private string walkingBase;
        private string speedOfWalking;
        private string cadence;
        private string stepFactor;
        private string cycleTime;
        private string ageGroup;

        // Stores the standard stride lenght 
        public string StrideLengthValue
        {
            get
            {
                return this.strideLength;
            }
            set
            {
                this.strideLength = value;
            }
        }

        // Stores the standard Wlaking Base
        public string WlakingBaseValue
        {
            get
            {
                return this.walkingBase;
            }
            set
            {
                this.walkingBase = value;
            }
        }
        // Stores the standard speed of walking
        public string SPeedOfWalkingValue
        {
            get
            {
                return this.speedOfWalking;
            }
            set
            {
                this.speedOfWalking = value;
            }
        }

        // Stores the standard cadence
        public string CadenceValue
        {
            get
            {
                return this.cadence;
            }
            set
            {
                this.cadence = value;
            }
        }

        // Stores the standard step factor
        public string StepFactorValue
        {
            get
            {
                return this.stepFactor;
            }
            set
            {
                this.stepFactor = value;
            }
        }

        // Stores the standard  cycle Time
        public string CycleTimeValue
        {
            get
            {
                return this.cycleTime;
            }
            set
            {
                this.cycleTime = value;
            }
        }

        public string AgeGroupValue
        {
            get
            {
                return this.ageGroup;
            }
            set
            {
                this.ageGroup = value;
            }
        }

        
    }
}
