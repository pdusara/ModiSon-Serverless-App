output "arn" {
  value       = aws_iam_policy.lambda_logging.arn
  description = "ARN of the CloudWatch logging policy"
}
