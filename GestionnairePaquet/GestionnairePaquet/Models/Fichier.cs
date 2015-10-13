using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    public class Fichier
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Extension { get; set; }

        public DateTimeOffset DateVersion { get; set; }

        public int Taille { get; set; }

        public string Chemin { get; set; }

    }
}