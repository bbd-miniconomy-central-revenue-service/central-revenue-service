terraform {
  backend "s3" {
    bucket = "crs-levelup-bucket"
    key = "crs/terraform.tfstate"  # Specify the path/key for your state file
    region = "us-east-1"
  }
}