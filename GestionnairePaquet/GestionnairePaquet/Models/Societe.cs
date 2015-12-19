﻿using System;
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
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
           
        //Navigation double sens
        public virtual ICollection<Produit> ListeProduit { get; set; }
    }
}