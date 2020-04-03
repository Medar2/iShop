namespace Shop.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Helper;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb( DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();

        }
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");          //Administrador
            await this.userHelper.CheckRoleAsync("Customer");       //Clientes
            await this.userHelper.CheckRoleAsync("Manager");        //Gerentes
            await this.userHelper.CheckRoleAsync("Cashier");        //Cajeros
            await this.userHelper.CheckRoleAsync("Accauntant");     //Contadores
            await this.userHelper.CheckRoleAsync("Official");     //Oficiales de Oficinas
            var cities = new List<City>();

            if (!this.context.Countries.Any())
            {
                cities = new List<City>();
                cities.Add(new City { Name = "Medellín" });
                cities.Add(new City { Name = "Bogotá" });
                cities.Add(new City { Name = "Calí" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });

                cities = new List<City>();
                cities.Add(new City { Name = "Distrito Nacional" });
                cities.Add(new City { Name = "Santo Domingo" });
                cities.Add(new City { Name = "Santiago Rodriguez" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Republica Dominicana"
                });

                await this.context.SaveChangesAsync();
            }



            //var user = await this.userManager.FindByEmailAsync("jcaraballo74@hotmail.com");
            var user = await this.userHelper.GetUserbyEmailAsync("jcaraballo74@hotmail.com");
            var cityID = await this.context.Cities.FindAsync(1);

            if (user == null)
            {
                user = new User
                {
                    FirsName = "Jose",
                    LastName = "Caraballo",
                    Email = "jcaraballo74@hotmail.com",
                    UserName = "jcaraballo74@hotmail.com",
                    PhoneNumber = "350 634 2747",
                    Address = "Calle Luna Calle Sol",
                    CityId = cityID.Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };
                //user = new User();
                //user.FirsName = "Jose";
                //user.LastName = "Caraballo";
                //user.Email = "jcaraballo74@hotmail.com";
                //user.UserName = "jcaraballo74@hotmail.com";
                //user.PhoneNumber = "350 634 2747";
                //user.Address = "Calle Luna Calle Sol";
                //user.CityId = cityID.Id;
                //user.City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault();


                //var result = await this.userManager.CreateAsync(user, "123456");
                var result = await this.userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                await this.userHelper.ConfirmEmailAsync(user, token);
            }

            var IsInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!IsInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }
            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X",user);
                this.AddProduct("Magic Mause", user);
                this.AddProduct("iWhatch Serie 4", user);

                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name,User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user


            }) ;
        }
    }
}
