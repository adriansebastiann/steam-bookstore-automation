using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamBookstoreTests.Api
{
    public static class ApiRoutes
    {
        public const string BaseUrl = "https://bookstore.toolsqa.com";

        public const string CreateUser = "/Account/v1/User";
        public const string GenerateToken = "/Account/v1/GenerateToken";
        public const string Authorize = "/Account/v1/Authorized";
        public const string GetUser = "/Account/v1/User/{0}";
        public const string DeleteUser = "/Account/v1/User/{0}";

        public const string GetBooks = "/BookStore/v1/Books";
        public const string AddBooks = "/BookStore/v1/Books";
        public const string ReplaceBook = "/BookStore/v1/Books/{0}";

        public const string AuthorizationHeader = "Authorization";
        public const string BearerTokenFormat = "Bearer {0}";
    }
}
