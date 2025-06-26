output "api_url" {
  value       = "${aws_apigatewayv2_api.this.api_endpoint}/api/products"
  description = "Fully qualified API Gateway URL for /products"
}
