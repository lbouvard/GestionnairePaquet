using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    /// <summary>
    /// Permet de gérer un fichier d'une version (programme, document...)
    /// </summary>
    public class Fichier
    {
        //Champs
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Extension { get; set; }
        public DateTimeOffset DateVersion { get; set; }
        public int Taille { get; set; }
        public int VersionID { get; set; }

        //Navigation double sens
        public virtual Version Version { get; set; }
    }
}