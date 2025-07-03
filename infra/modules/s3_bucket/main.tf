data "aws_caller_identity" "current" {}

resource "aws_s3_bucket" "modison_site" {
  bucket = "modison-bucket"
}

resource "aws_s3_bucket_versioning" "modison_versioning" {
  bucket = aws_s3_bucket.modison_site.id

  versioning_configuration {
    status = "Enabled"
  }
}

resource "aws_s3_bucket_server_side_encryption_configuration" "modison_encryption" {
  bucket = aws_s3_bucket.modison_site.id

  rule {
    apply_server_side_encryption_by_default {
      sse_algorithm = "AES256"
    }
  }
}

resource "aws_s3_bucket_policy" "modison_policy" {
  bucket = aws_s3_bucket.modison_site.id
  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Sid    = "AllowCloudFrontAccessWithOAC",
        Effect = "Allow",
        Principal = {
          Service = "cloudfront.amazonaws.com"
        },
        Action   = "s3:GetObject",
        Resource = "arn:aws:s3:::modison-bucket/*",
        Condition = {
          StringEquals = {
            "AWS:SourceArn" = "arn:aws:cloudfront::${data.aws_caller_identity.current.account_id}:distribution/${var.cloudfront_distribution_id}"
          }
        }
      }
    ]
  })
}








