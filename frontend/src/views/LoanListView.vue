<script setup>
import axios from 'axios'   
import { ref, onMounted } from "vue";
import { ElTable, ElTableColumn, ElButton, ElSelect, ElOption, ElInput } from "element-plus";


const loans = ref([]);

const loading = ref(false);


const filters = ref({
  status: "",
  minAmount: "",
  maxAmount: "",
  minTerm: "",
  maxTerm: "",
});


const API_URL = import.meta.env.VITE_API_URL;

const fetchLoans = async () => {
  loading.value = true;
  try {
    const params = {
      status: filters.value.status,
      minAmount: filters.value.minAmount || undefined,
      maxAmount: filters.value.maxAmount || undefined,
      minTerm: filters.value.minTerm || undefined,
      maxTerm: filters.value.maxTerm || undefined,
    };
    const response = await axios.get(`${API_URL}/api/loanapplications`, { params });
    loans.value = response.data;
  } catch (error) {
    console.error(error);
  } finally {
    loading.value = false;
  }
};

async function toggleStatus(loan) {
  try {
    await axios.patch(`${API_URL}/api/loanapplications/${loan.id}/toggle-status`);
    loan.status = loan.status === 'Published' ? 'Unpublished' : 'Published';
  } catch (err) {
    console.error('Ошибка при смене статуса', err);
  }
}



onMounted(fetchLoans);
</script>

<template>
  <div class="page">
    <h1>Список заявок</h1>

    
    <div class="filters" style="margin-bottom:15px; display:flex; gap:10px;">
      <el-select v-model="filters.status" placeholder="Статус" @change="fetchLoans" clearable>
        <el-option label="Все" value=""></el-option>
        <el-option label="Опубликованные" value="Published"></el-option>
        <el-option label="Снятые с публикации" value="Unpublished"></el-option>
      </el-select>

      <el-input v-model.number="filters.minAmount" placeholder="Мин. сумма" @input="fetchLoans" />
      <el-input v-model.number="filters.maxAmount" placeholder="Макс. сумма" @input="fetchLoans" />
      <el-input v-model.number="filters.minTerm" placeholder="Мин. срок" @input="fetchLoans" />
      <el-input v-model.number="filters.maxTerm" placeholder="Макс. срок" @input="fetchLoans" />
    </div>

   
    <el-table :data="loans" v-loading="loading" style="width: 100%">
      <el-table-column prop="number" label="Номер" />
      <el-table-column prop="amount" label="Сумма" />
      <el-table-column prop="termValue" label="Срок" />
      <el-table-column prop="interestValue" label="Процент (%)" />
      <el-table-column prop="status" label="Статус" />
      <el-table-column label="Дата создания">
        <template #default="{ row }">{{ new Date(row.createdAt).toLocaleString() }}</template>
      </el-table-column>
      <el-table-column label="Действие">
        <template #default="{ row }">
          <el-button
            :type="row.status === 'Published' ? 'danger' : 'success'"
            size="small"
            @click="toggleStatus(row)"
          >
            {{ row.status === "Published" ? "Снять с публикации" : "Опубликовать" }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
