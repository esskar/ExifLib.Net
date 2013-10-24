using System;
using System.IO;
using System.Reflection;
using System.Resources;
using ExifLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExifLibUnitTests
{
    [TestClass]
    public class ExifReaderTests
    {
        [TestMethod]
        public void CanonIxusTest()
        {
            using (var reader = CreateFromResource("canon-ixus.jpg"))
            {
                AssertStringValue(reader, ExifTags.Make, "Canon");
            }
        }

        private static void AssertStringValue(ExifReader reader, ExifTags tag, string expected)
        {
            string actual;
            if (!reader.GetTagValue(tag, out actual))
            {
                Assert.Fail("Tag '{0}' not found.", tag);
            }
            Assert.AreEqual(expected, actual, "Tag '{0}' value mismatch.");
        }

        private static ExifReader CreateFromResource(string name)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ExifLibUnitTests.Data.Images." + name);
            if (stream == null)
                throw new MissingManifestResourceException("Resource with name '" + name + "' was not found.");

            return new ExifReader(stream);
        }
    }
}
