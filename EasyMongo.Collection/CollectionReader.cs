﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using EasyMongo.Contract;
using EasyMongo.Collection;
using EasyMongo.Database;

namespace EasyMongo.Collection
{
    public class CollectionReader: ICollectionReader
    {
        public event ReadCompletedEvent     AsyncReadCompleted;
        public event DistinctBSONCompletedEvent AsyncDistinctBSONCompleted;
        public event DistinctCompletedEvent AsyncDistinctCompleted;

        private string _collectionName;
        private IDatabaseReader _mongoDBReader;

        public CollectionReader(IDatabaseReader mongoDatabaseReader, string collectionName)
        {
            _mongoDBReader  = mongoDatabaseReader;
            _collectionName = collectionName;

            _mongoDBReader.AsyncReadCompleted         += new ReadCompletedEvent(_mongoDBReader_AsyncReadCompleted);
            _mongoDBReader.AsyncDistinctBSONCompleted += new DistinctBSONCompletedEvent(_mongoDBReader_AsyncDistinctBSONCompleted);
            _mongoDBReader.AsyncDistinctCompleted     += new DistinctCompletedEvent(_mongoDBReader_AsyncDistinctCompleted);
        }

        #region   Synchronous
        public IEnumerable<T> Read<T>(string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end)
        {
            return _mongoDBReader.Read<T>(_collectionName, fieldName, regexPattern, dateTimeFieldName, start, end);
        }

        public IEnumerable<T> Read<T>(string dateTimeFieldName, DateTime start, DateTime end)
        {
            return _mongoDBReader.Read<T>(_collectionName, dateTimeFieldName, start, end);
        }

        public IEnumerable<T> Read<T>(string fieldName, string regexPattern)
        {
            return _mongoDBReader.Read<T>(_collectionName, fieldName, regexPattern);
        }
        #region Distinct BSON
        public IEnumerable<BsonValue> Distinct(string fieldName)
        {
            return _mongoDBReader.Distinct(_collectionName, fieldName);
        }
        public IEnumerable<BsonValue> Distinct(string fieldName, IMongoQuery query)
        {
            return _mongoDBReader.Distinct(_collectionName, fieldName, query);
        }
        #endregion Distinct BSON
        #region Distinct T
        public IEnumerable<T> Distinct<T>(string fieldName)
        {
            return _mongoDBReader.Distinct<T>(_collectionName, fieldName);
        }
        public IEnumerable<T> Distinct<T>(string fieldName, IMongoQuery query)
        {
            return _mongoDBReader.Distinct<T>(_collectionName, fieldName, query);
        }
        #endregion Distinct T
        #endregion Synchronous

        #region    Asynchronous
        public void ReadAsync<T>(string fieldName, string regexPattern, string dateTimeFieldName, DateTime start, DateTime end)
        {
            _mongoDBReader.ReadAsync<T>(_collectionName, fieldName, regexPattern, dateTimeFieldName, start, end);
        }

        public void ReadAsync<T>(string dateTimeFieldName, DateTime start, DateTime end)
        {
            _mongoDBReader.ReadAsync<T>(_collectionName, dateTimeFieldName, start, end);
        }

        public void ReadAsync<T>(string fieldName, string regexPattern)
        {
            _mongoDBReader.ReadAsync<T>(_collectionName, fieldName, regexPattern);
        }
        #region Distinct BSON
        public void DistinctAsync(string fieldName)
        {
            _mongoDBReader.DistinctAsync(_collectionName, fieldName);
        }
        public void DistinctAsync(string fieldName, IMongoQuery query)
        {
            _mongoDBReader.DistinctAsync(_collectionName, fieldName, query);
        }
        #endregion Distinct BSON
        #region Distinct T
        public void DistinctAsync<T>(string fieldName)
        {
            _mongoDBReader.DistinctAsync<T>(_collectionName, fieldName);
        }
        public void DistinctAsync<T>(string fieldName, IMongoQuery query)
        {
            _mongoDBReader.DistinctAsync<T>(_collectionName, fieldName, query);
        }
        #endregion Distinct T
        #endregion Asynchronous


        void _mongoDBReader_AsyncReadCompleted(object e, Exception ex)
        {
            if (AsyncReadCompleted != null)
                AsyncReadCompleted(e, ex);
        }

        void _mongoDBReader_AsyncDistinctBSONCompleted(IEnumerable<BsonValue> e, Exception ex)
        {
            if (AsyncDistinctBSONCompleted != null)
                AsyncDistinctBSONCompleted(e, ex);
        }

        void _mongoDBReader_AsyncDistinctCompleted(object e, Exception ex)
        {
            if (AsyncDistinctCompleted != null)
                AsyncDistinctCompleted(e, ex);
        }
    }
}