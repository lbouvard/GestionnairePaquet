using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    public class Version
    {
        [Key]
        public int Id { get; set; }

        public string Numero { get; set; }

        public string ChangeLog { get; set; }

        public virtual Produit Logiciel { get; set; }

        public virtual ICollection<Fichier> ListeFichier { get; set; }
    }

}