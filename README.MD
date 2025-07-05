# ModiSon Serverless App

A modern, cloud native serverless application built with AWS Lambda, .NET 8, and Terraform. This project demonstrates scalable backend architecture, infrastructure-as-code, and follows best practices for cloud deployments.

**Live Demo:** [ModiSon Serverless App](https://d1p4pvkzpycf8v.cloudfront.net/Views/Home/Index.html)  
**Frontend Source:** [Modison-static-site](https://github.com/pdusara/Modison-static-site/tree/main)

## Features

- **Serverless Architecture:** Modular AWS Lambda functions for Products and Orders.
- **Infrastructure as Code:** Automated AWS resource provisioning using Terraform.
- **API Gateway Integration:** Secure, scalable RESTful APIs.
- **Authentication:** AWS Cognito integration for user management and authentication.
- **CI/CD:** GitHub Actions workflow for automated deployment.
- **Unit Testing:** Integrated test projects and scripts for robust code quality.

## Project Structure

```
├── infra/                # Terraform scripts for AWS infrastructure 
│   ├── main.tf 
│   ├── variables.tf 
│   └── modules/ 
├── lambdas/ 
│   └── src/ 
│       ├── Products/ 
│       └── Orders/ 
├── .github/ 
│   └── workflows/ 
│       └── deploy-lambda.yml 
└── README.MD
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Terraform](https://www.terraform.io/downloads)
- [AWS CLI](https://aws.amazon.com/cli/)
- AWS account credentials configured

### Setup & Deployment

1. **Clone the repository:**
    ```sh
    git clone https://github.com/yourusername/ModiSon-Serverless-App.git
    cd ModiSon-Serverless-App
    ```

2. **Provision AWS Infrastructure:**
    ```sh
    cd infra
    terraform init
    terraform apply
    ```

3. **Build and Deploy Lambda Functions:**
    ```sh
    cd lambdas/src/Products
    dotnet build -c Release
    dotnet lambda deploy-function

    cd ../Orders
    dotnet build -c Release
    dotnet lambda deploy-function
    ```

4. **Run Unit Tests:**
    ```sh
    dotnet test
    ```

### CI/CD

- Automated deployments are configured via [GitHub Actions](.github/workflows/deploy-lambda.yml).









