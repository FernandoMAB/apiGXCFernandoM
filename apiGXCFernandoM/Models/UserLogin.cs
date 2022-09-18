﻿using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace apiGXCFernandoM.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? EmailAddress { get; set; }
        public string? GivenName { get; set; }
        public string? Role { get; set; }
    }
    public class TokenUsu
    {
        public string token { get; set; }
    }
}
