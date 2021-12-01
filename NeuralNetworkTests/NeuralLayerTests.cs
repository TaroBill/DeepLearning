using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork;
using NeuralNetwork.ActivationFunction;
using NeuralNetwork.LossFunction;
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
            _layer1Node1 = new NeuralNode(new Sigmoid());
            _layer1Node1.LoadWeights(new List<double>() { 0.1, 0.2});
            _layer1Node1.Output = 0.5;
            _layer1Node1.AddTotalDeltaWeight(0.5, 0);

            _layer1Node2 = new NeuralNode(new Sigmoid());
            _layer1Node2.LoadWeights(new List<double>() { 0.3, 0.4 });
            _layer1Node2.Output = 0.4;
            _layer1Node2.AddTotalDeltaWeight(0.4, 0);

            _layer1 = new NeuralLayer(0, 0.1, 0.8, new Sigmoid());
            _layer1.AddNode(_layer1Node1);
            _layer1.AddNode(_layer1Node2);
            _layer1.InitBiasWeight(new List<double>() { 0.5, 0.5});

            _layer2Node1 = new NeuralNode(new Sigmoid());
            _layer2Node1.LoadWeights(new List<double>() { 0.3, 0.4 });
            _layer2Node1.Output = 0.4;
            _layer2Node1.AddTotalDeltaWeight(0.5, 0);

            _layer2Node2 = new NeuralNode(new Sigmoid());
            _layer2Node2.LoadWeights(new List<double>() { 0.3, 0.4 });
            _layer2Node2.Output = 0.4;
            _layer2Node2.AddTotalDeltaWeight(0.4, 0);

            _layer2 = new NeuralLayer(0, 0.1, 0.8, new Sigmoid());
            _layer2.AddNode(_layer2Node1);
            _layer2.AddNode(_layer2Node2);
            _layer2.InitBiasWeight(new List<double>() { 0.5, 0.5 });

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

        //測試加入Node
        [TestMethod()]
        public void AddNodeTest()
        {
            Assert.IsTrue(_layer1.NodeAmount == 2);
            _layer1.AddNode(new NeuralNode(new Sigmoid()));
            Assert.IsTrue(_layer1.NodeAmount == 3);
        }

        //測試前匯1
        [TestMethod()]
        public void CalculateFeedForwardTest()
        {
            double expectValue1 = 1.0 / (1 + Math.Exp(0 - (0.5 * 0.1 + 0.4 * 0.3 + 0.8 * 0.5)));
            double expectValue2 = 1.0 / (1 + Math.Exp(0 - (0.2 * 0.5 + 0.4 * 0.4 + 0.8 * 0.5)));
            _layer2.CalculateFeedForward(_layer1);
            double resultValue1 = _layer2.GetOutput()[0];
            double resultValue2 = _layer2.GetOutput()[1];
            Assert.AreEqual(expectValue1, resultValue1, 0.000001);
            Assert.AreEqual(expectValue2, resultValue2, 0.000001);
        }

        //測試前匯2
        [TestMethod()]
        public void CalculateFeedForwardTest1()
        {
            double expectValue1 = 0.4;
            double expectValue2 = 0.3;
            _layer2.CalculateFeedForward(new List<double>() { expectValue1, expectValue2 });
            double resultValue1 = _layer2.GetOutput()[0];
            double resultValue2 = _layer2.GetOutput()[1];
            Assert.AreEqual(expectValue1, resultValue1, 0.000001);
            Assert.AreEqual(expectValue2, resultValue2, 0.000001);
        }

        //測試取得輸出
        [TestMethod()]
        public void GetOutputTest()
        {
            Assert.AreEqual(_layer1.GetOutput()[0], _layer1Node1.Output, 0.000001);
            Assert.AreEqual(_layer1.GetOutput()[1], _layer1Node2.Output, 0.000001);
            Assert.AreEqual(_layer2.GetOutput()[0], _layer2Node1.Output, 0.000001);
            Assert.AreEqual(_layer2.GetOutput()[1], _layer2Node2.Output, 0.000001);
        }

        //測試反向傳播法
        [TestMethod()]
        public void BackpropagationTest()
        {
            List<double> result = _layer2.Backpropagation();
            double expectValue1 = (0.5 * 0.3) * 0.4 * (1 - 0.4);
            double expectValue2 = (0.4 * 0.3) * 0.4 * (1 - 0.4);
            Assert.AreEqual(expectValue1, result[0], 0.000001);
            Assert.AreEqual(expectValue2, result[1], 0.000001);
        }

        //測試反向傳播法1
        [TestMethod()]
        public void BackpropagationTest1()
        {
            List<double> result = _layer2.Backpropagation(new List<double>() { 1, 0 }, new SquaredError());
            double expectValue1 = 0 - ((1 - 0.4) * 0.4 * (1 - 0.4));
            double expectValue2 = 0 - ((0 - 0.4) * 0.4 * (1 - 0.4));
            Assert.AreEqual(expectValue1, result[0], 0.000001);
            Assert.AreEqual(expectValue2, result[1], 0.000001);
        }

        //測試反向傳播法設置權重
        [TestMethod()]
        public void BackpropagationSetWeightTest()
        {
            List<NeuralNode> nodes = (List<NeuralNode>)_layer1PrrivateObject.GetFieldOrProperty("_nodes");
            _layer1.BackpropagationSetWeight(new List<double>() { 1, 0.2 });
            double expectValue1 = 0.1 - 1 * 0.5 * 0.1;
            double expectValue2 = 0.2 - 0.2 * 0.5 * 0.1;
            double expectValue3 = 0.3 - 1 * 0.4 * 0.1;
            double expectValue4 = 0.4 - 0.2 * 0.4 * 0.1;
            Assert.AreEqual(expectValue1, nodes[0].OutputWeight[0], 0.000001);
            Assert.AreEqual(expectValue2, nodes[0].OutputWeight[1], 0.000001);
            Assert.AreEqual(expectValue3, nodes[1].OutputWeight[0], 0.000001);
            Assert.AreEqual(expectValue4, nodes[1].OutputWeight[1], 0.000001);
        }

        //測試取得權重
        [TestMethod()]
        public void GetWeightsTest()
        {
            List<List<double>> result = _layer1.GetWeights();
            Assert.AreEqual(result[0][0], 0.1, 0.000001);
            Assert.AreEqual(result[0][1], 0.2, 0.000001);
            Assert.AreEqual(result[1][0], 0.3, 0.000001);
            Assert.AreEqual(result[1][1], 0.4, 0.000001);
        }

        //測試初始化權重
        [TestMethod()]
        public void InitBiasWeightTest()
        {
            List<double> biasWeight = (List<double>)_layer1PrrivateObject.GetFieldOrProperty("_biasWeight");
            Assert.AreEqual(biasWeight[0], 0.5, 0.000001);
            Assert.AreEqual(biasWeight[1], 0.5, 0.000001);
            _layer1.InitBiasWeight(new List<double>() { 0.2, 0.3 });
            Assert.AreEqual(biasWeight[0], 0.2, 0.000001);
            Assert.AreEqual(biasWeight[1], 0.3, 0.000001);
        }

        //測試複製
        [TestMethod()]
        public void CopyTest()
        {
            NeuralLayer layer = _layer1.Copy();
            PrivateObject layerPrivateObject = new PrivateObject(layer);
            PrivateObject layer1PrivateObject = new PrivateObject(_layer1);

            Assert.AreEqual((double)layerPrivateObject.GetFieldOrProperty("_bias"), (double)layer1PrivateObject.GetFieldOrProperty("_bias"), 0.0001);
            layer1PrivateObject.SetFieldOrProperty("_bias", 1.2);
            Assert.AreNotEqual((double)layerPrivateObject.GetFieldOrProperty("_bias"), (double)layer1PrivateObject.GetFieldOrProperty("_bias"), 0.0001);

            Assert.AreEqual((double)layerPrivateObject.GetFieldOrProperty("_learningRate"), (double)layer1PrivateObject.GetFieldOrProperty("_learningRate"), 0.0001);
            layer1PrivateObject.SetFieldOrProperty("_learningRate", 1.2);
            Assert.AreNotEqual((double)layerPrivateObject.GetFieldOrProperty("_learningRate"), (double)layer1PrivateObject.GetFieldOrProperty("_learningRate"), 0.0001);

            List<double> layerBiasWeights = (List<double>)layerPrivateObject.GetFieldOrProperty("_biasWeight");
            List<double> layer1BiasWeights = (List<double>)layer1PrivateObject.GetFieldOrProperty("_biasWeight");
            for (int index = 0; index < layerBiasWeights.Count(); index++)
            {
                Assert.AreEqual(layerBiasWeights[index], layer1BiasWeights[index], 0.0001);
                layer1BiasWeights[index] = 1.2;
                Assert.AreNotEqual(layerBiasWeights[index], layer1BiasWeights[index], 0.0001);
            }

            List<NeuralNode> layerNeuralNode = (List<NeuralNode>)layerPrivateObject.GetFieldOrProperty("_nodes");
            List<NeuralNode> layer1NeuralNode = (List<NeuralNode>)layer1PrivateObject.GetFieldOrProperty("_nodes");
            for (int index = 0; index < layerBiasWeights.Count(); index++)
            {
                Assert.AreNotEqual(layerNeuralNode[index], layer1NeuralNode[index]);
            }
        }
    }
}