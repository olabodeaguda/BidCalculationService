<template>
  <div class="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
    <div class=" flex justify-between">
      <h1 class="text-xl font-medium mb-4">Bid Histories</h1>

      <router-link to="/addbid" class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
        Add bid
      </router-link>
    </div>

    <div class="overflow-x-auto">
      <table class="min-w-full bg-white divide-y divide-gray-200">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Vehicle type
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Base
              price
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Total
              Fees
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Total
              Price
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Created At</th>
            <th></th>

          </tr>
        </thead>
        <tbody class="divide-y divide-gray-200">
          <tr v-for="(bid, index) in bids" :key="bid.id">
            <td class="px-6 py-4 whitespace-nowrap">{{ index + 1 }}</td>
            <td class="px-6 py-4 whitespace-nowrap">{{ bid.vehicleType }}</td>
            <td class="px-6 py-4 whitespace-nowrap">{{ bid.basePrice.toLocaleString('en-US', {
              style: 'currency',
              currency: 'USD'
            }) }}</td>
            <td class="px-6 py-4 whitespace-nowrap">{{ bid.totalFees.toLocaleString('en-US', {
              style: 'currency',
              currency: 'USD'
            }) }}</td>
            <td class="px-6 py-4 whitespace-nowrap">{{ (bid.totalFees + bid.basePrice).toLocaleString('en-US', {
              style:
                'currency', currency: 'USD'
            }) }}</td>

            <td class="px-6 py-4 whitespace-nowrap">{{ formatDate(bid.createdAt) }}</td>
            <td>
              <router-link :to="`/bidbyid/${bid.id}`" class="first-letter:text-blue-500 underline hover:text-blue-600">
                View Bid
              </router-link>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="mt-4">
      <PaginationComponent :page="pageNumber" :totalPages="totalPages" @page-change="fetchBids" />
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import PaginationComponent from '../components/PaginationComponent.vue';
import { state } from '@/utils/state';

export default {
  name: 'BidView',
  components: {
    PaginationComponent
  },
  setup() {
    const bids = ref([]);
    const pageNumber = ref(0);
    const pageSize = ref(5);
    const totalPages = ref(0);

    const fetchBids = async (newPage) => {
      try {
        const pNumber = newPage + 1;
        const response = await fetch(`api/v1/bids?pageNumber=${pNumber}&pageSize=${pageSize.value}`, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${state.token}`
          }
        });
        const data = await response.json();
        if (response.ok === true) {
          bids.value = data.items;
          totalPages.value = data.totalPages;
        } else if (response.status === 401) {
          this.$router.push('/login');
        } else {
          alert('Error fetching bids:');
        }
      } catch (error) {
        alert('Error fetching bids:', error);
      }
    };

    const formatDate = (dateString) => {
      const date = new Date(dateString);
      return date.toLocaleString();
    };

    onMounted(() => {
      fetchBids(0);
    });

    return {
      bids,
      pageNumber,
      pageSize,
      totalPages,
      fetchBids,
      formatDate
    };
  }
};
</script>