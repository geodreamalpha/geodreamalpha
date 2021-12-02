using NUnit.Framework;
using Altom.AltUnityDriver;
using UnityEngine;

public class SeedMenuTests
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

    //Tests user story #5
    [Test]
    public void GenerateWorldTest()
    {
	    altUnityDriver.LoadScene("TerrainGenerator/Scene/MenuScene");
        altUnityDriver.FindObject(By.NAME, "Generate Button").Click();
    }

    // I was not able to run any other tests like entering custom seeds due to how AltUnity interferes
    // with Tyler's code 

}