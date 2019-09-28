using System;

namespace Flies.Wpf.Views
{
    public partial class ExceptionDialogContent 
    {
        public ExceptionDialogContent(Exception e)
        {
            InitializeComponent();
            DataContext = e;
        }
    }
}
