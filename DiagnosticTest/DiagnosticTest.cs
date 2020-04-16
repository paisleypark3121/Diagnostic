using System;
using System.Collections.Generic;
using Diagnostic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiagnosticTest
{
    [TestClass]
    public class DiagnosticTest
    {
        [TestMethod]
        public void Dictionary2jsonTest()
        {
            #region arrange
            Dictionary<string, string> _internal_parameters = new Dictionary<string, string>();
            _internal_parameters.Add("x", "1");
            _internal_parameters.Add("y", "2");
            string expected = "{\"x\":\"1\",\"y\":\"2\"}";
            #endregion

            #region act
            string actual = JsonConvert.SerializeObject(_internal_parameters);
            #endregion

            #region assert
            Assert.AreEqual(expected, actual);
            #endregion
        }

        [TestMethod]
        public void AppInsightsJsonMessageTest()
        {
            /*
             * 
             * ApplicationInsights query for custom dimensions:
             * traces 
             * | where customDimensions.['MyPro1']=="prop1_value"
             * 
             */
            #region arrange
            JObject _jobject = new JObject();
            string header = "This is my header message";
            _jobject.Add(new JProperty("header", header));
            _jobject.Add(new JProperty("MyProp1", "prop1_value"));
            _jobject.Add(new JProperty("MyProp2", "prop2_value"));
            _jobject.Add(new JProperty("MyProp3", "prop3_value"));
            _jobject.Add(new JProperty("requestTime", DateTime.Now.ToString("yyyyMMdd HH:mm:ss")));
            string message=_jobject.ToString(Formatting.None);
            ApplicationInsightsTrace trace = new ApplicationInsightsTrace();
            #endregion

            #region act
            trace.trace(message);
            #endregion

            #region assert
            Assert.IsTrue(true);
            #endregion

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                System.Threading.Thread.Sleep(500);
            }
            // ADD LOOP PAUSE ETC TO LET THE TRACKING BE COMPLETE
        }

        [TestMethod]
        public void AppInsightSimpleMessageTest()
        {
            #region arrange
            ITrace trace = new ApplicationInsightsTrace();
            string message = "ciao";
            #endregion

            #region act
            trace.trace(message);
            #endregion

            #region assert
            Assert.IsTrue(true);
            #endregion

            for (int i=0;i<100;i++)
            {
                Console.WriteLine(i);
                System.Threading.Thread.Sleep(500);
            }
            // ADD LOOP PAUSE ETC TO LET THE TRACKING BE COMPLETE
        }

        [TestMethod]
        public void SimpleTelemetryTest()
        {
            #region arrange
            Dictionary<string, string> expected_dictionary = new Dictionary<string, string>();
            expected_dictionary.Add("x", "1");
            expected_dictionary.Add("y", "2");
            string internal_parameters = JsonConvert.SerializeObject(expected_dictionary);
            string diagnostic = "internal_id: 1234 - responseTime: " + DateTime.Now.ToString("HH:mm:ss.fff") + " - internal_parameters: " + internal_parameters;
            string label_internal_parameters = "internal_parameters: ";
            int index = diagnostic.IndexOf(label_internal_parameters) + label_internal_parameters.Length;
            #endregion

            #region act
            string actual_internal_parameters = diagnostic.Substring(index, diagnostic.Length - index);
            Dictionary<string, string> actual_dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(actual_internal_parameters);
            #endregion

            #region assert
            Assert.AreEqual(expected_dictionary.Count, actual_dictionary.Count);
            foreach (var entry in actual_dictionary)
                Assert.AreEqual(expected_dictionary[entry.Key], entry.Value);
            #endregion
        }
    }
}
