using API.Common.Models.Database;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Common.Interfaces.Database
{
    public interface IDatabase : IDisposable
    {
        /// <summary>
        /// Returns a single value of type <typeparamref name="T"/> from a <paramref name="selectStatement"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="selectStatement">Select statement to be executed against the database.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>Single value of type <typeparamref name="T"/>.</returns>
        object ReturnAs(string selectStatement, Dictionary<string, object> parameters = null);

        /// <summary>
        /// Returns a single value of type <typeparamref name="T"/> from a <paramref name="selectStatement"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="selectStatement">Select statement to be executed against the database.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>Single value of type <typeparamref name="T"/>.</returns>
        Task<object> ReturnAsAsync(string selectStatement, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of values of type <typeparamref name="T"/> from a <paramref name="selectStatement"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="selectStatement">Select statement to be executed against the database.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of values of type <typeparamref name="T"/>.</returns>
        Task<IList<string>> ReturnAsListAsync(string selectStatement, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default);
        Task<IList<object>> ReturnAsObjectListAsync(string selectStatement, string modelName, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default);
        Task<object> ReturnAsObjectAsync(string selectStatement, string modelName, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default);
        Task<Dictionary<string, object>> ReturnAsDictListAsync(string selectStatement, string[] key, string modelName, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default);

        IList<object> ReturnAsObjectList(string selectStatement, string modelName, Dictionary<string, object> parameters = null);
        Dictionary<string, object> ReturnAsDictList(string selectStatement, string[] key, string modelName, Dictionary<string, object> parameters = null);
        object ReturnAsObject(string selectStatement, string modelName, Dictionary<string, object> parameters = null);
        /// <summary>
        /// Returns a list of values of type <typeparamref name="T"/> from a <paramref name="selectStatement"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="selectStatement">Select statement to be executed against the database.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of values of type <typeparamref name="T"/>.</returns>
        IList<string> ReturnAsList(string selectStatement, Dictionary<string, object> parameters = null);

        /// <summary>
        /// Inserts data into the database using an <paramref name="tableName"/> provided.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise.</returns>
        Task<int> InsertAsync(string tableName, Dictionary<string, object> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts data into the database using an <paramref name="tableName"/> provided.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise.</returns>
        int Update(string tableName, Dictionary<string, object> parameters, Dictionary<string, object> whereParameters);

        /// <summary>
        /// Inserts data into the database using an <paramref name="tableName"/> provided.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise.</returns>
        Task<int> UpdateAsync(string tableName, Dictionary<string, object> parameters, Dictionary<string, object> whereParameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts data into the database using an <paramref name="tableName"/> provided.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise.</returns>
        int Insert(string tableName, Dictionary<string, object> parameters);

        /// <summary>
        /// Inserts data into the database using a <paramref name="tableName"/> provided ignoring duplicate exceptions.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise. If a duplicate is found, returns 0.</returns>
        Task<int> InsertIgnoringDuplicatesAsync(string tableName, Dictionary<string, object> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts data into the database using a <paramref name="tableName"/> provided ignoring duplicate exceptions.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise. If a duplicate is found, returns 0.</returns>
        Task<int> ProcessTransitionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts data into the database using a <paramref name="tableName"/> provided ignoring duplicate exceptions.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise. If a duplicate is found, returns 0.</returns>
        Task<int> InsertOracleBulkArrayDataAsync(string tableName, int commitRows, Dictionary<string, OracleArrayData> bulkData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts data into the database using a <paramref name="tableName"/> provided ignoring duplicate exceptions.
        /// </summary>
        /// <param name="tableName">Table name to be populated.</param>
        /// <param name="parameters">Key-value pairs that represent parameters names and their values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The number of rows affected if known; -1 otherwise. If a duplicate is found, returns 0.</returns>
        Task<int> DeleteAsync(string tableName, Dictionary<string, object> parameters, CancellationToken cancellationToken = default);

        int Delete(string tableName, Dictionary<string, object> parameters);

        int InsertOracleBulkArrayData(string tableName, int commitRows, Dictionary<string, OracleArrayData> bulkData);

        int CustomBulkUpdate(string query, Dictionary<string, object> parameters);
    }
}