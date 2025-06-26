provider "aws" {
  region = "us-east-1" # Adjust for your preferred region
}

# Cognito User Pool (Supports All Users)
resource "aws_cognito_user_pool" "user_pool" {
  name = "my-app-user-pool"

  lifecycle {
    ignore_changes = [schema]
  }

  alias_attributes         = ["email"]
  auto_verified_attributes = ["email"]

  admin_create_user_config {
    allow_admin_create_user_only = false # All users can register
  }

  schema {
    attribute_data_type = "String"
    name                = "email"
    required            = true
    mutable             = true
  }

  schema {
    attribute_data_type = "String"
    name                = "company"
    required            = true
    mutable             = true
  }
}

# Cognito User Pool Client (Implicit Flow for Static Apps)
resource "aws_cognito_user_pool_client" "user_pool_client" {
  name                                 = "app-client"
  user_pool_id                         = aws_cognito_user_pool.user_pool.id
  allowed_oauth_flows                  = ["implicit"] # Best for frontend static apps
  allowed_oauth_scopes                 = ["openid", "profile", "email"]
  allowed_oauth_flows_user_pool_client = true
  explicit_auth_flows                  = ["ALLOW_USER_SRP_AUTH", "ALLOW_REFRESH_TOKEN_AUTH"]
  generate_secret                      = false # Must be false for frontend authentication
  supported_identity_providers         = ["COGNITO"]

  # Authentication Redirect URLs
  callback_urls = ["https://localhost:7122", "https://your-static-site.com"]
  logout_urls   = ["https://localhost:7122/logout", "https://your-static-site.com/logout"]
}

# Cognito Hosted UI Domain
resource "aws_cognito_user_pool_domain" "domain" {
  domain       = "modison-auth-domain"
  user_pool_id = aws_cognito_user_pool.user_pool.id
}

# Outputs for Integration
output "user_pool_id" {
  value = aws_cognito_user_pool.user_pool.id
}

output "app_client_id" {
  value = aws_cognito_user_pool_client.user_pool_client.id
}

output "auth_domain" {
  value = "${aws_cognito_user_pool_domain.domain.domain}.auth.us-east-1.amazoncognito.com"
}
