using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    /// <summary>
    /// Représente un produit que l'on vend à nos clients
    /// </summary>
    public class Produit
    {
        //Champs
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Icone { get; set; }
        public bool Actif { get; set; }
        public int SocieteID { get; set; }

        //Navigation double sens
        public virtual Societe Societe { get; set; }
        public virtual ICollection<Version> ListeVersion { get; set; }
    }
}