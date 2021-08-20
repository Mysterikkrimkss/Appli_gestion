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

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour WindowRecette.xaml
    /// </summary>
    public partial class WindowRecette : Window
    {
        public WindowRecette()
        {
            InitializeComponent();
            refresh();
            RecetteList.SelectionChanged += RecetteList_SelectionChanged;

            combomois2.SelectionChanged += combomois2_SelectedIndexChanged;


            combomois.Items.Add("Janvier");
            combomois.Items.Add("Fevrier");
            combomois.Items.Add("Mars");
            combomois.Items.Add("Avril");
            combomois.Items.Add("Mai");
            combomois.Items.Add("Juin");
            combomois.Items.Add("Juiller");
            combomois.Items.Add("Aout");
            combomois.Items.Add("Septembre");
            combomois.Items.Add("Octobre");
            combomois.Items.Add("Novembre");
            combomois.Items.Add("Decembre");
            combomois.Items.Add("");

            combomois2.Items.Add("Janvier");
            combomois2.Items.Add("Fevrier");
            combomois2.Items.Add("Mars");
            combomois2.Items.Add("Avril");
            combomois2.Items.Add("Mai");
            combomois2.Items.Add("Juin");
            combomois2.Items.Add("Juiller");
            combomois2.Items.Add("Aout");
            combomois2.Items.Add("Septembre");
            combomois2.Items.Add("Octobre");
            combomois2.Items.Add("Novembre");
            combomois2.Items.Add("Decembre");
            combomois2.Items.Add("");


        }
        public ObservableCollection<Recette> observableCollection { get; set; } = new ObservableCollection<Recette>();


        void refresh()
        {
            observableCollection.Clear();
            foreach (Recette tr in CRUD_Recette.getRecette())
            {
                observableCollection.Add(tr);
            }
            RecetteList.Items.Refresh();

        }

        private void bt_add(object sender, RoutedEventArgs e)
        {
            if (tbmois.Text != "" && tbannee.Text != "" && tbrecette.Text != "")
            {
                CRUD_Recette.AjoutRecette(Convert.ToInt32(tbannee.Text), tbmois.Text,Convert.ToInt32(tbrecette.Text));
                tbmois.Clear();
                tbannee.Clear();
                tbrecette.Clear();
                combomois2.SelectedIndex = -1;
                MessageBox.Show("le mois " + tbmois.Text + " est ajouter !");
                refresh();
            }
            else
            {
                MessageBox.Show("Un des champs est vide");
            }
        }
        private void RecetteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecetteList.SelectedIndex != -1)
            {
                Recette R = new Recette();
                R = (Recette)RecetteList.SelectedItem;

                tbmois.Text = R.mois;
                tbannee.Text = R.annee.ToString();
                tbrecette.Text = R.prix.ToString();
                tbid.Text = R.id_recette.ToString();                
            }
        }

        private void editUpdate(object sender, RoutedEventArgs e)
        {
            if (RecetteList.SelectedIndex != -1)
            {
                if (tbmois.Text != "" && tbannee.Text != "" && tbrecette.Text != "")
                {
                    CRUD_Recette.editRecette(Convert.ToInt32(tbid.Text), Convert.ToInt32(tbannee.Text), tbmois.Text, Convert.ToInt32(tbrecette.Text));
                    MessageBox.Show("L'entreprise " + tbmois.Text +"de l'année"+ Convert.ToInt32(tbannee.Text) + " est Modifier !");
                    refresh();
                    tbmois.Clear();
                    tbannee.Clear();
                    tbrecette.Clear();
                    combomois2.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Un des champs est vide");
                }
            }
        }
        private void Suppr(object sender, RoutedEventArgs e)
        {
            if (RecetteList.SelectedIndex != -1)
            {
                Recette cl = new Recette();
                cl = (Recette)RecetteList.SelectedItem;
                CRUD_Recette.delRecette(cl.id_recette);
                tbid.Text = "";
                tbmois.Text = "";
                tbannee.Text = "";
                tbrecette.Text = "";
                combomois2.SelectedIndex = -1;


                MessageBox.Show("le mois " + tbmois.Text + "de l'année" + tbannee.Text + " est Supprimer !");
                refresh();
            }
            else
            {
                MessageBox.Show("sa na pas marcher");
                refresh();
            }
        }

        private void combomois2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combomois2.SelectedIndex == -1)
            {
                tbmois.Text = string.Empty;
            }
            else
            {
                tbmois.Text = combomois2.SelectedItem.ToString();
            }
        }
        public ObservableCollection<Recette> FilterCitations(string userSearch)
        {
            ObservableCollection<Recette> filteredGrid = new ObservableCollection<Recette>();

            // No filter
            if (userSearch == "")
            {
                foreach (Recette cit in observableCollection)
                {
                    filteredGrid.Add((Recette)cit);
                }

            }
            else
            {
                // Applique le filtre
                for (int i = 0; i < observableCollection.Count; i++)
                {
                    if (observableCollection[i].mois.ToUpper().Contains(userSearch.ToUpper()) && userSearch != "")
                    {
                        filteredGrid.Add(observableCollection[i]);
                    }
                }
            }
            return filteredGrid;
        }
        public void UpdateCitations(ObservableCollection<Recette> filteredExercices)
        {
            RecetteList.ItemsSource = filteredExercices;
        }

        private void combomois_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCitations(FilterCitations(combomois.SelectedItem.ToString()));
            RecetteList.Items.Refresh();
        }

        public ObservableCollection<Recette> FilterCitations2(string userSearch)
        {
            ObservableCollection<Recette> filteredGrid = new ObservableCollection<Recette>();

            // No filter
            if (userSearch == "")
            {
                foreach (Recette cit in observableCollection)
                {
                    filteredGrid.Add((Recette)cit);
                }

            }
            else
            {
                // Applique le filtre
                for (int i = 0; i < observableCollection.Count; i++)
                {
                    if (observableCollection[i].annee.ToString().ToUpper().Contains(userSearch.ToUpper()) && userSearch != "")
                    {
                        filteredGrid.Add(observableCollection[i]);
                    }
                }
            }
            return filteredGrid;
        }
        public void UpdateCitations2(ObservableCollection<Recette> filteredExercices)
        {
            RecetteList.ItemsSource = filteredExercices;
        }       

        private void tbfannee_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCitations(FilterCitations(tbfannee.Text));
            RecetteList.Items.Refresh();
        }
    }
}
