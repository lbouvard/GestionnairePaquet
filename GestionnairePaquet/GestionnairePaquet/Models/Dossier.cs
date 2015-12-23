using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    /// <summary>
    /// Stocke l'arboresence des dossiers du système de fichier (récurrence)
    /// </summary>
    public class Dossier
    {
        //Champs
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public int ParentID { get; set; }
        public bool EstCree { get; set; }

        public Dossier()
        {
            EstCree = false;
        }
    }
}