using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a priorityQueue with the following items (value, priority): (Bob, 1), (Tim, 1), (Sue, 1) and
    // run until the queue is empty
    // Expected Result: Bob, Tim, Sue
    // Defect(s) Found: Items of equal priority were returning the last item in the list instead of the first item in the list
    //                  Items were not being removed from the list when dequeued
    public void TestPriorityQueue_equalPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 1);
        priorityQueue.Enqueue("Sue", 1);

        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Tim", priorityQueue.Dequeue());
        Assert.AreEqual("Sue", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Create a priorityQueue with the following items (value, priority): (Bob, 1), (Tim, 2), (Sue, 3) and
    // run until the queue is empty
    // Expected Result: Sue, Tim, Bob
    // Defect(s) Found: The last item in the list was not being checked for priority
    public void TestPriorityQueue_growingPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 2);
        priorityQueue.Enqueue("Sue", 3);

        Assert.AreEqual("Sue", priorityQueue.Dequeue());
        Assert.AreEqual("Tim", priorityQueue.Dequeue());
        Assert.AreEqual("Bob", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Create a priorityQueue with the following items (value, priority): (Bob, 1), (Tim, 2), (Sue, 2), (Jan, 1) and
    // run until the queue is empty
    // Expected Result: Tim, Sue, Bob, Jan
    // Defect(s) Found: None found
    public void TestPriorityQueue_equalHighPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 2);
        priorityQueue.Enqueue("Sue", 2);
        priorityQueue.Enqueue("Jan", 1);

        Assert.AreEqual("Tim", priorityQueue.Dequeue());
        Assert.AreEqual("Sue", priorityQueue.Dequeue());
        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Jan", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Create an empty priorityQueue and attempt to dequeue
    // Expected Result: Exception should be thrown with appropriate error message
    // Defect(s) Found: None found
    public void TestPriorityQueue_emptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    // Add more test cases as needed below.
}