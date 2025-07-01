resource "aws_cloudfront_origin_access_control" "modison_oac" {
  name                              = "modison-oac"
  description                       = "OAC for Modison CloudFront"
  origin_access_control_origin_type = "s3"
  signing_behavior                  = "always"
  signing_protocol                  = "sigv4"
}

resource "aws_cloudfront_distribution" "modison_site" {
  origin {
    domain_name = var.s3_bucket_domain
    origin_id   = "S3Origin"

    origin_access_control_id = aws_cloudfront_origin_access_control.modison_oac.id
  }

  enabled             = true
  default_root_object = "Views/Home/Index.html"

  viewer_certificate {
    cloudfront_default_certificate = true
    minimum_protocol_version       = "TLSv1.2_2021"
  }

  restrictions {
    geo_restriction {
      restriction_type = "none"
    }
  }

  default_cache_behavior {
    allowed_methods  = ["GET", "HEAD"]
    cached_methods   = ["GET", "HEAD"]
    target_origin_id = "S3Origin"

    viewer_protocol_policy = "redirect-to-https"

    forwarded_values {
      query_string = false
      headers      = []
      cookies {
        forward = "none"
      }
    }
  }

  dynamic "logging_config" {
    for_each = var.logging_config == null ? [] : [var.logging_config]
    content {
      bucket          = logging_config.value.bucket
      prefix          = logging_config.value.prefix
      include_cookies = false
    }
  }
}

