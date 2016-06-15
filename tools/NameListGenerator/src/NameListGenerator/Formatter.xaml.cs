﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace NameListGenerator
{
    /// <summary>
    /// Interaction logic for Formatter.xaml
    /// </summary>
    public partial class Formatter : UserControl
    {
        private readonly List<string> lines;
        private string result;

        public Formatter()
        {
            InitializeComponent();

            this.lines = new List<string>();
        }

        private void ParseInput()
        {
            this.lines.Clear();
            string input = this.textBoxInput.Text;
            string[] lines = input.Split('\n', '\r');

            this.lines.AddRange(lines.Where(X => !string.IsNullOrEmpty(X)));
        }

        private void ButtonFormatClick(object Sender, RoutedEventArgs E)
        {
            this.ParseInput();

            List<string> resultLines = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (string line in this.lines)
            {
                string name = line;
                if (this.checkBoxRemoveClip.IsChecked == true)
                {
                    int first = name.IndexOf('(');
                    first--;
                    if (first >= 0)
                    {
                        name = name.Substring(0, first);
                    }
                }

                if (this.checkBoxAddQuotes.IsChecked == true)
                {
                    if (name.Contains(' '))
                    {
                        name = string.Format("\"{0}\"", name);
                    }
                }

                if (this.radioButtonNoBreak.IsChecked == true)
                {
                    sb.Append(name);
                    sb.Append(" ");
                }
                else if (this.radioButtonBreakLines.IsChecked == true)
                {
                    int maxLineLength = 160;
                    if (sb.Length + name.Length + 1 >= maxLineLength)
                    {
                        resultLines.Add(sb.ToString());
                        sb.Clear();
                    }

                    sb.Append(name);
                    sb.Append(" ");
                }
                else
                {
                    resultLines.Add(name);
                }

            }
            resultLines.Add(sb.ToString());

            sb.Clear();
            foreach (string resultLine in resultLines)
            {
                sb.AppendLine(resultLine);
            }
            this.result = sb.ToString();
            this.textBoxResult.Text = result;
        }

        private void ButtonCopyResultToClipboardClick(object Sender, RoutedEventArgs E)
        {
            Clipboard.SetText(this.result, TextDataFormat.Text);
        }
    }
}