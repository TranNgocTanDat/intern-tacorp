import MasterLayout from "@/layouts/MasterLayout.vue";
import { createRouter, createWebHistory } from "vue-router";
import Home from "@/pages/user/HomePage/Home.vue";
import Category from "@/pages/user/CategoryPage/Category.vue";
const routes = [
  {
    path: "/",
    component: MasterLayout,
    children: [
      {
        path: "",
        component: Home,
      },
      {
        path: "category",
        component: Category,
      }
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
