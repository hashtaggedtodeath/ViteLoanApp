<script setup>
import { ref } from "vue";
import { createLoan } from "@/api/loanService.js";
import { useRouter } from "vue-router";
import { ElForm, ElFormItem, ElInput, ElButton, ElMessage } from "element-plus";

const router = useRouter();

const form = ref({
  number: "",
  amount: null,
  termValue: null,
  interestValue: null,
});

const rules = {
  number: [{ required: true, message: "Введите номер заявки", trigger: "blur" }],
  amount: [{ required: true, message: "Сумма должна быть больше 0", trigger: "blur", type: "number", min: 1 }],
  termValue: [{ required: true, message: "Срок займа должен быть больше 0", trigger: "blur", type: "number", min: 1 }],
  interestValue: [{ required: true, message: "Процентная ставка должна быть больше 0", trigger: "blur", type: "number", min: 0.01 }],
};

const formRef = ref(null);

const handleSubmit = () => {
  formRef.value.validate(async (valid) => {
    if (!valid) return;
    try {
      await createLoan(form.value);
      ElMessage.success("Заявка успешно создана!");
      router.push("/");
    } catch (error) {
      console.error(error);
      ElMessage.error("Ошибка при создании заявки");
    }
  });
};
</script>

<template>
  <div class="page" style="max-width:400px; margin:0 auto;">
    <h1>Создание заявки</h1>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="140px">
      <el-form-item label="Номер заявки" prop="number">
        <el-input v-model="form.number" />
      </el-form-item>

      <el-form-item label="Сумма займа" prop="amount">
        <el-input type="number" v-model.number="form.amount" />
      </el-form-item>

      <el-form-item label="Срок займа" prop="termValue">
        <el-input type="number" v-model.number="form.termValue" />
      </el-form-item>

      <el-form-item label="Процентная ставка (%)" prop="interestValue">
        <el-input type="number" v-model.number="form.interestValue" />
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="handleSubmit">Создать заявку</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
