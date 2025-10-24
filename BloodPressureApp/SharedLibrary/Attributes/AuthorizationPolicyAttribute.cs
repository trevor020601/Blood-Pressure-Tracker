using Microsoft.AspNetCore.Authorization;
using SharedLibrary.Authentication;

namespace SharedLibrary.Attributes;

public sealed class AuthorizationPolicyAttribute : AuthorizeAttribute
{
    public AuthorizationPolicyAttribute(Policy policy) : base(policy.ToString()) { }
}
