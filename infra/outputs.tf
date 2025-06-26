output "api_url" {
  description = "API Gateway endpoint for the Products service"
  value       = module.products_api.api_url
}
