using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPFPageSwitch
{
	public partial class MainMenu : UserControl, ISwitchable
	{
		public MainMenu()
		{    
			InitializeComponent();
		}

		private void newGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Switcher.Switch(new Gameplay());
		}

        private void exploreButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new StatsExplorer());
        }
		
        private void cPlayerButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new CreatePlayer());
        }

        private void cTeamButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new CreateTeam());
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.newClick();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.saveClick();
        }

        #region Event For Child Window
        private void loginWindowForm_SubmitClicked(object sender, EventArgs e)
        {
            //ShowMessageBox("Login Successful", "Welcome, " + loginForm.NameText, MessageBoxIcon.Information);

        }

        private void registerForm_SubmitClicked(object sender, EventArgs e)
        {
        }


        #endregion

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
}