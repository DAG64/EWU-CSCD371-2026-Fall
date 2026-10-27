using Dag;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dag.Tests;

[TestClass]
public class DagTests
{
    [TestMethod]
    public void AddNode_AddsNode()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");

        Assert.AreEqual(0, dag.GetDependencies("A").Count);
        Assert.AreEqual(0, dag.GetDependents("A").Count);
    }

    [TestMethod]
    public void DuplicateNode_DoesNotThrow()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        dag.AddNode("A");

        Assert.IsNotNull(dag.GetDependencies("A"));
    }

    [TestMethod]
    public void AddDependency_AddsEdge()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        dag.AddNode("B");

        dag.AddDependency("A", "B");

        Assert.IsTrue(dag.GetDependencies("B").Contains("A"));
        Assert.IsTrue(dag.GetDependents("A").Contains("B"));
    }

    [TestMethod]
    public void DuplicateDependency_DoesNotAddDuplicateEdge()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        dag.AddNode("B");

        dag.AddDependency("A", "B");
        dag.AddDependency("A", "B");

        Assert.AreEqual(1, dag.GetDependencies("B").Count);
        Assert.AreEqual(1, dag.GetDependents("A").Count);
    }

    [TestMethod]
    public void GetDependencies_ReturnsDirectDependencies()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        dag.AddNode("B");
        dag.AddNode("C");

        dag.AddDependency("A", "B");
        dag.AddDependency("B", "C");

        var dependencies = dag.GetDependencies("C");

        Assert.AreEqual(1, dependencies.Count);
        Assert.IsTrue(dependencies.Contains("B"));
    }

    [TestMethod]
    public void GetDependents_ReturnsDirectDependents()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        dag.AddNode("B");
        dag.AddNode("C");

        dag.AddDependency("A", "B");
        dag.AddDependency("B", "C");

        var dependents = dag.GetDependents("A");

        Assert.AreEqual(1, dependents.Count);
        Assert.IsTrue(dependents.Contains("B"));
    }

    [TestMethod]
    public void DemonstrationGraph_FanOutFanIn()
    {
        var dag = new Dag<string>();

        dag.AddNode("CSCD 211");
        dag.AddNode("CSCD 300");
        dag.AddNode("CSCD 320");
        dag.AddNode("CSCD 371");

        dag.AddDependency("CSCD 211", "CSCD 300");
        dag.AddDependency("CSCD 211", "CSCD 320");
        dag.AddDependency("CSCD 300", "CSCD 371");
        dag.AddDependency("CSCD 320", "CSCD 371");

        var dependencies = dag.GetDependencies("CSCD 371");

        Assert.AreEqual(2, dependencies.Count);
        Assert.IsTrue(dependencies.Contains("CSCD 300"));
        Assert.IsTrue(dependencies.Contains("CSCD 320"));

        var dependents = dag.GetDependents("CSCD 211");

        Assert.AreEqual(2, dependents.Count);
        Assert.IsTrue(dependents.Contains("CSCD 300"));
        Assert.IsTrue(dependents.Contains("CSCD 320"));
    }

    [TestMethod]
    public void DemonstrationGraph_DetectsCycle()
    {
        var dag = new Dag<string>();

        dag.AddNode("CSCD 211");
        dag.AddNode("CSCD 300");
        dag.AddNode("CSCD 320");
        dag.AddNode("CSCD 371");

        dag.AddDependency("CSCD 211", "CSCD 300");
        dag.AddDependency("CSCD 211", "CSCD 320");
        dag.AddDependency("CSCD 300", "CSCD 371");
        dag.AddDependency("CSCD 320", "CSCD 371");
        AssertThrows<InvalidOperationException>(() =>
        {
            dag.AddDependency("CSCD 371", "CSCD 211");
        });
    }

    [TestMethod]
    public void CycleDetection_ThrowsException()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        dag.AddNode("B");

        dag.AddDependency("A", "B");

        AssertThrows<InvalidOperationException>(() =>
        {
            dag.AddDependency("B", "A");
        });
    }

    [TestMethod]
    public void AddDependency_MissingDependentNode()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");
        AssertThrows<ArgumentException>(() =>
        {
            dag.AddDependency("A", "B");
        });
    }

    [TestMethod]
    public void AddDependency_MissingDependsOnNode()
    {
        var dag = new Dag<string>();

        dag.AddNode("A");

        AssertThrows<ArgumentException>(() =>
                {
                    dag.AddDependency("B", "A");
                });
    }

[TestMethod]
public void GetDependencies_MissingNode()
{
    var dag = new Dag<string>();

    AssertThrows<ArgumentException>(() =>
    {
        dag.GetDependencies("A");
    });
}

    [TestMethod]
    public void GetDependents_MissingNode()
    {
        var dag = new Dag<string>();

        AssertThrows<ArgumentException>(() =>
        {
            dag.GetDependents("A");
        });
    }

    private static void AssertThrows<TException>(System.Action action)
        where TException : System.Exception
    {
        try
        {
            action();
        }
        catch (TException)
        {
            return;
        }

        Assert.Fail($"Expected {typeof(TException).Name} to be thrown.");
    }
}