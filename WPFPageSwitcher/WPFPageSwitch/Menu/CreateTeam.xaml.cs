using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPageSwitch
{
    public partial class CreateTeam : UserControl, ISwitchable
    {
        private void NewClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.newClick();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.saveClick();
        }

        public CreateTeam()
        {
            InitializeComponent();
        }
        
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Team t = new Team(NameBox.Text);
            DataAccessor.Instance.teams.RemoveAll(x => x.name == NameBox.Text);
            DataAccessor.Instance.teams.Add(t);
            MessageBox.Show("Team created!", "Created");
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataAccessor.Instance.teams.RemoveAll(x => x.name == NameBox.Text);
            MessageBox.Show("This team doesn't exist anymore!", "Deleted");
        }         

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Switcher.Switch(new MainMenu());
        }
    }
}
