// src/services/api.ts

import axios, { type AxiosRequestConfig } from "axios";

// Lấy từ biến môi trường trong Vite
const DOMAIN = "http://localhost:5271/api";

const api = axios.create({
  proxy: false,
  baseURL: DOMAIN,
});

// Xử lý lỗi response
api.interceptors.response.use(
  (response) => response.data,
  (error) => {
    console.error(error);
    if (axios.isAxiosError(error)) {
      if (error.code === "ERR_NETWORK") {
        console.log("Kết nối thất bại...");
      } else if (error.code === "ERR_CANCELED") {
        console.log("Kết nối bị huỷ...");
      }

      const response = error.response;
      const request = error.request;

      if (response) {
        return Promise.reject(response.data); // Trả ra phần data của lỗi
      } else if (request) {
        console.warn("No response received");
      }
    }

    return Promise.reject(error);
  }
);

// Thêm token vào header nếu có
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Export hàm dùng chung
export default {
  async get<T>(endpoint: string, option?: AxiosRequestConfig): Promise<T> {
    return await api.get(endpoint, option);
  },
  async post<T>(endpoint: string, data?: any, option?: AxiosRequestConfig): Promise<T> {
    return await api.post(endpoint, data, option);
  },
  async put<T>(endpoint: string, data?: any, option?: AxiosRequestConfig): Promise<T> {
    return await api.put(endpoint, data, option);
  },
  async delete<T>(endpoint: string, option?: AxiosRequestConfig): Promise<T> {
    return await api.delete(endpoint, option);
  },
  setDefaultHeader(key: string, data?: string) {
    api.defaults.headers.common[key] = data ?? "";
    console.log(`Set header: ${key} = ${data}`);
  },
};
