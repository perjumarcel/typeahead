# README #

The implementation for the Typeahead project is done using the `Asp.Net Core WebApi` for backend and `Angular` for Frontend.

### How do I get set up? ###

* Summary of set up
    * `Build` and `Run` the WebApi project (`Backend/Typeahead.WebApi/Typeahead.WebApi.csproj`)
    * Run `npm install` from `UI` folder to load all necessary dependencies
    * Run `npm install -g @angular/cli` to install angular cli
    * Run `ng serve` to start the angular app
    * Open in browser the page `http://localhost:4200/`
* Dependencies
        
    No external dependencies except `Nuget` for Backend and `npm` for UI
    
* Database configuration
    1. Create `SQL Server` database
    2. Run scripts from `Database` folder in the following order: `create-tables.sql`, `insert-terms.sql`, `insert-terms-weights.sql`, `filter-procedure.sql` and `weight-increase-procedure.sql`
    3. Replace the `ConnectionString` in the `appsettings.json` with the corresponding connection string to newly created database
    4. Replace the `ConnectionString` in the `appsettings.integrationtests.json` with the corresponding connection string to newly created database or a dedicated database for tests

### Decisions ###

* Database schema

    * From the project conditions that the search starts only after 3rd character, there was added a check for `Term length >= 3`. 
    * Based on quick search, found that the longest word has the length of 45 characters, so we can set the `max length to 50`.


* Query results

    In the `SelectFilteredTerms` stored procedure, there are few things to consider.

    * Because the distinction is done in the BE project, there may be less results shown than `5 + 5 + 20`
    * `Union all` was prefered against `Union` as the latest is removing the duplicates from the `Weighted` select and keeps the one from other selects.

* Increment weight query

    The `IncreaseTermWeight` stored procedure will check if the `Term.Name` under provided `TermId` contains the `Input` otherwise will skip the `Insert`

* Writing tests

    As the business logic stays in the Database (`stored procedures`), writing unit tests doesn't bring much value in this case as we have to check the result of the stored procedures. For that the decision was made to write the integration tests instead.
    
    Preferable would be to have the same state of the database when running the tests, that can be achieved with populating a local DB like SQLite before the tests and cleaning it on dispose. Based on time provided, this solution would require a bit more time to setup so the decision was made to have the tests assuming there is a dedicated database for testing populated with data from the provided scripts.

   