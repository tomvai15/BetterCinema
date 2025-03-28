import type { NextConfig } from "next";
const { NextFederationPlugin } = require('@module-federation/nextjs-mf');

const remotes = (isServer: boolean) => {
  const location = isServer ? 'ssr' : 'chunks';
  return {
    // specify remotes
    remote: `remote@http://localhost:3001/_next/static/${location}/remoteEntry.js`,
  };
}

const nextConfig: NextConfig = {
  /* config options here */

  webpack(config, options ){
    config.plugins.push(
      new NextFederationPlugin({
        name: 'remote',
        filename: 'static/chunks/remoteEntry.js',
        remotes: remotes(options.isServer),
        exposes: {
          // host does not need to expose
        },
        extraOptions: {
        },
      })
    )
    return config
  }
};

export default nextConfig;
