using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beijer.Thesaurus.Domain;
using Beijer.Thesaurus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Beijer.Thesaurus.Service {

    public class ThesaurusService : IThesaurusService {

        #region Members

        private readonly IRepository<Word> repository;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public ThesaurusService(IUnitOfWork unitOfWork) {

            this.unitOfWork = unitOfWork;
            repository = unitOfWork.GetRepository<Word>();

        }

        #endregion

        #region Methods

        public async Task AddSynonymsAsync(string word, string synonym) {

            ValidateAndFormatArgs(word, synonym);

            word = word.ToLower().Trim();
            synonym = synonym.ToLower().Trim();

            var synonymWord = new Synonym() { SynonymWord = new Word() { Content = synonym } };
            var existing = GetWordWithSynomyms(word);

            if (existing == null) {

                existing = new Word() { Content = word };
                existing.Synonyms.Add(synonymWord);
                repository.Add(existing);

            } else {

                if (existing.Synonyms.Any(x => x.SynonymWord.Content.Equals(synonym))) {
                    return;
                }

                synonymWord.WordId = existing.Id;
                existing.Synonyms.Add(synonymWord);

            }

            await unitOfWork.CommitAsync();

        }

        public async Task<IDictionary<string, IEnumerable<string>>> ListSynonymsAsync(string workFilter = null) {

            return await Task.Run<IDictionary<string, IEnumerable<string>>>(() => {

                var records = repository.Query()
                                       .Include(x => x.Synonyms)
                                           .ThenInclude(x => x.SynonymWord)
                                        .AsQueryable();                           

                records = string.IsNullOrWhiteSpace(workFilter)
                    ? records.Where(x => x.Synonyms.Count > 0)
                    : records.Where(x => x.Content.Equals(workFilter.ToLower().Trim()));

                var result = new Dictionary<string, IEnumerable<string>>();

                foreach (var item in records) {
                    result.Add(item.Content, item.Synonyms.Select(x => x.SynonymWord.Content));
                }

                return result;

            });           
        }

        private void ValidateAndFormatArgs(string word, string synonym) {

            if (word == null || string.IsNullOrWhiteSpace(word)) {
                throw new ArgumentNullException(nameof(word));
            }

            if (synonym == null || string.IsNullOrWhiteSpace(synonym)) {
                throw new ArgumentNullException(nameof(synonym));
            }

        }

        private Word GetWordWithSynomyms(string word) {

            return repository
                        .Query()
                        .Include(x => x.Synonyms)
                            .ThenInclude(x => x.SynonymWord)
                        .FirstOrDefault(x => x.Content.Equals(word));

        }

        #endregion

        #region Events

        #endregion

    }

}