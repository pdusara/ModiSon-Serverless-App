output "distribution_id" {
  value = aws_cloudfront_distribution.modison_site.id
}

output "domain_name" {
  value = aws_cloudfront_distribution.modison_site.domain_name
}
