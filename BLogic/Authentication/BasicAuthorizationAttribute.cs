using Microsoft.AspNetCore.Authorization;

namespace Pedalacom.BLogic.Authentication
{
    public class BasicAutorizationAttributes : AuthorizeAttribute
    {
        public BasicAutorizationAttributes()
        {
            Policy = "BasicAuthentication";
        }
    }
}
