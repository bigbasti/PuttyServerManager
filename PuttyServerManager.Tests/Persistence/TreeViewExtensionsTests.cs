using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

using PuttyServerManager.Tools.Extensions;
using System.IO;

namespace PuttyServerManager.Tests.Persistence {
    /// <summary>
    /// Zusammenfassungsbeschreibung für SessionSerializerTests
    /// </summary>
    [TestClass]
    public class TreeViewExtensionsTests {

        string testXmlFile = Path.Combine("C:\\temp", "text.xml");
        
        public TreeViewExtensionsTests() {
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
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        [TestCleanup()]
        public void MyTestCleanup() {

            if (File.Exists(testXmlFile)) {
                File.Delete(testXmlFile);
            }
        }
        
        #endregion

        [TestMethod]
        public void SirializeOneLevelNode() {
            TreeView sut = new TreeView();

            TreeNode node1 = new TreeNode("TestNode");

            sut.Nodes.Add(node1);

            sut.SerializeNode(sut.Nodes[0], testXmlFile);

            sut = null;
            sut = new TreeView();
            TreeNode readNode = sut.DeserializeNode(testXmlFile);
            sut.Nodes.Add(readNode);

            Assert.AreEqual(1, sut.Nodes.Count);
            Assert.AreEqual("TestNode", sut.Nodes[0].Text);
        }

        [TestMethod]
        public void SirializeXLevelLevelNode() {
            TreeView sut = new TreeView();

            TreeNode node1 = new TreeNode("TestNode");
            TreeNode node2 = new TreeNode("TestNode1");
            TreeNode node3 = new TreeNode("TestNode2");
            TreeNode node4 = new TreeNode("TestNode3");

            node1.Nodes.Add(node2);
            node2.Nodes.Add(node3);
            node2.Nodes.Add(node4);

            sut.Nodes.Add(node1);

            sut.SerializeNode(sut.Nodes[0], testXmlFile);

            sut = null;
            sut = new TreeView();
            TreeNode readNode = sut.DeserializeNode(testXmlFile);
            sut.Nodes.Add(readNode);

            Assert.AreEqual(1, sut.Nodes.Count);
            Assert.AreEqual("TestNode", sut.Nodes[0].Text);

            Assert.AreEqual(2, sut.Nodes[0].Nodes[0].Nodes.Count);
            Assert.AreEqual("TestNode2", sut.Nodes[0].Nodes[0].Nodes[0].Text);
        }

        [TestMethod]
        public void ExistingNodeCanBeFoundOverSeveralLevels() {
            TreeView sut = new TreeView();

            TreeNode node1 = new TreeNode("TestNode");
            TreeNode node2 = new TreeNode("TestNode1");
            TreeNode node3 = new TreeNode("TestNode2");
            TreeNode node4 = new TreeNode("TestNode3");

            node1.Nodes.Add(node2);
            node2.Nodes.Add(node3);
            node2.Nodes.Add(node4);

            sut.Nodes.Add(node1);

            Assert.IsTrue(sut.DoesNodeExist("TestNode2"));
            Assert.IsFalse(sut.DoesNodeExist("TestNode4"));
        }
    }
}
