output "lambda_function_name" {
  value = aws_lambda_function.order_lambda.function_name
}

output "lambda_invoke_arn" {
  value = aws_lambda_function.order_lambda.invoke_arn
}
