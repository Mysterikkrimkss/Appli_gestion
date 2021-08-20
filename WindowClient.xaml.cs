using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour WindowClient.xaml
    /// </summary>
    public partial class WindowClient : Window
    {
        public ObservableCollection<Client> observableCollection { get; set; } = new ObservableCollection<Client>();      
        void refresh()
        { // actulisation de la list l'or du lancement
            observableCollection.Clear();
            foreach (Client tr in CRUD_Client.getClient())
            {
                observableCollection.Add(tr);
            }
            ClientList.Items.Refresh();
        }
        public WindowClient()
        {
            InitializeComponent();
            refresh();
            ClientList.SelectionChanged += ClientList_SelectionChanged;
        }
        private void bt_add(object sender, RoutedEventArgs e)
        { // ajoute les données qui a sur les TBs et les envois dans le CRUD assigner
            if (tbNom_entreprise.Text != "" && tbNom_client.Text != "" && tbPrenom_client.Text != "" && tbEmail_client.Text != "" 
                && tbRaison_client.Text != "" && tbTelephone_client.Text != "" && tbAdresse_client.Text != "")
            {
                CRUD_Client.AjoutClient(tbNom_entreprise.Text, tbNom_client.Text, tbPrenom_client.Text,
                    tbEmail_client.Text, tbRaison_client.Text, Convert.ToInt32(tbTelephone_client.Text), tbAdresse_client.Text);
                MessageBox.Show("L'entreprise "+ tbNom_entreprise.Text+" est ajouter !");
                refresh();
            }
            else
            {
                MessageBox.Show("Un des champs est vide");
            }
        }
        private void ClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { // il recuper les données dans items selectionné dans la list puis les ajoute dans les TBs defini
            if (ClientList.SelectedIndex != -1)
            {
                Client cl = new Client();
                cl = (Client)ClientList.SelectedItem;
                tbId_client.Text = cl.id_client.ToString();
                tbNom_entreprise.Text = cl.nom_entreprise;
                tbNom_client.Text = cl.nom_client;
                tbPrenom_client.Text = cl.prenom_client;
                tbEmail_client.Text = cl.email_client;
                tbRaison_client.Text = cl.raison_sociale;
                tbTelephone_client.Text = cl.telephone_client.ToString();
                tbAdresse_client.Text = cl.adresse_client;           
            }
        }
        private void editUpdate(object sender, RoutedEventArgs e)
        { // update des données insere dans les TBs
            if (ClientList.SelectedIndex != -1)
            {
                if (tbId_client.Text != "" && tbNom_entreprise.Text != "" && tbNom_client.Text != "" && tbPrenom_client.Text != "" && tbEmail_client.Text != ""
                && tbRaison_client.Text != "" && tbTelephone_client.Text != "" && tbAdresse_client.Text != "")
                {
                    CRUD_Client.editClient(Convert.ToInt32(tbId_client.Text), tbNom_entreprise.Text, tbNom_client.Text, tbPrenom_client.Text,
                     tbEmail_client.Text, tbRaison_client.Text, Convert.ToInt32(tbTelephone_client.Text), tbAdresse_client.Text);
                    MessageBox.Show("L'entreprise " + tbNom_entreprise.Text + " est Modifier !");
                    refresh();
                }
                else
                {
                    MessageBox.Show("Un des champs est vide");
                }
            }
        }
        private void Suppr(object sender, RoutedEventArgs e)
        { // suppr
            if (ClientList.SelectedIndex != -1)
            {
                Client cl = new Client();
                cl = (Client)ClientList.SelectedItem;
                CRUD_Client.delClient(cl.id_client);
                tbId_client.Text = "";
                tbNom_entreprise.Text = "";
                tbNom_client.Text = "";
                tbPrenom_client.Text = "";
                tbEmail_client.Text = "";
                tbRaison_client.Text = "";
                tbTelephone_client.Text = "";
                tbAdresse_client.Text = "";
                MessageBox.Show("L'entreprise " + tbNom_entreprise.Text + " est Supprimer !");
                refresh();
            }
            else
            {
                MessageBox.Show("sa na pas marcher");
                refresh();
            }
        }

        private void tbNom_entreprise_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
