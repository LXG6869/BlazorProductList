using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProductList.Data.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string BildPath { get; set; }
        [Required(ErrorMessage ="Identifizierer can not be empty!")]
        public string Identifizierer { get; set; }

        public string Status { get; set; }

        public int Completeness { get; set; }

        public string Vorlage { get; set; }
        public string Anbieter { get; set; }

        public string Produktname { get; set; }

        public DateTime ChangedAt { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
