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
    public partial class CreatePlayer : UserControl, ISwitchable
    {
        private void NewClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.newClick();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.saveClick();
        }

        public CreatePlayer()
        {
            InitializeComponent();

            foreach (Team t in DataAccessor.Instance.teams)
            {
                TeamsSelect.Items.Add(t.ToString());
            }
        }
        
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Team t = null;

            try
            {
                t = DataAccessor.Instance.teams.Find(x => x.name == TeamsSelect.SelectedValue.ToString());
                if (t == null)
                    throw new Exception(); 
                Player p = new Player(NameBox.Text, SurnameBox.Text, t);
                DataAccessor.Instance.players.RemoveAll(x => x.name == NameBox.Text && x.surname == SurnameBox.Text);
                DataAccessor.Instance.players.Add(p);
                MessageBox.Show("Player created!", "Created");
            }
            catch (Exception)
            {
                MessageBox.Show("Can't create a player without team selected", "Error!");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataAccessor.Instance.players.RemoveAll(x => x.name == NameBox.Text && x.surname == SurnameBox.Text);
            MessageBox.Show("This player doesn't exist anymore!", "Deleted");
        }         

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Switcher.Switch(new MainMenu());
        }
    }
}
