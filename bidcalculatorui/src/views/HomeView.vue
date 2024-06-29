<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100 pt-10">
    <div class="flex space-x-4">
      <div
        class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 cursor-pointer"
      >
        <router-link to="/register">Register</router-link>
      </div>
      <div
        class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 cursor-pointer"
      >
        <router-link to="/login">Login</router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "HomeView",
};
</script>
