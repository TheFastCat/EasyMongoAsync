﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EasyMongo.Contract;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using EasyMongo.Test.Base;

namespace EasyMongo.Collection.Test
{
    [TestFixture]
    public class CollectionWriterTest : IntegrationTestFixture
    {
        [Test]
        public void ConstructorTest()
        {
            _collectionWriter = new Collection.CollectionWriter(_databaseWriter, MONGO_COLLECTION_1_NAME);
        }

        [Test]
        public async void WriteAsyncTest()
        {
            string entryMessage = "This is a test";
            await AddMongoEntryCollectionAsync(entryMessage, MONGO_COLLECTION_1_NAME);

            _results.AddRange(_collectionReader.Read<Entry>("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual(entryMessage, _results[0].Message);

            _results.Clear();

            string entryMessage2 = "This is a test as well";
            await AddMongoEntryCollectionAsync(entryMessage2, MONGO_COLLECTION_1_NAME);

            _results.AddRange(_collectionReader.Read<Entry>("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual(entryMessage2, _results[1].Message);
        }

        [Test]
        public void WriteTest()
        {
            string entryMessage = "This is a test";
            AddMongoEntryCollection(entryMessage, MONGO_COLLECTION_1_NAME);
            _results.AddRange(_collectionReader.Read<Entry>("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(1, _results.Count());
            Assert.AreEqual(entryMessage, _results[0].Message);

            _results.Clear();

            string entryMessage2 = "This is a test as well";
            AddMongoEntryCollection(entryMessage2, MONGO_COLLECTION_1_NAME);
            _results.AddRange(_collectionReader.Read<Entry>("TimeStamp", _beforeTest, DateTime.Now));
            Assert.AreEqual(2, _results.Count());
            Assert.AreEqual(entryMessage2, _results[1].Message);
        }
      }
}
