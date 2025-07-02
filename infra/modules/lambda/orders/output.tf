output "lambda_function_name" {
  value = aws_lambda_function.order_lambda.function_name
}

output "lambda_invoke_arn" {
  value = aws_lambda_function.order_lambda.invoke_arn
}

output "lambda_role_name" {
  value = aws_iam_role.orders_lambda_role.name

}
