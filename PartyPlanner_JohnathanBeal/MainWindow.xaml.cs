using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyPlanner_JohnathanBeal.Class;
using System;
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

namespace PartyPlanner_JohnathanBeal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Party party;
        int index;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGui();
        }

        private void InitializeGui()
        {
            totalCostToDisplayAsLabel.Content = "";
            totalNumberOfGuestsToDisplayAsLabel.Content = "";
            ChristianNameTextbox.Text = "";
            surnameTextbox.Text = "";
            guestListListBox.Items.Clear();            
            updateButton.IsEnabled = false;
            AddGuest.IsEnabled = false;
            deleteButton.IsEnabled = false;
            changeButton.IsEnabled = false;
        }

        private void ChristianNameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SurnameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            if (!NullCheckUtility.IsNotNull(party))
            {
                MessageBox.Show("Please enter total number of guests and costs per person, and click create a list");
            }
            else
            {
                var firstname = ChristianNameTextbox.Text;
                var lastname = surnameTextbox.Text;

                if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
                {
                    string message;
                    var guestWasAddedToList = party.AddToGuestList(firstname, lastname, out message);
                    if (!guestWasAddedToList)
                    {
                        MessageBox.Show(message);
                        AddGuest.IsEnabled = false;
                    }
                    else
                    {
                        var guestlist = party.GetGuestList();
                        guestListListBox.Items.Clear();
                        foreach (var guest in guestlist)
                        {
                            guestListListBox.Items.Add(guest);
                        }
                        totalCostToDisplayAsLabel.Content = party.CostPerCapita * party.NumberOfGuests();

                        totalNumberOfGuestsToDisplayAsLabel.Content = party.NumberOfGuests();
                        changeButton.IsEnabled = true;
                        deleteButton.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a first and last name");
                }               
            }          
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var removeGuest = guestListListBox.SelectedItem;

            if (NullCheckUtility.IsNotNull(removeGuest))
            {
                var removeGuestIndex = guestListListBox.Items.IndexOf(removeGuest);
                var removedFromGuestsList = party.RemoveGuests(removeGuestIndex);
                Assert.IsTrue(removedFromGuestsList);
                guestListListBox.Items.RemoveAt(removeGuestIndex);
                guestListListBox.Items.Clear();
                foreach (var guest in party.GetGuestList())
                {
                    guestListListBox.Items.Add(guest);
                }
                totalCostToDisplayAsLabel.Content = party.CostPerCapita * party.NumberOfGuests();
                totalNumberOfGuestsToDisplayAsLabel.Content = party.NumberOfGuests();
                AddGuest.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Please select a guest");
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NullCheckUtility.IsNotNull(party))
            {
                MessageBox.Show("Please enter total number of guests and costs per person, and click create a list");
            }
            else
            {
                var name = guestListListBox.SelectedItem;
                index = guestListListBox.SelectedIndex;
                var splitName = party.SplitName(name.ToString());

                var firstname = splitName.Item2.ToLower().Trim();
                var lastname = splitName.Item1.ToLower().Trim();

                ChristianNameTextbox.Text = StringUtility.UppercaseFirst(firstname);

                surnameTextbox.Text = StringUtility.UppercaseFirst(lastname);
                updateButton.IsEnabled = true;
                AddGuest.IsEnabled = false;
            }
        }

        private void GuestCountTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void CreateListButton_Click(object sender, RoutedEventArgs e)
        {
            string guestCountText = guestCountTextbox.Text;
            if (string.IsNullOrEmpty(guestCountText))
            {
                MessageBox.Show("Please enter total number of guests and costs per person, and click create a list");
            }
            else
            {
                string message;
                var guestCount = Input.ParseIntegerInput(guestCountTextbox.Text, out message);
                if (!NullCheckUtility.IsNotNull(guestCount))
                {
                    MessageBox.Show(message);
                }
                else
                { 
                    party = new Party((int)guestCount);

                    string messageCostPerCapita;
                    double? costPerHead = Input.ReadDoubleFromWpfTextbox(costPerHeadTextbox.Text, out messageCostPerCapita);
                    if (!NullCheckUtility.IsNotNull(costPerHead))
                    {
                        MessageBox.Show(messageCostPerCapita);
                    }
                    else
                    {
                        party.CostPerCapita = (double)costPerHead;
                        createListButton.IsEnabled = false;
                        AddGuest.IsEnabled = true;
                    }
                    
                }
            }
        }

        private void CostPerHeadTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!party.Equals(null))
            {
                AddGuest.IsEnabled = false;
                var firstname = ChristianNameTextbox.Text;
                var lastname = surnameTextbox.Text;
                string updatedGuest = "";

                if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
                {
                    updatedGuest = party.UpdateGuestList(firstname, lastname, index);
                    guestListListBox.Items.Remove(guestListListBox.SelectedItem);

                    guestListListBox.Items.Insert(index, updatedGuest);

                    totalCostToDisplayAsLabel.Content = party.CostPerCapita * party.NumberOfGuests();

                    totalNumberOfGuestsToDisplayAsLabel.Content = party.NumberOfGuests();

                    AddGuest.IsEnabled = true;
                    updateButton.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Please enter a first and last name");
                }
                
            }
            else
            {
                MessageBox.Show("Please enter total number of guests and costs per person first");
            }
        }

    }
}
