using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Tests
{
    [TestClass()]
    public class NeuralLayerTests
    {
        private NeuralNode _layer1Node1;
        private NeuralNode _layer1Node2;
        private NeuralNode _layer2Node1;
        private NeuralNode _layer2Node2;
        private NeuralLayer _layer1;
        private NeuralLayer _layer2;
        private PrivateObject _layer1PrrivateObject;

        //初始化神經元
        [TestInitialize()]
        public void Initialize()
        {
            _layer1Node1 = new NeuralNode();
            _layer1Node1.LoadWeights(new List<double>() { 0.1, 0.2});
            _layer1Node1.Output = 0.5;
            _layer1Node1.AddTotalDeltaWeight(0.5, 0);

            _layer1Node2 = new NeuralNode();
            _layer1Node2.LoadWeights(new List<double>() { 0.3, 0.4 });
            _layer1Node2.Output = 0.4;
            _layer1Node2.AddTotalDeltaWeight(0.4, 0);

            _layer1 = new NeuralLayer(0, 0.1, 0.8);
            _layer1.AddNode(_layer1Node1);
            _layer1.AddNode(_layer1Node2);

            _layer2Node1 = new NeuralNode();
            _layer2Node1.LoadWeights(new List<double>() { 0.3, 0.4 });
            _layer2Node1.Output = 0.4;
            _layer2Node1.AddTotalDeltaWeight(0.4, 0);

            _layer2Node2 = new NeuralNode();
            _layer2Node2.LoadWeights(new List<double>() { 0.3, 0.4 });
            _layer2Node2.Output = 0.4;
            _layer2Node2.AddTotalDeltaWeight(0.4, 0);

            _layer2 = new NeuralLayer(0, 0.1, 0.8);
            _layer2.AddNode(_layer2Node1);
            _layer2.AddNode(_layer2Node2);

            _layer1PrrivateObject = new PrivateObject(_layer1);
        }

        //綜合測試
        [TestMethod()]
        public void NeuralLayerTest()
        {

        }

        //測試用數量初始化節點
        [TestMethod()]
        public void InitializeNodesTest()
        {
            Assert.IsTrue(_layer1.NodeAmount == 2);
            _layer1.InitializeNodes(5);
            Assert.IsTrue(_layer1.NodeAmount == 5);
        }

        //測試設定此層nodes的weight數量
        [TestMethod()]
        public void InitWeightsTest()
        {
            List<NeuralNode> nodes = (List<NeuralNode>)_layer1PrrivateObject.GetFieldOrProperty("_nodes");
            foreach (NeuralNode node in nodes)
                Assert.IsTrue(node.OutputWeight.Count() == 2);
            _layer1.InitWeights(5);
            foreach (NeuralNode node in nodes)
                Assert.IsTrue(node.OutputWeight.Count() == 5);
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CalculateFeedForwardTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CalculateFeedForwardTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetOutputTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LogisticBackpropagationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LogisticBackpropagationTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LogisticBackpropagationSetWeightTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWeightsTest()
        {
            Assert.Fail();
        }
    }
}