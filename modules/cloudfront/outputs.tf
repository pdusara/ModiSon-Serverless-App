output "cloudfront_oai" {
  value = aws_cloudfront_origin_access_identity.oai.s3_canonical_user_id
}

output "distribution_id" {
  value = aws_cloudfront_distribution.modison_site.id
}
