variable "function_name" {
  description = "The name of the Lambda function"
  type        = string
}

variable "runtime" {
  description = "Runtime environment for Lambda"
  type        = string
  default     = "dotnet8"
}

variable "handler" {
  description = "Handler string (Assembly::Namespace.Class::Method)"
  type        = string
}

variable "filename" {
  description = "Path to the Lambda zip file"
  type        = string
}

variable "timeout" {
  description = "Lambda execution timeout in seconds"
  type        = number
  default     = 10
}
