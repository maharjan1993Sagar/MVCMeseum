﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Meseum.Extension
{
    public static class RoleExtension
    {
        public static string GetRole(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Role");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetStatus(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Satus");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : "false";
        }
    }
}