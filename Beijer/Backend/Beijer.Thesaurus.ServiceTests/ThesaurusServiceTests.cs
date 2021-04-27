using NUnit.Framework;
using Beijer.Thesaurus.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Beijer.Thesaurus.Infrastructure.Persistence;
using Beijer.Thesaurus.Domain;

namespace Beijer.Thesaurus.Service.Tests {

    [TestFixture]
    public class ThesaurusServiceTests {

        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<Word>> repository;

        [SetUp]
        public void Setup() {

            unitOfWork = new Mock<IUnitOfWork>();
            repository = new Mock<IRepository<Word>>();

            unitOfWork.Setup(x => x.GetRepository<Word>()).Returns(repository.Object);

        }

        [Test]
        public void GetValidInstanceOfThesaurusService() {

            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            Assert.IsNotNull(service);

        }

        [Test]
        public void When_I_Enter_A_Word_And_Synomym_AddSynonymsAsync_Saves_Succefully() {

            IThesaurusService service = new ThesaurusService(unitOfWork.Object);            
            Assert.DoesNotThrowAsync(() => service.AddSynonymsAsync("brave", "courageous"));

        }

        [Test]
        public void When_Word_And_Synomym_Are_Null_AddSynonymsAsync_Throws_ArgumentNullException() {

            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddSynonymsAsync(null, null));

        }

        [Test]
        public void When_Word_And_Synomym_Are_Empty_AddSynonymsAsync_Throws_ArgumentNullException() {

            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddSynonymsAsync(string.Empty, string.Empty));

        }

        [Test]
        public void When_Word_Is_Valid_And_The_Synomym_Is_Empty_AddSynonymsAsync_Throws_ArgumentNullException() {

            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddSynonymsAsync("brave", string.Empty));

        }

        [Test]
        public void When_Word_Is_Empty_And_The_Synomym_Is_Valid_AddSynonymsAsync_Throws_ArgumentNullException() {

            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddSynonymsAsync(string.Empty, "courageous"));

        }

        [Test]
        public async Task When_The_Repository_Of_Words_Is_Empty_ListSynonymsAsync_Returns_An_Empty_Collection() {

            // Prepare
            var queryStub = new List<Word>();
            repository.Setup(x => x.Query()).Returns(queryStub.AsQueryable());

            // Execute
            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            var expected = await service.ListSynonymsAsync();

            // Assert
            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.Count == 0);

        }

        [Test]
        public async Task If_The_Repository_Of_Words_Contains_Two_Words_With_Synonyms_Then_ListSynonymsAsync_Returns_A_Collection_With_Records() {

            // Prepare
            var queryStub = new List<Word>();
           
            var word1 = new Word() { Id = 1, Content = "brave" };
            var synonym1 = new Word() { Id = 2, Content = "courageous" };
            var synonym2 = new Word() { Id = 2, Content = "fearless" };
            word1.Synonyms.Add(new Synonym() { SynonymWordId = synonym1.Id, SynonymWord = synonym1, WordId = word1.Id, Word = word1 });
            word1.Synonyms.Add(new Synonym() { SynonymWordId = synonym2.Id, SynonymWord = synonym2, WordId = word1.Id, Word = word1 });

            var word2 = new Word() { Id = 2, Content = "shy" };
            var synonym21 = new Word() { Id = 3, Content = "timid" };
            word2.Synonyms.Add(new Synonym() { SynonymWordId = synonym21.Id, SynonymWord = synonym21, WordId = word2.Id, Word = word2 });

            queryStub.Add(word1);
            queryStub.Add(word2);
            repository.Setup(x => x.Query()).Returns(queryStub.AsQueryable());

            // Execute
            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            var expected = await service.ListSynonymsAsync();

            // Assert
            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.Count == 2);
            Assert.IsTrue(expected["brave"].Count() == 2);
            Assert.IsTrue(expected["brave"].Contains("courageous"));
            Assert.IsTrue(expected["brave"].Contains("fearless"));
            Assert.IsTrue(expected["shy"].Contains("timid"));

        }

        [Test]
        public async Task If_The_Repository_Of_Words_Returns_The_Record_When_ListSynonymsAsync_Receives_The_Filter_Of_An_Specific_Word() {

            // Prepare
            var queryStub = new List<Word>();
            var word = new Word() { Id = 1, Content = "brave" };
            var synonym = new Word() { Id = 2, Content = "courageous" };
            word.Synonyms.Add(new Synonym() { SynonymWordId = synonym.Id, SynonymWord = synonym, WordId = word.Id, Word = word });
            queryStub.Add(word);
            repository.Setup(x => x.Query()).Returns(queryStub.AsQueryable());

            // Execute
            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            var expected = await service.ListSynonymsAsync("brave");

            // Assert
            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.Count == 1);
            Assert.IsTrue(expected["brave"].Count() == 1);
            Assert.IsTrue(expected["brave"].Contains("courageous"));

        }

        [Test]
        public async Task If_The_Repository_Of_Words_Returns_Emtpy_Collection_When_ListSynonymsAsync_Receives_The_Filter_Of_Non_Existing_Word() {

            // Prepare
            var queryStub = new List<Word>();
            var word = new Word() { Id = 1, Content = "shy" };
            var synonym = new Word() { Id = 2, Content = "timid" };
            word.Synonyms.Add(new Synonym() { SynonymWordId = synonym.Id, SynonymWord = synonym, WordId = word.Id, Word = word });
            queryStub.Add(word);
            queryStub.Add(synonym);
            repository.Setup(x => x.Query()).Returns(queryStub.AsQueryable());

            // Execute
            IThesaurusService service = new ThesaurusService(unitOfWork.Object);
            var expected = await service.ListSynonymsAsync("brave");

            // Assert
            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.Count == 0);

        }
    }
}