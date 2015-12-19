using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionnairePaquet.Models
{
    /// <summary>
    /// Permet de définir une version d'un produit
    /// </summary>
    public class Version
    {
        //Champs
        [Key]
        public int ID { get; set; }
        public string Numero { get; set; }
        public string ChangeLog { get; set; }
        public int ProduitID { get; set; }

        //Navigation double sens
        public virtual Produit Produit { get; set; }
        public virtual ICollection<Fichier> ListeFichier { get; set; }
    }

}