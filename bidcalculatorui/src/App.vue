<template>
  <HeaderComponent />

  <router-view />
</template>

<script>
import HeaderComponent from "./components/HeaderComponent.vue";

export default {
  name: "App",
  components: {
    HeaderComponent,
  },
  data() {
    return {
      syncLogin: Boolean
    }
  },
  methods: {
    onSyncLogin() {
      this.syncLogin = !this.syncLogin;
    }
  }
};
</script>
