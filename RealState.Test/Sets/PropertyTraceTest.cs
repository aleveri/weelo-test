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
    public class PropertyTraceTest : IClassFixture<ServiceFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        private readonly GenericService<PropertyTrace> _service;

        private readonly GenericService<Property> _propertyService;

        private readonly GenericService<Owner> _ownerService;

        public PropertyTraceTest(ServiceFixture serviceFixture)
        {
            _serviceProvider = serviceFixture.ServiceProvider;
            _service = (GenericService<PropertyTrace>)_serviceProvider.GetService(
                typeof(IGenericService<PropertyTrace>));
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

            result = (Response)await _ownerService.GetAll(1, 1);

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

            result = (Response)await _propertyService.GetAll(1, 1);

            Assert.True(result.Status);

            result =
                (Response)await _service.Insert(new PropertyTrace()
                {
                    Name = "Test Property Trace",
                    Tax = 10.85,
                    Value = 502000.25,
                    DateSale = DateTime.Now.AddDays(7),
                    PropertyId = ((IEnumerable<Property>)result.Content).First().Id
                });

            Assert.True(result.Status);
        }

        [Fact]
        public async Task _2Get()
        {
            Response result = (Response)await _service.GetAll(1, 1);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<PropertyTrace>).Any());
            result = (Response)await _service.GetById((result.Content as IEnumerable<PropertyTrace>).First().Id);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _3Update()
        {
            Response result = (Response)await _service.GetAll(1, 1);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<PropertyTrace>).Any());
            PropertyTrace propertyTrace = ((IEnumerable<PropertyTrace>)result.Content).First();
            Assert.NotNull(propertyTrace);
            propertyTrace.Name = "Test Property Trace Updated";
            result = (Response)await _service.Update(propertyTrace);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _4Delete()
        {
            Response result = (Response)await _service.GetAll(1, 1);
            Assert.True(result.Status);
            Assert.True((result.Content as IEnumerable<PropertyTrace>).Any());
            PropertyTrace propertyTrace = (result.Content as IEnumerable<PropertyTrace>).First();
            result = (Response)await _service.Delete(propertyTrace.Id);
            Assert.True(result.Status);

            result = (Response)await _propertyService.Delete(propertyTrace.PropertyId);
            Assert.True(result.Status);
        }
    }
}
