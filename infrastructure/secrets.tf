data "aws_secretsmanager_secret_version" "creds" {
  secret_id = "sql-server"
}