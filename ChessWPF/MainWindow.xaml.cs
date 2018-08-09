﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessWPF
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

        bool isMouseCapture = false;


        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Border border = (Border)image.Parent;

            image.CaptureMouse();
            isMouseCapture = true;

        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }

}
