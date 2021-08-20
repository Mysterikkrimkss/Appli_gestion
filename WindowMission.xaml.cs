using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace Appli
{
    public partial class WindowMission : Window
    {
        public ObservableCollection<Mission> observableCollection { get; set; } = new ObservableCollection<Mission>();
        public ObservableCollection<Client> observableCollection2 { get; set; } = new ObservableCollection<Client>();
        void refresh()
        {
            observableCollection.Clear();
            foreach (Mission tr in CRUD_Mission.getMission())
            {
                observableCollection.Add(tr);
            }
            MissionList.Items.Refresh();   
            


        }
        public WindowMission()
        {
            InitializeComponent();            
            refresh();
            foreach (Client tr in CRUD_Client.getClient_name())
            {
                observableCollection2.Add(tr);
            }
            MissionList.SelectionChanged += DocumentList_SelectionChanged;
            //tbiddoc.Visibility = Visibility.Hidden;

        }


        public ObservableCollection<Mission> FilterCitations(string userSearch)
        {
            ObservableCollection<Mission> filteredGrid = new ObservableCollection<Mission>();

            // No filter
            if (userSearch == "")
            {
                foreach (Mission cit in observableCollection)
                {
                    filteredGrid.Add((Mission)cit);
                }

            }
            else
            {
                // Applique le filtre
                for (int i = 0; i < observableCollection.Count; i++)
                {
                    if (observableCollection[i].titre_mission.ToUpper().Contains(userSearch.ToUpper()) && userSearch != "")
                    {
                        filteredGrid.Add(observableCollection[i]);
                    }
                }
            }
            return filteredGrid;
        }
        public void UpdateCitations(ObservableCollection<Mission> filteredExercices)
        {
            MissionList.ItemsSource = filteredExercices;
        }

        private void tbfilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCitations(FilterCitations(tbfilter.Text));
            MissionList.Items.Refresh();
        }
       

        private void bt_add(object sender, RoutedEventArgs e)
        {
            if (nomMission.Text != "" && temps.Text != "" && comboNomEntreprise.Text != null)
            {
                Client tra = (Client)comboNomEntreprise.SelectedItem;
                //Object selectedItem = comboNomEntreprise.SelectedItem;
                CRUD_Mission.AjouteMission(nomMission.Text, temps.Text, tra.nom_client);
                nomMission.Clear();
                temps.Clear();                
                comboNomEntreprise.SelectedIndex = -1;
                refresh();
                MessageBox.Show("la mission " + nomMission.Text + " est ajouter !");                
            }
            else
            {
                MessageBox.Show("Un des champs est vide");
            }
        }

        private void DocumentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MissionList.SelectedIndex != -1)
            {
                Mission cl = new Mission();
                cl = (Mission)MissionList.SelectedItem;
                tbiddoc.Text = Convert.ToString( cl.id_mission);
                nomMission.Text = cl.titre_mission;
                temps.Text = cl.temps_passe;
                comboNomEntreprise.SelectedItem = cl.id_client;
            }
        }


        private void Suppr(object sender, RoutedEventArgs e)
        {
            if (MissionList.SelectedIndex != -1)
            {
                Mission cl = new Mission();
                cl = (Mission)MissionList.SelectedItem;
                CRUD_Mission.delMission(cl.id_mission);
                nomMission.Text = "";
                temps.Text = "";                
                comboNomEntreprise.Text = "";                

                MessageBox.Show("la mission " + nomMission.Text + " est Supprimer !");
                refresh();
            }
            else
            {
                MessageBox.Show("sa na pas marcher");
                refresh();
            }
        }


        private void editUpdate(object sender, RoutedEventArgs e)
        {
            if (MissionList.SelectedIndex != -1)
            {
                if (nomMission.Text != "" && temps.Text != "" && comboNomEntreprise.Text != null)
                {
                    Client tra = (Client)comboNomEntreprise.SelectedItem;
                    CRUD_Mission.editMission(Convert.ToInt32(tbiddoc.Text), nomMission.Text, temps.Text, tra.nom_client);
                    tbiddoc.Clear();
                    nomMission.Clear();
                    temps.Clear();                    
                    comboNomEntreprise.SelectedIndex = -1;
                    refresh();
                    MessageBox.Show("la mission " + nomMission.Text + " est Modifier !");
                }
                else
                {
                    MessageBox.Show("Un des champs est vide");
                }
            }
        }
    }
}
