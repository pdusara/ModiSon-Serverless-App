variable "api_name" {
  type = string
}

variable "lambda_invoke_arn" {
  description = "The invoke ARN of the Lambda function"
  type        = string
}

variable "lambda_function_name" {
  description = "The name of the Lambda function"
  type        = string
}

variable "cognito_client_id" {
  type = string
}

variable "cognito_user_pool_url" {
  description = "Cognito user pool domain prefix"
  type        = string
}

variable "cognito_issuer" {
  type = string
}

variable "allow_origins" {
  type = list(string)
}

variable "routes" {
  description = "List of route keys for API Gateway"
  type = list(object({
    route_key          = string # e.g., "GET /api/orders"
    authorization_type = string # e.g., "JWT"
  }))
}




