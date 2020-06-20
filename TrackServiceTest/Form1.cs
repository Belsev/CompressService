using System;
using System.Windows.Forms;
using TrackService;

namespace TrackDirChanges
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CompressorManager cm = new CompressorManager();
            cm.Start();
        }
    }
}
