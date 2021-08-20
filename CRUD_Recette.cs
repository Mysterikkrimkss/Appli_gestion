using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli
{
    class CRUD_Recette : Recette
    {
        public static List<Recette> getRecette()
        { // recup tout les données
            var recette = new RestClient("http://192.168.1.96:53984/api/Recette/");
            recette.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = recette.Execute(request);
            List<Recette> Recettes = JsonConvert.DeserializeObject<List<Recette>>(response.Content);
            return Recettes;
        }
        public static void AjoutRecette(int annee, string mois, int prix)
        { // insert recette
            var Recette = new RestClient("http://192.168.1.96:53984/api/Recette");
            Recette.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Recette R = new Recette();
            R.annee = annee;
            R.mois = mois;
            R.prix = prix;            
            string json = JsonConvert.SerializeObject(R);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = Recette.Execute(request);
        }
        public static void editRecette(int id_recette,int annee, string mois, int prix)
        { // modif recette
            var Recette = new RestClient("http://192.168.1.96:53984/api/Recette/" + id_recette);
            Recette.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            Recette R = new Recette();
            R.annee = annee;
            R.mois = mois;
            R.prix = prix;
            string json = JsonConvert.SerializeObject(R);

            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = Recette.Execute(request);
            Console.WriteLine(response.Content);
        }
        public static void delRecette(int id_recette)
        { // suppr recette
            var Recette = new RestClient("http://192.168.1.96:53984/api/Recette/" + id_recette);
            Recette.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{  \r\n    \"ID\":1\r\n}", ParameterType.RequestBody);
            IRestResponse response = Recette.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
    public class Recette
    {    
        public int id_recette { get; set; }
        public int annee { get; set; }
        public string mois { get; set; }
        public int prix { get; set; }
    }
}
