using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
#if !DEBUG
using Microsoft.AspNetCore.Authorization;
#endif
using Microsoft.AspNetCore.Mvc;
using Netatmo.Dashboard.Api.GraphQL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Netatmo.Dashboard.Api.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter documentExecuter;
        private readonly ISchema schema;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            this.schema = schema ?? throw new ArgumentNullException(nameof(schema));
            this.documentExecuter = documentExecuter ?? throw new ArgumentNullException(nameof(documentExecuter));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = schema,
                Query = query.Query,
                Inputs = inputs
            };
            var result = await documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);
            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
