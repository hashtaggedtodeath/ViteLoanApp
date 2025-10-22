import { createRouter, createWebHistory } from "vue-router";
import LoanListView from "@/views/LoanListView.vue";
import LoanCreateView from "@/views/LoanCreateView.vue";

const routes = [
  { path: "/", name: "loans", component: LoanListView },
  { path: "/create", name: "create", component: LoanCreateView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
