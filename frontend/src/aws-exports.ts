// src/aws-exports.js
const awsExports = {
    Auth: {
        region: 'your-region',
        userPoolId: 'your-user-pool-id',
        userPoolWebClientId: 'your-app-client-id',
        mandatorySignIn: true,
        authenticationFlowType: 'USER_PASSWORD_AUTH'
    }
};

export default awsExports;
