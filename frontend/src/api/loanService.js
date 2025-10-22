import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL + '/api/loanapplications',
});



export const getLoans = (filters = {}) => api.get("/", { params: filters });

export const createLoan = (data) => api.post("/", data);

export const toggleLoanStatus = (id) => api.patch(`/${id}/toggle-status`);
