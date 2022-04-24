using System;
using HtmlAgilityPack;
using System.Collections.Generic;


namespace WebAPI
{
    public class GitHubScraper
    {
        private const string baseGitHubUrl = "https://github.com/";
        private string gitHubUrl=string.Empty;

        public GitHubScraper(string username)
        {
            gitHubUrl = baseGitHubUrl + username+"?tab=repositories";
        }

        public IEnumerable<string> GetLanguages() 
        {
            List<string> repositories = new List<string>();
            var web = new HtmlWeb();
            var document = web.Load(gitHubUrl);
            Console.WriteLine(gitHubUrl);

            var languages = document.QuerySelectorAll("input[name=language]");
            foreach ( var lan in languages)
            {
                Console.WriteLine(lan.Attributes["value"].Value);
                repositories.Add(lan.Attributes["value"].Value);
            }

            //var repos = document.QuerySelectorAll("span[itemprop~=codeRepository]");
            //Console.WriteLine(repos.Count);
            //foreach (var repo in repos)
            //{
            //    Console.WriteLine(repo.InnerText);
            //    repositories.Add(repo.InnerText);
            //}
            return repositories;
        }
        public void GetUserData()
        {

        }
    }
}
