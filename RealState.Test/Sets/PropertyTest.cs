using Microsoft.Extensions.DependencyInjection;
using RealState.Common.Entities;
using RealState.Common.Interfaces.Services;
using RealState.Common.Models;
using RealState.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RealState.Test.Sets
{
    [TestCaseOrderer("RealState.Test.AlphabeticalOrderer", "RealState.Test")]
    public class PropertyTest : IClassFixture<ServiceFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        private readonly GenericService<Property> _service;

        private readonly GenericService<Owner> _ownerService;

        public PropertyTest(ServiceFixture serviceFixture)
        {
            _serviceProvider = serviceFixture.ServiceProvider;
            _service = (GenericService<Property>)_serviceProvider.GetService(
                typeof(IGenericService<Property>));
            _ownerService = (GenericService<Owner>)_serviceProvider.GetService(
                typeof(IGenericService<Owner>));
        }

        [Fact]
        public async Task _1Add()
        {
            Response result = (Response) await _ownerService.Insert(new Owner() 
            { 
                Name = $"Name {DateTime.Now.Ticks}",
                Address = $"Address For {DateTime.Now.Ticks}",
                BirthDate = DateTime.Now                
            });

            Assert.True(result.Status);

            result = (Response)await _ownerService.GetAll(1, 10);

            Assert.True(result.Status);

            result =
                (Response)await _service.Insert(new Property()
                {
                    Name = "Test Property",
                    Address = "Test Address",
                    CodeInternal = "CodeTest",
                    Price = 1000,
                    Year = 2000,
                    OwnerId = (((IEnumerable<Owner>)result.Content).First()).Id
                });

            Assert.True(result.Status);
        }

        [Fact]
        public async Task _2Get()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<Property>).Any());
            Assert.Contains(result.Content as IEnumerable<Property>, x => x.Name.Equals("Test Property"));
            result = (Response)await _service.GetById((result.Content as IEnumerable<Property>).First(x => x.Name.Equals("Test Property")).Id);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _3Update()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Property Property =
                ((IEnumerable<Property>)result.Content).FirstOrDefault(x => x.Name.Equals("Test Property"));
            Assert.NotNull(Property);
            Property.Name = "Test Property Update";
            result = (Response)await _service.Update(Property);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _4Delete()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<Property>).Any());
            Assert.Contains(result.Content as IEnumerable<Property>, x => x.Name.Equals("Test Property Update"));
            Property property = (result.Content as IEnumerable<Property>).First(x => x.Name.Equals("Test Property Update"));
            result = (Response)await _service.Delete(property.Id);
            Assert.True(result.Status);

            result = (Response)await _ownerService.Delete(property.OwnerId);
            Assert.True(result.Status);
        }
    }
}
