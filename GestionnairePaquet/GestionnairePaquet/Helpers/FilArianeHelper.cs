using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Helpers
{
    public class Fil
    {
        public string Url { get; set; }
        public string Nom { get; set; }
    }

    public class FilArianeHelper
    {
        // Private Member
        List<Fil> filariane = new List<Fil>();

        /// <summary>
        /// Add node to breadcrumb
        /// </summary>
        /// <param name="Url">Url</param>
        /// <param name="Nom">Nom</param>
        public void AddNode(string Url, string Nom)
        {
            this.filariane.Add(new Fil
            {
                Url = Url,
                Nom = Nom
            });
        }

        /// <summary>
        /// Outputs html to user
        /// </summary>
        /// <returns>string</returns>
        public IHtmlString Output()
        {
            string html = "";
            int count = 1;

            foreach (var fil in filariane)
            {
                //see if this is the last item on the list.
                if (filariane.Count == count)
                {
                    html += "<li class=\"active\">" + fil.Nom + "</li>";
                }
                else
                {
                    html += "<li><a href='" + fil.Url + "'>" + fil.Nom + "</a></li>";
                    count++;
                }
            }

            return new System.Web.Mvc.MvcHtmlString(html);
        }
    }
}