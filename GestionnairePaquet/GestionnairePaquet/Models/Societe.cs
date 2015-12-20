using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    /// <summary>
    /// Représente les différentes sociétés client du site
    /// </summary>
    public class Societe
    {
        //Champs
        [Key]
        public int ID { get; set; } 
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        [StringLength(5, ErrorMessage ="Veuillez entrer le code postal sur cinq caractères.", MinimumLength = 5)]
        public string CodePostal { get; set; }
        [Required]
        public string Ville { get; set; }
           
        //Navigation double sens
        public virtual ICollection<Produit> ListeProduit { get; set; }
    }
}