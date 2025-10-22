<script setup>
const props = defineProps({
  filters: Object,
});
const emit = defineEmits(["filter-change"]);

const applyFilters = () => emit("filter-change");
</script>

<template>
  <div class="filters">
    <select v-model="filters.status" @change="applyFilters">
      <option value="">Все</option>
      <option value="Published">Опубликованные</option>
      <option value="Unpublished">Снятые с публикации</option>
    </select>

    <input
      v-model.number="filters.minAmount"
      type="number"
      placeholder="Мин. сумма"
      @input="applyFilters"
    />
    <input
      v-model.number="filters.maxAmount"
      type="number"
      placeholder="Макс. сумма"
      @input="applyFilters"
    />
    <input
      v-model.number="filters.minTerm"
      type="number"
      placeholder="Мин. срок"
      @input="applyFilters"
    />
    <input
      v-model.number="filters.maxTerm"
      type="number"
      placeholder="Макс. срок"
      @input="applyFilters"
    />
  </div>
</template>

<style scoped>
.filters {
  display: flex;
  gap: 10px;
  margin-bottom: 15px;
}

input,
select {
  padding: 5px 8px;
}
</style>
