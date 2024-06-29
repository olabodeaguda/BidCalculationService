const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    proxy: {
      "^/api": {
        target: "https://localhost:7056",
        changeOrigin: true,
        logLevel: "debug",
        pathRewrite: { "^/api": "/api" },
      },
    },
  },
});
