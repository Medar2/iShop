using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    public class Product : IEntity
    {

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage= "Este campo no puede contener mas de 50 caracteres")]
        [Required]
        public string Name { get; set; }
        
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }
        
        [Display(Name ="Image")]
        public  string ImagenUrl { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }

        [Display(Name = "Is Availabe?")]
        public bool IsAvailabe { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public User User { get; set; } //Relacion de Uno a varios

        //Hacer esto cada vez que haya un cambio en la BD
        //agregar migracion
        //Add-migration NombeMigracion
        //Actualizar BD
        //Update-DataBase.

        //En caso de error por el Package manager Consoler, ir por CMD a la ruta del proyecto
        //y ejecutar el comando dotnet ef migration add MigrationName
        //Y para actulizar, dotnet ef database update.
    }
}
