﻿using System;
using System.Windows;

namespace RandstalkerGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}
