using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Typeahead.WebApi.Model;

namespace Typeahead.WebApi.Data
{
    public class TermsRepository
    {
        private readonly TermsDataOptions _options;

        public TermsRepository(TermsDataOptions options)
        {
            _options = options;
        }

        public async Task WeightIncrease(int termId, string input)
        {
            await using var con = new SqlConnection(_options.ConnectionString);

            var cmd = new SqlCommand(_options.SpIncreaseTermWeightName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TermId", termId);
            cmd.Parameters.AddWithValue("@Input", input);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        public async Task<IEnumerable<Term>> GetFilteredTerms(string input)
        {
            var terms = new List<Term>();
            await using var con = new SqlConnection(_options.ConnectionString);

            var cmd = new SqlCommand(_options.SpSelectFilteredTermsName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Input", input);

            await con.OpenAsync();
            var rdr = await cmd.ExecuteReaderAsync();

            while (rdr.Read())
            {
                var term = new Term { Id = Convert.ToInt32(rdr["Id"]), Name = rdr["Name"].ToString() };

                terms.Add(term);
            }

            await con.CloseAsync();

            return terms.Distinct();
        }
    }
}