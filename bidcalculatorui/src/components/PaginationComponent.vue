<template>
    <nav class="flex items-center justify-center mt-4" aria-label="Pagination">
        <ul class="inline-flex space-x-2">
            <li v-if="page > 0" @click="changePage(page - 1)"
                class="cursor-pointer px-2 py-1 bg-blue-500 text-white rounded hover:bg-blue-600">Previous</li>
            <li v-for="index in totalPages" :key="index" @click="changePage(index - 1)"
                :class="{ 'bg-blue-500 text-white px-4 py-2 rounded cursor-pointer hover:bg-blue-600': index === page + 1, 'text-gray-700 cursor-pointer hover:text-blue-500': index !== page + 1 }">
                {{ index }}</li>
            <li v-if="page < totalPages - 1" @click="changePage(page + 1)"
                class="cursor-pointer px-2 py-1 bg-blue-500 text-white rounded hover:bg-blue-600">Next</li>
        </ul>
    </nav>
</template>

<script>
export default {
    name: 'PaginationComponent',
    props: {
        page: {
            type: Number,
            required: true
        },
        totalPages: {
            type: Number,
            required: true
        }
    },
    methods: {
        changePage(newPage) {
            this.$emit('page-change', newPage);
        }
    }
};
</script>