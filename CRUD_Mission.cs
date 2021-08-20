using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli
{
    class CRUD_Mission : Mission
    {
        public static List<Mission> getMission()
        { // recup doc
            var mission = new RestClient("http://192.168.1.96:53984/api/Mission");
            mission.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = mission.Execute(request);
            List<Mission> missions = JsonConvert.DeserializeObject<List<Mission>>(response.Content);
            return missions;
        }
        public static void AjouteMission(string nomMission, string temps,
             string id_client)
        { // ajout doc
            var mission = new RestClient("http://192.168.1.96:53984/api/Mission");
            mission.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Mission M = new Mission();            
            M.titre_mission = nomMission;
            M.date_mission = DateTime.Now.Date;
            M.temps_passe = temps;
            M.id_client = id_client;
            string json = JsonConvert.SerializeObject(M);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = mission.Execute(request);
        }
        public static void editMission(int id_mission, string titre_mission, string temps_passe,
            string comboNomEntreprise)
        { // modif doc
            var mission = new RestClient("http://192.168.1.96:53984/api/Mission/" + id_mission);
            mission.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            Mission M = new Mission();
            M.id_mission = id_mission;
            M.titre_mission = titre_mission;
            M.date_mission = DateTime.Now.Date;
            M.temps_passe = temps_passe;
            M.id_client = comboNomEntreprise;            
            string json = JsonConvert.SerializeObject(M);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = mission.Execute(request);
            Console.WriteLine(response.Content);
        }
        public static void delMission(int id_mission)
        { // suppr doc
            var mission = new RestClient("http://192.168.1.96:53984/api/Mission/" + id_mission);
            mission.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{  \r\n    \"ID\":1\r\n}", ParameterType.RequestBody);
            IRestResponse response = mission.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
    public class Mission
    {
        public int id_mission { get; set; }
        public string titre_mission { get; set; }
        public DateTime date_mission { get; set; }
        public string temps_passe { get; set; }
        public string id_client { get; set; }
    }
}
