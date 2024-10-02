using System;
using System.Windows.Forms;
using NAudio.Wave;

namespace AudioPlayerApp
{
    public partial class Form1 : Form
    {
        private WaveOutEvent waveOut = new WaveOutEvent();
        private AudioFileReader audioFileReader;

        public Form1()
        {
            InitializeComponent();

            // Create the button
            Button selectFileButton = new Button();
            selectFileButton.Text = "Select Audio File";
            selectFileButton.Location = new System.Drawing.Point(50, 50);
            selectFileButton.Click += new EventHandler(SelectFileButton_Click);
            Controls.Add(selectFileButton);
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            // Open File Dialog to select audio file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.ogg;*.wav";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PlayAudio(openFileDialog.FileName);
            }
        }

        private void PlayAudio(string filePath)
        {
            // Dispose previous audio if playing
            waveOut.Stop();
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
            }

            // Load the new audio file
            audioFileReader = new AudioFileReader(filePath);
            waveOut.Init(audioFileReader);
            waveOut.Play();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            waveOut.Dispose();
            base.OnFormClosed(e);
        }
    }
}