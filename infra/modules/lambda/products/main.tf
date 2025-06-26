data "aws_caller_identity" "current" {}

resource "aws_iam_role" "products_lambda_role" {
  name = "products_lambda_role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [{
      Effect = "Allow"
      Principal = {
        Service = "lambda.amazonaws.com"
      }
      Action = "sts:AssumeRole"
    }]
  })
}

resource "aws_iam_role_policy" "products_dynamodb_access" {
  name = "products_dynamodb_access"
  role = aws_iam_role.products_lambda_role.id

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [{
      Effect = "Allow"
      Action = [
        "dynamodb:DescribeTable",
        "dynamodb:PutItem",
        "dynamodb:GetItem",
        "dynamodb:UpdateItem",
        "dynamodb:DeleteItem",
        "dynamodb:Scan",
        "dynamodb:Query"
      ]
      Resource = "arn:aws:dynamodb:us-east-1:${data.aws_caller_identity.current.account_id}:table/Products"
    }]
  })
}

resource "aws_lambda_function" "product_lambda" {
  function_name    = var.function_name
  role             = aws_iam_role.products_lambda_role.arn
  runtime          = var.runtime
  handler          = var.handler
  filename         = var.filename
  source_code_hash = filebase64sha256(var.filename)
  timeout          = 10
}



