// module containerRegistry './container-registry.bicep' = {
//   name: 'containerRegistryDeployment'
//   params: {
//     // Add required parameters for the container registry module here
//   }
// }

// Deploy Log Analytics workspace
module containerapps 'container-apps.bicep' = {
  name: 'logAnalyticsDeployment'
  params: {
  }
}

// // Deploy Log Analytics workspace
// module logAnalytics 'log-analytics-workspace.bicep' = {
//   name: 'logAnalyticsDeployment'
//   params: {
//     logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
//   }
// }

// // Deploy Application Insights and link to Log Analytics workspace
// module appInsights 'app-insights.bicep' = {
//   name: 'appInsightsDeployment'
//   params: {
//     appInsightsName: appInsightsName
//     applicationType: applicationType
//     logAnalyticsWorkspaceId: logAnalytics.outputs.logAnalyticsWorkspaceId
//   }
// }

// module containerizedApp './containerized-app.bicep' = {
//   name: 'containerizedAppDeployment'
//   params: {
//     logAnalyticsWorkspaceId: logAnalytics.outputs.logAnalyticsCustomerId
//     logAnalyticsWorkspaceKey: logAnalytics.outputs.logAnalyticsWorkspaceKey
//   }
// }
