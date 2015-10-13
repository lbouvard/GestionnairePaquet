using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Categorie { get; set; }

        public string Description { get; set; }

        public string Icone { get; set; }

        public bool Actif { get; set; }

        public virtual ICollection<Version>  ListeVersion { get; set; }
    }
}