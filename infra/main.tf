provider "aws" {
  region = "us-east-1"
}

module "dynamodb-products" {
  source = "./modules/dynamodb/products"
}

module "dynamodb-orders" {
  source = "./modules/dynamodb/orders"
}

module "aws_cognito" {
  source     = "./modules/aws_cognito"
  aws_region = "us-east-1"
}

module "s3_bucket" {
  source                     = "./modules/s3_bucket"
  cloudfront_distribution_id = module.cloudfront.distribution_id
}

module "cloudfront" {
  source           = "./modules/cloudfront"
  s3_bucket_domain = "modison-bucket.s3.amazonaws.com"
  s3_bucket_id     = "modison-bucket"

  logging_config = {
    bucket = module.s3_bucket.cf_logs_bucket_regional_domain_name
    prefix = "cf-modison-logs/"
  }
}

module "lambda_products" {
  source        = "./modules/lambda/products"
  function_name = "Products"
  runtime       = "dotnet8"
  handler       = "Products::Products.LambdaEntryPoint::FunctionHandlerAsync"
  filename      = "${path.module}/../lambdas/src/Products/ProductsFunction.zip"
}

module "products_api" {
  source               = "./modules/api_gateway"
  lambda_invoke_arn    = module.lambda_products.lambda_invoke_arn
  lambda_function_name = module.lambda_products.lambda_function_name

  cognito_client_id     = module.aws_cognito.user_pool_id
  cognito_user_pool_url = module.aws_cognito.user_pool_domain
  cognito_issuer        = "https://cognito-idp.us-east-1.amazonaws.com/${module.aws_cognito.user_pool_id}"

  allow_origins = ["https://${module.cloudfront.domain_name}"]
}




