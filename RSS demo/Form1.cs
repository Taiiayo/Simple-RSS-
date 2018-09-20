using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
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
                XmlReader fd_readxml = XmlReader.Create(textBox1.Text);
                SyndicationFeed fd_feed = SyndicationFeed.Load(fd_readxml);

                TabPage fd_tab = new TabPage(fd_feed.Title.Text);

                tabControl1.TabPages.Add(fd_tab);

                ListBox fd_list = new ListBox();
                fd_tab.Controls.Add(fd_list);
                fd_list.Dock = DockStyle.Fill;
                fd_list.HorizontalScrollbar = true;

                foreach (SyndicationItem fd_item in fd_feed.Items)
                {
                    string summary = fd_item.Summary.Text;
                    bool isRunning = true;
                    int numberOfLetters = 250;

                    string fix_sum = "";


                    foreach (char characterData in summary)
                    {
                        if (numberOfLetters >= 0 && characterData != '.' && isRunning)
                        {
                            numberOfLetters--;
                            fix_sum = fix_sum + characterData;
                        }
                        else if (characterData == '.' && isRunning && (numberOfLetters <= 100 & numberOfLetters >= 0))
                        {
                            fix_sum = fix_sum + characterData;
                            isRunning = false;
                        }
                        else if (isRunning && numberOfLetters >= 0)
                        {
                            fix_sum = fix_sum + characterData;
                        }
                    }

                    fd_list.Items.Add(fd_item.Title.Text);
                    fd_list.Items.Add(fix_sum);
                    fd_list.Items.Add("~ ~ ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}