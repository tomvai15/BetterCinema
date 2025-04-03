@description('Location for all resources.')
param location string = resourceGroup().location

param logAnalyticsWorkspaceName string

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
    workspaceCapping: {
      dailyQuotaGb: 1
    }
  }
}


output logAnalyticsWorkspace object = logAnalyticsWorkspace

output logAnalyticsWorkspaceId string = logAnalyticsWorkspace.id
#disable-next-line outputs-should-not-contain-secrets
output logAnalyticsWorkspaceKey string = logAnalyticsWorkspace.listKeys().primarySharedKey
