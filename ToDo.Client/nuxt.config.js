import colors from 'vuetify/es5/util/colors'

export default {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    titleTemplate: '%s - ToDo',
    title: 'ToDo',
    htmlAttrs: {
      lang: 'en'
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/eslint
    '@nuxtjs/eslint-module',
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify'
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    // https://go.nuxtjs.dev/axios
    '@nuxtjs/axios'
  ],

  // Axios module configuration: https://go.nuxtjs.dev/config-axios
  // axios: {
  //   proxy: true,
  //   debug: process.env.AXIOS_DEBUG
  // },
  // axios: {
  //   // WARNING: proxy doesn't work with nuxt generate,
  //   // have to use a prefix and set an API_URL
  //   proxy: false,
  //   prefix: process.env.API_URL
  // },
  // axios: {
  //   baseURL: 'http://localhost:5075',
  //   proxyHeaders: false,
  //   credentials: false
  // },

  // proxy: {
  //   '/api/': {
  //     target: process.env.BACKEND_API_ROOT,
  //     pathRewrite: {
  //       [process.env.AXIOS_PROXY_PATH_REWRITE_FROM]:
  //         process.env.AXIOS_PROXY_PATH_REWRITE_TO
  //     }
  //   }
  // },
  // proxy: {
  //   '^/api': {
  //     target: 'http://localhost:5075',
  //     ws: true,
  //     changeOrigin: true
  //   }
  // },

  axios: {
    proxy: true,
    debug: process.env.AXIOS_DEBUG
  },
  proxy: {
    '/api/': {
      target: process.env.BACKEND_API_ROOT,
      pathRewrite: {
        [process.env.AXIOS_PROXY_PATH_REWRITE_FROM]:
          process.env.AXIOS_PROXY_PATH_REWRITE_TO
      }
    }
  },

  // Vuetify module configuration: https://go.nuxtjs.dev/config-vuetify
  vuetify: {
    customVariables: ['~/assets/variables.scss'],
    theme: {
      dark: true,
      themes: {
        dark: {
          primary: colors.blue.darken2,
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3
        }
      }
    }
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
  }
  // publicRuntimeConfig: {
  //   env: process.env.NODE_ENV,
  //   baseURL: process.env.BASE_URL,
  //   axios: {
  //     debug: process.env.AXIOS_DEBUG
  //   },
  //   debouncingTimeout: process.env.DEBOUNCING_TIMEOUT
  // }
}
