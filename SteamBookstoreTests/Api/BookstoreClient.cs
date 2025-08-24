using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace SteamBookstoreTests.Api
{
    public class BookstoreClient
    {
        private readonly RestClient _client;

        public BookstoreClient(string baseUrl = ApiRoutes.BaseUrl)
        {
            _client = new RestClient(new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false
            });
        }

        public Task<RestResponse> GetBooksAsync() =>
            _client.ExecuteAsync(new RestRequest(ApiRoutes.GetBooks, Method.Get));

        public Task<RestResponse> CreateUserAsync(string userName, string password) =>
            _client.ExecuteAsync(new RestRequest(ApiRoutes.CreateUser, Method.Post)
                .AddJsonBody(new { userName, password }));

        public Task<RestResponse> GenerateTokenAsync(string userName, string password) =>
            _client.ExecuteAsync(new RestRequest(ApiRoutes.GenerateToken, Method.Post)
                .AddJsonBody(new { userName, password }));

        public Task<RestResponse> AuthorizeAsync(string userName, string password) =>
            _client.ExecuteAsync(new RestRequest(ApiRoutes.Authorize, Method.Post)
                .AddJsonBody(new { userName, password }));

        public Task<RestResponse> GetUserAsync(string userId, string token) =>
            _client.ExecuteAsync(new RestRequest(string.Format(ApiRoutes.GetUser, userId), Method.Get)
                .AddHeader(ApiRoutes.AuthorizationHeader, string.Format(ApiRoutes.BearerTokenFormat, token)));

        public Task<RestResponse> AddBooksAsync(string userId, string token, System.Collections.Generic.IEnumerable<string> isbns) =>
            _client.ExecuteAsync(new RestRequest(ApiRoutes.AddBooks, Method.Post)
                .AddHeader(ApiRoutes.AuthorizationHeader, string.Format(ApiRoutes.BearerTokenFormat, token))
                .AddJsonBody(new { userId, collectionOfIsbns = isbns.Select(isbn => new { isbn }) }));

        public Task<RestResponse> ReplaceBooksAsync(string userId, string token, string oldIsbn, string newIsbn) =>
            _client.ExecuteAsync(new RestRequest(string.Format(ApiRoutes.ReplaceBook, oldIsbn), Method.Put)
                .AddHeader(ApiRoutes.AuthorizationHeader, string.Format(ApiRoutes.BearerTokenFormat, token))
                .AddJsonBody(new { userId, isbn = newIsbn }));

        public Task<RestResponse> DeleteUserAsync(string userId, string token) =>
            _client.ExecuteAsync(new RestRequest(string.Format(ApiRoutes.DeleteUser, userId), Method.Delete)
                .AddHeader(ApiRoutes.AuthorizationHeader, string.Format(ApiRoutes.BearerTokenFormat, token)));
    }
}
