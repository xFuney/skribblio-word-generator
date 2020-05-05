using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribblio_Custom_Words_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random(); // Generates a variable with datatype Random.
            return random.Next(min, max); // Returns the random number.
        }

        public new void Dispose()
        {
            // Using GC does not help, the application will use 30MB no matter what.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open a file dialog, so the user can select their text file.
            // Be sure to specify the text document format.

            var FD = new System.Windows.Forms.OpenFileDialog();

            // Specifies that we should only open text files.
            FD.Filter = "txt files (*.txt)|*.txt";

            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Put the "fileToOpen" into its own variable.
                string fileToOpen = FD.FileName;

                // Get the amount of lines in the selected text file.
                int lines = System.IO.File.ReadLines(fileToOpen).Count();

                // Open a stream so we can read from the file.
                System.IO.StreamReader reader = new System.IO.StreamReader(fileToOpen);

                string outputText = "";
                string currentStr = "";

                int randomValue = 0; // We initialise it out here so we aren't initialising it constantly in the loop.
                int selectedAmount = 0;

                Random random = new Random();

                for (int i = 0; i < lines; i++)
                {
                    // This'll recurse for each line that exists.
                    // Don't bother throwing it into an array as that'll take up too much memory.

                    // For every line we'll generate a number between 0 and 1.
                    // IF 1, WE WILL NOT ADD THE NUMBER TO THE SET
                    // IF 0, WE WILL ADD THE NUMBER TO THE SET

                    // Once we've reached 29, we'll break out of this loop and produce the output to the user.

                    currentStr = reader.ReadLine();
                    //Console.WriteLine(currentStr);

                    if (currentStr[0] != '-')
                    {
                        randomValue = random.Next(0, 2) - 1;

                        if (randomValue != 0)
                        {
                            // Add one to the selected amount, and change the output text to reflect this.
                            selectedAmount++;
                            //Console.WriteLine("SelectedAmount is now " + selectedAmount + ".");
                            
                            outputText = outputText + currentStr + ",";
                           
                            //Console.WriteLine("OutputText is now " + outputText);
                        };

                        if (selectedAmount > 28)
                        {
                            // Break the FOR loop, as we have enough custom words now.
                            //Console.WriteLine("29 SELECTED, BREAKING LOOP");
                            break;
                        }
                    }
                    else
                    {
                        lines--;
                    }
                };

                // Close the file because we are finished with it.

                reader.Close();
                // Quickly remove the last character from the output to prevent a blank value being presented.

                outputText = outputText.Substring(0, outputText.Length - 1);

                // FOR loop is over, so let the user know that the output has been generated and produce it.

                output.Text = outputText;
                MessageBox.Show("Output has been generated.\nUsed " + selectedAmount + " out of " + lines + " words.", "Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Probably useless, but we'll wipe out all the used variables to reduce memory consumption.
                Dispose();
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nice, an easter egg has been found.\n\nSkribbl.io Custom Word Generator created by xFuney, 05/05/2020.\nGitHub: https://github.com/xFuney/skribblio-word-generator", "Easter Egg!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
