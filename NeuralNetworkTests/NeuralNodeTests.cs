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
    public class NeuralNodeTests
    {
        //測試一個Node的所有功能
        [TestMethod()]
        public void NeuralNodeTest()
        {
            
        }

        //測試設定新的Weight
        [TestMethod()]
        public void SetWeightsAmountTest()
        {
            NeuralNode testNode = new NeuralNode();
            Assert.IsTrue(testNode.OutputWeight.Count() == 0);
            testNode.SetWeightsAmount(5);
            Assert.IsTrue(testNode.OutputWeight.Count() == 5);
        }

        //測試加入新Node的Weight
        [TestMethod()]
        public void AddNewNodeWeightTest()
        {
            NeuralNode testNode = new NeuralNode();
            Assert.IsTrue(testNode.OutputWeight.Count() == 0);
            testNode.AddNewNodeWeight();
            Assert.IsTrue(testNode.OutputWeight.Count() == 1);
        }

        //測試logisicFunction
        [TestMethod()]
        public void LogisticFunctionTest()
        {
            NeuralNode testNode = new NeuralNode();
            List<NeuralNode> inputNodes = new List<NeuralNode>();
            List<double> weightOne = new List<double>() { 0.5 };
            List<double> weightTwo = new List<double>() { 0.2 };
            inputNodes.Add(new NeuralNode() { Output = 1, OutputWeight = weightOne });
            inputNodes.Add(new NeuralNode() { Output = 1, OutputWeight = weightTwo });
            double result = 1.0 / (1 + Math.Exp(0 - (0.5 * 1 + 0.2 * 1 + 0.3 * 0.4)));
            Assert.IsTrue(testNode.LogisticFunction(inputNodes, 0, 0.3, 0.4) == result);
        }
    }
}