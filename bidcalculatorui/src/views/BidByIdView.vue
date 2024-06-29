<template>
    <div class="min-h-screen bg-gray-100 flex items-center justify-center">
        <div class="bg-white p-8 rounded shadow-md w-full max-w-2xl relative">
            <router-link to="/bid" class="absolute left-4 top-4 text-blue-500 hover:underline">Back</router-link>
            <h1 class="text-2xl font-bold mb-6 text-center">Bid Details</h1>
            <div v-if="bid">
                <div class="mb-4">
                    <p><strong>Vehicle Type:</strong> {{ bid.vehicleType }}</p>
                    <p><strong>Base Price:</strong> ${{ bid.basePrice.toFixed(2) }}</p>
                    <p><strong>Created At:</strong> {{ formatDate(bid.createdAt) }}</p>
                    <p><strong>Total Fees:</strong> ${{ bid.totalFees.toFixed(2) }}</p>
                </div>
                <div class="mb-4">
                    <h2 class="text-xl font-semibold">Breakdown</h2>
                    <table class="min-w-full bg-white">
                        <thead>
                            <tr>
                                <th class="py-2">Fee Type</th>
                                <th class="py-2">Amount ($)</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="border px-4 py-2">Base Fee</td>
                                <td class="border px-4 py-2">{{ bid.basePrice.toFixed(2) }}</td>
                            </tr>
                            <tr v-for="fee in bid.fees" :key="fee.feeType">
                                <td class="border px-4 py-2">{{ formatFeeType(fee.feeType) }}</td>
                                <td class="border px-4 py-2">{{ fee.amount.toFixed(2) }}</td>
                            </tr>
                            <tr>
                                <td class="border px-4 py-2 font-bold">Total Amount</td>
                                <td class="border px-4 py-2  font-bold">{{ (bid.basePrice + bid.totalFees).toFixed(2) }}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div v-else>
                <p>Loading...</p>
            </div>
        </div>
    </div>
</template>

<script>
import { state } from '@/utils/state';

export default {
    name: 'BidByIdView',
    data() {
        return {
            bid: null,
        };
    },
    async created() {
        await this.fetchBidDetails();
    },
    methods: {
        async fetchBidDetails() {
            try {
                const response = await fetch(`${process.env.VUE_APP_BASE_URL}/api/v1/bids/${this.$route.params.id}`, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${state.token}`
                    }
                });
                const data = await response.json();

                if (data.isSuccess === true) {
                    this.bid = data.data;
                } else {
                    console.error(data.error?.description || 'Error fetching bid details');
                }
            } catch (error) {
                console.error('Error fetching bid details', error);
            }
        },
        formatDate(dateString) {
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            return new Date(dateString).toLocaleDateString(undefined, options);
        },
        formatFeeType(feeType) {
            return feeType
                .toLowerCase()
                .split('_')
                .map(word => word.charAt(0).toUpperCase() + word.slice(1))
                .join(' ');
        },
    },
};
</script>