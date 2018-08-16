using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class Board : UserControl, INotifyPropertyChanged
    {
        public Board()
        {
            InitializeComponent();
            Binding b = new Binding();
            b.Source = this;
            b.Path = new PropertyPath("DisplayedImage");
            imgChessPiece.SetBinding(Image.SourceProperty, b);
        }

        public ImageSource displayedImage;

        public event PropertyChangedEventHandler PropertyChanged;

        public ImageSource DisplayedImage
        {
            get { return displayedImage; }
            set
            {
                displayedImage = value;
                new PropertyChangedEventArgs("DisplayedImage");
            }
        }
    }
}
