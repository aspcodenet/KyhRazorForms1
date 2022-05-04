// See https://aka.ms/new-console-template for more information

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using GoodToHave.Data;

Console.WriteLine("Hello, World!");


// var dbContext = new ApplicationDbContext(...)


var searchClient = new SearchClient(new Uri("https://stefanpersonsearch.search.windows.net"),
    "personkyh", new AzureKeyCredential("EECC1FE33FD0246082BB240CCE37BF04"));

while (true)
{
    Console.Write("Ange sökord:");
    string sok = Console.ReadLine();

    var searchOptions = new SearchOptions
    {
        OrderBy = { "City desc" },
        Skip = 0,
        Size = 10,
        IncludeTotalCount = true
    };


    var searchResult = searchClient.Search<PersonInAzure>
        (sok, searchOptions);

    foreach (var result in searchResult.Value.GetResults())
    {
        Console.WriteLine(result.Document.Id);
    }


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

