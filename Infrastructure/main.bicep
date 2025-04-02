module containerRegistry './container-registry.bicep' = {
  name: 'containerRegistryDeployment'
  params: {
    // Add required parameters for the container registry module here
  }
}

module containerizedApp './containerized-app.bicep' = {
  name: 'containerizedAppDeployment'
  params: {
    // Add required parameters for the container registry module here
  }
}
