using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    public enum TypeDossier
    {
        Societe = 0,
        Produit = 1,
        Version = 2
    }

    /// <summary>
    /// Stocke l'arboresence des dossiers du système de fichier (récurrence)
    /// </summary>
    public partial class Dossier
    {
        //Champs
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public TypeDossier? TypeDossier { get; set; }
        public int ParentID { get; set; }
        public bool EstCree { get; set; }
        public bool EstDossier { get; set; }


        public Dossier()
        {
            EstCree = false;
            EstDossier = true;
        }
    }
}