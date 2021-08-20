using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
namespace Appli
{
    class CRUD_Client : Client
    {
        public static List<Client> getClient()
        { // recu tous les clientshttp://192.168.1.96/
            var client = new RestClient("http://192.168.1.96:53984/api/Client");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(response.Content);
            return clients;
        }
        public static List<Client> getClient_name()
        { // recu que les noms des clients
            var client = new RestClient("http://192.168.1.96:53984/api/ClientGetName");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(response.Content);
            return clients;
        }
        public static void AjoutClient(string nomEntreprise, string nomClient, string prenomClient, 
            string emailClient, string raisonSociale, int telephoneClient, string adresseClient)
        { // add un client
            var client = new RestClient("http://192.168.1.96:53984/api/Client");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Client C = new Client();
            C.nom_entreprise = nomEntreprise;
            C.nom_client = nomClient;
            C.prenom_client = prenomClient;
            C.email_client = emailClient;
            C.raison_sociale = raisonSociale;
            C.telephone_client = telephoneClient;
            C.adresse_client = adresseClient;
            string json = JsonConvert.SerializeObject(C);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
        public static void editClient(int IdClient,string nomEntreprise, string nomClient, string prenomClient,
            string emailClient, string raisonSociale, int telephoneClient, string adresseClient)
        { // modif client
            var client = new RestClient("http://192.168.1.96:53984/api/Client/" + IdClient);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            Client C = new Client();
            C.id_client = IdClient;
            C.nom_entreprise = nomEntreprise;
            C.nom_client = nomClient;
            C.prenom_client = prenomClient;
            C.email_client = emailClient;
            C.raison_sociale = raisonSociale;
            C.telephone_client = telephoneClient;
            C.adresse_client = adresseClient;
            string json = JsonConvert.SerializeObject(C);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
        public static void delClient(int id_client)
        { // suppr client
            var client = new RestClient("http://192.168.1.96:53984/api/Client/" + id_client);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{  \r\n    \"ID\":1\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
    public class Client
    {
        public int id_client { get; set; }
        public string nom_entreprise { get; set; }
        public string nom_client { get; set; }
        public string prenom_client { get; set; }
        public string email_client { get; set; }
        public string raison_sociale { get; set; }
        public int telephone_client { get; set; }
        public string adresse_client { get; set; }        
    }
}
