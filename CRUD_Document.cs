using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli
{
    class CRUD_Document : Document
    {
        public static List<Document> getDocument()
        { // recup doc
            var document = new RestClient("http://192.168.1.96:53984/api/Document");
            document.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = document.Execute(request);
            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(response.Content);
            return documents;
        }

        public static List<Document> getDocumentname(string comboNomEntreprise2)
        { // recup doc
            var document = new RestClient("http://192.168.1.96:53984/api/Document/"+ comboNomEntreprise2);
            document.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = document.Execute(request);
            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(response.Content);
            return documents;
        }
        public static void AjouteDocument(string nomDocument, string fichier,
            string description, string idClient)
        { // ajout doc
            var document = new RestClient("http://192.168.1.96:53984/api/Document");
            document.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Document D = new Document();
            D.nom_doc = nomDocument;
            D.date_ajouter = DateTime.Now.Date;
            D.fichier = fichier;
            D.description = description;
            D.id_client = idClient;            
            string json = JsonConvert.SerializeObject(D);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = document.Execute(request);
        }
        public static void editDocument(int id_doc, string nom_doc, string fichier,
            string description, string comboNomEntreprise)
        { // modif doc
            var document = new RestClient("http://192.168.1.96:53984/api/Document/" + id_doc);
            document.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            Document D = new Document();
            D.id_doc = id_doc;
            D.nom_doc = nom_doc;
            D.date_ajouter = DateTime.Now.Date;
            D.fichier = fichier;
            D.description = description;
            D.id_client = comboNomEntreprise;
            string json = JsonConvert.SerializeObject(D);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = document.Execute(request);
            Console.WriteLine(response.Content);
        }
        public static void delDocument(int id_client)
        { // suppr doc
            var client = new RestClient("http://192.168.1.96:53984/api/Document/" + id_client);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{  \r\n    \"ID\":1\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
    public class Document
    {
        public int id_doc { get; set; }
        public string nom_doc { get; set; }
        public DateTime date_ajouter { get; set; }
        public string fichier { get; set; }
        public string description { get; set; }
        public string id_client { get; set; }
    }
}
