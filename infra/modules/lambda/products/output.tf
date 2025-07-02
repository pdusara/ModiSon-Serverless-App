output "lambda_function_name" {
  value = aws_lambda_function.product_lambda.function_name
}

output "lambda_invoke_arn" {
  value = aws_lambda_function.product_lambda.invoke_arn
}

output "lambda_role_name" {
  value = aws_iam_role.products_lambda_role.name

}
