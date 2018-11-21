using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;

namespace TFS.CopyWorkItem
{
    public class AppelService
    {
        protected string UriBase { get { return "https://dev.azure.com/"; } }

        protected string Organization { get { return "cspq"; } }

        private string ApiVersion { get { return "api-version=4.1"; } }

        public Uri UriService { get { return new Uri(string.Format("{UriBase}/{organization}/_apis/wit/workitems?{apiversion}", Organization, ApiVersion)); } }

        public AppelService()
        {
        }

        public virtual void Execute()
        {
        }
    }

    public class ProjectList : AppelService
    {
        //public override void Execute()
        //{
        //    //encode your personal access token
        //    string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", ParamsTFS.PersonalAccessToken)));

        //    ListofProjectsResponse.Projects viewModel = null;

        //    //use the httpclient
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://dev.azure.com/{OrgName}");  //url of your organization
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        //        //connect to the REST endpoint
        //        HttpResponseMessage response = client.GetAsync("_apis/projects?stateFilter=All&api-version=1.0").Result;

        //        //check to see if we have a succesfull respond
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //set the viewmodel from the content in the response
        //            viewModel = response.Content.ReadAsAsync<ListofProjectsResponse.Projects>().Result;

        //            //var value = response.Content.ReadAsStringAsync().Result;
        //        }
        //    }
        //}

        public void Execute()
        {
            //create uri and VssBasicCredential variables
            Uri uri = ParamsTFS.ObtenirUriOrganisation();
            VssBasicCredential credentials = new VssBasicCredential("", ParamsTFS.PersonalAccessToken);

            using (ProjectHttpClient projectHttpClient = new ProjectHttpClient(uri, credentials))
            {
                IEnumerable<TeamProjectReference> projects = projectHttpClient.GetProjects().Result;
                foreach (TeamProjectReference project in projects)
                {
                    Console.WriteLine("Projet:{0} - {1}", project.Id, project.Name);
                }
            }
        }
    }
}
