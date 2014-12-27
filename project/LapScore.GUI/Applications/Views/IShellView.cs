using System.Waf.Applications;

namespace LapScore.GUI.Applications.Views
{
    internal interface IShellView : IView
    {
        void Show();

        void Close();
    }
}
