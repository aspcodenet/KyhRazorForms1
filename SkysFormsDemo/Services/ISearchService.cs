using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using GoodToHave.Data;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SkysFormsDemo.Infrastructure.Paging;

namespace SkysFormsDemo.Services;

public interface ISearchService
{
    PagedResult<Person> Search(string q, int page);
}

public class SearchService : ISearchService
{
    private readonly ApplicationDbContext _context;

    public SearchService(ApplicationDbContext context)
    {
        _context = context;
        //CreateIndexIfNotExists();
        //InsertData();
        SearchTest();
    }

    public void SearchTest()
    {

    }

    private void InsertData()
    {
        var searchClient = new SearchClient(new Uri("https://stefanpersonsearch.search.windows.net"),
            "personkyh", 
            new AzureKeyCredential("EECC1FE33FD0246082BB240CCE37BF04"));

        var batch = new IndexDocumentsBatch<PersonInAzure>();
        foreach (var person in _context.Person)
        {
            var personInAzure = new PersonInAzure
            {
                City = person.City,
                //Description = person.Description,
                Id = person.Id.ToString(),
                Namn = person.Name,
                StreetAddress = person.StreetAddress
            };
            batch.Actions.Add(new IndexDocumentsAction<PersonInAzure>(IndexActionType.MergeOrUpload,
                personInAzure));
        }
        var result = searchClient.IndexDocuments(batch).Value;
    }

    public class PersonInAzure
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string Id { get; set; }

        [SearchableField(IsSortable = true)]
        public string Namn { get; set; }

        [SearchableField(IsSortable = true)]
        public string StreetAddress { get; set; }

        [SearchableField(IsSortable = true)]
        public string City { get; set; }



        //Personbeskrivning mwed vanlig text - stol/stolar osv sov

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.SvLucene)]
        public string Description { get; set; }

    }

    private void CreateIndexIfNotExists()
    {
        var serviceEndpoint = new Uri("https://stefanpersonsearch.search.windows.net");
        var credential = new AzureKeyCredential("EECC1FE33FD0246082BB240CCE37BF04");
        var adminClient = new SearchIndexClient(serviceEndpoint, credential);

        var fieldBuilder = new FieldBuilder();
        var searchFields = fieldBuilder.Build(typeof(PersonInAzure));

        var definition = new SearchIndex("personkyh", searchFields);

        adminClient.CreateOrUpdateIndex(definition);

    }


    public PagedResult<Person> Search(string q, int page)
    {
        throw new NotImplementedException();
    }
}