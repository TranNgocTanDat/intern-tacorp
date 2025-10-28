<script setup lang="ts"></script>

<template>
  <div class="admin-root">
    <el-menu
      class="el-menu-vertical-demo admin-sidebar"
      background-color="#1f2937"
      text-color="#fff"
      active-text-color="#ffd04b"
      router
    >
      <el-menu-item index="/dashboard">Dashboard</el-menu-item>
      <el-menu-item index="/admin-user">Admin</el-menu-item>

      <el-sub-menu index="hero-section">
        <template #title>
          <span>Hero Section</span>
        </template>
        <el-menu-item index="/hero-section-admin"
          >Hero Section Main</el-menu-item
        >
        <el-menu-item index="/hero-section-product-admin"
          >Hero Section Product</el-menu-item
        >
      </el-sub-menu>

      <el-menu-item index="/category-admin">Danh mục</el-menu-item>
      <el-sub-menu index="product-admin">
        <template #title>
          <span>Sản phẩm</span>
        </template>
        <el-menu-item index="/product-admin">Tất cả sản phẩm</el-menu-item>
        <el-menu-item index="/product-media">Media</el-menu-item>
        <el-menu-item index="/product-spec">Thông số</el-menu-item>
        <el-menu-item index="/product-color">Màu sắc</el-menu-item>
        <el-menu-item index="/product-storage">Bô nhớ</el-menu-item>
      </el-sub-menu>
      <el-menu-item index="/contact-admin">Liên hệ</el-menu-item>
      <el-menu-item index="/partner-admin">Đối tác</el-menu-item>
      <el-menu-item index="/analytics-admin">Thống kê</el-menu-item>
    </el-menu>

    <main class="admin-main" :style="{ width: '80%' }">
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
  width: 17%; /* 64 */
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
.nav-sub {
  padding-left: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.nav-sublink {
  padding: 0.25rem 0.5rem;
  border-radius: 0.375rem;
  color: #d1d5db; /* gray-300 */
  text-decoration: none;
  font-size: 0.875rem;
}
</style>
