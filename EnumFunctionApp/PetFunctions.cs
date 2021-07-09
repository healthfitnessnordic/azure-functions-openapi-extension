using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using Newtonsoft.Json.Converters;

namespace EnumFunctionApp
{
    public static class PetFunctions
    {
        /// <summary>
        /// Types of animals
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Animal
        {
            Dog,
            Cat,
            Elephant
        }

        public record PetCount(Animal Animal, int Count);


        [OpenApiOperation(
            operationId: nameof(GetPetCount),
            Summary = "Gets the pet count",
            Description = "Gets the number of available pets of the supplied type",
            Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PetCount), Description = "Pet count")]
        [FunctionName(nameof(GetPetCount))]
        public static async Task<IActionResult> GetPetCount(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "pet-count")] HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult(new PetCount(Animal.Dog, 3));
        }
    }
}
