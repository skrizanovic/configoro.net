using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configoro.Net.Processor;
using Moq;
using Configoro.Net.Domain.Interface;
using Configoro.Net.Domain;
using Configoro.Net.Processor.Helper;
using Configoro.Net.UnitTests.Helper;
using System.Xml;

namespace Configoro.Net.UnitTests
{
    [TestClass]
    public class IntragrationTest
    {
        [TestMethod]
        public void TestEditConfigValue()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWithEditKey(1, "TestMode"));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState");

            Assert.AreEqual(1, nodes.Count);
            var xml = nodes.Cast<XmlElement>().First().Attributes["mode"].Value;

            Assert.AreEqual("TestMode", xml);


        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, could not find xpath /configuration/sessionState")]
        public void TestEditConfigValueNodeNotPresent()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWhichDoesNotExist(1));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

        }

        [TestMethod]
        public void TestEditConfigNode()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWithEditKey(2, "<test>blah</test>"));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState");

            Assert.AreEqual(1, nodes.Count);
            var xml = nodes.Cast<XmlElement>().First().InnerXml;

            Assert.AreEqual("<test>blah</test>", xml);


        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, could not find xpath /configuration/sessionState")]
        public void TestEditConfigNodeNodeNotPresent()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWhichDoesNotExist(2));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState");

            Assert.AreEqual(1, nodes.Count);
            var xml = nodes.Cast<XmlElement>().First().InnerXml;

            Assert.AreEqual("<test>blah</test>", xml);


        }

        [TestMethod]
        public void TestAddConfigNode()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWithEditKey(3, "<test>blah</test>",""));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState/test");

            Assert.AreEqual(1, nodes.Count);
            var xml = nodes.Cast<XmlElement>().First().InnerXml;

            Assert.AreEqual("blah", xml);


        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, could not find xpath /configuration/sessionState")]
        public void TestAddConfigNodeNodeNotPresent()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWhichDoesNotExist(3));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState");

            Assert.AreEqual(1, nodes.Count);
            var xml = nodes.Cast<XmlElement>().First().InnerXml;

            Assert.AreEqual("<test>blah</test>", xml);


        }

        
        [TestMethod]
        public void TestDeleteConfigNode()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWithEditKey(4, "doesnt matter"));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState");

            Assert.AreEqual(1, nodes.Count);
            var xml = nodes.Cast<XmlElement>().First().Attributes["mode"];

            Assert.AreEqual(null, xml);


        }
        [TestMethod]
        public void TestDeleteConfigNodeNodeNotPresent()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWhichDoesNotExist(4));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            //no exception means pass
            Assert.AreEqual(1, 1);

        }
        [TestMethod]
        public void TestDeleteConfigValue()
        {
            var mockDataAccess = new Mock<IConfigService>();
            mockDataAccess.Setup(p => p.GetSettings("PROD", "TEST")).Returns(GetTestConfigValuesWithEditKey(4, "doesnt matter",""));
            var flr = new FileLoaderWithoutSave();
            Executor exe = new Executor(mockDataAccess.Object, flr);

            exe.ConvertConfigFile("PROD", "TEST", "..\\..\\..\\UnitTest\\Configoro.Net.UnitTests\\resources\\web.config");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(flr.Content);

            var nodes = doc.SelectNodes("/configuration/system.web/sessionState");

            Assert.AreEqual(0, nodes.Count);
           

        }
        #region Config Methods
        public List<ConfigView> GetTestConfigValuesWithEditKey(int processorType, string value)
        {
            return GetTestConfigValuesWithEditKey(processorType, value, "mode");
        }
        public List<ConfigView> GetTestConfigValuesWithEditKey(int processorType, string value, string property)
        {
            var itm = new List<ConfigView>();

            itm.Add(new ConfigView()
            {
                Environment = "PROD",
                ProcessorTypeId = processorType,
                Property = property,
                xPath = "/configuration/system.web/sessionState",
                value = value
            });


            return itm;
        }
        public List<ConfigView> GetTestConfigValuesWhichDoesNotExist(int processorType)
        {
            var itm = new List<ConfigView>();

            itm.Add(new ConfigView()
            {
                Environment = "PROD",
                ProcessorTypeId = processorType,
                Property = "mode",
                xPath = "/configuration/sessionState",
                value = "TestMode"
            });


            return itm;
        }
        #endregion
    }
}
