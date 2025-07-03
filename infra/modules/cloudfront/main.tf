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
    allowed_methods        = ["GET", "HEAD"]
    cached_methods         = ["GET", "HEAD"]
    target_origin_id       = "S3Origin"
    viewer_protocol_policy = "redirect-to-https"

    min_ttl     = 3600     # Cache objects for at least 1 hour
    default_ttl = 86400    # Default to 1 day
    max_ttl     = 31536000 # Cache up to 1 year if not changed

    compress = true # Enable compression for text-based assets

    forwarded_values {
      query_string = false
      headers      = []
      cookies {
        forward = "none"
      }
    }

  }
}

