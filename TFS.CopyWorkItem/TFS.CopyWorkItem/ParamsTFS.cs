using System;

namespace TFS.CopyWorkItem
{
    public class ParamsTFS
    {
        public static string PersonalAccessToken { get { return ""; } }

        public static string UriBase { get { return "https://dev.azure.com/"; } }

        public static string Organization { get { return "cspq"; } }

        public static string ApiVersion { get { return "api-version=4.1"; } }

        public static Uri ObtenirUriOrganisation()
        { return new Uri(string.Format("{0}/{1}/", UriBase, Organization)); }

        public static Uri ObtenirUriProjet(string projet)
        { return new Uri(string.Format("{0}/{1}/{2}/", UriBase, Organization, projet)); }
    }
}
