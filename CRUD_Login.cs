using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Appli
{
    public class CRUD_Login : Login
    {
        public static List<Login> getlogin(string login, string password)
        { // recu l'id et le pass
            var loginu = new RestClient("http://192.168.1.96:53984/api/Login/" + login +"/"+ password );
            loginu.Timeout = -1;
            var request = new RestRequest(Method.GET);            
            IRestResponse response = loginu.Execute(request);
            List<Login> logins = JsonConvert.DeserializeObject<List<Login>>(response.Content);                    
            if (response.Content == "[]")
            {
                MessageBox.Show("id ou mdp pas bon");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
            else
            {
                MainWindow Main = new MainWindow();
                Main.Show();
                MessageBox.Show("vous etes connectés");
            }
            return logins;
        }
    }
    public class Login
    {    
        public int id_login { get; set; }
        public string login{ get; set; }
        public string password { get; set; }
    }
}
