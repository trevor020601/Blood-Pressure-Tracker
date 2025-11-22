using Microsoft.AspNetCore.Authorization;
using SharedLibrary.Authentication.Policies;

namespace SharedLibrary.Attributes;

public sealed class AuthorizationPolicyAttribute : AuthorizeAttribute
{
    public AuthorizationPolicyAttribute(Policy policy) : base(policy.Name) { }
}
