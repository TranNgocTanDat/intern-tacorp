// src/store/authStore.ts
import { defineStore } from "pinia";
import type { LoginRequest, LoginResponse } from "@/models/Authentication";
import authApi from "@/services/authApi";
import router from "@/router/Router";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    user: null as LoginResponse | null,
    token: localStorage.getItem("token") || "",
    loading: false,
  }),

  getters: {
    isAuthenticated: (state) => !!state.token,
  },

  actions: {
    async login(request: LoginRequest) {
      this.loading = true;
      try {
        const response = await authApi.login(request);
        this.user = response;
        this.token = response.accessToken;
        localStorage.setItem("token", response.accessToken);

        // Điều hướng tới dashboard sau khi login
        router.push("/dashboard");
      } catch (error) {
        console.error("Login failed:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    logout() {
      this.user = null;
      this.token = "";
      localStorage.removeItem("token");
      router.push("/login");
    },
  },
});
