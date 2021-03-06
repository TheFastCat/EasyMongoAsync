﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using EasyMongo.Test.Base;

namespace EasyMongo.Database.Test
{
    [TestFixture]
    public class DatabaseReaderTest : IntegrationTestFixture
    {
        #region    Read 
        [Test]
        public void ReadTest1()
        {
            AddMongoEntry();

            _results.AddRange(_databaseReader.Read<Entry>(MONGO_COLLECTION_1_NAME, "Message", "Hello", "TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
        }

        [Test]
        public void ReadTest2()
        {
            AddMongoEntry();

            _results.AddRange(_databaseReader.Read<Entry>(MONGO_COLLECTION_1_NAME, "Message", "Hello"));
            Assert.AreEqual(1, _results.Count());
        }

        [Test]
        public void ReadTest3()
        {
            AddMongoEntry();

            _results.AddRange(_databaseReader.Read<Entry>(MONGO_COLLECTION_1_NAME, "TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
        }

        [Test]
        public void ReadTest4()
        {
            AddMongoEntry();

            _results.AddRange(_databaseReader.Read<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", "Hello", "TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
        }

        [Test]
        public void ReadTest5()
        {
            AddMongoEntry();

            _results.AddRange(_databaseReader.Read<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", "Hello"));
            Assert.AreEqual(1, _results.Count());
        }

        [Test]
        public void ReadTest6()
        {
            AddMongoEntry();

            _results.AddRange(_databaseReader.Read<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
        }

        [Test]
        public void ReadTest7()
        {
            AddMongoEntry("Hello World 1", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Hello World 2", MONGO_COLLECTION_2_NAME);

            _results.AddRange(_databaseReader.Read<Entry>(new string[] { MONGO_COLLECTION_1_NAME, MONGO_COLLECTION_2_NAME }));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
            Assert.AreEqual("Hello World 2", _results[1].Message);
        }

        [Test]
        public void ReadTest8()
        {
            AddMongoEntry("Hello World 1", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Hello World 2", MONGO_COLLECTION_1_NAME);

            _results.AddRange(_databaseReader.Read<Entry>(MONGO_COLLECTION_1_NAME));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual("Hello World 1", _results.ElementAt(0).Message);
            Assert.AreEqual("Hello World 2", _results.ElementAt(1).Message);
        }

        #region    Async
        [Test]
        public async void ReadAsyncTest1()
        {
            string entryMessage = "Hello World";
            AddMongoEntryAsyncDelegate(message: entryMessage);
            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(MONGO_COLLECTION_1_NAME, "Message", entryMessage, "TimeStamp", _beforeTest, DateTime.Now);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(entryMessage, results.ElementAt(0).Message);
        }

        [Test]
        public async void ReadAsyncTest2()
        {
            string entryMessage = "Hello World";
            AddMongoEntryAsyncDelegate(message: entryMessage);
            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(MONGO_COLLECTION_1_NAME, "Message", entryMessage);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(entryMessage, results.ElementAt(0).Message);
        }

        [Test]
        public async void ReadAsyncTest3()
        {
            string entryMessage = "Hello World";
            AddMongoEntry(entryMessage, MONGO_COLLECTION_1_NAME);
            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", entryMessage);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(entryMessage, results.ElementAt(0).Message);

            _asyncReadResults.Clear();

            string entryMessage2 = "Hello World Again";
            AddMongoEntry(entryMessage2, MONGO_COLLECTION_2_NAME);
            results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", entryMessage);

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(entryMessage2, results.ElementAt(1).Message);
        }

        [Test]
        public async void ReadAsyncTest4()
        {
            string entryMessage = "Hello World";
            AddMongoEntry(entryMessage, MONGO_COLLECTION_1_NAME);
            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", entryMessage, "TimeStamp", _beforeTest, DateTime.Now);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(entryMessage, results.ElementAt(0).Message);

            _asyncReadResults.Clear();

            string entryMessage2 = "Hello World Again";
            AddMongoEntry(entryMessage2, MONGO_COLLECTION_2_NAME);
            results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", entryMessage, "TimeStamp", _beforeTest, DateTime.Now);

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(entryMessage2, results.ElementAt(1).Message);
        }

        [Test]
        public async void ReadAsyncTest5()
        {
            string entryMessage = "Hello World";
            AddMongoEntry(entryMessage, MONGO_COLLECTION_1_NAME);
            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", entryMessage);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(entryMessage, results.ElementAt(0).Message);

            _asyncReadResults.Clear();

            string entryMessage2 = "Hello World Again";
            AddMongoEntry(entryMessage2, MONGO_COLLECTION_2_NAME);
            results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "Message", entryMessage);

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(entryMessage2, results.ElementAt(1).Message);
        }

        [Test]
        public async void ReadAsyncTest6()
        {
            string entryMessage = "Hello World";
            AddMongoEntry(entryMessage, MONGO_COLLECTION_1_NAME);
            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "TimeStamp", _beforeTest, DateTime.Now);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(entryMessage, results.ElementAt(0).Message);

            string entryMessage2 = "Hello World Again";
            AddMongoEntry(entryMessage2, MONGO_COLLECTION_2_NAME);
            results = await _databaseReader.ReadAsync<Entry>(_mongoDatabaseConnection.GetCollectionNames(), "TimeStamp", _beforeTest, DateTime.Now);

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(entryMessage2, results.ElementAt(1).Message);
        }

        [Test]
        public async void ReadAsyncTest7()
        {
            AddMongoEntry("Hello World 1", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Hello World 2", MONGO_COLLECTION_2_NAME);

            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(new string[] { MONGO_COLLECTION_1_NAME, MONGO_COLLECTION_2_NAME });
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual("Hello World 1", results.ElementAt(0).Message);
            Assert.AreEqual("Hello World 2", results.ElementAt(1).Message);
        }

        [Test]
        public async void ReadAsyncTest8()
        {
            AddMongoEntry("Hello World 1", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Hello World 2", MONGO_COLLECTION_1_NAME);

            IEnumerable<Entry> results = await _databaseReader.ReadAsync<Entry>(MONGO_COLLECTION_1_NAME);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual("Hello World 1", results.ElementAt(0).Message);
            Assert.AreEqual("Hello World 2", results.ElementAt(1).Message);
        }
        #endregion Async
        #endregion Read

        #region    Distinct
        [Test]
        public void DistinctTest1()
        {
            AddMongoEntry("One");
            AddMongoEntry("One");
            AddMongoEntry("Two");
            AddMongoEntry("Three");

            List<string> list = new List<string>(_databaseReader.Distinct<string>(MONGO_COLLECTION_1_NAME, "Message"));

            Assert.AreEqual(3, list.Count());
            Assert.AreEqual("One", list[0]);
            Assert.AreEqual("Two", list[1]);
            Assert.AreEqual("Three", list[2]);
        }

        [Test]
        public void DistinctTest2()
        {
            // get distinct message values that are not "Two" or "Three"
            var searchQuery = Query.And(Query.NE("Message", "Two"), Query.NE("Message", "Three"));

            AddMongoEntry("One");
            AddMongoEntry("One");
            AddMongoEntry("Two");
            AddMongoEntry("Three");

            List<string> list = new List<string>(_databaseReader.Distinct<string>(MONGO_COLLECTION_1_NAME, "Message", searchQuery));

            Assert.AreEqual(1, list.Count());
            Assert.AreEqual("One", list[0]);
        }

        [Test]
        public void DistinctTest3()
        {
            AddMongoEntry("One", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("One", MONGO_COLLECTION_2_NAME);
            AddMongoEntry("Two", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Three", MONGO_COLLECTION_2_NAME);

            List<string> collections = new List<string>() { MONGO_COLLECTION_1_NAME, MONGO_COLLECTION_2_NAME };
            List<string> list = new List<string>(_databaseReader.Distinct<string>(collections, "Message"));

            Assert.AreEqual(3, list.Count());
            Assert.AreEqual("One", list[0]);
            Assert.AreEqual("Two", list[1]);
            Assert.AreEqual("Three", list[2]);
        }

        [Test]
        public void DistinctTest4()
        {
            // get distinct message values that are not "Two" or "Three"
            var searchQuery = Query.And(Query.NE("Message", "Two"), Query.NE("Message", "Three"));

            AddMongoEntry("One", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("One", MONGO_COLLECTION_2_NAME);
            AddMongoEntry("Two", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Three", MONGO_COLLECTION_2_NAME);

            List<string> collections = new List<string>() { MONGO_COLLECTION_1_NAME, MONGO_COLLECTION_2_NAME };
            List<string> list = new List<string>(_databaseReader.Distinct<string>(collections, "Message", searchQuery));

            Assert.AreEqual(1, list.Count());
            Assert.AreEqual("One", list[0]);
        }

        #region    Async
        [Test]
        public async void DistinctAsyncTest1()
        {
            AddMongoEntry("One");
            AddMongoEntry("One");
            AddMongoEntry("Two");
            AddMongoEntry("Three");

            IEnumerable<string> results = await _databaseReader.DistinctAsync<string>(MONGO_COLLECTION_1_NAME, "Message");

            Assert.AreEqual(3, results.Count());
            Assert.AreEqual("One", results.ElementAt(0));
            Assert.AreEqual("Two", results.ElementAt(1));
            Assert.AreEqual("Three", results.ElementAt(2));
        }

        [Test]
        public async void DistinctAsyncTest2()
        {
            //System.Diagnostics.Debugger.Launch();
            // get distinct message values that are not "Two" or "Three"
            var searchQuery = Query.And(Query.NE("Message", "Two"), Query.NE("Message", "Three"));

            AddMongoEntry("One");
            AddMongoEntry("One");
            AddMongoEntry("Two");
            AddMongoEntry("Three");

            IEnumerable<string> results = await _databaseReader.DistinctAsync<string>(MONGO_COLLECTION_1_NAME, "Message", searchQuery);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("One", results.ElementAt(0));
        }

        [Test]
        public async void DistinctAsyncTest3()
        {
            AddMongoEntry("One", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("One", MONGO_COLLECTION_2_NAME);
            AddMongoEntry("Two", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Three", MONGO_COLLECTION_2_NAME);

            List<string> collections = new List<string>() { MONGO_COLLECTION_1_NAME, MONGO_COLLECTION_2_NAME };

            IEnumerable<string> results = await _databaseReader.DistinctAsync<string>(collections, "Message");

            Assert.AreEqual(3, results.Count());
            Assert.AreEqual("One", results.ElementAt(0));
            Assert.AreEqual("Two", results.ElementAt(1));
            Assert.AreEqual("Three", results.ElementAt(2));
        }

        [Test]
        public async void DistinctAsyncTest4()
        {
            // get distinct message values that are not "Two" or "Three"
            var searchQuery = Query.And(Query.NE("Message", "Two"), Query.NE("Message", "Three"));

            AddMongoEntry("One", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("One", MONGO_COLLECTION_2_NAME);
            AddMongoEntry("Two", MONGO_COLLECTION_1_NAME);
            AddMongoEntry("Three", MONGO_COLLECTION_2_NAME);

            List<string> collections = new List<string>() { MONGO_COLLECTION_1_NAME, MONGO_COLLECTION_2_NAME };

            IEnumerable<string> results = await _databaseReader.DistinctAsync<string>(collections, "Message", searchQuery);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("One", results.ElementAt(0));
        }
        #endregion Async
        #endregion Distinct

        #region    Execute
        [Test]
        public void ExecuteTest1()
        {
            AddMongoEntry("Hello World 1");

            IMongoQuery query = Query.Matches("Message", new BsonRegularExpression("WORLD", "i"));

            _results.AddRange(_databaseReader.Execute<Entry>(MONGO_COLLECTION_1_NAME, query));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
        }

        [Test]
        public void ExecuteAndsTest1()
        {
            AddMongoEntry("Hello World 1");
            AddMongoEntry("Hello World 2");

            IList<IMongoQuery> queries = new List<IMongoQuery>();

            queries.Add(Query.Matches("Message", new BsonRegularExpression("WORLD", "i")));
            queries.Add(Query.Matches("Message", new BsonRegularExpression("1")));

            _results.AddRange(_databaseReader.ExecuteAnds<Entry>(MONGO_COLLECTION_1_NAME, queries));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
        }

        [Test]
        public void ExecuteOrsTest1()
        {
            AddMongoEntry("Hello World 1");
            AddMongoEntry("Goodbye Yellow Brick Road");

            IList<IMongoQuery> queries = new List<IMongoQuery>();

            queries.Add(Query.Matches("Message", new BsonRegularExpression("WORLD", "i")));
            queries.Add(Query.Matches("Message", new BsonRegularExpression("Road")));

            _results.AddRange(_databaseReader.ExecuteOrs<Entry>(MONGO_COLLECTION_1_NAME, queries));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
            Assert.AreEqual("Goodbye Yellow Brick Road", _results[1].Message);
        }

        #region    Async
        [Test]
        public async void ExecuteAsyncTest1()
        {
            AddMongoEntry("Hello World 1");

            IMongoQuery query = Query.Matches("Message", new BsonRegularExpression("WORLD", "i"));

            _results.AddRange(await _databaseReader.ExecuteAsync<Entry>(MONGO_COLLECTION_1_NAME, query));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
        }

        [Test]
        public async void ExecuteAndsAsyncTest1()
        {
            AddMongoEntry("Hello World 1");
            AddMongoEntry("Hello World 2");

            IList<IMongoQuery> queries = new List<IMongoQuery>();

            queries.Add(Query.Matches("Message", new BsonRegularExpression("WORLD", "i")));
            queries.Add(Query.Matches("Message", new BsonRegularExpression("1")));

            _results.AddRange(await _databaseReader.ExecuteAndsAsync<Entry>(MONGO_COLLECTION_1_NAME, queries));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
        }

        [Test]
        public async void ExecuteOrsAsyncTest1()
        {
            AddMongoEntry("Hello World 1");
            AddMongoEntry("Goodbye Yellow Brick Road");

            IList<IMongoQuery> queries = new List<IMongoQuery>();

            queries.Add(Query.Matches("Message", new BsonRegularExpression("WORLD", "i")));
            queries.Add(Query.Matches("Message", new BsonRegularExpression("Road")));

            _results.AddRange(await _databaseReader.ExecuteOrsAsync<Entry>(MONGO_COLLECTION_1_NAME, queries));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual("Hello World 1", _results[0].Message);
            Assert.AreEqual("Goodbye Yellow Brick Road", _results[1].Message);
        }
        #endregion Async
        #endregion Execute
    }
}
