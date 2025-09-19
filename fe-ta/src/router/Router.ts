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
    meta: { requiresAuth: true }, // chặn toàn bộ children nếu cần đăng nhập
    children: [
      {
        path: "/dashboard",
        component: DashboardPage,
        meta: { requiresAuth: true }
      },
      {
        path: "/admin-user",
        component: AdminUserPage,
        meta: { requiresAuth: true }
      },
      {
        path: "/analytics-admin",
        component: AnalyticsPageAdmin,
        meta: { requiresAuth: true }
      },
      {
        path: "/category-admin",
        component: CategoryPageAdmin,
        meta: { requiresAuth: true }
      },
      {
        path: "/product-admin",
        component: ProductPageAdmin,
        meta: { requiresAuth: true }
      },
      {
        path:"/hero-section-admin",
        component: HeroSectionPageAdmin,
        meta: { requiresAuth: true }
      },
      {
        path: "/contact-admin",
        component: ContactPageAdmin,
        meta: { requiresAuth: true }
      },
      {
        path: "/partner-admin",
        component: PartnerPageAdmin,
        meta: { requiresAuth: true }
      }
    ]
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// ✅ Thêm navigation guard tại đây
router.beforeEach((to, _from, next) => {
  const token = localStorage.getItem('token'); // Đổi tên key nếu backend bạn dùng khác

  // Nếu route cần đăng nhập và không có token => chuyển về /login
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!token) {
      return next('/login');
    }
  }

  // Nếu đã đăng nhập rồi mà vào /login => chuyển hướng về /dashboard
  if (to.path === '/login' && token) {
    return next('/dashboard');
  }

  next();
});

export default router;
