using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace Appli
{
    public partial class WindowDocument : Window
    {
        public ObservableCollection<Document> observableCollection { get; set; } = new ObservableCollection<Document>();
        public ObservableCollection<Client> observableCollection2 { get; set; } = new ObservableCollection<Client>();
        void refresh()
        {
            observableCollection.Clear();
            foreach (Document tr in CRUD_Document.getDocument())
            {
                observableCollection.Add(tr);
            }
            DocumentList.Items.Refresh();   
            


        }
        public WindowDocument()
        {
            InitializeComponent();            
            refresh();
            foreach (Client tr in CRUD_Client.getClient_name())
            {
                observableCollection2.Add(tr);
            }
            DocumentList.SelectionChanged += DocumentList_SelectionChanged;
            //tbiddoc.Visibility = Visibility.Hidden;

        }


        public ObservableCollection<Document> FilterCitations(string userSearch)
        {
            ObservableCollection<Document> filteredGrid = new ObservableCollection<Document>();

            // No filter
            if (userSearch == "")
            {
                foreach (Document cit in observableCollection)
                {
                    filteredGrid.Add((Document)cit);
                }

            }
            else
            {
                // Applique le filtre
                for (int i = 0; i < observableCollection.Count; i++)
                {
                    if (observableCollection[i].nom_doc.ToUpper().Contains(userSearch.ToUpper()) && userSearch != "")
                    {
                        filteredGrid.Add(observableCollection[i]);
                    }
                }
            }
            return filteredGrid;
        }
        public void UpdateCitations(ObservableCollection<Document> filteredExercices)
        {
            DocumentList.ItemsSource = filteredExercices;
        }

        private void tbfilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCitations(FilterCitations(tbfilter.Text));
            DocumentList.Items.Refresh();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf|*.pdf";
            openFileDialog.ShowDialog();
            fichier.Text = openFileDialog.FileName;
        }

        private void bt_add(object sender, RoutedEventArgs e)
        {
            if (nomdoc.Text != "" && fichier.Text != "" && description.Text != "" && comboNomEntreprise.Text != null)
            {
                Client tra = (Client)comboNomEntreprise.SelectedItem;
                //Object selectedItem = comboNomEntreprise.SelectedItem;
                CRUD_Document.AjouteDocument(nomdoc.Text, fichier.Text, description.Text, tra.nom_client);
                nomdoc.Clear();
                fichier.Clear();
                description.Clear();
                comboNomEntreprise.SelectedIndex = -1;
                refresh();
                MessageBox.Show("le document " + nomdoc.Text + " est ajouter !");                
            }
            else
            {
                MessageBox.Show("Un des champs est vide");
            }
        }

        private void DocumentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DocumentList.SelectedIndex != -1)
            {
                Document cl = new Document();
                cl = (Document)DocumentList.SelectedItem;
                tbiddoc.Text = Convert.ToString( cl.id_doc);
                nomdoc.Text = cl.nom_doc;
                fichier.Text = cl.fichier;
                description.Text = cl.description;
                comboNomEntreprise.SelectedItem = cl.id_client;
            }
        }


        private void Suppr(object sender, RoutedEventArgs e)
        {
            if (DocumentList.SelectedIndex != -1)
            {
                Document cl = new Document();
                cl = (Document)DocumentList.SelectedItem;
                CRUD_Document.delDocument(cl.id_doc);
                nomdoc.Text = "";
                fichier.Text = "";
                description.Text = "";
                comboNomEntreprise.Text = "";                

                MessageBox.Show("L'entreprise " + nomdoc.Text + " est Supprimer !");
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
            if (DocumentList.SelectedIndex != -1)
            {
                if (nomdoc.Text != "" && fichier.Text != "" && description.Text != "" && comboNomEntreprise.Text != null )
                {
                    Client tra = (Client)comboNomEntreprise.SelectedItem;
                    CRUD_Document.editDocument(Convert.ToInt32(tbiddoc.Text), nomdoc.Text, fichier.Text, description.Text,
                     tra.nom_client);
                    tbiddoc.Clear();
                    nomdoc.Clear();
                    fichier.Clear();
                    description.Clear();
                    comboNomEntreprise.SelectedIndex = -1;
                    refresh();
                    MessageBox.Show("L'entreprise " + nomdoc.Text + " est Modifier !");
                }
                else
                {
                    MessageBox.Show("Un des champs est vide");
                }
            }
        }

    }
}
