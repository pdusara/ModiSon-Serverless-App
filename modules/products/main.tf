resource "aws_dynamodb_table" "products" {
  name         = "Products"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "ProductId"

  attribute {
    name = "ProductId"
    type = "S"
  }

  tags = {
    Name = "ProductsTable"
  }
}
