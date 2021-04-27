using System.Threading.Tasks;
using Beijer.Thesaurus.Domain;
using Beijer.Thesaurus.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beijer.Thesaurus.WebApi.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class SynonymsController : ControllerBase {

        #region Members

        private readonly ILogger<SynonymsController> logger;
        private readonly IThesaurusService service;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public SynonymsController(IThesaurusService service, ILogger<SynonymsController> logger) {

            this.service = service;
            this.logger = logger;

        }

        #endregion

        #region Methods

        [HttpPost("synonym/{word}/{synonym}")]
        public async Task<IActionResult> SynonymsAsync(string word, string synonym) {

            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(synonym)) {
                return BadRequest($"Invalid {nameof(word)} or {nameof(synonym)}.");
            }

            await service.AddSynonymsAsync(word, synonym);
            return NoContent();

        }

        [HttpGet("synonyms/{word}")]
        public async Task<IActionResult> SynonymsAsync(string word) {

            if (string.IsNullOrWhiteSpace(word)) {
                return BadRequest($"Invalid {nameof(word)}.");
            }

            var result = await service.ListSynonymsAsync(word);
            return Ok(result);

        }

        [HttpGet("list")]
        public async Task<IActionResult> ListAsync() {

            var result = await service.ListSynonymsAsync();
            return Ok(result);

        }

        #endregion

        #region Events

        #endregion

    }

}