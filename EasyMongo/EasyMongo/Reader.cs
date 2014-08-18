﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EasyMongo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;
using EasyMongo.Contract;

namespace EasyMongo
{
    public class Reader : IReader
    {
        IDatabaseConnection _databaseConnection;

        public Reader(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        #region    Methods
        #region   Read Against a Collection
        /// <summary>
        /// Synchronously searches against a MongoDB collection
        /// </summary>
        /// <remarks>Proxy method to private implementation method CollectionDateRangeRead</remarks>
        /// <param name="collectionName">The MongoDB Collection to be read from</param>
        /// <param name="dateTimeFieldName">The name of the field (property) of the persisted object associated with a DateTime object</param>
        /// <param name="start">The time at which search should begin</param>
        /// <param name="end">The time at which search should end</param>
        /// <returns>IEnumberable<T> of read results from the MongoDB</returns>
        public IEnumerable<T> Read<T>(string collectionName, string dateTimeFieldName, DateTime start, DateTime end)
        {
            List<T> results;
            Find(collectionName, dateTimeFieldName, start, end, out results);
            return results;
        }
        /// <summary>
        /// Synchronously searches against a MongoDB collection
        /// </summary>
        /// <remarks>Proxy method to private implementation method CollectionDateRangePropertyRead</remarks>
        /// <param name="collectionName">The MongoDB Collection to be read from</param>
        /// <param name="fieldName">The name of the field (property) of the persisted object that will be searched for a matching regexPattern</param>
        /// <param name="regexPattern">A string representing text to search a fieldName for. An objected with an associated match will be returned as a result</param>
        /// <param name="dateTimeFieldName">The name of the field (property) of the persisted object associated with a DateTime object</param>
        /// <param name="start">The time at which search should begin</param>
        /// <param name="end">The time at which search should end</param>
        /// <returns>IEnumberable<T> of read results from the MongoDB</returns>
        public IEnumerable<T> Read<T>(string collectionName, string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end)
        {
            List<T> results;
            Find(collectionName, fieldName, regexPattern, dateTimeFieldName, start, end, out results);
            return results;
        }
        /// <summary>
        /// Synchronously searches against a MongoDB collection
        /// </summary>
        /// <remarks>Proxy method to private implementation method CollectionPropertyRead</remarks>
        /// <param name="collectionName">>The MongoDB Collection to be read from</param>
        /// <param name="fieldName">The name of the field (property) of the persisted object that will be searched for a matching regexPattern</param>
        /// <param name="regexPattern">A string representing text to search a fieldName for. An objected with an associated match will be returned as a result</param>
        /// <returns>IEnumberable<T> of read results from the MongoDB</returns>
        public IEnumerable<T> Read<T>(string collectionName, string fieldName, string regexPattern)
        {
            List<T> results;
            Find(collectionName, fieldName, regexPattern, out results);
            return results;
        }
        #endregion Read Against a Collection
        #region   Read Against Multiple Collections
        /// <summary>
        /// Synchronously searches against multiple MongoDB collections
        /// </summary>
        /// <remarks>Proxy method to private implementation method DateRangeRead</remarks>
        /// <param name="collectionName">The MongoDB Collection to be read from</param>
        /// <param name="dateTimeFieldName">The name of the field (property) of the persisted object associated with a DateTime object</param>
        /// <param name="start">The time at which search should begin</param>
        /// <param name="end">The time at which search should end</param>
        /// <returns>IEnumberable<T> of read results from the MongoDB</returns>
        public IEnumerable<T> Read<T>(IEnumerable<string> collectionNames, string dateTimeFieldName, DateTime start, DateTime end)
        {
            List<T> results;
            Find(collectionNames, dateTimeFieldName, start, end, out results);
            return results;
        }
        /// <summary>
        /// Synchronously searches against multiple MongoDB collections
        /// </summary>
        /// <remarks>Proxy method to private implementation method DateRangePropertyRead</remarks>
        /// <param name="collectionName">The MongoDB Collection to be read from</param>
        /// <param name="fieldName">The name of the field (property) of the persisted object that will be searched for a matching regexPattern</param>
        /// <param name="regexPattern">A string representing text to search a fieldName for. An objected with an associated match will be returned as a result</param>
        /// <param name="dateTimeFieldName">The name of the field (property) of the persisted object associated with a DateTime object</param>
        /// <param name="start">The time at which search should begin</param>
        /// <param name="end">The time at which search should end</param>
        /// <returns>IEnumberable<T> of read results from the MongoDB</returns>
        public IEnumerable<T> Read<T>(IEnumerable<string> collectionNames, string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end)
        {
            List<T> results;
            Find(collectionNames, fieldName, regexPattern, dateTimeFieldName, start, end, out results);
            return results;
        }
        /// <summary>
        /// Synchronously searches against multiple MongoDB collections
        /// </summary>
        /// <remarks>Proxy method to private implementation method PropertyRead</remarks>
        /// <param name="collectionName">>The MongoDB Collection to be read from</param>
        /// <param name="fieldName">The name of the field (property) of the persisted object that will be searched for a matching regexPattern</param>
        /// <param name="regexPattern">A string representing text to search a fieldName for. An objected with an associated match will be returned as a result</param>
        /// <returns>IEnumberable<T> of read results from the MongoDB</returns>
        public IEnumerable<T> Read<T>(IEnumerable<string> collectionNames, string fieldName, string regexPattern)
        {
            List<T> results;
            Find(collectionNames, fieldName, regexPattern, out results);
            return results;
        }
        #endregion Read Against Multiple Collections
        #region Read Implementation methods
        private void Find<T>(string collectionName, string dateTimeFieldName, DateTime start, DateTime end, out List<T> results)
        {
            results = new List<T>();
            var searchQuery = Query.And(Query.GTE(dateTimeFieldName, start), Query.LTE(dateTimeFieldName, end));
            var collection = _databaseConnection.GetCollection<T>(collectionName);
            results.AddRange(collection.Find(searchQuery));
        }
        private void Find<T>(string collectionName, string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end, out List<T> results)
        {
            results = new List<T>();
            var searchQuery = Query.And(Query.GTE(dateTimeFieldName, start), Query.LTE(dateTimeFieldName, end), Query.Matches(fieldName, new BsonRegularExpression(regexPattern)));
            var collection = _databaseConnection.GetCollection<T>(collectionName);
            results.AddRange(collection.Find(searchQuery));
        }
        private void Find<T>(string collectionName, string fieldName, string regexPattern, out List<T> results)
        {
            results = new List<T>();
            var searchQuery = Query.Matches(fieldName, new BsonRegularExpression(regexPattern));
            var collection = _databaseConnection.GetCollection<T>(collectionName);
            results.AddRange(collection.Find(searchQuery));
        }
        private void Find<T>(IEnumerable<string> collectionNames, string dateTimeFieldName, DateTime start, DateTime end, out List<T> results)
        {
            results = new List<T>();
            List<T> resultsForCollection;

            foreach (string collectionName in collectionNames)
            {
                Find(collectionName, dateTimeFieldName, start, end, out resultsForCollection);
                results.AddRange(resultsForCollection);
                resultsForCollection.Clear();
            }
        }
        private void Find<T>(IEnumerable<string> collectionNames, string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end, out List<T> results)
        {
            results = new List<T>();
            List<T> resultsForCollection;

            foreach (string collectionName in collectionNames)
            {
                Find(collectionName, fieldName, regexPattern, dateTimeFieldName, start, end, out resultsForCollection);
                results.AddRange(resultsForCollection);
                resultsForCollection.Clear();
            }
        }
        private void Find<T>(IEnumerable<string> collectionNames, string fieldName, string regexPattern, out List<T> results)
        {
            results = new List<T>();
            List<T> resultsForCollection;

            foreach (string collectionName in collectionNames)
            {
                Find(collectionName, fieldName, regexPattern, out resultsForCollection);
                results.AddRange(resultsForCollection);
                resultsForCollection.Clear();
            }
        }
        #endregion Read Implementation methods

        #region Distinct Across Collection T
        public IEnumerable<T> Distinct<T>(string collectionName, string fieldName)
        {
            List<T> results;
            Distinct<T>(collectionName, fieldName, out results);
            return results;
        }
        public IEnumerable<T> Distinct<T>(string collectionName, string fieldName, IMongoQuery query)
        {
            List<T> results;
            Distinct<T>(collectionName, fieldName, query, out results);
            return results;
        }
        #endregion Distinct Across Collection T
        #region Distinct Across Multiple Collections T
        public IEnumerable<T> Distinct<T>(IEnumerable<string> collectionNames, string fieldName)
        {
            List<T> results;
            Distinct<T>(collectionNames, fieldName, out results);
            return results;
        }
        public IEnumerable<T> Distinct<T>(IEnumerable<string> collectionNames, string fieldName, IMongoQuery query)
        {
            List<T> results;
            Distinct<T>(collectionNames, fieldName, query, out results);
            return results;
        }
        #endregion Distinct Across Multiple Collections T
        #region    Distinct Implementation
        #region    T
        private void Distinct<T>(string collectionName, string fieldName, out List<T> results)
        {
            results = new List<T>();
            var collection = _databaseConnection.GetCollection<T>(collectionName);
            results.AddRange(collection.Distinct<T>(fieldName));
        }
        private void Distinct<T>(string collectionName, string fieldName, IMongoQuery query, out List<T> results)
        {
            results = new List<T>();
            var collection = _databaseConnection.GetCollection<T>(collectionName);
            results.AddRange(collection.Distinct<T>(fieldName, query));
        }
        private void Distinct<T>(IEnumerable<string> collectionNames, string fieldName, out List<T> results)
        {
            results = new List<T>();
            List<T> resultsForCollection;

            foreach (string collectionName in collectionNames)
            {
                Distinct<T>(collectionName, fieldName, out resultsForCollection);

                // since this method returns distinct values and we are searching across collections
                // we need to cull values returned from other collections
                foreach (T result in resultsForCollection)
                {
                    if (!results.Contains(result))
                        results.Add(result);
                }

                resultsForCollection.Clear();
            }
        }
        private void Distinct<T>(IEnumerable<string> collectionNames, string fieldName, IMongoQuery query, out List<T> results)
        {
            results = new List<T>();
            List<T> resultsForCollection;

            foreach (string collectionName in collectionNames)
            {
                Distinct<T>(collectionName, fieldName, query, out resultsForCollection);

                // since this method returns distinct values and we are searching across collections
                // we need to cull values returned from other collections
                foreach (T result in resultsForCollection)
                {
                    if (!results.Contains(result))
                        results.Add(result);
                }
                resultsForCollection.Clear();
            }
        }
        #endregion T
        #endregion Distinct Implementation
        #endregion Methods
    }

    public class Reader<T> : IReader<T>
    {
        private IReader _reader;

        public Reader(IReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<T> Read(string collectionName, string dateTimeFieldName, DateTime start, DateTime end)
        {
            return _reader.Read<T>(collectionName, dateTimeFieldName, start, end);
        }

        public IEnumerable<T> Read(string collectionName, string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end)
        {
            return _reader.Read<T>(collectionName, fieldName, regexPattern, dateTimeFieldName, start, end);
        }

        public IEnumerable<T> Read(string collectionName, string fieldName, string regexPattern)
        {
            return _reader.Read<T>(collectionName, fieldName, regexPattern);
        }

        public IEnumerable<T> Read(IEnumerable<string> collectionNames, string fieldName, DateTime start, DateTime end)
        {
            return _reader.Read<T>(collectionNames, fieldName, start, end);
        }

        public IEnumerable<T> Read(IEnumerable<string> collectionNames, string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end)
        {
            return _reader.Read<T>(collectionNames, fieldName, regexPattern, dateTimeFieldName, start, end);
        }

        public IEnumerable<T> Read(IEnumerable<string> collectionNames, string fieldName, string regexPattern)
        {
            return _reader.Read<T>(collectionNames, fieldName, regexPattern);
        }

        public IEnumerable<Y> Distinct<Y>(string collectionName, string fieldName)
        {
            return _reader.Distinct<Y>(collectionName, fieldName);
        }

        public IEnumerable<Y> Distinct<Y>(string collectionName, string fieldName, IMongoQuery query)
        {
            return _reader.Distinct<Y>(collectionName, fieldName, query);
        }

        public IEnumerable<Y> Distinct<Y>(IEnumerable<string> collectionNames, string fieldName)
        {
            return _reader.Distinct<Y>(collectionNames, fieldName);
        }

        public IEnumerable<Y> Distinct<Y>(IEnumerable<string> collectionNames, string fieldName, IMongoQuery query)
        {
            return _reader.Distinct<Y>(collectionNames, fieldName, query);
        }
    }
}