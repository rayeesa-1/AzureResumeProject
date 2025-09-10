using Microsoft.Extensions.Logging;
using NSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;




namespace Api.Function.Tests.Unit
{
    public class GetVisitorCounterTests
    {
        private readonly GetVisitorCounter _sut;
        private readonly ILogger<GetVisitorCounter> _logger = NullLogger<GetVisitorCounter>.Instance;

        //private readonly ICosmosDbService _mockCounterService = Substitute.For<ICosmosDbService>();
        private readonly ICosmosDbService _cosmosDbService = Substitute.For<ICosmosDbService>();

        public GetVisitorCounterTests()
        {
            _sut = new GetVisitorCounter(_cosmosDbService);
        }

        // Test methods...

        [Fact]
        public async Task IncrementCounterAsync_ShouldIncrementCount()
        {

            // To test:: If I tell the mock to return a Counter with Count = 2, then when I call the mock, I get back a Counter with Count = 2.”
            /*
            // Arrange
            //var service = new ICosmosDbService();
            var service = Substitute.For<ICosmosDbService>();
            //var counter = new Counter("index", 1);
            var updatedCounter = new Counter { Id = "index", Count = 2 };
            _cosmosDbService.IncrementCounterAsync()
               .Returns(Task.FromResult(updatedCounter));
            
            // Act
            //var updatedCounter = service.IncrementCounter(counter);
            var result = await _cosmosDbService.IncrementCounterAsync();    

            // Assert
            result.Count.Should().Be(2);
            }*/
            var currentCounter = new Counter { Id = "index", Count = 5 };
            var incrementedCounter = new Counter { Id = "index", Count = currentCounter.Count + 1 };

            _cosmosDbService.IncrementCounterAsync()
                .Returns(Task.FromResult(incrementedCounter));

            var request = TestFactory.CreateHttpRequest(); // helper to make a fake HttpRequestData

            // Act
            var response = await _sut.Run(request);

            // Assert
            response.Body.Position = 0;
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            using var reader = new StreamReader(response.Body);

            var body = await reader.ReadToEndAsync();
            body.Should().Contain("\"count\":6");

        }
    }
}