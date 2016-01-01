using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using DataClass;

namespace XMLWrapper
{
    public class XmlReader
    {
        private string xmlFilePath = "";
        private XmlDocument xmlDocument;
       // private string personInformationFileName;

        // constructor
        public XmlReader(string xmlFile)
        {
            // Initialize the variable
            xmlFilePath = xmlFile;
            // Create or load the document
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
        }

        /// <summary>
        /// Method to get node value using tag name
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        public string GetNodeValue(string strNodeName) 
        {
            // NUll check
            if (xmlDocument == null)
            {
                return "";  
            }
            // Creating xmlNode list and getting the node value by the tag name
            XmlNodeList xmlNodeList;
            xmlNodeList = xmlDocument.GetElementsByTagName(strNodeName);

            //Null Check
            if (xmlNodeList.Count == 0)
            {
                return "";                
            }
            // Return the value
            return xmlNodeList[0].InnerText;
        }
        /// <summary>
        /// Method to get node value using its parent value too
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strParentNodeName"></param>
        /// <returns></returns>
        public string GetNodeValue(string strNodeName, string strParentNodeName)
        { 
            // NuLL check
            if (xmlDocument == null)
            {
                return "";
            }
            //Creating xml node list and getting the node value by the tag name
            XmlNodeList xmlNodeList;
            xmlNodeList = xmlDocument.GetElementsByTagName(strNodeName);

            if (xmlNodeList == null || xmlNodeList.Count == 0)
            {
                return "";
            }
            // Fetch the node value corresponding to the parent node
            for (int i = 0; i < xmlNodeList.Count - 1; i++)
            {
                XmlNode node;
                node = xmlNodeList[i];
                if (node.ParentNode.Name == strParentNodeName)
                {
                    return xmlNodeList[0].InnerText;
                }                
            }
            return "";
        }

        public void ReadPersonInforamtionFile(DataClass.PersonInformation personData)
        {
            if (xmlDocument == null)
            {
                return;
            }
            personData.NameValue = GetNodeValue("name");
            personData.HeightValue = GetNodeValue("height");
            personData.WeightValue = GetNodeValue("weight");
            personData.AgeValue = GetNodeValue("age");
            personData.GenderValue = GetNodeValue("gender");
            personData.FolderPathValue = GetNodeValue("folderPath");
            personData.KinectVersionValue = GetNodeValue("kinectVersion");
            
        }

        public void ReadStandardGaitPramFile(DataClass.StdGaitParameters stdGaitPram)
        {
            if (xmlDocument == null)
            {
                return;
            }
            stdGaitPram.AgeGroupValue = GetNodeValue("agegroup");
            stdGaitPram.CadenceValue = GetNodeValue("cadence");
            stdGaitPram.CycleTimeValue = GetNodeValue("cycletime");
            stdGaitPram.SPeedOfWalkingValue = GetNodeValue("speedofwalking");
            stdGaitPram.StrideLengthValue = GetNodeValue("stridelength");
            stdGaitPram.StepFactorValue = GetNodeValue("stepfactor");
            stdGaitPram.WlakingBaseValue = GetNodeValue("walkingbase");
        }

        public void InitializeObservedGaitParameters(DataClass.ObservedGaitParameters obsvGaitParam)
        {
            if (xmlDocument == null)
            {
                return;
            }
            obsvGaitParam.CadenceValue = GetNodeValue("cadence");
            obsvGaitParam.CycleTimeValue = GetNodeValue("cycletime");
            obsvGaitParam.SPeedOfWalkingValue = GetNodeValue("speedofwalking");
            obsvGaitParam.StrideLengthValue = GetNodeValue("stridelength");
            obsvGaitParam.StepFactorValue = GetNodeValue("stepfactor");
            obsvGaitParam.WlakingBaseValue = GetNodeValue("walkingbase");
        }

        public void ReadDefauldStandardGaitPramFile(DataClass.StdGaitParameters stdGaitPram)
        {
            if (xmlDocument == null)
            {
                return;
            }
            stdGaitPram.AgeGroupValue = GetNodeValue("agegroup");
            stdGaitPram.CadenceValue = GetNodeValue("cadence");
            stdGaitPram.CycleTimeValue = GetNodeValue("cycletime");
            stdGaitPram.SPeedOfWalkingValue = GetNodeValue("speedofwalking");
            stdGaitPram.StrideLengthValue = GetNodeValue("stridelength");
            stdGaitPram.StepFactorValue = GetNodeValue("stepfactor");
            stdGaitPram.WlakingBaseValue = GetNodeValue("walkingbase");
        }
    }
}
