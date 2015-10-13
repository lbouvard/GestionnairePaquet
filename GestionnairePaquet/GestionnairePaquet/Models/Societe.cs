using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    public class Societe
    {
        [Key]
        public int Id { get; set; } 
        
        public string Nom { get; set; }

        public string Adresse { get; set; }

        public string CodePostal { get; set; }

        public string Ville { get; set; }
           
        public virtual ICollection<Produit> ListeProduit { get; set; }

    }
}