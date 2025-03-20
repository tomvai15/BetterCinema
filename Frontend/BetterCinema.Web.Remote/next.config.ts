import type { NextConfig } from "next";
import { NextFederationPlugin } from '@module-federation/nextjs-mf';

const nextConfig: NextConfig = {
  /* config options here */

  webpack(config, options ){
    config.plugins.push(
      new NextFederationPlugin({
        name: 'remote',
        filename: 'static/chunks/remoteEntry.js',
        exposes: {
          // specify exposed pages and components
          './FancyComponent': './components/FancyComponent.js'  
        },
        extraOptions: {
        },
      })
    )
    return config
  }
};

export default nextConfig;
