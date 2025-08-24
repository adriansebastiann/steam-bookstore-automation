using NUnit.Framework;
using Reqnroll;
using RestSharp;
using SteamBookstoreTests.Api;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SteamBookstoreTests.Steps
{
    [Binding]
    public class BookApiSteps
    {
        private readonly ScenarioContext _ctx;
        private readonly BookstoreClient _client = new();
        private string? _userId;
        private string? _username;
        private string? _password;
        private string? _token;
        private string? _firstIsbn;
        private string? _secondIsbn;

        public BookApiSteps(ScenarioContext ctx) => _ctx = ctx;

        [Given(@"a new authorized user")]
        public async Task GivenANewAuthorizedUser()
        {
            _username = $"user_{Guid.NewGuid():N}".Substring(0, 12);
            _password = $"P@ssw0rd{new Random().Next(1000,9999)}!Aa";
            var create = await _client.CreateUserAsync(_username, _password);
            Assert.That((int)create.StatusCode, Is.EqualTo(201), "Create user failed");

            using var doc = JsonDocument.Parse(create.Content!);
            _userId = doc.RootElement.GetProperty("userID").GetString();

            var tokenResp = await _client.GenerateTokenAsync(_username, _password);
            Assert.That((int)tokenResp.StatusCode, Is.EqualTo(200), "Generate token failed");
            using var td = JsonDocument.Parse(tokenResp.Content!);
            _token = td.RootElement.GetProperty("token").GetString();
            Assert.IsNotEmpty(_token, "Token should not be empty");

            var auth = await _client.AuthorizeAsync(_username, _password);
            Assert.That((int)auth.StatusCode, Is.EqualTo(200), "Authorize failed");
        }

        [When(@"I get the list of all books")]
        public async Task WhenIGetTheBooks()
        {
            var resp = await _client.GetBooksAsync();
            Assert.That((int)resp.StatusCode, Is.EqualTo(200), "Get books failed");
            using var doc = JsonDocument.Parse(resp.Content!);
            var books = doc.RootElement.GetProperty("books").EnumerateArray().ToList();
            Assert.That(books.Count, Is.GreaterThan(1), "Expected at least 2 books");
            _firstIsbn = books[0].GetProperty("isbn").GetString();
            _secondIsbn = books[1].GetProperty("isbn").GetString();
        }

        [When(@"I add the first book to the user's collection")]
        public async Task WhenIAddFirstBook()
        {
            var resp = await _client.AddBooksAsync(_userId!, _token!, new[] { _firstIsbn! });
            Assert.That((int)resp.StatusCode, Is.EqualTo(201), "Add book failed");
        }

        [When(@"I get the user by id")]
        public async Task WhenIGetUserById()
        {
            var resp = await _client.GetUserAsync(_userId!, _token!);
            _ctx["lastUserResponse"] = resp;
            Assert.That((int)resp.StatusCode, Is.EqualTo(200), "Get user failed");
        }

        [Then(@"the user has exactly 1 book and it matches the added book")]
        public void ThenUserHas1BookMatchesFirst()
        {
            var resp = (RestResponse)_ctx["lastUserResponse"];
            using var doc = JsonDocument.Parse(resp.Content!);
            var books = doc.RootElement.GetProperty("books").EnumerateArray().ToList();
            Assert.That(books.Count, Is.EqualTo(1), "User should have 1 book");
            var isbn = books[0].GetProperty("isbn").GetString();
            Assert.That(isbn, Is.EqualTo(_firstIsbn), "Book should match the added book");
        }

        [When(@"I replace the user's book with the second book")]
        public async Task WhenIReplaceBook()
        {
            var resp = await _client.ReplaceBooksAsync(_userId!, _token!, _firstIsbn!, _secondIsbn!);
            Assert.That((int)resp.StatusCode, Is.EqualTo(200), "Replace (PUT) failed");
        }

        [Then(@"the user has exactly 1 book and it is the second book")]
        public void ThenUserHas1BookMatchesSecond()
        {
            var resp = (RestResponse)_ctx["lastUserResponse"];
            using var doc = JsonDocument.Parse(resp.Content!);
            var books = doc.RootElement.GetProperty("books").EnumerateArray().ToList();
            Assert.That(books.Count, Is.EqualTo(1), "User should have 1 book");
            var isbn = books[0].GetProperty("isbn").GetString();
            Assert.That(isbn, Is.EqualTo(_secondIsbn), "Book should be the second book");
        }

        [Then(@"I delete the created user")]
        public async Task ThenIDeleteUser()
        {
            var del = await _client.DeleteUserAsync(_userId!, _token!);
            Assert.That((int)del.StatusCode, Is.EqualTo(204), "Delete user failed");
        }
    }
}
