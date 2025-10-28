import { defineStore } from "pinia";

export const useAppStore = defineStore("appStore", {
  state: () => ({}), // nếu sau này cần thêm state global thì thêm ở đây
  actions: {
    reloadPage() {
      window.location.reload();
    },
    goHome() {
      window.location.href = "/";
    },
    scrollToTop() {
      window.scrollTo({ top: 0, behavior: "smooth" });
    },
  },
});