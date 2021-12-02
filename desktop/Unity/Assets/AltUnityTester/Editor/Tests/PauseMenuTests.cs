using NUnit.Framework;
using Altom.AltUnityDriver;
using UnityEngine;
using TerrainGeneratorComponent;

public class PauseMenuTests
{
    public AltUnityDriver altUnityDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altUnityDriver =new AltUnityDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altUnityDriver.Stop();
    }

    // [Test]
    // public void ResumeButtonTest()
    // {
	//     var tGen = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
    //     tGen.exitMenu.SetActive(true);
    // }

    // This ended up not working due to the inability to reference the pause menu or terrain generator
    // without a working key press simulator

}