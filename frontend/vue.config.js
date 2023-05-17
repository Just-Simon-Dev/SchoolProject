const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
	proxy: {
	  '^/api': {
		target: 'http://backend:3080',
		changeOrigin: true
	  },
	}
  }
})
