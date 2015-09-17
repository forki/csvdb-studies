﻿using System;
using NUnit.Framework;
using System.IO;
using SomeBasicCsvApp.Core;

namespace SomeBasicCsvApp.Tests
{
    [TestFixture]
    public class CsvFileTests
    {
        public class Data
        {
            public string C1
            {
                get;
                set;
            }
        }

        private const string baseData = @"C1
DATA";

        [Test]
        public void Read()
        {
            var data = baseData + @"
";
            using (var m = new MemoryStream())
            {
                var written = Streams.ToString(CsvFile.Write(m, CsvFile.Read<Data>(Streams.ToStream(data))));//write read data
                Assert.AreEqual(data, written);
            }
        }

        [Test]
        public void Append()
        {
            var data = baseData + @"
";
            var expected = baseData + @"
DATA2
";

            using (var m = Streams.ToStream(data))
            {
                var written = Streams.ToString(CsvFile.Append(m, new[]
                        {
                            new Data
                            {
                                C1 = "DATA2"
                            }
                        }));//append data
                Assert.AreEqual(expected, written);
            }
        }

        [Test]
        public void Append_when_missing_a_newline()
        {
            var data = baseData;
            var expected = baseData + @"
DATA2
";

            using (var m = Streams.ToStream(data))
            {
                var written = Streams.ToString(CsvFile.Append(m, new[]
                        {
                            new Data
                            {
                                C1 = "DATA2"
                            }
                        }));//append data
                Assert.AreEqual(expected, written);
            }
        }

    }
}

