using System.Collections.Generic;

namespace Beijer.Thesaurus.Domain {

    public class Word {

        #region Members

        #endregion

        #region Properties

        public long Id { get; set; }

        public string Content { get; set; }

        public Language Language { get; set; }

        public IList<Synonym> Synonyms { get; set; }

        public IList<Synonym> SynonymOf { get; set; }

        #endregion

        #region Constructors

        public Word() {
            Synonyms = new List<Synonym>();
            SynonymOf = new List<Synonym>();
        }

        #endregion

        #region Methods

        #endregion

        #region Events

        #endregion

    }
}
