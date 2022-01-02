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
    public class PropertyImageTest : IClassFixture<ServiceFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        private readonly GenericService<PropertyImage> _service;

        private readonly GenericService<Property> _propertyService;

        private readonly GenericService<Owner> _ownerService;

        public PropertyImageTest(ServiceFixture serviceFixture)
        {
            _serviceProvider = serviceFixture.ServiceProvider;
            _service = (GenericService<PropertyImage>)_serviceProvider.GetService(
                typeof(IGenericService<PropertyImage>));
            _propertyService = (GenericService<Property>)_serviceProvider.GetService(
                typeof(IGenericService<Property>));
            _ownerService = (GenericService<Owner>)_serviceProvider.GetService(
                typeof(IGenericService<Owner>));
        }

        [Fact]
        public async Task _1Add()
        {
            Response result = (Response)await _ownerService.Insert(new Owner()
            {
                Name = $"Name {DateTime.Now.Ticks}",
                Address = $"Address For {DateTime.Now.Ticks}",
                BirthDate = DateTime.Now
            });

            Assert.True(result.Status);

            result = (Response)await _ownerService.GetAll(1, 10);

            Assert.True(result.Status);

            result =
                (Response)await _propertyService.Insert(new Property()
                {
                    Name = "Test Property",
                    Address = "Test Address",
                    CodeInternal = "CodeTest",
                    Price = 1000,
                    Year = 2000,
                    OwnerId = ((IEnumerable<Owner>)result.Content).First().Id
                });

            Assert.True(result.Status);

            result = (Response)await _propertyService.GetAll(1, 10);

            Assert.True(result.Status);

            result =
                (Response)await _service.Insert(new PropertyImage()
                {
                    File = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Enabled = true,
                    PropertyId = ((IEnumerable<Property>)result.Content).First().Id
                });

            Assert.True(result.Status);
        }


        [Fact]
        public async Task _2Get()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<PropertyImage>).Any());
            result = (Response)await _service.GetById((result.Content as IEnumerable<PropertyImage>).First().Id);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _3Update()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<PropertyImage>).Any());
            PropertyImage propertyImage =((IEnumerable<PropertyImage>)result.Content).First();
            Assert.NotNull(propertyImage);
            propertyImage.Enabled = false;
            result = (Response)await _service.Update(propertyImage);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _4Delete()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<PropertyImage>).Any());
            PropertyImage propertyImage = (result.Content as IEnumerable<PropertyImage>).First();
            result = (Response)await _service.Delete(propertyImage.Id);
            Assert.True(result.Status);

            result = (Response)await _propertyService.Delete(propertyImage.PropertyId);
            Assert.True(result.Status);
        }

    }
}
