variable "s3_bucket_domain" {
  description = "The domain name of the S3 bucket"
  type        = string
}

variable "s3_bucket_id" {
  description = "The ID of the S3 bucket"
  type        = string
}

variable "logging_config" {
  description = "Optional map for CloudFront logging"
  type = object({
    bucket = string
    prefix = string
  })
  default = null
}
