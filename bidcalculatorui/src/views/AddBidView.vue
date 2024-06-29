<template>
    <div class="min-h-screen bg-gray-100 flex items-center justify-center">
        <div class="bg-white p-8 rounded shadow-md w-full max-w-md">
            <h1 class="text-2xl font-bold mb-6 text-center">Add New Bid</h1>
            <form @submit.prevent="submitBid">
                <div class="mb-4">
                    <label for="vehicleType" class="block text-gray-700 mb-2">Vehicle Type</label>
                    <select id="vehicleType" v-model="vehicleType"
                        class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required>
                        <option value="COMMON">COMMON</option>
                        <option value="LUXURY">LUXURY</option>
                    </select>
                </div>
                <div class="mb-4">
                    <label for="basePrice" class="block text-gray-700 mb-2">Base Price</label>
                    <input type="number" id="basePrice" v-model.number="basePrice"
                        class="w-full px-3 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required />
                </div>
                <button type="submit" class="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600">
                    Submit Bid
                </button>
            </form>
            <div v-if="responseMessage" class="mt-4">
                <p class="text-green-500" v-if="isSuccess">{{ responseMessage }}</p>
                <p class="text-red-500" v-else>{{ responseMessage }}</p>
            </div>
        </div>
    </div>
</template>

<script>
import { state } from '@/utils/state';
export default {
    name: 'AddBidView',
    data() {
        return {
            vehicleType: 'COMMON',
            basePrice: 0,
            responseMessage: '',
            isSuccess: false
        };
    },
    methods: {
        async submitBid() {
            if (!state.isLoggedIn) {
                this.$router.push('/login');
                return;
            }
            try {
                const response = await fetch(`${process.env.VUE_APP_BASE_URL}/api/v1/Bids`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${state.token}`
                    },
                    body: JSON.stringify({
                        vehicleType: this.vehicleType,
                        basePrice: this.basePrice
                    })
                });
                const data = await response.json();
                if (data.isSuccess) {
                    this.responseMessage = 'Bid submitted successfully!';
                    this.isSuccess = true;
                    this.$router.push('/bid');
                } else {
                    this.responseMessage = data.error.description || 'An error occurred.';
                    this.isSuccess = false;
                }
            } catch (error) {
                this.responseMessage = 'An error occurred while submitting the bid.';
                this.isSuccess = false;
            }
        }
    }
};
</script>