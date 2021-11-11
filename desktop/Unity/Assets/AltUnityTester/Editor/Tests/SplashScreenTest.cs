using NUnit.Framework;
using Altom.AltUnityDriver;

public class SplashScreenTest
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

    [Test]
    public void SuccessfulLoginTest()
    {
	    altUnityDriver.LoadScene("Main Menu");
        altUnityDriver.FindObject(By.NAME, "EmailForm").SetText("nick@geodream.alpha");
        altUnityDriver.FindObject(By.NAME, "PasswordField").SetText("1337h4x0r");
        altUnityDriver.FindObject(By.NAME, "LoginButton").Click();
        altUnityDriver.LoadScene("TerrainGenerator/Scene/MenuScene");
        altUnityDriver.FindObject(By.NAME, "Generate Button").Click();
    }

    [Test]
    public void WrongCredentialsTest()
    {
	    altUnityDriver.LoadScene("Main Menu");
        altUnityDriver.FindObject(By.NAME, "EmailForm").SetText("aaron@geodream.alpha");
        altUnityDriver.FindObject(By.NAME, "PasswordField").SetText("oijoidjew");
        altUnityDriver.FindObject(By.NAME, "LoginButton").Click();
    }

}