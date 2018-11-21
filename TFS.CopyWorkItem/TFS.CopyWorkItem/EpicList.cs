using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS.CopyWorkItem
{
    class EpicList : AppelService
    {
        public void Execute()
        {
            //create uri and VssBasicCredential variables
            Uri uri = ParamsTFS.ObtenirUriOrganisation();
            VssBasicCredential credentials = new VssBasicCredential("", ParamsTFS.PersonalAccessToken);

            //create a wiql object and build our query
            Wiql wiql = new Wiql()
            {
                Query = "Select [State], [Title], [Description], [Tags] " +
                        "From WorkItems " +
                        "Where [Work Item Type] = 'Epic' " +
                        "And [System.TeamProject] = '" + "DO-Gabarit" + "' " +
                        "And [System.State] <> 'Removed'"
            };

            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql).Result;

                //some error handling                
                if (workItemQueryResult.WorkItems.Count() != 0)
                {
                    //need to get the list of our work item ids and put them into an array
                    List<int> list = new List<int>();
                    foreach (var item in workItemQueryResult.WorkItems)
                    {
                        list.Add(item.Id);
                    }
                    int[] arr = list.ToArray();

                    //build a list of the fields we want to see
                    string[] fields = new string[4];
                    fields[0] = "System.Id";
                    fields[1] = "System.Title";
                    fields[2] = "System.Tags";
                    fields[3] = "System.Description";

                    //get work items for the ids found in query
                    var workItems = workItemTrackingHttpClient.GetWorkItemsAsync(arr, fields, workItemQueryResult.AsOf).Result;

                    Console.WriteLine("Query Results: {0} items found", workItems.Count);

                    //loop though work items and write to console
                    foreach (var workItem in workItems)
                    {
                        Console.WriteLine("{0} - {1} - {2}", workItem.Id, workItem.Fields["System.Title"], workItem.Fields["System.Tags"]);
                    }

                    //return workItems;
                }

                //return null;
            }
        }
    }
}