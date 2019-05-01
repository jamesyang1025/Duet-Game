using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Tests {

    [Test]
    public void TestsSimplePasses() {
        // Use the Assert class to test conditions.
        Assert.AreEqual(false, false);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator TestsWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        GameObject obstacle = UnityEngine.Object.Instantiate(Resources.Load("Rectangle"), Vector3.zero, Quaternion.identity) as GameObject;

        Assert.AreEqual(obstacle.transform.position, Vector3.zero);

        yield return null;

    }
}
