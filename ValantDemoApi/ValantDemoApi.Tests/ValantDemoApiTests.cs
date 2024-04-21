using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ValantDemoApi.DTO;

namespace ValantDemoApi.Tests
{
  [TestFixture]
  public class ValantDemoApiTests
  {
    private HttpClient client;

    [OneTimeSetUp]
    public void Setup()
    {
      var factory = new APIWebApplicationFactory();
      this.client = factory.CreateClient();
    }

    [Test]
    public async Task ShouldReturnAllFourDirectionsForMovementThroughMaze()
    {
      var result = await this.client.GetAsync("/Maze");
      result.EnsureSuccessStatusCode();
      var content = JsonConvert.DeserializeObject<string[]>(await result.Content.ReadAsStringAsync());
      content.Should().Contain("Up");
      content.Should().Contain("Down");
      content.Should().Contain("Left");
      content.Should().Contain("Right");
    }
    [Test]
    public async Task ShouldMoveToRightPosition()
    {
      string maze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||SOOOXXXXXE";
      string expectedMaze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||OSOOXXXXXE||";
      var movePosition = new MovePosition() { Direction = "Right", Maze = maze };
      var movePositionJson = JsonConvert.SerializeObject(movePosition);
      var result = await this.client.PostAsync("/MovePosition", new StringContent(movePositionJson, Encoding.UTF8, "application/json"));
      result.EnsureSuccessStatusCode();
      var content = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
      Assert.AreEqual(content, expectedMaze);
    }
    [Test]
    public async Task ShouldMoveToUpPosition()
    {
      string maze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||SOOOXXXXXE";
      string expectedMaze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||SXXOXXXXXO||OOOOXXXXXE||";
      var movePosition = new MovePosition() { Direction = "Up", Maze = maze };
      var movePositionJson = JsonConvert.SerializeObject(movePosition);
      var result = await this.client.PostAsync("/MovePosition", new StringContent(movePositionJson, Encoding.UTF8, "application/json"));
      result.EnsureSuccessStatusCode();
      var content = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
      Assert.AreEqual(content, expectedMaze);
    }
    [Test]
    public async Task ShouldMoveToLeftPosition()
    {
      string maze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||SOOOXXXXXE";
      string expectedMaze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||SOOOXXXXXE||";
      var movePosition = new MovePosition() { Direction = "Left", Maze = maze };
      var movePositionJson = JsonConvert.SerializeObject(movePosition);
      var result = await this.client.PostAsync("/MovePosition", new StringContent(movePositionJson, Encoding.UTF8, "application/json"));
      result.EnsureSuccessStatusCode();
      var content = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
      Assert.AreEqual(content, expectedMaze);
    }
    [Test]
    public async Task ShouldMoveToDownPosition()
    {
      string maze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||SOOOXXXXXE";
      string expectedMaze = "OOXXXXXXXX||OOOXXXXXXX||OXOOOXOOOO||XXXXOXOXXO||OOOOOOOXXO||OXXOXXXXXO||SOOOXXXXXE||";
      var movePosition = new MovePosition() { Direction = "Down", Maze = maze };
      var movePositionJson = JsonConvert.SerializeObject(movePosition);
      var result = await this.client.PostAsync("/MovePosition", new StringContent(movePositionJson, Encoding.UTF8, "application/json"));
      result.EnsureSuccessStatusCode();
      var content = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
      Assert.AreEqual(content, expectedMaze);
    }
  }
}
