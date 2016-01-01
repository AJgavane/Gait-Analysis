#region Default imports
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

#region Essential Imports
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using DataClass;
using XMLWrapper;
#endregion

namespace GaitAnalysis
{
    /// <summary>
    /// Interaction logic for Information.xaml
    /// </summary>
    public partial class Information : Window
    {
        public Information()
        {
            InitializeComponent();
        }

        private void frmInformation_Loaded(object sender, RoutedEventArgs e)
        {
            tbName.Text = GaitAnalysis.MainWindow.personInformation.NameValue;
            tbHeight.Text = GaitAnalysis.MainWindow.personInformation.HeightValue;
            tbWeight.Text = GaitAnalysis.MainWindow.personInformation.WeightValue;
            tbAge.Text = GaitAnalysis.MainWindow.personInformation.AgeValue;
            tbFolderPath.Text = GaitAnalysis.MainWindow.personInformation.FolderPathValue;
            cmbGender.Text = GaitAnalysis.MainWindow.personInformation.GenderValue;
            cmbKinectVersion.Text = GaitAnalysis.MainWindow.personInformation.KinectVersionValue;
        }

        private void frmInformation_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            tbFolderPath.Text = folderBrowser.SelectedPath;
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbAge.Text = "";
            tbFolderPath.Text = "";
            tbHeight.Text = "";
            tbWeight.Text = "";
            tbName.Text = "";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            GaitAnalysis.MainWindow.personInformation.NameValue = tbName.Text;
            GaitAnalysis.MainWindow.personInformation.AgeValue = tbAge.Text;
            GaitAnalysis.MainWindow.personInformation.HeightValue = tbHeight.Text;
            GaitAnalysis.MainWindow.personInformation.WeightValue = tbWeight.Text;
            GaitAnalysis.MainWindow.personInformation.FolderPathValue = tbFolderPath.Text;
            GaitAnalysis.MainWindow.personInformation.GenderValue = cmbGender.Text;
            GaitAnalysis.MainWindow.personInformation.KinectVersionValue = cmbKinectVersion.Text;
            this.Close();
        }


        
    }
}
