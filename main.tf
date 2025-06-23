provider "aws" {
  region = "us-east-1"
}

module "products" {
  source = "./modules/products"
}

module "orders" {
  source = "./modules/orders"
}

module "aws_cognito" {
  source = "./modules/aws_cognito"
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


