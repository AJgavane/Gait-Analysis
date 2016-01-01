using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataClass;
using System.Xml;

namespace XMLWrapper
{
    public class XmlWriter
    {
        private string xmlFilePath = "";
        private XmlDocument xmlDocument;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlFile"></param>
        public XmlWriter(string xmlFile) 
        {
            // initialize the variable;
            xmlFilePath = xmlFile;
            // create of load the document
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
        }
        public XmlWriter() { }

        public void SaveXmlFile()
        {
            if (xmlDocument == null)
            {
                return;
            }
            xmlDocument.Save(xmlFilePath);
        }

        /// <summary>
        /// Method to change the node value using tag name
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="newValue"></param>
        public void ChangeNodeValue(string nodeName, string newValue) 
        {
            // NuLL Check
            if (xmlDocument == null)
            {
                return;
            }
            // create xml node list
            XmlNodeList xmlNodeList;
            xmlNodeList = xmlDocument.GetElementsByTagName(nodeName);
            //Null Check
            if (xmlNodeList == null || xmlNodeList.Count == 0)
            {
                return;
            }
            xmlNodeList[0].InnerText = newValue;
        }

        /// <summary>
        /// Method to change node value give the node name and it's parent node name
        /// </summary>
        /// <param name="nodeNmae"></param>
        /// <param name="parentNodeName"></param>
        /// <param name="newValue"></param>
        public void ChangeNodeValue(string nodeNmae, string parentNodeName, string newValue)
        { 
            //NUll Check
            if (xmlDocument == null)
            {
                return;
            }
            //create a node list
            XmlNodeList xmlNodeList;
            xmlNodeList = xmlDocument.GetElementsByTagName(nodeNmae);
            // Null check
            if (xmlNodeList == null || xmlNodeList.Count == 0)
            {
                return;
            }
            // change the value of node correspoinding to the parent node
            for (int i = 0; i < xmlNodeList.Count -1; i++)
            {
                XmlNode node = xmlNodeList[i];
                if (node.ParentNode.Name == parentNodeName)
                {
                    xmlNodeList[i].InnerText = newValue;
                    return;
                }
            }
            return;
        }


        public void WritePersonInformationFile(DataClass.PersonInformation personData) 
        {
            if (xmlDocument == null)
            {
                return;
            }
            //Change node values
            ChangeNodeValue("name", personData.NameValue);
            ChangeNodeValue("age", personData.AgeValue);
            ChangeNodeValue("height", personData.HeightValue);
            ChangeNodeValue("weight", personData.WeightValue);
            ChangeNodeValue("gender", personData.GenderValue);
            ChangeNodeValue("folderPath", personData.FolderPathValue);
            ChangeNodeValue("kinectVersion", personData.KinectVersionValue);
            //Save file
            SaveXmlFile();
        }

        public void WriteStdGaitPrameterFile(DataClass.StdGaitParameters stdGaitParam)
        {
            if (xmlDocument == null)
            {
                return;
            }
            // change node value
            ChangeNodeValue("cadence", stdGaitParam.CadenceValue);
            ChangeNodeValue("agegroup", stdGaitParam.AgeGroupValue);
            ChangeNodeValue("cycletime", stdGaitParam.CycleTimeValue);
            ChangeNodeValue("speedofwalking", stdGaitParam.SPeedOfWalkingValue);
            ChangeNodeValue("stepfactor", stdGaitParam.StepFactorValue);
            ChangeNodeValue("stridelength", stdGaitParam.StrideLengthValue);
            ChangeNodeValue("walkingbase", stdGaitParam.WlakingBaseValue);
            //save file
            SaveXmlFile();
        }

        public void WriteReport( DataClass.PersonInformation personInfo, DataClass.StdGaitParameters stdParm, DataClass.ObservedGaitParameters obsvParam)
        {
            XmlTextWriter writer = new XmlTextWriter("Report.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteProcessingInstruction("xml-stylesheet", "type = 'text/xsl' href = 'Resources/HTMLReport.xsl'");
            writer.WriteStartElement("report");
              writer.WriteStartElement("summary");
                CreateSummaryNode(writer, personInfo);
              writer.WriteEndElement();
              writer.WriteStartElement("standardGaitParameters");
                CreateStandardGaitParameterNode(writer, stdParm);
              writer.WriteEndElement();
              writer.WriteStartElement("observedGaitParameters");
              CreateObservedGaitParameterNode(writer, obsvParam);
              writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
        }

        private void CreateObservedGaitParameterNode(XmlTextWriter writer, ObservedGaitParameters obsvParam)
        {
            writer.WriteStartElement("stridelength");
            writer.WriteAttributeString("obsv_value", obsvParam.StrideLengthValue);
            writer.WriteEndElement();
            writer.WriteStartElement("walkingbase");
            writer.WriteAttributeString("obsv_value", obsvParam.WlakingBaseValue);
            writer.WriteEndElement();
            writer.WriteStartElement("speedofwalking");
            writer.WriteAttributeString("obsv_value", obsvParam.SPeedOfWalkingValue);
            writer.WriteEndElement();
            writer.WriteStartElement("cadence");
            writer.WriteAttributeString("obsv_value", obsvParam.CadenceValue);
            writer.WriteEndElement();
            writer.WriteStartElement("stepfactor");
            writer.WriteAttributeString("obsv_value", obsvParam.StepFactorValue);
            writer.WriteEndElement();
            writer.WriteStartElement("cycletime");
            writer.WriteAttributeString("obsv_value", obsvParam.CycleTimeValue);
            writer.WriteEndElement();
        }

        private void CreateStandardGaitParameterNode(XmlTextWriter writer, StdGaitParameters stdParm)
        {
            writer.WriteStartElement("agegroup");
            writer.WriteAttributeString("std_value", stdParm.AgeGroupValue);
            writer.WriteEndElement();
            writer.WriteStartElement("stridelength");
            writer.WriteAttributeString("std_value", stdParm.StrideLengthValue);
            writer.WriteEndElement();
            writer.WriteStartElement("walkingbase");
            writer.WriteAttributeString("std_value", stdParm.WlakingBaseValue);
            writer.WriteEndElement();
            writer.WriteStartElement("speedofwalking");
            writer.WriteAttributeString("std_value", stdParm.SPeedOfWalkingValue);
            writer.WriteEndElement();
            writer.WriteStartElement("cadence");
            writer.WriteAttributeString("std_value", stdParm.CadenceValue);
            writer.WriteEndElement();
            writer.WriteStartElement("stepfactor");
            writer.WriteAttributeString("std_value", stdParm.StepFactorValue);
            writer.WriteEndElement();
            writer.WriteStartElement("cycletime");
            writer.WriteAttributeString("std_value", stdParm.CycleTimeValue);
            writer.WriteEndElement();
        }

        private void CreateSummaryNode(XmlTextWriter writer,PersonInformation personInfo)
        {
 	        writer.WriteStartElement("name");
            writer.WriteAttributeString("value", personInfo.NameValue);
            writer.WriteEndElement();
            writer.WriteStartElement("age");
            writer.WriteAttributeString("value", personInfo.AgeValue);
            writer.WriteEndElement();
            writer.WriteStartElement("folderPath");
            writer.WriteAttributeString("value", personInfo.FolderPathValue);
            writer.WriteEndElement();
            writer.WriteStartElement("gender");
            writer.WriteAttributeString("value", personInfo.GenderValue);
            writer.WriteEndElement();
            writer.WriteStartElement("height");
            writer.WriteAttributeString("value", personInfo.HeightValue);
            writer.WriteEndElement();
            writer.WriteStartElement("kinectVersion");
            writer.WriteAttributeString("value", personInfo.KinectVersionValue);
            writer.WriteEndElement();
            writer.WriteStartElement("weight");
            writer.WriteAttributeString("value", personInfo.WeightValue);
            writer.WriteEndElement();            
            writer.WriteStartElement("timestamp");
            writer.WriteAttributeString("value", DateTime.Now.ToString());
          //  writer.WriteAttributeString("time", DateTime.Now.ToString());
            writer.WriteEndElement();
        }
    }
}
