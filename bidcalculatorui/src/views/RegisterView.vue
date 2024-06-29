<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="bg-white p-8 rounded shadow-md w-full max-w-md">
      <h1 class="text-2xl font-bold mb-6 text-center">Register</h1>
      <form @submit.prevent="handleRegister">
        <div class="mb-4">
          <label for="email" class="block text-gray-700 mb-2">Email</label>
          <input type="email" id="email" v-model="email"
            class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500" required />
        </div>
        <div class="mb-4">
          <label for="firstName" class="block text-gray-700 mb-2">First Name</label>
          <input type="text" id="firstName" v-model="firstName"
            class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500" required />
        </div>
        <div class="mb-4">
          <label for="lastName" class="block text-gray-700 mb-2">Last Name</label>
          <input type="text" id="lastName" v-model="lastName"
            class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500" required />
        </div>
        <div class="mb-4">
          <label for="password" class="block text-gray-700 mb-2">Password</label>
          <input type="password" id="password" v-model="password"
            class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500" required />
        </div>
        <div class="mb-6">
          <label for="confirmPassword" class="block text-gray-700 mb-2">Confirm Password</label>
          <input type="password" id="confirmPassword" v-model="confirmPassword"
            class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500" required />
        </div>
        <button type="submit" class="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600">
          Register
        </button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'RegisterPage',
  data() {
    return {
      email: '',
      firstName: '',
      lastName: '',
      password: '',
      confirmPassword: ''
    };
  },
  methods: {
    async handleRegister() {
      if (this.password !== this.confirmPassword) {
        alert('Passwords do not match');
        return;
      }

      const userData = {
        email: this.email,
        firstName: this.firstName,
        lastName: this.lastName,
        password: this.password,
        confirmPassword: this.confirmPassword
      };

      try {
        const res = await fetch('api/v1/Accounts', {
          method: 'POST',
          headers: {
            'Content-type': 'application/json'
          },
          body: JSON.stringify(userData)
        });
        const data = await res.json();
        if (data.isSuccess === true) {
          alert('Registration successful');
          this.$router.push('/login');
        } else {
          alert(data?.error?.description ?? 'Registration failed');
        }
      } catch (error) {
        console.error('Error registering user:', error);
        alert('An error occurred while registering');
      }
    }
  }
};
</script>
