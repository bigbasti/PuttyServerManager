using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using PuttyServerManager.Tools;

namespace PuttyServerManager.Tests.Tools {
    /// <summary>
    /// Zusammenfassungsbeschreibung für FileToolsTests
    /// </summary>
    [TestClass]
    public class FileToolsTests {

        string testSessionFile = Path.Combine("C:\\temp", "testSessionFile");


        public FileToolsTests() {
            //
            // TODO: Konstruktorlogik hier hinzufügen
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
         //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
         [TestInitialize()]
         public void MyTestInitialize() {
             
             if (File.Exists(testSessionFile)) {
                 File.Delete(testSessionFile);
             }
             File.WriteAllText(testSessionFile, "UserName=\r\nHostName=\r\nRemoteCommand=\r\nColour0=255,255,255\r\nColour2=0,0,0");
         }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
         [TestCleanup()]
         public void MyTestCleanup() {
             if (File.Exists(testSessionFile)) {
                 File.Delete(testSessionFile);
             }
         }
        //
        #endregion

        [TestMethod]
        public void SetCustomColor() {
            FileTools.SetCustomColors("Colour2=111,111,111", "Colour0=222,222,222", testSessionFile);

            string changedFile = File.ReadAllText(testSessionFile);

            Assert.IsTrue(changedFile.Contains("Colour2=111,111,111"));
        }
    }
}
