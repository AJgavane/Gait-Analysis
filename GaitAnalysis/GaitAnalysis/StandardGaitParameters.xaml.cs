#region Defalut imports
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
using System.Windows.Shapes;
#endregion

#region essential Imports
using DataClass;
using XMLWrapper;
#endregion
namespace GaitAnalysis
{
    /// <summary>
    /// Interaction logic for StandardGaitParameters.xaml
    /// </summary>
    public partial class StandardGaitParameters : Window
    {
        public StandardGaitParameters()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

            XmlReader xmlReaderObj = new XmlReader("DefaultStandardGaitParameter.xml");
            xmlReaderObj.ReadDefauldStandardGaitPramFile(GaitAnalysis.MainWindow.stdGaitParametes);
            cmbAgeGroup.Text = GaitAnalysis.MainWindow.stdGaitParametes.AgeGroupValue;
            tbCadence.Text = GaitAnalysis.MainWindow.stdGaitParametes.CadenceValue;
            tbCycleTime.Text = GaitAnalysis.MainWindow.stdGaitParametes.CycleTimeValue;
            tbSpeedOfWalking.Text = GaitAnalysis.MainWindow.stdGaitParametes.SPeedOfWalkingValue;
            tbStepFactor.Text = GaitAnalysis.MainWindow.stdGaitParametes.StepFactorValue;
            tbStrideLength.Text = GaitAnalysis.MainWindow.stdGaitParametes.StrideLengthValue;
            tbWalkingBase.Text = GaitAnalysis.MainWindow.stdGaitParametes.WlakingBaseValue;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            GaitAnalysis.MainWindow.stdGaitParametes.AgeGroupValue = cmbAgeGroup.Text;
            GaitAnalysis.MainWindow.stdGaitParametes.CadenceValue = tbCadence.Text;
            GaitAnalysis.MainWindow.stdGaitParametes.CycleTimeValue = tbCycleTime.Text;
            GaitAnalysis.MainWindow.stdGaitParametes.SPeedOfWalkingValue = tbSpeedOfWalking.Text;
            GaitAnalysis.MainWindow.stdGaitParametes.StepFactorValue = tbStepFactor.Text;
            GaitAnalysis.MainWindow.stdGaitParametes.StrideLengthValue = tbStrideLength.Text;
            GaitAnalysis.MainWindow.stdGaitParametes.WlakingBaseValue = tbWalkingBase.Text;
            this.Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            cmbAgeGroup.Text = GaitAnalysis.MainWindow.stdGaitParametes.AgeGroupValue;
            tbCadence.Text = GaitAnalysis.MainWindow.stdGaitParametes.CadenceValue;
            tbCycleTime.Text = GaitAnalysis.MainWindow.stdGaitParametes.CycleTimeValue;
            tbSpeedOfWalking.Text = GaitAnalysis.MainWindow.stdGaitParametes.SPeedOfWalkingValue;
            tbStepFactor.Text = GaitAnalysis.MainWindow.stdGaitParametes.StepFactorValue;
            tbStrideLength.Text = GaitAnalysis.MainWindow.stdGaitParametes.StrideLengthValue;
            tbWalkingBase.Text = GaitAnalysis.MainWindow.stdGaitParametes.WlakingBaseValue;
        }
    }
}
