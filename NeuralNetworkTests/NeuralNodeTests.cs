using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork;
using NeuralNetwork.ActivationFunction;
using NeuralNetwork.Optimizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Tests
{
    [TestClass()]
    public class NeuralNodeTests
    {
        private NeuralNode _node;

        //初始化神經元
        [TestInitialize()]
        public void Initialize()
        {
            _node = new NeuralNode(new Sigmoid(), new SGD());
            _node.LoadWeights(new List<double>() { 0.1, 0.2, 0.3 });
            _node.Output = 0.5;
            _node.AddTotalDeltaWeight(0.5, 0);
        }


        //測試一個Node的所有功能
        [TestMethod()]
        public void NeuralNodeTest()
        {
            
        }

        //測試設定新的Weight
        [TestMethod()]
        public void SetWeightsAmountTest()
        {
            NeuralNode testNode = new NeuralNode(new Sigmoid(), new SGD());
            Assert.IsTrue(testNode.OutputWeight.Count == 0);
            testNode.SetWeightsAmount(5);
            Assert.IsTrue(testNode.OutputWeight.Count == 5);
        }

        //測試加入新Node的Weight
        [TestMethod()]
        public void AddNewNodeWeightTest()
        {
            NeuralNode testNode = new NeuralNode(new Sigmoid(), new SGD());
            Assert.IsTrue(testNode.OutputWeight.Count == 0);
            testNode.AddNewNodeWeight();
            Assert.IsTrue(testNode.OutputWeight.Count == 1);
        }

        //測試logisicFunction
        [TestMethod()]
        public void LogisticFunctionTest()
        {
            NeuralNode testNode = new NeuralNode(new Sigmoid(), new SGD());
            List<NeuralNode> inputNodes = new List<NeuralNode>();
            List<double> weightOne = new List<double>() { 0.5 };
            List<double> weightTwo = new List<double>() { 0.2 };
            inputNodes.Add(new NeuralNode(new Sigmoid(), new SGD()) { Output = 1, OutputWeight = weightOne });
            inputNodes.Add(new NeuralNode(new Sigmoid(), new SGD()) { Output = 1, OutputWeight = weightTwo });
            double result = 1.0 / (1 + Math.Exp(0 - (0.5 * 1 + 0.2 * 1 + 0.3 * 0.4)));
            Assert.IsTrue(testNode.ActivationFunction(inputNodes, 0, 0.3, 0.4) == result);
        }

        //測試直接輸入有多少weight
        [TestMethod()]
        public void LoadWeightsTest()
        {
            NeuralNode testNode = new NeuralNode(new Sigmoid(), new SGD());
            Assert.IsTrue(testNode.OutputWeight.Count == 0);
            testNode.LoadWeights(new List<double>() { 0.1, 0.2 });
            Assert.IsTrue(testNode.OutputWeight.Count == 2);
        }

        //測試利用Delta值來修正權重
        [TestMethod()]
        public void SetWeightTest()
        {
            double expectValue = _node.OutputWeight[0] - 0.3 * 0.5 * 0.1;
            _node.SetWeight(0.3, 0, 0.1);
            Assert.IsTrue(_node.OutputWeight[0] + 0.00001 > expectValue && _node.OutputWeight[0] - 0.00001 < expectValue);
        }

        //測試重置後面所有Delta值的和
        [TestMethod()]
        public void ResetTotalDeltaWeightTest()
        {
            Assert.IsTrue(_node.TotalDeltaWeight > 0.05 - 0.000001 && _node.TotalDeltaWeight < 0.05 + 0.000001);
            _node.ResetTotalDeltaWeight();
            Assert.IsTrue(_node.TotalDeltaWeight == 0);
        }

        //測試Copy
        [TestMethod()]
        public void CopyTest()
        {
            NeuralNode node = _node.Copy();
            PrivateObject nodePrivateObject = new PrivateObject(_node);

            Assert.AreEqual(node.Output, _node.Output, 0.0001);
            _node.Output = 0.01;
            Assert.AreNotEqual(node.Output, _node.Output, 0.0001);

            Assert.AreEqual(node.TotalDeltaWeight, _node.TotalDeltaWeight, 0.0001);
            nodePrivateObject.SetFieldOrProperty("_totalDeltaWeight", 1.2);
            Assert.AreNotEqual(node.TotalDeltaWeight, _node.TotalDeltaWeight, 0.0001);

            Assert.AreEqual(node.Net, _node.Net, 0.0001);
            nodePrivateObject.SetFieldOrProperty("_net", 1.2);
            Assert.AreNotEqual(node.Net, _node.Net, 0.0001);

            for (int index = 0; index < node.OutputWeight.Count; index++)
            {
                Assert.AreEqual(node.OutputWeight[index], _node.OutputWeight[index], 0.0001);
                _node.OutputWeight[index] = 1.2;
                Assert.AreNotEqual(node.OutputWeight[index], _node.OutputWeight[index], 0.0001);
            }
        }
    }
}