<script setup lang="ts">
import { RouterView, RouterLink } from "vue-router";
</script>

<template>
  <div class="admin-root">
    <!-- Sidebar -->
    <aside class="admin-sidebar">
      <h2 class="brand">Quản lý hệ thống</h2>
      <nav class="nav">
        <RouterLink to="/dashboard" class="nav-link">Dashboard</RouterLink>
        <RouterLink to="/admin-user" class="nav-link">Admin</RouterLink>
        <RouterLink to="/hero-section-admin" class="nav-link">Hero Section</RouterLink>
        <RouterLink to="/category-admin" class="nav-link">Danh mục</RouterLink>
        <RouterLink to="/product-admin" class="nav-link">Sản phẩm</RouterLink>
        <RouterLink to="/contact-admin" class="nav-link">Liên Hệ</RouterLink>
        <RouterLink to="/partner-admin" class="nav-link">Đối tác</RouterLink>
        <RouterLink to="/analytics-admin" class="nav-link">Thống kê</RouterLink>
      </nav>
    </aside>

    <!-- Main content -->
    <main class="admin-main">
      <RouterView />
    </main>
  </div>
</template>

<style scoped>
.admin-root {
  height: 100vh;
  width: 100vw;
  display: flex;
  overflow: hidden;
}
.admin-sidebar {
  width: 16rem; /* 64 */
  background: #1f2937; /* gray-800 */
  color: #fff;
  padding: 1rem;
  display: flex;
  flex-direction: column;
}
.admin-sidebar .brand {
  font-size: 1.125rem; /* text-xl */
  font-weight: 700;
  margin-bottom: 1.5rem;
}
.nav {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}
.nav-link {
  display: inline-block;
  padding: 0.5rem;
  border-radius: 0.375rem;
  color: inherit;
  text-decoration: none;
  transition: background-color 0.15s ease;
}
.nav-link:hover {
  background: #4b5563; /* gray-600 */
}
.admin-main {
  flex: 1 1 auto;
  padding: 1.5rem;
  background: #f3f4f6; /* gray-100 */
  overflow-y: auto;
}
</style>
