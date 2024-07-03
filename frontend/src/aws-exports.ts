const awsconfig = {
    aws_project_region: 'us-east-1',  // Replace with your own region
    aws_cognito_region: 'us-east-1',
    aws_user_pools_id: 'us-east-1_Ph41OKWF8',            // Add your own user-pool-id
    aws_user_pools_web_client_id: '165320a8ntn332eks21c3q7r8l', // Add your own client-id
    oauth: {
      domain: 'bbdrs.auth.us-east-1.amazoncognito.com',    // Add your own domain-url
      scope: [
        'phone',
        'email',
        'openid',
        'profile',
        'aws.cognito.signin.user.admin'
      ],
      redirectSignIn: 'http://localhost:4200/dashboard',    // Add your own redirect sign-in url
      redirectSignOut: 'http://localhost:4200',   // Add your own redirect sign-out url
      responseType: 'code'
    }
  };
  
  export default awsconfig;