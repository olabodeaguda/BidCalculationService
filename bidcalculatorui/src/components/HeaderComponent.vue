<template>
    <header class="bg-white shadow p-4 flex justify-between items-center">
        <div class="text-xl font-bold">
            Bid Calculator
        </div>
        <div class="flex items-center space-x-4" v-show="showLoginBtn">
            <span v-if="state.isLoggedIn" class="text-gray-700">Welcome, {{ state.username }}</span>
            <button v-if="state.isLoggedIn" @click="logout"
                class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
                Logout
            </button>
            <button v-else @click="login" class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
                Login
            </button>
        </div>
    </header>
</template>

<script>
import { state, setLoginState } from '@/utils/state';

export default {
    name: "HeaderComponent",
    data() {
        return {
            state
        };
    },
    methods: {
        logout() {
            localStorage.removeItem("access");
            setLoginState(false, '');
            this.$router.push("/login");
        },
        login() {
            this.$router.push("/login");
        },
    },
    computed: {
        showLoginBtn() {
            return this.$route.path !== '/login';
        }
    }
};
</script>
