using System.ComponentModel.Composition;
using System.Windows;
using LapScore.GUI.Applications.Views;

namespace LapScore.GUI.Presentation.Views
{
    [Export(typeof(IShellView))]
    public partial class ShellWindow : Window, IShellView
    {
        public ShellWindow()
        {
            InitializeComponent();
        }
    }
}
