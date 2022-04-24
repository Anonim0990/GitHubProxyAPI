using System;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebAPI
{

    public class repository 
    {
        public string name { get; set; }
    }
    public class user
    {
        [JsonPropertyName("login")]
        public string? _login { get; set; }

        [JsonPropertyName("name")]
        public string? _name { get; set; }

        [JsonPropertyName("bio")]
        public string? _bio { get; set; }
    }
    public class result1
    {
        public string? repositoryName { get; set; }
        public string? repositorylanguagesAndBytes { get; set; }
    }

    public class result2
    {
        public string? userLogin { get; set; }
        public string? userName { get; set; }
        public string? userBio { get; set; }
        public string? languagesAndBytes { get; set; }
    }

    public class GithubRequestsService : IGithubRequestsService
    {
        private const string baseGitHubUrl = "https://api.github.com/users/";
        private string gitHubUrl=string.Empty;

        private static readonly HttpClient client = new HttpClient();

        private async Task<Stream> GetTaskStream(string url )
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var res = await client.GetStreamAsync(url);
            return res;    
        }

        private async Task<string> GetTaskString(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var res = await client.GetStringAsync(url);
            return res;
        }

        private async Task<HttpResponseMessage> GetString(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var res = await client.GetAsync(url);
            return res;
        }

        public string ContentToString(HttpContent httpContent)
        {
            var readAsStringAsync = httpContent.ReadAsStringAsync();
            return readAsStringAsync.Result;
        }

        public void MyErrorHandler1(List<result1> repositories, HttpResponseMessage mess)
        {
            var a = mess.StatusCode;
            int b = (int)a;
            Console.WriteLine(b.ToString());
            repositories.Clear();
            repositories.Add(new result1 { repositoryName = b.ToString(), repositorylanguagesAndBytes = "-1" });
        }

        public void MyErrorHandler2(List<result2> informations, HttpResponseMessage mess)
        {
            var a = mess.StatusCode;
            int b = (int)a;
            Console.WriteLine(b.ToString());
            informations.Clear();
            informations.Add(new result2 { userLogin = b.ToString(), userName = "-1" ,userBio ="",languagesAndBytes=""});
        }

        public IEnumerable<result1> GetRepositories(string username) 
        {

            List<result1> repositories = new List<result1>();
            gitHubUrl = baseGitHubUrl + username + "/repos";
            var task1 = GetString(gitHubUrl);

            HttpResponseMessage mess1 = task1.Result;
            if (mess1.IsSuccessStatusCode == false)
            {
                MyErrorHandler1(repositories, mess1);
                return repositories;
            }

            var content1 = mess1.Content;
            string? answer1 = ContentToString(content1);

            var repos = JsonSerializer.Deserialize<List<repository>>(answer1);

            if(repos != null)
            {
                foreach (var repo in repos)
                {
                    string tmpUrl = "https://api.github.com/repos/"+username+"/"+repo.name+"/languages";
                    var task2 = GetString(tmpUrl);
                    HttpResponseMessage mess2 = task2.Result;
                    if (mess2.IsSuccessStatusCode == false)
                    {
                        MyErrorHandler1(repositories, mess2);
                        return repositories;
                    }
                    var content2 = mess2.Content;
                    string? answer2 = ContentToString(content2);
                    var R = new result1
                    {
                        repositoryName = repo.name,
                        repositorylanguagesAndBytes = answer2
                    };

                    repositories.Add(R);
                }
            }

            return repositories;
        }


        public IEnumerable<result2> GetUserData(string username)
        {
            List<result2> informations = new List<result2>();
            gitHubUrl = baseGitHubUrl + username;

            var task1 = GetString(gitHubUrl);

            HttpResponseMessage mess1 = task1.Result;
            if (mess1.IsSuccessStatusCode == false)
            {
                MyErrorHandler2(informations, mess1);
                return informations;
            }

            var content1 = mess1.Content;
            string? answer1 = ContentToString(content1);
            var userData = JsonSerializer.Deserialize<user>(answer1);

            gitHubUrl = baseGitHubUrl + username+"/repos";


            var task2 = GetString(gitHubUrl);

            HttpResponseMessage mess2 = task1.Result;
            if (mess2.IsSuccessStatusCode == false)
            {
                MyErrorHandler2(informations, mess2);
                return informations;
            }

            var content2 = mess1.Content;
            string? answer2 = ContentToString(content1);

            var repos = JsonSerializer.Deserialize<List<repository>>(answer2);

            List<string> projects = new List<string>();

            if (repos != null)
            {
                foreach (var repo in repos)
                {
                    projects.Add(repo.name);
                }
            }

            List<string> languagesTMP= new List<string>();
            List<int> bytes = new List<int>();

            foreach (var proj in projects)
            {
                string tmpUrl = "https://api.github.com/repos/" + username + "/" + proj + "/languages";

                var task3 = GetString(gitHubUrl);

                HttpResponseMessage mess3 = task3.Result;
                if (mess3.IsSuccessStatusCode == false)
                {
                    MyErrorHandler2(informations, mess3);
                    return informations;
                }

                var content3 = mess1.Content;
                string? answer3 = ContentToString(content1);

                string answerTMP1=answer3.Remove(0, 1);
                string answerTMP2 = answerTMP1.Remove(answerTMP1.Length-1, 1);
                  
                var tmpHelp = answerTMP2.Split('"');
                
                List<string> tmp = new List<string>();
                foreach (var item in tmpHelp)
                {
                    if (item != "")
                    {
                        tmp.Add(item);

                    }        
                }
                for(int i =0; i<tmp.Count(); i=i+2)
                {
                    if (languagesTMP.Contains(tmp[i]) == false)
                    {
                        languagesTMP.Add(tmp[i]);
                        bytes.Add(0);
                    }
                }
                for (int i = 0; i+1 < tmp.Count(); i = i + 2)
                {
                    for(int j = 0; j < languagesTMP.Count; j++)
                    {
                        if(languagesTMP[j] == tmp[i])
                        {
                            var num = tmp[i + 1];
                            if (num[num.Length - 1] == ',') num = num.Remove(num.Length - 1, 1);
                            bytes[j] = bytes[j]+ int.Parse(num.Remove(0, 1)); 
                            break;
                        }
                    }
                }
            }

            string finalAnswer=String.Empty;    
            for(int i =0;i<languagesTMP.Count;i++)
            {
                finalAnswer = finalAnswer+languagesTMP[i]+" : "+ bytes[i] +" ; ";
            }

            if (userData != null)
            {
                var R = new result2
                {
                    userLogin = userData._login,
                    userName = userData._name,
                    userBio = userData._bio,
                    languagesAndBytes = finalAnswer
                };
                informations.Add(R);
            }

            return informations;
        }
    }
}
