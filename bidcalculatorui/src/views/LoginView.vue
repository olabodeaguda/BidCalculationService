<template>
    <div class="flex items-center justify-center min-h-screen bg-gray-100">
        <div class="bg-white p-8 rounded shadow-md w-full max-w-md">
            <h1 class="text-2xl font-bold mb-6 text-center">Login</h1>
            <form @submit.prevent="handleLogin">
                <div class="mb-4">
                    <label for="username" class="block text-gray-700 mb-2">Email</label>
                    <input type="text" id="username" v-model="username"
                        class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required />
                </div>
                <div class="mb-6">
                    <label for="password" class="block text-gray-700 mb-2">Password</label>
                    <input type="password" id="password" v-model="password"
                        class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required />
                </div>
                <button type="submit" class="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600">
                    Login
                </button>
            </form>
            <p class="mt-4 text-center">
                Don't have an account?
                <router-link to="/register" class="text-blue-500 hover:underline">
                    Register
                </router-link>
            </p>
        </div>
    </div>
</template>

<script>
import { setLoginState } from '@/utils/state';

export default {
    name: "LoginView",
    data() {
        return {
            username: "",
            password: "",
        };
    },
    methods: {
        async handleLogin() {
            const res = await fetch("api/v1/Accounts/login", {
                method: "POST",
                headers: {
                    "Content-type": "application/json",
                },
                body: JSON.stringify({
                    email: this.username,
                    password: this.password,
                }),
            });
            const data = await res.json();
            if (data.isSuccess === true) {
                const userP = data.data;
                setLoginState(true, `${userP.userProfile.firstName} ${userP.userProfile.lastName}`, userP.token)
                this.$router.push("/bid");
            } else {
                alert(data?.error?.description ?? "email or password is invalid");
            }
        },
    },
};
</script>
