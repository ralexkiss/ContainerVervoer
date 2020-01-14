using ContainerVervoer.Enums;
using ContainerVervoer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        Port port;
        [TestInitialize]
        public void InitializeTests()
        {
            port = new Port(6, 6, 220000);
        }

        [TestMethod]
        public void CreateContainer()
        {
            Assert.AreEqual(new Container(20000, ContainerType.Normal).Weight, 20000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WeightTooHeigh()
        {
            _ = new Container(31000, ContainerType.Normal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WeightTooLow()
        {
            _ = new Container(1000, ContainerType.Normal);
        }

        [TestMethod]
        public void StackWeightWithinLimit()
        {
            Stack stack = new Stack();
            stack.Containers.Add(new Container(4000, ContainerType.Normal));
            stack.Containers.Add(new Container(7000, ContainerType.Normal));
            stack.Containers.Add(new Container(12000, ContainerType.Normal));
            Assert.IsTrue(stack.WeightWithinLimit(new Container(4000, ContainerType.Normal)));
        }

        [TestMethod]
        public void StackWeightExceedsLimit()
        {
            Stack stack = new Stack();
            stack.Containers.Add(new Container(4000, ContainerType.Normal));
            stack.Containers.Add(new Container(30000, ContainerType.Normal));
            stack.Containers.Add(new Container(30000, ContainerType.Normal));
            stack.Containers.Add(new Container(30000, ContainerType.Normal));
            stack.Containers.Add(new Container(15000, ContainerType.Normal));
            Assert.IsFalse(stack.WeightWithinLimit(new Container(20000, ContainerType.Normal)));
        }
    }
}