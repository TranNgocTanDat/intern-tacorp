import MasterLayout from "@/layouts/MasterLayout.vue";
import { createRouter, createWebHistory } from "vue-router";
import Home from "@/pages/user/HomePage/Home.vue";
import Category from "@/pages/user/CategoryPage/Category.vue";
import LoginPage from '@/pages/admin/Login/LoginPage.vue';
import MasterLayoutAdmin from "@/layouts/MasterLayoutAdmin.vue";
import DashboardPage from "@/pages/admin/dashboard/DashboardPage.vue";
import AdminUserPage from '@/pages/admin/admin-user/AdminUserPage.vue';
import AnalyticsPageAdmin from "@/pages/admin/analytics/AnalyticsPageAdmin.vue";
import CategoryPageAdmin from "@/pages/admin/category/CategoryPageAdmin.vue";
import ProductPageAdmin from '@/pages/admin/product/ProductPageAdmin.vue';
import HeroSectionPageAdmin from '@/pages/admin/hero-section/HeroSectionPageAdmin.vue';
import ContactPageAdmin from '@/pages/admin/contact/ContactPageAdmin.vue';
import PartnerPageAdmin from '@/pages/admin/partner/PartnerPageAdmin.vue';
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
        path: "category/:slug?",
        component: Category,
        props: true,
      }
    ],
  },
  {
    path: "/login",
    component: LoginPage
  },
  {
    path: "/dashboard",
    component: MasterLayoutAdmin,
    children: [
      {
        path: "/dashboard",
        component: DashboardPage
      },
      {
        path: "/admin-user",
        component: AdminUserPage
      },
      {
        path: "/analytics-admin",
        component: AnalyticsPageAdmin
      },
      {
        path: "/category-admin",
        component: CategoryPageAdmin
      },
      {
        path: "/product-admin",
        component: ProductPageAdmin
      },
      {
        path:"/hero-section-admin",
        component: HeroSectionPageAdmin
      },
      {
        path: "/contact-admin",
        component: ContactPageAdmin
      },
      {
        path: "/partner-admin",
        component: PartnerPageAdmin
      }

    ]
  }

];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
