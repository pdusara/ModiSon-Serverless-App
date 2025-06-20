output "s3_bucket_domain" {
  value = aws_s3_bucket.modison_site.bucket_regional_domain_name
}

output "s3_bucket_id" {
  value = aws_s3_bucket.modison_site.id
}

output "cf_logs_bucket_regional_domain_name" {
  value = aws_s3_bucket.cf_logs.bucket_regional_domain_name
}

