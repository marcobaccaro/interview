using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beijer.Thesaurus.Service {

    public interface IThesaurusService {

        #region Properties

        #endregion

        #region Methods

        Task AddSynonymsAsync(string word, string synonym);

        Task<IDictionary<string, IEnumerable<string>>> ListSynonymsAsync(string workFilter = null);

        #endregion

        #region Events

        #endregion

    }

}