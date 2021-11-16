using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Typeahead.WebApi.Data;

namespace Typeahead.WebApi.IntTests
{
    public class TestTermsRepository : TermsRepository
    {
        private readonly TermsDataOptions _options;

        public TestTermsRepository(TermsDataOptions options) : base(options)
        {
            _options = options;
        }

        public async Task<Weight> GetWeight(int termId, string input)
        {
            await using SqlConnection con = new SqlConnection(_options.ConnectionString);

            string sqlQuery = "SELECT * FROM Weights WHERE TermId=@TermId AND Input=@Input";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);

            cmd.Parameters.AddWithValue("@TermId", termId);
            cmd.Parameters.AddWithValue("@Input", input);

            await con.OpenAsync();
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();

            var weight = new Weight();
            while (rdr.Read())
            {
                weight.TermId = Convert.ToInt32(rdr["TermId"]);
                weight.Input = rdr["Input"].ToString();
                weight.Count = Convert.ToInt32(rdr["Count"]);
            }

            await con.CloseAsync();

            return weight;
        }
    }
}