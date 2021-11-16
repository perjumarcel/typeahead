namespace Typeahead.WebApi.Data
{
    public class TermsDataOptions
    {
        public string ConnectionString { get; set; }

        public string SpSelectFilteredTermsName { get; set; } = "SelectFilteredTerms";
        public string SpIncreaseTermWeightName { get; set; } = "IncreaseTermWeight";
    }
}