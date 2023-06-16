using System;
using MasterProject.Core.Entities;
using MasterProject.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLHotChoclateServer.DataService
{
	public static class AppDBContectDataService
	{
         
		public static void SeedData(AppDBContext _appDBContext) 
		{
            /*
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<AppDBContext>();
                _appDBContext = context;
            }*/
            
            var UserID = Guid.NewGuid();
            var USAID = Guid.NewGuid();         
            var CanadaID = Guid.NewGuid();
            _appDBContext.Countries.Add(new Country {
                Id = USAID,
                Name = "United State",
				Code = "USA"
			});

            _appDBContext.Countries.Add(new Country{
                Id=CanadaID,
                Name = "Canada",
                Code = "CAN"
            });

            _appDBContext.SaveChanges();

            var NJID = Guid.NewGuid();
            var PAID = Guid.NewGuid();
            var NYID = Guid.NewGuid();
            var MBID = Guid.NewGuid();
            var NBID = Guid.NewGuid();
            _appDBContext.States.Add(new State {
                Id = NJID,
				Name = "New Jersey",
				Code = "NJ",
				CountryID = USAID
			});
            _appDBContext.States.Add(new State
            {
                Id= PAID,
                Name = "Philadelphia",
                Code = "PA",
                CountryID = USAID
            });

            _appDBContext.States.Add(new State
            {
                Id= NYID,
                Name = "New York",
                Code = "NY",
                CountryID = USAID
            });
            _appDBContext.States.Add(new State
            {
                Id = MBID,
                Name = "Manitoba",
                Code = "MB",
                CountryID = CanadaID
            });
            _appDBContext.States.Add(new State
            {
                Id = NBID,
                Name = "New Brunswick",
                Code = "NB",
                CountryID = CanadaID
            });
            _appDBContext.SaveChanges();

            _appDBContext.Addresses.Add(new Address
			{
				AddressLine1 = "Erat lobortis sed",
				AddressLine2 = "Orci varius natoque",
				City = "Bibendum",
				StateID = NJID,
				ZipCode = "08439"
			});

            _appDBContext.Addresses.Add(new Address
            {
                AddressLine1 = "Maecenas ut massa",
                AddressLine2 = "Vestibulum semper enim",
                City = "Ligula",
                StateID = PAID,
                ZipCode = "08439"
            });
            _appDBContext.Addresses.Add(new Address
            {
                AddressLine1 = "Molestie eros non",
                AddressLine2 = "Nulla facilisi",
                City = "Aliquet purus",
                StateID = NYID,
                ZipCode = "03439"
            });

            _appDBContext.Addresses.Add(new Address
            {
                AddressLine1 = "Nulla scelerisque mi",
                AddressLine2 = "Orci varius natoque",
                City = "Ipsum",
                StateID = MBID,
                ZipCode = "08439"
            });

            _appDBContext.Addresses.Add(new Address
            {
                AddressLine1 = "Erat Canada",
                AddressLine2 = "Orci varius natoque",
                City = "Tempor",
                StateID = NBID,
                ZipCode = "08439"
            });

            _appDBContext.SaveChanges();

            _appDBContext.Clients.Add(
				 new Client
				 {
					 Name = "Holman",
					 AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NJ").Id
				 }
				);
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Google",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NB").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Microsoft",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "MB").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Youtube",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NY").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Buget",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NJ").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Enterprise",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NY").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "AVIS",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NJ").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Hertz",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "PA").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "ZipCar",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NJ").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "JustIn",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "PA").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "Carmania",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "NJ").Id
                 }
                );
            _appDBContext.Clients.Add(
                 new Client
                 {
                     Name = "National",
                     AddressID = _appDBContext.Addresses.First(a => a.State.Code == "PA").Id
                 }
                );
            _appDBContext.SaveChanges();


            _appDBContext.Products.Add(new Product{ Name = "Cloud Computing"});
            _appDBContext.Products.Add(new Product { Name = "Cloud Storage" });
            _appDBContext.Products.Add(new Product { Name = "Car renting" });
            _appDBContext.Products.Add(new Product { Name = "Content Managment" });
            _appDBContext.SaveChanges();

            _appDBContext.Attributes.AddRange(new MasterProject.Core.Entities.Attribute { Name = "Computer", Code= "COMPUTER" });
            _appDBContext.Attributes.AddRange(new MasterProject.Core.Entities.Attribute { Name = "Car", Code = "CAR" });

            _appDBContext.SaveChanges();

            _appDBContext.Categories.Add(new Category { Name = "Hosting Services", Code="HOSTING" });
            _appDBContext.Categories.Add(new Category { Name = "Car services", Code="CAR" });

            _appDBContext.SaveChanges();

            var MicrosoftId = _appDBContext.Clients.First(c => c.Name == "Microsoft").Id;
            var GoogleId = _appDBContext.Clients.First(c => c.Name == "Google").Id;
            var HolManId = _appDBContext.Clients.First(c => c.Name == "Holman").Id;
            var YoutubeId = _appDBContext.Clients.First(c => c.Name == "Youtube").Id;
            var BugetId = _appDBContext.Clients.First(c => c.Name == "Buget").Id;
            var EnterpriseId = _appDBContext.Clients.First(c => c.Name == "Enterprise").Id;
            var AVISId = _appDBContext.Clients.First(c => c.Name == "AVIS").Id;
            var HertzId = _appDBContext.Clients.First(c => c.Name == "Hertz").Id;
            var ZipCarId = _appDBContext.Clients.First(c => c.Name == "ZipCar").Id;
            var JustInId = _appDBContext.Clients.First(c => c.Name == "JustIn").Id;
            var CarmaniaId = _appDBContext.Clients.First(c => c.Name == "Carmania").Id;
            var NationalId = _appDBContext.Clients.First(c => c.Name == "National").Id;

            var CloudComputingId = _appDBContext.Products.First(p => p.Name == "Cloud Computing").Id;
            var CloudStorageId = _appDBContext.Products.First(p => p.Name == "Cloud Storage").Id;
            var CarRentingId = _appDBContext.Products.First(p => p.Name == "Car renting").Id;
            var ContectManagementId = _appDBContext.Products.First(p => p.Name == "Content Managment").Id;

            var ComputerAttributeId = _appDBContext.Attributes.First(a => a.Name == "Computer").Id;
            var CarAttributeId = _appDBContext.Attributes.First(a => a.Name == "Car").Id;
            var CarCategoriesId = _appDBContext.Categories.First(ct => ct.Name == "Car services").Id;
            var HostingCategoriesId = _appDBContext.Categories.First(ct => ct.Name == "Hosting Services").Id;


            _appDBContext.ClientCatergories.Add(new ClientCategories { ClientID = MicrosoftId, CategoryID = HostingCategoriesId });
            _appDBContext.ClientCatergories.Add(new ClientCategories { ClientID = GoogleId, CategoryID = HostingCategoriesId });
            _appDBContext.ClientCatergories.Add(new ClientCategories { ClientID = HolManId, CategoryID = CarCategoriesId });

            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId=MicrosoftId, ProductId = CloudComputingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = GoogleId, ProductId = CloudComputingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = GoogleId, ProductId = CloudStorageId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = HolManId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = BugetId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = EnterpriseId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = AVISId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = HertzId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = ZipCarId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = JustInId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = CarmaniaId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = NationalId, ProductId = CarRentingId });
            _appDBContext.ClientProducts.Add(new ClientProduct { ClientId = YoutubeId, ProductId = ContectManagementId });

            _appDBContext.ProductAttributes.Add(new ProductAttribute { ProductId = CloudComputingId, AttributeId = ComputerAttributeId });
            _appDBContext.ProductAttributes.Add(new ProductAttribute { ProductId = CloudStorageId, AttributeId = ComputerAttributeId });
            _appDBContext.ProductAttributes.Add(new ProductAttribute { ProductId = CarRentingId, AttributeId = CarAttributeId });

            _appDBContext.SaveChanges();
        }
	}
}

