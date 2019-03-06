using System;
using System.ServiceModel.Syndication;
using System.Windows.Forms;
using System.Xml;

namespace RSS_demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create(textBox1.Text);
                SyndicationFeed syndicationFeed = SyndicationFeed.Load(xmlReader);

                TabPage tabPage = new TabPage(syndicationFeed.Title.Text);

                tabControl1.TabPages.Add(tabPage);

                ListBox listBox = new ListBox();
                tabPage.Controls.Add(listBox);
                listBox.Dock = DockStyle.Fill;
                listBox.HorizontalScrollbar = true;

                foreach (SyndicationItem syndicationItem in syndicationFeed.Items)
                {
                    string summaryText = syndicationItem.Summary.Text;
                    bool isRunning = true;
                    int numberOfLetters = 250;

                    string sum = "";

                    foreach (char characterData in summaryText)
                    {
                        if (numberOfLetters >= 0 && characterData != '.' && isRunning)
                        {
                            numberOfLetters--;
                            sum = sum + characterData;
                        }
                        else if (characterData == '.' && isRunning && (numberOfLetters <= 100 & numberOfLetters >= 0))
                        {
                            sum = sum + characterData;
                            isRunning = false;
                        }
                        else if (isRunning && numberOfLetters >= 0)
                        {
                            sum = sum + characterData;
                        }
                    }

                    listBox.Items.Add(syndicationItem.Title.Text);
                    listBox.Items.Add(sum);
                    listBox.Items.Add("~ ~ ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}