using System.Net;
using api.DTOs.Requests;
using FluentAssertions;

namespace E2E;

/// <summary>
/// Contains end-to-end authentication related tests.
/// </summary>
[TestFixture]
public class AuthenticationTests
{
    /// <summary>
    /// The relative URI path for the Authentication API endpoint used in HTTP requests.
    /// </summary>
    private const string requestUri = "/Authentication";

    /// <summary>
    /// HttpClient instance used for sending HTTP requests to the test server.
    /// </summary>
    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationTests"/> class and creates an HTTP client for testing.
    /// </summary>
    public AuthenticationTests()
    {
        client = TestAssemblySetup.Factory.CreateClient();
    }

    /// <summary>
    /// Disposes of the HttpClient instance after all the tests in this class are complete.
    /// </summary>
    [OneTimeTearDown]
    public void TearDown() => client.Dispose();

    /// <summary>
    /// Tests the SignIn process with valid number and password.
    /// </summary>
    [Test]
    public async Task SignInAsync_ReturnsAcceptedAndData()
    {
        //Arrange
        string requestUrl = string.Concat(requestUri, "/signin");
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558275,
            Password = "8428Ss827$"
        };

        Dictionary<string, string> formData = new()
        {
            { "Number", signInRequestDTO.Number.ToString() },
            { "Password", signInRequestDTO.Password }
        };

        FormUrlEncodedContent formUrlEncodedContent = new(formData);

        //Act
        var response = await client.PostAsync(requestUrl, formUrlEncodedContent);

        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    /// <summary>
    /// Tests the SignIn process with invalid number and password.
    /// </summary>
    [Test]
    public async Task SignInAsync_ReturnsUnAuthorizedAndNull()
    {
        //Arrange
        string requestUrl = string.Concat(requestUri, "/signin");
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558276,
            Password = "8428S$827s"
        };

        Dictionary<string, string> formData = new()
        {
            { "Number", signInRequestDTO.Number.ToString() },
            { "Password", signInRequestDTO.Password }
        };

        FormUrlEncodedContent formUrlEncodedContent = new(formData);

        //Act
        var response = await client.PostAsync(requestUrl, formUrlEncodedContent);

        //Assert
        response.IsSuccessStatusCode.Should().Be(false);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}