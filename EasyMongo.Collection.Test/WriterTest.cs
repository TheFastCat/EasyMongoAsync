﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EasyMongo.Contract;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace EasyMongo.Collection.Test
{
    [TestFixture]
    public class WriterTest : TestBase
    {
        [Test]
        public void ConstructorTest()
        {
            IDatabaseWriter<TestEntry> databaseWriter = new Database.Writer<TestEntry>(MONGO_CONNECTION_STRING, MONGO_DATABASE_1_NAME);
            _writer = new Collection.Writer<TestEntry>(databaseWriter, MONGO_COLLECTION_1_NAME);
        }

        [Test]
        public void WriteAsyncTest()
        {
            string entryMessage = "This is a test";
            AddMongoEntryAsync(entryMessage, MONGO_COLLECTION_1_NAME);
            _results.AddRange(_reader.Read("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual(entryMessage, _results[0].Message);

            _results.Clear();

            string entryMessage2 = "This is a test as well";
            AddMongoEntryAsync(entryMessage2, MONGO_COLLECTION_1_NAME);
            _results.AddRange(_reader.Read("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual(entryMessage2, _results[1].Message);
        }

        [Test]
        public void WriteTest()
        {
            string entryMessage = "This is a test";
            AddMongoEntry(entryMessage, MONGO_COLLECTION_1_NAME);
            _results.AddRange(_reader.Read("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual(entryMessage, _results[0].Message);

            _results.Clear();

            string entryMessage2 = "This is a test as well";
            AddMongoEntry(entryMessage2, MONGO_COLLECTION_1_NAME);
            _results.AddRange(_reader.Read("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual(entryMessage2, _results[1].Message);
        }
      }
}